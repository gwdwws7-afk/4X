using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
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
            TestA4SaveLoadGuardrail();
            TestD2MilitaryPoliticalLinkage();
            TestD3ProxyCivilAffairs();
            TestD4NuclearDeterrence();
            TestD6MilitaryTech();
            TestJVictoryDefeat();
            TestD1NodeControlEventSemantics();
            TestD5NodeControlEventSemantics();
            TestGAdjacencyGuardrail();
            TestGAIDecisionExecutionGuardrail();
            TestI1EventTriggerGuardrail();
            TestB5SettlementExpenseGuardrail();
            TestB4ExchangeRateReciprocalGuardrail();
            TestB5OverdraftThresholdGuardrail();
            TestB1CurrencyCanonicalGuardrail();
            TestB5SettlementCanonicalResourceIdGuardrail();
            TestH2H3RuntimeGuardrail();
            TestH2DefaultRouteConnectivityGuardrail();
            TestH3SecondaryDepthVisionGuardrail();
            TestC1DiplomaticRelationsGuardrail();
            TestC2DiplomaticProtocolsCanonicalGuardrail();
            TestC3IdeologyGuardrail();
            TestC4AllianceGuardrail();
            TestC5InternationalOrgsGuardrail();
            TestEInternalPoliticsGuardrail();
            TestF1IntelligenceTerrainVisionGuardrail();
            TestH1MapAdjacencyGuardrail();
            TestKUiTimeSyncGuardrail();
            TestKUiTimeFallbackGuardrail();
            TestUiCanonicalDisplayGuardrail();
            TestUiPanelAggregationAndOrderingGuardrail();
            TestUiPriorityAndDedupGuardrail();
            TestUiDigestSummaryGuardrail();
            TestUiTurnSummaryGuardrail();
            TestTimeoutOwnershipGuardrail();
            TestTimeoutSingleFireGuardrail();
            TestTimeoutVictoryPriorityGuardrail();
            TestTimeoutConfigSourceOfTruthGuardrail();
            TestTimeoutA5JEventPathGuardrail();
            TestCanonicalIdGuardrail();
            TestResourceCanonicalConvergenceGuardrail();
            
            Debug.Log($"=== Results: {passed} passed, {failed} failed ===");
        }
        
        private static void TestGameConfig()
        {
            Debug.Log("\n--- Testing GameConfig ---");
            
            var config = ScriptableObject.CreateInstance<GameConfig>();
            config.PhaseConfigs = new PhaseConfig[6];
            config.PhaseConfigs[0] = new PhaseConfig { PhaseName = "澶栦氦", BaseActionPoints = 2 };
            config.PhaseConfigs[1] = new PhaseConfig { PhaseName = "鎴樼暐", BaseActionPoints = 2 };
            config.PhaseConfigs[2] = new PhaseConfig { PhaseName = "浣滄垬", BaseActionPoints = 3 };
            config.PhaseConfigs[3] = new PhaseConfig { PhaseName = "鍚庡嫟", BaseActionPoints = 1 };
            config.PhaseConfigs[4] = new PhaseConfig { PhaseName = "鎯呮姤", BaseActionPoints = 1 };
            config.PhaseConfigs[5] = new PhaseConfig { PhaseName = "AI鍝嶅簲", BaseActionPoints = 0 };
            
            Assert("6 phases configured", config.PhaseConfigs.Length == 6);
            Assert("Phase 0 name is 澶栦氦", config.PhaseConfigs[0].PhaseName == "澶栦氦");
            Assert("Phase 0 AP is 2", config.PhaseConfigs[0].BaseActionPoints == 2);
            Assert("Phase 5 AP is 0", config.PhaseConfigs[5].BaseActionPoints == 0);
            
            int totalAP = 0;
            for (int i = 0; i < 6; i++)
            {
                totalAP += config.PhaseConfigs[i].BaseActionPoints;
            }
            totalAP += GameConfig.kUniversalActionPoints;
            Assert("Total AP including universal is 11", totalAP == GameConfig.kTotalActionPoints);
            
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
            Assert("Initial phase AP is 2", state.CurrentPhaseActionPointsRemaining == 2);
            Assert("Initial universal AP is 2", state.UniversalActionPointsRemaining == 2);
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
            config.PhaseConfigs = new PhaseConfig[1];
            config.PhaseConfigs[0] = new PhaseConfig { PhaseName = "Test", BaseActionPoints = 2 };
            config.FactionConfigs = new FactionConfig[1];
            config.FactionConfigs[0] = new FactionConfig { FactionId = "TestFaction", FactionName = "Test", IsPlayerControlled = true, InitialControlledPoints = 100, InitialRelationship = 0 };
            config.ResourceConfigs = new ResourceConfig[3];
            config.ResourceConfigs[0] = new ResourceConfig { ResourceId = "Arms", ResourceName = "鎴樻", InitialAmount = 50, MaxCapacity = 100, ResourceType = ResourceType.Consumable };
            config.ResourceConfigs[1] = new ResourceConfig { ResourceId = "FireOil", ResourceName = "鐏补", InitialAmount = 80, MaxCapacity = 200, ResourceType = ResourceType.Accumulative };
            config.ResourceConfigs[2] = new ResourceConfig { ResourceId = "Social", ResourceName = "Social", InitialAmount = 100, MaxCapacity = 100, ResourceType = ResourceType.Ratio };
            config.RegionConfigs = new RegionConfig[1];
            config.RegionConfigs[0] = new RegionConfig { RegionId = "R1", RegionName = "Region1", NodeConfigs = new NodeConfig[1] };
            config.RegionConfigs[0].NodeConfigs[0] = new NodeConfig { NodeId = "N1", NodeName = "Node1", NodeType = NodeType.ResourceNode, DefenseBonus = 0, InitialController = "TestFaction", InitialControlPoints = 50, MaxControlPoints = 100 };
            
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
            config.PhaseConfigs[0] = new PhaseConfig { PhaseName = "Phase0", BaseActionPoints = 2 };
            config.PhaseConfigs[1] = new PhaseConfig { PhaseName = "Phase1", BaseActionPoints = 2 };
            config.PhaseConfigs[2] = new PhaseConfig { PhaseName = "Phase2", BaseActionPoints = 3 };
            config.PhaseConfigs[3] = new PhaseConfig { PhaseName = "Phase3", BaseActionPoints = 1 };
            config.PhaseConfigs[4] = new PhaseConfig { PhaseName = "Phase4", BaseActionPoints = 1 };
            config.PhaseConfigs[5] = new PhaseConfig { PhaseName = "Phase5", BaseActionPoints = 0 };
            config.FactionConfigs = new FactionConfig[1];
            config.FactionConfigs[0] = new FactionConfig { FactionId = "TestFaction", FactionName = "Test", IsPlayerControlled = true, InitialControlledPoints = 100, InitialRelationship = 0 };
            config.ResourceConfigs = new ResourceConfig[1];
            config.ResourceConfigs[0] = new ResourceConfig { ResourceId = "Arms", ResourceName = "Arms", InitialAmount = 50, MaxCapacity = 100, ResourceType = ResourceType.Consumable };
            config.RegionConfigs = new RegionConfig[1];
            config.RegionConfigs[0] = new RegionConfig { RegionId = "R1", RegionName = "Region1", NodeConfigs = new NodeConfig[1] };
            config.RegionConfigs[0].NodeConfigs[0] = new NodeConfig { NodeId = "N1", NodeName = "Node1", NodeType = NodeType.ResourceNode, DefenseBonus = 0, InitialController = "TestFaction", InitialControlPoints = 50, MaxControlPoints = 100 };
            
            var state = ScriptableObject.CreateInstance<GameState>();
            state.Config = config;
            state.Initialize();
            
            Assert("Initial AP is 11", state.ActionPointsRemaining == 11);
            Assert("Initial phase AP is 2", state.CurrentPhaseActionPointsRemaining == 2);
            Assert("Initial universal AP is 2", state.UniversalActionPointsRemaining == 2);

            bool spend3InPhase0 = state.TrySpendActionPoints(3);
            Assert("Can spend 3 AP in phase 0 (2+1)", spend3InPhase0);
            Assert("Total AP deducted to 8", state.ActionPointsRemaining == 8);
            Assert("Phase AP consumed first", state.CurrentPhaseActionPointsRemaining == 0);
            Assert("Universal AP consumed for shortfall", state.UniversalActionPointsRemaining == 1);

            bool cannotOverspendImmediateInPhase0 = !state.TrySpendActionPoints(2);
            Assert("Cannot spend 2 more AP in phase 0 when only universal 1 remains", cannotOverspendImmediateInPhase0);

            state.ExpireCurrentPhaseActionPoints();
            state.CurrentPhaseIndex = 1;
            state.PreparePhaseActionPoints(1);
            Assert("Phase 1 grants fresh phase AP", state.CurrentPhaseActionPointsRemaining == 2);
            Assert("Universal AP carries across player phases", state.UniversalActionPointsRemaining == 1);
            Assert("Turn AP remains 8 after phase switch", state.ActionPointsRemaining == 8);

            bool spend3InPhase1 = state.TrySpendActionPoints(3);
            Assert("Can spend 3 AP in phase 1 using phase+universal", spend3InPhase1);
            Assert("Universal AP reaches 0", state.UniversalActionPointsRemaining == 0);

            state.ExpireCurrentPhaseActionPoints();
            state.CurrentPhaseIndex = GameConfig.kAiResponsePhaseIndex;
            state.PreparePhaseActionPoints(GameConfig.kAiResponsePhaseIndex);
            Assert("AI phase base AP is 0", state.CurrentPhaseActionPointsRemaining == 0);
            Assert("Cannot spend AP in AI phase", !state.TrySpendActionPoints(1));
            
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
            
            int[] phaseAP = { 2, 2, 3, 1, 1, 0 };
            int totalPhaseAP = 0;
            for (int i = 0; i < phaseAP.Length; i++)
            {
                totalPhaseAP += phaseAP[i];
            }
            Assert("Sum of phase AP is 9", totalPhaseAP == 9);
            
            int universalAP = 2;
            int totalAP = totalPhaseAP + universalAP;
            Assert("Total with universal is 11", totalAP == 11);
            
            int phase0UniversalUsed = 0;
            int neededFromUniversal = Mathf.Max(0, 3 - phaseAP[0]);
            Assert("Need 1 from universal for cost 3 in phase 0", neededFromUniversal == 1);
            
            int universalAvailable = universalAP - phase0UniversalUsed;
            bool canCover = universalAvailable >= neededFromUniversal;
            Assert("Universal can cover the shortfall", canCover);
        }

        private static void TestA4SaveLoadGuardrail()
        {
            Debug.Log("\n--- Testing A4 Save/Load Guardrail ---");

            ResetGameManagerSingleton();

            var managerGo = new GameObject("A4SaveManager");
            var manager = managerGo.AddComponent<GameManager>();
            var systemsRoot = new GameObject("A4Systems");
            systemsRoot.transform.SetParent(managerGo.transform);

            var state = CreateDefaultState();
            var events = ScriptableObject.CreateInstance<GameEvents>();

            var saveSystem = systemsRoot.AddComponent<EventideAge.Systems.A4.SaveSystem>();
            var j = systemsRoot.AddComponent<EventideAge.Systems.J.VictoryDefeatSystem>();
            var d4 = systemsRoot.AddComponent<EventideAge.Systems.D4.NuclearDeterrenceSystem>();

            manager.Systems = new List<GameSystem> { saveSystem, j, d4 };

            j.Initialize(state, events);
            d4.Initialize(state, events);
            saveSystem.Initialize(state, events);

            string saveName = $"a4_guardrail_{Guid.NewGuid():N}";
            var goldLeaf = state.GetResource(GameIds.Resource.GoldLeaf);

            state.CurrentTurn = 7;
            state.CurrentPhaseIndex = 2;
            if (goldLeaf != null) goldLeaf.Amount = 321;
            d4.SetWarheadCount(15);
            j.ForceEndGame("save_guardrail_end");

            bool saveResult = saveSystem.SaveGame(saveName);
            Assert("A4 SaveGame succeeds", saveResult);
            Assert("A4 save file exists after save", saveSystem.SaveExists(saveName));

            var allSaves = saveSystem.GetAllSaves();
            Assert("A4 GetAllSaves contains newly created save", allSaves.Any(name => name == saveName));

            state.CurrentTurn = 1;
            state.CurrentPhaseIndex = 0;
            if (goldLeaf != null) goldLeaf.Amount = 12;
            d4.SetWarheadCount(0);
            j.Reset();

            bool loadResult = saveSystem.LoadGame(saveName);
            Assert("A4 LoadGame succeeds", loadResult);
            Assert("A4 load restores turn number", state.CurrentTurn == 7);
            Assert("A4 load restores phase index", state.CurrentPhaseIndex == 2);
            Assert("A4 load restores canonical GoldLeaf value", goldLeaf != null && goldLeaf.Amount == 321);
            Assert("A4 load restores D4 warhead count", d4.GetState().WarheadCount == 15);
            Assert("A4 load restores J game-ended flag", j.IsGameEnded());
            Assert("A4 load restores J end reason", j.GetEndReason() == "save_guardrail_end");

            bool deleteResult = saveSystem.DeleteSave(saveName);
            Assert("A4 DeleteSave removes created save", deleteResult);
            Assert("A4 save file no longer exists after delete", !saveSystem.SaveExists(saveName));

            UnityEngine.Object.DestroyImmediate(saveSystem);
            UnityEngine.Object.DestroyImmediate(j);
            UnityEngine.Object.DestroyImmediate(d4);
            UnityEngine.Object.DestroyImmediate(managerGo);
            CleanupTestState(state, events);
            ResetGameManagerSingleton();
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
            int mediumTurns = d2.GetDigestionTurnsForNode(GameIds.Node.SyriaZone);
            int mediumAliasTurns = d2.GetDigestionTurnsForNode("Damascus");
            int largeTurns = d2.GetDigestionTurnsForNode("ChokepointNode");
            
            Assert("Resource node digestion is 5 turns", smallTurns == 5);
            Assert("City node digestion is 3 turns", mediumTurns == 3);
            Assert("City node alias resolves to same digestion turns", mediumAliasTurns == mediumTurns);
            Assert("Chokepoint node digestion is 8 turns", largeTurns == 8);
            
            d2.StartNodeDigestion("Damascus");
            Assert("Node starts under digestion", d2.IsNodeUnderDigestion("Damascus"));
            Assert("D2 digestion registry uses canonical node id for aliases", d2.IsNodeUnderDigestion(GameIds.Node.SyriaZone));
            
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
            d3.RegisterProxyRegion("Damascus", "ResistanceAxis");
            
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

            var canonicalAliasRegion = d3.GetProxyRegion(GameIds.Node.SyriaZone);
            Assert("D3 alias node registration resolves to canonical node id", canonicalAliasRegion != null);
            Assert("D3 alias faction registration resolves to canonical faction id", canonicalAliasRegion != null && canonicalAliasRegion.ControllerFactionId == GameIds.Faction.AshConfederacy);
            
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
            
            d4.SetWarheadCount(0);
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
            Assert("Available techs count is 4 (base tier)", available.Length == 4);
            
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
            
            var ashWill = state.GetResource("AshWill");
            Assert("AshWill resource exists", ashWill != null);
            if (ashWill != null)
            {
                ashWill.Amount = 25;
            }
            var risks = j.GetCurrentDefeatRisks();
            Assert("Low AshWill triggers defeat risk", risks.Length > 0);
            
            j.Reset();
            Assert("Reset clears game ended state", !j.IsGameEnded());
            
            UnityEngine.Object.DestroyImmediate(j);
            UnityEngine.Object.DestroyImmediate(go);
            CleanupTestState(state, events);
        }

        private static void TestD1NodeControlEventSemantics()
        {
            Debug.Log("\n--- Testing D1 NodeControlChanged Semantics ---");

            var go = new GameObject("D1NodeEventTest");
            var state = CreateMinimalState();
            var events = ScriptableObject.CreateInstance<GameEvents>();

            var d1 = go.AddComponent<EventideAge.Systems.D1.MilitaryOperationsSystem>();
            d1.Initialize(state, events);
            d1.ProxySuccessRate = 1f;

            var socialValue = state.GetResource("SocialValue");
            if (socialValue != null) socialValue.Amount = 100;
            var ashWill = state.GetResource("AshWill");
            if (ashWill != null) ashWill.Amount = 100;

            string capturedNode = null;
            string capturedOld = null;
            string capturedNew = null;
            int capturedControlPoints = -1;
            int eventCount = 0;
            Action<string, string, string, int> handler = (nodeId, oldController, newController, controlPoints) =>
            {
                eventCount++;
                capturedNode = nodeId;
                capturedOld = oldController;
                capturedNew = newController;
                capturedControlPoints = controlPoints;
            };

            events.OnNodeControlChanged += handler;

            var node = state.GetNode(GameIds.Node.SyriaZone);
            int controlBefore = node != null ? node.ControlPoints : -1;
            string controllerBefore = node != null ? GameIds.ResolveFactionId(node.ControllingFactionId) : null;

            bool success = d1.ExecuteAction(EventideAge.Systems.D1.MilitaryActionType.Proxy, "Damascus");

            Assert("D1 Proxy action succeeds for semantics test", success);
            Assert("D1 NodeControlChanged emitted once", eventCount == 1);
            Assert("D1 event node id matches target", capturedNode == GameIds.Node.SyriaZone);
            Assert("D1 event old controller equals pre-action controller", capturedOld == controllerBefore);
            Assert("D1 event new controller equals node controller", node != null && capturedNew == GameIds.ResolveFactionId(node.ControllingFactionId));
            Assert("D1 CP-only update keeps controller unchanged", capturedOld == capturedNew);
            Assert("D1 event control points equals node state", node != null && capturedControlPoints == node.ControlPoints);
            Assert("D1 proxy action changes control points", node != null && node.ControlPoints < controlBefore);

            events.OnNodeControlChanged -= handler;

            UnityEngine.Object.DestroyImmediate(d1);
            UnityEngine.Object.DestroyImmediate(go);
            CleanupTestState(state, events);
        }

        private static void TestD5NodeControlEventSemantics()
        {
            Debug.Log("\n--- Testing D5 NodeControlChanged Semantics ---");

            var go = new GameObject("D5NodeEventTest");
            var state = CreateMinimalState();
            var events = ScriptableObject.CreateInstance<GameEvents>();

            var arms = state.GetResource("Arms");
            if (arms != null) arms.Amount = 200;
            var socialValue = state.GetResource("SocialValue");
            if (socialValue != null) socialValue.Amount = 100;

            var d5 = go.AddComponent<EventideAge.Systems.D5.WarResolutionSystem>();
            d5.Initialize(state, events);

            string capturedNode = null;
            string capturedOld = null;
            string capturedNew = null;
            int capturedControlPoints = -1;
            int eventCount = 0;
            Action<string, string, string, int> handler = (nodeId, oldController, newController, controlPoints) =>
            {
                eventCount++;
                capturedNode = nodeId;
                capturedOld = oldController;
                capturedNew = newController;
                capturedControlPoints = controlPoints;
            };

            events.OnNodeControlChanged += handler;

            var node = state.GetNode(GameIds.Node.SyriaZone);
            string controllerBefore = node != null ? GameIds.ResolveFactionId(node.ControllingFactionId) : null;

            int attackerLosses;
            int defenderLosses;
            d5.ResolveWar("Damascus", out attackerLosses, out defenderLosses);

            Assert("D5 NodeControlChanged emitted once", eventCount == 1);
            Assert("D5 event node id matches target", capturedNode == GameIds.Node.SyriaZone);
            Assert("D5 event old controller equals pre-resolution controller", capturedOld == controllerBefore);
            Assert("D5 event new controller equals node controller", node != null && capturedNew == GameIds.ResolveFactionId(node.ControllingFactionId));
            Assert("D5 can emit controller transition", capturedOld != capturedNew);
            Assert("D5 event control points equals node state", node != null && capturedControlPoints == node.ControlPoints);

            events.OnNodeControlChanged -= handler;

            UnityEngine.Object.DestroyImmediate(d5);
            UnityEngine.Object.DestroyImmediate(go);
            CleanupTestState(state, events);
        }

        private static void TestGAdjacencyGuardrail()
        {
            Debug.Log("\n--- Testing G Adjacency Guardrail ---");

            ResetGameManagerSingleton();

            var go = new GameObject("GAIAdjacencyTest");
            var state = CreateMinimalState();
            var events = ScriptableObject.CreateInstance<GameEvents>();

            var h2 = go.AddComponent<EventideAge.Systems.H2.NodeNetworkSystem>();
            h2.Initialize(state, events);

            var g = go.AddComponent<EventideAge.Systems.G.FactionAISystem>();
            g.NodeNetworkSystem = h2;
            g.Initialize(state, events);
            Assert("G GetAI resolves alias faction id", g.GetAI("GoldLeader") != null);

            var friendlyNodes = new List<string> { GameIds.Node.IraqBorder };
            var friendlyAliasNodes = new List<string> { "Tigris" };

            bool sameNode = (bool)InvokePrivateMethod(g, "IsNodeAdjacentToFriendly", GameIds.Node.IraqBorder, friendlyNodes);
            bool adjacentNode = (bool)InvokePrivateMethod(g, "IsNodeAdjacentToFriendly", "SmallNode", friendlyNodes);
            bool adjacentByAlias = (bool)InvokePrivateMethod(g, "IsNodeAdjacentToFriendly", "SmallNode", friendlyAliasNodes);
            bool sameNodeByAlias = (bool)InvokePrivateMethod(g, "IsNodeAdjacentToFriendly", GameIds.Node.SyriaZone, new List<string> { "Damascus" });
            bool farNode = (bool)InvokePrivateMethod(g, "IsNodeAdjacentToFriendly", GameIds.Node.Mediterranean, friendlyNodes);

            Assert("G adjacency accepts same node", sameNode);
            Assert("G adjacency accepts connected node", adjacentNode);
            Assert("G adjacency resolves alias friendly-node id", adjacentByAlias);
            Assert("G adjacency resolves alias target-node id", sameNodeByAlias);
            Assert("G adjacency rejects non-neighbor node", !farNode);

            UnityEngine.Object.DestroyImmediate(g);
            UnityEngine.Object.DestroyImmediate(h2);
            UnityEngine.Object.DestroyImmediate(go);
            CleanupTestState(state, events);
            ResetGameManagerSingleton();
        }

        private static void TestGAIDecisionExecutionGuardrail()
        {
            Debug.Log("\n--- Testing G AI Decision Execution Guardrail ---");

            ResetGameManagerSingleton();

            var managerGO = new GameObject("GAIDecisionManager");
            var manager = managerGO.AddComponent<GameManager>();

            var systemsGO = new GameObject("Systems");
            systemsGO.transform.SetParent(managerGO.transform);

            var state = CreateMinimalState();
            var events = ScriptableObject.CreateInstance<GameEvents>();

            var h2 = systemsGO.AddComponent<EventideAge.Systems.H2.NodeNetworkSystem>();
            var b3 = systemsGO.AddComponent<EventideAge.Systems.B3.TradeNetworkSystem>();
            var b2 = systemsGO.AddComponent<EventideAge.Systems.B2.BlockadeSystem>();
            var c2 = systemsGO.AddComponent<EventideAge.Systems.C2.DiplomaticProtocolsSystem>();
            var d2 = systemsGO.AddComponent<EventideAge.Systems.D2.MilitaryPoliticalLinkageSystem>();
            var d5 = systemsGO.AddComponent<EventideAge.Systems.D5.WarResolutionSystem>();
            var d1 = systemsGO.AddComponent<EventideAge.Systems.D1.MilitaryOperationsSystem>();
            var g = systemsGO.AddComponent<EventideAge.Systems.G.FactionAISystem>();

            h2.Initialize(state, events);
            b3.Initialize(state, events);
            b2.Initialize(state, events);
            c2.Initialize(state, events);
            d2.Initialize(state, events);
            d5.Initialize(state, events);
            d1.Initialize(state, events);
            g.Initialize(state, events);

            g.NodeNetworkSystem = h2;
            d1.ProxySuccessRate = 1f;

            var socialValue = state.GetResource(GameIds.Resource.SocialValue);
            if (socialValue != null) socialValue.Amount = 100;
            var ashWill = state.GetResource(GameIds.Resource.AshWill);
            if (ashWill != null) ashWill.Amount = 100;

            manager.Systems = new List<GameSystem>
            {
                h2, b3, b2, c2, d2, d5, d1, g
            };

            var ai = new EventideAge.Systems.G.FactionAI
            {
                FactionId = GameIds.Faction.Aurean,
                Personality = EventideAge.Systems.G.AIPersonality.Aggressive,
                AggressionLevel = 0.8f,
                DiplomaticLevel = 0.7f
            };

            var targetNode = state.GetNode(GameIds.Node.SyriaZone);
            int controlBefore = targetNode != null ? targetNode.ControlPoints : -1;

            var militaryDecision = new EventideAge.Systems.G.AIDecision
            {
                FactionId = ai.FactionId,
                DecisionType = EventideAge.Systems.G.AIDecisionType.Military,
                ActionId = "Proxy",
                TargetId = "Damascus",
                // Keep below aggressive SpecialForces threshold to exercise deterministic Proxy path.
                Priority = 0.7f,
                ShouldExecute = true
            };

            InvokePrivateMethod(g, "ExecuteMilitaryDecision", ai, militaryDecision);

            Assert("G military decision changes target node control", targetNode != null && targetNode.ControlPoints < controlBefore);
            Assert("G military decision triggers D2 digestion", d2.IsNodeUnderDigestion(GameIds.Node.SyriaZone));

            var diplomaticDecision = new EventideAge.Systems.G.AIDecision
            {
                FactionId = ai.FactionId,
                DecisionType = EventideAge.Systems.G.AIDecisionType.Diplomatic,
                ActionId = "ProposeProtocol",
                TargetId = "HolyFire",
                Priority = 0.8f,
                ShouldExecute = true
            };

            InvokePrivateMethod(g, "ExecuteDiplomaticDecision", ai, diplomaticDecision);
            Assert("G diplomatic decision signs protocol via C2", c2.GetActiveProtocolCount(ai.FactionId) > 0);

            var economicDecision = new EventideAge.Systems.G.AIDecision
            {
                FactionId = ai.FactionId,
                DecisionType = EventideAge.Systems.G.AIDecisionType.Economic,
                ActionId = "ApplyBlockade",
                TargetId = "Blockade",
                Priority = 0.8f,
                ShouldExecute = true
            };

            InvokePrivateMethod(g, "ExecuteEconomicDecision", ai, economicDecision);
            Assert("G economic decision activates blockade via B2", b2.GetTotalPressure() > 0f);

            UnityEngine.Object.DestroyImmediate(managerGO);
            CleanupTestState(state, events);
            ResetGameManagerSingleton();
        }

        private static void TestI1EventTriggerGuardrail()
        {
            Debug.Log("\n--- Testing I1 Event Trigger Guardrail ---");

            var go = new GameObject("I1EventTriggerTest");
            var state = CreateMinimalState();
            var events = ScriptableObject.CreateInstance<GameEvents>();

            var i1 = go.AddComponent<EventideAge.Systems.I1.EventSystem>();
            i1.Initialize(state, events);

            var goldLeaf = state.GetResource(GameIds.Resource.GoldLeaf);
            int initialGold = goldLeaf != null ? goldLeaf.Amount : 0;

            var turnBased = i1.CreateTurnBasedEvent(
                "TurnBasedIncome",
                2,
                new[] { $"resource_change:{GameIds.Resource.GoldLeaf}:10" });
            i1.QueueEvent(turnBased);

            events.TurnEnded(1);
            Assert("I1 turn-based event does not trigger before target turn", goldLeaf != null && goldLeaf.Amount == initialGold);

            state.CurrentTurn = 2;
            events.TurnEnded(2);
            Assert("I1 turn-based event triggers at target turn", goldLeaf != null && goldLeaf.Amount == initialGold + 10);

            var conditionEvent = i1.CreateResponseEvent(
                "ConditionIncome",
                $"resource:{GameIds.Resource.GoldLeaf}>=110&&relation:{GameIds.Faction.Aurean}<=-40&&node_control:{GameIds.Node.SyriaZone}={GameIds.Faction.Aurean}",
                new[] { $"resource_change:{GameIds.Resource.GoldLeaf}:5" });
            i1.QueueEvent(conditionEvent);

            events.TurnEnded(2);
            Assert("I1 condition-based event triggers when conditions are satisfied", goldLeaf != null && goldLeaf.Amount == initialGold + 15);

            var failedConditionEvent = i1.CreateResponseEvent(
                "ConditionBlocked",
                $"resource:{GameIds.Resource.GoldLeaf}>999",
                new[] { $"resource_change:{GameIds.Resource.GoldLeaf}:7" });
            i1.QueueEvent(failedConditionEvent);

            events.TurnEnded(2);
            Assert("I1 condition-based event remains blocked when condition is false", goldLeaf != null && goldLeaf.Amount == initialGold + 15);

            UnityEngine.Object.DestroyImmediate(i1);
            UnityEngine.Object.DestroyImmediate(go);
            CleanupTestState(state, events);
        }

        private static void TestB5SettlementExpenseGuardrail()
        {
            Debug.Log("\n--- Testing B5 Settlement Expense Guardrail ---");

            ResetGameManagerSingleton();

            var managerGO = new GameObject("B5SettlementManager");
            var manager = managerGO.AddComponent<GameManager>();
            var systemsGO = new GameObject("Systems");
            systemsGO.transform.SetParent(managerGO.transform);

            var state = CreateMinimalState();
            var events = ScriptableObject.CreateInstance<GameEvents>();

            var finance = systemsGO.AddComponent<EventideAge.Systems.B1.FinanceSystem>();
            var tradeNetwork = systemsGO.AddComponent<EventideAge.Systems.B3.TradeNetworkSystem>();
            var b5 = systemsGO.AddComponent<EventideAge.Systems.B5.EconomicSettlementSystem>();

            finance.Initialize(state, events);
            tradeNetwork.Initialize(state, events);
            b5.Initialize(state, events);

            manager.Systems = new List<GameSystem> { finance, tradeNetwork, b5 };

            var arms = state.GetResource(GameIds.Resource.Arms);
            if (arms != null) arms.Amount = 120;
            var socialValue = state.GetResource(GameIds.Resource.SocialValue);
            if (socialValue != null) socialValue.Amount = 10;

            int goldBefore = state.GetResource(GameIds.Resource.GoldLeaf)?.Amount ?? 0;
            var settlement = b5.ExecuteTurnSettlement();
            int goldAfter = state.GetResource(GameIds.Resource.GoldLeaf)?.Amount ?? 0;

            Assert("B5 settlement returns result", settlement != null);
            Assert("B5 settlement changes GoldLeaf", goldAfter != goldBefore);

            bool hasExpenseLog = settlement.ResourceChanges.Any(change =>
                change.ResourceId == GameIds.Resource.GoldLeaf
                && change.ChangeAmount < 0
                && !string.IsNullOrEmpty(change.ChangeReason)
                && change.ChangeReason.Contains("Operational upkeep"));
            Assert("B5 settlement records expense phase change", hasExpenseLog);

            var report = b5.GetResourceChangeReport();
            Assert("B5 report returns last settlement snapshot", report.Count == settlement.ResourceChanges.Count);

            UnityEngine.Object.DestroyImmediate(managerGO);
            CleanupTestState(state, events);
            ResetGameManagerSingleton();
        }

        private static void TestB4ExchangeRateReciprocalGuardrail()
        {
            Debug.Log("\n--- Testing B4 Exchange Rate Reciprocal Guardrail ---");

            var go = new GameObject("B4ExchangeRateTest");
            var state = CreateMinimalState();
            var events = ScriptableObject.CreateInstance<GameEvents>();

            var b4 = go.AddComponent<EventideAge.Systems.B4.ExchangeRateSystem>();
            b4.EvaluationInterval = 1;
            b4.Initialize(state, events);

            float initialForward = b4.GetExchangeRate(EventideAge.Systems.B4.Currency.GoldLeaves, EventideAge.Systems.B4.Currency.TradeNotes);
            float initialReverse = b4.GetExchangeRate(EventideAge.Systems.B4.Currency.TradeNotes, EventideAge.Systems.B4.Currency.GoldLeaves);
            Assert("B4 initial GoldLeaves<->TradeNotes rates are reciprocal", Mathf.Abs((initialForward * initialReverse) - 1f) < 0.001f);

            events.TurnEnded(state.CurrentTurn);

            var rates = b4.GetAllRates();
            float tradeMin = b4.GoldLeavesPerTradeNotes * (1f - b4.TradeNotesVolatility);
            float tradeMax = b4.GoldLeavesPerTradeNotes * (1f + b4.TradeNotesVolatility);
            Assert("B4 trade-notes forward rate remains within configured volatility band", rates.GoldLeavesToTradeNotes >= tradeMin && rates.GoldLeavesToTradeNotes <= tradeMax);

            float northMin = b4.GoldLeavesPerNorthCoins * (1f - b4.NorthCoinsVolatility);
            float northMax = b4.GoldLeavesPerNorthCoins * (1f + b4.NorthCoinsVolatility);
            Assert("B4 north-coins forward rate remains within configured volatility band", rates.GoldLeavesToNorthCoins >= northMin && rates.GoldLeavesToNorthCoins <= northMax);

            float updatedReverse = b4.GetExchangeRate(EventideAge.Systems.B4.Currency.TradeNotes, EventideAge.Systems.B4.Currency.GoldLeaves);
            Assert("B4 updated reverse trade-notes rate tracks forward reciprocal", Mathf.Abs(updatedReverse - (1f / rates.GoldLeavesToTradeNotes)) < 0.001f);

            float seedAmount = 137f;
            float tradeNotes = b4.Convert(seedAmount, EventideAge.Systems.B4.Currency.GoldLeaves, EventideAge.Systems.B4.Currency.TradeNotes);
            float roundTrip = b4.Convert(tradeNotes, EventideAge.Systems.B4.Currency.TradeNotes, EventideAge.Systems.B4.Currency.GoldLeaves);
            Assert("B4 GoldLeaves -> TradeNotes -> GoldLeaves roundtrip remains near identity", Mathf.Abs(roundTrip - seedAmount) < 0.05f);

            UnityEngine.Object.DestroyImmediate(b4);
            UnityEngine.Object.DestroyImmediate(go);
            CleanupTestState(state, events);
        }

        private static void TestB5OverdraftThresholdGuardrail()
        {
            Debug.Log("\n--- Testing B5 Overdraft Threshold Guardrail ---");

            ResetGameManagerSingleton();

            var managerGO = new GameObject("B5OverdraftManager");
            var manager = managerGO.AddComponent<GameManager>();
            var systemsGO = new GameObject("Systems");
            systemsGO.transform.SetParent(managerGO.transform);

            var state = CreateMinimalState();
            var events = ScriptableObject.CreateInstance<GameEvents>();

            var finance = systemsGO.AddComponent<EventideAge.Systems.B1.FinanceSystem>();
            var tradeNetwork = systemsGO.AddComponent<EventideAge.Systems.B3.TradeNetworkSystem>();
            var b5 = systemsGO.AddComponent<EventideAge.Systems.B5.EconomicSettlementSystem>();
            b5.MaxOverdraftMonths = 2;
            b5.OverdraftInterestRate = 0f;

            finance.Initialize(state, events);
            tradeNetwork.Initialize(state, events);
            b5.Initialize(state, events);

            manager.Systems = new List<GameSystem> { finance, tradeNetwork, b5 };

            var goldLeaf = state.GetResource(GameIds.Resource.GoldLeaf);
            if (goldLeaf != null) goldLeaf.Amount = 10;
            var arms = state.GetResource(GameIds.Resource.Arms);
            if (arms != null) arms.Amount = 200;
            var socialValue = state.GetResource(GameIds.Resource.SocialValue);
            if (socialValue != null) socialValue.Amount = 100;

            int endCount = 0;
            string endReason = null;
            events.OnGameEnded += reason =>
            {
                endCount++;
                endReason = reason;
            };

            b5.ExecuteTurnSettlement();
            Assert("B5 first overdraft turn does not trigger collapse before configured threshold", endCount == 0);
            Assert("B5 GoldLeaf remains negative while in overdraft", goldLeaf != null && goldLeaf.Amount < 0);

            b5.ExecuteTurnSettlement();
            Assert("B5 overdraft collapse triggers at configured threshold", endCount == 1 && endReason == "economic_collapse");

            UnityEngine.Object.DestroyImmediate(managerGO);
            CleanupTestState(state, events);
            ResetGameManagerSingleton();
        }

        private static void TestB1CurrencyCanonicalGuardrail()
        {
            Debug.Log("\n--- Testing B1 Currency Canonical Guardrail ---");

            var go = new GameObject("B1CurrencyCanonicalTest");
            var state = CreateMinimalState();
            var events = ScriptableObject.CreateInstance<GameEvents>();

            var finance = go.AddComponent<EventideAge.Systems.B1.FinanceSystem>();
            finance.Initialize(state, events);

            var goldLeaf = state.GetResource(GameIds.Resource.GoldLeaf);
            var tradeToken = state.GetResource(GameIds.Resource.TradeToken);
            if (goldLeaf != null) goldLeaf.Amount = 200;
            if (tradeToken != null) tradeToken.Amount = 120;

            var changedResourceIds = new List<string>();
            events.OnResourceChanged += (resourceId, oldAmount, newAmount) =>
            {
                changedResourceIds.Add(resourceId);
            };

            bool legacyGoldPurchase = finance.TryPurchaseWithCurrency("GoldLeaves", 2f, 10f);
            bool canonicalGoldPurchase = finance.TryPurchaseWithCurrency(GameIds.Resource.GoldLeaf, 1f, 10f);
            bool legacyTradeNotesPurchase = finance.TryPurchaseWithCurrency("TradeNotes", 2f, 10f);
            bool legacyNorthCoinsPurchase = finance.TryPurchaseWithCurrency("NorthCoins", 1f, 10f);
            bool canonicalTradeTokenPurchase = finance.TryPurchaseWithCurrency(GameIds.Resource.TradeToken, 3f, 10f);
            bool unknownChannelPurchase = finance.TryPurchaseWithCurrency("UnknownCurrency", 1f, 10f);

            Assert("B1 legacy GoldLeaves channel remains usable via canonical mapping", legacyGoldPurchase);
            Assert("B1 canonical GoldLeaf resource id is accepted for purchase", canonicalGoldPurchase);
            Assert("B1 legacy TradeNotes channel maps to TradeToken resource", legacyTradeNotesPurchase);
            Assert("B1 legacy NorthCoins channel maps to TradeToken resource", legacyNorthCoinsPurchase);
            Assert("B1 canonical TradeToken resource id is accepted for purchase", canonicalTradeTokenPurchase);
            Assert("B1 unknown currency channel is rejected", !unknownChannelPurchase);

            Assert("B1 GoldLeaf balance deduction is correct after mixed legacy/canonical spending", goldLeaf != null && goldLeaf.Amount == 170);
            Assert("B1 TradeToken balance deduction is correct after mixed legacy/canonical spending", tradeToken != null && tradeToken.Amount == 60);

            bool emitsCanonicalOnly = changedResourceIds.All(id =>
                id == GameIds.Resource.GoldLeaf || id == GameIds.Resource.TradeToken);
            Assert("B1 purchase path emits canonical resource ids only", changedResourceIds.Count == 5 && emitsCanonicalOnly);

            UnityEngine.Object.DestroyImmediate(finance);
            UnityEngine.Object.DestroyImmediate(go);
            CleanupTestState(state, events);
        }

        private static void TestB5SettlementCanonicalResourceIdGuardrail()
        {
            Debug.Log("\n--- Testing B5 Settlement Canonical ResourceId Guardrail ---");

            ResetGameManagerSingleton();

            var managerGO = new GameObject("B5CanonicalResourceIdManager");
            var manager = managerGO.AddComponent<GameManager>();
            var systemsGO = new GameObject("Systems");
            systemsGO.transform.SetParent(managerGO.transform);

            var state = CreateMinimalState();
            var events = ScriptableObject.CreateInstance<GameEvents>();

            var finance = systemsGO.AddComponent<EventideAge.Systems.B1.FinanceSystem>();
            var tradeNetwork = systemsGO.AddComponent<EventideAge.Systems.B3.TradeNetworkSystem>();
            var b5 = systemsGO.AddComponent<EventideAge.Systems.B5.EconomicSettlementSystem>();

            finance.Initialize(state, events);
            tradeNetwork.Initialize(state, events);
            b5.Initialize(state, events);

            manager.Systems = new List<GameSystem> { finance, tradeNetwork, b5 };

            var arms = state.GetResource(GameIds.Resource.Arms);
            if (arms != null) arms.Amount = 160;
            var socialValue = state.GetResource(GameIds.Resource.SocialValue);
            if (socialValue != null) socialValue.Amount = 15;

            var settlement = b5.ExecuteTurnSettlement();
            Assert("B5 settlement result exists for canonical resource id check", settlement != null);

            bool settlementUsesCanonicalResourceIds = settlement != null && settlement.ResourceChanges.All(change =>
                !string.IsNullOrEmpty(change.ResourceId) && IsCanonicalResourceId(change.ResourceId));
            Assert("B5 settlement resource report uses canonical resource ids only", settlementUsesCanonicalResourceIds);

            var report = b5.GetResourceChangeReport();
            bool reportUsesCanonicalResourceIds = report.All(change =>
                !string.IsNullOrEmpty(change.ResourceId) && IsCanonicalResourceId(change.ResourceId));
            Assert("B5 cached resource change report keeps canonical resource ids only", reportUsesCanonicalResourceIds);

            UnityEngine.Object.DestroyImmediate(managerGO);
            CleanupTestState(state, events);
            ResetGameManagerSingleton();
        }

        private static void TestH2H3RuntimeGuardrail()
        {
            Debug.Log("\n--- Testing H2/H3 Runtime Guardrail ---");

            var go = new GameObject("H2H3RuntimeTest");
            var state = CreateMinimalState();
            var events = ScriptableObject.CreateInstance<GameEvents>();

            var h2 = go.AddComponent<EventideAge.Systems.H2.NodeNetworkSystem>();
            var f1 = go.AddComponent<EventideAge.Systems.F1.IntelligenceSystem>();
            var h3 = go.AddComponent<EventideAge.Systems.H3.TerrainVisionSystem>();

            h3.NodeNetworkSystem = h2;
            h3.IntelligenceSystem = f1;
            f1.TerrainVisionSystem = h3;

            h2.Initialize(state, events);
            f1.Initialize(state, events);
            h3.Initialize(state, events);

            Assert("H2 contains SyriaZone node", h2.HasNode(GameIds.Node.SyriaZone));
            Assert("H2 alias lookup resolves Damascus node", h2.HasNode("Damascus"));
            Assert("H2 detects regional adjacency", h2.AreAdjacent(GameIds.Node.IraqBorder, "SmallNode"));
            Assert("H2 adjacency accepts alias node id", h2.AreAdjacent("Damascus", "ChokepointNode"));
            Assert("H2 distance across disconnected regions is -1", h2.GetDistance(GameIds.Node.IraqBorder, GameIds.Node.SyriaZone) == -1);

            Assert("H3 initially marks distant enemy node as unknown", h3.GetVisionLevel(GameIds.Node.SyriaZone) == EventideAge.Systems.H3.VisionLevel.Unknown);
            h3.ApplyIntelligenceReport("Damascus", EventideAge.Systems.F1.FogLevel.Clear, 0.9f);
            Assert("H3 applies intelligence report visibility", h3.IsVisibleToPlayer(GameIds.Node.SyriaZone));

            UnityEngine.Object.DestroyImmediate(h3);
            UnityEngine.Object.DestroyImmediate(f1);
            UnityEngine.Object.DestroyImmediate(h2);
            UnityEngine.Object.DestroyImmediate(go);
            CleanupTestState(state, events);
        }

        private static void TestH2DefaultRouteConnectivityGuardrail()
        {
            Debug.Log("\n--- Testing H2 Default Route Connectivity Guardrail ---");

            var go = new GameObject("H2DefaultConnectivityTest");
            var state = CreateDefaultState();
            var events = ScriptableObject.CreateInstance<GameEvents>();

            var h2 = go.AddComponent<EventideAge.Systems.H2.NodeNetworkSystem>();
            h2.Initialize(state, events);

            Assert("H2 default map contains 12 canonical nodes", h2.GetNodeCount() == 12);
            Assert("H2 default map adds inter-region Hormuz-IraqBorder route", h2.AreAdjacent(GameIds.Node.Hormuz, GameIds.Node.IraqBorder));
            Assert("H2 default map adds inter-region Bushehr-Caucasus route", h2.AreAdjacent(GameIds.Node.Bushehr, GameIds.Node.Caucasus));
            Assert("H2 default map accepts Damascus alias adjacency checks", h2.AreAdjacent("Damascus", GameIds.Node.IraqBorder));

            int hormuzToTradeHub = h2.GetDistance(GameIds.Node.Hormuz, GameIds.Node.TradeHub);
            Assert("H2 default map keeps long-haul route reachable", hormuzToTradeHub > 0);
            Assert("H2 default map builds a non-trivial connection graph", h2.GetConnectionCount() >= 13);

            UnityEngine.Object.DestroyImmediate(h2);
            UnityEngine.Object.DestroyImmediate(go);
            CleanupTestState(state, events);
        }

        private static void TestH3SecondaryDepthVisionGuardrail()
        {
            Debug.Log("\n--- Testing H3 Secondary Depth Vision Guardrail ---");

            var go = new GameObject("H3DepthVisionTest");
            var state = CreateDefaultState();
            var events = ScriptableObject.CreateInstance<GameEvents>();

            var h2 = go.AddComponent<EventideAge.Systems.H2.NodeNetworkSystem>();
            var h3 = go.AddComponent<EventideAge.Systems.H3.TerrainVisionSystem>();
            h3.NodeNetworkSystem = h2;
            h3.SecondaryVisionDepth = 0;

            h2.Initialize(state, events);
            h3.Initialize(state, events);

            Assert("H3 depth=0 keeps 2-hop SyriaZone unknown", h3.GetVisionLevel(GameIds.Node.SyriaZone) == EventideAge.Systems.H3.VisionLevel.Unknown);
            Assert("H3 depth=0 keeps 3-hop Mediterranean unknown", h3.GetVisionLevel(GameIds.Node.Mediterranean) == EventideAge.Systems.H3.VisionLevel.Unknown);

            h3.SecondaryVisionDepth = 2;
            h3.RecalculateVision();

            Assert("H3 depth=2 reveals SyriaZone as secondary vision", h3.GetVisionLevel(GameIds.Node.SyriaZone) != EventideAge.Systems.H3.VisionLevel.Unknown);
            Assert("H3 depth=2 reaches Mediterranean on deeper route", h3.GetVisionLevel(GameIds.Node.Mediterranean) != EventideAge.Systems.H3.VisionLevel.Unknown);

            UnityEngine.Object.DestroyImmediate(h3);
            UnityEngine.Object.DestroyImmediate(h2);
            UnityEngine.Object.DestroyImmediate(go);
            CleanupTestState(state, events);
        }

        private static void TestC1DiplomaticRelationsGuardrail()
        {
            Debug.Log("\n--- Testing C1 Diplomatic Relations Guardrail ---");

            var go = new GameObject("C1RelationsTest");
            var state = CreateMinimalState();
            var events = ScriptableObject.CreateInstance<GameEvents>();

            var c1 = go.AddComponent<EventideAge.Systems.C1.DiplomaticRelationsSystem>();
            c1.Initialize(state, events);

            Assert("C1 can negotiate initially", c1.CanNegotiate(GameIds.Faction.Aurean));
            Assert("C1 can negotiate via alias faction id", c1.CanNegotiate("AureanDominion"));

            bool modified = c1.ModifyRelation("GoldLeader", 20, "guardrail_test");
            Assert("C1 relation modification succeeds", modified);
            Assert("C1 relation value updates", c1.GetRelationValue(GameIds.Faction.Aurean) == -30);

            var relation = c1.GetRelation(GameIds.Faction.Aurean);
            int cooldownBefore = relation != null ? relation.CooldownTurns : -1;
            Assert("C1 cooldown set after modification", cooldownBefore > 0);

            events.TurnEnded(1);
            var relationAfter = c1.GetRelation(GameIds.Faction.Aurean);
            int cooldownAfter = relationAfter != null ? relationAfter.CooldownTurns : -1;
            Assert("C1 cooldown decays on turn end", cooldownAfter == cooldownBefore - 1);

            UnityEngine.Object.DestroyImmediate(c1);
            UnityEngine.Object.DestroyImmediate(go);
            CleanupTestState(state, events);
        }

        private static void TestC2DiplomaticProtocolsCanonicalGuardrail()
        {
            Debug.Log("\n--- Testing C2 Diplomatic Protocols Canonical Guardrail ---");

            var go = new GameObject("C2ProtocolsCanonicalTest");
            var state = CreateDefaultState();
            var events = ScriptableObject.CreateInstance<GameEvents>();

            var c2 = go.AddComponent<EventideAge.Systems.C2.DiplomaticProtocolsSystem>();
            c2.Initialize(state, events);

            var proposal = c2.ProposeProtocol("AureanDominion", "HolyFire", EventideAge.Systems.C2.ProtocolType.TradeAgreement);
            Assert("C2 accepts alias factions when proposing protocol", proposal != null);
            Assert("C2 stores canonical proposer faction id", proposal != null && proposal.FromFaction == GameIds.Faction.Aurean);
            Assert("C2 stores canonical target faction id", proposal != null && proposal.ToFaction == GameIds.Faction.SacredFire);

            if (proposal != null)
            {
                c2.SignProtocol(proposal.ProtocolId);
                Assert(
                    "C2 HasActiveProtocol resolves alias ids to canonical pair",
                    c2.HasActiveProtocol("GoldLeader", GameIds.Faction.SacredFire, EventideAge.Systems.C2.ProtocolType.TradeAgreement));
                Assert("C2 active protocol count accepts alias faction id", c2.GetActiveProtocolCount("GoldLeader") == 1);
                Assert("C2 active protocol list accepts canonical faction id", c2.GetActiveProtocolsFor(GameIds.Faction.Aurean).Length == 1);
            }

            UnityEngine.Object.DestroyImmediate(c2);
            UnityEngine.Object.DestroyImmediate(go);
            CleanupTestState(state, events);
        }

        private static void TestC3IdeologyGuardrail()
        {
            Debug.Log("\n--- Testing C3 Ideology Guardrail ---");

            var go = new GameObject("C3IdeologyTest");
            var state = CreateMinimalState();
            var events = ScriptableObject.CreateInstance<GameEvents>();

            var c3 = go.AddComponent<EventideAge.Systems.C3.IdeologySystem>();
            c3.Initialize(state, events);

            var ashWill = state.GetResource(GameIds.Resource.AshWill);
            var socialValue = state.GetResource(GameIds.Resource.SocialValue);
            if (ashWill != null) ashWill.Amount = ashWill.MaxCapacity;
            if (socialValue != null) socialValue.Amount = socialValue.MaxCapacity;
            c3.PropagandaSuccessRate = 1f;

            int ashBefore = ashWill != null ? ashWill.Amount : 0;
            int socialBefore = socialValue != null ? socialValue.Amount : 0;
            const string c3AliasTarget = "GoldLeader";

            Assert("C3 can execute propaganda initially", c3.CanExecuteAction(EventideAge.Systems.C3.IdeologyActionType.Propaganda, c3AliasTarget));

            c3.ExecuteAction(EventideAge.Systems.C3.IdeologyActionType.Propaganda, c3AliasTarget);
            Assert("C3 AshWill cost deducted once", ashWill != null && ashWill.Amount == ashBefore - c3.PropagandaCostAshWill);
            Assert("C3 SocialValue cost deducted once", socialValue != null && socialValue.Amount == socialBefore - c3.PropagandaCostSocialValue);
            Assert("C3 cooldown blocks immediate same-target re-execution", !c3.CanExecuteAction(EventideAge.Systems.C3.IdeologyActionType.Propaganda, GameIds.Faction.Aurean));

            int ashAfterFirst = ashWill != null ? ashWill.Amount : 0;
            int socialAfterFirst = socialValue != null ? socialValue.Amount : 0;
            bool secondExecutionResult = c3.ExecuteAction(EventideAge.Systems.C3.IdeologyActionType.Propaganda, GameIds.Faction.Aurean);
            Assert("C3 immediate re-execution returns false due cooldown", !secondExecutionResult);
            Assert("C3 blocked execution does not spend AshWill", ashWill != null && ashWill.Amount == ashAfterFirst);
            Assert("C3 blocked execution does not spend SocialValue", socialValue != null && socialValue.Amount == socialAfterFirst);

            events.TurnEnded(state.CurrentTurn);
            events.TurnEnded(state.CurrentTurn + 1);
            Assert("C3 cooldown unlocks after two turn-end ticks", c3.CanExecuteAction(EventideAge.Systems.C3.IdeologyActionType.Propaganda, GameIds.Faction.Aurean));

            if (ashWill != null) ashWill.Amount = c3.PropagandaCostAshWill - 1;
            Assert("C3 blocks actions when AshWill is insufficient", !c3.CanExecuteAction(EventideAge.Systems.C3.IdeologyActionType.Propaganda, "C3_test_target"));

            UnityEngine.Object.DestroyImmediate(c3);
            UnityEngine.Object.DestroyImmediate(go);
            CleanupTestState(state, events);
        }

        private static void TestC4AllianceGuardrail()
        {
            Debug.Log("\n--- Testing C4 Alliance Guardrail ---");

            var go = new GameObject("C4AllianceTest");
            var state = CreateDefaultState();
            var events = ScriptableObject.CreateInstance<GameEvents>();

            var c4 = go.AddComponent<EventideAge.Systems.C4.AllianceSystem>();
            c4.Initialize(state, events);

            var firstAlliance = c4.FormAlliance("Vahid", "GoldLeader", EventideAge.Systems.C4.AllianceType.LimitedAlliance);
            Assert("C4 forms first alliance for Vashid", firstAlliance != null);
            Assert("C4 marks existing pair as active alliance", c4.HasExistingAlliance(GameIds.Faction.Vashid, GameIds.Faction.Aurean));
            Assert("C4 alliance count resolves alias faction id", c4.GetAllianceCount("Vahid") == 1);
            Assert(
                "C4 allows distinct second alliance for same faction when below cap",
                c4.CanFormAlliance(GameIds.Faction.Vashid, GameIds.Faction.SacredFire, EventideAge.Systems.C4.AllianceType.LimitedAlliance));

            var secondAlliance = c4.FormAlliance(GameIds.Faction.Vashid, GameIds.Faction.SacredFire, EventideAge.Systems.C4.AllianceType.LimitedAlliance);
            Assert("C4 forms second distinct alliance", secondAlliance != null);
            Assert("C4 enforces max 2 active alliances per faction", !c4.CanFormAlliance(GameIds.Faction.Vashid, GameIds.Faction.GoldenHord, EventideAge.Systems.C4.AllianceType.LimitedAlliance));
            Assert("C4 blocks duplicate same-pair alliance", !c4.CanFormAlliance(GameIds.Faction.Vashid, GameIds.Faction.Aurean, EventideAge.Systems.C4.AllianceType.LimitedAlliance));

            var goldLeaf = state.GetResource(GameIds.Resource.GoldLeaf);
            if (goldLeaf != null)
            {
                goldLeaf.Amount = c4.LimitedAllianceMaintenance * 2;
                c4.ProcessMaintenance();
                Assert("C4 maintenance deducts cost while funded", goldLeaf.Amount == 0);

                goldLeaf.Amount = c4.LimitedAllianceMaintenance - 1;
                c4.ProcessMaintenance();
                Assert("C4 dissolves alliances when maintenance cannot be paid", c4.GetAlliancesFor(GameIds.Faction.Vashid).Length == 0);
            }
            else
            {
                Assert("C4 test state provides GoldLeaf resource", false);
            }

            UnityEngine.Object.DestroyImmediate(c4);
            UnityEngine.Object.DestroyImmediate(go);
            CleanupTestState(state, events);
        }

        private static void TestC5InternationalOrgsGuardrail()
        {
            Debug.Log("\n--- Testing C5 International Orgs Guardrail ---");

            var go = new GameObject("C5InternationalOrgsTest");
            var state = CreateDefaultState();
            var events = ScriptableObject.CreateInstance<GameEvents>();

            var c5 = go.AddComponent<EventideAge.Systems.C5.InternationalOrgsSystem>();
            c5.Initialize(state, events);

            var sanctionsResolution = c5.ProposeResolution("Vahid", "GoldLeader", EventideAge.Systems.C5.ResolutionType.Sanctions);
            Assert("C5 can propose initial sanctions resolution", sanctionsResolution != null);
            Assert(
                "C5 proposer enters cooldown immediately after proposal",
                !c5.CanProposeResolution(GameIds.Faction.Vashid, EventideAge.Systems.C5.ResolutionType.Ceasefire));

            if (sanctionsResolution != null)
            {
                int aureanWeight = 10 + Mathf.RoundToInt((state.GetFaction(GameIds.Faction.Aurean)?.ControlledPoints ?? 0) / 20f);
                c5.CastVote(sanctionsResolution, "GoldLeader", true);
                int forAfterFirstVote = sanctionsResolution.TotalVotesFor;
                c5.CastVote(sanctionsResolution, GameIds.Faction.Aurean, true);
                Assert("C5 does not double-count repeated same-side vote", sanctionsResolution.TotalVotesFor == forAfterFirstVote);

                c5.CastVote(sanctionsResolution, GameIds.Faction.Aurean, false);
                Assert("C5 moving vote from FOR to AGAINST clears prior FOR weight", sanctionsResolution.TotalVotesFor == 0);
                Assert("C5 moving vote from FOR to AGAINST keeps single current weight", sanctionsResolution.TotalVotesAgainst == aureanWeight);
            }

            state.CurrentTurn += c5.ResolutionCooldown;
            Assert(
                "C5 proposer cooldown expires after configured turns",
                c5.CanProposeResolution(GameIds.Faction.Vashid, EventideAge.Systems.C5.ResolutionType.Condemnation));

            var goldLeaf = state.GetResource(GameIds.Resource.GoldLeaf);
            int goldBefore = goldLeaf != null ? goldLeaf.Amount : 0;
            var passingResolution = c5.ProposeResolution(GameIds.Faction.Vashid, GameIds.Faction.Aurean, EventideAge.Systems.C5.ResolutionType.Sanctions);
            Assert("C5 can propose second resolution after cooldown", passingResolution != null);

            if (passingResolution != null)
            {
                c5.CastVote(passingResolution, GameIds.Faction.Vashid, true);
                c5.CastVote(passingResolution, GameIds.Faction.Neutral, true);

                var tallyResult = c5.TallyVotes(passingResolution);
                Assert("C5 sanctions resolution can pass with majority", tallyResult == EventideAge.Systems.C5.VotingResult.Passed);

                int expectedGold = Mathf.Max(0, goldBefore + c5.SanctionsGoldLeafPenalty);
                Assert("C5 sanctions apply GoldLeaf penalty once", goldLeaf != null && goldLeaf.Amount == expectedGold);
            }

            UnityEngine.Object.DestroyImmediate(c5);
            UnityEngine.Object.DestroyImmediate(go);
            CleanupTestState(state, events);
        }

        private static void TestEInternalPoliticsGuardrail()
        {
            Debug.Log("\n--- Testing E Internal Politics Guardrail ---");

            var go = new GameObject("EInternalPoliticsTest");
            var state = CreateMinimalState();
            var events = ScriptableObject.CreateInstance<GameEvents>();

            var e = go.AddComponent<EventideAge.Systems.E.InternalPoliticsSystem>();
            e.Initialize(state, events);

            string gameEndedReason = null;
            events.OnGameEnded += reason => gameEndedReason = reason;

            var arms = state.GetResource(GameIds.Resource.Arms);
            if (arms != null) arms.Amount = 0;
            var social = state.GetResource(GameIds.Resource.SocialValue);
            if (social != null) social.Amount = 0;

            var hawks = e.GetFaction("Hawks");
            if (hawks != null) hawks.Satisfaction = 10;

            events.TurnEnded(1);

            var factions = e.GetAllFactions();
            Assert("E initializes 3 internal factions", factions.Length == 3);
            int totalInfluence = factions.Sum(f => f.Influence);
            Assert("E keeps influence distribution normalized", totalInfluence >= 95 && totalInfluence <= 105);
            Assert("E crisis can trigger hawks endgame reason", gameEndedReason == "faction_crisis_hawks");

            UnityEngine.Object.DestroyImmediate(e);
            UnityEngine.Object.DestroyImmediate(go);
            CleanupTestState(state, events);
        }

        private static void TestF1IntelligenceTerrainVisionGuardrail()
        {
            Debug.Log("\n--- Testing F1 Intelligence-TerrainVision Guardrail ---");

            var go = new GameObject("F1TerrainVisionTest");
            var state = CreateMinimalState();
            var events = ScriptableObject.CreateInstance<GameEvents>();

            var h2 = go.AddComponent<EventideAge.Systems.H2.NodeNetworkSystem>();
            var f1 = go.AddComponent<EventideAge.Systems.F1.IntelligenceSystem>();
            var h3 = go.AddComponent<EventideAge.Systems.H3.TerrainVisionSystem>();

            h3.NodeNetworkSystem = h2;
            h3.IntelligenceSystem = f1;
            f1.TerrainVisionSystem = h3;

            h2.Initialize(state, events);
            f1.Initialize(state, events);
            h3.Initialize(state, events);

            int goldBefore = state.GetResource(GameIds.Resource.GoldLeaf)?.Amount ?? 0;
            var report = f1.ExecuteIntelligenceAction(EventideAge.Systems.F1.IntelligenceActionType.Reconnaissance, GameIds.Node.SyriaZone);
            int goldAfter = state.GetResource(GameIds.Resource.GoldLeaf)?.Amount ?? 0;

            Assert("F1 reconnaissance produces report", report != null);
            Assert("F1 reconnaissance deducts GoldLeaf by action cost", goldAfter == goldBefore - f1.ReconnaissanceCost);
            Assert("F1 report has known fog level", report != null && report.FogLevel != EventideAge.Systems.F1.FogLevel.Unknown);
            Assert("F1 report updates H3 vision", h3.GetVisionLevel(GameIds.Node.SyriaZone) != EventideAge.Systems.H3.VisionLevel.Unknown);

            UnityEngine.Object.DestroyImmediate(h3);
            UnityEngine.Object.DestroyImmediate(f1);
            UnityEngine.Object.DestroyImmediate(h2);
            UnityEngine.Object.DestroyImmediate(go);
            CleanupTestState(state, events);
        }

        private static void TestH1MapAdjacencyGuardrail()
        {
            Debug.Log("\n--- Testing H1 Map Adjacency Guardrail ---");

            var go = new GameObject("H1MapAdjacencyTest");
            var state = CreateMinimalState();
            var events = ScriptableObject.CreateInstance<GameEvents>();

            var h2 = go.AddComponent<EventideAge.Systems.H2.NodeNetworkSystem>();
            var h1 = go.AddComponent<EventideAge.Systems.H1.StrategicMapSystem>();
            h1.NodeNetworkSystem = h2;

            h2.Initialize(state, events);
            h1.Initialize(state, events);

            var adjacent = h1.GetAdjacentNodes(GameIds.Node.IraqBorder);
            Assert("H1 reads H2 adjacency for map operations", adjacent.Contains("SmallNode"));
            Assert("H1 adjacency resolves alias node id", h1.GetAdjacentNodes("Tigris").Contains("SmallNode"));

            var controlledNodes = h1.GetNodesControlledBy(GameIds.Faction.Vashid);
            Assert("H1 enumerates controlled nodes", controlledNodes.Count >= 2);
            Assert("H1 controlled-node query accepts alias faction id", h1.GetNodesControlledBy("Vahid").Count == controlledNodes.Count);
            Assert("H1 aggregates control points", h1.GetTotalControlPoints(GameIds.Faction.Vashid) > 0);

            UnityEngine.Object.DestroyImmediate(h1);
            UnityEngine.Object.DestroyImmediate(h2);
            UnityEngine.Object.DestroyImmediate(go);
            CleanupTestState(state, events);
        }

        private static void TestKUiTimeSyncGuardrail()
        {
            Debug.Log("\n--- Testing K UI Time Sync Guardrail ---");

            ResetGameManagerSingleton();

            var managerGO = new GameObject("KUiManager");
            var manager = managerGO.AddComponent<GameManager>();
            var systemsGO = new GameObject("Systems");
            systemsGO.transform.SetParent(managerGO.transform);

            var state = CreateMinimalState();
            var events = ScriptableObject.CreateInstance<GameEvents>();

            var gameClock = systemsGO.AddComponent<EventideAge.Systems.A5.GameClock>();
            var uiManager = systemsGO.AddComponent<EventideAge.UI.UIManager>();

            var timeTextObject = new GameObject("TimeText");
            var timeText = timeTextObject.AddComponent<UnityEngine.UI.Text>();
            uiManager.TimeText = timeText;

            manager.Systems = new List<GameSystem> { gameClock, uiManager };

            gameClock.Initialize(state, events);
            uiManager.Initialize(state, events);

            Assert("K UI reads initial time from A5", timeText.text == "2028 H1");

            int oldTurn = state.CurrentTurn;
            state.CurrentTurn = 2;
            events.TurnChanged(oldTurn, state.CurrentTurn);
            Assert("K UI updates time on turn change", timeText.text == "2028 H2");

            UnityEngine.Object.DestroyImmediate(timeTextObject);
            UnityEngine.Object.DestroyImmediate(managerGO);
            CleanupTestState(state, events);
            ResetGameManagerSingleton();
        }

        private static void TestKUiTimeFallbackGuardrail()
        {
            Debug.Log("\n--- Testing K UI Time Fallback Guardrail ---");

            ResetGameManagerSingleton();

            var managerGO = new GameObject("KUiFallbackManager");
            var manager = managerGO.AddComponent<GameManager>();
            var systemsGO = new GameObject("Systems");
            systemsGO.transform.SetParent(managerGO.transform);

            var state = CreateMinimalState();
            var events = ScriptableObject.CreateInstance<GameEvents>();

            state.CurrentTurn = 3;

            var uiManager = systemsGO.AddComponent<EventideAge.UI.UIManager>();
            var timeTextObject = new GameObject("FallbackTimeText");
            var timeText = timeTextObject.AddComponent<UnityEngine.UI.Text>();
            uiManager.TimeText = timeText;

            manager.Systems = new List<GameSystem> { uiManager };

            uiManager.Initialize(state, events);

            string expectedInitial = EventideAge.Systems.A5.GameClock.FormatTurnAsHalfYear(state.CurrentTurn);
            Assert("K UI fallback renders canonical A5 time format without GameClock instance", timeText.text == expectedInitial);

            int oldTurn = state.CurrentTurn;
            state.CurrentTurn = 4;
            events.TurnChanged(oldTurn, state.CurrentTurn);
            string expectedAfterTurn = EventideAge.Systems.A5.GameClock.FormatTurnAsHalfYear(state.CurrentTurn);
            Assert("K UI fallback keeps canonical time format after turn change", timeText.text == expectedAfterTurn);

            UnityEngine.Object.DestroyImmediate(timeTextObject);
            UnityEngine.Object.DestroyImmediate(managerGO);
            CleanupTestState(state, events);
            ResetGameManagerSingleton();
        }

        private static void TestUiCanonicalDisplayGuardrail()
        {
            Debug.Log("\n--- Testing UI Canonical Display Guardrail ---");

            var root = new GameObject("UiCanonicalDisplayTest");
            var state = CreateMinimalState();
            var events = ScriptableObject.CreateInstance<GameEvents>();

            var mapPanel = root.AddComponent<EventideAge.UI.MapPanelUI>();
            mapPanel.LatestMapText = new GameObject("MapLatest").AddComponent<UnityEngine.UI.Text>();
            mapPanel.LatestMapText.transform.SetParent(root.transform);
            mapPanel.MapHistoryText = new GameObject("MapHistory").AddComponent<UnityEngine.UI.Text>();
            mapPanel.MapHistoryText.transform.SetParent(root.transform);
            mapPanel.Initialize(state, events);

            var diplomacyPanel = root.AddComponent<EventideAge.UI.DiplomacyPanelUI>();
            diplomacyPanel.LatestDiplomacyText = new GameObject("DiplomacyLatest").AddComponent<UnityEngine.UI.Text>();
            diplomacyPanel.LatestDiplomacyText.transform.SetParent(root.transform);
            diplomacyPanel.DiplomacyHistoryText = new GameObject("DiplomacyHistory").AddComponent<UnityEngine.UI.Text>();
            diplomacyPanel.DiplomacyHistoryText.transform.SetParent(root.transform);
            diplomacyPanel.Initialize(state, events);

            var actionLog = root.AddComponent<EventideAge.UI.ActionLogUI>();
            actionLog.LatestEntryText = new GameObject("ActionLatest").AddComponent<UnityEngine.UI.Text>();
            actionLog.LatestEntryText.transform.SetParent(root.transform);
            actionLog.HistoryText = new GameObject("ActionHistory").AddComponent<UnityEngine.UI.Text>();
            actionLog.HistoryText.transform.SetParent(root.transform);
            actionLog.Initialize(state, events);

            var eventPanel = root.AddComponent<EventideAge.UI.EventPanelUI>();
            eventPanel.LatestEventText = new GameObject("EventLatest").AddComponent<UnityEngine.UI.Text>();
            eventPanel.LatestEventText.transform.SetParent(root.transform);
            eventPanel.EventHistoryText = new GameObject("EventHistory").AddComponent<UnityEngine.UI.Text>();
            eventPanel.EventHistoryText.transform.SetParent(root.transform);
            eventPanel.Initialize(state, events);

            events.NodeControlChanged("Damascus", "GoldLeader", "ResistanceAxis", 42);
            string mapLatest = mapPanel.LatestMapText.text;
            Assert("Map panel canonicalizes node alias", mapLatest.Contains(GameIds.Node.SyriaZone));
            Assert("Map panel canonicalizes old faction alias", mapLatest.Contains(GameIds.Faction.Aurean));
            Assert("Map panel canonicalizes new faction alias", mapLatest.Contains(GameIds.Faction.AshConfederacy));

            events.ActionLogAdded("C2.Pact.GoldLeader", "Vahid met ResistanceAxis in Damascus", FeedbackSeverity.Info);
            string diplomacyLatest = diplomacyPanel.LatestDiplomacyText.text;
            Assert("Diplomacy panel canonicalizes source id token", diplomacyLatest.Contains("C2.Pact.Aurean"));
            Assert("Diplomacy panel canonicalizes message faction and node aliases",
                diplomacyLatest.Contains(GameIds.Faction.Vashid)
                && diplomacyLatest.Contains(GameIds.Faction.AshConfederacy)
                && diplomacyLatest.Contains(GameIds.Node.SyriaZone));

            events.ActionLogAdded("D1.SpecialForces.Damascus", "AshCloud attacked GoldLeader near Beirut", FeedbackSeverity.Warning);
            string actionLatest = actionLog.LatestEntryText.text;
            Assert("Battle report canonicalizes source node alias", actionLatest.Contains("D1.SpecialForces.SyriaZone"));
            Assert("Battle report canonicalizes message aliases",
                actionLatest.Contains(GameIds.Faction.AshConfederacy)
                && actionLatest.Contains(GameIds.Faction.Aurean)
                && actionLatest.Contains(GameIds.Node.Mediterranean));

            events.NarrativeEventAdded("I1.AshCloud.Beirut", "Vahid envoy arrived at Damascus", FeedbackSeverity.Critical);
            string eventLatest = eventPanel.LatestEventText.text;
            Assert("Event panel canonicalizes event id aliases", eventLatest.Contains("I1.Ash Confederacy.Mediterranean"));
            Assert("Event panel canonicalizes event message aliases",
                eventLatest.Contains(GameIds.Faction.Vashid)
                && eventLatest.Contains(GameIds.Node.SyriaZone));

            UnityEngine.Object.DestroyImmediate(root);
            CleanupTestState(state, events);
        }

        private static void TestUiPanelAggregationAndOrderingGuardrail()
        {
            Debug.Log("\n--- Testing UI Panel Aggregation and Ordering Guardrail ---");

            var root = new GameObject("UiPanelAggregationAndOrderingTest");
            var state = CreateMinimalState();
            var events = ScriptableObject.CreateInstance<GameEvents>();

            var mapPanel = root.AddComponent<EventideAge.UI.MapPanelUI>();
            mapPanel.LatestMapText = new GameObject("MapLatest2").AddComponent<UnityEngine.UI.Text>();
            mapPanel.LatestMapText.transform.SetParent(root.transform);
            mapPanel.MapHistoryText = new GameObject("MapHistory2").AddComponent<UnityEngine.UI.Text>();
            mapPanel.MapHistoryText.transform.SetParent(root.transform);
            mapPanel.Initialize(state, events);

            events.NodeControlChanged("Damascus", "GoldLeader", "GoldLeader", 41);
            events.NodeControlChanged("Damascus", "GoldLeader", "ResistanceAxis", 39);

            string mapHistory = mapPanel.MapHistoryText.text ?? string.Empty;
            int mapSummaryCount = mapHistory
                .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                .Count(line => line.StartsWith("[MAP] T", StringComparison.Ordinal));
            Assert("Map panel merges same-turn same-node updates into one summary", mapSummaryCount == 1);
            Assert("Map summary includes aggregated update count", mapHistory.Contains("updates x2"));
            Assert("Map summary keeps net controller result", mapHistory.Contains(GameIds.Faction.AshConfederacy));

            var diplomacyPanel = root.AddComponent<EventideAge.UI.DiplomacyPanelUI>();
            diplomacyPanel.LatestDiplomacyText = new GameObject("DiplomacyLatest2").AddComponent<UnityEngine.UI.Text>();
            diplomacyPanel.LatestDiplomacyText.transform.SetParent(root.transform);
            diplomacyPanel.DiplomacyHistoryText = new GameObject("DiplomacyHistory2").AddComponent<UnityEngine.UI.Text>();
            diplomacyPanel.DiplomacyHistoryText.transform.SetParent(root.transform);
            diplomacyPanel.Initialize(state, events);

            events.ActionLogAdded("C2.Pact.GoldLeader", "Vahid opens channel in Damascus", FeedbackSeverity.Info);
            events.RelationshipChanged("GoldLeader", 2);
            events.ActionLogAdded("C1.Dialogue.ResistanceAxis", "GoldLeader responds to envoy", FeedbackSeverity.Info);
            events.RelationshipChanged("GoldLeader", -1);

            string diplomacyHistory = diplomacyPanel.DiplomacyHistoryText.text ?? string.Empty;
            int relationSummaryCount = diplomacyHistory
                .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                .Count(line => line.Contains("[RELATION]") && line.Contains(GameIds.Faction.Aurean));
            Assert("Diplomacy panel aggregates relation changes by faction", relationSummaryCount == 1);
            Assert("Diplomacy panel reports net relation delta", diplomacyHistory.Contains("net +1"));
            Assert("Diplomacy panel includes relation reasons", diplomacyHistory.Contains("reasons:"));

            var actionLog = root.AddComponent<EventideAge.UI.ActionLogUI>();
            actionLog.LatestEntryText = new GameObject("ActionLatest2").AddComponent<UnityEngine.UI.Text>();
            actionLog.LatestEntryText.transform.SetParent(root.transform);
            actionLog.HistoryText = new GameObject("ActionHistory2").AddComponent<UnityEngine.UI.Text>();
            actionLog.HistoryText.transform.SetParent(root.transform);
            actionLog.Initialize(state, events);

            events.ActionLogAdded("D5.Resolve.Battle", "Military resolution result", FeedbackSeverity.Warning);
            events.ActionLogAdded("D1.SpecialForces.Damascus", "Special forces action executed", FeedbackSeverity.Info);
            events.ConsequenceAdded("D1.SpecialForces.Damascus", "Fallback corridor opened at Beirut", 2, true);

            string actionHistory = actionLog.HistoryText.text ?? string.Empty;
            int executionIndex = actionHistory.IndexOf("=== ACTION EXECUTION ===", StringComparison.Ordinal);
            int resolutionIndex = actionHistory.IndexOf("=== RESOLUTION ===", StringComparison.Ordinal);
            int consequenceIndex = actionHistory.IndexOf("=== CONSEQUENCE ===", StringComparison.Ordinal);
            Assert("Battle log includes execution group", executionIndex >= 0);
            Assert("Battle log includes resolution group", resolutionIndex >= 0);
            Assert("Battle log includes consequence group", consequenceIndex >= 0);
            Assert("Battle log group order is fixed", executionIndex < resolutionIndex && resolutionIndex < consequenceIndex);

            var eventPanel = root.AddComponent<EventideAge.UI.EventPanelUI>();
            eventPanel.LatestEventText = new GameObject("EventLatest2").AddComponent<UnityEngine.UI.Text>();
            eventPanel.LatestEventText.transform.SetParent(root.transform);
            eventPanel.EventHistoryText = new GameObject("EventHistory2").AddComponent<UnityEngine.UI.Text>();
            eventPanel.EventHistoryText.transform.SetParent(root.transform);
            eventPanel.Initialize(state, events);

            events.NarrativeEventAdded("I1.Story.Damascus", "Narrative breakthrough at Beirut", FeedbackSeverity.Info);
            events.AlertAdded("System.Monitor", "Routine system alert", FeedbackSeverity.Warning);

            string eventHistory = eventPanel.EventHistoryText.text ?? string.Empty;
            int storyIndex = eventHistory.IndexOf("[STORY EVENTS]", StringComparison.Ordinal);
            int systemIndex = eventHistory.IndexOf("[SYSTEM EVENTS]", StringComparison.Ordinal);
            Assert("Event panel includes story section", storyIndex >= 0);
            Assert("Event panel includes system section", systemIndex >= 0);
            Assert("Event panel prioritizes story section above system section", storyIndex < systemIndex);
            Assert("Event panel latest display prioritizes story event", eventPanel.LatestEventText.text.Contains("Narrative breakthrough"));

            UnityEngine.Object.DestroyImmediate(root);
            CleanupTestState(state, events);
        }

        private static void TestUiPriorityAndDedupGuardrail()
        {
            Debug.Log("\n--- Testing UI Priority and Dedup Guardrail ---");

            var root = new GameObject("UiPriorityAndDedupTest");
            var state = CreateMinimalState();
            var events = ScriptableObject.CreateInstance<GameEvents>();

            var mapPanel = root.AddComponent<EventideAge.UI.MapPanelUI>();
            mapPanel.LatestMapText = new GameObject("MapLatest3").AddComponent<UnityEngine.UI.Text>();
            mapPanel.LatestMapText.transform.SetParent(root.transform);
            mapPanel.MapHistoryText = new GameObject("MapHistory3").AddComponent<UnityEngine.UI.Text>();
            mapPanel.MapHistoryText.transform.SetParent(root.transform);
            mapPanel.Initialize(state, events);

            events.IntelReportAdded("D1.SpecialForces.Damascus", "Enemy route spotted at Beirut", FeedbackSeverity.Warning);
            events.IntelReportAdded("D1.SpecialForces.Damascus", "Enemy route spotted at Beirut", FeedbackSeverity.Warning);
            events.NodeControlChanged("Damascus", "GoldLeader", "ResistanceAxis", 31);
            events.IntelReportAdded("D1.SpecialForces.Damascus", "Routine patrol report", FeedbackSeverity.Info);

            string mapHistory = mapPanel.MapHistoryText.text ?? string.Empty;
            int mapIntelDuplicateCount = mapHistory
                .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                .Count(line => line.Contains("Enemy route spotted at"));
            Assert("Map panel deduplicates same-turn repeated intel", mapIntelDuplicateCount == 1);
            Assert("Map panel latest prioritizes critical node handover", mapPanel.LatestMapText.text.Contains("SyriaZone net"));

            var diplomacyPanel = root.AddComponent<EventideAge.UI.DiplomacyPanelUI>();
            diplomacyPanel.LatestDiplomacyText = new GameObject("DiplomacyLatest3").AddComponent<UnityEngine.UI.Text>();
            diplomacyPanel.LatestDiplomacyText.transform.SetParent(root.transform);
            diplomacyPanel.DiplomacyHistoryText = new GameObject("DiplomacyHistory3").AddComponent<UnityEngine.UI.Text>();
            diplomacyPanel.DiplomacyHistoryText.transform.SetParent(root.transform);
            diplomacyPanel.Initialize(state, events);

            events.AlertAdded("C2.Pact.GoldLeader", "Sanction pressure rises", FeedbackSeverity.Warning);
            events.AlertAdded("C2.Pact.GoldLeader", "Sanction pressure rises", FeedbackSeverity.Warning);
            events.ActionLogAdded("C2.Pact.GoldLeader", "Escalation in Damascus talks", FeedbackSeverity.Info);
            events.RelationshipChanged("GoldLeader", -7);
            events.NotificationAdded("C2.Pact.GoldLeader", "Routine diplomatic briefing", FeedbackSeverity.Info);

            string diplomacyHistory = diplomacyPanel.DiplomacyHistoryText.text ?? string.Empty;
            int diplomacyDuplicateCount = diplomacyHistory
                .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                .Count(line => line.Contains("Sanction pressure rises"));
            Assert("Diplomacy panel deduplicates same-turn repeated alerts", diplomacyDuplicateCount == 1);
            Assert("Diplomacy latest prioritizes critical relation summary", diplomacyPanel.LatestDiplomacyText.text.Contains("net -7"));

            var actionLog = root.AddComponent<EventideAge.UI.ActionLogUI>();
            actionLog.LatestEntryText = new GameObject("ActionLatest3").AddComponent<UnityEngine.UI.Text>();
            actionLog.LatestEntryText.transform.SetParent(root.transform);
            actionLog.HistoryText = new GameObject("ActionHistory3").AddComponent<UnityEngine.UI.Text>();
            actionLog.HistoryText.transform.SetParent(root.transform);
            actionLog.Initialize(state, events);

            events.ActionLogAdded("D5.Resolve.Battle", "High casualties recorded", FeedbackSeverity.Critical);
            events.ActionLogAdded("D5.Resolve.Battle", "High casualties recorded", FeedbackSeverity.Critical);
            events.ActionLogAdded("D5.Resolve.Battle", "Minor skirmish outcome", FeedbackSeverity.Info);

            string actionHistory = actionLog.HistoryText.text ?? string.Empty;
            int battleDuplicateCount = actionHistory
                .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                .Count(line => line.Contains("High casualties recorded"));
            int highIndex = actionHistory.IndexOf("High casualties recorded", StringComparison.Ordinal);
            int minorIndex = actionHistory.IndexOf("Minor skirmish outcome", StringComparison.Ordinal);
            Assert("Battle log deduplicates same-turn repeated entries", battleDuplicateCount == 1);
            Assert("Battle log sorts higher severity before lower severity in stage", highIndex >= 0 && minorIndex >= 0 && highIndex < minorIndex);

            var eventPanel = root.AddComponent<EventideAge.UI.EventPanelUI>();
            eventPanel.LatestEventText = new GameObject("EventLatest3").AddComponent<UnityEngine.UI.Text>();
            eventPanel.LatestEventText.transform.SetParent(root.transform);
            eventPanel.EventHistoryText = new GameObject("EventHistory3").AddComponent<UnityEngine.UI.Text>();
            eventPanel.EventHistoryText.transform.SetParent(root.transform);
            eventPanel.Initialize(state, events);

            events.AlertAdded("System.Monitor", "Power grid unstable", FeedbackSeverity.Critical);
            events.AlertAdded("System.Monitor", "Power grid unstable", FeedbackSeverity.Critical);
            events.NarrativeEventAdded("I1.Story.Damascus", "Heroic speech delivered", FeedbackSeverity.Info);

            string eventHistory = eventPanel.EventHistoryText.text ?? string.Empty;
            int eventDuplicateCount = eventHistory
                .Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries)
                .Count(line => line.Contains("Power grid unstable"));
            Assert("Event panel deduplicates repeated system alerts", eventDuplicateCount == 1);
            Assert("Event latest prioritizes story event over system event", eventPanel.LatestEventText.text.Contains("Heroic speech delivered"));

            UnityEngine.Object.DestroyImmediate(root);
            CleanupTestState(state, events);
        }

        private static void TestUiDigestSummaryGuardrail()
        {
            Debug.Log("\n--- Testing UI Digest Summary Guardrail ---");

            var root = new GameObject("UiDigestSummaryTest");
            var state = CreateMinimalState();
            var events = ScriptableObject.CreateInstance<GameEvents>();

            var mapPanel = root.AddComponent<EventideAge.UI.MapPanelUI>();
            mapPanel.LatestMapText = new GameObject("MapLatest4").AddComponent<UnityEngine.UI.Text>();
            mapPanel.LatestMapText.transform.SetParent(root.transform);
            mapPanel.MapHistoryText = new GameObject("MapHistory4").AddComponent<UnityEngine.UI.Text>();
            mapPanel.MapHistoryText.transform.SetParent(root.transform);
            mapPanel.Initialize(state, events);

            events.NodeControlChanged("Damascus", "GoldLeader", "ResistanceAxis", 29);
            events.IntelReportAdded("D1.SpecialForces.Damascus", "Follow-up report", FeedbackSeverity.Warning);

            string mapHistory = mapPanel.MapHistoryText.text ?? string.Empty;
            Assert("Map digest summary is rendered", mapHistory.Contains("[MAP DIGEST]"));
            Assert("Map digest tracks hotspot transfers", mapHistory.Contains("hotspot transfers 1"));

            var diplomacyPanel = root.AddComponent<EventideAge.UI.DiplomacyPanelUI>();
            diplomacyPanel.LatestDiplomacyText = new GameObject("DiplomacyLatest4").AddComponent<UnityEngine.UI.Text>();
            diplomacyPanel.LatestDiplomacyText.transform.SetParent(root.transform);
            diplomacyPanel.DiplomacyHistoryText = new GameObject("DiplomacyHistory4").AddComponent<UnityEngine.UI.Text>();
            diplomacyPanel.DiplomacyHistoryText.transform.SetParent(root.transform);
            diplomacyPanel.Initialize(state, events);

            events.ActionLogAdded("C2.Pact.GoldLeader", "Escalation warning", FeedbackSeverity.Warning);
            events.RelationshipChanged("GoldLeader", -4);

            string diplomacyHistory = diplomacyPanel.DiplomacyHistoryText.text ?? string.Empty;
            Assert("Diplomacy digest summary is rendered", diplomacyHistory.Contains("[DIPLO DIGEST]"));
            Assert("Diplomacy digest includes relation hotspot", diplomacyHistory.Contains("relation hotspot"));

            var actionLog = root.AddComponent<EventideAge.UI.ActionLogUI>();
            actionLog.LatestEntryText = new GameObject("ActionLatest4").AddComponent<UnityEngine.UI.Text>();
            actionLog.LatestEntryText.transform.SetParent(root.transform);
            actionLog.HistoryText = new GameObject("ActionHistory4").AddComponent<UnityEngine.UI.Text>();
            actionLog.HistoryText.transform.SetParent(root.transform);
            actionLog.Initialize(state, events);

            events.ActionLogAdded("D1.SpecialForces.Damascus", "Raid launched", FeedbackSeverity.Info);
            events.ActionLogAdded("D5.Resolve.Battle", "Casualties confirmed", FeedbackSeverity.Critical);

            string actionHistory = actionLog.HistoryText.text ?? string.Empty;
            Assert("Battle digest summary is rendered", actionHistory.Contains("[BATTLE DIGEST]"));
            Assert("Battle digest includes stage counters", actionHistory.Contains("stages E/R/C"));

            var eventPanel = root.AddComponent<EventideAge.UI.EventPanelUI>();
            eventPanel.LatestEventText = new GameObject("EventLatest4").AddComponent<UnityEngine.UI.Text>();
            eventPanel.LatestEventText.transform.SetParent(root.transform);
            eventPanel.EventHistoryText = new GameObject("EventHistory4").AddComponent<UnityEngine.UI.Text>();
            eventPanel.EventHistoryText.transform.SetParent(root.transform);
            eventPanel.Initialize(state, events);

            events.NarrativeEventAdded("I1.Story.Damascus", "Legendary pact announced", FeedbackSeverity.Info);
            events.AlertAdded("System.Monitor", "Security breach", FeedbackSeverity.Critical);

            string eventHistory = eventPanel.EventHistoryText.text ?? string.Empty;
            Assert("Event digest summary is rendered", eventHistory.Contains("[EVENT DIGEST]"));
            Assert("Event digest includes story/system split", eventHistory.Contains("story/system"));

            UnityEngine.Object.DestroyImmediate(root);
            CleanupTestState(state, events);
        }

        private static void TestUiTurnSummaryGuardrail()
        {
            Debug.Log("\n--- Testing UI Turn Summary Guardrail ---");

            var root = new GameObject("UiTurnSummaryTest");
            var state = CreateMinimalState();
            var events = ScriptableObject.CreateInstance<GameEvents>();

            var mapPanel = root.AddComponent<EventideAge.UI.MapPanelUI>();
            mapPanel.LatestMapText = new GameObject("MapLatest5").AddComponent<UnityEngine.UI.Text>();
            mapPanel.LatestMapText.transform.SetParent(root.transform);
            mapPanel.MapHistoryText = new GameObject("MapHistory5").AddComponent<UnityEngine.UI.Text>();
            mapPanel.MapHistoryText.transform.SetParent(root.transform);
            mapPanel.Initialize(state, events);

            var diplomacyPanel = root.AddComponent<EventideAge.UI.DiplomacyPanelUI>();
            diplomacyPanel.LatestDiplomacyText = new GameObject("DiplomacyLatest5").AddComponent<UnityEngine.UI.Text>();
            diplomacyPanel.LatestDiplomacyText.transform.SetParent(root.transform);
            diplomacyPanel.DiplomacyHistoryText = new GameObject("DiplomacyHistory5").AddComponent<UnityEngine.UI.Text>();
            diplomacyPanel.DiplomacyHistoryText.transform.SetParent(root.transform);
            diplomacyPanel.Initialize(state, events);

            var actionLog = root.AddComponent<EventideAge.UI.ActionLogUI>();
            actionLog.LatestEntryText = new GameObject("ActionLatest5").AddComponent<UnityEngine.UI.Text>();
            actionLog.LatestEntryText.transform.SetParent(root.transform);
            actionLog.HistoryText = new GameObject("ActionHistory5").AddComponent<UnityEngine.UI.Text>();
            actionLog.HistoryText.transform.SetParent(root.transform);
            actionLog.Initialize(state, events);

            var eventPanel = root.AddComponent<EventideAge.UI.EventPanelUI>();
            eventPanel.LatestEventText = new GameObject("EventLatest5").AddComponent<UnityEngine.UI.Text>();
            eventPanel.LatestEventText.transform.SetParent(root.transform);
            eventPanel.EventHistoryText = new GameObject("EventHistory5").AddComponent<UnityEngine.UI.Text>();
            eventPanel.EventHistoryText.transform.SetParent(root.transform);
            eventPanel.Initialize(state, events);

            events.NodeControlChanged("Damascus", "GoldLeader", "ResistanceAxis", 25);
            events.IntelReportAdded("D1.SpecialForces.Damascus", "Corridor pressure rising", FeedbackSeverity.Warning);
            events.ActionLogAdded("C2.Pact.GoldLeader", "Diplomatic escalation", FeedbackSeverity.Warning);
            events.RelationshipChanged("GoldLeader", -5);
            events.ActionLogAdded("D5.Resolve.Battle", "Battle losses confirmed", FeedbackSeverity.Critical);
            events.NarrativeEventAdded("I1.Story.Damascus", "Historic address delivered", FeedbackSeverity.Info);

            int oldTurn = state.CurrentTurn;
            state.CurrentTurn = oldTurn + 1;
            events.TurnChanged(oldTurn, state.CurrentTurn);

            string mapHistory = mapPanel.MapHistoryText.text ?? string.Empty;
            string diplomacyHistory = diplomacyPanel.DiplomacyHistoryText.text ?? string.Empty;
            string actionHistory = actionLog.HistoryText.text ?? string.Empty;
            string eventHistory = eventPanel.EventHistoryText.text ?? string.Empty;

            Assert("Map panel emits turn summary", mapHistory.Contains($"[TURN {oldTurn} SUMMARY] map events"));
            Assert("Diplomacy panel emits turn summary", diplomacyHistory.Contains($"[TURN {oldTurn} SUMMARY] diplomacy events"));
            Assert("Battle panel emits turn summary", actionHistory.Contains($"[TURN {oldTurn} SUMMARY] battle events"));
            Assert("Event panel emits turn summary", eventHistory.Contains($"[TURN {oldTurn} SUMMARY] events"));

            UnityEngine.Object.DestroyImmediate(root);
            CleanupTestState(state, events);
        }

        private static void TestTimeoutOwnershipGuardrail()
        {
            Debug.Log("\n--- Testing A5/J Timeout Ownership Guardrail ---");

            var go = new GameObject("TimeoutOwnershipTest");
            var state = CreateMinimalState();
            var events = ScriptableObject.CreateInstance<GameEvents>();

            var a5 = go.AddComponent<EventideAge.Systems.A5.GameClock>();
            var j = go.AddComponent<EventideAge.Systems.J.VictoryDefeatSystem>();

            a5.Initialize(state, events);
            j.Initialize(state, events);

            j.VictoryThreshold = 101f;
            state.CurrentTurn = GameConfig.kMaxTurns;

            int endCount = 0;
            string endReason = null;
            events.OnGameEnded += reason =>
            {
                endCount++;
                endReason = reason;
            };

            a5.OnTurnEnded(state.CurrentTurn);
            Assert("A5 does not trigger timeout endgame directly", endCount == 0);

            j.CheckVictoryDefeat();
            Assert("J triggers timeout endgame", endCount == 1);
            Assert("J timeout reason is attrition", endReason == "attrition");

            UnityEngine.Object.DestroyImmediate(j);
            UnityEngine.Object.DestroyImmediate(a5);
            UnityEngine.Object.DestroyImmediate(go);
            CleanupTestState(state, events);
        }

        private static void TestTimeoutSingleFireGuardrail()
        {
            Debug.Log("\n--- Testing A5/J Timeout Single-Fire Guardrail ---");

            var go = new GameObject("TimeoutSingleFireTest");
            var state = CreateMinimalState();
            var events = ScriptableObject.CreateInstance<GameEvents>();

            var j = go.AddComponent<EventideAge.Systems.J.VictoryDefeatSystem>();
            j.Initialize(state, events);

            j.VictoryThreshold = 101f;
            state.CurrentTurn = GameConfig.kMaxTurns;

            int endCount = 0;
            string endReason = null;
            events.OnGameEnded += reason =>
            {
                endCount++;
                endReason = reason;
            };

            j.OnTurnEnded(state.CurrentTurn);
            events.TurnEnded(state.CurrentTurn);
            j.OnTurnEnded(state.CurrentTurn);

            Assert("J timeout endgame fires once even with dual turn-end callbacks", endCount == 1);
            Assert("J single-fire timeout reason is attrition", endReason == "attrition");

            UnityEngine.Object.DestroyImmediate(j);
            UnityEngine.Object.DestroyImmediate(go);
            CleanupTestState(state, events);
        }

        private static void TestTimeoutVictoryPriorityGuardrail()
        {
            Debug.Log("\n--- Testing A5/J Timeout Victory Priority Guardrail ---");

            var go = new GameObject("TimeoutVictoryPriorityTest");
            var state = CreateMinimalState();
            var events = ScriptableObject.CreateInstance<GameEvents>();

            var j = go.AddComponent<EventideAge.Systems.J.VictoryDefeatSystem>();
            j.Initialize(state, events);

            // Ensure a victory path can trigger before timeout check on max turn.
            j.VictoryThreshold = 0f;
            state.CurrentTurn = GameConfig.kMaxTurns;

            int endCount = 0;
            string endReason = null;
            events.OnGameEnded += reason =>
            {
                endCount++;
                endReason = reason;
            };

            j.CheckVictoryDefeat();

            Assert("Victory takes precedence over timeout on max turn", endCount == 1 && endReason != "attrition");

            UnityEngine.Object.DestroyImmediate(j);
            UnityEngine.Object.DestroyImmediate(go);
            CleanupTestState(state, events);
        }

        private static void TestTimeoutConfigSourceOfTruthGuardrail()
        {
            Debug.Log("\n--- Testing A5/J Timeout Config Source Guardrail ---");

            var go = new GameObject("TimeoutConfigSourceTest");
            var state = CreateMinimalState();
            var events = ScriptableObject.CreateInstance<GameEvents>();

            var j = go.AddComponent<EventideAge.Systems.J.VictoryDefeatSystem>();
            j.MaxGameTurns = 1;
            j.Initialize(state, events);

            Assert("J MaxGameTurns is pinned to GameConfig.kMaxTurns", j.MaxGameTurns == GameConfig.kMaxTurns);

            UnityEngine.Object.DestroyImmediate(j);
            UnityEngine.Object.DestroyImmediate(go);
            CleanupTestState(state, events);
        }

        private static void TestTimeoutA5JEventPathGuardrail()
        {
            Debug.Log("\n--- Testing A5/J Timeout Event Path Guardrail ---");

            var go = new GameObject("TimeoutEventPathTest");
            var state = CreateMinimalState();
            var events = ScriptableObject.CreateInstance<GameEvents>();

            var a5 = go.AddComponent<EventideAge.Systems.A5.GameClock>();
            var j = go.AddComponent<EventideAge.Systems.J.VictoryDefeatSystem>();

            a5.Initialize(state, events);
            j.Initialize(state, events);
            j.VictoryThreshold = 101f;
            state.CurrentTurn = GameConfig.kMaxTurns;

            int endCount = 0;
            string endReason = null;
            events.OnGameEnded += reason =>
            {
                endCount++;
                endReason = reason;
            };

            // Simulate the real turn-end event path where both A5 and J are subscribed.
            events.TurnEnded(state.CurrentTurn);

            Assert("A5/J shared TurnEnded path emits a single endgame", endCount == 1);
            Assert("A5/J shared TurnEnded path keeps J timeout reason", endReason == "attrition");

            UnityEngine.Object.DestroyImmediate(j);
            UnityEngine.Object.DestroyImmediate(a5);
            UnityEngine.Object.DestroyImmediate(go);
            CleanupTestState(state, events);
        }

        private static void TestCanonicalIdGuardrail()
        {
            Debug.Log("\n--- Testing Canonical ID Guardrail ---");

            Assert("Route id canonical constant is route_persian_gulf", GameIds.Route.PersianGulf == "route_persian_gulf");
            Assert("Legacy route id maps to canonical via GameIds", GameIds.ResolveRouteId("PersianGulf") == GameIds.Route.PersianGulf);
            Assert("Legacy resource id maps to canonical via GameIds", GameIds.ResolveResourceId("Energy") == GameIds.Resource.FireOil);
            Assert("Legacy NorthCoins maps to canonical TradeToken", GameIds.ResolveResourceId("NorthCoins") == GameIds.Resource.TradeToken);
            Assert("Legacy GoldLeafReserve maps to canonical GoldLeaf", GameIds.ResolveResourceId("GoldLeafReserve") == GameIds.Resource.GoldLeaf);
            Assert("Legacy faction id maps to canonical via GameIds", GameIds.ResolveFactionId("ResistanceAxis") == GameIds.Faction.AshConfederacy);

            var routeCandidates = GameIds.GetRouteIdCandidates(GameIds.Route.PersianGulf);
            bool includesCanonical = routeCandidates.Any(id => string.Equals(id, GameIds.Route.PersianGulf, StringComparison.OrdinalIgnoreCase));
            bool includesLegacy = routeCandidates.Any(id => string.Equals(id, "PersianGulf", StringComparison.OrdinalIgnoreCase));
            Assert("Route candidates include canonical id", includesCanonical);
            Assert("Route candidates keep legacy id for migration only", includesLegacy);

            var go = new GameObject("JCanonicalIdGuardrail");
            var state = CreateMinimalState();
            var events = ScriptableObject.CreateInstance<GameEvents>();

            var j = go.AddComponent<EventideAge.Systems.J.VictoryDefeatSystem>();
            j.Initialize(state, events);

            bool legacyAxis = (bool)InvokePrivateMethod(j, "IsUnderResistanceAxis", "ResistanceAxis");
            bool canonicalAxis = (bool)InvokePrivateMethod(j, "IsUnderResistanceAxis", GameIds.Faction.AshConfederacy);

            Assert("J resolves legacy axis id through GameIds", legacyAxis);
            Assert("J accepts canonical axis id", canonicalAxis);

            UnityEngine.Object.DestroyImmediate(j);
            UnityEngine.Object.DestroyImmediate(go);
            CleanupTestState(state, events);
        }

        private static void TestResourceCanonicalConvergenceGuardrail()
        {
            Debug.Log("\n--- Testing Resource Canonical Convergence Guardrail ---");

            var config = ScriptableObject.CreateInstance<GameConfig>();
            config.PhaseConfigs = new PhaseConfig[1];
            config.PhaseConfigs[0] = new PhaseConfig { PhaseName = "Test", BaseActionPoints = 2 };
            config.FactionConfigs = new FactionConfig[1];
            config.FactionConfigs[0] = new FactionConfig
            {
                FactionId = GameIds.Faction.Vashid,
                FactionName = "Vashid",
                IsPlayerControlled = true,
                InitialControlledPoints = 100,
                InitialRelationship = 100
            };
            config.ResourceConfigs = new ResourceConfig[4];
            config.ResourceConfigs[0] = new ResourceConfig { ResourceId = GameIds.Resource.TradeToken, ResourceName = "TradeToken", InitialAmount = 77, MaxCapacity = 200, ResourceType = ResourceType.Accumulative };
            config.ResourceConfigs[1] = new ResourceConfig { ResourceId = "NorthCoins", ResourceName = "NorthCoins", InitialAmount = 11, MaxCapacity = 200, ResourceType = ResourceType.Consumable };
            config.ResourceConfigs[2] = new ResourceConfig { ResourceId = GameIds.Resource.GoldLeaf, ResourceName = "GoldLeaf", InitialAmount = 90, MaxCapacity = 1000, ResourceType = ResourceType.Accumulative };
            config.ResourceConfigs[3] = new ResourceConfig { ResourceId = "GoldLeafReserve", ResourceName = "GoldLeafReserve", InitialAmount = 12, MaxCapacity = 1000, ResourceType = ResourceType.Accumulative };
            config.RegionConfigs = new RegionConfig[1];
            config.RegionConfigs[0] = new RegionConfig { RegionId = "R1", RegionName = "Region1", NodeConfigs = new NodeConfig[1] };
            config.RegionConfigs[0].NodeConfigs[0] = new NodeConfig
            {
                NodeId = "N1",
                NodeName = "Node1",
                NodeType = NodeType.ResourceNode,
                DefenseBonus = 0,
                InitialController = GameIds.Faction.Vashid,
                InitialControlPoints = 50,
                MaxControlPoints = 100
            };

            var state = ScriptableObject.CreateInstance<GameState>();
            state.Config = config;
            state.Initialize();

            var tradeTokenByLegacy = state.GetResource("NorthCoins");
            Assert(
                "Legacy NorthCoins lookup resolves to canonical TradeToken first",
                tradeTokenByLegacy != null
                && tradeTokenByLegacy.ResourceId == GameIds.Resource.TradeToken
                && tradeTokenByLegacy.Amount == 77);

            var goldLeafByLegacy = state.GetResource("GoldLeafReserve");
            Assert(
                "Legacy GoldLeafReserve lookup resolves to canonical GoldLeaf first",
                goldLeafByLegacy != null
                && goldLeafByLegacy.ResourceId == GameIds.Resource.GoldLeaf
                && goldLeafByLegacy.Amount == 90);

            UnityEngine.Object.DestroyImmediate(state);
            UnityEngine.Object.DestroyImmediate(config);
        }

        private static object InvokePrivateMethod(object target, string methodName, params object[] args)
        {
            var method = target.GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.NonPublic);
            if (method == null)
            {
                throw new MissingMethodException(target.GetType().Name, methodName);
            }

            return method.Invoke(target, args);
        }

        private static bool IsCanonicalResourceId(string resourceId)
        {
            return resourceId == GameIds.Resource.Arms
                || resourceId == GameIds.Resource.FireOil
                || resourceId == GameIds.Resource.GoldLeaf
                || resourceId == GameIds.Resource.TradeToken
                || resourceId == GameIds.Resource.SocialValue
                || resourceId == GameIds.Resource.AshWill
                || resourceId == GameIds.Resource.TributeOrder;
        }

        private static void ResetGameManagerSingleton()
        {
            var field = typeof(GameManager).GetField("<Instance>k__BackingField", BindingFlags.Static | BindingFlags.NonPublic);
            if (field != null)
            {
                field.SetValue(null, null);
            }
        }

        private static GameState CreateDefaultState()
        {
            var config = ScriptableObject.CreateInstance<GameConfig>();
            config.PhaseConfigs = new PhaseConfig[1];
            config.PhaseConfigs[0] = new PhaseConfig { PhaseName = "Test", BaseActionPoints = 2 };

            config.FactionConfigs = new FactionConfig[6];
            config.FactionConfigs[0] = new FactionConfig { FactionId = GameIds.Faction.Vashid, FactionName = "Vashid", IsPlayerControlled = true, InitialControlledPoints = 100, InitialRelationship = 100, InitialSatisfaction = 100 };
            config.FactionConfigs[1] = new FactionConfig { FactionId = GameIds.Faction.Aurean, FactionName = "Aurean", IsPlayerControlled = false, InitialControlledPoints = 90, InitialRelationship = -70, InitialSatisfaction = 70 };
            config.FactionConfigs[2] = new FactionConfig { FactionId = GameIds.Faction.SacredFire, FactionName = "SacredFire", IsPlayerControlled = false, InitialControlledPoints = 80, InitialRelationship = -60, InitialSatisfaction = 70 };
            config.FactionConfigs[3] = new FactionConfig { FactionId = GameIds.Faction.GoldenHord, FactionName = "GoldenHord", IsPlayerControlled = false, InitialControlledPoints = 70, InitialRelationship = -40, InitialSatisfaction = 70 };
            config.FactionConfigs[4] = new FactionConfig { FactionId = GameIds.Faction.AshConfederacy, FactionName = "AshConfederacy", IsPlayerControlled = false, InitialControlledPoints = 75, InitialRelationship = 20, InitialSatisfaction = 70 };
            config.FactionConfigs[5] = new FactionConfig { FactionId = GameIds.Faction.Neutral, FactionName = "Neutral", IsPlayerControlled = false, InitialControlledPoints = 40, InitialRelationship = 0, InitialSatisfaction = 70 };

            config.ResourceConfigs = new ResourceConfig[7];
            config.ResourceConfigs[0] = new ResourceConfig { ResourceId = GameIds.Resource.Arms, ResourceName = "Arms", InitialAmount = 50, MaxCapacity = 200, ResourceType = ResourceType.Consumable };
            config.ResourceConfigs[1] = new ResourceConfig { ResourceId = GameIds.Resource.FireOil, ResourceName = "FireOil", InitialAmount = 80, MaxCapacity = 300, ResourceType = ResourceType.Accumulative };
            config.ResourceConfigs[2] = new ResourceConfig { ResourceId = GameIds.Resource.GoldLeaf, ResourceName = "GoldLeaf", InitialAmount = 100, MaxCapacity = 1000, ResourceType = ResourceType.Accumulative };
            config.ResourceConfigs[3] = new ResourceConfig { ResourceId = GameIds.Resource.TradeToken, ResourceName = "TradeToken", InitialAmount = 30, MaxCapacity = 300, ResourceType = ResourceType.Consumable };
            config.ResourceConfigs[4] = new ResourceConfig { ResourceId = GameIds.Resource.SocialValue, ResourceName = "SocialValue", InitialAmount = 90, MaxCapacity = 100, ResourceType = ResourceType.Ratio };
            config.ResourceConfigs[5] = new ResourceConfig { ResourceId = GameIds.Resource.AshWill, ResourceName = "AshWill", InitialAmount = 55, MaxCapacity = 100, ResourceType = ResourceType.Ratio };
            config.ResourceConfigs[6] = new ResourceConfig { ResourceId = GameIds.Resource.TributeOrder, ResourceName = "TributeOrder", InitialAmount = 20, MaxCapacity = 100, ResourceType = ResourceType.Ratio };

            config.RegionConfigs = new RegionConfig[6];
            config.RegionConfigs[0] = new RegionConfig { RegionId = "PersianGulf", RegionName = "PersianGulf", NodeConfigs = new NodeConfig[2] };
            config.RegionConfigs[0].NodeConfigs[0] = new NodeConfig { NodeId = GameIds.Node.Hormuz, NodeName = "Hormuz", NodeType = NodeType.Chokepoint, DefenseBonus = 30, InitialController = GameIds.Faction.Vashid, InitialControlPoints = 100, MaxControlPoints = 100 };
            config.RegionConfigs[0].NodeConfigs[1] = new NodeConfig { NodeId = GameIds.Node.Bushehr, NodeName = "Bushehr", NodeType = NodeType.Port, DefenseBonus = 20, InitialController = GameIds.Faction.Vashid, InitialControlPoints = 80, MaxControlPoints = 100 };

            config.RegionConfigs[1] = new RegionConfig { RegionId = "WesternFront", RegionName = "WesternFront", NodeConfigs = new NodeConfig[2] };
            config.RegionConfigs[1].NodeConfigs[0] = new NodeConfig { NodeId = GameIds.Node.IraqBorder, NodeName = "IraqBorder", NodeType = NodeType.Chokepoint, DefenseBonus = 25, InitialController = GameIds.Faction.AshConfederacy, InitialControlPoints = 60, MaxControlPoints = 100 };
            config.RegionConfigs[1].NodeConfigs[1] = new NodeConfig { NodeId = GameIds.Node.SyriaZone, NodeName = "SyriaZone", NodeType = NodeType.City, DefenseBonus = 15, InitialController = GameIds.Faction.AshConfederacy, InitialControlPoints = 50, MaxControlPoints = 100 };

            config.RegionConfigs[2] = new RegionConfig { RegionId = "NorthernTerritory", RegionName = "NorthernTerritory", NodeConfigs = new NodeConfig[2] };
            config.RegionConfigs[2].NodeConfigs[0] = new NodeConfig { NodeId = GameIds.Node.Caspian, NodeName = "Caspian", NodeType = NodeType.ResourceNode, DefenseBonus = 10, InitialController = GameIds.Faction.Vashid, InitialControlPoints = 70, MaxControlPoints = 100 };
            config.RegionConfigs[2].NodeConfigs[1] = new NodeConfig { NodeId = GameIds.Node.Caucasus, NodeName = "Caucasus", NodeType = NodeType.Chokepoint, DefenseBonus = 35, InitialController = GameIds.Faction.Neutral, InitialControlPoints = 30, MaxControlPoints = 100 };

            config.RegionConfigs[3] = new RegionConfig { RegionId = "ArabianPeninsula", RegionName = "ArabianPeninsula", NodeConfigs = new NodeConfig[2] };
            config.RegionConfigs[3].NodeConfigs[0] = new NodeConfig { NodeId = GameIds.Node.RedSea, NodeName = "RedSea", NodeType = NodeType.Chokepoint, DefenseBonus = 30, InitialController = GameIds.Faction.GoldenHord, InitialControlPoints = 80, MaxControlPoints = 100 };
            config.RegionConfigs[3].NodeConfigs[1] = new NodeConfig { NodeId = GameIds.Node.GulfBase, NodeName = "GulfBase", NodeType = NodeType.Port, DefenseBonus = 20, InitialController = GameIds.Faction.Aurean, InitialControlPoints = 90, MaxControlPoints = 100 };

            config.RegionConfigs[4] = new RegionConfig { RegionId = "Levant", RegionName = "Levant", NodeConfigs = new NodeConfig[2] };
            config.RegionConfigs[4].NodeConfigs[0] = new NodeConfig { NodeId = GameIds.Node.Mediterranean, NodeName = "Mediterranean", NodeType = NodeType.Chokepoint, DefenseBonus = 25, InitialController = GameIds.Faction.SacredFire, InitialControlPoints = 70, MaxControlPoints = 100 };
            config.RegionConfigs[4].NodeConfigs[1] = new NodeConfig { NodeId = GameIds.Node.IsraelCore, NodeName = "IsraelCore", NodeType = NodeType.City, DefenseBonus = 40, InitialController = GameIds.Faction.SacredFire, InitialControlPoints = 100, MaxControlPoints = 100 };

            config.RegionConfigs[5] = new RegionConfig { RegionId = "CentralAsia", RegionName = "CentralAsia", NodeConfigs = new NodeConfig[2] };
            config.RegionConfigs[5].NodeConfigs[0] = new NodeConfig { NodeId = GameIds.Node.Afghanistan, NodeName = "Afghanistan", NodeType = NodeType.Chokepoint, DefenseBonus = 20, InitialController = GameIds.Faction.Neutral, InitialControlPoints = 40, MaxControlPoints = 100 };
            config.RegionConfigs[5].NodeConfigs[1] = new NodeConfig { NodeId = GameIds.Node.TradeHub, NodeName = "TradeHub", NodeType = NodeType.ResourceNode, DefenseBonus = 10, InitialController = GameIds.Faction.Neutral, InitialControlPoints = 50, MaxControlPoints = 100 };

            var state = ScriptableObject.CreateInstance<GameState>();
            state.Config = config;
            state.Initialize();
            return state;
        }
        
        private static GameState CreateMinimalState()
        {
            var config = ScriptableObject.CreateInstance<GameConfig>();
            config.PhaseConfigs = new PhaseConfig[1];
            config.PhaseConfigs[0] = new PhaseConfig { PhaseName = "Test", BaseActionPoints = 2 };
            
            config.FactionConfigs = new FactionConfig[2];
            config.FactionConfigs[0] = new FactionConfig { FactionId = GameIds.Faction.Vashid, FactionName = "Vashid", IsPlayerControlled = true, InitialControlledPoints = 100, InitialRelationship = 100, InitialSatisfaction = 100 };
            config.FactionConfigs[1] = new FactionConfig { FactionId = GameIds.Faction.Aurean, FactionName = "Aurean", IsPlayerControlled = false, InitialControlledPoints = 50, InitialRelationship = -50, InitialSatisfaction = 50 };
            
            config.ResourceConfigs = new ResourceConfig[7];
            config.ResourceConfigs[0] = new ResourceConfig { ResourceId = "SocialValue", ResourceName = "SocialValue", InitialAmount = 50, MaxCapacity = 100, ResourceType = ResourceType.Ratio };
            config.ResourceConfigs[1] = new ResourceConfig { ResourceId = "GoldLeaf", ResourceName = "GoldLeaf", InitialAmount = 100, MaxCapacity = 1000, ResourceType = ResourceType.Consumable };
            config.ResourceConfigs[2] = new ResourceConfig { ResourceId = "Arms", ResourceName = "Arms", InitialAmount = 50, MaxCapacity = 200, ResourceType = ResourceType.Consumable };
            config.ResourceConfigs[3] = new ResourceConfig { ResourceId = "FireOil", ResourceName = "FireOil", InitialAmount = 100, MaxCapacity = 500, ResourceType = ResourceType.Accumulative };
            config.ResourceConfigs[4] = new ResourceConfig { ResourceId = "TradeToken", ResourceName = "TradeToken", InitialAmount = 30, MaxCapacity = 200, ResourceType = ResourceType.Consumable };
            config.ResourceConfigs[5] = new ResourceConfig { ResourceId = "AshWill", ResourceName = "AshWill", InitialAmount = 50, MaxCapacity = 100, ResourceType = ResourceType.Ratio };
            config.ResourceConfigs[6] = new ResourceConfig { ResourceId = "TributeOrder", ResourceName = "TributeOrder", InitialAmount = 10, MaxCapacity = 100, ResourceType = ResourceType.Ratio };
            
            config.RegionConfigs = new RegionConfig[3];
            config.RegionConfigs[0] = new RegionConfig { RegionId = "WesternFront", RegionName = "WesternFront Region", NodeConfigs = new NodeConfig[2] };
            config.RegionConfigs[0].NodeConfigs[0] = new NodeConfig { NodeId = GameIds.Node.IraqBorder, NodeName = "IraqBorder", NodeType = NodeType.Chokepoint, DefenseBonus = 10, InitialController = GameIds.Faction.Vashid, InitialControlPoints = 50, MaxControlPoints = 100 };
            config.RegionConfigs[0].NodeConfigs[1] = new NodeConfig { NodeId = "SmallNode", NodeName = "SmallNode", NodeType = NodeType.ResourceNode, DefenseBonus = 5, InitialController = GameIds.Faction.Vashid, InitialControlPoints = 30, MaxControlPoints = 60 };
            
            config.RegionConfigs[1] = new RegionConfig { RegionId = "Levant", RegionName = "Levant Region", NodeConfigs = new NodeConfig[2] };
            config.RegionConfigs[1].NodeConfigs[0] = new NodeConfig { NodeId = GameIds.Node.SyriaZone, NodeName = "SyriaZone", NodeType = NodeType.City, DefenseBonus = 15, InitialController = GameIds.Faction.Aurean, InitialControlPoints = 70, MaxControlPoints = 100 };
            config.RegionConfigs[1].NodeConfigs[1] = new NodeConfig { NodeId = "ChokepointNode", NodeName = "ChokepointNode", NodeType = NodeType.Chokepoint, DefenseBonus = 20, InitialController = GameIds.Faction.Aurean, InitialControlPoints = 50, MaxControlPoints = 100 };
            
            config.RegionConfigs[2] = new RegionConfig { RegionId = "Mediterranean", RegionName = "Mediterranean Region", NodeConfigs = new NodeConfig[1] };
            config.RegionConfigs[2].NodeConfigs[0] = new NodeConfig { NodeId = GameIds.Node.Mediterranean, NodeName = "Mediterranean", NodeType = NodeType.Port, DefenseBonus = 12, InitialController = GameIds.Faction.Aurean, InitialControlPoints = 60, MaxControlPoints = 100 };
            
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
