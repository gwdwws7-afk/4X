using UnityEngine;
using System.Collections.Generic;
using EventideAge.Core;
using EventideAge.Systems.B1;
using EventideAge.Systems.J;

namespace EventideAge.Systems.B2
{
    public enum CountermeasureChannel
    {
        TradeNotes,
        NorthCoins,
        LandRoute,
        GreyMarket,
        Barter
    }
    
    public class BlockadeState
    {
        public B1.BlockadeLevel Level;
        public float TotalPressure;
        public List<B1.BlockadeType> ActiveBlockades = new List<B1.BlockadeType>();
        public float NextEscalationRisk;
    }
    
    public class CountermeasureStatus
    {
        public CountermeasureChannel Channel;
        public float Efficiency;
        public float StabilityCoeff;
        public int TurnsUsed;
        public bool IsTargeted;
    }
    
    public class RiskAssessment
    {
        public float RiskValue;
        public string[] MitigationOptions;
        public bool TriggersEscalation;
    }
    
    public class BlockadeSystem : GameSystem
    {
        [Header("Pressure Thresholds")]
        public float SingleBlockadeThreshold = 25f;
        public float MultiBlockadeThreshold = 60f;
        public float FullBlockadeThreshold = 100f;
        
        [Header("Blockade Pressures")]
        public float EnergyEmbargoPressure = 30f;
        public float FinancialBlockadePressure = 25f;
        public float SecondaryBlockadePressure = 20f;
        public float MilitaryEmbargoPressure = 15f;
        public float NavalBlockadePressure = 40f;
        
        [Header("Countermeasure Base Efficiency")]
        public float TradeNotesBaseEfficiency = 0.85f;
        public float NorthCoinsBaseEfficiency = 0.75f;
        public float LandRouteBaseEfficiency = 0.60f;
        public float GreyMarketBaseEfficiency = 0.55f;
        
        [Header("Stability Decay")]
        public float StabilityDecayPerTurn = 0.1f;
        public float MinStabilityCoeff = 0.5f;
        public int StabilityDecayInterval = 3;
        
        [Header("Blockade Collapse")]
        public float[] CollapseProbabilities = new float[] { 0f, 0.02f, 0.05f, 0.10f };
        public int[] CollapseTurnThresholds = new int[] { 0, 5, 10, 20 };
        
        private Dictionary<B1.BlockadeType, bool> _activeBlockades = new Dictionary<B1.BlockadeType, bool>();
        private Dictionary<B1.BlockadeType, float> _blockadePressures = new Dictionary<B1.BlockadeType, float>();
        private Dictionary<CountermeasureChannel, CountermeasureStatus> _countermeasures = new Dictionary<CountermeasureChannel, CountermeasureStatus>();
        
        private int _blockadeTurnsActive;
        private FinanceSystem _financeSystem;
        public VictoryDefeatSystem VictoryDefeatSystem { get; set; }
        
        public override void Initialize(GameState state, GameEvents events)
        {
            base.Initialize(state, events);
            
            for (int i = 0; i < 5; i++)
            {
                var type = (B1.BlockadeType)i;
                _activeBlockades[type] = false;
                _blockadePressures[type] = 0f;
            }
            
            foreach (CountermeasureChannel channel in System.Enum.GetValues(typeof(CountermeasureChannel)))
            {
                _countermeasures[channel] = new CountermeasureStatus
                {
                    Channel = channel,
                    Efficiency = GetBaseEfficiency(channel),
                    StabilityCoeff = 1.0f,
                    TurnsUsed = 0,
                    IsTargeted = false
                };
            }
            
            _blockadeTurnsActive = 0;
            
            Debug.Log("[BlockadeSystem] Initialized");
        }
        
        public override void OnTurnEnded(int turnNumber)
        {
            _blockadeTurnsActive++;
            UpdateCountermeasureStability();
            CheckBlockadeCollapse();
        }
        
