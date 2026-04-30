using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using EventideAge.Core;
using EventideAge.Systems.L4;

namespace EventideAge.UI
{
    public class DiplomacyPanelUI : GameSystem
    {
        [Header("UI References")]
        public GameObject DiplomacyPanel;
        public Text LatestDiplomacyText;
        public Text DiplomacyHistoryText;

        [Header("Display")]
        public int MaxEntries = 20;
        public bool EnableTurnSummary = true;

        private sealed class RelationshipTurnAggregate
        {
            public int Turn;
            public string FactionId;
            public int NetDelta;
            public int UpdateCount;
            public readonly HashSet<string> Reasons = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        }

        private sealed class DiplomacyEntry
        {
            public long Sequence;
            public FeedbackSeverity Severity;
            public string Line;
        }

        private readonly List<DiplomacyEntry> _entries = new List<DiplomacyEntry>();
        private readonly Dictionary<string, RelationshipTurnAggregate> _relationshipAggregates = new Dictionary<string, RelationshipTurnAggregate>(StringComparer.OrdinalIgnoreCase);
        private readonly Dictionary<string, int> _relationshipEntryIndexes = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
        private readonly HashSet<string> _turnDedupKeys = new HashSet<string>(StringComparer.Ordinal);
        private int _aggregationTurn = -1;
        private int _dedupTurn = -1;
        private long _nextSequence = 1;
        private long _turnStartSequence = 1;
        private string _latestReasonHint = string.Empty;
        private LocalizationSystem _localizationSystem;

        public override void Initialize(GameState state, GameEvents events)
        {
            base.Initialize(state, events);
            Events.OnActionLogAdded += HandleActionLogAdded;
            Events.OnConsequenceAdded += HandleConsequenceAdded;
            Events.OnNotificationAdded += HandleNotificationAdded;
            Events.OnAlertAdded += HandleAlertAdded;
            Events.OnRelationshipChanged += HandleRelationshipChanged;
            Events.OnTurnChanged += HandleTurnChanged;

            if (DiplomacyPanel != null)
            {
                DiplomacyPanel.SetActive(true);
            }

            _localizationSystem = ResolveLocalizationSystem();
            _aggregationTurn = State != null ? State.CurrentTurn : -1;
            _dedupTurn = _aggregationTurn;
            _turnStartSequence = _nextSequence;
            RefreshDisplay();
        }

        private void OnDestroy()
        {
            if (Events == null)
            {
                return;
            }

            Events.OnActionLogAdded -= HandleActionLogAdded;
            Events.OnConsequenceAdded -= HandleConsequenceAdded;
            Events.OnNotificationAdded -= HandleNotificationAdded;
            Events.OnAlertAdded -= HandleAlertAdded;
            Events.OnRelationshipChanged -= HandleRelationshipChanged;
            Events.OnTurnChanged -= HandleTurnChanged;
        }

        private void HandleTurnChanged(int oldTurn, int newTurn)
        {
            if (EnableTurnSummary)
            {
                EmitTurnSummary(oldTurn);
            }

            _turnStartSequence = _nextSequence;
            ResetTurnAggregation(newTurn);
            ResetTurnDedup(newTurn);
        }

        private void HandleActionLogAdded(string sourceId, string message, FeedbackSeverity severity)
        {
            if (!IsDiplomacySource(sourceId))
            {
                return;
            }

            string canonicalSourceId = UiCanonicalText.CanonicalizeSourceId(sourceId);
            string canonicalMessage = UiCanonicalText.CanonicalizeMessage(message);
            TrackReasonHint(canonicalSourceId, canonicalMessage);
            string dedupeKey = $"DIP-ACT|{canonicalSourceId}|{canonicalMessage}|{severity}";
            string line = $"[{severity.ToString().ToUpperInvariant()}] [{canonicalSourceId}] {canonicalMessage}";
            line = UiSurfaceSemantics.AppendMeta(line, severity, canonicalSourceId, canonicalMessage, UiSurfaceTarget.Diplomacy);
            PushEntry(line, severity, dedupeKey);
        }

