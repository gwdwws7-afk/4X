using UnityEngine;
using System;
using System.Collections.Generic;
using EventideAge.Core;

namespace EventideAge.Systems.B1
{
    public enum BlockadeLevel
    {
        None,
        Financial,
        EnergyEmbargo,
        Full
    }
    
    public enum BlockadeType
    {
        EnergyEmbargo,
        FinancialBlockade,
        SecondaryBlockade,
        MilitaryEmbargo,
        NavalBlockade
    }
    
    public enum SettlementChannel
    {
        GoldLeaves,
        TradeNotes,
        NorthCoins,
        Barter,
        GreyMarket
    }
    
    public class ExchangeResult
    {
        public float GoldLeaves;
        public float TradeNotes;
        public float NorthCoins;
        public float BarterValue;
        public float GreyMarketValue;
    }
    
    public class ChannelAvailability
    {
        public bool GoldLeaves;
        public bool TradeNotes;
        public bool NorthCoins;
        public bool Barter;
        public bool GreyMarket;
    }
    
    public class BlockadeSystemData
    {
        public BlockadeLevel Level;
        public Dictionary<BlockadeType, bool> ActiveBlockades = new Dictionary<BlockadeType, bool>();
        public float TotalPressure;
        public float[] BlockadePressure = new float[5];
    }
    
    public class FinanceSystem : GameSystem
    {
        [Header("Finance Parameters")]
        public float BaseOilPrice = 80f;
        public float VashidMarketShareBonus = -0.1f;
        public float ExportRatio = 0.7f;
        public float DomesticConsumptionRatio = 0.3f;
        
        [Header("Channel Exchange Rates")]
        public float GoldLeavesPerFireOil = 80f;
        public float TradeNotesPerFireOil = 70f;
        public float NorthCoinsPerFireOil = 60f;
        public float BarterEfficiency = 0.7f;
        public float GreyMarketPremium = 1.4f;
        
        [Header("Blockade Coefficients")]
        public float[] BlockadeDiscount = new float[] { 1.0f, 0.6f, 0.3f, 0.1f };
        public float[] StarCoinAvailability = new float[] { 1.0f, 0.2f, 0.2f, 0.1f, 0.05f };
        
        [Header("Grey Market")]
        public float SmugglingExposureRate = 0.1f;
        public float GreyMarketCostMultiplier = 1.4f;
        public int SmugglingExposurePenalty;
        public int MaxSmugglingExposurePenalty = -30;
        
        private BlockadeSystemData _blockadeData = new BlockadeSystemData();
        private float _globalOilPrice;
        private float _previousTurnIncome;
        
        public override void Initialize(GameState state, GameEvents events)
        {
            base.Initialize(state, events);
            
            _globalOilPrice = BaseOilPrice;
            
            _blockadeData.Level = BlockadeLevel.None;
            for (int i = 0; i < 5; i++)
            {
                _blockadeData.ActiveBlockades[(BlockadeType)i] = false;
                _blockadeData.BlockadePressure[i] = 0f;
            }
            _blockadeData.TotalPressure = 0f;
            
            Events.OnTurnEnded += HandleTurnEnded;
            
            Debug.Log("[FinanceSystem] Initialized");
        }
        
        private void OnDestroy()
        {
            Events.OnTurnEnded -= HandleTurnEnded;
        }
        
        private void HandleTurnEnded(int turnNumber)
        {
            CalculateGlobalOilPrice();
        }
        
        public void SetBlockadeLevel(BlockadeLevel level)
        {
            _blockadeData.Level = level;
            Debug.Log($"[FinanceSystem] Blockade level set to: {level}");
        }
        
        public void SetBlockadeActive(BlockadeType type, bool active, float pressure)
        {
            _blockadeData.ActiveBlockades[type] = active;
            int index = (int)type;
            _blockadeData.BlockadePressure[index] = pressure;
            
            CalculateTotalPressure();
            UpdateBlockadeLevel();
        }
        
