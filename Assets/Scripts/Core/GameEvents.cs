using UnityEngine;
using System;

namespace EventideAge.Core
{
    [CreateAssetMenu(fileName = "GameEvents", menuName = "EventideAge/GameEvents")]
    public class GameEvents : ScriptableObject
    {
        public event Action<int, int> OnTurnChanged;
        public event Action<int> OnTurnEnded;
        public event Action<int> OnPhaseChanged;
        public event Action<int> OnPhaseExited;
        public event Action<int> OnActionPointsChanged;
        public event Action<string, int, int> OnResourceChanged;
        public event Action<string, int> OnRelationshipChanged;
        public event Action<string, int, int> OnNodeControlChanged;
        public event Action<string, int> OnFactionPolicyChanged;
        public event Action<string, int> OnVictoryProgressChanged;
        public event Action<string> OnGameEnded;
        
        public void TurnChanged(int oldTurn, int newTurn)
        {
            OnTurnChanged?.Invoke(oldTurn, newTurn);
        }
        
        public void TurnEnded(int turnNumber)
        {
            OnTurnEnded?.Invoke(turnNumber);
        }
        
        public void PhaseChanged(int newPhaseIndex)
        {
            OnPhaseChanged?.Invoke(newPhaseIndex);
        }
        
        public void PhaseExited(int phaseIndex)
        {
            OnPhaseExited?.Invoke(phaseIndex);
        }
        
        public void ActionPointsChanged(int remaining)
        {
            OnActionPointsChanged?.Invoke(remaining);
        }
        
        public void ResourceChanged(string resourceId, int oldAmount, int newAmount)
        {
            OnResourceChanged?.Invoke(resourceId, oldAmount, newAmount);
        }
        
        public void RelationshipChanged(string factionId, int delta)
        {
            OnRelationshipChanged?.Invoke(factionId, delta);
        }
        
        public void NodeControlChanged(string nodeId, string oldController, string newController, int controlPoints)
        {
            OnNodeControlChanged?.Invoke(nodeId, controlPoints, controlPoints);
        }
        
        public void FactionPolicyChanged(string factionId, int satisfaction)
        {
            OnFactionPolicyChanged?.Invoke(factionId, satisfaction);
        }
        
        public void VictoryProgressChanged(string pathId, int progress)
        {
            OnVictoryProgressChanged?.Invoke(pathId, progress);
        }
        
        public void GameEnded(string reason)
        {
            OnGameEnded?.Invoke(reason);
        }
    }
}
