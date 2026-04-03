using System;
using System.Linq;
using UnityEngine;
using EventideAge.Core;

namespace EventideAge.Tests
{
    public static class StandaloneTest
    {
        private static int passed = 0;
        private static int failed = 0;
        
        public static void RunAll()
        {
            Debug.Log("=== Standalone Unit Tests ===");
            passed = 0;
            failed = 0;
            
            TestGameConfig();
            TestGameState();
            TestResourceSystem();
            TestTurnLoopLogic();
            TestPhaseEngineLogic();
            TestD2MilitaryPoliticalLinkage();
            TestD3ProxyCivilAffairs();
            TestD4NuclearDeterrence();
            TestD6MilitaryTech();
            TestJVictoryDefeat();
            
            Debug.Log($"=== Results: {passed} passed, {failed} failed ===");
        }
        
        private static void TestGameConfig()
        {
            Debug.Log("\n--- Testing GameConfig ---");
            
            var config = ScriptableObject.CreateInstance<GameConfig>();
            config.PhaseConfigs = new PhaseConfig[6];
            config.PhaseConfigs[0] = new PhaseConfig { PhaseName = "外交", BaseActionPoints = 2 };
            config.PhaseConfigs[1] = new PhaseConfig { PhaseName = "战略", BaseActionPoints = 2 };
            config.PhaseConfigs[2] = new PhaseConfig { PhaseName = "作战", BaseActionPoints = 2 };
            config.PhaseConfigs[3] = new PhaseConfig { PhaseName = "后勤", BaseActionPoints = 1 };
            config.PhaseConfigs[4] = new PhaseConfig { PhaseName = "情报", BaseActionPoints = 1 };
            config.PhaseConfigs[5] = new PhaseConfig { PhaseName = "AI响应", BaseActionPoints = 0 };
            
            Assert("6 phases configured", config.PhaseConfigs.Length == 6);
            Assert("Phase 0 name is 外交", config.PhaseConfigs[0].PhaseName == "外交");
            Assert("Phase 0 AP is 2", config.PhaseConfigs[0].BaseActionPoints == 2);
            Assert("Phase 5 AP is 0", config.PhaseConfigs[5].BaseActionPoints == 0);
            
            int totalAP = 0;
            for (int i = 0; i < 6; i++)
            {
                totalAP += config.PhaseConfigs[i].BaseActionPoints;
            }
            totalAP += 2;
            Assert("Total AP including universal is 11", totalAP == 11);
            
            UnityEngine.Object.DestroyImmediate(config);
        }
        
        private static void TestGameState()
        {
            Debug.Log("\n--- Testing GameState ---");
            
            var config = ScriptableObject.CreateInstance<GameConfig>();
            config.PhaseConfigs = new PhaseConfig[1];
            config.PhaseConfigs[0] = new PhaseConfig { PhaseName = "Test", BaseActionPoints = 2 };
            config.FactionConfigs = new FactionConfig[1];
            config.FactionConfigs[0] = new FactionConfig { FactionId = "TestFaction", FactionName = "Test", IsPlayerControlled = true, InitialControlledPoints = 100, InitialRelationship = 100 };
            config.ResourceConfigs = new ResourceConfig[1];
            config.ResourceConfigs[0] = new ResourceConfig { ResourceId = "TestRes", ResourceName = "Test", InitialAmount = 50, MaxCapacity = 100, ResourceType = ResourceType.Consumable };
            config.RegionConfigs = new RegionConfig[1];
            config.RegionConfigs[0] = new RegionConfig { RegionId = "R1", RegionName = "Region1", NodeConfigs = new NodeConfig[1] };
            config.RegionConfigs[0].NodeConfigs[0] = new NodeConfig { NodeId = "N1", NodeName = "Node1", NodeType = NodeType.Chokepoint, DefenseBonus = 10, InitialController = "TestFaction", InitialControlPoints = 50, MaxControlPoints = 100 };
            
            var state = ScriptableObject.CreateInstance<GameState>();
            state.Config = config;
            state.Initialize();
            
            Assert("Initial turn is 1", state.CurrentTurn == 1);
            Assert("Initial phase is 0", state.CurrentPhaseIndex == 0);
            Assert("Initial AP is 11", state.ActionPointsRemaining == 11);
            Assert("Factions array populated", state.Factions != null && state.Factions.Length == 1);
            Assert("Resources array populated", state.Resources != null && state.Resources.Length == 1);
            Assert("Map regions populated", state.Map != null && state.Map.Regions != null && state.Map.Regions.Length == 1);
            
            var faction = state.GetFaction("TestFaction");
            Assert("GetFaction works", faction != null);
            Assert("Faction controlled points is 100", faction?.ControlledPoints == 100);
            
            var resource = state.GetResource("TestRes");
            Assert("GetResource works", resource != null);
            Assert("Resource initial amount is 50", resource?.Amount == 50);
            
            var node = state.GetNode("N1");
            Assert("GetNode works", node != null);
            Assert("Node is Chokepoint", node?.NodeType == NodeType.Chokepoint);
            
            UnityEngine.Object.DestroyImmediate(state);
            UnityEngine.Object.DestroyImmediate(config);
        }
        