        private void HandleConsequenceAdded(string sourceActionId, string description, int durationTurns, bool reversible)
        {
            if (!sourceActionId.StartsWith("C2.", StringComparison.Ordinal))
            {
                return;
            }

            string durationLabel = durationTurns < 0 ? "Persistent" : $"{durationTurns}T";
            string canonicalSourceId = UiCanonicalText.CanonicalizeSourceId(sourceActionId);
            string canonicalDescription = UiCanonicalText.CanonicalizeMessage(description);
            TrackReasonHint(canonicalSourceId, canonicalDescription);
            FeedbackSeverity severity = (!reversible || durationTurns < 0) ? FeedbackSeverity.Warning : FeedbackSeverity.Info;
            string dedupeKey = $"DIP-CQ|{canonicalSourceId}|{canonicalDescription}|{durationTurns}|{reversible}";
            string line = $"[CONSEQUENCE] [{canonicalSourceId}] {canonicalDescription} ({durationLabel})";
            line = UiSurfaceSemantics.AppendMeta(line, severity, canonicalSourceId, canonicalDescription, UiSurfaceTarget.Diplomacy);
            PushEntry(line, severity, dedupeKey);
        }

        private void HandleNotificationAdded(string sourceId, string message, FeedbackSeverity severity)
        {
            if (!IsDiplomacySource(sourceId))
            {
                return;
            }

            string canonicalSourceId = UiCanonicalText.CanonicalizeSourceId(sourceId);
            string canonicalMessage = UiCanonicalText.CanonicalizeMessage(message);
            TrackReasonHint(canonicalSourceId, canonicalMessage);
            string dedupeKey = $"DIP-NOTICE|{canonicalSourceId}|{canonicalMessage}|{severity}";
            string line = $"[NOTICE/{severity.ToString().ToUpperInvariant()}] [{canonicalSourceId}] {canonicalMessage}";
            line = UiSurfaceSemantics.AppendMeta(line, severity, canonicalSourceId, canonicalMessage, UiSurfaceTarget.Diplomacy);
            PushEntry(line, severity, dedupeKey);
        }

        private void HandleAlertAdded(string sourceId, string message, FeedbackSeverity severity)
        {
            if (!IsDiplomacySource(sourceId))
            {
                return;
            }

            string canonicalSourceId = UiCanonicalText.CanonicalizeSourceId(sourceId);
            string canonicalMessage = UiCanonicalText.CanonicalizeMessage(message);
            TrackReasonHint(canonicalSourceId, canonicalMessage);
            string dedupeKey = $"DIP-ALERT|{canonicalSourceId}|{canonicalMessage}|{severity}";
            string line = $"[ALERT/{severity.ToString().ToUpperInvariant()}] [{canonicalSourceId}] {canonicalMessage}";
            line = UiSurfaceSemantics.AppendMeta(line, severity, canonicalSourceId, canonicalMessage, UiSurfaceTarget.Diplomacy);
            PushEntry(line, severity, dedupeKey);
        }

        private void HandleRelationshipChanged(string factionId, int delta)
        {
            EnsureTurnAggregation();

            string canonicalFactionId = UiCanonicalText.CanonicalizeFactionId(factionId);
            if (!_relationshipAggregates.TryGetValue(canonicalFactionId, out var aggregate))
            {
                aggregate = new RelationshipTurnAggregate
                {
                    Turn = _aggregationTurn,
                    FactionId = canonicalFactionId,
                    NetDelta = delta,
                    UpdateCount = 1
                };
                _relationshipAggregates[canonicalFactionId] = aggregate;
            }
            else
            {
                aggregate.NetDelta += delta;
                aggregate.UpdateCount++;
            }

            if (!string.IsNullOrWhiteSpace(_latestReasonHint))
            {
                aggregate.Reasons.Add(_latestReasonHint);
            }
            else
            {
                aggregate.Reasons.Add("[System] RelationshipChanged");
            }

            string netLabel = aggregate.NetDelta >= 0 ? $"+{aggregate.NetDelta}" : aggregate.NetDelta.ToString();
            string reasonsLabel = aggregate.Reasons.Count == 0
                ? "n/a"
                : string.Join(" | ", aggregate.Reasons.Take(2));
            string summary = $"[RELATION] T{aggregate.Turn} {aggregate.FactionId}: net {netLabel} (updates x{aggregate.UpdateCount}) | reasons: {reasonsLabel}";
            FeedbackSeverity severity = GetRelationshipSeverity(aggregate.NetDelta);
            summary = UiSurfaceSemantics.AppendMeta(
                summary,
                severity,
                $"C1.Relation.{canonicalFactionId}",
                reasonsLabel,
                UiSurfaceTarget.Diplomacy);
            UpsertRelationshipEntry(canonicalFactionId, summary, severity);
        }

        private bool IsDiplomacySource(string sourceId)
        {
            if (string.IsNullOrWhiteSpace(sourceId))
            {
                return false;
            }

            return sourceId.StartsWith("C1.", StringComparison.Ordinal)
                || sourceId.StartsWith("C2.", StringComparison.Ordinal)
                || sourceId == "C1"
                || sourceId == "C2";
        }

