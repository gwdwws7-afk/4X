using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EventideAge.Core;

namespace EventideAge.UI
{
    public class AlertPanelUI : GameSystem
    {
        [Header("UI References")]
        public GameObject AlertPanel;
        public Text AlertText;
        public Image AlertBackground;

        [Header("Severity Colors")]
        public Color InfoColor = new Color(0.2f, 0.4f, 0.8f, 0.9f);
        public Color WarningColor = new Color(0.9f, 0.6f, 0.1f, 0.95f);
        public Color CriticalColor = new Color(0.82f, 0.16f, 0.16f, 0.95f);

        [Header("Display")]
        public int MaxEntries = 8;

        private readonly Queue<AlertItem> _alerts = new Queue<AlertItem>();

        public override void Initialize(GameState state, GameEvents events)
        {
            base.Initialize(state, events);
            Events.OnAlertAdded += HandleAlertAdded;

            if (AlertPanel != null)
            {
                AlertPanel.SetActive(false);
            }
        }

        private void OnDestroy()
        {
            if (Events != null)
            {
                Events.OnAlertAdded -= HandleAlertAdded;
            }
        }

        private void HandleAlertAdded(string sourceId, string message, FeedbackSeverity severity)
        {
            _alerts.Enqueue(new AlertItem
            {
                SourceId = sourceId,
                Message = message,
                Severity = severity
            });

            while (_alerts.Count > Mathf.Max(1, MaxEntries))
            {
                _alerts.Dequeue();
            }

            RenderLatest();
        }

        private void RenderLatest()
        {
            AlertItem latest = null;
            foreach (var item in _alerts)
            {
                latest = item;
            }

            if (latest == null)
            {
                return;
            }

            if (AlertPanel != null)
            {
                AlertPanel.SetActive(true);
            }

            if (AlertText != null)
            {
                AlertText.text = $"[{latest.Severity.ToString().ToUpperInvariant()}] [{latest.SourceId}] {latest.Message}";
            }

            if (AlertBackground != null)
            {
                AlertBackground.color = GetColorBySeverity(latest.Severity);
            }
        }

        private Color GetColorBySeverity(FeedbackSeverity severity)
        {
            switch (severity)
            {
                case FeedbackSeverity.Warning:
                    return WarningColor;
                case FeedbackSeverity.Critical:
                    return CriticalColor;
                default:
                    return InfoColor;
            }
        }

        private class AlertItem
        {
            public string SourceId;
            public string Message;
            public FeedbackSeverity Severity;
        }
    }
}
