using UnityEngine;
using System.Collections.Generic;
using EventideAge.Core;
using EventideAge.Systems.C1;
using EventideAge.Systems.C2;
using EventideAge.Systems.C4;
using EventideAge.Systems.D1;
using EventideAge.Systems.D2;
using EventideAge.Systems.D3;
using EventideAge.Systems.D4;
using EventideAge.Systems.D6;
using EventideAge.Systems.B1;
using EventideAge.Systems.B2;
using EventideAge.Systems.B3;

namespace EventideAge.Systems.G
{
    public enum AIPersonality
    {
        Aggressive,
        Defensive,
        Diplomatic,
        Expansionist
    }

    public enum AIDecisionType
    {
        Military,
        Diplomatic,
        Economic,
        Intelligence,
        None
    }

    public class FactionAI
    {
        public string FactionId;
        public AIPersonality Personality;
        public float AggressionLevel;
        public float DefenseLevel;
        public float DiplomaticLevel;
        public float ExpansionLevel;
        public List<string> ActiveGoals;
        public int DecisionCooldown;
        public float ThreatPerception;
        public float OpportunityPerception;
        public List<string> HostileFactions;
        public List<string> AlliedFactions;
        public float TotalMilitaryStrength;
        public float EconomicStrength;
        public float TerritorialStrength;

        public FactionAI()
        {
            ActiveGoals = new List<string>();
            HostileFactions = new List<string>();
            AlliedFactions = new List<string>();
        }
    }

    public class AIDecision
    {
        public string FactionId;
        public AIDecisionType DecisionType;
        public string ActionId;
        public string TargetId;
        public float Priority;
        public float ExpectedOutcome;
        public int CostAp;
        public int CostGold;
        public bool ShouldExecute;
        public string Description;

        public AIDecision()
        {
            Priority = 0f;
            ExpectedOutcome = 0.5f;
            CostAp = 1;
            CostGold = 0;
            ShouldExecute = false;
        }
    }

    public class MilitaryActionOption
    {
        public MilitaryActionType ActionType;
        public string TargetNodeId;
        public float SuccessProbability;
        public float ExpectedGain;
        public float PoliticalCost;
        public int ArmsCost;
        public int GoldCost;
        public int ApCost;
    }

    public class DiplomaticActionOption
    {
        public ProtocolType ProtocolType;
        public string TargetFaction;
        public float SuccessProbability;
        public float RelationshipGain;
        public int Cost;
    }

    public class FactionAISystem : GameSystem
    {
        [Header("Personality Multipliers")]
        public float AggressiveAggression = 1.5f;
        public float DefensiveDefense = 1.5f;
        public float DiplomaticDiplomacy = 1.5f;
        public float ExpansionistExpansion = 1.5f;

        [Header("AI Update Interval")]
        public int AIUpdateInterval = 1;

        [Header("Coordination Cost")]
        public int BaseCoordinationCost = 10;

        [Header("Decision Thresholds")]
        public float MilitaryActionThreshold = 0.5f;
        public float DiplomaticActionThreshold = 0.4f;
        public float EconomicActionThreshold = 0.3f;

        [Header("Resource Thresholds for AI")]
        public int MinGoldLeafForAttack = 30;
        public int MinArmsForMilitaryAction = 5;
        public float LowResourceThreshold = 0.3f;

        private Dictionary<string, FactionAI> _factionAIs = new Dictionary<string, FactionAI>();
        private int _turnSinceLastUpdate;

        public override void Initialize(GameState state, GameEvents events)
        {
            base.Initialize(state, events);

            InitializeFactionAIs();

            Events.OnTurnEnded += HandleTurnEnded;

            Debug.Log("[FactionAISystem] Initialized with full AI decision trees");
        }

        private void OnDestroy()
        {
            Events.OnTurnEnded -= HandleTurnEnded;
        }