        private float GetBaseEfficiency(CountermeasureChannel channel)
        {
            switch (channel)
            {
                case CountermeasureChannel.TradeNotes: return TradeNotesBaseEfficiency;
                case CountermeasureChannel.NorthCoins: return NorthCoinsBaseEfficiency;
                case CountermeasureChannel.LandRoute: return LandRouteBaseEfficiency;
                case CountermeasureChannel.GreyMarket: return GreyMarketBaseEfficiency;
                case CountermeasureChannel.Barter: return 0.7f;
                default: return 0.5f;
            }
        }
        
        public void ActivateBlockade(B1.BlockadeType type)
        {
            _activeBlockades[type] = true;
            _blockadePressures[type] = GetPressureForType(type);
            _blockadeTurnsActive = 0;
            
            UpdateFinanceSystem();
            NotifyFactionRelations();
            
            Debug.Log($"[BlockadeSystem] {type} activated");
        }
        
        public void DeactivateBlockade(B1.BlockadeType type)
        {
            _activeBlockades[type] = false;
            _blockadePressures[type] = 0f;
            
            UpdateFinanceSystem();
            
            Debug.Log($"[BlockadeSystem] {type} deactivated");
        }
        
        private float GetPressureForType(B1.BlockadeType type)
        {
            switch (type)
            {
                case B1.BlockadeType.EnergyEmbargo: return EnergyEmbargoPressure;
                case B1.BlockadeType.FinancialBlockade: return FinancialBlockadePressure;
                case B1.BlockadeType.SecondaryBlockade: return SecondaryBlockadePressure;
                case B1.BlockadeType.MilitaryEmbargo: return MilitaryEmbargoPressure;
                case B1.BlockadeType.NavalBlockade: return NavalBlockadePressure;
                default: return 0f;
            }
        }
        
        private void UpdateFinanceSystem()
        {
            if (_financeSystem == null)
            {
                _financeSystem = FindSystem<FinanceSystem>();
            }
            
            if (_financeSystem != null)
            {
                _financeSystem.SetBlockadeLevel(GetBlockadeLevel());
                
                foreach (var kvp in _activeBlockades)
                {
                    float pressure = kvp.Value ? _blockadePressures[kvp.Key] : 0f;
                    _financeSystem.SetBlockadeActive(kvp.Key, kvp.Value, pressure);
                }
            }
            
            if (VictoryDefeatSystem != null)
            {
                VictoryDefeatSystem.SetBlockadeLevel(ConvertToVictoryBlockadeLevel(GetBlockadeLevel()));
            }
        }
        
        private J.BlockadeLevel ConvertToVictoryBlockadeLevel(B1.BlockadeLevel level)
        {
            switch (level)
            {
                case B1.BlockadeLevel.None: return J.BlockadeLevel.None;
                case B1.BlockadeLevel.Financial: return J.BlockadeLevel.Unilateral;
                case B1.BlockadeLevel.EnergyEmbargo: return J.BlockadeLevel.Multilateral;
                case B1.BlockadeLevel.Full: return J.BlockadeLevel.Total;
                default: return J.BlockadeLevel.Unilateral;
            }
        }
        
        private T FindSystem<T>() where T : GameSystem
        {
            foreach (var system in GameManager.Instance.Systems)
            {
                if (system is T)
                    return (T)system;
            }
            return default(T);
        }
        
        private void NotifyFactionRelations()
        {
            float totalPressure = GetTotalPressure();
            
            if (totalPressure >= MultiBlockadeThreshold)
            {
                Events.RelationshipChanged("Aurean", -10);
            }
        }
        
        private void UpdateCountermeasureStability()
        {
            foreach (var kvp in _countermeasures)
            {
                var status = kvp.Value;
                if (status.TurnsUsed > 0 && status.TurnsUsed % StabilityDecayInterval == 0)
                {
                    status.StabilityCoeff = Mathf.Max(MinStabilityCoeff, status.StabilityCoeff - StabilityDecayPerTurn);
                }
                status.TurnsUsed++;
            }
        }
        
        private void CheckBlockadeCollapse()
        {
            if (_blockadeTurnsActive < CollapseTurnThresholds[1])
                return;
            
            int tier = 0;
            for (int i = CollapseTurnThresholds.Length - 1; i >= 0; i--)
            {
                if (_blockadeTurnsActive >= CollapseTurnThresholds[i])
                {
                    tier = i;
                    break;
                }
            }
            
            float collapseChance = CollapseProbabilities[tier];
            
            if (UnityEngine.Random.value < collapseChance)
            {
                TriggerBlockadeCollapse();
            }
        }
        
