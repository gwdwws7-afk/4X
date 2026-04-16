using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using EventideAge.Core;

namespace EventideAge.UI
{
    public class IntelPanelUI : GameSystem
    {
        [Header("UI References")]
        public GameObject IntelPanel;
        public Text LatestIntelText;
        public Text IntelHistoryText;

        [Header("Display")]
        public int MaxEntries = 20;

        private readonly Queue<string> _intelEntries = new Queue<string>();

        public override void Initialize(GameState state, GameEvents events)
        {
            base.Initialize(state, events);
            Events.OnIntelReportAdded += HandleIntelReportAdded;
            Events.OnNodeControlChanged += HandleNodeControlChanged;

            if (IntelPanel != null)
            {
                IntelPanel.SetActive(true);
            }

            RefreshDisplay();
        }

        private void OnDestroy()
        {
            if (Events != null)
            {
                Events.OnIntelReportAdded -= HandleIntelReportAdded;
                Events.OnNodeControlChanged -= HandleNodeControlChanged;
            }
        }

        private void HandleIntelReportAdded(string sourceId, string message, FeedbackSeverity severity)
        {
            PushLine($"[{severity.ToString().ToUpperInvariant()}] [{sourceId}] {message}");
        }

        private void HandleNodeControlChanged(string nodeId, string oldController, string newController, int controlPoints)
        {
            if (!string.Equals(oldController, newController))
            {
                PushLine($"[INFO] [MapIntel] {nodeId}: {oldController} -> {newController} (CP {controlPoints})");
            }
        }

        private void PushLine(string line)
        {
            _intelEntries.Enqueue(line);
            while (_intelEntries.Count > Mathf.Max(1, MaxEntries))
            {
                _intelEntries.Dequeue();
            }

            RefreshDisplay();
        }

        private void RefreshDisplay()
        {
            if (LatestIntelText != null)
            {
                string latest = "No intel reports.";
                foreach (var item in _intelEntries)
                {
                    latest = item;
                }

                LatestIntelText.text = latest;
            }

            if (IntelHistoryText != null)
            {
                if (_intelEntries.Count == 0)
                {
                    IntelHistoryText.text = "No intel reports.";
                    return;
                }

                var sb = new StringBuilder();
                foreach (var item in _intelEntries)
                {
                    sb.AppendLine(item);
                }

                IntelHistoryText.text = sb.ToString().TrimEnd();
            }
        }
    }
}