        private void InitializeFactionAIs()
        {
            _factionAIs["GoldLeader"] = new FactionAI
            {
                FactionId = "GoldLeader",
                Personality = AIPersonality.Aggressive,
                AggressionLevel = 0.8f,
                DefenseLevel = 0.5f,
                DiplomaticLevel = 0.3f,
                ExpansionLevel = 0.7f,
                ActiveGoals = new List<string> { "maintain_blockade", "protect_allies", "expand_influence" },
                ThreatPerception = 0.7f,
                OpportunityPerception = 0.8f
            };

            _factionAIs["HolyFire"] = new FactionAI
            {
                FactionId = "HolyFire",
                Personality = AIPersonality.Defensive,
                AggressionLevel = 0.3f,
                DefenseLevel = 0.9f,
                DiplomaticLevel = 0.4f,
                ExpansionLevel = 0.2f,
                ActiveGoals = new List<string> { "defend_core_territory", "maintain_stability" },
                ThreatPerception = 0.9f,
                OpportunityPerception = 0.3f
            };

            _factionAIs["NorthAlliance"] = new FactionAI
            {
                FactionId = "NorthAlliance",
                Personality = AIPersonality.Diplomatic,
                AggressionLevel = 0.4f,
                DefenseLevel = 0.6f,
                DiplomaticLevel = 0.8f,
                ExpansionLevel = 0.3f,
                ActiveGoals = new List<string> { "build_alliances", "trade_expansion", "maintain_neutrality" },
                ThreatPerception = 0.5f,
                OpportunityPerception = 0.6f
            };

            _factionAIs["EastTrader"] = new FactionAI
            {
                FactionId = "EastTrader",
                Personality = AIPersonality.Expansionist,
                AggressionLevel = 0.5f,
                DefenseLevel = 0.4f,
                DiplomaticLevel = 0.6f,
                ExpansionLevel = 0.9f,
                ActiveGoals = new List<string> { "control_trade", "expand_economic_influence", "form_trade_alliances" },
                ThreatPerception = 0.4f,
                OpportunityPerception = 0.9f
            };

            _factionAIs["AshCloud"] = new FactionAI
            {
                FactionId = "AshCloud",
                Personality = AIPersonality.Aggressive,
                AggressionLevel = 0.7f,
                DefenseLevel = 0.5f,
                DiplomaticLevel = 0.2f,
                ExpansionLevel = 0.6f,
                ActiveGoals = new List<string> { "destabilize_region", "exploit_conflicts", "expand_influence" },
                ThreatPerception = 0.6f,
                OpportunityPerception = 0.7f
            };

            foreach (var ai in _factionAIs.Values)
            {
                ApplyPersonalityModifiers(ai);
                UpdateFactionAssessment(ai);
            }
        }

        private void ApplyPersonalityModifiers(FactionAI ai)
        {
            switch (ai.Personality)
            {
                case AIPersonality.Aggressive:
                    ai.AggressionLevel *= AggressiveAggression;
                    ai.ThreatPerception *= 1.2f;
                    break;
                case AIPersonality.Defensive:
                    ai.DefenseLevel *= DefensiveDefense;
                    ai.ThreatPerception *= 1.3f;
                    break;
                case AIPersonality.Diplomatic:
                    ai.DiplomaticLevel *= DiplomaticDiplomacy;
                    ai.OpportunityPerception *= 1.2f;
                    break;
                case AIPersonality.Expansionist:
                    ai.ExpansionLevel *= ExpansionistExpansion;
                    ai.OpportunityPerception *= 1.3f;
                    break;
            }
        }

        private void UpdateFactionAssessment(FactionAI ai)
        {
            ai.TotalMilitaryStrength = CalculateMilitaryStrength(ai.FactionId);
            ai.EconomicStrength = CalculateEconomicStrength(ai.FactionId);
            ai.TerritorialStrength = CalculateTerritorialStrength(ai.FactionId);

            UpdateHostileAndAlliedRelations(ai);
        }