        private void CalculateTotalPressure()
        {
            float[] pressureWeights = { 30f, 25f, 20f, 15f, 40f };
            _blockadeData.TotalPressure = 0f;
            
            for (int i = 0; i < 5; i++)
            {
                if (_blockadeData.ActiveBlockades[(BlockadeType)i])
                {
                    _blockadeData.TotalPressure += _blockadeData.BlockadePressure[i];
                }
            }
        }
        
        private void UpdateBlockadeLevel()
        {
            if (_blockadeData.TotalPressure < 25f)
                _blockadeData.Level = BlockadeLevel.None;
            else if (_blockadeData.TotalPressure < 60f)
                _blockadeData.Level = BlockadeLevel.Financial;
            else if (_blockadeData.TotalPressure < 100f)
                _blockadeData.Level = BlockadeLevel.EnergyEmbargo;
            else
                _blockadeData.Level = BlockadeLevel.Full;
        }
        
        private void CalculateGlobalOilPrice()
        {
            float adjustment = UnityEngine.Random.Range(-0.05f, 0.05f);
            _globalOilPrice = BaseOilPrice * (1 + VashidMarketShareBonus + adjustment);
            _globalOilPrice = Mathf.Clamp(_globalOilPrice, BaseOilPrice * 0.7f, BaseOilPrice * 1.4f);
        }
        
        public float CalculateEnergyExportIncome(float energyOutput, float routeEfficiency)
        {
            float domesticIncome = energyOutput * DomesticConsumptionRatio * _globalOilPrice;
            
            float exportAmount = energyOutput * ExportRatio;
            float exportPrice = _globalOilPrice * (1 + VashidMarketShareBonus);
            float starCoinCoeff = GetStarCoinAvailability();
            float blockadeDiscount = GetBlockadeDiscount();
            
            float exportIncome = exportAmount * exportPrice * starCoinCoeff * blockadeDiscount * routeEfficiency;
            
            _previousTurnIncome = exportIncome + domesticIncome;
            
            return exportIncome + domesticIncome;
        }
        
        public float GetStarCoinAvailability()
        {
            return StarCoinAvailability[(int)_blockadeData.Level];
        }
        
        public float GetBlockadeDiscount()
        {
            return BlockadeDiscount[(int)_blockadeData.Level];
        }
        
        public ExchangeResult CalculateEnergyExchange(float fireOilAmount)
        {
            var result = new ExchangeResult();
            
            result.GoldLeaves = fireOilAmount * GoldLeavesPerFireOil * GetStarCoinAvailability();
            result.TradeNotes = fireOilAmount * TradeNotesPerFireOil;
            result.NorthCoins = fireOilAmount * NorthCoinsPerFireOil;
            result.BarterValue = fireOilAmount * GoldLeavesPerFireOil * BarterEfficiency;
            result.GreyMarketValue = fireOilAmount * GoldLeavesPerFireOil * GetBlockadeDiscount() / GreyMarketPremium;
            
            return result;
        }
        
        public ChannelAvailability GetChannelAvailability()
        {
            var avail = new ChannelAvailability();
            
            avail.GoldLeaves = true;
            avail.TradeNotes = HasSacredFireTradeAgreement();
            avail.NorthCoins = HasGoldenHordAlliance();
            avail.Barter = true;
            avail.GreyMarket = SmugglingExposurePenalty > MaxSmugglingExposurePenalty;
            
            return avail;
        }
        
        private bool HasSacredFireTradeAgreement()
        {
            var vashid = State.GetFaction(GameIds.Faction.Vashid);
            if (vashid == null) return false;
            return vashid.FactionPolicies.Contains("TradeAgreement_East");
        }
        
        private bool HasGoldenHordAlliance()
        {
            var vashid = State.GetFaction(GameIds.Faction.Vashid);
            if (vashid == null) return false;
            return vashid.RelationshipWithPlayer >= 60;
        }
        
