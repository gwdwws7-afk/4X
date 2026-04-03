using UnityEngine;

namespace Prototypes.CoreLoop
{
    public abstract class GameSystem : MonoBehaviour
    {
        protected GameState State;
        protected GameEvents Events;
        
        public virtual void Initialize(GameState state, GameEvents events)
        {
            State = state;
            Events = events;
        }
        
        public virtual void OnPhaseEnter(PhaseType phase) { }
        public virtual void ExecuteAction(GameAction action) { }
        public virtual bool CanExecuteAction(GameAction action) => false;
        public virtual void OnPhaseExit(PhaseType phase) { }
    }
    
    [System.Serializable]
    public class GameAction
    {
        public string ActionId;
        public int Cost;
        public PhaseType ValidPhase;
    }
}
