using UnityEngine;
using System.Collections.Generic;
using EventideAge.Core;

namespace EventideAge.Systems.D3
{
    public enum ProxyCivilStatus
    {
        Stable,
        Normal,
        Turbulent,
        Collapsed
    }

    public class ProxyControlRegion
    {
        public string NodeId;
        public string ControllerFactionId;
        public int Stability;
        public int PublicSupport;
        public GovernanceLevel GovernanceLevel;
        public int TurnsInCurrentState;
    }

    public enum GovernanceLevel
    {
        Basic,
        Enhanced,
        Maximum
    }

    public class ProxyCivilAffairsSystem : GameSystem
    {
        private Dictionary<string, ProxyControlRegion> _proxyRegions = new Dictionary<string, ProxyControlRegion>();

        [Header("Governance Costs & Effects")]
        public int BasicGovernanceCost = 1;
        public int BasicGovernanceStabilityGain = 2;

        public int EnhancedGovernanceCost = 2;
        public int EnhancedGovernanceStabilityGain = 5;

        public int MaximumGovernanceCost = 3;
        public int MaximumGovernanceStabilityGain = 8;

        [Header("Decay Rates")]
        public int NaturalStabilityDecay = 3;
        public int LowSupportExtraDecay = 5;

        [Header("Thresholds")]
        public int StableThreshold = 70;
        public int NormalThreshold = 40;
        public int TurbulentThreshold = 20;

        public override void Initialize(GameState state, GameEvents events)
        {
            base.Initialize(state, events);
            Events.OnTurnEnded += HandleTurnEnded;
            Debug.Log("[ProxyCivilAffairsSystem] Initialized");
        }
        
        private void OnDestroy()
        {
            Events.OnTurnEnded -= HandleTurnEnded;
        }
        
        public override void OnTurnEnded(int turnNumber)
        {
            ProcessAllRegionsDecay();
        }

        private void HandleTurnEnded(int turnNumber)
        {
            OnTurnEnded(turnNumber);
        }
        
        private void ProcessAllRegionsDecay()
        {
            foreach (var region in _proxyRegions.Values)
            {
                ProcessTurnDecay(region.NodeId);
            }
        }
        
        public void RegisterProxyRegion(string nodeId, string factionId)
        {
            if (!_proxyRegions.ContainsKey(nodeId))
            {
                _proxyRegions[nodeId] = new ProxyControlRegion
                {
                    NodeId = nodeId,
                    ControllerFactionId = factionId,
                    Stability = 50,
                    PublicSupport = 50,
                    GovernanceLevel = GovernanceLevel.Basic,
                    TurnsInCurrentState = 0
                };
            }
        }

        public void SetGovernanceLevel(string nodeId, GovernanceLevel level)
        {
            if (!_proxyRegions.ContainsKey(nodeId))
                return;

            _proxyRegions[nodeId].GovernanceLevel = level;
        }

        public void ApplyGovernance(string nodeId)
        {
            if (!_proxyRegions.ContainsKey(nodeId))
                return;

            var region = _proxyRegions[nodeId];
            int cost = GetGovernanceCost(region.GovernanceLevel);
            int gain = GetGovernanceStabilityGain(region.GovernanceLevel);

            var socialValue = State.GetResource("SocialValue");
            if (socialValue != null && socialValue.Amount >= cost)
            {
                int old = socialValue.Amount;
                socialValue.Amount -= cost;
                Events.ResourceChanged("SocialValue", old, socialValue.Amount);

                region.Stability = Mathf.Min(100, region.Stability + gain);
            }
        }

        public void ProcessTurnDecay(string nodeId)
        {
            if (!_proxyRegions.ContainsKey(nodeId))
                return;

            var region = _proxyRegions[nodeId];
            int decay = NaturalStabilityDecay;

            if (region.PublicSupport < 40)
                decay += LowSupportExtraDecay;

            region.Stability = Mathf.Max(0, region.Stability - decay);
            region.TurnsInCurrentState++;
        }

        public ProxyCivilStatus GetNodeStatus(string nodeId)
        {
            if (!_proxyRegions.ContainsKey(nodeId))
                return ProxyCivilStatus.Normal;

            var region = _proxyRegions[nodeId];
            if (region.Stability > StableThreshold && region.PublicSupport > 60)
                return ProxyCivilStatus.Stable;
            if (region.Stability > TurbulentThreshold && region.PublicSupport > 20)
                return ProxyCivilStatus.Normal;
            if (region.Stability > TurbulentThreshold)
                return ProxyCivilStatus.Turbulent;
            return ProxyCivilStatus.Collapsed;
        }

        public bool HasRegionCollapsed(string nodeId)
        {
            return GetNodeStatus(nodeId) == ProxyCivilStatus.Collapsed;
        }

        public void UpdatePublicSupport(string nodeId, int delta)
        {
            if (!_proxyRegions.ContainsKey(nodeId))
                return;

            _proxyRegions[nodeId].PublicSupport = Mathf.Clamp(
                _proxyRegions[nodeId].PublicSupport + delta, 0, 100);
        }

        public ProxyControlRegion GetProxyRegion(string nodeId)
        {
            return _proxyRegions.ContainsKey(nodeId) ? _proxyRegions[nodeId] : null;
        }

        public float GetOutputModifier(string nodeId)
        {
            var status = GetNodeStatus(nodeId);
            switch (status)
            {
                case ProxyCivilStatus.Stable: return 1.0f;
                case ProxyCivilStatus.Normal: return 0.8f;
                case ProxyCivilStatus.Turbulent: return 0.5f;
                case ProxyCivilStatus.Collapsed: return 0.0f;
                default: return 1.0f;
            }
        }

        private int GetGovernanceCost(GovernanceLevel level)
        {
            switch (level)
            {
                case GovernanceLevel.Basic: return BasicGovernanceCost;
                case GovernanceLevel.Enhanced: return EnhancedGovernanceCost;
                case GovernanceLevel.Maximum: return MaximumGovernanceCost;
                default: return BasicGovernanceCost;
            }
        }

        private int GetGovernanceStabilityGain(GovernanceLevel level)
        {
            switch (level)
            {
                case GovernanceLevel.Basic: return BasicGovernanceStabilityGain;
                case GovernanceLevel.Enhanced: return EnhancedGovernanceStabilityGain;
                case GovernanceLevel.Maximum: return MaximumGovernanceStabilityGain;
                default: return BasicGovernanceStabilityGain;
            }
        }
    }
}