        private static void TestResourceSystem()
        {
            Debug.Log("\n--- Testing Resource System Logic ---");
            
            var config = ScriptableObject.CreateInstance<GameConfig>();
            config.ResourceConfigs = new ResourceConfig[3];
            config.ResourceConfigs[0] = new ResourceConfig { ResourceId = "Arms", ResourceName = "战械", InitialAmount = 50, MaxCapacity = 100, ResourceType = ResourceType.Consumable };
            config.ResourceConfigs[1] = new ResourceConfig { ResourceId = "FireOil", ResourceName = "火油", InitialAmount = 80, MaxCapacity = 200, ResourceType = ResourceType.Accumulative };
            config.ResourceConfigs[2] = new ResourceConfig { ResourceId = "Social", ResourceName = "社稷值", InitialAmount = 100, MaxCapacity = 100, ResourceType = ResourceType.Ratio };
            
            var state = ScriptableObject.CreateInstance<GameState>();
            state.Config = config;
            state.Initialize();
            
            int armsBefore = state.GetResource("Arms")?.Amount ?? 0;
            state.GetResource("Arms").Amount = Mathf.Clamp(state.GetResource("Arms").Amount + 10, 0, state.GetResource("Arms").MaxCapacity);
            Assert("Arms can be increased", state.GetResource("Arms").Amount == armsBefore + 10);
            
            state.GetResource("Arms").Amount = Mathf.Clamp(state.GetResource("Arms").Amount - 20, 0, state.GetResource("Arms").MaxCapacity);
            Assert("Arms can be decreased", state.GetResource("Arms").Amount == armsBefore - 10);
            
            state.GetResource("Arms").Amount = Mathf.Clamp(state.GetResource("Arms").Amount - 9999, 0, state.GetResource("Arms").MaxCapacity);
            Assert("Arms clamped to 0 at minimum", state.GetResource("Arms").Amount == 0);
            
            state.GetResource("FireOil").Amount = state.GetResource("FireOil").MaxCapacity + 100;
            state.GetResource("FireOil").Amount = Mathf.Clamp(state.GetResource("FireOil").Amount, 0, state.GetResource("FireOil").MaxCapacity);
            Assert("FireOil clamped to max capacity", state.GetResource("FireOil").Amount == 200);
            
            UnityEngine.Object.DestroyImmediate(state);
            UnityEngine.Object.DestroyImmediate(config);
        }
        
        private static void TestTurnLoopLogic()
        {
            Debug.Log("\n--- Testing Turn Loop Logic ---");
            
            var config = ScriptableObject.CreateInstance<GameConfig>();
            config.PhaseConfigs = new PhaseConfig[6];
            for (int i = 0; i < 6; i++)
            {
                config.PhaseConfigs[i] = new PhaseConfig { PhaseName = $"Phase{i}", BaseActionPoints = 2 };
            }
            
            var state = ScriptableObject.CreateInstance<GameState>();
            state.Config = config;
            state.Initialize();
            
            Assert("Initial AP is 11", state.ActionPointsRemaining == 11);
            
            state.ActionPointsRemaining -= 3;
            Assert("AP deducted correctly", state.ActionPointsRemaining == 8);
            
            bool canSpend3 = state.ActionPointsRemaining >= 3;
            Assert("Can spend 3 AP when have 8", canSpend3);
            
            bool cannotSpend100 = state.ActionPointsRemaining < 100;
            Assert("Cannot spend 100 AP when have 8", cannotSpend100);
            
            UnityEngine.Object.DestroyImmediate(state);
            UnityEngine.Object.DestroyImmediate(config);
        }
        
