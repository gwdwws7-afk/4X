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
        public int ActionPointsRemaining = GameConfig.kTotalActionPoints;
        public int CurrentPhaseActionPointsRemaining = 0;
        public int UniversalActionPointsRemaining = GameConfig.kUniversalActionPoints;
        
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
            ResetTurnActionPoints();
            PreparePhaseActionPoints(CurrentPhaseIndex);
            InitializeFactions();
            InitializeResources();
            InitializeMap();
        }

        public void ResetTurnActionPoints()
        {
            ActionPointsRemaining = GameConfig.kTotalActionPoints;
            UniversalActionPointsRemaining = GameConfig.kUniversalActionPoints;
            CurrentPhaseActionPointsRemaining = 0;
        }

        public void PreparePhaseActionPoints(int phaseIndex)
        {
            CurrentPhaseActionPointsRemaining = GetPhaseBaseActionPoints(phaseIndex);
        }

        public void ExpireCurrentPhaseActionPoints()
        {
            if (CurrentPhaseActionPointsRemaining <= 0)
            {
                CurrentPhaseActionPointsRemaining = 0;
                return;
            }

            ActionPointsRemaining = Mathf.Max(0, ActionPointsRemaining - CurrentPhaseActionPointsRemaining);
            CurrentPhaseActionPointsRemaining = 0;
        }

        public bool CanSpendActionPoints(int cost)
        {
            if (cost <= 0) return true;
            if (CurrentPhaseIndex == GameConfig.kAiResponsePhaseIndex) return false;

            int immediatelyAvailable = CurrentPhaseActionPointsRemaining + UniversalActionPointsRemaining;
            return cost <= immediatelyAvailable && cost <= ActionPointsRemaining;
        }

        public bool TrySpendActionPoints(int cost)
        {
            if (!CanSpendActionPoints(cost))
                return false;

            int spendFromPhase = Mathf.Min(cost, CurrentPhaseActionPointsRemaining);
            CurrentPhaseActionPointsRemaining -= spendFromPhase;

            int spendFromUniversal = cost - spendFromPhase;
            if (spendFromUniversal > 0)
            {
                UniversalActionPointsRemaining -= spendFromUniversal;
            }

            ActionPointsRemaining = Mathf.Max(0, ActionPointsRemaining - cost);
            return true;
        }

        public bool TrySpendUniversalActionPoints(int amount)
        {
            if (amount <= 0) return true;
            if (CurrentPhaseIndex == GameConfig.kAiResponsePhaseIndex) return false;
            if (UniversalActionPointsRemaining < amount) return false;

            UniversalActionPointsRemaining -= amount;
            ActionPointsRemaining = Mathf.Max(0, ActionPointsRemaining - amount);
            return true;
        }

        private int GetPhaseBaseActionPoints(int phaseIndex)
        {
            if (Config?.PhaseConfigs == null)
                return 0;

            if (phaseIndex < 0 || phaseIndex >= Config.PhaseConfigs.Length)
                return 0;

            return Mathf.Max(0, Config.PhaseConfigs[phaseIndex].BaseActionPoints);
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
            if (Factions == null) return null;

            var candidates = GameIds.GetFactionIdCandidates(factionId);
            foreach (var candidate in candidates)
            {
                foreach (var faction in Factions)
                {
                    if (faction != null && faction.FactionId == candidate)
                        return faction;
                }
            }

            return null;
        }
        
        public ResourceState GetResource(string resourceId)
        {
            if (Resources == null) return null;

            var candidates = GameIds.GetResourceIdCandidates(resourceId);
            foreach (var candidate in candidates)
            {
                foreach (var resource in Resources)
                {
                    if (resource != null && resource.ResourceId == candidate)
                        return resource;
                }
            }

            return null;
        }
        
        public NodeState GetNode(string nodeId)
        {
            if (Map == null || Map.Regions == null) return null;

            var candidates = GameIds.GetNodeIdCandidates(nodeId);
            foreach (var candidate in candidates)
            {
                foreach (var region in Map.Regions)
                {
                    if (region == null || region.Nodes == null) continue;
                    foreach (var node in region.Nodes)
                    {
                        if (node != null && node.NodeId == candidate)
                            return node;
                    }
                }
            }

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
