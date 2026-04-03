using UnityEngine;
using System.Collections.Generic;
using EventideAge.Core;
using EventideAge.Systems.D2;
using EventideAge.Systems.D6;
using EventideAge.Systems.J;

namespace EventideAge.Systems.D1
{
    public enum MilitaryActionType
    {
        Proxy,
        SpecialForces,
        ChokepointThreat,
        AsymmetricDefense,
        NuclearDeterrence,
        TotalWar
    }
    
    public class MilitaryAction
    {
        public string ActionId;
        public MilitaryActionType Type;
        public string TargetNodeId;
        public int CostArms;
        public int CostGoldLeaf;
        public int CostActionPoints;
        public int ExpectedCasualties;
        public float SuccessProbability;
        public int BlockadeRiskIncrease;
    }
    
    public class MilitaryOperationsSystem : GameSystem
    {
        [Header("System References")]
        public MilitaryTechSystem MilitaryTechSystem { get; set; }
        public MilitaryPoliticalLinkageSystem PoliticalLinkageSystem { get; set; }
        public VictoryDefeatSystem VictoryDefeatSystem { get; set; }
        
        [Header("Action Costs")]
        public int ProxyCostArms = 2;
        public int ProxyCostGoldLeaf = 20;
        public int ProxyCostAP = 1;
        public float ProxySuccessRate = 0.65f;
        
        public int SpecialForcesCostArms = 3;
        public int SpecialForcesCostGoldLeaf = 30;
        public int SpecialForcesCostAP = 2;
        public float SpecialForcesSuccessRate = 0.55f;
        
        public int ChokepointThreatCostArms = 1;
        public int ChokepointThreatCostGoldLeaf = 15;
        public int ChokepointThreatCostAP = 1;
        public float ChokepointThreatSuccessRate = 0.70f;
        
        public int AsymmetricDefenseCostArms = 4;
        public int AsymmetricDefenseCostGoldLeaf = 25;
        public int AsymmetricDefenseCostAP = 2;
        public float AsymmetricDefenseSuccessRate = 0.60f;
        
        public int NuclearDeterrenceCostArms = 0;
        public int NuclearDeterrenceCostGoldLeaf = 50;
        public int NuclearDeterrenceCostAP = 3;
        public float NuclearDeterrenceSuccessRate = 0.85f;
        
        public int TotalWarCostArms = 8;
        public int TotalWarCostGoldLeaf = 60;
        public int TotalWarCostAP = 4;
        public float TotalWarSuccessRate = 0.45f;
        
        [Header("Blockade Risk")]
        public int ProxyBlockadeRisk = 5;
        public int SpecialForcesBlockadeRisk = 10;
        public int ChokepointThreatBlockadeRisk = 30;
        public int AsymmetricDefenseBlockadeRisk = 8;
        public int NuclearDeterrenceBlockadeRisk = 0;
        public int TotalWarBlockadeRisk = 40;
        
        public override void Initialize(GameState state, GameEvents events)
        {
            base.Initialize(state, events);
            
            Debug.Log("[MilitaryOperationsSystem] Initialized");
        }
        
        public bool CanExecuteAction(MilitaryActionType type, string targetNodeId)
        {
            var action = GetActionTemplate(type);
            if (action == null) return false;
            
            var arms = State.GetResource("Arms");
            var goldLeaf = State.GetResource("GoldLeaf");
            
            if (arms != null && arms.Amount < action.CostArms)
                return false;
            
            if (goldLeaf != null && goldLeaf.Amount < action.CostGoldLeaf)
                return false;
            
            if (State.ActionPointsRemaining < action.CostActionPoints)
                return false;
            
            return true;
        }
        
        public bool ExecuteAction(MilitaryActionType type, string targetNodeId)
        {
            if (!CanExecuteAction(type, targetNodeId))
                return false;
            
            var action = GetActionTemplate(type);
            if (action == null) return false;
            
            SpendResources(action);
            State.ActionPointsRemaining -= action.CostActionPoints;
            Events.ActionPointsChanged(State.ActionPointsRemaining);
            
            float actualSuccessRate = CalculateSuccessRate(type, targetNodeId);
            bool success = Random.value < actualSuccessRate;
            
            if (success)
            {
                ApplyActionEffects(type, targetNodeId);
                
                if (PoliticalLinkageSystem != null)
                {
                    PoliticalLinkageSystem.ApplyMilitaryActionPoliticalCost(type);
                }
                
                if (type == MilitaryActionType.TotalWar && VictoryDefeatSystem != null)
                {
                    VictoryDefeatSystem.SetLargeScaleConflictActive(true);
                }
            }
            
            int blockadeRisk = GetBlockadeRiskIncrease(type);
            if (blockadeRisk > 0)
            {
                NotifyBlockadeRisk(type, blockadeRisk);
            }
            
            Debug.Log($"[MilitaryOperations] {type} on {targetNodeId}: {(success ? "SUCCESS" : "FAILED")} ({actualSuccessRate:P})");
            
            return success;
        }
        
