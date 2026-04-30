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
    public class MapPanelUI : GameSystem
    {
        [Header("UI References")]
        public GameObject MapPanel;
        public Text LatestMapText;
        public Text MapHistoryText;

        [Header("Display")]
        public int MaxEntries = 20;
        public bool EnableTurnSummary = true;

        private sealed class NodeTurnAggregate
        {
            public int Turn;
            public string NodeId;
            public string InitialController;
            public string CurrentController;
            public int LatestControlPoints;
            public int UpdateCount;
        }

        private sealed class MapEntry
        {
            public long Sequence;
            public FeedbackSeverity Severity;
            public string Line;
        }

        private readonly List<MapEntry> _entries = new List<MapEntry>();
        private readonly Dictionary<string, NodeTurnAggregate> _nodeAggregates = new Dictionary<string, NodeTurnAggregate>(StringComparer.OrdinalIgnoreCase);
        private readonly Dictionary<string, int> _nodeEntryIndexes = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
        private readonly HashSet<string> _turnDedupKeys = new HashSet<string>(StringComparer.Ordinal);
        private int _aggregationTurn = -1;
        private int _dedupTurn = -1;
        private long _nextSequence = 1;
        private long _turnStartSequence = 1;
        private LocalizationSystem _localizationSystem;

        public override void Initialize(GameState state, GameEvents events)
        {
            base.Initialize(state, events);
            Events.OnNodeControlChanged += HandleNodeControlChanged;
            Events.OnConsequenceAdded += HandleConsequenceAdded;
            Events.OnIntelReportAdded += HandleIntelReportAdded;
            Events.OnTurnChanged += HandleTurnChanged;

            if (MapPanel != null)
            {
                MapPanel.SetActive(true);
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

            Events.OnNodeControlChanged -= HandleNodeControlChanged;
            Events.OnConsequenceAdded -= HandleConsequenceAdded;
            Events.OnIntelReportAdded -= HandleIntelReportAdded;
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

        private void HandleNodeControlChanged(string nodeId, string oldController, string newController, int controlPoints)
        {
            EnsureTurnAggregation();

            string canonicalNodeId = UiCanonicalText.CanonicalizeNodeId(nodeId);
            string canonicalOldController = UiCanonicalText.CanonicalizeFactionId(oldController);
            string canonicalNewController = UiCanonicalText.CanonicalizeFactionId(newController);

            if (!_nodeAggregates.TryGetValue(canonicalNodeId, out var aggregate))
            {
                aggregate = new NodeTurnAggregate
                {
                    Turn = _aggregationTurn,
                    NodeId = canonicalNodeId,
                    InitialController = canonicalOldController,
                    CurrentController = canonicalNewController,
                    LatestControlPoints = controlPoints,
                    UpdateCount = 1
                };
                _nodeAggregates[canonicalNodeId] = aggregate;
            }
            else
            {
                aggregate.CurrentController = canonicalNewController;
                aggregate.LatestControlPoints = controlPoints;
                aggregate.UpdateCount++;
            }

            string controlSummary;
            if (string.Equals(aggregate.InitialController, aggregate.CurrentController, StringComparison.OrdinalIgnoreCase))
            {
                controlSummary = $"{aggregate.CurrentController} holds";
            }
            else
            {
                controlSummary = $"{aggregate.InitialController} -> {aggregate.CurrentController}";
            }

            string summary = $"[MAP] T{aggregate.Turn} {aggregate.NodeId} net: {controlSummary}, CP {aggregate.LatestControlPoints} (updates x{aggregate.UpdateCount})";
            FeedbackSeverity severity = string.Equals(aggregate.InitialController, aggregate.CurrentController, StringComparison.OrdinalIgnoreCase)
                ? FeedbackSeverity.Info
                : FeedbackSeverity.Critical;
            summary = UiSurfaceSemantics.AppendMeta(
                summary,
                severity,
                $"H1.{canonicalNodeId}",
                summary,
                UiSurfaceTarget.Map);
            UpsertNodeEntry(canonicalNodeId, summary, severity);
        }

        private void HandleConsequenceAdded(string sourceActionId, string description, int durationTurns, bool reversible)
        {
            if (!IsMapRelatedAction(sourceActionId))
            {
                return;
            }

            string durationLabel = durationTurns < 0 ? "Persistent" : $"{durationTurns}T";
            string canonicalSourceId = UiCanonicalText.CanonicalizeSourceId(sourceActionId);
            string canonicalDescription = UiCanonicalText.CanonicalizeMessage(description);
            FeedbackSeverity severity = (!reversible || durationTurns < 0) ? FeedbackSeverity.Warning : FeedbackSeverity.Info;
            string dedupeKey = $"MAP-CQ|{canonicalSourceId}|{canonicalDescription}|{durationTurns}|{reversible}";
            string line = $"[MAP CONSEQUENCE] [{canonicalSourceId}] {canonicalDescription} ({durationLabel})";
            line = UiSurfaceSemantics.AppendMeta(line, severity, canonicalSourceId, canonicalDescription, UiSurfaceTarget.Map);
            PushEntry(line, severity, dedupeKey);
        }

        private void HandleIntelReportAdded(string sourceId, string message, FeedbackSeverity severity)
        {
            if (!sourceId.StartsWith("D1.", StringComparison.Ordinal))
            {
                return;
            }

            string canonicalSourceId = UiCanonicalText.CanonicalizeSourceId(sourceId);
            string canonicalMessage = UiCanonicalText.CanonicalizeMessage(message);
            string dedupeKey = $"MAP-INTEL|{canonicalSourceId}|{canonicalMessage}|{severity}";
            string line = $"[INTEL/{severity.ToString().ToUpperInvariant()}] {canonicalMessage}";
            line = UiSurfaceSemantics.AppendMeta(line, severity, canonicalSourceId, canonicalMessage, UiSurfaceTarget.Map);
            PushEntry(line, severity, dedupeKey);
        }

        private bool IsMapRelatedAction(string sourceActionId)
        {
            if (string.IsNullOrWhiteSpace(sourceActionId))
            {
                return false;
            }

            return sourceActionId.StartsWith("D1.SpecialForces.")
                || sourceActionId.StartsWith("D1.ChokepointThreat.")
                || sourceActionId == "D1.TotalWar"
                || sourceActionId == "C2.EnergyTransit.Sign"
                || sourceActionId == "B2.NavalBlockade.Upgrade"
                || sourceActionId == "B2.Countermeasure.LandRoute";
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

            _entries.Add(new MapEntry
            {
                Sequence = _nextSequence++,
                Severity = severity,
                Line = line
            });

            TrimEntries();
            RefreshDisplay();
        }

        private void UpsertNodeEntry(string nodeId, string line, FeedbackSeverity severity)
        {
            if (_nodeEntryIndexes.TryGetValue(nodeId, out var index) && index >= 0 && index < _entries.Count)
            {
                _entries[index].Line = line;
                _entries[index].Severity = severity;
                _entries[index].Sequence = _nextSequence++;
            }
            else
            {
                _entries.Add(new MapEntry
                {
                    Sequence = _nextSequence++,
                    Severity = severity,
                    Line = line
                });

                _nodeEntryIndexes[nodeId] = _entries.Count - 1;
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
                ShiftNodeEntryIndexes(oldestIndex);
            }
        }

        private void ShiftNodeEntryIndexes(int removedIndex)
        {
            var keys = new List<string>(_nodeEntryIndexes.Keys);
            foreach (var key in keys)
            {
                int current = _nodeEntryIndexes[key];
                if (current == removedIndex)
                {
                    _nodeEntryIndexes.Remove(key);
                }
                else if (current > removedIndex)
                {
                    _nodeEntryIndexes[key] = current - 1;
                }
            }
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
            _nodeAggregates.Clear();
            _nodeEntryIndexes.Clear();
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

        private static IEnumerable<MapEntry> GetDisplayOrder(IEnumerable<MapEntry> entries)
        {
            return entries
                .OrderByDescending(entry => GetSeverityWeight(entry.Severity))
                .ThenByDescending(entry => entry.Sequence);
        }

        private static MapEntry GetTopPriorityEntry(List<MapEntry> entries)
        {
            if (entries.Count == 0)
            {
                return null;
            }

            MapEntry best = entries[0];
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
            int hotspotTransfers = turnEntries.Count(entry => entry.Line.StartsWith("[MAP] T", StringComparison.Ordinal) && entry.Line.Contains("->", StringComparison.Ordinal));

            string summary = $"[TURN {turn} SUMMARY] map events {turnEntries.Count} | hotspots {hotspotTransfers} | C/W/I {criticalCount}/{warningCount}/{infoCount}";
            FeedbackSeverity severity = (hotspotTransfers > 0 || criticalCount > 0) ? FeedbackSeverity.Warning : FeedbackSeverity.Info;
            string dedupeKey = $"MAP-TURN-SUMMARY|{turn}";
            summary = UiSurfaceSemantics.AppendMeta(summary, severity, "MAP.TurnSummary", summary, UiSurfaceTarget.Map);
            PushEntry(summary, severity, dedupeKey);
        }

        private string BuildMapDigest()
        {
            var nonSummaryEntries = _entries
                .Where(entry => !entry.Line.StartsWith("[TURN ", StringComparison.Ordinal))
                .ToList();

            int criticalCount = nonSummaryEntries.Count(entry => entry.Severity == FeedbackSeverity.Critical);
            int warningCount = nonSummaryEntries.Count(entry => entry.Severity == FeedbackSeverity.Warning);
            int infoCount = nonSummaryEntries.Count(entry => entry.Severity == FeedbackSeverity.Info);
            int transferCount = nonSummaryEntries.Count(entry => entry.Line.StartsWith("[MAP] T", StringComparison.Ordinal) && entry.Line.Contains("->", StringComparison.Ordinal));

            return $"[MAP DIGEST] entries {nonSummaryEntries.Count} | hotspot transfers {transferCount} | C/W/I {criticalCount}/{warningCount}/{infoCount}";
        }

        private void RefreshDisplay()
        {
            if (LatestMapText != null)
            {
                MapEntry latest = GetTopPriorityEntry(_entries);
                if (latest == null)
                {
                    LatestMapText.text = "No map updates.";
                }
                else if (latest.Severity == FeedbackSeverity.Critical)
                {
                    LatestMapText.text = $"[{Localize("ui.map.hotspot", "HOTSPOT")}] {latest.Line}";
                }
                else
                {
                    LatestMapText.text = latest.Line;
                }
            }

            if (MapHistoryText != null)
            {
                if (_entries.Count == 0)
                {
                    MapHistoryText.text = "No entries.";
                    return;
                }

                var sb = new StringBuilder();
                sb.AppendLine(BuildMapDigest());
                sb.AppendLine();
                foreach (var entry in GetDisplayOrder(_entries))
                {
                    sb.AppendLine(entry.Line);
                }

                MapHistoryText.text = sb.ToString().TrimEnd();
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
