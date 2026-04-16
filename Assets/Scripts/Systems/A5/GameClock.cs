using UnityEngine;
using EventideAge.Core;

namespace EventideAge.Systems.A5
{
    public class GameClock : GameSystem
    {
        public string CurrentTimeDisplay => GetCurrentTimeDisplay();
        public float GameProgress => GetGameProgress();
        
        public override void Initialize(GameState state, GameEvents events)
        {
            base.Initialize(state, events);
            Events.OnTurnChanged += HandleTurnChanged;
        }
        
        private void OnDestroy()
        {
            Events.OnTurnChanged -= HandleTurnChanged;
        }
        
        public string GetCurrentTimeDisplay()
        {
            return FormatTurnAsHalfYear(State.CurrentTurn);
        }
        
        public float GetGameProgress()
        {
            return (float)State.CurrentTurn / Core.GameConfig.kMaxTurns;
        }
        
        public bool IsGameNearEnd()
        {
            return State.CurrentTurn >= Core.GameConfig.kMaxTurns - 4;
        }
        
        public int TurnsRemaining()
        {
            return Mathf.Max(0, Core.GameConfig.kMaxTurns - State.CurrentTurn);
        }
        
        public override void OnTurnEnded(int turnNumber)
        {
            if (turnNumber >= Core.GameConfig.kMaxTurns)
            {
                Debug.Log($"[GameClock] Max turns reached ({Core.GameConfig.kMaxTurns}). Timeout ownership is handled by VictoryDefeatSystem.");
            }
        }
        
        private void HandleTurnChanged(int oldTurn, int newTurn)
        {
            Debug.Log($"[GameClock] Turn changed: {oldTurn} → {newTurn}, Time: {GetCurrentTimeDisplay()}");
        }

        public static string FormatTurnAsHalfYear(int turn)
        {
            int safeTurn = Mathf.Max(1, turn);
            int year = Core.GameConfig.kStartingYear + ((safeTurn - 1) / 2);
            string half = (safeTurn % 2 == 1) ? "H1" : "H2";
            return $"{year} {half}";
        }
    }
}
