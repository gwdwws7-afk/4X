using UnityEngine;

namespace Prototypes.CoreLoop
{
    [CreateAssetMenu(fileName = "GameState", menuName = "EventideAge/GameState")]
    public class GameState : ScriptableObject
    {
        [Header("Turn Info")]
        public int CurrentTurn = 1;
        public PhaseType CurrentPhase = PhaseType.Diplomacy;
        public int ActionPointsRemaining = 11;
        
        [Header("Factions")]
        public FactionState[] Factions;
        
        [Header("Resources")]
        public ResourceState[] Resources;
        
        public void Reset()
        {
            CurrentTurn = 1;
            CurrentPhase = PhaseType.Diplomacy;
            ActionPointsRemaining = 11;
        }
    }
    
    public enum PhaseType
    {
        Diplomacy,      // Phase 1: 外交行动
        Strategy,       // Phase 2: 战略部署  
        Operations,     // Phase 3: 作战行动
        Logistics,      // Phase 4: 后勤补给
        Intelligence,   // Phase 5: 情报活动
        AIResponse      // Phase 6: AI响应
    }
    
    [System.Serializable]
    public class FactionState
    {
        public string FactionId;
        public string FactionName;
        public int ControlledPoints;
        public int RelationshipWithPlayer; // -100 to 100
    }
    
    [System.Serializable]
    public class ResourceState
    {
        public string ResourceId;
        public int Amount;
        public int MaxCapacity;
    }
}
