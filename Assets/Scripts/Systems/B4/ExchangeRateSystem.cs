using UnityEngine;
using System.Collections.Generic;
using EventideAge.Core;

namespace EventideAge.Systems.B4
{
    public enum Currency
    {
        GoldLeaves,
        TradeNotes,
        NorthCoins,
        Barter
    }
    
    public class ExchangeRateMap
    {
        public float GoldLeavesToStarCoin = 1.0f;
        public float GoldLeavesToTradeNotes = 0.875f;
        public float GoldLeavesToNorthCoins = 0.75f;
        
        public float TradeNotesToGoldLeaves = 1.14f;
        public float NorthCoinsToGoldLeaves = 1.33f;
    }
    
    public class ExchangeRateSystem : GameSystem
    {
        [Header("Base Exchange Rates")]
        public float GoldLeavesPerStarCoin = 1.0f;
        public float GoldLeavesPerTradeNotes = 0.875f;
        public float GoldLeavesPerNorthCoins = 0.75f;
        
        [Header("Volatility Ranges")]
        public float GoldLeavesVolatility = 0.05f;
        public float TradeNotesVolatility = 0.15f;
        public float NorthCoinsVolatility = 0.20f;
        
        [Header("Evaluation Interval")]
        public int EvaluationInterval = 5;
        
        private ExchangeRateMap _currentRates = new ExchangeRateMap();
        private int _turnsSinceLastEvaluation;
        
        public override void Initialize(GameState state, GameEvents events)
        {
            base.Initialize(state, events);
            
            _currentRates.GoldLeavesToStarCoin = GoldLeavesPerStarCoin;
            _currentRates.GoldLeavesToTradeNotes = GoldLeavesPerTradeNotes;
            _currentRates.GoldLeavesToNorthCoins = GoldLeavesPerNorthCoins;
            RecalculateDerivedRates();
            
            _turnsSinceLastEvaluation = 0;
            
            Events.OnTurnEnded += HandleTurnEnded;
            
            Debug.Log("[ExchangeRateSystem] Initialized");
        }
        
        private void OnDestroy()
        {
            Events.OnTurnEnded -= HandleTurnEnded;
        }
        
        private void HandleTurnEnded(int turnNumber)
        {
            _turnsSinceLastEvaluation++;
            
            if (_turnsSinceLastEvaluation >= EvaluationInterval)
            {
                EvaluateExchangeRates();
                _turnsSinceLastEvaluation = 0;
            }
        }
        
        private void EvaluateExchangeRates()
        {
            float marketNoise = Random.Range(-0.1f, 0.1f);
            
            _currentRates.GoldLeavesToTradeNotes = GoldLeavesPerTradeNotes * (1 + marketNoise);
            _currentRates.GoldLeavesToTradeNotes = Mathf.Clamp(
                _currentRates.GoldLeavesToTradeNotes,
                GoldLeavesPerTradeNotes * (1 - TradeNotesVolatility),
                GoldLeavesPerTradeNotes * (1 + TradeNotesVolatility)
            );
            
            float northNoise = Random.Range(-0.1f, 0.1f);
            _currentRates.GoldLeavesToNorthCoins = GoldLeavesPerNorthCoins * (1 + northNoise);
            _currentRates.GoldLeavesToNorthCoins = Mathf.Clamp(
                _currentRates.GoldLeavesToNorthCoins,
                GoldLeavesPerNorthCoins * (1 - NorthCoinsVolatility),
                GoldLeavesPerNorthCoins * (1 + NorthCoinsVolatility)
            );

            RecalculateDerivedRates();
            
            Debug.Log($"[ExchangeRateSystem] Rates updated: GoldLeaves/TradeNotes={_currentRates.GoldLeavesToTradeNotes:F3}, GoldLeaves/NorthCoins={_currentRates.GoldLeavesToNorthCoins:F3}");
        }

        private void RecalculateDerivedRates()
        {
            _currentRates.TradeNotesToGoldLeaves = _currentRates.GoldLeavesToTradeNotes > 0f
                ? 1f / _currentRates.GoldLeavesToTradeNotes
                : 0f;

            _currentRates.NorthCoinsToGoldLeaves = _currentRates.GoldLeavesToNorthCoins > 0f
                ? 1f / _currentRates.GoldLeavesToNorthCoins
                : 0f;
        }
        
        public float GetExchangeRate(Currency from, Currency to)
        {
            if (from == to)
                return 1.0f;
            
            float fromToGold;
            float goldTo;
            
            switch (from)
            {
                case Currency.GoldLeaves:
                    fromToGold = 1.0f;
                    break;
                case Currency.TradeNotes:
                    fromToGold = _currentRates.TradeNotesToGoldLeaves;
                    break;
                case Currency.NorthCoins:
                    fromToGold = _currentRates.NorthCoinsToGoldLeaves;
                    break;
                case Currency.Barter:
                    fromToGold = 0.7f;
                    break;
                default:
                    fromToGold = 1.0f;
                    break;
            }
            
            switch (to)
            {
                case Currency.GoldLeaves:
                    goldTo = 1.0f;
                    break;
                case Currency.TradeNotes:
                    goldTo = _currentRates.GoldLeavesToTradeNotes;
                    break;
                case Currency.NorthCoins:
                    goldTo = _currentRates.GoldLeavesToNorthCoins;
                    break;
                case Currency.Barter:
                    goldTo = 0.7f;
                    break;
                default:
                    goldTo = 1.0f;
                    break;
            }
            
            return fromToGold * goldTo;
        }
        
        public ExchangeRateMap GetAllRates()
        {
            return new ExchangeRateMap
            {
                GoldLeavesToStarCoin = _currentRates.GoldLeavesToStarCoin,
                GoldLeavesToTradeNotes = _currentRates.GoldLeavesToTradeNotes,
                GoldLeavesToNorthCoins = _currentRates.GoldLeavesToNorthCoins,
                TradeNotesToGoldLeaves = _currentRates.TradeNotesToGoldLeaves,
                NorthCoinsToGoldLeaves = _currentRates.NorthCoinsToGoldLeaves
            };
        }
        
        public float Convert(float amount, Currency from, Currency to)
        {
            return amount * GetExchangeRate(from, to);
        }
    }
}
