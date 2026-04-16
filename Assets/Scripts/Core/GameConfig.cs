using UnityEngine;
using System;

namespace EventideAge.Core
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "EventideAge/GameConfig")]
    public class GameConfig : ScriptableObject
    {
        [Header("Game Constants")]
        public const int kTotalActionPoints = 11;
        public const int kUniversalActionPoints = 2;
        public const int kAiResponsePhaseIndex = 5;
        public const int kMaxTurns = 24;
        public const int kStartingYear = 2028;
        
        [Header("Phase Configuration")]
        public PhaseConfig[] PhaseConfigs;
        
        [Header("Faction Configuration")]
        public FactionConfig[] FactionConfigs;
        
        [Header("Resource Configuration")]
        public ResourceConfig[] ResourceConfigs;
        
        [Header("Region Configuration")]
        public RegionConfig[] RegionConfigs;
        
        [Header("Victory Thresholds")]
        public int EnergyLiberationThreshold = 500;
        public int MilitaryEquilibriumThreshold = 400;
        public int ResistanceAxisVictoryThreshold = 350;
        public int DiplomaticSolutionThreshold = 450;
    }
    
    [Serializable]
    public class PhaseConfig
    {
        public string PhaseName;
        public int BaseActionPoints;
        public int SortOrder;
    }
    
    [Serializable]
    public class FactionConfig
    {
        public string FactionId;
        public string FactionName;
        public bool IsPlayerControlled;
        public int InitialControlledPoints;
        public int InitialRelationship;
        public int InitialSatisfaction = 100;
    }
    
    [Serializable]
    public class ResourceConfig
    {
        public string ResourceId;
        public string ResourceName;
        public int InitialAmount;
        public int MaxCapacity;
        public ResourceType ResourceType;
    }
    
    [Serializable]
    public class RegionConfig
    {
        public string RegionId;
        public string RegionName;
        public NodeConfig[] NodeConfigs;
    }
    
    [Serializable]
    public class NodeConfig
    {
        public string NodeId;
        public string NodeName;
        public NodeType NodeType;
        public int DefenseBonus;
        public string InitialController;
        public int InitialControlPoints;
        public int MaxControlPoints;
    }
}
