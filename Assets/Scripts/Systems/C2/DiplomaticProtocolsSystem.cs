using UnityEngine;
using System.Collections.Generic;
using EventideAge.Core;

namespace EventideAge.Systems.C2
{
    public enum ProtocolType
    {
        Any,
        TradeAgreement,
        NonAggression,
        DefensePact,
        Coalition,
        Ceasefire,
        Neutrality
    }
    
    public enum ProtocolStatus
    {
        Proposed,
        Active,
        Expired,
        Violated
    }
    
    public class DiplomaticProtocol
    {
        public string ProtocolId;
        public ProtocolType Type;
        public string FromFaction;
        public string ToFaction;
        public ProtocolStatus Status;
        public int TurnSigned;
        public int Duration;
        public int TurnsRemaining;
        public List<string> Terms;
        public int Cost;
    }
    
    public class DiplomaticProtocolsSystem : GameSystem
    {
        [Header("Protocol Base Costs")]
        public int TradeAgreementCost = 30;
        public int NonAggressionCost = 50;
        public int DefensePactCost = 80;
        public int CoalitionCost = 100;
        public int CeasefireCost = 40;
        public int NeutralityCost = 20;
        
        [Header("Protocol Durations")]
        public int TradeAgreementDuration = 10;
        public int NonAggressionDuration = 8;
        public int DefensePactDuration = 6;
        public int CoalitionDuration = 5;
        public int CeasefireDuration = 4;
        public int NeutralityDuration = 12;
        
        [Header("Auto Renewal")]
        public bool AutoRenewEnabled = true;
        public int AutoRenewThreshold = 2;
        
        private List<DiplomaticProtocol> _activeProtocols = new List<DiplomaticProtocol>();
        private int _protocolIdCounter = 0;
        
        public override void Initialize(GameState state, GameEvents events)
        {
            base.Initialize(state, events);
            
            Events.OnTurnEnded += HandleTurnEnded;
            
            Debug.Log("[DiplomaticProtocolsSystem] Initialized");
        }
        
        private void OnDestroy()
        {
            Events.OnTurnEnded -= HandleTurnEnded;
        }
        
        private void HandleTurnEnded(int turnNumber)
        {
            UpdateProtocolDurations();
            CheckAutoRenewal();
        }
        
        private void UpdateProtocolDurations()
        {
            for (int i = _activeProtocols.Count - 1; i >= 0; i--)
            {
                var protocol = _activeProtocols[i];
                if (protocol.Status == ProtocolStatus.Active)
                {
                    protocol.TurnsRemaining--;
                    
                    if (protocol.TurnsRemaining <= 0)
                    {
                        protocol.Status = ProtocolStatus.Expired;
                        Debug.Log($"[DiplomaticProtocols] {protocol.ProtocolId} expired");
                    }
                }
            }
            
            _activeProtocols.RemoveAll(p => p.Status == ProtocolStatus.Expired);
        }
        
        private void CheckAutoRenewal()
        {
            if (!AutoRenewEnabled) return;
            
            foreach (var protocol in _activeProtocols)
            {
                if (protocol.Status == ProtocolStatus.Active &&
                    protocol.TurnsRemaining <= AutoRenewThreshold &&
                    protocol.Type == ProtocolType.TradeAgreement)
                {
                    protocol.TurnsRemaining = GetProtocolDuration(protocol.Type);
                    Debug.Log($"[DiplomaticProtocols] {protocol.ProtocolId} auto-renewed for {protocol.TurnsRemaining} turns");
                }
            }
        }
        
        public bool CanProposeProtocol(string fromFaction, string toFaction, ProtocolType type)
        {
            if (HasActiveProtocol(fromFaction, toFaction, type))
                return false;
            
            if (GetActiveProtocolCount(fromFaction) >= 3)
                return false;
            
            return true;
        }
        
        public DiplomaticProtocol ProposeProtocol(string fromFaction, string toFaction, ProtocolType type)
        {
            if (!CanProposeProtocol(fromFaction, toFaction, type))
                return null;
            
            var protocol = new DiplomaticProtocol
            {
                ProtocolId = $"protocol_{_protocolIdCounter++}",
                Type = type,
                FromFaction = fromFaction,
                ToFaction = toFaction,
                Status = ProtocolStatus.Proposed,
                TurnSigned = State.CurrentTurn,
                Duration = GetProtocolDuration(type),
                TurnsRemaining = GetProtocolDuration(type),
                Cost = GetProtocolCost(type),
                Terms = GetProtocolTerms(type)
            };
            
            _activeProtocols.Add(protocol);
            
            Debug.Log($"[DiplomaticProtocols] Proposed {type} from {fromFaction} to {toFaction}");
            
            return protocol;
        }
        
