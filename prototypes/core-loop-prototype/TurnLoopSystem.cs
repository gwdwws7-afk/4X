using UnityEngine;
using System.Collections.Generic;

namespace Prototypes.CoreLoop
{
    /// <summary>
    /// TurnLoopSystem - Implements A1 回合主循环
    /// 6-phase structure, 11 action points, AI responds only phase 6
    /// </summary>
    public class TurnLoopSystem : GameSystem
    {
        [Header("Phase Configuration")]
        [SerializeField] private PhaseType[] phaseOrder = new PhaseType[]
        {
            PhaseType.Diplomacy,    // Phase 1: 外交行动
            PhaseType.Strategy,     // Phase 2: 战略部署
            PhaseType.Operations,   // Phase 3: 作战行动
            PhaseType.Logistics,    // Phase 4: 后交补给
            PhaseType.Intelligence,  // Phase 5: 情报活动
            PhaseType.AIResponse    // Phase 6: AI响应
        };
        
        private int universalActionPoints = 2;
        private int[] phaseBasePoints = new int[] { 2, 2, 2, 1, 1, 0 };
        
        public override void Initialize(GameState state, GameEvents events)
        {
            base.Initialize(state, events);
            Events.OnPhaseChanged += HandlePhaseChanged;
            Events.OnTurnEnded += HandleTurnEnded;
        }
        
        private void OnDestroy()
        {
            Events.OnPhaseChanged -= HandlePhaseChanged;
            Events.OnTurnEnded -= HandleTurnEnded;
        }
        
        public void StartNewTurn()
        {
            State.CurrentTurn++;
            State.ActionPointsRemaining = CalculateTotalActionPoints();
            AdvanceToNextPhase();
        }
        
        private int CalculateTotalActionPoints()
        {
            int phasePoints = 0;
            for (int i = 0; i < phaseOrder.Length - 1; i++) // Exclude AI phase
            {
                phasePoints += phaseBasePoints[i];
            }
            return phasePoints + universalActionPoints;
        }
        
        private void AdvanceToNextPhase()
        {
            int currentIndex = (int)State.CurrentPhase;
            int nextIndex = (currentIndex + 1) % phaseOrder.Length;
            PhaseType nextPhase = phaseOrder[nextIndex];
            
            if (nextIndex == 0) // Completed all phases, new turn
            {
                Events.TurnEnded(State.CurrentTurn);
                State.CurrentTurn++;
                State.ActionPointsRemaining = CalculateTotalActionPoints();
            }
            
            State.CurrentPhase = nextPhase;
            Events.PhaseChanged(nextPhase);
        }
        
        public bool SpendActionPoints(int cost, PhaseType currentPhase)
        {
            if (cost > State.ActionPointsRemaining) return false;
            
            // Universal points (2) can be used in any phase
            // Phase-specific points can only be used in that phase
            int phaseIndex = System.Array.IndexOf(phaseOrder, currentPhase);
            int availableInPhase = phaseBasePoints[phaseIndex];
            
            if (cost <= availableInPhase)
            {
                State.ActionPointsRemaining -= cost;
                Events.ActionPointsChanged(State.ActionPointsRemaining);
                return true;
            }
            
            // Fall back to universal points
            int remaining = cost - availableInPhase;
            if (remaining <= universalActionPoints)
            {
                State.ActionPointsRemaining -= cost;
                Events.ActionPointsChanged(State.ActionPointsRemaining);
                return true;
            }
            
            return false;
        }
        
        private void HandlePhaseChanged(PhaseType newPhase)
        {
            Debug.Log($"[TurnLoop] Entering Phase: {newPhase}");
        }
        
        private void HandleTurnEnded(int turnNumber)
        {
            Debug.Log($"[TurnLoop] Turn {turnNumber} completed, starting Turn {turnNumber + 1}");
        }
        
        public PhaseType GetCurrentPhase() => State.CurrentPhase;
        public int GetRemainingAP() => State.ActionPointsRemaining;
    }
}
