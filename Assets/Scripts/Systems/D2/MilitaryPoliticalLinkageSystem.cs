using UnityEngine;
using System.Collections.Generic;
using EventideAge.Core;
using EventideAge.Systems.D1;

namespace EventideAge.Systems.D2
{
    public class MilitaryPoliticalLinkageSystem : GameSystem
    {
        private List<string> _occupiedNodesUnderDigestion = new List<string>();
        private Dictionary<string, int> _digestionTurnsRemaining = new Dictionary<string, int>();

        public override void Initialize(GameState state, GameEvents events)
        {
            base.Initialize(state, events);
            Events.OnTurnEnded += HandleTurnEnded;
            Debug.Log("[MilitaryPoliticalLinkageSystem] Initialized");
        }
        
        private void OnDestroy()
        {
            Events.OnTurnEnded -= HandleTurnEnded;
        }
        
        public override void OnTurnEnded(int turnNumber)
        {
            ProcessAllNodeDigestion();
        }
        
        private void ProcessAllNodeDigestion()
        {
            var nodesToProcess = new List<string>(_occupiedNodesUnderDigestion);
            foreach (var nodeId in nodesToProcess)
            {
                ProcessNodeDigestion(nodeId);
            }
        }

        public void ApplyMilitaryActionPoliticalCost(MilitaryActionType actionType)
        {
            var socialValue = State.GetResource("SocialValue");
            if (socialValue == null) return;

            int socialCost = GetSocialCost(actionType);
            int oldAmount = socialValue.Amount;
            socialValue.Amount = Mathf.Max(0, socialValue.Amount - socialCost);
            Events.ResourceChanged("SocialValue", oldAmount, socialValue.Amount);
        }

        public void ApplyMilitaryActionPoliticalBenefit(string benefitType)
        {
            var socialValue = State.GetResource("SocialValue");
            if (socialValue == null) return;

            int socialGain = 0;
            switch (benefitType)
            {
                case "NodeControl": socialGain = 3; break;
                case "MajorVictory": socialGain = 5; break;
                case "DefenseSuccess": socialGain = 4; break;
            }

            int oldAmount = socialValue.Amount;
            socialValue.Amount = Mathf.Clamp(socialValue.Amount + socialGain, 0, 100);
            Events.ResourceChanged("SocialValue", oldAmount, socialValue.Amount);
        }

        public float GetActionCostModifier()
        {
            return 1f;
        }

        public float GetSuccessRateBonus()
        {
            return 0f;
        }

        private int GetSocialCost(MilitaryActionType actionType)
        {
            switch (actionType)
            {
                case MilitaryActionType.Proxy: return 2;
                case MilitaryActionType.SpecialForces: return 3;
                case MilitaryActionType.ChokepointThreat: return 5;
                case MilitaryActionType.AsymmetricDefense: return 4;
                case MilitaryActionType.NuclearDeterrence: return 10;
                case MilitaryActionType.TotalWar: return 20;
                default: return 0;
            }
        }

        public int GetDigestionTurnsForNode(string nodeId)
        {
            var node = State.GetNode(nodeId);
            if (node == null) return 0;

            if (node.NodeType == NodeType.Chokepoint)
                return 8;
            if (node.NodeType == NodeType.ResourceNode)
                return 5;
            return 3;
        }

        public void StartNodeDigestion(string nodeId)
        {
            if (!_occupiedNodesUnderDigestion.Contains(nodeId))
            {
                _occupiedNodesUnderDigestion.Add(nodeId);
                _digestionTurnsRemaining[nodeId] = GetDigestionTurnsForNode(nodeId);
            }
        }

        public void ProcessNodeDigestion(string nodeId)
        {
            if (!_occupiedNodesUnderDigestion.Contains(nodeId))
                return;

            var socialValue = State.GetResource("SocialValue");
            var arms = State.GetResource("Arms");

            if (socialValue != null)
            {
                int old = socialValue.Amount;
                socialValue.Amount = Mathf.Max(0, socialValue.Amount - 2);
                Events.ResourceChanged("SocialValue", old, socialValue.Amount);
            }

            if (arms != null)
            {
                int old = arms.Amount;
                arms.Amount = Mathf.Max(0, arms.Amount - 1);
                Events.ResourceChanged("Arms", old, arms.Amount);
            }

            if (!_digestionTurnsRemaining.ContainsKey(nodeId))
                _digestionTurnsRemaining[nodeId] = GetDigestionTurnsForNode(nodeId);

            _digestionTurnsRemaining[nodeId]--;

            if (_digestionTurnsRemaining[nodeId] <= 0)
            {
                _occupiedNodesUnderDigestion.Remove(nodeId);
                _digestionTurnsRemaining.Remove(nodeId);
                Debug.Log($"[D2] Node {nodeId} digestion complete");
            }
        }

        public bool IsNodeUnderDigestion(string nodeId)
        {
            return _occupiedNodesUnderDigestion.Contains(nodeId);
        }
    }
}