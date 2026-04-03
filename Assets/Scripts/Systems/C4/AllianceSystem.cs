using UnityEngine;
using System.Collections.Generic;
using EventideAge.Core;

namespace EventideAge.Systems.C4
{
    public enum AllianceType
    {
        LimitedAlliance,
        FullAlliance,
        Coalition,
        Hegemony
    }
    
    public enum AllianceStatus
    {
        Proposed,
        Active,
        Dissolved
    }
    
    public class Alliance
    {
        public string AllianceId;
        public string[] MemberFactions;
        public AllianceType Type;
        public AllianceStatus Status;
        public int TurnFormed;
        public int Duration;
        public int MaintenanceCost;
        public float MilitaryBonus;
    }
    
    public class AllianceSystem : GameSystem
    {
        [Header("Alliance Requirements")]
        public int LimitedAllianceRelation = 40;
        public int FullAllianceRelation = 70;
        public int CoalitionRelation = 80;
        
        [Header("Maintenance")]
        public int LimitedAllianceMaintenance = 20;
        public int FullAllianceMaintenance = 40;
        public int CoalitionMaintenance = 60;
        
        [Header("Military Bonuses")]
        public float LimitedAllianceBonus = 0.1f;
        public float FullAllianceBonus = 0.2f;
        public float CoalitionBonus = 0.3f;
        
        private List<Alliance> _activeAlliances = new List<Alliance>();
        private int _allianceIdCounter = 0;
        
        public override void Initialize(GameState state, GameEvents events)
        {
            base.Initialize(state, events);
            
            Events.OnTurnEnded += HandleTurnEnded;
            
            Debug.Log("[AllianceSystem] Initialized");
        }
        
        private void OnDestroy()
        {
            Events.OnTurnEnded -= HandleTurnEnded;
        }
        
        private void HandleTurnEnded(int turnNumber)
        {
            ProcessMaintenance();
        }
        
        public bool CanFormAlliance(string faction1, string faction2, AllianceType type)
        {
            int relation1 = GetRelation(faction1, faction2);
            int requiredRelation = GetRequiredRelation(type);
            
            if (relation1 < requiredRelation)
                return false;
            
            if (GetAllianceCount(faction1) >= 2)
                return false;
            
            if (HasExistingAlliance(faction1, faction2))
                return false;
            
            return true;
        }
        
        public Alliance FormAlliance(string faction1, string faction2, AllianceType type)
        {
            if (!CanFormAlliance(faction1, faction2, type))
                return null;
            
            var alliance = new Alliance
            {
                AllianceId = $"alliance_{_allianceIdCounter++}",
                MemberFactions = new string[] { faction1, faction2 },
                Type = type,
                Status = AllianceStatus.Active,
                TurnFormed = State.CurrentTurn,
                Duration = -1,
                MaintenanceCost = GetMaintenanceCost(type),
                MilitaryBonus = GetMilitaryBonus(type)
            };
            
            _activeAlliances.Add(alliance);
            
            Debug.Log($"[AllianceSystem] Alliance formed: {alliance.AllianceId} ({type}) between {faction1} and {faction2}");
            
            return alliance;
        }
        
        public void DissolveAlliance(string allianceId)
        {
            var alliance = GetAlliance(allianceId);
            if (alliance == null || alliance.Status != AllianceStatus.Active)
                return;
            
            alliance.Status = AllianceStatus.Dissolved;
            Debug.Log($"[AllianceSystem] Alliance dissolved: {allianceId}");
        }
        
        public void ProcessMaintenance()
        {
            for (int i = _activeAlliances.Count - 1; i >= 0; i--)
            {
                var alliance = _activeAlliances[i];
                if (alliance.Status != AllianceStatus.Active)
                    continue;
                
                var goldLeaf = State.GetResource("GoldLeaf");
                if (goldLeaf != null && goldLeaf.Amount >= alliance.MaintenanceCost)
                {
                    goldLeaf.Amount -= alliance.MaintenanceCost;
                }
                else
                {
                    DissolveAlliance(alliance.AllianceId);
                }
            }
        }
        
        public bool HasExistingAlliance(string faction1, string faction2)
        {
            foreach (var alliance in _activeAlliances)
            {
                if (alliance.Status != AllianceStatus.Active)
                    continue;
                
                foreach (var member in alliance.MemberFactions)
                {
                    if (member == faction1 || member == faction2)
                    {
                        bool hasOther = false;
                        foreach (var other in alliance.MemberFactions)
                        {
                            if (other == faction1 || other == faction2)
                                hasOther = true;
                        }
                        if (hasOther)
                            return true;
                    }
                }
            }
            return false;
        }
        
        public Alliance GetAlliance(string allianceId)
        {
            return _activeAlliances.Find(a => a.AllianceId == allianceId);
        }
        
        public int GetAllianceCount(string factionId)
        {
            int count = 0;
            foreach (var alliance in _activeAlliances)
            {
                if (alliance.Status != AllianceStatus.Active)
                    continue;
                
                foreach (var member in alliance.MemberFactions)
                {
                    if (member == factionId)
                    {
                        count++;
                        break;
                    }
                }
            }
            return count;
        }
        
        public float GetMilitaryBonusFor(string factionId)
        {
            float totalBonus = 0f;
            foreach (var alliance in _activeAlliances)
            {
                if (alliance.Status != AllianceStatus.Active)
                    continue;
                
                foreach (var member in alliance.MemberFactions)
                {
                    if (member == factionId)
                    {
                        totalBonus += alliance.MilitaryBonus;
                        break;
                    }
                }
            }
            return totalBonus;
        }
        
        public Alliance[] GetAlliancesFor(string factionId)
        {
            var result = new List<Alliance>();
            foreach (var alliance in _activeAlliances)
            {
                if (alliance.Status != AllianceStatus.Active)
                    continue;
                
                foreach (var member in alliance.MemberFactions)
                {
                    if (member == factionId)
                    {
                        result.Add(alliance);
                        break;
                    }
                }
            }
            return result.ToArray();
        }
        
        private int GetRelation(string faction1, string faction2)
        {
            var f = State.GetFaction(faction1);
            return f?.RelationshipWithPlayer ?? 0;
        }
        
        private int GetRequiredRelation(AllianceType type)
        {
            switch (type)
            {
                case AllianceType.LimitedAlliance: return LimitedAllianceRelation;
                case AllianceType.FullAlliance: return FullAllianceRelation;
                case AllianceType.Coalition: return CoalitionRelation;
                default: return 50;
            }
        }
        
        private int GetMaintenanceCost(AllianceType type)
        {
            switch (type)
            {
                case AllianceType.LimitedAlliance: return LimitedAllianceMaintenance;
                case AllianceType.FullAlliance: return FullAllianceMaintenance;
                case AllianceType.Coalition: return CoalitionMaintenance;
                default: return 30;
            }
        }
        
        private float GetMilitaryBonus(AllianceType type)
        {
            switch (type)
            {
                case AllianceType.LimitedAlliance: return LimitedAllianceBonus;
                case AllianceType.FullAlliance: return FullAllianceBonus;
                case AllianceType.Coalition: return CoalitionBonus;
                default: return 0.1f;
            }
        }
    }
}
