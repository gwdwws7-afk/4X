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
            Debug.Log($"[TurnLoop] Turn {turnNumber} started. Turn AP {State.ActionPointsRemaining}, Phase AP {State.CurrentPhaseActionPointsRemaining}, Universal AP {State.UniversalActionPointsRemaining}");
        }
        
        public override bool CanExecuteAction(GameAction action)
        {
            bool canSpend = GameManager.Instance != null
                ? GameManager.Instance.CanSpendActionPoints(action.Cost)
                : State.CanSpendActionPoints(action.Cost);

            if (!canSpend)
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
            
            bool spent = GameManager.Instance != null
                ? GameManager.Instance.SpendActionPoints(action.Cost)
                : State.TrySpendActionPoints(action.Cost);

            if (spent)
            {
                if (GameManager.Instance == null)
                    Events.ActionPointsChanged(State.ActionPointsRemaining);
                Debug.Log($"[TurnLoop] Executed {action.ActionId}, Cost: {action.Cost}, Remaining AP: {State.ActionPointsRemaining} (Phase {State.CurrentPhaseActionPointsRemaining}, Universal {State.UniversalActionPointsRemaining})");
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