        private float CalculateMilitaryStrength(string factionId)
        {
            var techSystem = FindSystem<D6.MilitaryTechSystem>();
            var d1System = FindSystem<D1.MilitaryOperationsSystem>();

            float strength = 50f;

            if (techSystem != null)
            {
                var completedTechs = techSystem.GetCompletedTechs();
                strength += completedTechs.Length * 15f;
            }

            var controlledNodes = GetControlledNodes(factionId);
            strength += controlledNodes.Count * 10f;

            return Mathf.Clamp(strength, 0f, 200f);
        }

        private float CalculateEconomicStrength(string factionId)
        {
            var resource = State.GetResource("GoldLeaf");
            float strength = 100f;

            if (resource != null)
            {
                strength = resource.Amount;
            }

            var tradeSystem = FindSystem<B3.TradeNetworkSystem>();
            if (tradeSystem != null)
            {
                var controlledNodes = GetControlledNodes(factionId);
                strength += controlledNodes.Count * 20f;
            }

            return Mathf.Clamp(strength, 0f, 1000f);
        }

        private float CalculateTerritorialStrength(string factionId)
        {
            var controlledNodes = GetControlledNodes(factionId);
            float strength = controlledNodes.Count * 25f;

            foreach (var nodeId in controlledNodes)
            {
                var node = State.GetNode(nodeId);
                if (node != null)
                {
                    strength += node.ControlPoints * 0.25f;
                }
            }

            return Mathf.Clamp(strength, 0f, 500f);
        }

        private void UpdateHostileAndAlliedRelations(FactionAI ai)
        {
            ai.HostileFactions.Clear();
            ai.AlliedFactions.Clear();

            foreach (var faction in State.Factions)
            {
                if (faction.FactionId == ai.FactionId) continue;

                int relation = faction.RelationshipWithPlayer;

                if (relation < -30)
                    ai.HostileFactions.Add(faction.FactionId);
                else if (relation > 30)
                    ai.AlliedFactions.Add(faction.FactionId);
            }
        }

        private List<string> GetControlledNodes(string factionId)
        {
            var nodes = new List<string>();
            foreach (var region in State.Map.Regions)
            {
                foreach (var node in region.Nodes)
                {
                    if (node.ControllingFactionId == factionId)
                    {
                        nodes.Add(node.NodeId);
                    }
                }
            }
            return nodes;
        }

        private void HandleTurnEnded(int turnNumber)
        {
            _turnSinceLastUpdate++;

            foreach (var ai in _factionAIs.Values)
            {
                UpdateFactionAssessment(ai);
            }

            if (_turnSinceLastUpdate >= AIUpdateInterval)
            {
                ProcessAIDecisions();
                _turnSinceLastUpdate = 0;
            }
        }

        public void ProcessAIDecisions()
        {
            foreach (var kvp in _factionAIs)
            {
                var ai = kvp.Value;

                if (ai.DecisionCooldown > 0)
                {
                    ai.DecisionCooldown--;
                    continue;
                }

                if (!ShouldAIAct(ai))
                    continue;

                var decisions = GenerateDecisions(ai);
                if (decisions.Count > 0)
                {
                    var bestDecision = SelectBestDecision(decisions);
                    if (bestDecision.ShouldExecute)
                    {
                        ExecuteDecision(ai, bestDecision);
                        ai.DecisionCooldown = CalculateCooldown(ai, bestDecision);
                    }
                }
            }
        }

        private bool ShouldAIAct(FactionAI ai)
        {
            var goldLeaf = State.GetResource("GoldLeaf");
            if (goldLeaf == null || goldLeaf.Amount < MinGoldLeafForAttack)
            {
                return false;
            }

            return true;
        }