        private void SpendResources(MilitaryAction action)
        {
            var arms = State.GetResource("Arms");
            var goldLeaf = State.GetResource("GoldLeaf");
            
            if (arms != null)
                arms.Amount -= action.CostArms;
            
            if (goldLeaf != null)
                goldLeaf.Amount -= action.CostGoldLeaf;
        }
        
        private float CalculateSuccessRate(MilitaryActionType type, string targetNodeId)
        {
            var action = GetActionTemplate(type);
            if (action == null) return 0f;
            
            float rate = action.SuccessProbability;
            
            var node = State.GetNode(targetNodeId);
            if (node != null)
            {
                rate *= (1 + node.DefenseBonus / 200f);
            }
            
            var socialValue = State.GetResource("SocialValue");
            if (socialValue != null)
            {
                rate *= (socialValue.Amount / 100f);
            }
            
            var ashWill = State.GetResource("AshWill");
            if (ashWill != null)
            {
                rate *= (1 + ashWill.Amount / 300f);
            }
            
            if (MilitaryTechSystem != null)
            {
                float techBonus = MilitaryTechSystem.GetMilitaryActionBonus(type);
                rate += techBonus;
            }
            
            return Mathf.Clamp01(rate);
        }
        
        private void ApplyActionEffects(MilitaryActionType type, string targetNodeId)
        {
            switch (type)
            {
                case MilitaryActionType.Proxy:
                    ApplyProxyEffect(targetNodeId);
                    break;
                    
                case MilitaryActionType.SpecialForces:
                    ApplySpecialForcesEffect(targetNodeId);
                    break;
                    
                case MilitaryActionType.ChokepointThreat:
                    ApplyChokepointThreatEffect(targetNodeId);
                    break;
                    
                case MilitaryActionType.AsymmetricDefense:
                    ApplyAsymmetricDefenseEffect(targetNodeId);
                    break;
                    
                case MilitaryActionType.NuclearDeterrence:
                    ApplyNuclearDeterrenceEffect(targetNodeId);
                    break;
                    
                case MilitaryActionType.TotalWar:
                    ApplyTotalWarEffect(targetNodeId);
                    break;
            }
        }
        
        private void ApplyProxyEffect(string targetNodeId)
        {
            var node = State.GetNode(targetNodeId);
            if (node == null) return;
            
            int controlDelta = Mathf.RoundToInt(node.MaxControlPoints * 0.15f);
            node.ControlPoints = Mathf.Clamp(node.ControlPoints - controlDelta, 0, node.MaxControlPoints);
            
            Events.NodeControlChanged(targetNodeId, node.ControllingFactionId, node.ControllingFactionId, node.ControlPoints);
        }
        
        private void ApplySpecialForcesEffect(string targetNodeId)
        {
            var node = State.GetNode(targetNodeId);
            if (node == null) return;
            
            int controlDelta = Mathf.RoundToInt(node.MaxControlPoints * 0.25f);
            node.ControlPoints = Mathf.Clamp(node.ControlPoints - controlDelta, 0, node.MaxControlPoints);
            
            Events.NodeControlChanged(targetNodeId, node.ControllingFactionId, node.ControllingFactionId, node.ControlPoints);
        }
        
        private void ApplyChokepointThreatEffect(string targetNodeId)
        {
            var node = State.GetNode(targetNodeId);
            if (node == null) return;
            
            node.DefenseBonus = Mathf.Max(0, node.DefenseBonus - 10);
            
            Events.NodeControlChanged(targetNodeId, node.ControllingFactionId, node.ControllingFactionId, node.ControlPoints);
        }
        
        private void ApplyAsymmetricDefenseEffect(string targetNodeId)
        {
            var node = State.GetNode(targetNodeId);
            if (node == null) return;
            
            node.DefenseBonus += 15;
            node.ControlPoints = Mathf.Clamp(node.ControlPoints + 20, 0, node.MaxControlPoints);
            
            Events.NodeControlChanged(targetNodeId, node.ControllingFactionId, node.ControllingFactionId, node.ControlPoints);
        }
        
