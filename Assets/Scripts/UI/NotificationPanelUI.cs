using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using EventideAge.Core;

namespace EventideAge.UI
{
    public class NotificationPanelUI : GameSystem
    {
        [Header("UI References")]
        public GameObject NotificationPanel;
        public Text LatestNotificationText;
        public Text NotificationHistoryText;

        [Header("Display")]
        public int MaxEntries = 20;

        private readonly Queue<string> _entries = new Queue<string>();

        public override void Initialize(GameState state, GameEvents events)
        {
            base.Initialize(state, events);
            Events.OnNotificationAdded += HandleNotificationAdded;

            if (NotificationPanel != null)
            {
                NotificationPanel.SetActive(true);
            }

            RefreshDisplay();
        }

        private void OnDestroy()
        {
            if (Events != null)
            {
                Events.OnNotificationAdded -= HandleNotificationAdded;
            }
        }

        private void HandleNotificationAdded(string sourceId, string message, FeedbackSeverity severity)
        {
            string canonicalSourceId = UiCanonicalText.CanonicalizeSourceId(sourceId);
            string canonicalMessage = UiCanonicalText.CanonicalizeMessage(message);
            string line = $"[{severity.ToString().ToUpperInvariant()}] [{canonicalSourceId}] {canonicalMessage}";
            line = UiSurfaceSemantics.AppendMeta(line, severity, canonicalSourceId, canonicalMessage, UiSurfaceTarget.BattleReport);
            _entries.Enqueue(line);
            while (_entries.Count > Mathf.Max(1, MaxEntries))
            {
                _entries.Dequeue();
            }

            RefreshDisplay();
        }

        private void RefreshDisplay()
        {
            if (LatestNotificationText != null)
            {
                string latest = "No notifications.";
                foreach (var item in _entries)
                {
                    latest = item;
                }

                LatestNotificationText.text = latest;
            }

            if (NotificationHistoryText != null)
            {
                if (_entries.Count == 0)
                {
                    NotificationHistoryText.text = "No notifications.";
                    return;
                }

                var sb = new StringBuilder();
                foreach (var item in _entries)
                {
                    sb.AppendLine(item);
                }

                NotificationHistoryText.text = sb.ToString().TrimEnd();
            }
        }
    }
}
