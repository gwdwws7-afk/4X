using UnityEngine;
using System;

namespace Prototypes.CoreLoop
{
    [CreateAssetMenu(fileName = "GameEvents", menuName = "EventideAge/GameEvents")]
    public class GameEvents : ScriptableObject
    {
        public event Action<PhaseType> OnPhaseChanged;
        public event Action<int> OnTurnEnded;
        public event Action<string, int> OnResourceChanged;
        public event Action<string, int> OnActionPointsChanged;
        
        public void PhaseChanged(PhaseType newPhase)
        {
            OnPhaseChanged?.Invoke(newPhase);
            Debug.Log($"[GameEvents] Phase changed to: {newPhase}");
        }
        
        public void TurnEnded(int turnNumber)
        {
            OnTurnEnded?.Invoke(turnNumber);
            Debug.Log($"[GameEvents] Turn {turnNumber} ended");
        }
        
        public void ResourceChanged(string resourceId, int newAmount)
        {
            OnResourceChanged?.Invoke(resourceId, newAmount);
        }
        
        public void ActionPointsChanged(int remaining)
        {
            OnActionPointsChanged?.Invoke("Universal", remaining);
        }
    }
}
