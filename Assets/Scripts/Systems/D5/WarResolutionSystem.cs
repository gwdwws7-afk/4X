using UnityEngine;
using System.Collections.Generic;
using EventideAge.Core;
using EventideAge.Systems.J;

namespace EventideAge.Systems.D5
{
    public class WarResolutionSystem : GameSystem
    {
        public VictoryDefeatSystem VictoryDefeatSystem { get; set; }
        
        [Header("Control Point Resolution")]
        public int BaseControlPoints = 100;
        public int LeadBonusThreshold = 30;
        public int LeadBonus = 30;
        
        [Header("Attrition")]
        public float BaseAttritionRate = 0.1f;
        public float DefenseAttritionReduction = 0.5f;
        
        [Header("Stabilization")]
        public int MaxStabilizationBonus = 20;
        public int StabilizationTurnThreshold = 3;
        
        public override void Initialize(GameState state, GameEvents events)
        {
            base.Initialize(state, events);
            
            Debug.Log("[WarResolutionSystem] Initialized");
        }
        
        public int ResolveWar(string nodeId, out int attackerLosses, out int defenderLosses)
        {
            nodeId = GameIds.ResolveNodeId(nodeId);
            var node = State.GetNode(nodeId);
            if (node == null)
            {
                attackerLosses = 0;
                defenderLosses = 0;
                return 0;
            }
            
            int controlThreshold = BaseControlPoints;
            int currentControl = node.ControlPoints;
            string controlling = GameIds.ResolveFactionId(node.ControllingFactionId);
            
            int attackerPower = CalculateAttackerPower();
            int defenderPower = CalculateDefenderPower(node);
            
            int totalPower = attackerPower + defenderPower;
            
            if (totalPower > 150 && VictoryDefeatSystem != null)
            {
                VictoryDefeatSystem.SetLargeScaleConflictActive(true);
            }
            
            if (totalPower == 0)
            {
                attackerLosses = 0;
                defenderLosses = 0;
                return 0;
            }
            
            float attackerRatio = (float)attackerPower / totalPower;
            float defenderRatio = (float)defenderPower / totalPower;
            
            int controlGained = Mathf.RoundToInt((attackerRatio - 0.5f) * BaseControlPoints);
            
            attackerLosses = Mathf.RoundToInt(BaseAttritionRate * attackerPower * (1 - DefenseAttritionReduction));
            defenderLosses = Mathf.RoundToInt(BaseAttritionRate * defenderPower);
            
            int stabilizationBonus = CalculateStabilizationBonus();
            controlGained = Mathf.Clamp(controlGained + stabilizationBonus, -BaseControlPoints, BaseControlPoints);
            
            int newControl = currentControl - controlGained;
            newControl = Mathf.Clamp(newControl, 0, node.MaxControlPoints);
            
            node.ControlPoints = newControl;
            
            if (controlGained > LeadBonusThreshold)
            {
                string oldController = GameIds.ResolveFactionId(node.ControllingFactionId);
                node.ControllingFactionId = GameIds.Faction.Vashid;
                
                if (oldController != GameIds.Faction.Vashid && VictoryDefeatSystem != null)
                {
                    VictoryDefeatSystem.RecordEnemyKeyNodeLoss();
                }
            }
            
            Events.NodeControlChanged(nodeId, controlling, GameIds.ResolveFactionId(node.ControllingFactionId), node.ControlPoints);
            
            Debug.Log($"[WarResolution] {nodeId}: control {currentControl} → {newControl}, attacker losses: {attackerLosses}, defender losses: {defenderLosses}");
            
            return controlGained;
        }
        
        private int CalculateAttackerPower()
        {
            var arms = State.GetResource(GameIds.Resource.Arms);
            return arms?.Amount ?? 0;
        }
        
        private int CalculateDefenderPower(NodeState node)
        {
            int baseDefense = 50;
            int defenseBonus = node?.DefenseBonus ?? 0;
            return baseDefense + defenseBonus;
        }
        
        private int CalculateStabilizationBonus()
        {
            var socialValue = State.GetResource(GameIds.Resource.SocialValue);
            if (socialValue == null) return 0;
            
            if (socialValue.Amount >= StabilizationTurnThreshold * 10)
                return MaxStabilizationBonus;
            
            return Mathf.RoundToInt(socialValue.Amount / 2f);
        }
        
        public bool CheckWarConclusion(string nodeId)
        {
            nodeId = GameIds.ResolveNodeId(nodeId);
            var node = State.GetNode(nodeId);
            if (node == null) return false;
            
            if (node.ControlPoints <= 0)
            {
                string oldController = GameIds.ResolveFactionId(node.ControllingFactionId);
                node.ControllingFactionId = GameIds.Faction.Vashid;
                node.ControlPoints = node.MaxControlPoints / 2;
                
                if (oldController != GameIds.Faction.Vashid && VictoryDefeatSystem != null)
                {
                    VictoryDefeatSystem.RecordEnemyKeyNodeLoss();
                }
                
                return true;
            }
            
            if (node.ControlPoints >= node.MaxControlPoints * 0.9f)
            {
                return true;
            }
            
            return false;
        }
    }
}
