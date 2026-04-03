using UnityEngine;
using EventideAge.Core;

namespace EventideAge.Systems.A1
{
    public class TurnLoopSystem : GameSystem
    {
        public override void Initialize(GameState state, GameEvents events)
        {
            base.Initialize(state, events);
            Events.OnPhaseChanged += HandlePhaseChanged;
            Events.OnTurnChanged += HandleTurnChanged;
        }
        
        private void OnDestroy()
        {
            Events.OnPhaseChanged -= HandlePhaseChanged;
            Events.OnTurnChanged -= HandleTurnChanged;
        }
        
        public override void OnTurnStarted(int turnNumber)
        {
            State.ActionPointsRemaining = Core.GameConfig.kTotalActionPoints;
            Events.ActionPointsChanged(State.ActionPointsRemaining);
            Debug.Log($"[TurnLoop] Turn {turnNumber} started. AP reset to {State.ActionPointsRemaining}");
        }
        
        public override bool CanExecuteAction(GameAction action)
        {
            if (State.ActionPointsRemaining < action.Cost)
                return false;
            
            foreach (int phase in action.ValidPhases)
            {
                if (phase == State.CurrentPhaseIndex)
                    return true;
            }
            return false;
        }
        
        public override void ExecuteAction(GameAction action)
        {
            if (!CanExecuteAction(action))
            {
                Debug.LogWarning($"[TurnLoop] Cannot execute action {action.ActionId}: insufficient AP or invalid phase");
                return;
            }
            
            if (GameManager.Instance.SpendActionPoints(action.Cost))
            {
                Debug.Log($"[TurnLoop] Executed {action.ActionId}, Cost: {action.Cost}, Remaining AP: {State.ActionPointsRemaining}");
            }
        }
        
        private void HandlePhaseChanged(int newPhaseIndex)
        {
            Debug.Log($"[TurnLoop] Phase changed to index: {newPhaseIndex} ({GetPhaseName(newPhaseIndex)})");
        }
        
        private void HandleTurnChanged(int oldTurn, int newTurn)
        {
            Debug.Log($"[TurnLoop] Turn changed: {oldTurn} -> {newTurn}");
        }
        
        public string GetPhaseName(int phaseIndex)
        {
            if (State.Config == null || State.Config.PhaseConfigs == null)
                return $"Phase_{phaseIndex}";
            
            if (phaseIndex >= 0 && phaseIndex < State.Config.PhaseConfigs.Length)
                return State.Config.PhaseConfigs[phaseIndex].PhaseName;
            
            return $"Phase_{phaseIndex}";
        }
        
        public int GetCurrentPhaseIndex() => State.CurrentPhaseIndex;
        public int GetRemainingAP() => State.ActionPointsRemaining;
    }
}
