using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EventideAge.Core;

namespace EventideAge.UI
{
    public class GlobalAlertUI : GameSystem
    {
        [Header("UI References")]
        public GameObject GlobalAlertPanel;
        public Text AlertText;
        public Image AlertBackground;

        [Header("Severity Colors")]
        public Color InfoColor = new Color(0.2f, 0.4f, 0.8f, 0.9f);
        public Color WarningColor = new Color(0.85f, 0.6f, 0.1f, 0.95f);
        public Color CriticalColor = new Color(0.8f, 0.15f, 0.15f, 0.95f);

        [Header("Display")]
        public int MaxAlerts = 10;

        private readonly Queue<AlertEntry> _alerts = new Queue<AlertEntry>();

        public override void Initialize(GameState state, GameEvents events)
        {
            base.Initialize(state, events);
            Events.OnGlobalAlertRaised += HandleGlobalAlertRaised;
            Events.OnGameEnded += HandleGameEnded;

            if (GlobalAlertPanel != null)
            {
                GlobalAlertPanel.SetActive(false);
            }
        }

        private void OnDestroy()
        {
            if (Events != null)
            {
                Events.OnGlobalAlertRaised -= HandleGlobalAlertRaised;
                Events.OnGameEnded -= HandleGameEnded;
            }
        }

        private void HandleGlobalAlertRaised(string message, FeedbackSeverity severity)
        {
            PushAlert(message, severity);
        }

        private void HandleGameEnded(string reason)
        {
            PushAlert($"Game ended: {reason}", FeedbackSeverity.Critical);
        }

        private void PushAlert(string message, FeedbackSeverity severity)
        {
            _alerts.Enqueue(new AlertEntry
            {
                Message = message,
                Severity = severity
            });

            while (_alerts.Count > Mathf.Max(1, MaxAlerts))
            {
                _alerts.Dequeue();
            }

            var latest = GetLatestAlert();
            if (latest == null)
            {
                return;
            }

            if (GlobalAlertPanel != null)
            {
                GlobalAlertPanel.SetActive(true);
            }

            if (AlertText != null)
            {
                AlertText.text = $"[{latest.Severity.ToString().ToUpperInvariant()}] {latest.Message}";
            }

            if (AlertBackground != null)
            {
                AlertBackground.color = GetColorBySeverity(latest.Severity);
            }
        }

        private AlertEntry GetLatestAlert()
        {
            AlertEntry latest = null;
            foreach (var entry in _alerts)
            {
                latest = entry;
            }

            return latest;
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

        private class AlertEntry
        {
            public string Message;
            public FeedbackSeverity Severity;
        }
    }
}
