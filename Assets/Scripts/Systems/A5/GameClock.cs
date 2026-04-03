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
            int year = Core.GameConfig.kStartingYear;
            float timeValue = State.CurrentTurn * 0.5f;
            return $"{year + (int)timeValue}.{(State.CurrentTurn % 2 == 0 ? 0 : 5)}";
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
                Debug.Log($"[GameClock] Max turns reached ({Core.GameConfig.kMaxTurns}). Game ending due to time.");
                Events.GameEnded("time_limit_reached");
            }
        }
        
        private void HandleTurnChanged(int oldTurn, int newTurn)
        {
            Debug.Log($"[GameClock] Turn changed: {oldTurn} → {newTurn}, Time: {GetCurrentTimeDisplay()}");
            
            if (newTurn >= Core.GameConfig.kMaxTurns)
            {
                Events.GameEnded("time_limit_reached");
            }
        }
    }
}
