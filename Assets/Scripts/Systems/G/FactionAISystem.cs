using UnityEngine;
using System.Collections.Generic;
using EventideAge.Core;

namespace EventideAge.Systems.G
{
    public enum AIPersonality
    {
        Aggressive,
        Defensive,
        Diplomatic,
        Expansionist
    }
    
    public class FactionAI
    {
        public string FactionId;
        public AIPersonality Personality;
        public float AggressionLevel;
        public float DefenseLevel;
        public float DiplomaticLevel;
        public List<string> ActiveGoals;
        public int DecisionCooldown;
    }
    
    public class AIDecision
    {
        public string FactionId;
        public string DecisionType;
        public string TargetId;
        public float Priority;
        public bool ShouldExecute;
    }
    
    public class FactionAISystem : GameSystem
    {
        [Header("Personality Multipliers")]
        public float AggressiveAggression = 1.5f;
        public float DefensiveDefense = 1.5f;
        public float DiplomaticDiplomacy = 1.5f;
        public float ExpansionistExpansion = 1.5f;
        
        [Header("AI Update Interval")]
        public int AIUpdateInterval = 1;
        
        [Header("Coordination Cost")]
        public int BaseCoordinationCost = 10;
        
        private Dictionary<string, FactionAI> _factionAIs = new Dictionary<string, FactionAI>();
        private int _turnSinceLastUpdate;
        
        public override void Initialize(GameState state, GameEvents events)
        {
            base.Initialize(state, events);
            
            InitializeFactionAIs();
            
            Events.OnTurnEnded += HandleTurnEnded;
            
            Debug.Log("[FactionAISystem] Initialized");
        }
        
        private void OnDestroy()
        {
            Events.OnTurnEnded -= HandleTurnEnded;
        }
        
        private void InitializeFactionAIs()
        {
            _factionAIs["Aurean"] = new FactionAI
            {
                FactionId = "Aurean",
                Personality = AIPersonality.Aggressive,
                AggressionLevel = 0.8f,
                DefenseLevel = 0.5f,
                DiplomaticLevel = 0.3f,
                ActiveGoals = new List<string> { "maintain_blockade", "protect_allies" }
            };
            
            _factionAIs["SacredFire"] = new FactionAI
            {
                FactionId = "SacredFire",
                Personality = AIPersonality.Defensive,
                AggressionLevel = 0.3f,
                DefenseLevel = 0.9f,
                DiplomaticLevel = 0.4f,
                ActiveGoals = new List<string> { "defend_core_territory" }
            };
            
            _factionAIs["GoldenHord"] = new FactionAI
            {
                FactionId = "GoldenHord",
                Personality = AIPersonality.Expansionist,
                AggressionLevel = 0.7f,
                DefenseLevel = 0.4f,
                DiplomaticLevel = 0.5f,
                ActiveGoals = new List<string> { "expand_influence", "control_trade" }
            };
            
            foreach (var ai in _factionAIs.Values)
            {
                ai.AggressionLevel *= GetPersonalityMultiplier(ai.Personality, AIPersonality.Aggressive);
                ai.DefenseLevel *= GetPersonalityMultiplier(ai.Personality, AIPersonality.Defensive);
            }
        }
        
        private float GetPersonalityMultiplier(AIPersonality personality, AIPersonality target)
        {
            if (personality == target)
                return 1.5f;
            return 1.0f;
        }
        
        private void HandleTurnEnded(int turnNumber)
        {
            _turnSinceLastUpdate++;
            
            if (_turnSinceLastUpdate >= AIUpdateInterval)
            {
                ProcessAI Decisions();
                _turnSinceLastUpdate = 0;
            }
        }
        
        public void ProcessAI Decisions()
        {
            foreach (var kvp in _factionAIs)
            {
                if (kvp.Value.DecisionCooldown > 0)
                {
                    kvp.Value.DecisionCooldown--;
                    continue;
                }
                
                var decisions = GenerateDecisions(kvp.Value);
                ExecuteDecisions(kvp.Value, decisions);
            }
        }
        
