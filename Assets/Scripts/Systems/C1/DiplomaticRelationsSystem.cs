using UnityEngine;
using System.Collections.Generic;
using EventideAge.Core;

namespace EventideAge.Systems.C1
{
    public enum RelationLevel
    {
        Hostile = -2,
        Unfriendly = -1,
        Neutral = 0,
        Friendly = 1,
        Allied = 2
    }
    
    public class FactionRelation
    {
        public string FactionId;
        public string FactionName;
        public int RelationValue;
        public RelationLevel Level;
        public int CooldownTurns;
        public List<string> ActivePolicies;
    }
    
    public class DiplomaticRelationsSystem : GameSystem
    {
        [Header("Relation Bounds")]
        public int MinRelation = -100;
        public int MaxRelation = 100;
        
        [Header("Relation Level Thresholds")]
        public int HostileThreshold = -60;
        public int UnfriendlyThreshold = -20;
        public int FriendlyThreshold = 40;
        public int AlliedThreshold = 80;
        
        [Header("Cooldown")]
        public int BaseCooldown = 3;
        public int MaxCooldown = 10;
        
        [Header("Isolation Penalty")]
        public int IsolationTurnsThreshold = 5;
        public int IsolationPenaltyPerTurn = -2;
        
        private Dictionary<string, FactionRelation> _relations = new Dictionary<string, FactionRelation>();
        private Dictionary<string, int> _factionIsolationTurns = new Dictionary<string, int>();
        
        public override void Initialize(GameState state, GameEvents events)
        {
            base.Initialize(state, events);
            
            InitializeRelations();
            
            Events.OnTurnEnded += HandleTurnEnded;
            
            Debug.Log("[DiplomaticRelationsSystem] Initialized");
        }
        
        private void OnDestroy()
        {
            Events.OnTurnEnded -= HandleTurnEnded;
        }
        
        private void InitializeRelations()
        {
            if (State?.Factions == null) return;
            
            foreach (var faction in State.Factions)
            {
                if (faction.FactionId == "Vashid") continue;
                
                _relations[faction.FactionId] = new FactionRelation
                {
                    FactionId = faction.FactionId,
                    FactionName = faction.FactionName,
                    RelationValue = faction.RelationshipWithPlayer,
                    Level = GetRelationLevel(faction.RelationshipWithPlayer),
                    CooldownTurns = 0,
                    ActivePolicies = new List<string>()
                };
                
                _factionIsolationTurns[faction.FactionId] = 0;
            }
        }
        
        public bool ModifyRelation(string factionId, int delta, string reason = "")
        {
            if (!_relations.ContainsKey(factionId))
                return false;
            
            var relation = _relations[factionId];
            
            if (relation.CooldownTurns > 0)
            {
                Debug.Log($"[DiplomaticRelations] Cannot modify {factionId} relation: cooldown active ({relation.CooldownTurns} turns)");
                return false;
            }
            
            int oldValue = relation.RelationValue;
            relation.RelationValue = Mathf.Clamp(relation.RelationValue + delta, MinRelation, MaxRelation);
            relation.Level = GetRelationLevel(relation.RelationValue);
            
            relation.CooldownTurns = CalculateCooldown(oldValue, relation.RelationValue);
            
            Events.RelationshipChanged(factionId, delta);
            
            Debug.Log($"[DiplomaticRelations] {factionId}: {oldValue} → {relation.RelationValue} ({delta:+#;-#;0}) - {reason}");
            
            return true;
        }
        
        private int CalculateCooldown(int oldValue, int newValue)
        {
            int diff = Mathf.Abs(newValue - oldValue);
            
            if (diff >= 50)
                return MaxCooldown;
            if (diff >= 30)
                return 7;
            if (diff >= 15)
                return 5;
            return BaseCooldown;
        }
        
        private RelationLevel GetRelationLevel(int relationValue)
        {
            if (relationValue >= AlliedThreshold)
                return RelationLevel.Allied;
            if (relationValue >= FriendlyThreshold)
                return RelationLevel.Friendly;
            if (relationValue >= UnfriendlyThreshold)
                return RelationLevel.Neutral;
            if (relationValue >= HostileThreshold)
                return RelationLevel.Unfriendly;
            return RelationLevel.Hostile;
        }
        
        private void HandleTurnEnded(int turnNumber)
        {
            UpdateCooldowns();
            UpdateIsolationPenalties();
        }
        
        private void UpdateCooldowns()
        {
            foreach (var kvp in _relations)
            {
                if (kvp.Value.CooldownTurns > 0)
                    kvp.Value.CooldownTurns--;
            }
        }
        
        private void UpdateIsolationPenalties()
        {
            foreach (var kvp in _relations)
            {
                if (kvp.Value.Level == RelationLevel.Hostile)
                {
                    _factionIsolationTurns[kvp.Key]++;
                    
                    if (_factionIsolationTurns[kvp.Key] >= IsolationTurnsThreshold)
                    {
                        int penalty = IsolationPenaltyPerTurn * (_factionIsolationTurns[kvp.Key] - IsolationTurnsThreshold + 1);
                        ModifyRelation(kvp.Key, penalty, "Isolation penalty");
                    }
                }
                else
                {
                    _factionIsolationTurns[kvp.Key] = 0;
                }
            }
        }
        
        public FactionRelation GetRelation(string factionId)
        {
            if (_relations.TryGetValue(factionId, out var relation))
                return relation;
            return null;
        }
        
        public RelationLevel GetRelationLevel(string factionId)
        {
            if (_relations.TryGetValue(factionId, out var relation))
                return relation.Level;
            return RelationLevel.Neutral;
        }
        
        public int GetRelationValue(string factionId)
        {
            if (_relations.TryGetValue(factionId, out var relation))
                return relation.RelationValue;
            return 0;
        }
        
        public bool CanNegotiate(string factionId)
        {
            if (!_relations.ContainsKey(factionId))
                return false;
            return _relations[factionId].CooldownTurns == 0;
        }
        
        public FactionRelation[] GetAllRelations()
        {
            var result = new List<FactionRelation>();
            foreach (var kvp in _relations)
            {
                result.Add(kvp.Value);
            }
            return result.ToArray();
        }
        
        public void AddPolicy(string factionId, string policy)
        {
            if (_relations.TryGetValue(factionId, out var relation))
            {
                if (!relation.ActivePolicies.Contains(policy))
                {
                    relation.ActivePolicies.Add(policy);
                }
            }
        }
        
        public void RemovePolicy(string factionId, string policy)
        {
            if (_relations.TryGetValue(factionId, out var relation))
            {
                relation.ActivePolicies.Remove(policy);
            }
        }
        
        public bool HasPolicy(string factionId, string policy)
        {
            if (_relations.TryGetValue(factionId, out var relation))
            {
                return relation.ActivePolicies.Contains(policy);
            }
            return false;
        }
        
        public int GetDiplomaticSuccessBonus(string factionId)
        {
            if (_relations.TryGetValue(factionId, out var relation))
            {
                return Mathf.RoundToInt(relation.RelationValue / 5f);
            }
            return 0;
        }
    }
}