        private List<AIDecision> GenerateDecisions(FactionAI ai)
        {
            var decisions = new List<AIDecision>();

            var militaryOptions = GenerateMilitaryOptions(ai);
            foreach (var option in militaryOptions)
            {
                decisions.Add(option);
            }

            var diplomaticOptions = GenerateDiplomaticOptions(ai);
            foreach (var option in diplomaticOptions)
            {
                decisions.Add(option);
            }

            var economicOptions = GenerateEconomicOptions(ai);
            foreach (var option in economicOptions)
            {
                decisions.Add(option);
            }

            return decisions;
        }

        private List<AIDecision> GenerateMilitaryOptions(FactionAI ai)
        {
            var options = new List<AIDecision>();

            if (ai.Personality == AIPersonality.Defensive && ai.ThreatPerception < 0.5f)
            {
                return options;
            }

            var controlledNodes = GetControlledNodes(ai.FactionId);
            var hostileTargets = GetHostileNodes(ai);

            foreach (var target in hostileTargets)
            {
                if (ShouldAttackNode(ai, target))
                {
                    var option = EvaluateMilitaryAction(ai, target);
                    if (option != null && option.Priority >= MilitaryActionThreshold)
                    {
                        options.Add(option);
                    }
                }
            }

            if (ai.Personality == AIPersonality.Aggressive && options.Count == 0)
            {
                var neutralTargets = GetNeutralNodes(ai);
                foreach (var target in neutralTargets)
                {
                    var option = EvaluateMilitaryAction(ai, target);
                    if (option != null && option.Priority >= MilitaryActionThreshold * 0.8f)
                    {
                        options.Add(option);
                    }
                }
            }

            return options;
        }

        private List<string> GetHostileNodes(FactionAI ai)
        {
            var nodes = new List<string>();
            foreach (var hostileFaction in ai.HostileFactions)
            {
                nodes.AddRange(GetControlledNodes(hostileFaction));
            }
            return nodes;
        }

        private List<string> GetNeutralNodes(FactionAI ai)
        {
            var nodes = new List<string>();
            var controlled = GetControlledNodes(ai.FactionId);

            foreach (var region in State.Map.Regions)
            {
                foreach (var node in region.Nodes)
                {
                    if (node.ControllingFactionId != ai.FactionId &&
                        !ai.HostileFactions.Contains(node.ControllingFactionId) &&
                        !ai.AlliedFactions.Contains(node.ControllingFactionId))
                    {
                        if (IsNodeAdjacentToFriendly(node.NodeId, controlled))
                        {
                            nodes.Add(node.NodeId);
                        }
                    }
                }
            }
            return nodes;
        }

        private bool IsNodeAdjacentToFriendly(string nodeId, List<string> friendlyNodes)
        {
            return true;
        }

        private bool ShouldAttackNode(FactionAI ai, string nodeId)
        {
            if (ai.TotalMilitaryStrength < 80f)
                return false;

            if (ai.HostileFactions.Count == 0)
                return false;

            return ai.AggressionLevel * ai.ThreatPerception > 0.4f;
        }

        private AIDecision EvaluateMilitaryAction(FactionAI ai, string targetNodeId)
        {
            var node = State.GetNode(targetNodeId);
            if (node == null) return null;

            var option = new AIDecision
            {
                FactionId = ai.FactionId,
                DecisionType = AIDecisionType.Military,
                TargetId = targetNodeId,
                ActionId = "Proxy"
            };

            float attackPower = ai.TotalMilitaryStrength;
            float defensePower = CalculateNodeDefense(targetNodeId);

            option.ExpectedOutcome = attackPower / (attackPower + defensePower);
            option.ExpectedOutcome *= Random.Range(0.7f, 1.1f);
            option.ExpectedOutcome = Mathf.Clamp(option.ExpectedOutcome, 0.1f, 0.9f);

            option.Priority = CalculateMilitaryPriority(ai, node, option.ExpectedOutcome);

            option.CostAp = 1;
            option.CostGold = CalculateMilitaryCost(node);
            option.Description = $"进攻 {node.NodeName}";

            option.ShouldExecute = option.Priority >= MilitaryActionThreshold &&
                                   ai.EconomicStrength >= option.CostGold;

            return option;
        }

