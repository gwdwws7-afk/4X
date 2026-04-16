using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using EventideAge.Core;

namespace EventideAge.UI
{
    public class ConsequencePanelUI : GameSystem
    {
        [Header("UI References")]
        public GameObject ConsequencePanel;
        public Text ActiveConsequenceText;

        [Header("Display")]
        public int MaxEntries = 12;

        private readonly List<ConsequenceEntry> _activeEntries = new List<ConsequenceEntry>();

        public override void Initialize(GameState state, GameEvents events)
        {
            base.Initialize(state, events);
            Events.OnConsequenceAdded += HandleConsequenceAdded;
            Events.OnTurnEnded += HandleTurnEnded;

            if (ConsequencePanel != null)
            {
                ConsequencePanel.SetActive(true);
            }

            RefreshDisplay();
        }

        private void OnDestroy()
        {
            if (Events != null)
            {
                Events.OnConsequenceAdded -= HandleConsequenceAdded;
                Events.OnTurnEnded -= HandleTurnEnded;
            }
        }

        private void HandleConsequenceAdded(string sourceActionId, string description, int durationTurns, bool reversible)
        {
            if (durationTurns == 0)
            {
                durationTurns = 1;
            }

            var existing = _activeEntries.Find(e => e.SourceActionId == sourceActionId && e.Description == description);
            if (existing != null)
            {
                existing.DurationTurns = durationTurns;
                existing.Reversible = reversible;
                existing.CreatedTurn = State != null ? State.CurrentTurn : 0;
            }
            else
            {
                _activeEntries.Add(new ConsequenceEntry
                {
                    SourceActionId = sourceActionId,
                    Description = description,
                    DurationTurns = durationTurns,
                    Reversible = reversible,
                    CreatedTurn = State != null ? State.CurrentTurn : 0
                });
            }

            if (_activeEntries.Count > Mathf.Max(1, MaxEntries))
            {
                _activeEntries.RemoveAt(0);
            }

            RefreshDisplay();
        }

        private void HandleTurnEnded(int turnNumber)
        {
            bool changed = false;
            for (int i = _activeEntries.Count - 1; i >= 0; i--)
            {
                if (_activeEntries[i].DurationTurns > 0)
                {
                    _activeEntries[i].DurationTurns--;
                    changed = true;
                    if (_activeEntries[i].DurationTurns <= 0)
                    {
                        _activeEntries.RemoveAt(i);
                    }
                }
            }

            if (changed)
            {
                RefreshDisplay();
            }
        }

        private void RefreshDisplay()
        {
            if (ActiveConsequenceText == null)
            {
                return;
            }

            if (_activeEntries.Count == 0)
            {
                ActiveConsequenceText.text = "No active consequences.";
                return;
            }

            var sb = new StringBuilder();
            foreach (var entry in _activeEntries)
            {
                string durationLabel = entry.DurationTurns < 0
                    ? "Persistent"
                    : $"{entry.DurationTurns}T";

                string reversibleLabel = entry.Reversible ? "Reversible" : "Irreversible";
                sb.AppendLine($"[{entry.SourceActionId}] {entry.Description} ({durationLabel}, {reversibleLabel})");
            }

            ActiveConsequenceText.text = sb.ToString().TrimEnd();
        }

        private class ConsequenceEntry
        {
            public string SourceActionId;
            public string Description;
            public int DurationTurns;
            public bool Reversible;
            public int CreatedTurn;
        }
    }
}
