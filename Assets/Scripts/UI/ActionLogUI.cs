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
    public class ActionLogUI : GameSystem
    {
        [Header("UI References")]
        public GameObject ActionLogPanel;
        public Text LatestEntryText;
        public Text HistoryText;

        [Header("Display")]
        public int MaxEntries = 20;
        public bool EnableTurnSummary = true;

        private enum BattleStage
        {
            Summary = -1,
            Execution = 0,
            Resolution = 1,
            Consequence = 2
        }

        private sealed class BattleEntry
        {
            public long Sequence;
            public BattleStage Stage;
            public FeedbackSeverity Severity;
            public string Line;
        }

        private readonly List<BattleEntry> _entries = new List<BattleEntry>();
        private readonly HashSet<string> _turnDedupKeys = new HashSet<string>(StringComparer.Ordinal);
        private long _nextSequence = 1;
        private int _dedupTurn = -1;
        private long _turnStartSequence = 1;
        private LocalizationSystem _localizationSystem;

        public override void Initialize(GameState state, GameEvents events)
        {
            base.Initialize(state, events);
            Events.OnActionLogAdded += HandleActionLogAdded;
            Events.OnConsequenceAdded += HandleConsequenceAdded;
            Events.OnTurnChanged += HandleTurnChanged;

            if (ActionLogPanel != null)
            {
                ActionLogPanel.SetActive(true);
            }

            _localizationSystem = ResolveLocalizationSystem();
            _dedupTurn = State != null ? State.CurrentTurn : -1;
            _turnStartSequence = _nextSequence;
            RefreshDisplay();
        }

        private void OnDestroy()
        {
            if (Events != null)
            {
                Events.OnActionLogAdded -= HandleActionLogAdded;
                Events.OnConsequenceAdded -= HandleConsequenceAdded;
                Events.OnTurnChanged -= HandleTurnChanged;
            }
        }

        private void HandleTurnChanged(int oldTurn, int newTurn)
        {
            if (EnableTurnSummary)
            {
                EmitTurnSummary(oldTurn);
            }

            _turnStartSequence = _nextSequence;
            ResetTurnDedup(newTurn);
        }

        private void HandleActionLogAdded(string sourceId, string message, FeedbackSeverity severity)
        {
            string turnLabel = State != null ? $"T{State.CurrentTurn}" : "T?";
            string phaseLabel = State != null ? $"P{State.CurrentPhaseIndex}" : "P?";
            string severityLabel = severity.ToString().ToUpperInvariant();
            string canonicalSourceId = UiCanonicalText.CanonicalizeSourceId(sourceId);
            string canonicalMessage = UiCanonicalText.CanonicalizeMessage(message);

            string line = $"[{turnLabel}/{phaseLabel}] [{severityLabel}] [{canonicalSourceId}] {canonicalMessage}";
            BattleStage stage = ClassifyStage(canonicalSourceId);
            line = UiSurfaceSemantics.AppendMeta(line, severity, canonicalSourceId, canonicalMessage, UiSurfaceTarget.BattleReport);
            string dedupeKey = $"BATTLE-ACT|{stage}|{canonicalSourceId}|{canonicalMessage}|{severity}";
            PushEntry(line, stage, severity, dedupeKey);
        }

        private void HandleConsequenceAdded(string sourceActionId, string description, int durationTurns, bool reversible)
        {
            if (!IsMilitarySource(sourceActionId))
            {
                return;
            }

            string turnLabel = State != null ? $"T{State.CurrentTurn}" : "T?";
            string phaseLabel = State != null ? $"P{State.CurrentPhaseIndex}" : "P?";
            string canonicalSourceId = UiCanonicalText.CanonicalizeSourceId(sourceActionId);
            string canonicalDescription = UiCanonicalText.CanonicalizeMessage(description);
            string durationLabel = durationTurns < 0 ? "Persistent" : $"{durationTurns}T";
            FeedbackSeverity severity = (!reversible || durationTurns < 0) ? FeedbackSeverity.Warning : FeedbackSeverity.Info;
            string line = $"[{turnLabel}/{phaseLabel}] [CONSEQUENCE] [{canonicalSourceId}] {canonicalDescription} ({durationLabel})";
            line = UiSurfaceSemantics.AppendMeta(line, severity, canonicalSourceId, canonicalDescription, UiSurfaceTarget.BattleReport);
            string dedupeKey = $"BATTLE-CQ|{canonicalSourceId}|{canonicalDescription}|{durationTurns}|{reversible}";
            PushEntry(line, BattleStage.Consequence, severity, dedupeKey);
        }

        private void PushEntry(string line, BattleStage stage, FeedbackSeverity severity, string dedupeKey)
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

            _entries.Add(new BattleEntry
            {
                Sequence = _nextSequence++,
                Stage = stage,
                Severity = severity,
                Line = line
            });

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
            }

            RefreshDisplay();
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

        private static bool IsMilitarySource(string sourceId)
        {
            if (string.IsNullOrWhiteSpace(sourceId))
            {
                return false;
            }

            return sourceId.StartsWith("D", StringComparison.Ordinal);
        }

        private static BattleStage ClassifyStage(string sourceId)
        {
            if (string.IsNullOrWhiteSpace(sourceId))
            {
                return BattleStage.Execution;
            }

            if (sourceId.StartsWith("D2.", StringComparison.Ordinal) || sourceId.StartsWith("D5.", StringComparison.Ordinal) || sourceId == "D2" || sourceId == "D5")
            {
                return BattleStage.Resolution;
            }

            if (sourceId.StartsWith("D1.", StringComparison.Ordinal)
                || sourceId.StartsWith("D3.", StringComparison.Ordinal)
                || sourceId.StartsWith("D4.", StringComparison.Ordinal)
                || sourceId.StartsWith("D6.", StringComparison.Ordinal)
                || sourceId == "D1"
                || sourceId == "D3"
                || sourceId == "D4"
                || sourceId == "D6")
            {
                return BattleStage.Execution;
            }

            return BattleStage.Execution;
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

        private string GetStageHeader(BattleStage stage)
        {
            switch (stage)
            {
                case BattleStage.Summary:
                    return $"=== {Localize("ui.report.summary", "TURN SUMMARY")} ===";
                case BattleStage.Execution:
                    return "=== ACTION EXECUTION ===";
                case BattleStage.Resolution:
                    return "=== RESOLUTION ===";
                case BattleStage.Consequence:
                    return "=== CONSEQUENCE ===";
                default:
                    return "=== ACTION EXECUTION ===";
            }
        }

        private static BattleEntry GetTopPriorityEntry(List<BattleEntry> entries)
        {
            if (entries.Count == 0)
            {
                return null;
            }

            BattleEntry best = entries[0];
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

            int executionCount = turnEntries.Count(entry => entry.Stage == BattleStage.Execution);
            int resolutionCount = turnEntries.Count(entry => entry.Stage == BattleStage.Resolution);
            int consequenceCount = turnEntries.Count(entry => entry.Stage == BattleStage.Consequence);
            int criticalCount = turnEntries.Count(entry => entry.Severity == FeedbackSeverity.Critical);
            int warningCount = turnEntries.Count(entry => entry.Severity == FeedbackSeverity.Warning);
            int infoCount = turnEntries.Count(entry => entry.Severity == FeedbackSeverity.Info);
            FeedbackSeverity severity = criticalCount > 0 ? FeedbackSeverity.Warning : FeedbackSeverity.Info;
            string summary = $"[TURN {turn} SUMMARY] battle events {turnEntries.Count} | E/R/C {executionCount}/{resolutionCount}/{consequenceCount} | C/W/I {criticalCount}/{warningCount}/{infoCount}";
            string dedupeKey = $"BATTLE-TURN-SUMMARY|{turn}";
            summary = UiSurfaceSemantics.AppendMeta(summary, severity, "D.TurnSummary", summary, UiSurfaceTarget.BattleReport);
            PushEntry(summary, BattleStage.Summary, severity, dedupeKey);
        }

        private string BuildBattleDigest()
        {
            var nonSummaryEntries = _entries
                .Where(entry => entry.Stage != BattleStage.Summary)
                .ToList();

            int executionCount = nonSummaryEntries.Count(entry => entry.Stage == BattleStage.Execution);
            int resolutionCount = nonSummaryEntries.Count(entry => entry.Stage == BattleStage.Resolution);
            int consequenceCount = nonSummaryEntries.Count(entry => entry.Stage == BattleStage.Consequence);
            int criticalCount = nonSummaryEntries.Count(entry => entry.Severity == FeedbackSeverity.Critical);
            int warningCount = nonSummaryEntries.Count(entry => entry.Severity == FeedbackSeverity.Warning);
            int infoCount = nonSummaryEntries.Count(entry => entry.Severity == FeedbackSeverity.Info);

            return $"[BATTLE DIGEST] entries {nonSummaryEntries.Count} | stages E/R/C {executionCount}/{resolutionCount}/{consequenceCount} | C/W/I {criticalCount}/{warningCount}/{infoCount}";
        }

        private void RefreshDisplay()
        {
            if (LatestEntryText != null)
            {
                if (_entries.Count == 0)
                {
                    LatestEntryText.text = "No action logs yet.";
                }
                else
                {
                    BattleEntry latest = GetTopPriorityEntry(_entries);
                    LatestEntryText.text = $"[{GetStageHeader(latest.Stage).Replace("=== ", string.Empty).Replace(" ===", string.Empty)}] {latest.Line}";
                }
            }

            if (HistoryText != null)
            {
                if (_entries.Count == 0)
                {
                    HistoryText.text = "No entries.";
                    return;
                }

                var sb = new StringBuilder();
                sb.AppendLine(BuildBattleDigest());
                sb.AppendLine();
                var ordered = _entries
                    .OrderBy(entry => entry.Stage)
                    .ThenByDescending(entry => GetSeverityWeight(entry.Severity))
                    .ThenByDescending(entry => entry.Sequence)
                    .ToList();

                BattleStage? currentStage = null;
                foreach (var entry in ordered)
                {
                    if (currentStage != entry.Stage)
                    {
                        if (sb.Length > 0)
                        {
                            sb.AppendLine();
                        }

                        sb.AppendLine(GetStageHeader(entry.Stage));
                        currentStage = entry.Stage;
                    }

                    sb.AppendLine(entry.Line);
                }

                HistoryText.text = sb.ToString().TrimEnd();
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