        private float CalculateNodeDefense(string nodeId)
        {
            var node = State.GetNode(nodeId);
            if (node == null) return 50f;

            float defense = 50f;

            switch (node.NodeType)
            {
                case NodeType.City:
                    defense = 80f;
                    break;
                case NodeType.Chokepoint:
                    defense = 60f;
                    break;
                case NodeType.Port:
                    defense = 50f;
                    break;
                case NodeType.ResourceNode:
                    defense = 40f;
                    break;
            }

            defense += node.ControlPoints * 0.3f;
            defense += node.DefenseBonus;

            return defense;
        }

        private float CalculateMilitaryPriority(FactionAI ai, NodeState target, float successProb)
        {
            float priority = ai.AggressionLevel * successProb;

            priority += ai.ThreatPerception * 0.2f;

            if (ai.Personality == AIPersonality.Expansionist)
            {
                if (target.NodeType == NodeType.ResourceNode)
                    priority += 0.2f;
                if (target.NodeType == NodeType.Chokepoint)
                    priority += 0.15f;
            }

            if (ai.Personality == AIPersonality.Aggressive)
            {
                priority += 0.15f;
            }

            if (ai.HostileFactions.Contains(target.ControllingFactionId))
            {
                priority += 0.2f;
            }

            var vashidRelation = State.GetFaction("Vashid")?.RelationshipWithPlayer ?? 0;
            if (target.ControllingFactionId == "Vashid")
            {
                priority += (vashidRelation < 0 ? 0.15f : -0.1f);
            }

            return Mathf.Clamp(priority, 0f, 1f);
        }

        private int CalculateMilitaryCost(NodeState node)
        {
            int baseCost = 20;

            switch (node.NodeType)
            {
                case NodeType.City:
                    baseCost = 35;
                    break;
                case NodeType.Chokepoint:
                    baseCost = 25;
                    break;
                case NodeType.Port:
                    baseCost = 20;
                    break;
                case NodeType.ResourceNode:
                    baseCost = 30;
                    break;
            }

            baseCost += (100 - node.ControlPoints) / 5;

            return baseCost;
        }

        private List<AIDecision> GenerateDiplomaticOptions(FactionAI ai)
        {
            var options = new List<AIDecision>();

            if (ai.AlliedFactions.Count < 2 && ai.DiplomaticLevel > 0.4f)
            {
                var potentialAllies = GetPotentialAllies(ai);
                foreach (var allyFaction in potentialAllies)
                {
                    var option = EvaluateDiplomaticAction(ai, allyFaction);
                    if (option != null && option.Priority >= DiplomaticActionThreshold)
                    {
                        options.Add(option);
                    }
                }
            }

            var blockadeSystem = FindSystem<B2.BlockadeSystem>();
            if (blockadeSystem != null && ai.HostileFactions.Count > 0)
            {
                var option = EvaluateBlockadeAction(ai);
                if (option != null)
                {
                    options.Add(option);
                }
            }

            return options;
        }

        private List<string> GetPotentialAllies(FactionAI ai)
        {
            var allies = new List<string>();

            foreach (var faction in State.Factions)
            {
                if (faction.FactionId == ai.FactionId) continue;
                if (ai.HostileFactions.Contains(faction.FactionId)) continue;
                if (ai.AlliedFactions.Contains(faction.FactionId)) continue;

                int relation = faction.RelationshipWithPlayer;
                if (relation > 20)
                {
                    allies.Add(faction.FactionId);
                }
            }

            return allies;
        }

