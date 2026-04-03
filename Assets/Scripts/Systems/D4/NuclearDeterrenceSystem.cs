using UnityEngine;
using System;
using EventideAge.Core;

namespace EventideAge.Systems.D4
{
    public enum NuclearCapabilityLevel
    {
        None,
        Limited,
        Credible,
        Enhanced,
        Absolute
    }

    [Serializable]
    public class NuclearDeterrenceState
    {
        public NuclearCapabilityLevel CapabilityLevel = NuclearCapabilityLevel.None;
        public int WarheadCount = 0;
        public int DisplayCooldown = 0;
        public bool IsFullWarLockActive = false;
        public int FullWarLockTurnsRemaining = 0;
    }

    public class NuclearDeterrenceSystem : GameSystem
    {
        private NuclearDeterrenceState _deterrenceState = new NuclearDeterrenceState();

        [Header("Warhead Thresholds")]
        public int LimitedThreshold = 1;
        public int CredibleThreshold = 11;
        public int EnhancedThreshold = 31;
        public int AbsoluteThreshold = 61;

        [Header("Display Costs")]
        public int LimitedDisplayAshWillCost = 10;
        public int CredibleDisplayAshWillCost = 15;
        public int CredibleDisplayTradeTokenCost = 10;
        public int EnhancedDisplayAshWillCost = 20;
        public int EnhancedDisplayTradeTokenCost = 15;
        public int AbsoluteDisplayAshWillCost = 25;
        public int AbsoluteDisplayTradeTokenCost = 20;

        [Header("Cooldown")]
        public int BaseCooldownTurns = 3;

        public override void Initialize(GameState state, GameEvents events)
        {
            base.Initialize(state, events);
            Events.OnTurnEnded += HandleTurnEnded;
            Debug.Log("[NuclearDeterrenceSystem] Initialized");
        }
        
        private void OnDestroy()
        {
            Events.OnTurnEnded -= HandleTurnEnded;
        }
        
        public override void OnTurnEnded(int turnNumber)
        {
            ProcessTurnCooldown();
        }
        
        public NuclearDeterrenceState GetState()
        {
            return _deterrenceState;
        }

        public void SetWarheadCount(int count)
        {
            _deterrenceState.WarheadCount = Mathf.Clamp(count, 0, 100);
            _deterrenceState.CapabilityLevel = CalculateCapabilityLevel(_deterrenceState.WarheadCount);
        }
        
        public void RestoreState(NuclearDeterrenceState state)
        {
            if (state == null) return;
            _deterrenceState.WarheadCount = state.WarheadCount;
            _deterrenceState.CapabilityLevel = state.CapabilityLevel;
            _deterrenceState.DisplayCooldown = state.DisplayCooldown;
            _deterrenceState.IsFullWarLockActive = state.IsFullWarLockActive;
            _deterrenceState.FullWarLockTurnsRemaining = state.FullWarLockTurnsRemaining;
        }

        public bool CanDisplayDeterrence()
        {
            if (_deterrenceState.CapabilityLevel == NuclearCapabilityLevel.None)
                return false;

            if (_deterrenceState.DisplayCooldown > 0)
                return false;

            return true;
        }

        public bool ExecuteDeterrenceDisplay()
        {
            if (!CanDisplayDeterrence())
                return false;

            if (!SpendDisplayCosts())
                return false;

            ApplyDeterrenceEffects();
            _deterrenceState.DisplayCooldown = BaseCooldownTurns;
            _deterrenceState.IsFullWarLockActive = true;
            _deterrenceState.FullWarLockTurnsRemaining = 2;

            Debug.Log($"[NuclearDeterrence] Deterrence display executed. Level: {_deterrenceState.CapabilityLevel}");
            return true;
        }

        private bool SpendDisplayCosts()
        {
            var ashWill = State.GetResource("AshWill");
            var tradeToken = State.GetResource("TradeToken");

            int ashCost = 0;
            int tradeCost = 0;

            switch (_deterrenceState.CapabilityLevel)
            {
                case NuclearCapabilityLevel.Limited:
                    ashCost = LimitedDisplayAshWillCost;
                    break;
                case NuclearCapabilityLevel.Credible:
                    ashCost = CredibleDisplayAshWillCost;
                    tradeCost = CredibleDisplayTradeTokenCost;
                    break;
                case NuclearCapabilityLevel.Enhanced:
                    ashCost = EnhancedDisplayAshWillCost;
                    tradeCost = EnhancedDisplayTradeTokenCost;
                    break;
                case NuclearCapabilityLevel.Absolute:
                    ashCost = AbsoluteDisplayAshWillCost;
                    tradeCost = AbsoluteDisplayTradeTokenCost;
                    break;
            }

            if (ashWill != null && ashWill.Amount < ashCost)
                return false;

            if (tradeToken != null && tradeToken.Amount < tradeCost)
                return false;

            if (ashWill != null)
            {
                int old = ashWill.Amount;
                ashWill.Amount -= ashCost;
                Events.ResourceChanged("AshWill", old, ashWill.Amount);
            }

            if (tradeToken != null)
            {
                int old = tradeToken.Amount;
                tradeToken.Amount -= tradeCost;
                Events.ResourceChanged("TradeToken", old, tradeToken.Amount);
            }

            return true;
        }

        private void ApplyDeterrenceEffects()
        {
            foreach (var faction in State.Factions)
            {
                if (!faction.IsPlayerControlled)
                {
                    int relationDelta = -5;
                    int oldRelation = faction.RelationshipWithPlayer;
                    faction.RelationshipWithPlayer = Mathf.Clamp(faction.RelationshipWithPlayer + relationDelta, -100, 100);
                    Events.RelationshipChanged(faction.FactionId, faction.RelationshipWithPlayer - oldRelation);
                }
            }

            if (_deterrenceState.CapabilityLevel >= NuclearCapabilityLevel.Enhanced)
            {
                _deterrenceState.IsFullWarLockActive = true;
            }
        }

        public void ProcessTurnCooldown()
        {
            if (_deterrenceState.DisplayCooldown > 0)
                _deterrenceState.DisplayCooldown--;

            if (_deterrenceState.FullWarLockTurnsRemaining > 0)
                _deterrenceState.FullWarLockTurnsRemaining--;

            if (_deterrenceState.FullWarLockTurnsRemaining <= 0)
                _deterrenceState.IsFullWarLockActive = false;
        }

        public bool IsFullWarLocked()
        {
            return _deterrenceState.IsFullWarLockActive;
        }

        public float GetEnemySuccessRateModifier()
        {
            if (_deterrenceState.IsFullWarLockActive)
                return 0.8f;
            return 1.0f;
        }

        public int GetDiplomaticDifficultyModifier()
        {
            if (_deterrenceState.DisplayCooldown > 0)
                return 20;
            return 0;
        }

        private NuclearCapabilityLevel CalculateCapabilityLevel(int warheadCount)
        {
            if (warheadCount >= AbsoluteThreshold)
                return NuclearCapabilityLevel.Absolute;
            if (warheadCount >= EnhancedThreshold)
                return NuclearCapabilityLevel.Enhanced;
            if (warheadCount >= CredibleThreshold)
                return NuclearCapabilityLevel.Credible;
            if (warheadCount >= LimitedThreshold)
                return NuclearCapabilityLevel.Limited;
            return NuclearCapabilityLevel.None;
        }
    }
}