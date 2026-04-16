using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using EventideAge.Core;

namespace EventideAge.UI
{
    public class ActionPanelUI : GameSystem
    {
        [Header("UI References")]
        public GameObject ActionPanel;
        public Text LatestActionText;
        public Text ActionHistoryText;

        [Header("Display")]
        public int MaxEntries = 20;

        private readonly Queue<string> _entries = new Queue<string>();

        public override void Initialize(GameState state, GameEvents events)
        {
            base.Initialize(state, events);
            Events.OnActionLogAdded += HandleActionLogAdded;
            Events.OnConsequenceAdded += HandleConsequenceAdded;

            if (ActionPanel != null)
            {
                ActionPanel.SetActive(true);
            }

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
        }

        private void HandleActionLogAdded(string sourceId, string message, FeedbackSeverity severity)
        {
            if (string.Equals(sourceId, "Core"))
            {
                return;
            }

            string turn = State != null ? $"T{State.CurrentTurn}" : "T?";
            string phase = State != null ? $"P{State.CurrentPhaseIndex}" : "P?";
            PushEntry($"[{turn}/{phase}] [{severity.ToString().ToUpperInvariant()}] [{sourceId}] {message}");
        }

        private void HandleConsequenceAdded(string sourceActionId, string description, int durationTurns, bool reversible)
        {
            string durationLabel = durationTurns < 0 ? "Persistent" : $"{durationTurns}T";
            string reversibleLabel = reversible ? "Reversible" : "Irreversible";
            PushEntry($"[CONSEQUENCE] [{sourceActionId}] {description} ({durationLabel}, {reversibleLabel})");
        }

        private void PushEntry(string line)
        {
            _entries.Enqueue(line);
            while (_entries.Count > Mathf.Max(1, MaxEntries))
            {
                _entries.Dequeue();
            }

            RefreshDisplay();
        }

        private void RefreshDisplay()
        {
            if (LatestActionText != null)
            {
                string latest = "No action records.";
                foreach (var entry in _entries)
                {
                    latest = entry;
                }

                LatestActionText.text = latest;
            }

            if (ActionHistoryText != null)
            {
                if (_entries.Count == 0)
                {
                    ActionHistoryText.text = "No entries.";
                    return;
                }

                var sb = new StringBuilder();
                foreach (var entry in _entries)
                {
                    sb.AppendLine(entry);
                }

                ActionHistoryText.text = sb.ToString().TrimEnd();
            }
        }
    }
}