        private static void TestPhaseEngineLogic()
        {
            Debug.Log("\n--- Testing Phase Engine Logic ---");
            
            int currentPhase = 0;
            int totalPhases = 6;
            
            currentPhase = (currentPhase + 1) % totalPhases;
            Assert("Phase 0 -> 1", currentPhase == 1);
            
            currentPhase = 5;
            currentPhase = (currentPhase + 1) % totalPhases;
            Assert("Phase 5 -> 0 (wrap)", currentPhase == 0);
            
            currentPhase = (currentPhase + 1) % totalPhases;
            Assert("Phase 0 -> 1 again", currentPhase == 1);
            
            int[] phaseAP = { 2, 2, 2, 1, 1, 0 };
            int totalPhaseAP = 0;
            for (int i = 0; i < phaseAP.Length; i++)
            {
                totalPhaseAP += phaseAP[i];
            }
            Assert("Sum of phase AP is 8", totalPhaseAP == 8);
            
            int universalAP = 2;
            int totalAP = totalPhaseAP + universalAP;
            Assert("Total with universal is 10", totalAP == 10);
            
            int phase0UniversalUsed = 0;
            int neededFromUniversal = Mathf.Max(0, 3 - phaseAP[0]);
            Assert("Need 1 from universal for cost 3 in phase 0", neededFromUniversal == 1);
            
            int universalAvailable = universalAP - phase0UniversalUsed;
            bool canCover = universalAvailable >= neededFromUniversal;
            Assert("Universal can cover the shortfall", canCover);
        }
        
        private static void TestD2MilitaryPoliticalLinkage()
        {
            Debug.Log("\n--- Testing D2 Military-Political Linkage ---");
            
            var go = new GameObject("D2Test");
            var state = CreateMinimalState();
            var events = ScriptableObject.CreateInstance<GameEvents>();
            
            var d2 = go.AddComponent<EventideAge.Systems.D2.MilitaryPoliticalLinkageSystem>();
            d2.Initialize(state, events);
            
            Assert("D2 initialized", d2 != null);
            
            int initialSocial = state.GetResource("SocialValue")?.Amount ?? 0;
            int cost = 5;
            state.GetResource("SocialValue").Amount = initialSocial + cost;
            
            d2.ApplyMilitaryActionPoliticalCost(EventideAge.Systems.D1.MilitaryActionType.ChokepointThreat);
            
            int afterSocial = state.GetResource("SocialValue").Amount;
            Assert("Social cost for ChokepointThreat is 5", initialSocial + cost - afterSocial == 5);
            
            int smallTurns = d2.GetDigestionTurnsForNode("SmallNode");
            int mediumTurns = d2.GetDigestionTurnsForNode("MediumNode");
            int largeTurns = d2.GetDigestionTurnsForNode("ChokepointNode");
            
            Assert("Small node digestion is 3 turns", smallTurns == 3);
            Assert("Medium node digestion is 5 turns", mediumTurns == 5);
            Assert("Chokepoint node digestion is 8 turns", largeTurns == 8);
            
            d2.StartNodeDigestion("TestNode");
            Assert("Node starts under digestion", d2.IsNodeUnderDigestion("TestNode"));
            
            UnityEngine.Object.DestroyImmediate(d2);
            UnityEngine.Object.DestroyImmediate(go);
            CleanupTestState(state, events);
        }
        
