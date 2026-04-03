using UnityEngine;
using System.Collections.Generic;
using EventideAge.Core;
using EventideAge.Systems.B1;
using EventideAge.Systems.B2;
using EventideAge.Systems.B3;

namespace EventideAge.Systems.B5
{
    public enum EconomicHealthGrade
    {
        A, B, C, D, F
    }
    
    public enum HealthTrend
    {
        Improving,
        Stable,
        Declining
    }
    
    public enum WarningLevel
    {
        None,
        Level1,
        Level2,
        Level3
    }
    
    public class EconomicHealthScore
    {
        public int Overall;
        public EconomicHealthGrade Grade;
        public int ReservesScore;
        public int StabilityScore;
        public int BlockadePressureScore;
        public int GrowthTrendScore;
        public HealthTrend Trend;
    }
    
    public class ResourceChange
    {
        public string ResourceId;
        public int PreviousValue;
        public int ChangeAmount;
        public int NewValue;
        public string ChangeReason;
    }
    
    public class EconomicWarning
    {
        public WarningLevel Level;
        public string Message;
        public int TurnsUntilCrisis;
    }
    
    public class SettlementResult
    {
        public int TurnNumber;
        public List<ResourceChange> ResourceChanges = new List<ResourceChange>();
        public EconomicHealthScore Health;
        public List<EconomicWarning> Warnings = new List<EconomicWarning>();
    }
    
    public class EconomicSettlementSystem : GameSystem
    {
        [Header("Health Score Weights")]
        public int ReservesWeight = 25;
        public int StabilityWeight = 25;
        public int BlockadeWeight = 25;
        public int TrendWeight = 25;
        
        [Header("Health Thresholds")]
        public int ProsperityThreshold = 90;
        public int HealthyThreshold = 70;
        public int NormalThreshold = 50;
        public int PressureThreshold = 30;
        
        [Header("Warning Thresholds")]
        public int Warning1Threshold = 50;
        public int Warning2Threshold = 35;
        public int Warning3Threshold = 20;
        
        [Header("Overdraft")]
        public float OverdraftInterestRate = 0.1f;
        public int MaxOverdraftMonths = 1;
        public int OverdraftDiplomaticPenalty = -15;
        
        [Header("Resource Limits")]
        public int SocialValueDecayThreshold = 20;
        
        private int _previousGoldLeavesIncome;
        private int _currentTurnIncome;
        private int _overdraftTurns;
        
        public override void Initialize(GameState state, GameEvents events)
        {
            base.Initialize(state, events);
            
            Events.OnTurnEnded += HandleTurnEnded;
            
            _previousGoldLeavesIncome = 0;
            _currentTurnIncome = 0;
            _overdraftTurns = 0;
            
            Debug.Log("[EconomicSettlementSystem] Initialized");
        }
        
        private void OnDestroy()
        {
            Events.OnTurnEnded -= HandleTurnEnded;
        }
        
        public SettlementResult ExecuteTurnSettlement()
        {
            var result = new SettlementResult();
            result.TurnNumber = State.CurrentTurn;
            
            CalculateIncomePhase(result);
            
            CalculateExpensePhase(result);
            
            ApplySocialValueDiscount(result);
            
            EnforceResourceBounds(result);
            
            CalculateEconomicHealth(result);
            
            CheckWarnings(result);
            
            CheckOverdraft(result);
            
            _previousGoldLeavesIncome = _currentTurnIncome;
            
            Debug.Log($"[EconomicSettlementSystem] Turn {result.TurnNumber} settlement complete. Health: {result.Health.Overall}");
            
            return result;
        }
        
        private void CalculateIncomePhase(SettlementResult result)
        {
            var goldLeaf = State.GetResource("GoldLeaf");
            int goldLeafBefore = goldLeaf?.Amount ?? 0;
            
            _currentTurnIncome = CalculateTotalIncome();
            
            if (goldLeaf != null)
            {
                goldLeaf.Amount += _currentTurnIncome;
                Events.ResourceChanged("GoldLeaf", goldLeafBefore, goldLeaf.Amount);
                
                result.ResourceChanges.Add(new ResourceChange
                {
                    ResourceId = "GoldLeaf",
                    PreviousValue = goldLeafBefore,
                    ChangeAmount = _currentTurnIncome,
                    NewValue = goldLeaf.Amount,
                    ChangeReason = "Energy export + trade hub income"
                });
            }
            
            var fireOil = State.GetResource("FireOil");
            if (fireOil != null)
            {
                int production = CalculateEnergyProduction();
                int fireOilBefore = fireOil.Amount;
                
                fireOil.Amount = Mathf.Min(fireOil.Amount + production, fireOil.MaxCapacity);
                Events.ResourceChanged("FireOil", fireOilBefore, fireOil.Amount);
                
                result.ResourceChanges.Add(new ResourceChange
                {
                    ResourceId = "FireOil",
                    PreviousValue = fireOilBefore,
                    ChangeAmount = production,
                    NewValue = fireOil.Amount,
                    ChangeReason = "Domestic energy production"
                });
            }
        }
        
