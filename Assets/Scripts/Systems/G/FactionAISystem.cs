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
using EventideAge.Systems.D5;
using EventideAge.Systems.D6;
using EventideAge.Systems.B1;
using EventideAge.Systems.B2;
using EventideAge.Systems.B3;
using EventideAge.Systems.H2;

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
        public NodeNetworkSystem NodeNetworkSystem { get; set; }

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
        private Dictionary<string, HashSet<string>> _nodeAdjacency = new Dictionary<string, HashSet<string>>();
        private Dictionary<string, string> _nodeRegionLookup = new Dictionary<string, string>();
        private int _turnSinceLastUpdate;

        public override void Initialize(GameState state, GameEvents events)
        {
            base.Initialize(state, events);

            BuildNodeAdjacency();
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
            _factionAIs[GameIds.Faction.Aurean] = new FactionAI
            {
                FactionId = GameIds.Faction.Aurean,
                Personality = AIPersonality.Aggressive,
                AggressionLevel = 0.8f,
                DefenseLevel = 0.5f,
                DiplomaticLevel = 0.3f,
                ExpansionLevel = 0.7f,
                ActiveGoals = new List<string> { "maintain_blockade", "protect_allies", "expand_influence" },
                ThreatPerception = 0.7f,
                OpportunityPerception = 0.8f
            };

            _factionAIs[GameIds.Faction.SacredFire] = new FactionAI
            {
                FactionId = GameIds.Faction.SacredFire,
                Personality = AIPersonality.Defensive,
                AggressionLevel = 0.3f,
                DefenseLevel = 0.9f,
                DiplomaticLevel = 0.4f,
                ExpansionLevel = 0.2f,
                ActiveGoals = new List<string> { "defend_core_territory", "maintain_stability" },
                ThreatPerception = 0.9f,
                OpportunityPerception = 0.3f
            };

            _factionAIs[GameIds.Faction.GoldenHord] = new FactionAI
            {
                FactionId = GameIds.Faction.GoldenHord,
                Personality = AIPersonality.Diplomatic,
                AggressionLevel = 0.4f,
                DefenseLevel = 0.6f,
                DiplomaticLevel = 0.8f,
                ExpansionLevel = 0.3f,
                ActiveGoals = new List<string> { "build_alliances", "trade_expansion", "maintain_balance" },
                ThreatPerception = 0.5f,
                OpportunityPerception = 0.6f
            };

            _factionAIs[GameIds.Faction.AshConfederacy] = new FactionAI
            {
                FactionId = GameIds.Faction.AshConfederacy,
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
            var resource = State.GetResource(GameIds.Resource.GoldLeaf);
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
            string aiFactionId = GameIds.ResolveFactionId(ai.FactionId);

            foreach (var faction in State.Factions)
            {
                string factionId = GameIds.ResolveFactionId(faction.FactionId);
                if (factionId == aiFactionId) continue;

                int relation = faction.RelationshipWithPlayer;

                if (relation < -30)
                    ai.HostileFactions.Add(factionId);
                else if (relation > 30)
                    ai.AlliedFactions.Add(factionId);
            }
        }

        private List<string> GetControlledNodes(string factionId)
        {
            factionId = GameIds.ResolveFactionId(factionId);
            var nodes = new List<string>();
            foreach (var region in State.Map.Regions)
            {
                foreach (var node in region.Nodes)
                {
                    if (GameIds.ResolveFactionId(node.ControllingFactionId) == factionId)
                    {
                        nodes.Add(GameIds.ResolveNodeId(node.NodeId));
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
            var goldLeaf = State.GetResource(GameIds.Resource.GoldLeaf);
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
            string aiFactionId = GameIds.ResolveFactionId(ai.FactionId);

            foreach (var region in State.Map.Regions)
            {
                foreach (var node in region.Nodes)
                {
                    string controller = GameIds.ResolveFactionId(node.ControllingFactionId);
                    string nodeId = GameIds.ResolveNodeId(node.NodeId);

                    if (controller != aiFactionId &&
                        !ai.HostileFactions.Contains(controller) &&
                        !ai.AlliedFactions.Contains(controller))
                    {
                        if (IsNodeAdjacentToFriendly(nodeId, controlled))
                        {
                            nodes.Add(nodeId);
                        }
                    }
                }
            }
            return nodes;
        }

        private bool IsNodeAdjacentToFriendly(string nodeId, List<string> friendlyNodes)
        {
            nodeId = GameIds.ResolveNodeId(nodeId);
            if (string.IsNullOrEmpty(nodeId) || friendlyNodes == null || friendlyNodes.Count == 0)
                return false;

            var canonicalFriendlyNodes = new List<string>(friendlyNodes.Count);
            for (int i = 0; i < friendlyNodes.Count; i++)
            {
                canonicalFriendlyNodes.Add(GameIds.ResolveNodeId(friendlyNodes[i]));
            }

            foreach (var friendly in canonicalFriendlyNodes)
            {
                if (friendly == nodeId)
                    return true;
            }

            if (NodeNetworkSystem != null)
            {
                foreach (var friendly in canonicalFriendlyNodes)
                {
                    if (NodeNetworkSystem.AreAdjacent(nodeId, friendly))
                        return true;
                }
            }

            if (_nodeAdjacency.Count == 0)
            {
                BuildNodeAdjacency();
            }

            if (_nodeAdjacency.TryGetValue(nodeId, out var neighbors))
            {
                foreach (var friendly in canonicalFriendlyNodes)
                {
                    if (neighbors.Contains(friendly))
                        return true;
                }
            }

            if (_nodeRegionLookup.TryGetValue(nodeId, out var regionId))
            {
                foreach (var friendly in canonicalFriendlyNodes)
                {
                    if (_nodeRegionLookup.TryGetValue(friendly, out var friendlyRegion) && friendlyRegion == regionId)
                        return true;
                }
            }

            return false;
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
            targetNodeId = GameIds.ResolveNodeId(targetNodeId);
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
            nodeId = GameIds.ResolveNodeId(nodeId);
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

            string targetController = GameIds.ResolveFactionId(target.ControllingFactionId);
            if (ai.HostileFactions.Contains(targetController))
            {
                priority += 0.2f;
            }

            var vashidRelation = State.GetFaction(GameIds.Faction.Vashid)?.RelationshipWithPlayer ?? 0;
            if (targetController == GameIds.Faction.Vashid)
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
            string aiFactionId = GameIds.ResolveFactionId(ai.FactionId);

            foreach (var faction in State.Factions)
            {
                string factionId = GameIds.ResolveFactionId(faction.FactionId);
                if (factionId == aiFactionId) continue;
                if (ai.HostileFactions.Contains(factionId)) continue;
                if (ai.AlliedFactions.Contains(factionId)) continue;

                int relation = faction.RelationshipWithPlayer;
                if (relation > 20)
                {
                    allies.Add(factionId);
                }
            }

            return allies;
        }

        private AIDecision EvaluateDiplomaticAction(FactionAI ai, string targetFaction)
        {
            targetFaction = GameIds.ResolveFactionId(targetFaction);
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
            var controlledNodes = GetControlledNodes(ai.FactionId);
            foreach (var node in tradeNodes)
            {
                if (!controlledNodes.Contains(node))
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
            var tradeNodes = new List<string> { GameIds.Node.Hormuz, GameIds.Node.Bushehr, GameIds.Node.TradeHub };
            return tradeNodes;
        }

        private AIDecision EvaluateTradeNodeAction(FactionAI ai, string nodeId)
        {
            nodeId = GameIds.ResolveNodeId(nodeId);
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
            string targetNodeId = GameIds.ResolveNodeId(decision.TargetId);
            var d1System = FindSystem<D1.MilitaryOperationsSystem>();
            var d2System = FindSystem<D2.MilitaryPoliticalLinkageSystem>();
            var d5System = FindSystem<D5.WarResolutionSystem>();

            if (d1System == null)
            {
                Debug.LogWarning($"[FactionAI] Military systems not found for {ai.FactionId}");
                return;
            }

            MilitaryActionType actionType = ResolveMilitaryActionType(decision, ai);
            bool executed = d1System.ExecuteActionForAI(actionType, targetNodeId);

            if (executed && d2System != null)
            {
                d2System.StartNodeDigestion(targetNodeId);
            }

            if (executed && d5System != null && actionType == MilitaryActionType.TotalWar)
            {
                d5System.CheckWarConclusion(targetNodeId);
            }

            Events.ActionLogAdded("G", $"{ai.FactionId} military action {actionType} on {targetNodeId}: {(executed ? "executed" : "failed")}", executed ? FeedbackSeverity.Warning : FeedbackSeverity.Info);
            Debug.Log($"[FactionAI] {ai.FactionId} executed military action: {actionType} on {targetNodeId}");
        }

        private void ExecuteDiplomaticDecision(FactionAI ai, AIDecision decision)
        {
            string fromFactionId = GameIds.ResolveFactionId(ai.FactionId);
            string targetFactionId = GameIds.ResolveFactionId(decision.TargetId);
            var c2System = FindSystem<C2.DiplomaticProtocolsSystem>();

            if (c2System == null)
            {
                Debug.LogWarning($"[FactionAI] Diplomatic systems not found for {ai.FactionId}");
                return;
            }

            if (decision.ActionId == "ProposeProtocol")
            {
                if (string.IsNullOrEmpty(targetFactionId) || targetFactionId == fromFactionId)
                {
                    Debug.LogWarning($"[FactionAI] Invalid diplomatic target for {ai.FactionId}");
                    return;
                }

                ProtocolType type = ResolveProtocolType(ai, decision);
                var proposal = c2System.ProposeProtocol(fromFactionId, targetFactionId, type);
                if (proposal != null)
                {
                    c2System.SignProtocol(proposal.ProtocolId);
                    Events.ActionLogAdded("G", $"{fromFactionId} signed {type} with {targetFactionId}", FeedbackSeverity.Info);
                }
            }

            Debug.Log($"[FactionAI] {fromFactionId} executed diplomatic action: {decision.ActionId} with {targetFactionId}");
        }

        private void ExecuteEconomicDecision(FactionAI ai, AIDecision decision)
        {
            string targetNodeId = GameIds.ResolveNodeId(decision.TargetId);
            if (decision.ActionId == "ApplyBlockade")
            {
                var blockadeSystem = FindSystem<B2.BlockadeSystem>();
                if (blockadeSystem != null)
                {
                    var blockadeType = ResolveBlockadeType(ai);
                    blockadeSystem.ActivateBlockade(blockadeType);
                    Events.ActionLogAdded("G", $"{ai.FactionId} activates blockade: {blockadeType}", FeedbackSeverity.Warning);
                }
            }
            else if (decision.ActionId == "FocusOnEconomy")
            {
                var tradeSystem = FindSystem<B3.TradeNetworkSystem>();
                if (tradeSystem != null)
                {
                    tradeSystem.RecalculateAllRoutes();
                }

                var goldLeaf = State.GetResource(GameIds.Resource.GoldLeaf);
                if (goldLeaf != null)
                {
                    int oldAmount = goldLeaf.Amount;
                    int bonus = Mathf.Max(5, Mathf.RoundToInt(10f * ai.DiplomaticLevel));
                    goldLeaf.Amount = Mathf.Clamp(goldLeaf.Amount + bonus, 0, goldLeaf.MaxCapacity);
                    Events.ResourceChanged(GameIds.Resource.GoldLeaf, oldAmount, goldLeaf.Amount);
                }

                Events.ActionLogAdded("G", $"{ai.FactionId} focuses on economy and recalculates trade network", FeedbackSeverity.Info);
            }
            else if (decision.ActionId == "SecureTradeNode")
            {
                var d1System = FindSystem<D1.MilitaryOperationsSystem>();
                var tradeSystem = FindSystem<B3.TradeNetworkSystem>();
                if (d1System != null)
                {
                    bool executed = d1System.ExecuteActionForAI(MilitaryActionType.ChokepointThreat, targetNodeId);
                    Events.ActionLogAdded("G", $"{ai.FactionId} secure-trade attempt on {targetNodeId}: {(executed ? "executed" : "failed")}", executed ? FeedbackSeverity.Warning : FeedbackSeverity.Info);
                }

                if (tradeSystem != null)
                {
                    tradeSystem.RecalculateAllRoutes();
                }
            }

            Debug.Log($"[FactionAI] {ai.FactionId} executed economic action: {decision.ActionId}");
        }

        private void BuildNodeAdjacency()
        {
            _nodeAdjacency.Clear();
            _nodeRegionLookup.Clear();

            if (State?.Map?.Regions == null)
                return;

            foreach (var region in State.Map.Regions)
            {
                if (region?.Nodes == null)
                    continue;

                for (int i = 0; i < region.Nodes.Length; i++)
                {
                    var node = region.Nodes[i];
                    if (node == null || string.IsNullOrEmpty(node.NodeId))
                        continue;

                    string canonicalNodeId = GameIds.ResolveNodeId(node.NodeId);
                    EnsureNodeKey(canonicalNodeId);
                    _nodeRegionLookup[canonicalNodeId] = region.RegionId;
                }

                for (int i = 0; i < region.Nodes.Length; i++)
                {
                    var nodeA = region.Nodes[i];
                    if (nodeA == null || string.IsNullOrEmpty(nodeA.NodeId))
                        continue;

                    for (int j = i + 1; j < region.Nodes.Length; j++)
                    {
                        var nodeB = region.Nodes[j];
                        if (nodeB == null || string.IsNullOrEmpty(nodeB.NodeId))
                            continue;

                        AddBidirectionalEdge(GameIds.ResolveNodeId(nodeA.NodeId), GameIds.ResolveNodeId(nodeB.NodeId));
                    }
                }
            }
        }

        private void EnsureNodeKey(string nodeId)
        {
            nodeId = GameIds.ResolveNodeId(nodeId);
            if (!_nodeAdjacency.ContainsKey(nodeId))
            {
                _nodeAdjacency[nodeId] = new HashSet<string>();
            }
        }

        private void AddBidirectionalEdge(string nodeA, string nodeB)
        {
            nodeA = GameIds.ResolveNodeId(nodeA);
            nodeB = GameIds.ResolveNodeId(nodeB);
            EnsureNodeKey(nodeA);
            EnsureNodeKey(nodeB);
            _nodeAdjacency[nodeA].Add(nodeB);
            _nodeAdjacency[nodeB].Add(nodeA);
        }

        private MilitaryActionType ResolveMilitaryActionType(AIDecision decision, FactionAI ai)
        {
            if (decision.ActionId == "SecureTradeNode")
                return MilitaryActionType.ChokepointThreat;

            if (ai.Personality == AIPersonality.Defensive)
                return MilitaryActionType.AsymmetricDefense;

            if (ai.Personality == AIPersonality.Aggressive && decision.Priority > 0.75f)
                return MilitaryActionType.SpecialForces;

            return MilitaryActionType.Proxy;
        }

        private ProtocolType ResolveProtocolType(FactionAI ai, AIDecision decision)
        {
            if (ai.Personality == AIPersonality.Diplomatic)
                return ProtocolType.TradeAgreement;

            if (ai.Personality == AIPersonality.Defensive)
                return ProtocolType.NonAggression;

            return ProtocolType.Neutrality;
        }

        private B1.BlockadeType ResolveBlockadeType(FactionAI ai)
        {
            if (ai.Personality == AIPersonality.Aggressive)
                return B1.BlockadeType.NavalBlockade;

            if (ai.Personality == AIPersonality.Defensive)
                return B1.BlockadeType.FinancialBlockade;

            return B1.BlockadeType.SecondaryBlockade;
        }

        private T FindSystem<T>() where T : GameSystem
        {
            if (GameManager.Instance != null)
            {
                foreach (var system in GameManager.Instance.Systems)
                {
                    if (system is T typedSystem)
                        return typedSystem;
                }
            }

            var local = GetComponentInParent<T>();
            if (local != null && local != this)
                return local;

            var allSystems = FindObjectsOfType<GameSystem>(true);
            foreach (var system in allSystems)
            {
                if (system is T typedSystem)
                    return typedSystem;
            }

            return null;
        }

        public FactionAI GetAI(string factionId)
        {
            factionId = GameIds.ResolveFactionId(factionId);
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
            factionId = GameIds.ResolveFactionId(factionId);
            var ai = GetAI(factionId);
            if (ai == null) return new List<AIDecision>();

            return GenerateDecisions(ai);
        }
    }
}
