using UnityEngine;
using EventideAge.Core;

namespace EventideAge.Systems.A2
{
    public class PhaseEngine : GameSystem
    {
        private int _universalPointsUsed = 0;
        
        public override void Initialize(GameState state, GameEvents events)
        {
            base.Initialize(state, events);
            Debug.Log("[PhaseEngine] Initialized");
        }
        
        public override void OnTurnStarted(int turnNumber)
        {
            _universalPointsUsed = 0;
            ResetToPhase0();
            Events.PhaseChanged(0);
        }
        
        public override void OnPhaseEntered(int phaseIndex)
        {
            _universalPointsUsed = 0;
            Debug.Log($"[PhaseEngine] Entered Phase {phaseIndex}: {GetPhaseName(phaseIndex)}");
        }
        
        public override void OnPhaseExited(int phaseIndex)
        {
            int baseAP = GetPhaseBaseAP(phaseIndex);
            Debug.Log($"[PhaseEngine] Exited Phase {phaseIndex}, Base AP spent: {baseAP}, Universal used: {_universalPointsUsed}");
        }
        
        public void AdvanceToNextPhase()
        {
            int currentPhase = State.CurrentPhaseIndex;
            int totalPhases = State.Config.PhaseConfigs != null ? State.Config.PhaseConfigs.Length : 6;
            
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
            _universalPointsUsed = 0;
            Events.PhaseChanged(nextPhase);
        }
        
        public void ResetToPhase0()
        {
            State.CurrentPhaseIndex = 0;
            _universalPointsUsed = 0;
        }
        
        public bool CanUseUniversalPoints(int cost, int phaseBaseAP)
        {
            int remainingPhaseAP = phaseBaseAP - GetPhaseBaseAP(State.CurrentPhaseIndex);
            int neededFromUniversal = Mathf.Max(0, cost - (GetPhaseBaseAP(State.CurrentPhaseIndex) - remainingPhaseAP));
            int universalAvailable = Core.GameConfig.kUniversalActionPoints - _universalPointsUsed;
            return universalAvailable >= neededFromUniversal;
        }
        
        public void UseUniversalPoints(int amount)
        {
            _universalPointsUsed = Mathf.Min(_universalPointsUsed + amount, Core.GameConfig.kUniversalActionPoints);
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
        
        public int GetUniversalPointsUsed() => _universalPointsUsed;
        public int GetUniversalPointsRemaining() => Core.GameConfig.kUniversalActionPoints - _universalPointsUsed;
    }
}
