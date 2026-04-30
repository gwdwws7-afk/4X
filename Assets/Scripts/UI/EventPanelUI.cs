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
    public class EventPanelUI : GameSystem
    {
        [Header("UI References")]
        public GameObject EventPanel;
        public Text LatestEventText;
        public Text EventHistoryText;

        [Header("Display")]
        public int MaxEntries = 16;
        public bool EnableTurnSummary = true;

        private sealed class EventEntry
        {
            public long Sequence;
            public bool IsStory;
            public FeedbackSeverity Severity;
            public string Line;
        }

        private readonly List<EventEntry> _events = new List<EventEntry>();
        private readonly HashSet<string> _turnDedupKeys = new HashSet<string>(StringComparer.Ordinal);
        private long _nextSequence = 1;
        private int _dedupTurn = -1;
        private long _turnStartSequence = 1;
        private LocalizationSystem _localizationSystem;

        public override void Initialize(GameState state, GameEvents events)
        {
            base.Initialize(state, events);
            Events.OnNarrativeEventAdded += HandleNarrativeEventAdded;
            Events.OnNotificationAdded += HandleNotificationAdded;
            Events.OnAlertAdded += HandleAlertAdded;
            Events.OnGlobalAlertRaised += HandleGlobalAlertRaised;
            Events.OnTurnChanged += HandleTurnChanged;

            if (EventPanel != null)
            {
                EventPanel.SetActive(true);
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
                Events.OnNarrativeEventAdded -= HandleNarrativeEventAdded;
                Events.OnNotificationAdded -= HandleNotificationAdded;
                Events.OnAlertAdded -= HandleAlertAdded;
                Events.OnGlobalAlertRaised -= HandleGlobalAlertRaised;
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

        private void HandleNarrativeEventAdded(string eventId, string message, FeedbackSeverity severity)
        {
            string canonicalEventId = UiCanonicalText.CanonicalizeSourceId(eventId);
            string canonicalMessage = UiCanonicalText.CanonicalizeMessage(message);
            string line = $"[{severity.ToString().ToUpperInvariant()}] [{canonicalEventId}] {canonicalMessage}";
            string decorated = UiSurfaceSemantics.AppendMeta(line, severity, canonicalEventId, canonicalMessage, UiSurfaceTarget.Event);
            string dedupeKey = $"EV-STORY|{canonicalEventId}|{canonicalMessage}|{severity}";
            PushEvent(decorated, isStory: true, severity: severity, dedupeKey: dedupeKey);
        }

        private void HandleNotificationAdded(string sourceId, string message, FeedbackSeverity severity)
        {
            string canonicalSourceId = UiCanonicalText.CanonicalizeSourceId(sourceId);
            string canonicalMessage = UiCanonicalText.CanonicalizeMessage(message);
            string line = $"[SYSTEM/{severity.ToString().ToUpperInvariant()}] [{canonicalSourceId}] {canonicalMessage}";
            string decorated = UiSurfaceSemantics.AppendMeta(line, severity, canonicalSourceId, canonicalMessage, UiSurfaceTarget.Event);
            string dedupeKey = $"EV-NOTICE|{canonicalSourceId}|{canonicalMessage}|{severity}";
            PushEvent(decorated, isStory: false, severity: severity, dedupeKey: dedupeKey);
        }

        private void HandleAlertAdded(string sourceId, string message, FeedbackSeverity severity)
        {
            string canonicalSourceId = UiCanonicalText.CanonicalizeSourceId(sourceId);
            string canonicalMessage = UiCanonicalText.CanonicalizeMessage(message);
            string line = $"[SYSTEM ALERT/{severity.ToString().ToUpperInvariant()}] [{canonicalSourceId}] {canonicalMessage}";
            string decorated = UiSurfaceSemantics.AppendMeta(line, severity, canonicalSourceId, canonicalMessage, UiSurfaceTarget.Event);
            string dedupeKey = $"EV-ALERT|{canonicalSourceId}|{canonicalMessage}|{severity}";
            PushEvent(decorated, isStory: false, severity: severity, dedupeKey: dedupeKey);
        }

        private void HandleGlobalAlertRaised(string message, FeedbackSeverity severity)
        {
            string canonicalMessage = UiCanonicalText.CanonicalizeMessage(message);
            string line = $"[GLOBAL/{severity.ToString().ToUpperInvariant()}] {canonicalMessage}";
            string decorated = UiSurfaceSemantics.AppendMeta(line, severity, "I1.GlobalAlert", canonicalMessage, UiSurfaceTarget.Event);
            string dedupeKey = $"EV-GLOBAL|{canonicalMessage}|{severity}";
            PushEvent(decorated, isStory: false, severity: severity, dedupeKey: dedupeKey);
        }

        private void PushEvent(string line, bool isStory, FeedbackSeverity severity, string dedupeKey)
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

            _events.Add(new EventEntry
            {
                Sequence = _nextSequence++,
                IsStory = isStory,
                Severity = severity,
                Line = line
            });

            int limit = Mathf.Max(1, MaxEntries);
            while (_events.Count > limit)
            {
                int oldestIndex = 0;
                long oldestSequence = _events[0].Sequence;
                for (int i = 1; i < _events.Count; i++)
                {
                    if (_events[i].Sequence < oldestSequence)
                    {
                        oldestSequence = _events[i].Sequence;
                        oldestIndex = i;
                    }
                }

                _events.RemoveAt(oldestIndex);
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

        private static EventEntry GetTopPriorityEntry(IEnumerable<EventEntry> entries)
        {
            EventEntry best = null;
            foreach (var entry in entries)
            {
                if (best == null)
                {
                    best = entry;
                    continue;
                }

                int entryWeight = GetSeverityWeight(entry.Severity);
                int bestWeight = GetSeverityWeight(best.Severity);
                if (entryWeight > bestWeight || (entryWeight == bestWeight && entry.Sequence > best.Sequence))
                {
                    best = entry;
                }
            }

            return best;
        }

        private static IEnumerable<EventEntry> GetOrderedEntries(IEnumerable<EventEntry> entries)
        {
            return entries
                .OrderByDescending(entry => GetSeverityWeight(entry.Severity))
                .ThenByDescending(entry => entry.Sequence);
        }

        private void EmitTurnSummary(int turn)
        {
            if (turn <= 0)
            {
                return;
            }

            var turnEntries = _events
                .Where(entry => entry.Sequence >= _turnStartSequence)
                .Where(entry => !entry.Line.StartsWith("[TURN ", StringComparison.Ordinal))
                .ToList();

            if (turnEntries.Count == 0)
            {
                return;
            }

            int storyCount = turnEntries.Count(entry => entry.IsStory);
            int systemCount = turnEntries.Count(entry => !entry.IsStory);
            int criticalCount = turnEntries.Count(entry => entry.Severity == FeedbackSeverity.Critical);
            int warningCount = turnEntries.Count(entry => entry.Severity == FeedbackSeverity.Warning);
            int infoCount = turnEntries.Count(entry => entry.Severity == FeedbackSeverity.Info);
            FeedbackSeverity severity = criticalCount > 0 ? FeedbackSeverity.Warning : FeedbackSeverity.Info;
            string summary = $"[TURN {turn} SUMMARY] events {turnEntries.Count} | story/system {storyCount}/{systemCount} | C/W/I {criticalCount}/{warningCount}/{infoCount}";
            string dedupeKey = $"EVENT-TURN-SUMMARY|{turn}";
            summary = UiSurfaceSemantics.AppendMeta(summary, severity, "I1.TurnSummary", summary, UiSurfaceTarget.Event);
            PushEvent(summary, isStory: false, severity: severity, dedupeKey: dedupeKey);
        }

        private string BuildEventDigest()
        {
            var nonSummaryEvents = _events
                .Where(entry => !entry.Line.StartsWith("[TURN ", StringComparison.Ordinal))
                .ToList();

            int storyCount = nonSummaryEvents.Count(entry => entry.IsStory);
            int systemCount = nonSummaryEvents.Count(entry => !entry.IsStory);
            int criticalCount = nonSummaryEvents.Count(entry => entry.Severity == FeedbackSeverity.Critical);
            int warningCount = nonSummaryEvents.Count(entry => entry.Severity == FeedbackSeverity.Warning);
            int infoCount = nonSummaryEvents.Count(entry => entry.Severity == FeedbackSeverity.Info);

            return $"[EVENT DIGEST] entries {nonSummaryEvents.Count} | story/system {storyCount}/{systemCount} | C/W/I {criticalCount}/{warningCount}/{infoCount}";
        }

        private void RefreshDisplay()
        {
            if (LatestEventText != null)
            {
                var storyEntries = _events.Where(entry => entry.IsStory);
                var systemEntries = _events.Where(entry => !entry.IsStory);
                EventEntry latest = GetTopPriorityEntry(storyEntries) ?? GetTopPriorityEntry(systemEntries);
                if (latest == null)
                {
                    LatestEventText.text = "No events.";
                }
                else if (latest.IsStory)
                {
                    LatestEventText.text = $"[{Localize("ui.event.option.preview", "OPTION PREVIEW")}] {latest.Line}";
                }
                else
                {
                    LatestEventText.text = latest.Line;
                }
            }

            if (EventHistoryText != null)
            {
                if (_events.Count == 0)
                {
                    EventHistoryText.text = "No events.";
                    return;
                }

                var sb = new StringBuilder();
                sb.AppendLine(BuildEventDigest());
                sb.AppendLine();
                var storyEvents = GetOrderedEntries(_events.Where(entry => entry.IsStory)).ToList();
                var systemEvents = GetOrderedEntries(_events.Where(entry => !entry.IsStory)).ToList();

                if (storyEvents.Count > 0)
                {
                    sb.AppendLine("[STORY EVENTS]");
                    foreach (var entry in storyEvents)
                    {
                        sb.AppendLine(entry.Line);
                    }
                }

                if (systemEvents.Count > 0)
                {
                    if (sb.Length > 0)
                    {
                        sb.AppendLine();
                    }

                    sb.AppendLine("[SYSTEM EVENTS]");
                    foreach (var entry in systemEvents)
                    {
                        sb.AppendLine(entry.Line);
                    }
                }

                EventHistoryText.text = sb.ToString().TrimEnd();
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
