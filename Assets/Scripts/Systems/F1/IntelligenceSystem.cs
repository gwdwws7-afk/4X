using UnityEngine;
using System.Collections.Generic;
using EventideAge.Core;

namespace EventideAge.Systems.F1
{
    public enum IntelligenceActionType
    {
        Reconnaissance,
        CounterIntelligence,
        DiplomaticSpying,
        MilitaryIntelligence,
        Sabotage
    }
    
    public enum FogLevel
    {
        Clear,
        Partial,
        Obscured,
        Unknown
    }
    
    public class IntelligenceReport
    {
        public string NodeId;
        public FogLevel FogLevel;
        public float Reliability;
        public bool IsDeceptive;
        public int TurnAcquired;
    }
    
    public class IntelligenceSystem : GameSystem
    {
        [Header("Action Costs")]
        public int ReconnaissanceCost = 10;
        public int CounterIntelligenceCost = 15;
        public int DiplomaticSpyingCost = 20;
        public int MilitaryIntelligenceCost = 25;
        public int SabotageCost = 30;
        
        [Header("Reliability Base")]
        public float BaseReliability = 0.6f;
        public float MaxReliability = 0.95f;
        public float MinReliability = 0.5f;
        
        [Header("Fog Levels")]
        public float ClearFogThreshold = 0.8f;
        public float PartialFogThreshold = 0.5f;
        public float ObscuredFogThreshold = 0.2f;
        
        private Dictionary<string, IntelligenceReport> _reports = new Dictionary<string, IntelligenceReport>();
        
        public override void Initialize(GameState state, GameEvents events)
        {
            base.Initialize(state, events);
            
            Events.OnTurnEnded += HandleTurnEnded;
            
            Debug.Log("[IntelligenceSystem] Initialized");
        }
        
        private void OnDestroy()
        {
            Events.OnTurnEnded -= HandleTurnEnded;
        }
        
        private void HandleTurnEnded(int turnNumber)
        {
            DecayIntelligenceReliability();
        }
        
        public bool CanExecuteAction(IntelligenceActionType type)
        {
            int cost = GetActionCost(type);
            var goldLeaf = State.GetResource("GoldLeaf");
            return goldLeaf != null && goldLeaf.Amount >= cost;
        }
        
        public IntelligenceReport ExecuteIntelligenceAction(IntelligenceActionType type, string targetId)
        {
            if (!CanExecuteAction(type))
                return null;
            
            int cost = GetActionCost(type);
            var goldLeaf = State.GetResource("GoldLeaf");
            if (goldLeaf != null)
                goldLeaf.Amount -= cost;
            
            float reliability = CalculateReliability(type);
            bool isDeceptive = CheckDeception(type);
            
            var report = new IntelligenceReport
            {
                NodeId = targetId,
                FogLevel = GetFogLevel(reliability),
                Reliability = reliability,
                IsDeceptive = isDeceptive,
                TurnAcquired = State.CurrentTurn
            };
            
            _reports[targetId] = report;
            
            Debug.Log($"[Intelligence] {type} on {targetId}: reliability {reliability:P}, fog {report.FogLevel}");
            
            return report;
        }
        
        private float CalculateReliability(IntelligenceActionType type)
        {
            float baseRel = BaseReliability;
            
            var ashWill = State.GetResource("AshWill");
            if (ashWill != null)
            {
                baseRel *= (1 + ashWill.Amount / 200f);
            }
            
            float finalRel = Mathf.Clamp(baseRel, MinReliability, MaxReliability);
            return finalRel;
        }
        
        private bool CheckDeception(IntelligenceActionType type)
        {
            if (type == IntelligenceActionType.CounterIntelligence)
                return false;
            
            float deceptionChance = 1f - CalculateReliability(type);
            return Random.value < deceptionChance * 0.3f;
        }
        
        private FogLevel GetFogLevel(float reliability)
        {
            if (reliability >= ClearFogThreshold)
                return FogLevel.Clear;
            if (reliability >= PartialFogThreshold)
                return FogLevel.Partial;
            if (reliability >= ObscuredFogThreshold)
                return FogLevel.Obscured;
            return FogLevel.Unknown;
        }
        
        private void DecayIntelligenceReliability()
        {
            var keys = new List<string>(_reports.Keys);
            foreach (var key in keys)
            {
                var report = _reports[key];
                report.Reliability *= 0.95f;
                report.Reliability = Mathf.Max(report.Reliability, MinReliability);
                report.FogLevel = GetFogLevel(report.Reliability);
            }
        }
        
        private int GetActionCost(IntelligenceActionType type)
        {
            switch (type)
            {
                case IntelligenceActionType.Reconnaissance: return ReconnaissanceCost;
                case IntelligenceActionType.CounterIntelligence: return CounterIntelligenceCost;
                case IntelligenceActionType.DiplomaticSpying: return DiplomaticSpyingCost;
                case IntelligenceActionType.MilitaryIntelligence: return MilitaryIntelligenceCost;
                case IntelligenceActionType.Sabotage: return SabotageCost;
                default: return 20;
            }
        }
        
        public IntelligenceReport GetReport(string nodeId)
        {
            if (_reports.TryGetValue(nodeId, out var report))
                return report;
            return null;
        }
        
        public FogLevel GetFogLevelFor(string nodeId)
        {
            if (_reports.TryGetValue(nodeId, out var report))
                return report.FogLevel;
            return FogLevel.Unknown;
        }
        
        public Dictionary<string, IntelligenceReport> GetAllReports()
        {
            return new Dictionary<string, IntelligenceReport>(_reports);
        }
    }
}