        private static void TestD3ProxyCivilAffairs()
        {
            Debug.Log("\n--- Testing D3 Proxy Civil Affairs ---");
            
            var go = new GameObject("D3Test");
            var state = CreateMinimalState();
            var events = ScriptableObject.CreateInstance<GameEvents>();
            
            var d3 = go.AddComponent<EventideAge.Systems.D3.ProxyCivilAffairsSystem>();
            d3.Initialize(state, events);
            
            Assert("D3 initialized", d3 != null);
            
            d3.RegisterProxyRegion("ProxyNode", "ProxyFaction");
            
            var region = d3.GetProxyRegion("ProxyNode");
            Assert("Proxy region registered", region != null);
            Assert("Initial stability is 50", region.Stability == 50);
            Assert("Initial public support is 50", region.PublicSupport == 50);
            
            d3.SetGovernanceLevel("ProxyNode", EventideAge.Systems.D3.GovernanceLevel.Enhanced);
            region = d3.GetProxyRegion("ProxyNode");
            Assert("Governance level set to Enhanced", region.GovernanceLevel == EventideAge.Systems.D3.GovernanceLevel.Enhanced);
            
            d3.UpdatePublicSupport("ProxyNode", -20);
            region = d3.GetProxyRegion("ProxyNode");
            Assert("Public support decreased by 20", region.PublicSupport == 30);
            
            var status = d3.GetNodeStatus("ProxyNode");
            Assert("Status is Normal after support drop", status == EventideAge.Systems.D3.ProxyCivilStatus.Normal);
            
            float mod = d3.GetOutputModifier("ProxyNode");
            Assert("Output modifier is 0.8 for Normal status", Mathf.Approximately(mod, 0.8f));
            
            UnityEngine.Object.DestroyImmediate(d3);
            UnityEngine.Object.DestroyImmediate(go);
            CleanupTestState(state, events);
        }
        
        private static void TestD4NuclearDeterrence()
        {
            Debug.Log("\n--- Testing D4 Nuclear Deterrence ---");
            
            var go = new GameObject("D4Test");
            var state = CreateMinimalState();
            var events = ScriptableObject.CreateInstance<GameEvents>();
            
            var d4 = go.AddComponent<EventideAge.Systems.D4.NuclearDeterrenceSystem>();
            d4.Initialize(state, events);
            
            Assert("D4 initialized", d4 != null);
            
            var initialState = d4.GetState();
            Assert("Initial warhead count is 0", initialState.WarheadCount == 0);
            Assert("Initial capability is None", initialState.CapabilityLevel == EventideAge.Systems.D4.NuclearCapabilityLevel.None);
            
            d4.SetWarheadCount(5);
            var state5 = d4.GetState();
            Assert("Warhead count 5 -> Limited capability", state5.CapabilityLevel == EventideAge.Systems.D4.NuclearCapabilityLevel.Limited);
            
            d4.SetWarheadCount(15);
            var state15 = d4.GetState();
            Assert("Warhead count 15 -> Credible capability", state15.CapabilityLevel == EventideAge.Systems.D4.NuclearCapabilityLevel.Credible);
            
            d4.SetWarheadCount(50);
            var state50 = d4.GetState();
            Assert("Warhead count 50 -> Enhanced capability", state50.CapabilityLevel == EventideAge.Systems.D4.NuclearCapabilityLevel.Enhanced);
            
            d4.SetWarheadCount(100);
            var state100 = d4.GetState();
            Assert("Warhead count 100 -> Absolute capability", state100.CapabilityLevel == EventideAge.Systems.D4.NuclearCapabilityLevel.Absolute);
            
            Assert("Cannot display with None capability", !d4.CanDisplayDeterrence());
            
            d4.SetWarheadCount(5);
            Assert("Can display with Limited capability", d4.CanDisplayDeterrence());
            
            UnityEngine.Object.DestroyImmediate(d4);
            UnityEngine.Object.DestroyImmediate(go);
            CleanupTestState(state, events);
        }
        
