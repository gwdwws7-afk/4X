using UnityEngine;
using System;
using System.Collections.Generic;

namespace EventideAge.Core
{
    [CreateAssetMenu(fileName = "GameState", menuName = "EventideAge/GameState")]
    public class GameState : ScriptableObject
    {
        [Header("Turn Info")]
        public int CurrentTurn = 1;
        public int CurrentPhaseIndex = 0;
        public int ActionPointsRemaining = 11;
        
        [Header("Factions")]
        public FactionState[] Factions;
        
        [Header("Resources")]
        public ResourceState[] Resources;
        
        [Header("Map")]
        public MapState Map;
        
        [Header("Game Config")]
        public GameConfig Config;
        
        public void Initialize()
        {
            CurrentTurn = 1;
            CurrentPhaseIndex = 0;
            ActionPointsRemaining = GameConfig.kTotalActionPoints;
            InitializeFactions();
            InitializeResources();
            InitializeMap();
        }
        
        private void InitializeFactions()
        {
            Factions = new FactionState[Config.FactionConfigs.Length];
            for (int i = 0; i < Config.FactionConfigs.Length; i++)
            {
                var cfg = Config.FactionConfigs[i];
                Factions[i] = new FactionState
                {
                    FactionId = cfg.FactionId,
                    FactionName = cfg.FactionName,
                    IsPlayerControlled = cfg.IsPlayerControlled,
                    ControlledPoints = cfg.InitialControlledPoints,
                    RelationshipWithPlayer = cfg.InitialRelationship,
                    Satisfaction = cfg.InitialSatisfaction,
                    FactionPolicies = new List<string>()
                };
            }
        }
        
        private void InitializeResources()
        {
            Resources = new ResourceState[Config.ResourceConfigs.Length];
            for (int i = 0; i < Config.ResourceConfigs.Length; i++)
            {
                var cfg = Config.ResourceConfigs[i];
                Resources[i] = new ResourceState
                {
                    ResourceId = cfg.ResourceId,
                    ResourceName = cfg.ResourceName,
                    Amount = cfg.InitialAmount,
                    MaxCapacity = cfg.MaxCapacity,
                    ResourceType = cfg.ResourceType
                };
            }
        }
        
        private void InitializeMap()
        {
            Map = new MapState
            {
                Regions = new RegionState[Config.RegionConfigs.Length]
            };
            for (int i = 0; i < Config.RegionConfigs.Length; i++)
            {
                var cfg = Config.RegionConfigs[i];
                Map.Regions[i] = new RegionState
                {
                    RegionId = cfg.RegionId,
                    RegionName = cfg.RegionName,
                    Nodes = new NodeState[cfg.NodeConfigs.Length]
                };
                for (int j = 0; j < cfg.NodeConfigs.Length; j++)
                {
                    var nodeCfg = cfg.NodeConfigs[j];
                    Map.Regions[i].Nodes[j] = new NodeState
                    {
                        NodeId = nodeCfg.NodeId,
                        NodeName = nodeCfg.NodeName,
                        NodeType = nodeCfg.NodeType,
                        DefenseBonus = nodeCfg.DefenseBonus,
                        ControllingFactionId = nodeCfg.InitialController,
                        ControlPoints = nodeCfg.InitialControlPoints,
                        MaxControlPoints = nodeCfg.MaxControlPoints
                    };
                }
            }
        }
        
        public FactionState GetFaction(string factionId)
        {
            foreach (var f in Factions)
                if (f.FactionId == factionId) return f;
            return null;
        }
        
        public ResourceState GetResource(string resourceId)
        {
            foreach (var r in Resources)
                if (r.ResourceId == resourceId) return r;
            return null;
        }
        
        public NodeState GetNode(string nodeId)
        {
            foreach (var region in Map.Regions)
                foreach (var node in region.Nodes)
                    if (node.NodeId == nodeId) return node;
            return null;
        }
    }
    
    [Serializable]
    public class FactionState
    {
        public string FactionId;
        public string FactionName;
        public bool IsPlayerControlled;
        public int ControlledPoints;
        public int RelationshipWithPlayer;
        public int Satisfaction;
        public List<string> FactionPolicies;
    }
    
    [Serializable]
    public class ResourceState
    {
        public string ResourceId;
        public string ResourceName;
        public int Amount;
        public int MaxCapacity;
        public ResourceType ResourceType;
    }
    
    public enum ResourceType
    {
        Consumable,
        Accumulative,
        Ratio
    }
    
    [Serializable]
    public class MapState
    {
        public RegionState[] Regions;
    }
    
    [Serializable]
    public class RegionState
    {
        public string RegionId;
        public string RegionName;
        public NodeState[] Nodes;
    }
    
    [Serializable]
    public class NodeState
    {
        public string NodeId;
        public string NodeName;
        public NodeType NodeType;
        public int DefenseBonus;
        public string ControllingFactionId;
        public int ControlPoints;
        public int MaxControlPoints;
    }
    
    public enum NodeType
    {
        Chokepoint,
        ResourceNode,
        City,
        Port
    }
}