        private void ApplyNuclearDeterrenceEffect(string targetNodeId)
        {
            Debug.Log($"[MilitaryOperations] Nuclear deterrence active - blockade escalation prevented");
        }
        
        private void ApplyTotalWarEffect(string targetNodeId)
        {
            var node = State.GetNode(targetNodeId);
            if (node == null) return;
            
            int controlDelta = Mathf.RoundToInt(node.MaxControlPoints * 0.4f);
            node.ControlPoints = Mathf.Clamp(node.ControlPoints - controlDelta, 0, node.MaxControlPoints);
            
            Events.NodeControlChanged(targetNodeId, node.ControllingFactionId, node.ControllingFactionId, node.ControlPoints);
        }
        
        private void NotifyBlockadeRisk(MilitaryActionType type, int riskIncrease)
        {
            Debug.Log($"[MilitaryOperations] Blockade risk increased by {riskIncrease} due to {type}");
        }
        
        private int GetBlockadeRiskIncrease(MilitaryActionType type)
        {
            switch (type)
            {
                case MilitaryActionType.Proxy: return ProxyBlockadeRisk;
                case MilitaryActionType.SpecialForces: return SpecialForcesBlockadeRisk;
                case MilitaryActionType.ChokepointThreat: return ChokepointThreatBlockadeRisk;
                case MilitaryActionType.AsymmetricDefense: return AsymmetricDefenseBlockadeRisk;
                case MilitaryActionType.NuclearDeterrence: return NuclearDeterrenceBlockadeRisk;
                case MilitaryActionType.TotalWar: return TotalWarBlockadeRisk;
                default: return 0;
            }
        }
        
        private MilitaryAction GetActionTemplate(MilitaryActionType type)
        {
            switch (type)
            {
                case MilitaryActionType.Proxy:
                    return new MilitaryAction
                    {
                        Type = type,
                        CostArms = ProxyCostArms,
                        CostGoldLeaf = ProxyCostGoldLeaf,
                        CostActionPoints = ProxyCostAP,
                        SuccessProbability = ProxySuccessRate,
                        BlockadeRiskIncrease = ProxyBlockadeRisk
                    };
                    
                case MilitaryActionType.SpecialForces:
                    return new MilitaryAction
                    {
                        Type = type,
                        CostArms = SpecialForcesCostArms,
                        CostGoldLeaf = SpecialForcesCostGoldLeaf,
                        CostActionPoints = SpecialForcesCostAP,
                        SuccessProbability = SpecialForcesSuccessRate,
                        BlockadeRiskIncrease = SpecialForcesBlockadeRisk
                    };
                    
                case MilitaryActionType.ChokepointThreat:
                    return new MilitaryAction
                    {
                        Type = type,
                        CostArms = ChokepointThreatCostArms,
                        CostGoldLeaf = ChokepointThreatCostGoldLeaf,
                        CostActionPoints = ChokepointThreatCostAP,
                        SuccessProbability = ChokepointThreatSuccessRate,
                        BlockadeRiskIncrease = ChokepointThreatBlockadeRisk
                    };
                    
                case MilitaryActionType.AsymmetricDefense:
                    return new MilitaryAction
                    {
                        Type = type,
                        CostArms = AsymmetricDefenseCostArms,
                        CostGoldLeaf = AsymmetricDefenseCostGoldLeaf,
                        CostActionPoints = AsymmetricDefenseCostAP,
                        SuccessProbability = AsymmetricDefenseSuccessRate,
                        BlockadeRiskIncrease = AsymmetricDefenseBlockadeRisk
                    };
                    
                case MilitaryActionType.NuclearDeterrence:
                    return new MilitaryAction
                    {
                        Type = type,
                        CostArms = NuclearDeterrenceCostArms,
                        CostGoldLeaf = NuclearDeterrenceCostGoldLeaf,
                        CostActionPoints = NuclearDeterrenceCostAP,
                        SuccessProbability = NuclearDeterrenceSuccessRate,
                        BlockadeRiskIncrease = NuclearDeterrenceBlockadeRisk
                    };
                    
                case MilitaryActionType.TotalWar:
                    return new MilitaryAction
                    {
                        Type = type,
                        CostArms = TotalWarCostArms,
                        CostGoldLeaf = TotalWarCostGoldLeaf,
                        CostActionPoints = TotalWarCostAP,
                        SuccessProbability = TotalWarSuccessRate,
                        BlockadeRiskIncrease = TotalWarBlockadeRisk
                    };
                    
                default:
                    return null;
            }
        }
    }
}