        public bool TryPurchaseWithCurrency(string currency, float amount, float price)
        {
            if (string.IsNullOrWhiteSpace(currency))
                return false;

            string resourceId = ResolveSettlementResourceId(currency);
            if (string.IsNullOrEmpty(resourceId))
                return false;

            int totalCost = (int)(amount * price);
            if (totalCost < 0)
                return false;

            return TrySpendResource(resourceId, totalCost);
        }

        private string ResolveSettlementResourceId(string currency)
        {
            string canonicalResourceId = GameIds.ResolveResourceId(currency);
            if (canonicalResourceId == GameIds.Resource.GoldLeaf)
                return GameIds.Resource.GoldLeaf;

            if (canonicalResourceId == GameIds.Resource.TradeToken)
                return GameIds.Resource.TradeToken;

            return null;
        }

        private bool TrySpendResource(string resourceId, int amount)
        {
            var resource = State.GetResource(resourceId);
            if (resource == null || resource.Amount < amount)
                return false;

            int oldAmount = resource.Amount;
            resource.Amount -= amount;
            Events.ResourceChanged(resourceId, oldAmount, resource.Amount);
            return true;
        }
        
        public bool AttemptSmuggling()
        {
            if (SmugglingExposurePenalty <= MaxSmugglingExposurePenalty)
                return false;
            
            if (UnityEngine.Random.value < SmugglingExposureRate)
            {
                SmugglingExposurePenalty -= 10;
                Debug.Log($"[FinanceSystem] Smuggling exposed! Exposure penalty: {SmugglingExposurePenalty}");
                return true;
            }
            return false;
        }
        
        public float GetGlobalOilPrice() => _globalOilPrice;
        public BlockadeLevel GetBlockadeLevel() => _blockadeData.Level;
        public float GetTotalBlockadePressure() => _blockadeData.TotalPressure;
        public int GetSmugglingExposurePenalty() => SmugglingExposurePenalty;
        
        public EconomicState GetEconomicState()
        {
            var state = new EconomicState();
            
            var goldLeaf = State.GetResource(GameIds.Resource.GoldLeaf);
            state.GoldLeaf = goldLeaf?.Amount ?? 0;
            
            var fireOil = State.GetResource(GameIds.Resource.FireOil);
            state.FireOil = fireOil?.Amount ?? 0;
            
            var tradeNotes = State.GetResource(GameIds.Resource.TradeToken);
            state.TradeToken = tradeNotes?.Amount ?? 0;

            state.NorthCoins = EstimateNorthCoinsSettlement(state.FireOil);
            
            state.BlockadeLevel = _blockadeData.Level;
            state.GlobalOilPrice = _globalOilPrice;
            state.EnergyExportIncome = _previousTurnIncome;
            state.DomesticEnergyIncome = 0;
            
            return state;
        }

        private int EstimateNorthCoinsSettlement(int fireOilStock)
        {
            if (fireOilStock <= 0)
                return 0;

            var channels = GetChannelAvailability();
            if (!channels.NorthCoins)
                return 0;

            float exportableFireOil = fireOilStock * ExportRatio;
            return Mathf.Max(0, Mathf.RoundToInt(exportableFireOil * NorthCoinsPerFireOil));
        }
    }
    
    public class EconomicState
    {
        public int GoldLeaf;
        public int FireOil;
        public int TradeToken;
        public int NorthCoins;
        public BlockadeLevel BlockadeLevel;
        public float GlobalOilPrice;
        public float EnergyExportIncome;
        public float DomesticEnergyIncome;

        [Obsolete("Use GoldLeaf")]
        public int GoldLeaves
        {
            get => GoldLeaf;
            set => GoldLeaf = value;
        }

        [Obsolete("Use TradeToken")]
        public int TradeNotes
        {
            get => TradeToken;
            set => TradeToken = value;
        }
    }
}