        private List<AIDecision> GenerateDecisions(FactionAI ai)
        {
            var decisions = new List<AIDecision>();
            
            float militaryPriority = CalculateMilitaryPriority(ai);
            float diplomaticPriority = CalculateDiplomaticPriority(ai);
            float economicPriority = CalculateEconomicPriority(ai);
            
            if (militaryPriority > diplomaticPriority && militaryPriority > economicPriority)
            {
                decisions.Add(new AIDecision
                {
                    FactionId = ai.FactionId,
                    DecisionType = "military",
                    Priority = militaryPriority,
                    ShouldExecute = true
                });
            }
            else if (diplomaticPriority > economicPriority)
            {
                decisions.Add(new AIDecision
                {
                    FactionId = ai.FactionId,
                    DecisionType = "diplomatic",
                    Priority = diplomaticPriority,
                    ShouldExecute = true
                });
            }
            else
            {
                decisions.Add(new AIDecision
                {
                    FactionId = ai.FactionId,
                    DecisionType = "economic",
                    Priority = economicPriority,
                    ShouldExecute = true
                });
            }
            
            return decisions;
        }
        
        private float CalculateMilitaryPriority(FactionAI ai)
        {
            float priority = ai.AggressionLevel * Random.Range(0.3f, 0.8f);
            
            var relation = GetVashidRelation(ai.FactionId);
            if (relation < -50)
                priority += 0.3f;
            
            return priority;
        }
        
        private float CalculateDiplomaticPriority(FactionAI ai)
        {
            float priority = ai.DiplomaticLevel * Random.Range(0.2f, 0.6f);
            
            var allianceCount = GetAllianceCount(ai.FactionId);
            if (allianceCount < 1)
                priority += 0.2f;
            
            return priority;
        }
        
        private float CalculateEconomicPriority(FactionAI ai)
        {
            float priority = 0.3f * Random.Range(0.2f, 0.5f);
            
            var goldLeaf = State.GetResource("GoldLeaf");
            if (goldLeaf != null && goldLeaf.Amount < 50)
                priority += 0.3f;
            
            return priority;
        }
        
        private void ExecuteDecisions(FactionAI ai, List<AIDecision> decisions)
        {
            foreach (var decision in decisions)
            {
                if (!decision.ShouldExecute) continue;
                
                switch (decision.DecisionType)
                {
                    case "military":
                        ExecuteMilitaryDecision(ai, decision);
                        break;
                    case "diplomatic":
                        ExecuteDiplomaticDecision(ai, decision);
                        break;
                    case "economic":
                        ExecuteEconomicDecision(ai, decision);
                        break;
                }
                
                ai.DecisionCooldown = 2;
            }
        }
        
        private void ExecuteMilitaryDecision(FactionAI ai, AIDecision decision)
        {
            Debug.Log($"[FactionAI] {ai.FactionId} executing military decision with priority {decision.Priority}");
        }
        
        private void ExecuteDiplomaticDecision(FactionAI ai, AIDecision decision)
        {
            Debug.Log($"[FactionAI] {ai.FactionId} executing diplomatic decision with priority {decision.Priority}");
        }
        
        private void ExecuteEconomicDecision(FactionAI ai, AIDecision decision)
        {
            Debug.Log($"[FactionAI] {ai.FactionId} executing economic decision with priority {decision.Priority}");
        }
        
        private int GetVashidRelation(string factionId)
        {
            var faction = State.GetFaction(factionId);
            return faction?.RelationshipWithPlayer ?? 0;
        }
        
        private int GetAllianceCount(string factionId)
        {
            return 0;
        }
        
        public FactionAI GetAI(string factionId)
        {
            if (_factionAIs.TryGetValue(factionId, out var ai))
                return ai;
            return null;
        }
        
        public Dictionary<string, FactionAI> GetAllAIs()
        {
            return new Dictionary<string, FactionAI>(_factionAIs);
        }
    }
}