        public bool SignProtocol(string protocolId)
        {
            var protocol = GetProtocol(protocolId);
            if (protocol == null || protocol.Status != ProtocolStatus.Proposed)
                return false;
            
            protocol.Status = ProtocolStatus.Active;
            
            Debug.Log($"[DiplomaticProtocols] {protocolId} signed and active");
            
            return true;
        }
        
        public bool ViolateProtocol(string protocolId)
        {
            var protocol = GetProtocol(protocolId);
            if (protocol == null || protocol.Status != ProtocolStatus.Active)
                return false;
            
            protocol.Status = ProtocolStatus.Violated;
            
            Events.RelationshipChanged(protocol.ToFaction, -20);
            Events.RelationshipChanged(protocol.FromFaction, -10);
            
            Debug.Log($"[DiplomaticProtocols] {protocolId} violated by {protocol.FromFaction}");
            
            return true;
        }
        
        public bool HasActiveProtocol(string faction1, string faction2, ProtocolType type)
        {
            foreach (var protocol in _activeProtocols)
            {
                if (protocol.Status != ProtocolStatus.Active)
                    continue;
                
                if ((protocol.FromFaction == faction1 && protocol.ToFaction == faction2) ||
                    (protocol.FromFaction == faction2 && protocol.ToFaction == faction1))
                {
                    if (type == ProtocolType.Any || protocol.Type == type)
                        return true;
                }
            }
            return false;
        }
        
        public int GetActiveProtocolCount(string factionId)
        {
            int count = 0;
            foreach (var protocol in _activeProtocols)
            {
                if (protocol.Status == ProtocolStatus.Active &&
                    (protocol.FromFaction == factionId || protocol.ToFaction == factionId))
                {
                    count++;
                }
            }
            return count;
        }
        
        public DiplomaticProtocol[] GetActiveProtocolsFor(string factionId)
        {
            var result = new List<DiplomaticProtocol>();
            foreach (var protocol in _activeProtocols)
            {
                if (protocol.Status == ProtocolStatus.Active &&
                    (protocol.FromFaction == factionId || protocol.ToFaction == factionId))
                {
                    result.Add(protocol);
                }
            }
            return result.ToArray();
        }
        
        public DiplomaticProtocol GetProtocol(string protocolId)
        {
            return _activeProtocols.Find(p => p.ProtocolId == protocolId);
        }
        
        private int GetProtocolCost(ProtocolType type)
        {
            switch (type)
            {
                case ProtocolType.TradeAgreement: return TradeAgreementCost;
                case ProtocolType.NonAggression: return NonAggressionCost;
                case ProtocolType.DefensePact: return DefensePactCost;
                case ProtocolType.Coalition: return CoalitionCost;
                case ProtocolType.Ceasefire: return CeasefireCost;
                case ProtocolType.Neutrality: return NeutralityCost;
                default: return 50;
            }
        }
        
        private int GetProtocolDuration(ProtocolType type)
        {
            switch (type)
            {
                case ProtocolType.TradeAgreement: return TradeAgreementDuration;
                case ProtocolType.NonAggression: return NonAggressionDuration;
                case ProtocolType.DefensePact: return DefensePactDuration;
                case ProtocolType.Coalition: return CoalitionDuration;
                case ProtocolType.Ceasefire: return CeasefireDuration;
                case ProtocolType.Neutrality: return NeutralityDuration;
                default: return 5;
            }
        }
        
        private List<string> GetProtocolTerms(ProtocolType type)
        {
            var terms = new List<string>();
            
            switch (type)
            {
                case ProtocolType.TradeAgreement:
                    terms.Add("Unlocks TradeNotes currency channel");
                    terms.Add("+10 TradeNotes capacity");
                    break;
                case ProtocolType.NonAggression:
                    terms.Add("Cannot attack for duration");
                    terms.Add("-20% blockade pressure from this faction");
                    break;
                case ProtocolType.DefensePact:
                    terms.Add("Must defend if attacked");
                    terms.Add("Shares military intelligence");
                    break;
                case ProtocolType.Coalition:
                    terms.Add("Full military coordination");
                    terms.Add("Joint operations bonus");
                    break;
                case ProtocolType.Ceasefire:
                    terms.Add("All hostilities pause");
                    terms.Add("Territory held at signing");
                    break;
                case ProtocolType.Neutrality:
                    terms.Add("Non-aggression in this region");
                    terms.Add("Trade access guaranteed");
                    break;
            }
            
            return terms;
        }
        
        public bool SameTypeStackingAllowed(ProtocolType type)
        {
            return type == ProtocolType.TradeAgreement;
        }
    }
}