        private AIDecision EvaluateDiplomaticAction(FactionAI ai, string targetFaction)
        {
            var option = new AIDecision
            {
                FactionId = ai.FactionId,
                DecisionType = AIDecisionType.Diplomatic,
                TargetId = targetFaction,
                ActionId = "ProposeProtocol"
            };

            var faction = State.GetFaction(targetFaction);
            if (faction == null) return null;

            int currentRelation = faction.RelationshipWithPlayer;
            option.ExpectedOutcome = (currentRelation + 50f) / 100f;
            option.ExpectedOutcome *= ai.DiplomaticLevel;

            option.Priority = ai.DiplomaticLevel * 0.5f + (currentRelation > 0 ? 0.2f : 0f);

            option.Description = $"与 {faction.FactionName} 建立协议";

            if (ai.Personality == AIPersonality.Diplomatic)
            {
                option.Priority *= 1.3f;
            }

            option.ShouldExecute = option.Priority >= DiplomaticActionThreshold;

            return option;
        }

        private AIDecision EvaluateBlockadeAction(FactionAI ai)
        {
            if (ai.EconomicStrength < 200f)
                return null;

            var option = new AIDecision
            {
                FactionId = ai.FactionId,
                DecisionType = AIDecisionType.Economic,
                TargetId = "Blockade",
                ActionId = "ApplyBlockade"
            };

            option.Priority = ai.AggressionLevel * 0.4f;
            option.Description = "实施封锁";
            option.ShouldExecute = option.Priority > 0.3f && ai.EconomicStrength > 300f;

            return option;
        }

        private List<AIDecision> GenerateEconomicOptions(FactionAI ai)
        {
            var options = new List<AIDecision>();

            if (ai.EconomicStrength < 100f)
            {
                var option = new AIDecision
                {
                    FactionId = ai.FactionId,
                    DecisionType = AIDecisionType.Economic,
                    TargetId = "Internal",
                    ActionId = "FocusOnEconomy"
                };

                option.Priority = 0.7f - (ai.EconomicStrength / 100f);
                option.Description = "专注经济发展";
                option.ShouldExecute = true;
                options.Add(option);
            }

            var tradeNodes = GetTradeNodes(ai);
            foreach (var node in tradeNodes)
            {
                if (!GetControlledNodes(ai.FactionId).Contains(node))
                {
                    var option = EvaluateTradeNodeAction(ai, node);
                    if (option != null)
                    {
                        options.Add(option);
                    }
                }
            }

            return options;
        }

        private List<string> GetTradeNodes(FactionAI ai)
        {
            var tradeNodes = new List<string> { "Hormuz", "GulfPort", "Basra" };
            return tradeNodes;
        }

        private AIDecision EvaluateTradeNodeAction(FactionAI ai, string nodeId)
        {
            var node = State.GetNode(nodeId);
            if (node == null) return null;

            var option = new AIDecision
            {
                FactionId = ai.FactionId,
                DecisionType = AIDecisionType.Economic,
                TargetId = nodeId,
                ActionId = "SecureTradeNode"
            };

            option.Priority = 0.35f;
            if (ai.Personality == AIPersonality.Expansionist)
            {
                option.Priority = 0.5f;
            }

            option.Description = $"控制贸易节点 {node.NodeName}";
            option.ShouldExecute = ai.EconomicStrength > 200f && option.Priority > EconomicActionThreshold;

            return option;
        }

        private AIDecision SelectBestDecision(List<AIDecision> decisions)
        {
            if (decisions.Count == 0)
                return new AIDecision { ShouldExecute = false };

            decisions.Sort((a, b) => b.Priority.CompareTo(a.Priority));

            return decisions[0];
        }

        private int CalculateCooldown(FactionAI ai, AIDecision decision)
        {
            int baseCooldown = 2;

            switch (decision.DecisionType)
            {
                case AIDecisionType.Military:
                    baseCooldown = 3;
                    if (ai.Personality == AIPersonality.Aggressive)
                        baseCooldown = 2;
                    break;
                case AIDecisionType.Diplomatic:
                    baseCooldown = 2;
                    if (ai.Personality == AIPersonality.Diplomatic)
                        baseCooldown = 1;
                    break;
                case AIDecisionType.Economic:
                    baseCooldown = 1;
                    break;
            }

            return baseCooldown;
        }