        private void PushEntry(string line, FeedbackSeverity severity = FeedbackSeverity.Info, string dedupeKey = null)
        {
            EnsureTurnDedup();

            if (!string.IsNullOrWhiteSpace(dedupeKey))
            {
                if (_turnDedupKeys.Contains(dedupeKey))
                {
                    return;
                }

                _turnDedupKeys.Add(dedupeKey);
            }

            _entries.Add(new DiplomacyEntry
            {
                Sequence = _nextSequence++,
                Severity = severity,
                Line = line
            });

            TrimEntries();
            RefreshDisplay();
        }

        private void UpsertRelationshipEntry(string factionId, string line, FeedbackSeverity severity)
        {
            if (_relationshipEntryIndexes.TryGetValue(factionId, out var index) && index >= 0 && index < _entries.Count)
            {
                _entries[index].Line = line;
                _entries[index].Severity = severity;
                _entries[index].Sequence = _nextSequence++;
            }
            else
            {
                _entries.Add(new DiplomacyEntry
                {
                    Sequence = _nextSequence++,
                    Severity = severity,
                    Line = line
                });

                _relationshipEntryIndexes[factionId] = _entries.Count - 1;
                TrimEntries();
            }

            RefreshDisplay();
        }

        private void TrimEntries()
        {
            int limit = Mathf.Max(1, MaxEntries);
            while (_entries.Count > limit)
            {
                int oldestIndex = 0;
                long oldestSequence = _entries[0].Sequence;
                for (int i = 1; i < _entries.Count; i++)
                {
                    if (_entries[i].Sequence < oldestSequence)
                    {
                        oldestSequence = _entries[i].Sequence;
                        oldestIndex = i;
                    }
                }

                _entries.RemoveAt(oldestIndex);
                ShiftRelationshipEntryIndexes(oldestIndex);
            }
        }

        private void ShiftRelationshipEntryIndexes(int removedIndex)
        {
            var keys = new List<string>(_relationshipEntryIndexes.Keys);
            foreach (var key in keys)
            {
                int current = _relationshipEntryIndexes[key];
                if (current == removedIndex)
                {
                    _relationshipEntryIndexes.Remove(key);
                }
                else if (current > removedIndex)
                {
                    _relationshipEntryIndexes[key] = current - 1;
                }
            }
        }

        private void TrackReasonHint(string sourceId, string message)
        {
            _latestReasonHint = $"[{sourceId}] {message}";
        }

        private void EnsureTurnAggregation()
        {
            int turn = State != null ? State.CurrentTurn : -1;
            if (turn != _aggregationTurn)
            {
                ResetTurnAggregation(turn);
            }
        }

        private void ResetTurnAggregation(int turn)
        {
            _aggregationTurn = turn;
            _latestReasonHint = string.Empty;
            _relationshipAggregates.Clear();
            _relationshipEntryIndexes.Clear();
        }

        private void EnsureTurnDedup()
        {
            int turn = State != null ? State.CurrentTurn : -1;
            if (turn != _dedupTurn)
            {
                ResetTurnDedup(turn);
            }
        }

        private void ResetTurnDedup(int turn)
        {
            _dedupTurn = turn;
            _turnDedupKeys.Clear();
        }

        private static FeedbackSeverity GetRelationshipSeverity(int netDelta)
        {
            if (netDelta <= -6)
            {
                return FeedbackSeverity.Critical;
            }

            if (netDelta <= -3)
            {
                return FeedbackSeverity.Warning;
            }

            return FeedbackSeverity.Info;
        }

        private static int GetSeverityWeight(FeedbackSeverity severity)
        {
            switch (severity)
            {
                case FeedbackSeverity.Critical:
                    return 2;
                case FeedbackSeverity.Warning:
                    return 1;
                default:
                    return 0;
            }
        }

        private static IEnumerable<DiplomacyEntry> GetDisplayOrder(IEnumerable<DiplomacyEntry> entries)
        {
            return entries
                .OrderByDescending(entry => GetSeverityWeight(entry.Severity))
                .ThenByDescending(entry => entry.Sequence);
        }

        private static DiplomacyEntry GetTopPriorityEntry(List<DiplomacyEntry> entries)
        {
            if (entries.Count == 0)
            {
                return null;
            }

            DiplomacyEntry best = entries[0];
            for (int i = 1; i < entries.Count; i++)
            {
                var candidate = entries[i];
                int candidateWeight = GetSeverityWeight(candidate.Severity);
                int bestWeight = GetSeverityWeight(best.Severity);
                if (candidateWeight > bestWeight || (candidateWeight == bestWeight && candidate.Sequence > best.Sequence))
                {
                    best = candidate;
                }
            }

            return best;
        }