        private static void TestD6MilitaryTech()
        {
            Debug.Log("\n--- Testing D6 Military Tech ---");
            
            var go = new GameObject("D6Test");
            var state = CreateMinimalState();
            var events = ScriptableObject.CreateInstance<GameEvents>();
            
            state.GetResource("GoldLeaf").Amount = 200;
            
            var d6 = go.AddComponent<EventideAge.Systems.D6.MilitaryTechSystem>();
            d6.Initialize(state, events);
            
            Assert("D6 initialized", d6 != null);
            
            var available = d6.GetAvailableTechs();
            Assert("Available techs count is 5 (base tier)", available.Length == 5);
            
            bool canStart1 = d6.StartResearch("DefensiveEnhancementI");
            Assert("Can start DefensiveEnhancementI research", canStart1);
            
            var research = d6.GetCurrentResearch();
            Assert("Research is in progress", research != null);
            Assert("Research tech ID is correct", research.TechId == "DefensiveEnhancementI");
            
            bool cannotStartSecond = d6.StartResearch("DefensiveEnhancementII");
            Assert("Cannot start second research while one is active", !cannotStartSecond);
            
            Assert("DefensiveEnhancementII has prereq", !d6.CanStartResearch("DefensiveEnhancementII"));
            
            for (int i = 0; i < 3; i++)
            {
                d6.ProcessResearchTurn();
            }
            
            Assert("Tech completed after 3 turns", d6.HasCompleted("DefensiveEnhancementI"));
            
            var completed = d6.GetCompletedTechs();
            Assert("Completed techs list contains DefensiveEnhancementI", Array.Exists(completed, t => t == "DefensiveEnhancementI"));
            
            UnityEngine.Object.DestroyImmediate(d6);
            UnityEngine.Object.DestroyImmediate(go);
            CleanupTestState(state, events);
        }
        
        private static void TestJVictoryDefeat()
        {
            Debug.Log("\n--- Testing J Victory/Defeat ---");
            
            var go = new GameObject("JTest");
            var state = CreateMinimalState();
            var events = ScriptableObject.CreateInstance<GameEvents>();
            
            var j = go.AddComponent<EventideAge.Systems.J.VictoryDefeatSystem>();
            j.Initialize(state, events);
            
            Assert("J initialized", j != null);
            Assert("Game has not ended initially", !j.IsGameEnded());
            
            var paths = j.GetAllVictoryPaths();
            Assert("4 victory paths exist", paths.Length == 4);
            
            var closest = j.GetClosestPath();
            Assert("Closest path exists", closest != null);
            
            state.GetResource("SocialValue").Amount = 25;
            var risks = j.GetCurrentDefeatRisks();
            Assert("Low AshWill triggers defeat risk", risks.Length > 0);
            
            j.Reset();
            Assert("Reset clears game ended state", !j.IsGameEnded());
            
            UnityEngine.Object.DestroyImmediate(j);
            UnityEngine.Object.DestroyImmediate(go);
            CleanupTestState(state, events);
        }
        