        private void ExecuteDecision(FactionAI ai, AIDecision decision)
        {
            Debug.Log($"[FactionAI] {ai.FactionId} executing {decision.DecisionType} decision: {decision.Description} (Priority: {decision.Priority:F2})");

            switch (decision.DecisionType)
            {
                case AIDecisionType.Military:
                    ExecuteMilitaryDecision(ai, decision);
                    break;
                case AIDecisionType.Diplomatic:
                    ExecuteDiplomaticDecision(ai, decision);
                    break;
                case AIDecisionType.Economic:
                    ExecuteEconomicDecision(ai, decision);
                    break;
            }
        }

        private void ExecuteMilitaryDecision(FactionAI ai, AIDecision decision)
        {
            var d1System = FindSystem<D1.MilitaryOperationsSystem>();
            var d2System = FindSystem<D2.MilitaryPoliticalLinkageSystem>();

            if (d1System == null || d2System == null)
            {
                Debug.LogWarning($"[FactionAI] Military systems not found for {ai.FactionId}");
                return;
            }

            var goldLeaf = State.GetResource("GoldLeaf");
            if (goldLeaf != null && goldLeaf.Amount >= decision.CostGold)
            {
                goldLeaf.Amount -= decision.CostGold;
                Debug.Log($"[FactionAI] {ai.FactionId} spent {decision.CostGold} GoldLeaf on military action");
            }

            Debug.Log($"[FactionAI] {ai.FactionId} executed military action: {decision.ActionId} on {decision.TargetId}");
        }

        private void ExecuteDiplomaticDecision(FactionAI ai, AIDecision decision)
        {
            var c2System = FindSystem<C2.DiplomaticProtocolsSystem>();

            if (c2System == null)
            {
                Debug.LogWarning($"[FactionAI] Diplomatic systems not found for {ai.FactionId}");
                return;
            }

            if (decision.ActionId == "ProposeProtocol")
            {
                Debug.Log($"[FactionAI] {ai.FactionId} proposes diplomatic protocol to {decision.TargetId}");
            }

            Debug.Log($"[FactionAI] {ai.FactionId} executed diplomatic action: {decision.ActionId} with {decision.TargetId}");
        }

        private void ExecuteEconomicDecision(FactionAI ai, AIDecision decision)
        {
            if (decision.ActionId == "ApplyBlockade")
            {
                var blockadeSystem = FindSystem<B2.BlockadeSystem>();
                if (blockadeSystem != null)
                {
                    Debug.Log($"[FactionAI] {ai.FactionId} applies blockade");
                }
            }
            else if (decision.ActionId == "FocusOnEconomy")
            {
                Debug.Log($"[FactionAI] {ai.FactionId} focuses on internal economy");
            }
            else if (decision.ActionId == "SecureTradeNode")
            {
                Debug.Log($"[FactionAI] {ai.FactionId} attempts to secure trade node {decision.TargetId}");
            }

            Debug.Log($"[FactionAI] {ai.FactionId} executed economic action: {decision.ActionId}");
        }

        private T FindSystem<T>() where T : GameSystem
        {
            if (GameManager.Instance == null) return null;

            foreach (var system in GameManager.Instance.Systems)
            {
                if (system is T typedSystem)
                    return typedSystem;
            }
            return null;
        }

        public FactionAI GetAI(string factionId)
        {
            if (_factionAIs.TryGetValue(factionId, out var ai))
                return ai;
            return null;
        }

        public Dictionary<string, FactionAI> GetAllAIs()
        {
            return new Dictionary<string, FactionAI>(_factionAIs);
        }

        public List<AIDecision> GetCurrentDecisions(string factionId)
        {
            var ai = GetAI(factionId);
            if (ai == null) return new List<AIDecision>();

            return GenerateDecisions(ai);
        }
    }
}