        private int CalculateTotalIncome()
        {
            int totalIncome = 0;
            
            var tradeNetwork = FindSystem<TradeNetworkSystem>();
            var finance = FindSystem<FinanceSystem>();
            
            if (tradeNetwork != null && finance != null)
            {
                var allocation = tradeNetwork.GetAvailableTradeAmount();
                float tradeIncome = allocation.TotalAvailable * finance.GetGlobalOilPrice();
                totalIncome += Mathf.RoundToInt(tradeIncome);
            }
            
            return totalIncome;
        }
        
        private int CalculateEnergyProduction()
        {
            int production = 0;
            
            foreach (var region in State.Map.Regions)
            {
                foreach (var node in region.Nodes)
                {
                    if (node.NodeType == NodeType.ResourceNode && node.ControllingFactionId == "Vashid")
                    {
                        production += 8;
                    }
                }
            }
            
            return production;
        }
        
        private void CalculateExpensePhase(SettlementResult result)
        {
        }
        
        private void ApplySocialValueDiscount(SettlementResult result)
        {
            var socialValue = State.GetResource("SocialValue");
            if (socialValue == null) return;
            
            float discount = socialValue.Amount / 100f;
            
            var goldLeaf = State.GetResource("GoldLeaf");
            if (goldLeaf != null)
            {
                goldLeaf.Amount = Mathf.RoundToInt(goldLeaf.Amount * discount);
            }
        }
        
        private void EnforceResourceBounds(SettlementResult result)
        {
            foreach (var resource in State.Resources)
            {
                int previousAmount = resource.Amount;
                
                if (resource.ResourceType == ResourceType.Ratio)
                {
                    resource.Amount = Mathf.Clamp(resource.Amount, 0, 100);
                }
                else
                {
                    resource.Amount = Mathf.Clamp(resource.Amount, 0, resource.MaxCapacity);
                }
                
                if (previousAmount != resource.Amount)
                {
                    Events.ResourceChanged(resource.ResourceId, previousAmount, resource.Amount);
                    
                    result.ResourceChanges.Add(new ResourceChange
                    {
                        ResourceId = resource.ResourceId,
                        PreviousValue = previousAmount,
                        ChangeAmount = resource.Amount - previousAmount,
                        NewValue = resource.Amount,
                        ChangeReason = "Resource bound enforcement"
                    });
                }
            }
        }
        
        private void CalculateEconomicHealth(SettlementResult result)
        {
            var health = new EconomicHealthScore();
            
            health.ReservesScore = CalculateReservesScore();
            health.StabilityScore = CalculateStabilityScore();
            health.BlockadePressureScore = CalculateBlockadePressureScore();
            health.GrowthTrendScore = CalculateGrowthTrendScore();
            
            health.Overall = 
                (health.ReservesScore * ReservesWeight +
                 health.StabilityScore * StabilityWeight +
                 health.BlockadePressureScore * BlockadeWeight +
                 health.GrowthTrendScore * TrendWeight) / 100;
            
            health.Grade = GetGrade(health.Overall);
            
            int prevIncome = _previousGoldLeavesIncome;
            if (_currentTurnIncome > prevIncome)
                health.Trend = HealthTrend.Improving;
            else if (_currentTurnIncome < prevIncome)
                health.Trend = HealthTrend.Declining;
            else
                health.Trend = HealthTrend.Stable;
            
            result.Health = health;
        }
        
        private int CalculateReservesScore()
        {
            var goldLeaf = State.GetResource("GoldLeaf");
            if (goldLeaf == null) return 0;
            
            int monthlyExpense = 50;
            int monthsOfReserve = goldLeaf.Amount / monthlyExpense;
            
            if (monthsOfReserve >= 3) return 100;
            if (monthsOfReserve >= 2) return 70;
            if (monthsOfReserve >= 1) return 40;
            return 10;
        }
        
