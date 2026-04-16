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
            targetNodeId = GameIds.ResolveNodeId(targetNodeId);
            var action = GetActionTemplate(type);
            if (action == null) return false;
            
            var arms = State.GetResource(GameIds.Resource.Arms);
            var goldLeaf = State.GetResource(GameIds.Resource.GoldLeaf);
            
            if (arms != null && arms.Amount < action.CostArms)
                return false;
            
            if (goldLeaf != null && goldLeaf.Amount < action.CostGoldLeaf)
                return false;
            
            if (!CanSpendActionPoints(action.CostActionPoints))
                return false;
            
            return true;
        }
        
        public bool ExecuteAction(MilitaryActionType type, string targetNodeId)
        {
            targetNodeId = GameIds.ResolveNodeId(targetNodeId);
            if (!CanExecuteAction(type, targetNodeId))
                return false;
            
            var action = GetActionTemplate(type);
            if (action == null) return false;
            string actionId = GetCanonicalActionId(type);

            if (!SpendActionPoints(action.CostActionPoints))
                return false;

            SpendResources(action);
            
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

            EmitActionFeedback(actionId, type, targetNodeId, success, actualSuccessRate, blockadeRisk);
            
            Debug.Log($"[MilitaryOperations] {type} on {targetNodeId}: {(success ? "SUCCESS" : "FAILED")} ({actualSuccessRate:P})");
            
            return success;
        }

        public bool ExecuteActionForAI(MilitaryActionType type, string targetNodeId)
        {
            targetNodeId = GameIds.ResolveNodeId(targetNodeId);
            var action = GetActionTemplate(type);
            if (action == null)
                return false;

            string actionId = GetCanonicalActionId(type);
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

            EmitActionFeedback(actionId, type, targetNodeId, success, actualSuccessRate, blockadeRisk);

            Debug.Log($"[MilitaryOperations] AI {type} on {targetNodeId}: {(success ? "SUCCESS" : "FAILED")} ({actualSuccessRate:P})");
            return success;
        }
        
        private void SpendResources(MilitaryAction action)
        {
            var arms = State.GetResource(GameIds.Resource.Arms);
            var goldLeaf = State.GetResource(GameIds.Resource.GoldLeaf);
            
            if (arms != null)
            {
                int oldAmount = arms.Amount;
                arms.Amount -= action.CostArms;
                Events.ResourceChanged(GameIds.Resource.Arms, oldAmount, arms.Amount);
            }
            
            if (goldLeaf != null)
            {
                int oldAmount = goldLeaf.Amount;
                goldLeaf.Amount -= action.CostGoldLeaf;
                Events.ResourceChanged(GameIds.Resource.GoldLeaf, oldAmount, goldLeaf.Amount);
            }
        }
        
        private float CalculateSuccessRate(MilitaryActionType type, string targetNodeId)
        {
            targetNodeId = GameIds.ResolveNodeId(targetNodeId);
            var action = GetActionTemplate(type);
            if (action == null) return 0f;
            
            float rate = action.SuccessProbability;
            
            var node = State.GetNode(targetNodeId);
            if (node != null)
            {
                rate *= (1 + node.DefenseBonus / 200f);
            }
            
            var socialValue = State.GetResource(GameIds.Resource.SocialValue);
            if (socialValue != null)
            {
                rate *= (socialValue.Amount / 100f);
            }
            
            var ashWill = State.GetResource(GameIds.Resource.AshWill);
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
            targetNodeId = GameIds.ResolveNodeId(targetNodeId);
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
            targetNodeId = GameIds.ResolveNodeId(targetNodeId);
            var node = State.GetNode(targetNodeId);
            if (node == null) return;

            string oldController = node.ControllingFactionId;
            
            int controlDelta = Mathf.RoundToInt(node.MaxControlPoints * 0.15f);
            node.ControlPoints = Mathf.Clamp(node.ControlPoints - controlDelta, 0, node.MaxControlPoints);

            PublishNodeControlSnapshot(targetNodeId, node, oldController);
        }
        
        private void ApplySpecialForcesEffect(string targetNodeId)
        {
            targetNodeId = GameIds.ResolveNodeId(targetNodeId);
            var node = State.GetNode(targetNodeId);
            if (node == null) return;

            string oldController = node.ControllingFactionId;
            
            int controlDelta = Mathf.RoundToInt(node.MaxControlPoints * 0.25f);
            node.ControlPoints = Mathf.Clamp(node.ControlPoints - controlDelta, 0, node.MaxControlPoints);

            PublishNodeControlSnapshot(targetNodeId, node, oldController);
        }
        
        private void ApplyChokepointThreatEffect(string targetNodeId)
        {
            targetNodeId = GameIds.ResolveNodeId(targetNodeId);
            var node = State.GetNode(targetNodeId);
            if (node == null) return;

            string oldController = node.ControllingFactionId;
            node.DefenseBonus = Mathf.Max(0, node.DefenseBonus - 10);

            PublishNodeControlSnapshot(targetNodeId, node, oldController);
        }
        
        private void ApplyAsymmetricDefenseEffect(string targetNodeId)
        {
            targetNodeId = GameIds.ResolveNodeId(targetNodeId);
            var node = State.GetNode(targetNodeId);
            if (node == null) return;

            string oldController = node.ControllingFactionId;
            node.DefenseBonus += 15;
            node.ControlPoints = Mathf.Clamp(node.ControlPoints + 20, 0, node.MaxControlPoints);

            PublishNodeControlSnapshot(targetNodeId, node, oldController);
        }
        
        private void ApplyNuclearDeterrenceEffect(string targetNodeId)
        {
            Debug.Log($"[MilitaryOperations] Nuclear deterrence active - blockade escalation prevented");
        }
        
        private void ApplyTotalWarEffect(string targetNodeId)
        {
            targetNodeId = GameIds.ResolveNodeId(targetNodeId);
            var node = State.GetNode(targetNodeId);
            if (node == null) return;

            string oldController = node.ControllingFactionId;
            
            int controlDelta = Mathf.RoundToInt(node.MaxControlPoints * 0.4f);
            node.ControlPoints = Mathf.Clamp(node.ControlPoints - controlDelta, 0, node.MaxControlPoints);

            PublishNodeControlSnapshot(targetNodeId, node, oldController);
        }

        private void PublishNodeControlSnapshot(string nodeId, NodeState node, string oldController)
        {
            string canonicalNodeId = GameIds.ResolveNodeId(nodeId);
            string canonicalOldController = GameIds.ResolveFactionId(oldController);
            string canonicalNewController = GameIds.ResolveFactionId(node.ControllingFactionId);
            Events.NodeControlChanged(canonicalNodeId, canonicalOldController, canonicalNewController, node.ControlPoints);
        }
        
        private void NotifyBlockadeRisk(MilitaryActionType type, int riskIncrease)
        {
            Events.ActionLogAdded("D1", $"Blockade pressure risk +{riskIncrease} from {type}", FeedbackSeverity.Warning);
            Debug.Log($"[MilitaryOperations] Blockade risk increased by {riskIncrease} due to {type}");
        }

        private void EmitActionFeedback(
            string actionId,
            MilitaryActionType type,
            string targetNodeId,
            bool success,
            float successRate,
            int blockadeRisk)
        {
            targetNodeId = GameIds.ResolveNodeId(targetNodeId);
            var severity = success ? FeedbackSeverity.Info : FeedbackSeverity.Warning;
            string result = success ? "SUCCESS" : "FAILED";
            Events.ActionLogAdded("D1", $"{actionId} on {targetNodeId}: {result} ({successRate:P0})", severity);

            if (success)
            {
                string description = GetConsequenceDescription(type, targetNodeId);
                int durationTurns = GetConsequenceDuration(type);
                bool reversible = IsConsequenceReversible(type);
                Events.ConsequenceAdded(actionId, description, durationTurns, reversible);
            }
            else
            {
                Events.ConsequenceAdded(actionId, "Operation failed: strategic pressure increased.", 1, true);
            }

            if (type == MilitaryActionType.SpecialForces)
            {
                string intelMessage = success
                    ? $"Recon update at {targetNodeId}: defensive posture degraded and exposure windows detected."
                    : $"Recon operation at {targetNodeId} compromised; exposure risk increased.";
                Events.IntelReportAdded("D1.SpecialForces.Intel", intelMessage, severity);
            }

            if (type == MilitaryActionType.TotalWar)
            {
                Events.GlobalAlertRaised("Total war declared. Regional escalation risk is critical.", FeedbackSeverity.Critical);
            }
            else if (type == MilitaryActionType.NuclearDeterrence)
            {
                Events.GlobalAlertRaised("Nuclear deterrence display executed. Global posture tightened.", FeedbackSeverity.Warning);
            }

            if (blockadeRisk >= 30)
            {
                Events.GlobalAlertRaised($"High blockade escalation risk (+{blockadeRisk}) after {actionId}.", FeedbackSeverity.Critical);
            }
        }

        private string GetCanonicalActionId(MilitaryActionType type)
        {
            switch (type)
            {
                case MilitaryActionType.Proxy: return "D1.ProxyAction.Standard";
                case MilitaryActionType.SpecialForces: return "D1.SpecialForces.Strike";
                case MilitaryActionType.ChokepointThreat: return "D1.ChokepointThreat.Start";
                case MilitaryActionType.AsymmetricDefense: return "D1.AsymmetricDefense.Fortify";
                case MilitaryActionType.NuclearDeterrence: return "D1.NuclearDeterrence.Display";
                case MilitaryActionType.TotalWar: return "D1.TotalWar";
                default: return "D1.UnknownAction";
            }
        }

        private string GetConsequenceDescription(MilitaryActionType type, string targetNodeId)
        {
            switch (type)
            {
                case MilitaryActionType.Proxy:
                    return $"Proxy pressure applied on {targetNodeId}; blockade pressure may rise.";
                case MilitaryActionType.SpecialForces:
                    return $"Special forces strike at {targetNodeId}; control balance shifted.";
                case MilitaryActionType.ChokepointThreat:
                    return $"Chokepoint threat activated at {targetNodeId}; route efficiency degraded.";
                case MilitaryActionType.AsymmetricDefense:
                    return $"Asymmetric defense fortified at {targetNodeId}; node stability increased.";
                case MilitaryActionType.NuclearDeterrence:
                    return "Deterrence posture active; enemy full-war options temporarily constrained.";
                case MilitaryActionType.TotalWar:
                    return $"Total war launched at {targetNodeId}; systemic stability penalties applied.";
                default:
                    return "Military consequence recorded.";
            }
        }

        private int GetConsequenceDuration(MilitaryActionType type)
        {
            switch (type)
            {
                case MilitaryActionType.Proxy: return 2;
                case MilitaryActionType.SpecialForces: return 1;
                case MilitaryActionType.ChokepointThreat: return 2;
                case MilitaryActionType.AsymmetricDefense: return 2;
                case MilitaryActionType.NuclearDeterrence: return 1;
                case MilitaryActionType.TotalWar: return 4;
                default: return 1;
            }
        }

        private bool IsConsequenceReversible(MilitaryActionType type)
        {
            switch (type)
            {
                case MilitaryActionType.NuclearDeterrence:
                case MilitaryActionType.TotalWar:
                    return false;
                default:
                    return true;
            }
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

        private bool CanSpendActionPoints(int cost)
        {
            var manager = GameManager.Instance;
            if (manager != null)
                return manager.CanSpendActionPoints(cost);

            return State.CanSpendActionPoints(cost);
        }

        private bool SpendActionPoints(int cost)
        {
            var manager = GameManager.Instance;
            if (manager != null)
                return manager.SpendActionPoints(cost);

            if (!State.TrySpendActionPoints(cost))
                return false;

            Events.ActionPointsChanged(State.ActionPointsRemaining);
            return true;
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
