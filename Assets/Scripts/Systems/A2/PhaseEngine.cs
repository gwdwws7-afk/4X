using UnityEngine;
using EventideAge.Core;

namespace EventideAge.Systems.A2
{
    public class PhaseEngine : GameSystem
    {
        public override void Initialize(GameState state, GameEvents events)
        {
            base.Initialize(state, events);
            Debug.Log("[PhaseEngine] Initialized");
        }
        
        public override void OnTurnStarted(int turnNumber)
        {
            ResetToPhase0();
            Debug.Log($"[PhaseEngine] Turn {turnNumber} started");
        }
        
        public override void OnPhaseEntered(int phaseIndex)
        {
            Debug.Log($"[PhaseEngine] Entered Phase {phaseIndex}: {GetPhaseName(phaseIndex)}");
        }
        
        public override void OnPhaseExited(int phaseIndex)
        {
            Debug.Log($"[PhaseEngine] Exited Phase {phaseIndex}, Remaining phase AP expired");
        }
        
        public void AdvanceToNextPhase()
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.AdvancePhase();
                return;
            }

            int currentPhase = State.CurrentPhaseIndex;
            int totalPhases = State.Config.PhaseConfigs != null ? State.Config.PhaseConfigs.Length : 6;

            State.ExpireCurrentPhaseActionPoints();
            Events.ActionPointsChanged(State.ActionPointsRemaining);
            Events.PhaseExited(currentPhase);

            int nextPhase = (currentPhase + 1) % totalPhases;

            if (nextPhase == 0)
            {
                int oldTurn = State.CurrentTurn;
                Events.TurnEnded(State.CurrentTurn);
                State.CurrentTurn++;
                Events.TurnChanged(oldTurn, State.CurrentTurn);
            }

            State.CurrentPhaseIndex = nextPhase;
            Events.PhaseChanged(nextPhase);
        }
        
        public void ResetToPhase0()
        {
            State.CurrentPhaseIndex = 0;
        }
        
        public bool CanUseUniversalPoints(int cost, int phaseBaseAP)
        {
            return State.CurrentPhaseIndex != Core.GameConfig.kAiResponsePhaseIndex
                && cost <= State.UniversalActionPointsRemaining;
        }
        
        public void UseUniversalPoints(int amount)
        {
            if (!State.TrySpendUniversalActionPoints(amount))
            {
                Debug.LogWarning($"[PhaseEngine] Cannot spend universal AP: requested {amount}, remaining {State.UniversalActionPointsRemaining}");
                return;
            }

            Events.ActionPointsChanged(State.ActionPointsRemaining);
        }
        
        public int GetPhaseBaseAP(int phaseIndex)
        {
            if (State.Config?.PhaseConfigs == null)
                return 2;
            
            if (phaseIndex >= 0 && phaseIndex < State.Config.PhaseConfigs.Length)
                return State.Config.PhaseConfigs[phaseIndex].BaseActionPoints;
            
            return 2;
        }
        
        public string GetPhaseName(int phaseIndex)
        {
            if (State.Config?.PhaseConfigs == null)
                return $"Phase_{phaseIndex}";
            
            if (phaseIndex >= 0 && phaseIndex < State.Config.PhaseConfigs.Length)
                return State.Config.PhaseConfigs[phaseIndex].PhaseName;
            
            return $"Phase_{phaseIndex}";
        }
        
        public int GetUniversalPointsUsed() => Core.GameConfig.kUniversalActionPoints - State.UniversalActionPointsRemaining;
        public int GetUniversalPointsRemaining() => State.UniversalActionPointsRemaining;
    }
}