        private int CalculateStabilityScore()
        {
            var socialValue = State.GetResource("SocialValue");
            if (socialValue == null) return 50;
            
            if (socialValue.Amount >= 80) return 100;
            if (socialValue.Amount >= 60) return 70;
            if (socialValue.Amount >= 40) return 50;
            if (socialValue.Amount >= 20) return 30;
            return 10;
        }
        
        private int CalculateBlockadePressureScore()
        {
            var finance = FindSystem<FinanceSystem>();
            if (finance == null) return 100;
            
            var blockadeLevel = finance.GetBlockadeLevel();
            
            switch (blockadeLevel)
            {
                case B1.BlockadeLevel.None:
                case B1.BlockadeLevel.Financial:
                    return 100;
                case B1.BlockadeLevel.EnergyEmbargo:
                    return 60;
                case B1.BlockadeLevel.Full:
                    return 20;
                default:
                    return 100;
            }
        }
        
        private int CalculateGrowthTrendScore()
        {
            if (_previousGoldLeavesIncome == 0) return 50;
            
            int diff = _currentTurnIncome - _previousGoldLeavesIncome;
            int percentChange = (diff * 100) / _previousGoldLeavesIncome;
            
            if (percentChange > 0) return Mathf.Min(100, 50 + percentChange);
            if (percentChange < 0) return Mathf.Max(0, 50 + percentChange);
            return 50;
        }
        
        private EconomicHealthGrade GetGrade(int score)
        {
            if (score >= ProsperityThreshold) return EconomicHealthGrade.A;
            if (score >= HealthyThreshold) return EconomicHealthGrade.B;
            if (score >= NormalThreshold) return EconomicHealthGrade.C;
            if (score >= PressureThreshold) return EconomicHealthGrade.D;
            return EconomicHealthGrade.F;
        }
        
        private void CheckWarnings(SettlementResult result)
        {
            var health = result.Health;
            var goldLeaf = State.GetResource("GoldLeaf");
            int monthlyExpense = 50;
            int monthsOfReserve = goldLeaf != null ? goldLeaf.Amount / monthlyExpense : 0;
            
            if (health.Overall < Warning3Threshold || monthsOfReserve < 1)
            {
                result.Warnings.Add(new EconomicWarning
                {
                    Level = WarningLevel.Level3,
                    Message = "经济危机迫在眉睫！下回合将出现负收入，请立即采取行动！",
                    TurnsUntilCrisis = 1
                });
            }
            else if (health.Overall < Warning2Threshold || monthsOfReserve < 2)
            {
                result.Warnings.Add(new EconomicWarning
                {
                    Level = WarningLevel.Level2,
                    Message = "经济压力增大，当前路线不可持续",
                    TurnsUntilCrisis = 2
                });
            }
            else if (health.Overall < Warning1Threshold)
            {
                result.Warnings.Add(new EconomicWarning
                {
                    Level = WarningLevel.Level1,
                    Message = "经济承压，建议调整策略",
                    TurnsUntilCrisis = 3
                });
            }
        }
        
        private void CheckOverdraft(SettlementResult result)
        {
            var goldLeaf = State.GetResource("GoldLeaf");
            if (goldLeaf == null) return;
            
            if (goldLeaf.Amount < 0)
            {
                _overdraftTurns++;
                
                int interest = Mathf.RoundToInt(Mathf.Abs(goldLeaf.Amount) * OverdraftInterestRate);
                goldLeaf.Amount -= interest;
                
                var socialValue = State.GetResource("SocialValue");
                if (socialValue != null)
                {
                    socialValue.Amount = Mathf.Max(0, socialValue.Amount - 5);
                }
                
                result.Warnings.Add(new EconomicWarning
                {
                    Level = WarningLevel.Level2,
                    Message = $"透支中！利息消耗 {interest} 金叶，社稷值下降",
                    TurnsUntilCrisis = 3 - _overdraftTurns
                });
                
                if (_overdraftTurns >= 5)
                {
                    Events.GameEnded("economic_collapse");
                }
            }
            else
            {
                _overdraftTurns = 0;
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
        
        private void HandleTurnEnded(int turnNumber)
        {
            ExecuteTurnSettlement();
        }
        
        public EconomicHealthScore GetEconomicHealth()
        {
            var result = new SettlementResult();
            CalculateEconomicHealth(result);
            return result.Health;
        }
        
        public List<ResourceChange> GetResourceChangeReport()
        {
            var settlement = ExecuteTurnSettlement();
            return settlement.ResourceChanges;
        }
    }
}