        private void TriggerBlockadeCollapse()
        {
            _blockadeTurnsActive = 0;
            
            foreach (var type in System.Enum.GetValues(typeof(B1.BlockadeType)))
            {
                _activeBlockades[(B1.BlockadeType)type] = false;
                _blockadePressures[(B1.BlockadeType)type] = 0f;
            }
            
            UpdateFinanceSystem();
            
            Debug.Log("[BlockadeSystem] Blockade collapse triggered!");
            Events.GameEnded("blockade_collapse");
        }
        
        public B1.BlockadeLevel GetBlockadeLevel()
        {
            float total = GetTotalPressure();
            
            if (total >= FullBlockadeThreshold)
                return B1.BlockadeLevel.Full;
            else if (total >= MultiBlockadeThreshold)
                return B1.BlockadeLevel.EnergyEmbargo;
            else if (total >= SingleBlockadeThreshold)
                return B1.BlockadeLevel.Financial;
            else
                return B1.BlockadeLevel.None;
        }
        
        public float GetTotalPressure()
        {
            float total = 0f;
            foreach (var kvp in _activeBlockades)
            {
                if (kvp.Value)
                    total += _blockadePressures[kvp.Key];
            }
            return total;
        }
        
        public float GetCountermeasureEfficiency(CountermeasureChannel channel)
        {
            if (!_countermeasures.ContainsKey(channel))
                return 0f;
            
            var status = _countermeasures[channel];
            float baseEff = status.Efficiency;
            float stability = status.StabilityCoeff;
            float blockadeDiscount = GetBlockadeIntensityDiscount();
            
            float effectiveEfficiency = baseEff * stability * (1f - blockadeDiscount);
            
            if (status.IsTargeted)
                effectiveEfficiency *= 0.5f;
            
            return effectiveEfficiency;
        }
        
        private float GetBlockadeIntensityDiscount()
        {
            switch (GetBlockadeLevel())
            {
                case B1.BlockadeLevel.None: return 0f;
                case B1.BlockadeLevel.Financial: return 0.1f;
                case B1.BlockadeLevel.EnergyEmbargo: return 0.3f;
                case B1.BlockadeLevel.Full: return 0.5f;
                default: return 0f;
            }
        }
        
        public RiskAssessment AssessBlockadeRisk(float actionPressure)
        {
            var assessment = new RiskAssessment();
            
            float currentPressure = GetTotalPressure();
            float potentialPressure = currentPressure + actionPressure;
            
            assessment.RiskValue = actionPressure;
            
            if (potentialPressure >= FullBlockadeThreshold)
                assessment.TriggersEscalation = true;
            else if (potentialPressure >= MultiBlockadeThreshold)
                assessment.TriggersEscalation = UnityEngine.Random.value < 0.7f;
            else
                assessment.TriggersEscalation = UnityEngine.Random.value < 0.3f;
            
            assessment.MitigationOptions = new string[]
            {
                "Diplomatic preparation: -20% risk",
                "Alternative countermeasures ready: -30% risk",
                "Military + diplomatic pressure: +50% risk"
            };
            
            return assessment;
        }
        
        public void ApplySecondaryBlockadeTo(CountermeasureChannel channel)
        {
            if (_countermeasures.ContainsKey(channel))
            {
                _countermeasures[channel].IsTargeted = true;
                Debug.Log($"[BlockadeSystem] {channel} targeted by secondary blockade");
            }
        }
        
        public BlockadeState GetBlockadeState()
        {
            var state = new BlockadeState();
            state.Level = GetBlockadeLevel();
            state.TotalPressure = GetTotalPressure();
            state.ActiveBlockades.Clear();
            
            foreach (var kvp in _activeBlockades)
            {
                if (kvp.Value)
                    state.ActiveBlockades.Add(kvp.Key);
            }
            
            state.NextEscalationRisk = GetTotalPressure() >= SingleBlockadeThreshold ? 0.3f : 0f;
            
            return state;
        }
    }
}
