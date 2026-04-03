using UnityEngine;
using System.Collections.Generic;
using EventideAge.Core;

namespace EventideAge.Systems.E
{
    public enum FactionPoliticalLean
    {
        Hawks,
        Moderates,
        Pragmatists
    }
    
    public class InternalFaction
    {
        public string FactionId;
        public string FactionName;
        public FactionPoliticalLean Lean;
        public int Satisfaction;
        public int Influence;
        public List<string> ActivePolicies;
    }
    
    public class InternalPoliticsSystem : GameSystem
    {
        [Header("Faction Base Stats")]
        public int HawksBaseSatisfaction = 60;
        public int ModeratesBaseSatisfaction = 70;
        public int PragmatistsBaseSatisfaction = 50;
        
        [Header("Influence Rates")]
        public int HawksInfluenceRate = 5;
        public int ModeratesInfluenceRate = 3;
        public int PragmatistsInfluenceRate = 4;
        
        [Header("Satisfaction Effects")]
        public int LowSatisfactionThreshold = 30;
        public int HighSatisfactionThreshold = 80;
        
        private List<InternalFaction> _internalFactions = new List<InternalFaction>();
        
        public override void Initialize(GameState state, GameEvents events)
        {
            base.Initialize(state, events);
            
            InitializeFactions();
            
            Events.OnTurnEnded += HandleTurnEnded;
            
            Debug.Log("[InternalPoliticsSystem] Initialized");
        }
        
        private void OnDestroy()
        {
            Events.OnTurnEnded -= HandleTurnEnded;
        }
        
        private void InitializeFactions()
        {
            _internalFactions.Add(new InternalFaction
            {
                FactionId = "Hawks",
                FactionName = "鹰派",
                Lean = FactionPoliticalLean.Hawks,
                Satisfaction = HawksBaseSatisfaction,
                Influence = 30,
                ActivePolicies = new List<string>()
            });
            
            _internalFactions.Add(new InternalFaction
            {
                FactionId = "Moderates",
                FactionName = "温和派",
                Lean = FactionPoliticalLean.Moderates,
                Satisfaction = ModeratesBaseSatisfaction,
                Influence = 40,
                ActivePolicies = new List<string>()
            });
            
            _internalFactions.Add(new InternalFaction
            {
                FactionId = "Pragmatists",
                FactionName = "务实派",
                Lean = FactionPoliticalLean.Pragmatists,
                Satisfaction = PragmatistsBaseSatisfaction,
                Influence = 30,
                ActivePolicies = new List<string>()
            });
        }
        
        private void HandleTurnEnded(int turnNumber)
        {
            UpdateFactionSatisfaction();
            UpdateFactionInfluence();
            CheckFactionCrisis();
        }
        
        private void UpdateFactionSatisfaction()
        {
            var socialValue = State.GetResource("SocialValue");
            int socialFactor = socialValue?.Amount ?? 50;
            
            foreach (var faction in _internalFactions)
            {
                int targetSatisfaction = CalculateTargetSatisfaction(faction, socialFactor);
                int diff = targetSatisfaction - faction.Satisfaction;
                faction.Satisfaction = Mathf.Clamp(faction.Satisfaction + Mathf.RoundToInt(diff * 0.2f), 0, 100);
            }
        }
        
        private int CalculateTargetSatisfaction(InternalFaction faction, int socialFactor)
        {
            switch (faction.Lean)
            {
                case FactionPoliticalLean.Hawks:
                    int militaryFactor = (State.GetResource("Arms")?.Amount ?? 0) / 10;
                    return Mathf.Clamp(40 + militaryFactor + socialFactor / 5, 0, 100);
                    
                case FactionPoliticalLean.Moderates:
                    int diplomaticFactor = (State.GetResource("TradeToken")?.Amount ?? 0) / 5;
                    return Mathf.Clamp(50 + diplomaticFactor + socialFactor / 4, 0, 100);
                    
                case FactionPoliticalLean.Pragmatists:
                    int economicFactor = (State.GetResource("GoldLeaf")?.Amount ?? 0) / 20;
                    return Mathf.Clamp(45 + economicFactor + socialFactor / 3, 0, 100);
                    
                default:
                    return 50;
            }
        }
        
        private void UpdateFactionInfluence()
        {
            int totalInfluence = 100;
            
            var hawks = GetFaction("Hawks");
            var moderates = GetFaction("Moderates");
            var pragmatists = GetFaction("Pragmatists");
            
            if (hawks != null && moderates != null && pragmatists != null)
            {
                hawks.Influence = HawksInfluenceRate + (hawks.Satisfaction / 10);
                moderates.Influence = ModeratesInfluenceRate + (moderates.Satisfaction / 10);
                pragmatists.Influence = PragmatistsInfluenceRate + (pragmatists.Satisfaction / 10);
                
                int total = hawks.Influence + moderates.Influence + pragmatists.Influence;
                float scale = (float)totalInfluence / total;
                
                hawks.Influence = Mathf.RoundToInt(hawks.Influence * scale);
                moderates.Influence = Mathf.RoundToInt(moderates.Influence * scale);
                pragmatists.Influence = Mathf.RoundToInt(pragmatists.Influence * scale);
            }
        }
        
        private void CheckFactionCrisis()
        {
            foreach (var faction in _internalFactions)
            {
                if (faction.Satisfaction < LowSatisfactionThreshold)
                {
                    Debug.Log($"[InternalPolitics] {faction.FactionName} in crisis! Satisfaction: {faction.Satisfaction}");
                    
                    if (faction.Lean == FactionPoliticalLean.Hawks)
                    {
                        Events.GameEnded("faction_crisis_hawks");
                    }
                }
            }
        }
        
        public InternalFaction GetFaction(string factionId)
        {
            return _internalFactions.Find(f => f.FactionId == factionId);
        }
        
        public InternalFaction[] GetAllFactions()
        {
            return _internalFactions.ToArray();
        }
        
        public FactionPoliticalLean GetDominantFaction()
        {
            InternalFaction dominant = null;
            foreach (var faction in _internalFactions)
            {
                if (dominant == null || faction.Influence > dominant.Influence)
                    dominant = faction;
            }
            return dominant?.Lean ?? FactionPoliticalLean.Moderates;
        }
        
        public bool ChangePolicy(string factionId, string policy, bool add)
        {
            var faction = GetFaction(factionId);
            if (faction == null) return false;
            
            if (add)
            {
                if (!faction.ActivePolicies.Contains(policy))
                {
                    faction.ActivePolicies.Add(policy);
                }
            }
            else
            {
                faction.ActivePolicies.Remove(policy);
            }
            
            return true;
        }
    }
}