        private static GameState CreateMinimalState()
        {
            var config = ScriptableObject.CreateInstance<GameConfig>();
            config.PhaseConfigs = new PhaseConfig[1];
            config.PhaseConfigs[0] = new PhaseConfig { PhaseName = "Test", BaseActionPoints = 2 };
            
            config.FactionConfigs = new FactionConfig[2];
            config.FactionConfigs[0] = new FactionConfig { FactionId = "Vashid", FactionName = "Vashid", IsPlayerControlled = true, InitialControlledPoints = 100, InitialRelationship = 100, InitialSatisfaction = 100 };
            config.FactionConfigs[1] = new FactionConfig { FactionId = "GoldLeader", FactionName = "GoldLeader", IsPlayerControlled = false, InitialControlledPoints = 50, InitialRelationship = -50, InitialSatisfaction = 50 };
            
            config.ResourceConfigs = new ResourceConfig[8];
            config.ResourceConfigs[0] = new ResourceConfig { ResourceId = "SocialValue", ResourceName = "SocialValue", InitialAmount = 50, MaxCapacity = 100, ResourceType = ResourceType.Ratio };
            config.ResourceConfigs[1] = new ResourceConfig { ResourceId = "GoldLeaf", ResourceName = "GoldLeaf", InitialAmount = 100, MaxCapacity = 1000, ResourceType = ResourceType.Consumable };
            config.ResourceConfigs[2] = new ResourceConfig { ResourceId = "Arms", ResourceName = "Arms", InitialAmount = 50, MaxCapacity = 200, ResourceType = ResourceType.Consumable };
            config.ResourceConfigs[3] = new ResourceConfig { ResourceId = "Energy", ResourceName = "Energy", InitialAmount = 100, MaxCapacity = 500, ResourceType = ResourceType.Accumulative };
            config.ResourceConfigs[4] = new ResourceConfig { ResourceId = "TradeToken", ResourceName = "TradeToken", InitialAmount = 30, MaxCapacity = 200, ResourceType = ResourceType.Consumable };
            config.ResourceConfigs[5] = new ResourceConfig { ResourceId = "AshWill", ResourceName = "AshWill", InitialAmount = 50, MaxCapacity = 100, ResourceType = ResourceType.Ratio };
            config.ResourceConfigs[6] = new ResourceConfig { ResourceId = "NorthCoins", ResourceName = "NorthCoins", InitialAmount = 20, MaxCapacity = 200, ResourceType = ResourceType.Consumable };
            config.ResourceConfigs[7] = new ResourceConfig { ResourceId = "GoldLeafReserve", ResourceName = "GoldLeafReserve", InitialAmount = 100, MaxCapacity = 1000, ResourceType = ResourceType.Accumulative };
            
            config.RegionConfigs = new RegionConfig[3];
            config.RegionConfigs[0] = new RegionConfig { RegionId = "Tigris", RegionName = "Tigris Region", NodeConfigs = new NodeConfig[2] };
            config.RegionConfigs[0].NodeConfigs[0] = new NodeConfig { NodeId = "Tigris", NodeName = "Tigris", NodeType = NodeType.Chokepoint, DefenseBonus = 10, InitialController = "Vashid", InitialControlPoints = 50, MaxControlPoints = 100 };
            config.RegionConfigs[0].NodeConfigs[1] = new NodeConfig { NodeId = "SmallNode", NodeName = "SmallNode", NodeType = NodeType.ResourceNode, DefenseBonus = 5, InitialController = "Vashid", InitialControlPoints = 30, MaxControlPoints = 60 };
            
            config.RegionConfigs[1] = new RegionConfig { RegionId = "Levant", RegionName = "Levant Region", NodeConfigs = new NodeConfig[2] };
            config.RegionConfigs[1].NodeConfigs[0] = new NodeConfig { NodeId = "Damascus", NodeName = "Damascus", NodeType = NodeType.City, DefenseBonus = 15, InitialController = "GoldLeader", InitialControlPoints = 70, MaxControlPoints = 100 };
            config.RegionConfigs[1].NodeConfigs[1] = new NodeConfig { NodeId = "ChokepointNode", NodeName = "ChokepointNode", NodeType = NodeType.Chokepoint, DefenseBonus = 20, InitialController = "GoldLeader", InitialControlPoints = 50, MaxControlPoints = 100 };
            
            config.RegionConfigs[2] = new RegionConfig { RegionId = "Mediterranean", RegionName = "Mediterranean Region", NodeConfigs = new NodeConfig[1] };
            config.RegionConfigs[2].NodeConfigs[0] = new NodeConfig { NodeId = "Beirut", NodeName = "Beirut", NodeType = NodeType.Port, DefenseBonus = 12, InitialController = "GoldLeader", InitialControlPoints = 60, MaxControlPoints = 100 };
            
            var state = ScriptableObject.CreateInstance<GameState>();
            state.Config = config;
            state.Initialize();
            
            return state;
        }
        
        private static void CleanupTestState(GameState state, GameEvents events)
        {
            if (state != null && state.Config != null)
            {
                UnityEngine.Object.DestroyImmediate(state.Config);
            }
            UnityEngine.Object.DestroyImmediate(state);
            UnityEngine.Object.DestroyImmediate(events);
        }
        
        private static void Assert(string name, bool condition)
        {
            if (condition)
            {
                Debug.Log($"[PASS] {name}");
                passed++;
            }
            else
            {
                Debug.LogError($"[FAIL] {name}");
                failed++;
            }
        }
    }
}
