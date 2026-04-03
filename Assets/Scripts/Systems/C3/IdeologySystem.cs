using UnityEngine;
using System.Collections.Generic;
using EventideAge.Core;

namespace EventideAge.Systems.C3
{
    public enum IdeologyActionType
    {
        Propaganda,
        CulturalExchange,
        AidProgram,
        RevolutionarySupport,
        DiplomaticPressure
    }
    
    public class IdeologyAction
    {
        public string ActionId;
        public IdeologyActionType Type;
        public string TargetFaction;
        public int CostAshWill;
        public int CostSocialValue;
        public float BaseSuccessRate;
        public int Cooldown;
    }
    
    public class IdeologySystem : GameSystem
    {
        [Header("Action Costs")]
        public int PropagandaCostAshWill = 5;
        public int PropagandaCostSocialValue = 3;
        public float PropagandaSuccessRate = 0.4f;
        
        public int CulturalExchangeCostAshWill = 8;
        public int CulturalExchangeCostSocialValue = 5;
        public float CulturalExchangeSuccessRate = 0.5f;
        
        public int AidProgramCostAshWill = 10;
        public int AidProgramCostSocialValue = 8;
        public float AidProgramSuccessRate = 0.6f;
        
        public int RevolutionarySupportCostAshWill = 15;
        public int RevolutionarySupportCostSocialValue = 10;
        public float RevolutionarySupportSuccessRate = 0.35f;
        
        public int DiplomaticPressureCostAshWill = 7;
        public int DiplomaticPressureCostSocialValue = 5;
        public float DiplomaticPressureSuccessRate = 0.45f;
        
        [Header("Infection Rate")]
        public float BaseInfectionRate = 0.02f;
        public float InfectionSpreadMultiplier = 1.5f;
        
        [Header("Cooldown")]
        public int ActionCooldown = 2;
        
        private Dictionary<string, int> _factionCooldowns = new Dictionary<string, int>();
        
        public override void Initialize(GameState state, GameEvents events)
        {
            base.Initialize(state, events);
            
            Events.OnTurnEnded += HandleTurnEnded;
            
            Debug.Log("[IdeologySystem] Initialized");
        }
        
        private void OnDestroy()
        {
            Events.OnTurnEnded -= HandleTurnEnded;
        }
        
        private void HandleTurnEnded(int turnNumber)
        {
            UpdateCooldowns();
            SpreadIdeology();
        }
        
        private void UpdateCooldowns()
        {
            var keys = new List<string>(_factionCooldowns.Keys);
            foreach (var key in keys)
            {
                if (_factionCooldowns[key] > 0)
                    _factionCooldowns[key]--;
            }
        }
        
        private void SpreadIdeology()
        {
            var ashWill = State.GetResource("AshWill");
            if (ashWill == null) return;
            
            float infectionRate = BaseInfectionRate * (1 + ashWill.Amount / 200f);
            
            if (Random.value < infectionRate)
            {
                Debug.Log($"[IdeologySystem] Ideology spreading. Infection rate: {infectionRate:P}");
            }
        }
        
        public bool CanExecuteAction(IdeologyActionType type, string targetFaction)
        {
            if (_factionCooldowns.TryGetValue(targetFaction, out var cooldown) && cooldown > 0)
                return false;
            
            var action = GetActionTemplate(type);
            if (action == null) return false;
            
            var ashWill = State.GetResource("AshWill");
            var socialValue = State.GetResource("SocialValue");
            
            if (ashWill != null && ashWill.Amount < action.CostAshWill)
                return false;
            
            if (socialValue != null && socialValue.Amount < action.CostSocialValue)
                return false;
            
            return true;
        }
        
        public bool ExecuteAction(IdeologyActionType type, string targetFaction)
        {
            if (!CanExecuteAction(type, targetFaction))
                return false;
            
            var action = GetActionTemplate(type);
            if (action == null) return false;
            
            var ashWill = State.GetResource("AshWill");
            var socialValue = State.GetResource("SocialValue");
            
            if (ashWill != null)
                ashWill.Amount -= action.CostAshWill;
            
            if (socialValue != null)
                socialValue.Amount -= action.CostSocialValue;
            
            _factionCooldowns[targetFaction] = ActionCooldown;
            
            float successRate = CalculateSuccessRate(type, targetFaction);
            bool success = Random.value < successRate;
            
            if (success)
            {
                int relationBonus = GetRelationBonus(type);
                Events.RelationshipChanged(targetFaction, relationBonus);
                Debug.Log($"[IdeologySystem] {type} on {targetFaction} succeeded!");
            }
            else
            {
                Debug.Log($"[IdeologySystem] {type} on {targetFaction} failed");
            }
            
            return success;
        }
        
        private float CalculateSuccessRate(IdeologyActionType type, string targetFaction)
        {
            var action = GetActionTemplate(type);
            if (action == null) return 0f;
            
            float rate = action.BaseSuccessRate;
            
            var ashWill = State.GetResource("AshWill");
            if (ashWill != null)
            {
                rate *= (1 + ashWill.Amount / 200f);
            }
            
            var socialValue = State.GetResource("SocialValue");
            if (socialValue != null)
            {
                rate *= (socialValue.Amount / 100f);
            }
            
            return Mathf.Clamp01(rate);
        }
        
        private int GetRelationBonus(IdeologyActionType type)
        {
            switch (type)
            {
                case IdeologyActionType.Propaganda: return 10;
                case IdeologyActionType.CulturalExchange: return 15;
                case IdeologyActionType.AidProgram: return 20;
                case IdeologyActionType.RevolutionarySupport: return -5;
                case IdeologyActionType.DiplomaticPressure: return 5;
                default: return 0;
            }
        }
        
        private IdeologyAction GetActionTemplate(IdeologyActionType type)
        {
            switch (type)
            {
                case IdeologyActionType.Propaganda:
                    return new IdeologyAction
                    {
                        Type = type,
                        CostAshWill = PropagandaCostAshWill,
                        CostSocialValue = PropagandaCostSocialValue,
                        BaseSuccessRate = PropagandaSuccessRate
                    };
                case IdeologyActionType.CulturalExchange:
                    return new IdeologyAction
                    {
                        Type = type,
                        CostAshWill = CulturalExchangeCostAshWill,
                        CostSocialValue = CulturalExchangeCostSocialValue,
                        BaseSuccessRate = CulturalExchangeSuccessRate
                    };
                case IdeologyActionType.AidProgram:
                    return new IdeologyAction
                    {
                        Type = type,
                        CostAshWill = AidProgramCostAshWill,
                        CostSocialValue = AidProgramCostSocialValue,
                        BaseSuccessRate = AidProgramSuccessRate
                    };
                case IdeologyActionType.RevolutionarySupport:
                    return new IdeologyAction
                    {
                        Type = type,
                        CostAshWill = RevolutionarySupportCostAshWill,
                        CostSocialValue = RevolutionarySupportCostSocialValue,
                        BaseSuccessRate = RevolutionarySupportSuccessRate
                    };
                case IdeologyActionType.DiplomaticPressure:
                    return new IdeologyAction
                    {
                        Type = type,
                        CostAshWill = DiplomaticPressureCostAshWill,
                        CostSocialValue = DiplomaticPressureCostSocialValue,
                        BaseSuccessRate = DiplomaticPressureSuccessRate
                    };
                default:
                    return null;
            }
        }
    }
}