        private void EmitTurnSummary(int turn)
        {
            if (turn <= 0)
            {
                return;
            }

            var turnEntries = _entries
                .Where(entry => entry.Sequence >= _turnStartSequence)
                .Where(entry => !entry.Line.StartsWith("[TURN ", StringComparison.Ordinal))
                .ToList();

            if (turnEntries.Count == 0)
            {
                return;
            }

            int criticalCount = turnEntries.Count(entry => entry.Severity == FeedbackSeverity.Critical);
            int warningCount = turnEntries.Count(entry => entry.Severity == FeedbackSeverity.Warning);
            int infoCount = turnEntries.Count(entry => entry.Severity == FeedbackSeverity.Info);
            int relationUpdates = turnEntries.Count(entry => entry.Line.StartsWith("[RELATION]", StringComparison.Ordinal));

            string summary = $"[TURN {turn} SUMMARY] diplomacy events {turnEntries.Count} | relation updates {relationUpdates} | C/W/I {criticalCount}/{warningCount}/{infoCount}";
            FeedbackSeverity severity = criticalCount > 0 ? FeedbackSeverity.Warning : FeedbackSeverity.Info;
            string dedupeKey = $"DIP-TURN-SUMMARY|{turn}";
            summary = UiSurfaceSemantics.AppendMeta(summary, severity, "C1.TurnSummary", summary, UiSurfaceTarget.Diplomacy);
            PushEntry(summary, severity, dedupeKey);
        }

        private string BuildDiplomacyDigest()
        {
            var nonSummaryEntries = _entries
                .Where(entry => !entry.Line.StartsWith("[TURN ", StringComparison.Ordinal))
                .ToList();

            int criticalCount = nonSummaryEntries.Count(entry => entry.Severity == FeedbackSeverity.Critical);
            int warningCount = nonSummaryEntries.Count(entry => entry.Severity == FeedbackSeverity.Warning);
            int infoCount = nonSummaryEntries.Count(entry => entry.Severity == FeedbackSeverity.Info);
            string hotspot = "n/a";
            var worstRelation = _relationshipAggregates.Values
                .OrderBy(value => value.NetDelta)
                .FirstOrDefault();
            if (worstRelation != null)
            {
                hotspot = $"{worstRelation.FactionId} {(worstRelation.NetDelta >= 0 ? "+" : string.Empty)}{worstRelation.NetDelta}";
            }

            return $"[DIPLO DIGEST] entries {nonSummaryEntries.Count} | relation hotspot {hotspot} | C/W/I {criticalCount}/{warningCount}/{infoCount}";
        }

        private void RefreshDisplay()
        {
            if (LatestDiplomacyText != null)
            {
                DiplomacyEntry latest = GetTopPriorityEntry(_entries);
                if (latest == null)
                {
                    LatestDiplomacyText.text = "No diplomacy updates.";
                }
                else if (latest.Line.IndexOf("status:Locked", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    LatestDiplomacyText.text = $"[{Localize("ui.diplomacy.action.locked", "ACTION LOCKED")}] {latest.Line}";
                }
                else if (latest.Line.StartsWith("[RELATION]", StringComparison.Ordinal) && latest.Severity != FeedbackSeverity.Info)
                {
                    LatestDiplomacyText.text = $"[RELATION HOTSPOT] {latest.Line}";
                }
                else
                {
                    LatestDiplomacyText.text = latest.Line;
                }
            }

            if (DiplomacyHistoryText != null)
            {
                if (_entries.Count == 0)
                {
                    DiplomacyHistoryText.text = "No entries.";
                    return;
                }

                var sb = new StringBuilder();
                sb.AppendLine(BuildDiplomacyDigest());
                sb.AppendLine();
                foreach (var entry in GetDisplayOrder(_entries))
                {
                    sb.AppendLine(entry.Line);
                }

                DiplomacyHistoryText.text = sb.ToString().TrimEnd();
            }
        }

        private LocalizationSystem ResolveLocalizationSystem()
        {
            if (GameManager.Instance != null && GameManager.Instance.Systems != null)
            {
                for (int i = 0; i < GameManager.Instance.Systems.Count; i++)
                {
                    if (GameManager.Instance.Systems[i] is LocalizationSystem localization)
                    {
                        return localization;
                    }
                }
            }

            return UnityEngine.Object.FindObjectOfType<LocalizationSystem>();
        }

        private string Localize(string key, string fallback)
        {
            if (_localizationSystem == null)
            {
                return fallback;
            }

            return _localizationSystem.Translate(key, fallback);
        }
    }
}
