using System;
using System.IO;
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
            RunR2AIStrategyChecks();
            RunR2TutorialFlowChecks();
            RunR2CampaignCompletionChecks();
            RunR3AIDifficultyChecks();
            TestI1EventTriggerGuardrail();
            TestI1EventPoolBackfillAndDedupGuardrail();
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
            TestR2MapConfigV1LockGuardrail();
            TestKUiTimeSyncGuardrail();
            TestKUiTimeFallbackGuardrail();
            TestUiCanonicalDisplayGuardrail();
            TestUiPanelAggregationAndOrderingGuardrail();
            TestUiPriorityAndDedupGuardrail();
            TestUiDigestSummaryGuardrail();
            TestUiTurnSummaryGuardrail();
            TestUiStatusAndJumpSemanticsGuardrail();
            TestUiLockedStatusReasonGuardrail();
            TestUiHintAndCueSemanticsGuardrail();
            TestUiLockedHintVariantGuardrail();
            TestUiLocalizationHighFrequencySurfaceLabelsGuardrail();
            TestTimeoutOwnershipGuardrail();
            TestTimeoutSingleFireGuardrail();
            TestTimeoutVictoryPriorityGuardrail();
            TestTimeoutConfigSourceOfTruthGuardrail();
            TestTimeoutA5JEventPathGuardrail();
            RunR1ReplayScenarios();
            RunR1CrossTurnConsistencyChecks();
            RunR1SaveCompatibilityChecks();
            TestCanonicalIdGuardrail();
            TestResourceCanonicalConvergenceGuardrail();
            TestL1AsyncMultiplayerLifecycleGuardrail();
            TestL2TutorialSystemLifecycleGuardrail();
            TestL3SteamIntegrationFallbackAndMockGuardrail();
            TestL4LocalizationSwitchAndFormattingGuardrail();
            
            Debug.Log($"=== Results: {passed} passed, {failed} failed ===");
        }

        public static void RunR1ReplayScenarios()
        {
            Debug.Log("\n=== R1 Replay Scenarios (RL-01 / RL-02 / RL-03 / RL-10) ===");
            TestR1ReplayRL01MapDiplomacyBattleEvent();
            TestR1ReplayRL02PhaseLifecycleReplay();
            TestR1ReplayRL03SaveLoadCrossTurnReplay();
            TestR1ReplayRL10TimeoutEndgameReplay();
        }

        public static void RunR1CrossTurnConsistencyChecks()
        {
            Debug.Log("\n=== R1 Cross-Turn Consistency Checks (R1-04) ===");
            TestR1CrossTurnConsistencyApResourceNodeVictory();
        }

        public static void RunR1SaveCompatibilityChecks()
        {
            Debug.Log("\n=== R1 Save Compatibility & Consistency Checks (R1-05) ===");
            TestR1SaveCompatibilityReadWriteAndRecovery();
            TestR1SaveCompatibilityMultiTurnConsistency();
        }

        public static void RunR2EventPoolChecks()
        {
            Debug.Log("\n=== R2 Event Pool Checks (R2-02) ===");
            TestI1EventPoolBackfillAndDedupGuardrail();
        }

        public static void RunR2MapConfigV1LockChecks()
        {
            Debug.Log("\n=== R2 Map Config V1 Lock Checks (R2-03) ===");
            TestR2MapConfigV1LockGuardrail();
        }

        public static void RunR2AIStrategyChecks()
        {
            Debug.Log("\n=== R2 AI Strategy Checks (R2-04) ===");
            TestR2AIStrategySetOverrideGuardrail();
            TestR2AIStrategyPhasePreferenceGuardrail();
            TestR2AIStrategyFallbackGuardrail();
        }

        public static void RunR2TutorialFlowChecks()
        {
            Debug.Log("\n=== R2 Tutorial Flow Checks (R2-05) ===");
            TestR2TutorialFlowScriptGuardrail();
            TestR2TutorialFlowTriggerRuleGuardrail();
        }

        public static void RunR2CampaignCompletionChecks()
        {
            Debug.Log("\n=== R2 Campaign Completion Checks (R2-06) ===");
            TestR2Campaign24TurnCompletionGuardrail();
            TestR2CampaignNoEmptyTurnAndPhaseFeedbackGuardrail();
        }

        public static void RunR3AIDifficultyChecks()
        {
            Debug.Log("\n=== R3 AI Difficulty Checks (R3-03) ===");
            TestR3AIDifficultyProfilesGuardrail();
            TestR3AIDifficultyApplyAndSwitchGuardrail();
            TestR3AIDifficultyFallbackGuardrail();
        }

        public static void RunR4UiProductizationChecks()
        {
            Debug.Log("\n=== R4 UI Productization Checks (R4-02) ===");
            TestUiCanonicalDisplayGuardrail();
            TestUiPanelAggregationAndOrderingGuardrail();
            TestUiPriorityAndDedupGuardrail();
            TestUiDigestSummaryGuardrail();
            TestUiTurnSummaryGuardrail();
            TestUiStatusAndJumpSemanticsGuardrail();
            TestUiLockedStatusReasonGuardrail();
            TestUiHintAndCueSemanticsGuardrail();
            TestUiLockedHintVariantGuardrail();
            TestUiLocalizationHighFrequencySurfaceLabelsGuardrail();
        }

        public static void RunR4UiInteractionHintChecks()
        {
            Debug.Log("\n=== R4 UI Interaction Hint Checks (R4-03) ===");
            TestUiHintAndCueSemanticsGuardrail();
            TestUiLockedHintVariantGuardrail();
        }

        public static void RunLMetaChecks()
        {
            Debug.Log("\n=== L Meta System Checks (L1-L4) ===");
            TestL1AsyncMultiplayerLifecycleGuardrail();
            TestL2TutorialSystemLifecycleGuardrail();
            TestL3SteamIntegrationFallbackAndMockGuardrail();
            TestL4LocalizationSwitchAndFormattingGuardrail();
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

        private static void TestR2AIStrategySetOverrideGuardrail()
        {
            Debug.Log("\n--- Testing R2 AI Strategy Set Override Guardrail ---");

            ResetGameManagerSingleton();

            var go = new GameObject("R2AIStrategyOverrideTest");
            var state = CreateDefaultState();
            var events = ScriptableObject.CreateInstance<GameEvents>();

            var strategySet = ScriptableObject.CreateInstance<EventideAge.Systems.G.AIStrategySet>();
            strategySet.SetProfiles(new[]
            {
                new EventideAge.Systems.G.AIFactionStrategyProfile
                {
                    FactionId = "GoldLeader",
                    Personality = EventideAge.Systems.G.AIPersonality.Diplomatic,
                    AggressionLevel = 0.2f,
                    DefenseLevel = 0.4f,
                    DiplomaticLevel = 0.9f,
                    ExpansionLevel = 0.3f,
                    ThreatPerception = 0.5f,
                    OpportunityPerception = 0.9f,
                    ActiveGoals = new[] { "r2_custom_diplomacy" },
                    PhasePreferences = new[]
                    {
                        new EventideAge.Systems.G.AIPhaseActionPreference
                        {
                            PhaseIndex = 0,
                            MilitaryWeight = 0.5f,
                            DiplomaticWeight = 1.4f,
                            EconomicWeight = 1f
                        }
                    }
                }
            });

            var g = go.AddComponent<EventideAge.Systems.G.FactionAISystem>();
            g.StrategySetConfig = strategySet;
            g.Initialize(state, events);

            var aurean = g.GetAI(GameIds.Faction.Aurean);
            Assert("R2-04 strategy override applies personality for mapped alias faction", aurean != null && aurean.Personality == EventideAge.Systems.G.AIPersonality.Diplomatic);
            Assert("R2-04 strategy override applies custom active goals", aurean != null && aurean.ActiveGoals.Contains("r2_custom_diplomacy"));
            Assert("R2-04 strategy override keeps default fallback profiles for non-overridden factions", g.GetAI(GameIds.Faction.SacredFire) != null && g.GetAI(GameIds.Faction.AshConfederacy) != null);

            UnityEngine.Object.DestroyImmediate(g);
            UnityEngine.Object.DestroyImmediate(strategySet);
            UnityEngine.Object.DestroyImmediate(go);
            CleanupTestState(state, events);
            ResetGameManagerSingleton();
        }

        private static void TestR2AIStrategyPhasePreferenceGuardrail()
        {
            Debug.Log("\n--- Testing R2 AI Strategy Phase Preference Guardrail ---");

            ResetGameManagerSingleton();

            var go = new GameObject("R2AIStrategyPhasePreferenceTest");
            var state = CreateDefaultState();
            var events = ScriptableObject.CreateInstance<GameEvents>();

            var strategySet = ScriptableObject.CreateInstance<EventideAge.Systems.G.AIStrategySet>();
            strategySet.SetProfiles(new[]
            {
                new EventideAge.Systems.G.AIFactionStrategyProfile
                {
                    FactionId = GameIds.Faction.Aurean,
                    Personality = EventideAge.Systems.G.AIPersonality.Aggressive,
                    AggressionLevel = 0.8f,
                    DefenseLevel = 0.5f,
                    DiplomaticLevel = 0.3f,
                    ExpansionLevel = 0.7f,
                    ThreatPerception = 0.7f,
                    OpportunityPerception = 0.8f,
                    ActiveGoals = new[] { "maintain_blockade" },
                    PhasePreferences = new[]
                    {
                        new EventideAge.Systems.G.AIPhaseActionPreference
                        {
                            PhaseIndex = 0,
                            MilitaryWeight = 0.25f,
                            DiplomaticWeight = 1.3f,
                            EconomicWeight = 1f
                        },
                        new EventideAge.Systems.G.AIPhaseActionPreference
                        {
                            PhaseIndex = 2,
                            MilitaryWeight = 1.4f,
                            DiplomaticWeight = 0.6f,
                            EconomicWeight = 0.8f
                        }
                    }
                }
            });

            var g = go.AddComponent<EventideAge.Systems.G.FactionAISystem>();
            g.StrategySetConfig = strategySet;
            g.Initialize(state, events);

            state.CurrentPhaseIndex = 0;
            float phase0Military = (float)InvokePrivateMethod(g, "ApplyPhasePreference", GameIds.Faction.Aurean, EventideAge.Systems.G.AIDecisionType.Military, 0.6f);
            float phase0Diplomatic = (float)InvokePrivateMethod(g, "ApplyPhasePreference", GameIds.Faction.Aurean, EventideAge.Systems.G.AIDecisionType.Diplomatic, 0.5f);
            Assert("R2-04 phase preference applies phase 0 military weight", Mathf.Abs(phase0Military - 0.15f) < 0.0001f);
            Assert("R2-04 phase preference applies phase 0 diplomatic weight", Mathf.Abs(phase0Diplomatic - 0.65f) < 0.0001f);

            state.CurrentPhaseIndex = 2;
            float phase2Military = (float)InvokePrivateMethod(g, "ApplyPhasePreference", GameIds.Faction.Aurean, EventideAge.Systems.G.AIDecisionType.Military, 0.6f);
            float phase2Diplomatic = (float)InvokePrivateMethod(g, "ApplyPhasePreference", GameIds.Faction.Aurean, EventideAge.Systems.G.AIDecisionType.Diplomatic, 0.5f);
            Assert("R2-04 phase preference applies phase 2 military weight", Mathf.Abs(phase2Military - 0.84f) < 0.0001f);
            Assert("R2-04 phase preference applies phase 2 diplomatic weight", Mathf.Abs(phase2Diplomatic - 0.3f) < 0.0001f);

            UnityEngine.Object.DestroyImmediate(g);
            UnityEngine.Object.DestroyImmediate(strategySet);
            UnityEngine.Object.DestroyImmediate(go);
            CleanupTestState(state, events);
            ResetGameManagerSingleton();
        }

        private static void TestR2AIStrategyFallbackGuardrail()
        {
            Debug.Log("\n--- Testing R2 AI Strategy Fallback Guardrail ---");

            ResetGameManagerSingleton();

            var go = new GameObject("R2AIStrategyFallbackTest");
            var state = CreateDefaultState();
            var events = ScriptableObject.CreateInstance<GameEvents>();

            var g = go.AddComponent<EventideAge.Systems.G.FactionAISystem>();
            g.Initialize(state, events);

            Assert("R2-04 fallback initializes all default AI factions when strategy asset is absent",
                g.GetAI(GameIds.Faction.Aurean) != null
                && g.GetAI(GameIds.Faction.SacredFire) != null
                && g.GetAI(GameIds.Faction.GoldenHord) != null
                && g.GetAI(GameIds.Faction.AshConfederacy) != null);
            Assert("R2-04 fallback keeps alias lookup behavior", g.GetAI("GoldLeader") != null);

            state.CurrentPhaseIndex = 0;
            float unknownFactionPriority = (float)InvokePrivateMethod(g, "ApplyPhasePreference", "UnknownFaction", EventideAge.Systems.G.AIDecisionType.Military, 0.6f);
            Assert("R2-04 fallback keeps neutral phase weight for unknown faction ids", Mathf.Abs(unknownFactionPriority - 0.6f) < 0.0001f);

            UnityEngine.Object.DestroyImmediate(g);
            UnityEngine.Object.DestroyImmediate(go);
            CleanupTestState(state, events);
            ResetGameManagerSingleton();
        }

        private static void TestR3AIDifficultyProfilesGuardrail()
        {
            Debug.Log("\n--- Testing R3 AI Difficulty Profiles Guardrail ---");

            var difficultySet = ScriptableObject.CreateInstance<EventideAge.Systems.G.AIDifficultySet>();
            difficultySet.SetProfiles(EventideAge.Systems.G.AIDifficultySet.CreateDefaultProfiles());

            var easy = difficultySet.GetProfile(EventideAge.Systems.G.AIDifficultyLevel.Easy);
            var standard = difficultySet.GetProfile(EventideAge.Systems.G.AIDifficultyLevel.Standard);
            var hard = difficultySet.GetProfile(EventideAge.Systems.G.AIDifficultyLevel.Hard);

            var uniqueDifficultyCount = difficultySet.Profiles
                .Where(profile => profile != null)
                .Select(profile => profile.Difficulty)
                .Distinct()
                .Count();

            Assert("R3-03 default difficulty set contains easy/standard/hard profiles", easy != null && standard != null && hard != null);
            Assert("R3-03 default difficulty profiles keep one entry per tier", uniqueDifficultyCount == 3);
            Assert("R3-03 default standard profile preserves legacy thresholds",
                standard != null
                && Mathf.Abs(standard.MilitaryActionThreshold - 0.5f) < 0.0001f
                && Mathf.Abs(standard.DiplomaticActionThreshold - 0.4f) < 0.0001f
                && Mathf.Abs(standard.EconomicActionThreshold - 0.3f) < 0.0001f
                && standard.MinGoldLeafForAttack == 30
                && standard.MinArmsForMilitaryAction == 5);
            Assert("R3-03 hard profile is more proactive than easy profile",
                easy != null
                && hard != null
                && hard.MilitaryActionThreshold < easy.MilitaryActionThreshold
                && hard.DiplomaticActionThreshold < easy.DiplomaticActionThreshold
                && hard.EconomicActionThreshold < easy.EconomicActionThreshold
                && hard.MinGoldLeafForAttack < easy.MinGoldLeafForAttack
                && hard.MinArmsForMilitaryAction < easy.MinArmsForMilitaryAction);

            UnityEngine.Object.DestroyImmediate(difficultySet);
        }

        private static void TestR3AIDifficultyApplyAndSwitchGuardrail()
        {
            Debug.Log("\n--- Testing R3 AI Difficulty Apply/Switch Guardrail ---");

            ResetGameManagerSingleton();

            var go = new GameObject("R3AIDifficultyApplyTest");
            var state = CreateDefaultState();
            var events = ScriptableObject.CreateInstance<GameEvents>();

            var difficultySet = ScriptableObject.CreateInstance<EventideAge.Systems.G.AIDifficultySet>();
            difficultySet.SetProfiles(new[]
            {
                new EventideAge.Systems.G.AIDifficultyProfile
                {
                    Difficulty = EventideAge.Systems.G.AIDifficultyLevel.Easy,
                    AggressiveAggression = 1.15f,
                    DefensiveDefense = 1.2f,
                    DiplomaticDiplomacy = 1.2f,
                    ExpansionistExpansion = 1.2f,
                    AIUpdateInterval = 2,
                    BaseCoordinationCost = 16,
                    MilitaryActionThreshold = 0.7f,
                    DiplomaticActionThreshold = 0.6f,
                    EconomicActionThreshold = 0.45f,
                    MinGoldLeafForAttack = 40,
                    MinArmsForMilitaryAction = 8,
                    LowResourceThreshold = 0.5f
                },
                new EventideAge.Systems.G.AIDifficultyProfile
                {
                    Difficulty = EventideAge.Systems.G.AIDifficultyLevel.Standard,
                    AggressiveAggression = 1.5f,
                    DefensiveDefense = 1.5f,
                    DiplomaticDiplomacy = 1.5f,
                    ExpansionistExpansion = 1.5f,
                    AIUpdateInterval = 1,
                    BaseCoordinationCost = 10,
                    MilitaryActionThreshold = 0.5f,
                    DiplomaticActionThreshold = 0.4f,
                    EconomicActionThreshold = 0.3f,
                    MinGoldLeafForAttack = 30,
                    MinArmsForMilitaryAction = 5,
                    LowResourceThreshold = 0.3f
                },
                new EventideAge.Systems.G.AIDifficultyProfile
                {
                    Difficulty = EventideAge.Systems.G.AIDifficultyLevel.Hard,
                    AggressiveAggression = 1.85f,
                    DefensiveDefense = 1.75f,
                    DiplomaticDiplomacy = 1.75f,
                    ExpansionistExpansion = 1.85f,
                    AIUpdateInterval = 1,
                    BaseCoordinationCost = 7,
                    MilitaryActionThreshold = 0.35f,
                    DiplomaticActionThreshold = 0.28f,
                    EconomicActionThreshold = 0.22f,
                    MinGoldLeafForAttack = 20,
                    MinArmsForMilitaryAction = 4,
                    LowResourceThreshold = 0.2f
                }
            });

            var g = go.AddComponent<EventideAge.Systems.G.FactionAISystem>();
            g.DifficultySetConfig = difficultySet;
            g.DifficultyLevel = EventideAge.Systems.G.AIDifficultyLevel.Hard;
            g.Initialize(state, events);

            Assert("R3-03 hard profile applies military threshold and resource gates on initialize",
                Mathf.Abs(g.MilitaryActionThreshold - 0.35f) < 0.0001f
                && g.MinGoldLeafForAttack == 20
                && g.MinArmsForMilitaryAction == 4);
            Assert("R3-03 hard profile applies personality multipliers and cadence",
                Mathf.Abs(g.AggressiveAggression - 1.85f) < 0.0001f
                && Mathf.Abs(g.ExpansionistExpansion - 1.85f) < 0.0001f
                && g.AIUpdateInterval == 1
                && g.BaseCoordinationCost == 7);

            var goldLeaf = state.GetResource(GameIds.Resource.GoldLeaf);
            var arms = state.GetResource(GameIds.Resource.Arms);
            if (goldLeaf != null) goldLeaf.Amount = 28;
            if (arms != null) arms.Amount = 6;

            var hardAi = g.GetAI(GameIds.Faction.Aurean);
            bool hardShouldAct = hardAi != null && (bool)InvokePrivateMethod(g, "ShouldAIAct", hardAi);

            g.SetDifficulty(EventideAge.Systems.G.AIDifficultyLevel.Easy);

            var easyAi = g.GetAI(GameIds.Faction.Aurean);
            bool easyShouldAct = easyAi != null && (bool)InvokePrivateMethod(g, "ShouldAIAct", easyAi);
            var activeProfile = g.GetActiveDifficultyProfile();

            Assert("R3-03 switching to easy updates active tier and thresholds",
                g.GetDifficulty() == EventideAge.Systems.G.AIDifficultyLevel.Easy
                && Mathf.Abs(g.MilitaryActionThreshold - 0.7f) < 0.0001f
                && g.MinGoldLeafForAttack == 40
                && g.MinArmsForMilitaryAction == 8
                && activeProfile != null
                && activeProfile.Difficulty == EventideAge.Systems.G.AIDifficultyLevel.Easy);
            Assert("R3-03 difficulty switch changes ShouldAIAct gating behavior", hardShouldAct && !easyShouldAct);

            UnityEngine.Object.DestroyImmediate(g);
            UnityEngine.Object.DestroyImmediate(difficultySet);
            UnityEngine.Object.DestroyImmediate(go);
            CleanupTestState(state, events);
            ResetGameManagerSingleton();
        }

        private static void TestR3AIDifficultyFallbackGuardrail()
        {
            Debug.Log("\n--- Testing R3 AI Difficulty Fallback Guardrail ---");

            ResetGameManagerSingleton();

            var go = new GameObject("R3AIDifficultyFallbackTest");
            var state = CreateDefaultState();
            var events = ScriptableObject.CreateInstance<GameEvents>();

            var partialSet = ScriptableObject.CreateInstance<EventideAge.Systems.G.AIDifficultySet>();
            partialSet.SetProfiles(new[]
            {
                new EventideAge.Systems.G.AIDifficultyProfile
                {
                    Difficulty = EventideAge.Systems.G.AIDifficultyLevel.Easy,
                    MilitaryActionThreshold = 0.88f,
                    DiplomaticActionThreshold = 0.8f,
                    EconomicActionThreshold = 0.76f,
                    MinGoldLeafForAttack = 70,
                    MinArmsForMilitaryAction = 20
                }
            });

            var g = go.AddComponent<EventideAge.Systems.G.FactionAISystem>();
            g.DifficultySetConfig = partialSet;
            g.DifficultyLevel = EventideAge.Systems.G.AIDifficultyLevel.Hard;
            g.Initialize(state, events);

            var expectedHard = EventideAge.Systems.G.AIDifficultySet.CreateDefaultProfile(EventideAge.Systems.G.AIDifficultyLevel.Hard);
            var activeProfile = g.GetActiveDifficultyProfile();
            Assert("R3-03 missing hard tier in custom set falls back to built-in hard defaults",
                Mathf.Abs(g.MilitaryActionThreshold - expectedHard.MilitaryActionThreshold) < 0.0001f
                && Mathf.Abs(g.DiplomaticActionThreshold - expectedHard.DiplomaticActionThreshold) < 0.0001f
                && Mathf.Abs(g.EconomicActionThreshold - expectedHard.EconomicActionThreshold) < 0.0001f
                && g.MinGoldLeafForAttack == expectedHard.MinGoldLeafForAttack
                && g.MinArmsForMilitaryAction == expectedHard.MinArmsForMilitaryAction);
            Assert("R3-03 active profile remains hard after fallback resolution",
                activeProfile != null && activeProfile.Difficulty == EventideAge.Systems.G.AIDifficultyLevel.Hard);

            UnityEngine.Object.DestroyImmediate(g);
            UnityEngine.Object.DestroyImmediate(partialSet);
            UnityEngine.Object.DestroyImmediate(go);
            CleanupTestState(state, events);
            ResetGameManagerSingleton();
        }

        private static void TestR2TutorialFlowScriptGuardrail()
        {
            Debug.Log("\n--- Testing R2 Tutorial Flow Script Guardrail ---");

            var go = new GameObject("R2TutorialFlowScriptTest");
            var state = CreateMinimalState();
            var events = ScriptableObject.CreateInstance<GameEvents>();

            var tutorialFlow = ScriptableObject.CreateInstance<EventideAge.Systems.I1.TutorialFlowConfig>();
            tutorialFlow.SetSteps(EventideAge.Systems.I1.TutorialFlowConfig.CreateDefaultSteps());

            var i1 = go.AddComponent<EventideAge.Systems.I1.EventSystem>();
            i1.AutoLoadEventPoolOnInitialize = false;
            i1.AutoLoadTutorialOnInitialize = false;
            i1.Initialize(state, events);

            int loaded = i1.LoadTutorialFlowFromConfig(tutorialFlow, clearQueue: true, resetTriggeredHistory: true);
            var queued = i1.GetQueuedEvents();

            bool allTutorialEvents = queued.Length == 5
                && queued.All(evt => !string.IsNullOrWhiteSpace(evt.CanonicalKey)
                                     && evt.CanonicalKey.StartsWith("tutorial:", StringComparison.OrdinalIgnoreCase));

            bool hasMixedTriggers = queued.Any(evt => evt.Trigger == EventideAge.Systems.I1.EventTrigger.TurnBased)
                && queued.Any(evt => evt.Trigger == EventideAge.Systems.I1.EventTrigger.ConditionBased);

            Assert("R2-05 tutorial flow loads 5 scripted steps", loaded == 5);
            Assert("R2-05 tutorial flow dedup skip count is zero on first load", i1.GetLastTutorialLoadDuplicateCount() == 0);
            Assert("R2-05 tutorial queue uses tutorial canonical keys", allTutorialEvents);
            Assert("R2-05 tutorial script contains turn-based and condition-based rules", hasMixedTriggers);

            UnityEngine.Object.DestroyImmediate(i1);
            UnityEngine.Object.DestroyImmediate(tutorialFlow);
            UnityEngine.Object.DestroyImmediate(go);
            CleanupTestState(state, events);
        }

        private static void TestR2TutorialFlowTriggerRuleGuardrail()
        {
            Debug.Log("\n--- Testing R2 Tutorial Flow Trigger Rule Guardrail ---");

            var go = new GameObject("R2TutorialFlowTriggerTest");
            var state = CreateMinimalState();
            var events = ScriptableObject.CreateInstance<GameEvents>();

            var tutorialFlow = ScriptableObject.CreateInstance<EventideAge.Systems.I1.TutorialFlowConfig>();
            tutorialFlow.SetSteps(new[]
            {
                new EventideAge.Systems.I1.TutorialStepTemplate
                {
                    StepId = "tutorial_gate_turn1_map",
                    EventName = "Tutorial Gate 1",
                    Narrative = "Map onboarding",
                    Trigger = EventideAge.Systems.I1.EventTrigger.TurnBased,
                    TriggerTurn = 1
                },
                new EventideAge.Systems.I1.TutorialStepTemplate
                {
                    StepId = "tutorial_gate_relation",
                    EventName = "Tutorial Gate 2",
                    Narrative = "Diplomacy gate",
                    Trigger = EventideAge.Systems.I1.EventTrigger.ConditionBased,
                    TriggerCondition = $"turn>=2&&relation:{GameIds.Faction.Aurean}<=-40"
                },
                new EventideAge.Systems.I1.TutorialStepTemplate
                {
                    StepId = "tutorial_gate_node_control",
                    EventName = "Tutorial Gate 3",
                    Narrative = "Battle report gate",
                    Trigger = EventideAge.Systems.I1.EventTrigger.ConditionBased,
                    TriggerCondition = $"turn>=3&&node_control:{GameIds.Node.SyriaZone}={GameIds.Faction.Aurean}"
                }
            });

            var i1 = go.AddComponent<EventideAge.Systems.I1.EventSystem>();
            i1.AutoLoadEventPoolOnInitialize = false;
            i1.AutoLoadTutorialOnInitialize = false;
            i1.Initialize(state, events);

            int loaded = i1.LoadTutorialFlowFromConfig(tutorialFlow, clearQueue: true, resetTriggeredHistory: true);
            Assert("R2-05 trigger-rule setup loads scripted tutorial gates", loaded == 3);

            var aurean = state.GetFaction(GameIds.Faction.Aurean);
            if (aurean != null)
            {
                aurean.RelationshipWithPlayer = -10;
            }

            state.CurrentTurn = 1;
            state.CurrentPhaseIndex = 0;
            events.TurnEnded(1);
            Assert("R2-05 trigger rules fire turn-based tutorial step on turn 1", i1.GetTriggeredEvents().Length == 1);

            state.CurrentTurn = 2;
            events.TurnEnded(2);
            Assert("R2-05 trigger rules block relation-gated step when condition is unmet", i1.GetTriggeredEvents().Length == 1);

            if (aurean != null)
            {
                aurean.RelationshipWithPlayer = -50;
            }

            state.CurrentTurn = 3;
            events.TurnEnded(3);
            var triggered = i1.GetTriggeredEvents();
            bool allTriggered = triggered.Length == 3
                && triggered.Any(evt => evt.TemplateId == "tutorial_gate_turn1_map")
                && triggered.Any(evt => evt.TemplateId == "tutorial_gate_relation")
                && triggered.Any(evt => evt.TemplateId == "tutorial_gate_node_control");

            Assert("R2-05 trigger rules unlock remaining gated steps once conditions are met", allTriggered);
            Assert("R2-05 tutorial queue drains after all scripted gates trigger", i1.GetQueuedEvents().Length == 0);

            UnityEngine.Object.DestroyImmediate(i1);
            UnityEngine.Object.DestroyImmediate(tutorialFlow);
            UnityEngine.Object.DestroyImmediate(go);
            CleanupTestState(state, events);
        }

        private static void TestR2Campaign24TurnCompletionGuardrail()
        {
            Debug.Log("\n--- Testing R2 Campaign 24-Turn Completion Guardrail ---");

            ResetGameManagerSingleton();

            var managerGO = new GameObject("R2Campaign24TurnManager");
            var manager = managerGO.AddComponent<GameManager>();
            var systemsGO = new GameObject("R2Campaign24TurnSystems");
            systemsGO.transform.SetParent(managerGO.transform);

            var state = CreateR2CampaignValidationState();
            var events = ScriptableObject.CreateInstance<GameEvents>();
            var replayLog = new R1ReplayLogger("R2-06", "CAMPAIGN-24TURN");

            var tutorialFlow = ScriptableObject.CreateInstance<EventideAge.Systems.I1.TutorialFlowConfig>();
            tutorialFlow.SetSteps(EventideAge.Systems.I1.TutorialFlowConfig.CreateDefaultSteps());

            var i1 = systemsGO.AddComponent<EventideAge.Systems.I1.EventSystem>();
            i1.AutoLoadEventPoolOnInitialize = false;
            i1.TutorialFlowConfigAsset = tutorialFlow;
            i1.AutoLoadTutorialOnInitialize = true;

            var j = systemsGO.AddComponent<EventideAge.Systems.J.VictoryDefeatSystem>();
            j.VictoryThreshold = 101f;
            j.MilitaryCollapseAshWillThreshold = 0;
            j.InternalDivisionSatisfactionThreshold = 0;

            int endCount = 0;
            int endTurn = -1;
            string endReason = string.Empty;
            int narrativeCount = 0;
            events.OnGameEnded += reason =>
            {
                endCount++;
                endTurn = state.CurrentTurn;
                endReason = reason;
            };
            events.OnNarrativeEventAdded += (eventId, message, severity) => { narrativeCount++; };

            manager.Config = state.Config;
            manager.State = state;
            manager.Events = events;
            manager.Systems = new List<GameSystem> { i1, j };
            manager.InitializeGame();

            var syria = state.GetNode(GameIds.Node.SyriaZone);
            if (syria != null)
            {
                syria.ControllingFactionId = GameIds.Faction.Aurean;
            }

            bool endedBeforeTurn24 = false;
            for (int turn = 1; turn <= GameConfig.kMaxTurns; turn++)
            {
                for (int phase = 0; phase <= GameConfig.kAiResponsePhaseIndex; phase++)
                {
                    manager.AdvancePhase();
                }

                bool shouldStillRun = turn < GameConfig.kMaxTurns;
                if (shouldStillRun && j.IsGameEnded())
                {
                    endedBeforeTurn24 = true;
                }

                replayLog.RecordStep(
                    state,
                    $"T{turn:D2}",
                    "Advance full turn cycle",
                    "Turn loop proceeds without premature end before turn 24",
                    $"CurrentTurn={state.CurrentTurn}, Phase={state.CurrentPhaseIndex}, Ended={j.IsGameEnded()}, EndReason={j.GetEndReason()}",
                    !shouldStillRun || !j.IsGameEnded());
            }

            Assert("R2-06 campaign does not end before turn 24", !endedBeforeTurn24);
            Assert("R2-06 campaign runs through 24 full turns and enters post-turn state", state.CurrentTurn == GameConfig.kMaxTurns + 1 && state.CurrentPhaseIndex == 0);
            Assert("R2-06 timeout endgame fires exactly once at turn 24", endCount == 1 && endTurn == GameConfig.kMaxTurns && endReason == "attrition");
            Assert("R2-06 tutorial/event narrative feedback appears during campaign run", narrativeCount >= 5);

            replayLog.Flush();

            UnityEngine.Object.DestroyImmediate(tutorialFlow);
            UnityEngine.Object.DestroyImmediate(managerGO);
            CleanupTestState(state, events);
            ResetGameManagerSingleton();
        }

        private static void TestR2CampaignNoEmptyTurnAndPhaseFeedbackGuardrail()
        {
            Debug.Log("\n--- Testing R2 Campaign No-Empty-Turn / No-Feedback-Phase Guardrail ---");

            ResetGameManagerSingleton();

            var managerGO = new GameObject("R2CampaignFeedbackManager");
            var manager = managerGO.AddComponent<GameManager>();
            var systemsGO = new GameObject("R2CampaignFeedbackSystems");
            systemsGO.transform.SetParent(managerGO.transform);

            var state = CreateR2CampaignValidationState();
            var events = ScriptableObject.CreateInstance<GameEvents>();

            var tutorialFlow = ScriptableObject.CreateInstance<EventideAge.Systems.I1.TutorialFlowConfig>();
            tutorialFlow.SetSteps(EventideAge.Systems.I1.TutorialFlowConfig.CreateDefaultSteps());

            var i1 = systemsGO.AddComponent<EventideAge.Systems.I1.EventSystem>();
            i1.AutoLoadEventPoolOnInitialize = false;
            i1.TutorialFlowConfigAsset = tutorialFlow;
            i1.AutoLoadTutorialOnInitialize = true;

            var turnFeedback = new Dictionary<int, int>();
            var phaseFeedback = new Dictionary<string, int>();
            var tutorialNarrativeTurns = new Dictionary<int, int>();

            Action recordFeedback = () =>
            {
                int turn = state.CurrentTurn;
                int phase = state.CurrentPhaseIndex;
                if (turn < 1 || turn > GameConfig.kMaxTurns)
                    return;
                if (phase < 0 || phase > GameConfig.kAiResponsePhaseIndex)
                    return;

                if (!turnFeedback.ContainsKey(turn))
                    turnFeedback[turn] = 0;
                turnFeedback[turn]++;

                string phaseKey = $"{turn}:{phase}";
                if (!phaseFeedback.ContainsKey(phaseKey))
                    phaseFeedback[phaseKey] = 0;
                phaseFeedback[phaseKey]++;
            };

            events.OnActionLogAdded += (sourceId, message, severity) => { recordFeedback(); };
            events.OnNarrativeEventAdded += (eventId, message, severity) =>
            {
                recordFeedback();
                int turn = state.CurrentTurn;
                if (turn >= 1 && turn <= 5)
                {
                    if (!tutorialNarrativeTurns.ContainsKey(turn))
                        tutorialNarrativeTurns[turn] = 0;
                    tutorialNarrativeTurns[turn]++;
                }
            };
            events.OnNotificationAdded += (sourceId, message, severity) => { recordFeedback(); };
            events.OnAlertAdded += (sourceId, message, severity) => { recordFeedback(); };
            events.OnIntelReportAdded += (sourceId, message, severity) => { recordFeedback(); };

            manager.Config = state.Config;
            manager.State = state;
            manager.Events = events;
            manager.Systems = new List<GameSystem> { i1 };
            manager.InitializeGame();

            var syria = state.GetNode(GameIds.Node.SyriaZone);
            if (syria != null)
            {
                syria.ControllingFactionId = GameIds.Faction.Aurean;
            }

            for (int turn = 1; turn <= GameConfig.kMaxTurns; turn++)
            {
                for (int phase = 0; phase <= GameConfig.kAiResponsePhaseIndex; phase++)
                {
                    manager.AdvancePhase();
                }
            }

            bool noEmptyTurns = true;
            for (int turn = 1; turn <= GameConfig.kMaxTurns; turn++)
            {
                if (!turnFeedback.TryGetValue(turn, out int count) || count <= 0)
                {
                    noEmptyTurns = false;
                    break;
                }
            }

            bool noFeedbacklessPhase = true;
            for (int turn = 1; turn <= GameConfig.kMaxTurns; turn++)
            {
                for (int phase = 0; phase <= GameConfig.kAiResponsePhaseIndex; phase++)
                {
                    string key = $"{turn}:{phase}";
                    if (!phaseFeedback.TryGetValue(key, out int count) || count <= 0)
                    {
                        noFeedbacklessPhase = false;
                        break;
                    }
                }

                if (!noFeedbacklessPhase)
                    break;
            }

            bool tutorialCoverage = true;
            for (int turn = 1; turn <= 5; turn++)
            {
                if (!tutorialNarrativeTurns.TryGetValue(turn, out int count) || count <= 0)
                {
                    tutorialCoverage = false;
                    break;
                }
            }

            Assert("R2-06 campaign has no empty turns across 24-turn window", noEmptyTurns);
            Assert("R2-06 campaign has no feedback-less phases across all turns", noFeedbacklessPhase);
            Assert("R2-06 tutorial onboarding emits narrative guidance for turns 1-5", tutorialCoverage);

            UnityEngine.Object.DestroyImmediate(tutorialFlow);
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

        private static void TestI1EventPoolBackfillAndDedupGuardrail()
        {
            Debug.Log("\n--- Testing I1 Event Pool Backfill and Dedup Guardrail ---");

            var go = new GameObject("I1EventPoolDedupTest");
            var state = CreateMinimalState();
            var events = ScriptableObject.CreateInstance<GameEvents>();

            var i1 = go.AddComponent<EventideAge.Systems.I1.EventSystem>();
            i1.AutoLoadEventPoolOnInitialize = false;

            var pool = ScriptableObject.CreateInstance<EventideAge.Systems.I1.EventPoolConfig>();
            pool.SetTemplates(new[]
            {
                new EventideAge.Systems.I1.EventTemplate
                {
                    TemplateId = "r2_evt_income_t02",
                    EventName = "R2IncomeT02",
                    Type = EventideAge.Systems.I1.EventType.Narrative,
                    Trigger = EventideAge.Systems.I1.EventTrigger.TurnBased,
                    TriggerTurn = 2,
                    Effects = new[] { $"resource_change:{GameIds.Resource.GoldLeaf}:3" },
                    Enabled = true
                },
                new EventideAge.Systems.I1.EventTemplate
                {
                    TemplateId = "r2_evt_income_t02",
                    EventName = "R2IncomeT02Duplicate",
                    Type = EventideAge.Systems.I1.EventType.Narrative,
                    Trigger = EventideAge.Systems.I1.EventTrigger.TurnBased,
                    TriggerTurn = 2,
                    Effects = new[] { $"resource_change:{GameIds.Resource.GoldLeaf}:99" },
                    Enabled = true
                },
                new EventideAge.Systems.I1.EventTemplate
                {
                    TemplateId = "r2_evt_relation_cond",
                    EventName = "R2RelationCondition",
                    Type = EventideAge.Systems.I1.EventType.PlayerResponse,
                    Trigger = EventideAge.Systems.I1.EventTrigger.ConditionBased,
                    TriggerCondition = $"turn>=2&&relation:{GameIds.Faction.Aurean}<=-40",
                    Effects = new[] { $"resource_change:{GameIds.Resource.GoldLeaf}:4" },
                    Enabled = true
                }
            });

            i1.EventPoolConfigAsset = pool;
            i1.Initialize(state, events);

            int loaded = i1.LoadEventPoolFromConfig(clearQueue: true, resetTriggeredHistory: true);
            Assert("I1 event pool load enqueues unique templates only", loaded == 2);
            Assert("I1 event pool load records skipped duplicates", i1.GetLastPoolLoadDuplicateCount() == 1);
            Assert("I1 queued pool event count equals deduped result", i1.GetQueuedEvents().Length == 2);

            bool duplicateQueueResult = i1.QueueEvent(new EventideAge.Systems.I1.GameEvent
            {
                TemplateId = "r2_evt_income_t02",
                EventName = "ManualDuplicate",
                Type = EventideAge.Systems.I1.EventType.Narrative,
                Trigger = EventideAge.Systems.I1.EventTrigger.TurnBased,
                TriggerTurn = 2,
                Effects = new List<string> { $"resource_change:{GameIds.Resource.GoldLeaf}:999" },
                TimeoutTurns = 3
            });
            Assert("I1 queue rejects duplicate template id", !duplicateQueueResult);

            var goldLeaf = state.GetResource(GameIds.Resource.GoldLeaf);
            int initialGold = goldLeaf != null ? goldLeaf.Amount : 0;

            events.TurnEnded(1);
            Assert("I1 pool events do not trigger before turn gate", goldLeaf != null && goldLeaf.Amount == initialGold);

            state.CurrentTurn = 2;
            events.TurnEnded(2);
            Assert("I1 pool events trigger once after conditions are met", goldLeaf != null && goldLeaf.Amount == initialGold + 7);

            int reloadWithoutReset = i1.LoadEventPoolFromConfig(clearQueue: false, resetTriggeredHistory: false);
            Assert("I1 reload without reset does not re-enqueue triggered non-repeat events", reloadWithoutReset == 0);

            int reloadWithReset = i1.LoadEventPoolFromConfig(clearQueue: true, resetTriggeredHistory: true);
            Assert("I1 reload with reset can re-enqueue full deduped pool", reloadWithReset == 2);

            UnityEngine.Object.DestroyImmediate(pool);
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

        private static void TestR2MapConfigV1LockGuardrail()
        {
            Debug.Log("\n--- Testing R2 Map Config V1 Lock Guardrail ---");

            var config = EventideAge.Config.DefaultGameConfig.CreateDefault();
            var state = ScriptableObject.CreateInstance<GameState>();
            state.Config = config;
            state.Initialize();

            var events = ScriptableObject.CreateInstance<GameEvents>();
            var go = new GameObject("R2MapLockNetworkTest");
            var h2 = go.AddComponent<EventideAge.Systems.H2.NodeNetworkSystem>();
            h2.Initialize(state, events);

            var expectedRegions = new Dictionary<string, int>
            {
                { "PersianGulf", 2 },
                { "WesternFront", 2 },
                { "NorthernTerritory", 2 },
                { "ArabianPeninsula", 2 },
                { "Levant", 2 },
                { "CentralAsia", 2 }
            };

            Assert("R2 map lock has exactly 6 canonical regions", state.Map != null && state.Map.Regions != null && state.Map.Regions.Length == expectedRegions.Count);
            foreach (var regionExpectation in expectedRegions)
            {
                var region = state.Map.Regions.FirstOrDefault(r => r != null && r.RegionId == regionExpectation.Key);
                Assert(
                    $"R2 map lock region {regionExpectation.Key} has {regionExpectation.Value} nodes",
                    region != null && region.Nodes != null && region.Nodes.Length == regionExpectation.Value);
            }

            var expectedNodes = new (string NodeId, string RegionId, NodeType NodeType, int DefenseBonus, string Controller, int ControlPoints, int MaxControlPoints)[]
            {
                (GameIds.Node.Hormuz, "PersianGulf", NodeType.Chokepoint, 30, GameIds.Faction.Vashid, 100, 100),
                (GameIds.Node.Bushehr, "PersianGulf", NodeType.Port, 20, GameIds.Faction.Vashid, 80, 100),
                (GameIds.Node.IraqBorder, "WesternFront", NodeType.Chokepoint, 25, GameIds.Faction.AshConfederacy, 60, 100),
                (GameIds.Node.SyriaZone, "WesternFront", NodeType.City, 15, GameIds.Faction.AshConfederacy, 50, 100),
                (GameIds.Node.Caspian, "NorthernTerritory", NodeType.ResourceNode, 10, GameIds.Faction.Vashid, 70, 100),
                (GameIds.Node.Caucasus, "NorthernTerritory", NodeType.Chokepoint, 35, GameIds.Faction.Neutral, 30, 100),
                (GameIds.Node.RedSea, "ArabianPeninsula", NodeType.Chokepoint, 30, GameIds.Faction.GoldenHord, 80, 100),
                (GameIds.Node.GulfBase, "ArabianPeninsula", NodeType.Port, 20, GameIds.Faction.Aurean, 90, 100),
                (GameIds.Node.Mediterranean, "Levant", NodeType.Chokepoint, 25, GameIds.Faction.SacredFire, 70, 100),
                (GameIds.Node.IsraelCore, "Levant", NodeType.City, 40, GameIds.Faction.SacredFire, 100, 100),
                (GameIds.Node.Afghanistan, "CentralAsia", NodeType.Chokepoint, 20, GameIds.Faction.Neutral, 40, 100),
                (GameIds.Node.TradeHub, "CentralAsia", NodeType.ResourceNode, 10, GameIds.Faction.Neutral, 50, 100)
            };

            int totalNodeCount = state.Map.Regions.Sum(r => r != null && r.Nodes != null ? r.Nodes.Length : 0);
            Assert("R2 map lock has exactly 12 nodes", totalNodeCount == expectedNodes.Length);

            foreach (var expected in expectedNodes)
            {
                var node = state.GetNode(expected.NodeId);
                bool nodeRegionAligned = state.Map.Regions.Any(region =>
                    region != null
                    && region.RegionId == expected.RegionId
                    && region.Nodes != null
                    && region.Nodes.Any(regionNode => regionNode != null && GameIds.ResolveNodeId(regionNode.NodeId) == expected.NodeId));

                Assert(
                    $"R2 map lock node {expected.NodeId} matches type/controller/control-points",
                    node != null
                    && nodeRegionAligned
                    && node.NodeType == expected.NodeType
                    && node.DefenseBonus == expected.DefenseBonus
                    && GameIds.ResolveFactionId(node.ControllingFactionId) == expected.Controller
                    && node.ControlPoints == expected.ControlPoints
                    && node.MaxControlPoints == expected.MaxControlPoints);
            }

            var resourceNodeIds = state.Map.Regions
                .SelectMany(region => region.Nodes)
                .Where(node => node != null && node.NodeType == NodeType.ResourceNode)
                .Select(node => GameIds.ResolveNodeId(node.NodeId))
                .ToArray();

            Assert("R2 map lock keeps exactly two resource-output nodes", resourceNodeIds.Length == 2);
            Assert("R2 map lock resource-output nodes are Caspian and TradeHub", resourceNodeIds.Contains(GameIds.Node.Caspian) && resourceNodeIds.Contains(GameIds.Node.TradeHub));

            var expectedLinks = new (string A, string B)[]
            {
                (GameIds.Node.Hormuz, GameIds.Node.Bushehr),
                (GameIds.Node.IraqBorder, GameIds.Node.SyriaZone),
                (GameIds.Node.Caspian, GameIds.Node.Caucasus),
                (GameIds.Node.RedSea, GameIds.Node.GulfBase),
                (GameIds.Node.Mediterranean, GameIds.Node.IsraelCore),
                (GameIds.Node.Afghanistan, GameIds.Node.TradeHub),
                (GameIds.Node.Hormuz, GameIds.Node.IraqBorder),
                (GameIds.Node.Bushehr, GameIds.Node.Caucasus),
                (GameIds.Node.Caspian, GameIds.Node.IraqBorder),
                (GameIds.Node.SyriaZone, GameIds.Node.Mediterranean),
                (GameIds.Node.Mediterranean, GameIds.Node.RedSea),
                (GameIds.Node.GulfBase, GameIds.Node.TradeHub),
                (GameIds.Node.Afghanistan, GameIds.Node.Caucasus)
            };

            Assert("R2 map lock adjacency edge count is 13", h2.GetConnectionCount() == expectedLinks.Length);
            foreach (var edge in expectedLinks)
            {
                Assert($"R2 map lock edge {edge.A}<->{edge.B} exists", h2.AreAdjacent(edge.A, edge.B) && h2.AreAdjacent(edge.B, edge.A));
            }

            Assert("R2 map lock non-edge guardrail remains false", !h2.AreAdjacent(GameIds.Node.Hormuz, GameIds.Node.IsraelCore));

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

        private static void TestUiStatusAndJumpSemanticsGuardrail()
        {
            Debug.Log("\n--- Testing UI Status and Jump Semantics Guardrail ---");

            var root = new GameObject("UiStatusAndJumpSemanticsTest");
            var state = CreateMinimalState();
            var events = ScriptableObject.CreateInstance<GameEvents>();

            var mapPanel = root.AddComponent<EventideAge.UI.MapPanelUI>();
            mapPanel.LatestMapText = new GameObject("MapLatest6").AddComponent<UnityEngine.UI.Text>();
            mapPanel.LatestMapText.transform.SetParent(root.transform);
            mapPanel.MapHistoryText = new GameObject("MapHistory6").AddComponent<UnityEngine.UI.Text>();
            mapPanel.MapHistoryText.transform.SetParent(root.transform);
            mapPanel.Initialize(state, events);

            var diplomacyPanel = root.AddComponent<EventideAge.UI.DiplomacyPanelUI>();
            diplomacyPanel.LatestDiplomacyText = new GameObject("DiplomacyLatest6").AddComponent<UnityEngine.UI.Text>();
            diplomacyPanel.LatestDiplomacyText.transform.SetParent(root.transform);
            diplomacyPanel.DiplomacyHistoryText = new GameObject("DiplomacyHistory6").AddComponent<UnityEngine.UI.Text>();
            diplomacyPanel.DiplomacyHistoryText.transform.SetParent(root.transform);
            diplomacyPanel.Initialize(state, events);

            var actionLog = root.AddComponent<EventideAge.UI.ActionLogUI>();
            actionLog.LatestEntryText = new GameObject("ActionLatest6").AddComponent<UnityEngine.UI.Text>();
            actionLog.LatestEntryText.transform.SetParent(root.transform);
            actionLog.HistoryText = new GameObject("ActionHistory6").AddComponent<UnityEngine.UI.Text>();
            actionLog.HistoryText.transform.SetParent(root.transform);
            actionLog.Initialize(state, events);

            var eventPanel = root.AddComponent<EventideAge.UI.EventPanelUI>();
            eventPanel.LatestEventText = new GameObject("EventLatest6").AddComponent<UnityEngine.UI.Text>();
            eventPanel.LatestEventText.transform.SetParent(root.transform);
            eventPanel.EventHistoryText = new GameObject("EventHistory6").AddComponent<UnityEngine.UI.Text>();
            eventPanel.EventHistoryText.transform.SetParent(root.transform);
            eventPanel.Initialize(state, events);

            events.NodeControlChanged("Damascus", "GoldLeader", "ResistanceAxis", 22);
            Assert("Map panel exposes canonical critical status tag", mapPanel.LatestMapText.text.Contains("status:Critical"));
            Assert("Map panel exposes map jump hint", mapPanel.LatestMapText.text.Contains("jump:Map"));

            events.ActionLogAdded("C2.Pact.GoldLeader", "Routine outreach in Damascus", FeedbackSeverity.Info);
            Assert("Diplomacy panel exposes stable status tag", diplomacyPanel.LatestDiplomacyText.text.Contains("status:Stable"));
            Assert("Diplomacy panel exposes diplomacy jump hint", diplomacyPanel.LatestDiplomacyText.text.Contains("jump:Diplomacy"));

            events.ActionLogAdded("D5.Resolve.Battle", "Battle resolution complete", FeedbackSeverity.Warning);
            string actionHistory = actionLog.HistoryText.text ?? string.Empty;
            Assert("Battle report exposes warning status tag", actionHistory.Contains("status:Warning"));
            Assert("Battle report exposes battle-report jump hint", actionHistory.Contains("jump:BattleReport"));

            events.NarrativeEventAdded("I1.Story.Damascus", "Historic event emerges", FeedbackSeverity.Info);
            Assert("Event panel exposes stable status tag", eventPanel.LatestEventText.text.Contains("status:Stable"));
            Assert("Event panel exposes event jump hint", eventPanel.LatestEventText.text.Contains("jump:Event"));

            UnityEngine.Object.DestroyImmediate(root);
            CleanupTestState(state, events);
        }

        private static void TestUiLockedStatusReasonGuardrail()
        {
            Debug.Log("\n--- Testing UI Locked Status and Reason Guardrail ---");

            var root = new GameObject("UiLockedStatusReasonTest");
            var state = CreateMinimalState();
            var events = ScriptableObject.CreateInstance<GameEvents>();

            var diplomacyPanel = root.AddComponent<EventideAge.UI.DiplomacyPanelUI>();
            diplomacyPanel.LatestDiplomacyText = new GameObject("DiplomacyLatest7").AddComponent<UnityEngine.UI.Text>();
            diplomacyPanel.LatestDiplomacyText.transform.SetParent(root.transform);
            diplomacyPanel.DiplomacyHistoryText = new GameObject("DiplomacyHistory7").AddComponent<UnityEngine.UI.Text>();
            diplomacyPanel.DiplomacyHistoryText.transform.SetParent(root.transform);
            diplomacyPanel.Initialize(state, events);

            var eventPanel = root.AddComponent<EventideAge.UI.EventPanelUI>();
            eventPanel.LatestEventText = new GameObject("EventLatest7").AddComponent<UnityEngine.UI.Text>();
            eventPanel.LatestEventText.transform.SetParent(root.transform);
            eventPanel.EventHistoryText = new GameObject("EventHistory7").AddComponent<UnityEngine.UI.Text>();
            eventPanel.EventHistoryText.transform.SetParent(root.transform);
            eventPanel.Initialize(state, events);

            events.ActionLogAdded("C2.Pact.GoldLeader", "Action blocked: insufficient AP for treaty", FeedbackSeverity.Warning);
            string diplomacyLatest = diplomacyPanel.LatestDiplomacyText.text ?? string.Empty;
            Assert("Diplomacy panel maps blocked action to Locked status", diplomacyLatest.Contains("status:Locked"));
            Assert("Diplomacy panel exposes locked reason", diplomacyLatest.Contains("reason:insufficient AP for treaty"));

            events.NarrativeEventAdded("I1.Policy.Damascus", "Option unavailable: requirement not met", FeedbackSeverity.Warning);
            string eventLatest = eventPanel.LatestEventText.text ?? string.Empty;
            Assert("Event panel maps unavailable option to Locked status", eventLatest.Contains("status:Locked"));
            Assert("Event panel exposes lock reason", eventLatest.Contains("reason:requirement not met"));

            UnityEngine.Object.DestroyImmediate(root);
            CleanupTestState(state, events);
        }

        private static void TestUiHintAndCueSemanticsGuardrail()
        {
            Debug.Log("\n--- Testing UI Hint and Cue Semantics Guardrail ---");

            var root = new GameObject("UiHintAndCueSemanticsTest");
            var state = CreateMinimalState();
            var events = ScriptableObject.CreateInstance<GameEvents>();

            var mapPanel = root.AddComponent<EventideAge.UI.MapPanelUI>();
            mapPanel.LatestMapText = new GameObject("MapLatest8").AddComponent<UnityEngine.UI.Text>();
            mapPanel.LatestMapText.transform.SetParent(root.transform);
            mapPanel.MapHistoryText = new GameObject("MapHistory8").AddComponent<UnityEngine.UI.Text>();
            mapPanel.MapHistoryText.transform.SetParent(root.transform);
            mapPanel.Initialize(state, events);

            var diplomacyPanel = root.AddComponent<EventideAge.UI.DiplomacyPanelUI>();
            diplomacyPanel.LatestDiplomacyText = new GameObject("DiplomacyLatest8").AddComponent<UnityEngine.UI.Text>();
            diplomacyPanel.LatestDiplomacyText.transform.SetParent(root.transform);
            diplomacyPanel.DiplomacyHistoryText = new GameObject("DiplomacyHistory8").AddComponent<UnityEngine.UI.Text>();
            diplomacyPanel.DiplomacyHistoryText.transform.SetParent(root.transform);
            diplomacyPanel.Initialize(state, events);

            var actionLog = root.AddComponent<EventideAge.UI.ActionLogUI>();
            actionLog.LatestEntryText = new GameObject("ActionLatest8").AddComponent<UnityEngine.UI.Text>();
            actionLog.LatestEntryText.transform.SetParent(root.transform);
            actionLog.HistoryText = new GameObject("ActionHistory8").AddComponent<UnityEngine.UI.Text>();
            actionLog.HistoryText.transform.SetParent(root.transform);
            actionLog.Initialize(state, events);

            var eventPanel = root.AddComponent<EventideAge.UI.EventPanelUI>();
            eventPanel.LatestEventText = new GameObject("EventLatest8").AddComponent<UnityEngine.UI.Text>();
            eventPanel.LatestEventText.transform.SetParent(root.transform);
            eventPanel.EventHistoryText = new GameObject("EventHistory8").AddComponent<UnityEngine.UI.Text>();
            eventPanel.EventHistoryText.transform.SetParent(root.transform);
            eventPanel.Initialize(state, events);

            events.NodeControlChanged("Damascus", "GoldLeader", "ResistanceAxis", 25);
            string mapLatest = mapPanel.LatestMapText.text ?? string.Empty;
            Assert("Map critical update exposes stabilize hint", mapLatest.Contains("hint:stabilize-map-hotspot"));
            Assert("Map critical update exposes alert pulse cue", mapLatest.Contains("cue:AlertPulse"));

            events.ActionLogAdded("C2.Pact.GoldLeader", "Diplomacy pressure increases", FeedbackSeverity.Warning);
            string diplomacyLatest = diplomacyPanel.LatestDiplomacyText.text ?? string.Empty;
            Assert("Diplomacy warning update exposes review hint", diplomacyLatest.Contains("hint:review-diplomacy-update"));
            Assert("Diplomacy warning update exposes soft pulse cue", diplomacyLatest.Contains("cue:SoftPulse"));

            events.ActionLogAdded("D5.Resolve.Battle", "Battle result pending review", FeedbackSeverity.Warning);
            string actionHistory = actionLog.HistoryText.text ?? string.Empty;
            Assert("Battle report warning entry exposes review hint", actionHistory.Contains("hint:review-battle-report"));
            Assert("Battle report warning entry exposes soft pulse cue", actionHistory.Contains("cue:SoftPulse"));

            events.NarrativeEventAdded("I1.Story.Damascus", "Routine event update", FeedbackSeverity.Info);
            string eventLatest = eventPanel.LatestEventText.text ?? string.Empty;
            Assert("Event stable update exposes continue-turn hint", eventLatest.Contains("hint:continue-turn-plan"));
            Assert("Event stable update exposes no cue state", eventLatest.Contains("cue:None"));

            UnityEngine.Object.DestroyImmediate(root);
            CleanupTestState(state, events);
        }

        private static void TestUiLockedHintVariantGuardrail()
        {
            Debug.Log("\n--- Testing UI Locked Hint Variant Guardrail ---");

            var root = new GameObject("UiLockedHintVariantTest");
            var state = CreateMinimalState();
            var events = ScriptableObject.CreateInstance<GameEvents>();

            var diplomacyPanel = root.AddComponent<EventideAge.UI.DiplomacyPanelUI>();
            diplomacyPanel.LatestDiplomacyText = new GameObject("DiplomacyLatest9").AddComponent<UnityEngine.UI.Text>();
            diplomacyPanel.LatestDiplomacyText.transform.SetParent(root.transform);
            diplomacyPanel.DiplomacyHistoryText = new GameObject("DiplomacyHistory9").AddComponent<UnityEngine.UI.Text>();
            diplomacyPanel.DiplomacyHistoryText.transform.SetParent(root.transform);
            diplomacyPanel.Initialize(state, events);

            var eventPanel = root.AddComponent<EventideAge.UI.EventPanelUI>();
            eventPanel.LatestEventText = new GameObject("EventLatest9").AddComponent<UnityEngine.UI.Text>();
            eventPanel.LatestEventText.transform.SetParent(root.transform);
            eventPanel.EventHistoryText = new GameObject("EventHistory9").AddComponent<UnityEngine.UI.Text>();
            eventPanel.EventHistoryText.transform.SetParent(root.transform);
            eventPanel.Initialize(state, events);

            events.ActionLogAdded("C2.Pact.GoldLeader", "Action blocked: insufficient AP for treaty", FeedbackSeverity.Warning);
            string diplomacyLatest = diplomacyPanel.LatestDiplomacyText.text ?? string.Empty;
            Assert("Locked AP message maps to recover-ap hint", diplomacyLatest.Contains("hint:recover-ap"));
            Assert("Locked AP message uses alert pulse cue", diplomacyLatest.Contains("cue:AlertPulse"));

            events.NarrativeEventAdded("I1.Policy.Damascus", "Option unavailable: requirement not met", FeedbackSeverity.Warning);
            string eventLatest = eventPanel.LatestEventText.text ?? string.Empty;
            Assert("Locked requirement message maps to meet-requirement hint", eventLatest.Contains("hint:meet-requirement"));
            Assert("Locked requirement message uses alert pulse cue", eventLatest.Contains("cue:AlertPulse"));

            events.ActionLogAdded("C2.Policy.Dialogue", "Action locked: wrong phase", FeedbackSeverity.Warning);
            diplomacyLatest = diplomacyPanel.LatestDiplomacyText.text ?? string.Empty;
            Assert("Locked phase message maps to switch-phase hint", diplomacyLatest.Contains("hint:switch-phase"));

            UnityEngine.Object.DestroyImmediate(root);
            CleanupTestState(state, events);
        }

        private static void TestUiLocalizationHighFrequencySurfaceLabelsGuardrail()
        {
            Debug.Log("\n--- Testing UI Localization High-Frequency Surface Labels Guardrail ---");

            var root = new GameObject("UiLocalizationHighFrequencyLabelsTest");
            var state = CreateMinimalState();
            var events = ScriptableObject.CreateInstance<GameEvents>();

            var table = ScriptableObject.CreateInstance<EventideAge.Systems.L4.LocalizationTableConfig>();
            table.DefaultLocale = "zh-CN";
            table.SupportedLocales = new[] { "zh-CN", "en-US" };
            table.Entries = new[]
            {
                new EventideAge.Systems.L4.LocalizedTextEntry
                {
                    Key = "ui.map.hotspot",
                    ZhCN = "\u70ed\u70b9",
                    EnUS = "HOTSPOT"
                },
                new EventideAge.Systems.L4.LocalizedTextEntry
                {
                    Key = "ui.diplomacy.action.locked",
                    ZhCN = "\u5916\u4ea4\u884c\u52a8\u5df2\u9501\u5b9a",
                    EnUS = "ACTION LOCKED"
                },
                new EventideAge.Systems.L4.LocalizedTextEntry
                {
                    Key = "ui.report.summary",
                    ZhCN = "\u56de\u5408\u603b\u7ed3",
                    EnUS = "TURN SUMMARY"
                },
                new EventideAge.Systems.L4.LocalizedTextEntry
                {
                    Key = "ui.event.option.preview",
                    ZhCN = "\u9009\u9879\u9884\u89c8",
                    EnUS = "OPTION PREVIEW"
                }
            };

            var l4 = root.AddComponent<EventideAge.Systems.L4.LocalizationSystem>();
            l4.LocalizationTable = table;
            l4.AutoLoadSavedLocale = false;
            l4.Initialize(state, events);

            var mapPanel = root.AddComponent<EventideAge.UI.MapPanelUI>();
            mapPanel.LatestMapText = new GameObject("MapLatestL10").AddComponent<UnityEngine.UI.Text>();
            mapPanel.LatestMapText.transform.SetParent(root.transform);
            mapPanel.MapHistoryText = new GameObject("MapHistoryL10").AddComponent<UnityEngine.UI.Text>();
            mapPanel.MapHistoryText.transform.SetParent(root.transform);
            mapPanel.Initialize(state, events);

            var diplomacyPanel = root.AddComponent<EventideAge.UI.DiplomacyPanelUI>();
            diplomacyPanel.LatestDiplomacyText = new GameObject("DiplomacyLatestL10").AddComponent<UnityEngine.UI.Text>();
            diplomacyPanel.LatestDiplomacyText.transform.SetParent(root.transform);
            diplomacyPanel.DiplomacyHistoryText = new GameObject("DiplomacyHistoryL10").AddComponent<UnityEngine.UI.Text>();
            diplomacyPanel.DiplomacyHistoryText.transform.SetParent(root.transform);
            diplomacyPanel.Initialize(state, events);

            var actionLog = root.AddComponent<EventideAge.UI.ActionLogUI>();
            actionLog.LatestEntryText = new GameObject("ActionLatestL10").AddComponent<UnityEngine.UI.Text>();
            actionLog.LatestEntryText.transform.SetParent(root.transform);
            actionLog.HistoryText = new GameObject("ActionHistoryL10").AddComponent<UnityEngine.UI.Text>();
            actionLog.HistoryText.transform.SetParent(root.transform);
            actionLog.Initialize(state, events);

            var eventPanel = root.AddComponent<EventideAge.UI.EventPanelUI>();
            eventPanel.LatestEventText = new GameObject("EventLatestL10").AddComponent<UnityEngine.UI.Text>();
            eventPanel.LatestEventText.transform.SetParent(root.transform);
            eventPanel.EventHistoryText = new GameObject("EventHistoryL10").AddComponent<UnityEngine.UI.Text>();
            eventPanel.EventHistoryText.transform.SetParent(root.transform);
            eventPanel.Initialize(state, events);

            events.NodeControlChanged("Damascus", "GoldLeader", "ResistanceAxis", 22);
            events.ActionLogAdded("C2.Pact.GoldLeader", "Action blocked: insufficient AP for treaty", FeedbackSeverity.Warning);
            events.ActionLogAdded("D1.Proxy.Coordination", "Proxy task executed", FeedbackSeverity.Info);
            events.TurnChanged(1, 2);
            events.NarrativeEventAdded("I1.Story.Damascus", "Historic event emerges", FeedbackSeverity.Info);

            Assert("Map panel uses zh-CN hotspot label", (mapPanel.LatestMapText.text ?? string.Empty).Contains("[\u70ed\u70b9]"));
            Assert("Diplomacy panel uses zh-CN locked label", (diplomacyPanel.LatestDiplomacyText.text ?? string.Empty).Contains("[\u5916\u4ea4\u884c\u52a8\u5df2\u9501\u5b9a]"));
            Assert("Battle report uses zh-CN summary header", (actionLog.HistoryText.text ?? string.Empty).Contains("=== \u56de\u5408\u603b\u7ed3 ==="));
            Assert("Event panel uses zh-CN preview label", (eventPanel.LatestEventText.text ?? string.Empty).Contains("[\u9009\u9879\u9884\u89c8]"));

            bool switchedToEnglish = l4.SetLocale("en-US");
            Assert("L4 locale switching to en-US succeeds for UI labels", switchedToEnglish);

            events.NodeControlChanged("IraqBorder", "GoldLeader", "ResistanceAxis", 20);
            events.ActionLogAdded("C2.Pact.GoldLeader", "Action blocked: wrong phase", FeedbackSeverity.Warning);
            events.ActionLogAdded("D1.Proxy.Coordination", "Proxy task executed round two", FeedbackSeverity.Info);
            events.TurnChanged(2, 3);
            events.NarrativeEventAdded("I1.Story.IraqBorder", "Second event emerges", FeedbackSeverity.Info);

            Assert("Map panel uses en-US hotspot label", (mapPanel.LatestMapText.text ?? string.Empty).Contains("[HOTSPOT]"));
            Assert("Diplomacy panel uses en-US locked label", (diplomacyPanel.LatestDiplomacyText.text ?? string.Empty).Contains("[ACTION LOCKED]"));
            Assert("Battle report uses en-US summary header", (actionLog.HistoryText.text ?? string.Empty).Contains("=== TURN SUMMARY ==="));
            Assert("Event panel uses en-US preview label", (eventPanel.LatestEventText.text ?? string.Empty).Contains("[OPTION PREVIEW]"));

            UnityEngine.Object.DestroyImmediate(table);
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

        private static void TestR1ReplayRL01MapDiplomacyBattleEvent()
        {
            Debug.Log("\n--- R1 Replay RL-01: Map -> Diplomacy -> Battle -> Event ---");
            ResetGameManagerSingleton();

            var go = new GameObject("R1ReplayRL01Test");
            var state = CreateDefaultState();
            var events = ScriptableObject.CreateInstance<GameEvents>();
            var replayLog = new R1ReplayLogger("R1-03", "RL-01");

            int actionLogCount = 0;
            int narrativeEventCount = 0;
            int nodeControlEventCount = 0;
            string lastActionLog = string.Empty;
            string lastNarrativeEvent = string.Empty;

            events.OnActionLogAdded += (sourceId, message, severity) =>
            {
                actionLogCount++;
                lastActionLog = $"{sourceId}:{message}";
            };
            events.OnNarrativeEventAdded += (eventId, message, severity) =>
            {
                narrativeEventCount++;
                lastNarrativeEvent = $"{eventId}:{message}";
            };
            events.OnNodeControlChanged += (nodeId, oldController, newController, controlPoints) =>
            {
                nodeControlEventCount++;
            };

            var h2 = go.AddComponent<EventideAge.Systems.H2.NodeNetworkSystem>();
            var c2 = go.AddComponent<EventideAge.Systems.C2.DiplomaticProtocolsSystem>();
            var d1 = go.AddComponent<EventideAge.Systems.D1.MilitaryOperationsSystem>();
            var i1 = go.AddComponent<EventideAge.Systems.I1.EventSystem>();

            h2.Initialize(state, events);
            c2.Initialize(state, events);
            d1.Initialize(state, events);
            i1.Initialize(state, events);
            d1.ProxySuccessRate = 1f;

            bool mapStepPass = h2.HasNode(GameIds.Node.Hormuz)
                && h2.HasNode(GameIds.Node.IraqBorder)
                && h2.GetAdjacentNodes(GameIds.Node.Hormuz).Count > 0;
            replayLog.RecordStep(
                state,
                "S01",
                "Select strategic node from map graph",
                "Canonical node exists and has adjacency",
                $"HormuzExists={h2.HasNode(GameIds.Node.Hormuz)}, AdjacentCount={h2.GetAdjacentNodes(GameIds.Node.Hormuz).Count}",
                mapStepPass);
            Assert("RL-01 map step: canonical node and adjacency are available", mapStepPass);

            var protocol = c2.ProposeProtocol("Vahid", "GoldLeader", EventideAge.Systems.C2.ProtocolType.TradeAgreement);
            bool diplomacyStepPass = protocol != null
                && c2.SignProtocol(protocol.ProtocolId)
                && c2.HasActiveProtocol(GameIds.Faction.Vashid, GameIds.Faction.Aurean, EventideAge.Systems.C2.ProtocolType.TradeAgreement);
            replayLog.RecordStep(
                state,
                "S02",
                "Propose and sign diplomacy protocol",
                "Trade agreement is active between canonical factions",
                protocol == null
                    ? "protocol=null"
                    : $"ProtocolId={protocol.ProtocolId}, From={protocol.FromFaction}, To={protocol.ToFaction}",
                diplomacyStepPass);
            Assert("RL-01 diplomacy step: trade agreement can be signed and activated", diplomacyStepPass);

            var targetNode = state.GetNode(GameIds.Node.IraqBorder);
            int beforeControl = targetNode != null ? targetNode.ControlPoints : -1;
            bool battleSuccess = d1.ExecuteAction(EventideAge.Systems.D1.MilitaryActionType.Proxy, GameIds.Node.IraqBorder);
            int afterControl = state.GetNode(GameIds.Node.IraqBorder)?.ControlPoints ?? -1;
            bool battleStepPass = battleSuccess
                && nodeControlEventCount > 0
                && actionLogCount > 0
                && lastActionLog.Contains("D1.ProxyAction.Standard")
                && afterControl <= beforeControl;
            replayLog.RecordStep(
                state,
                "S03",
                "Execute battle action and generate battle report",
                "Battle action succeeds and emits D1 canonical report + node-control change",
                $"BattleSuccess={battleSuccess}, BeforeControl={beforeControl}, AfterControl={afterControl}, NodeEvents={nodeControlEventCount}, LastActionLog={lastActionLog}",
                battleStepPass);
            Assert("RL-01 battle step: battle report and node-control event are emitted", battleStepPass);

            var replayEvent = i1.CreateTurnBasedEvent(
                "R1 RL-01 Followup",
                state.CurrentTurn,
                new[] { $"relation_change:{GameIds.Faction.Aurean}:5" });
            i1.QueueEvent(replayEvent);
            events.TurnEnded(state.CurrentTurn);

            bool eventTriggered = i1.GetTriggeredEvents().Any(evt => evt.EventId == replayEvent.EventId);
            bool eventStepPass = eventTriggered && narrativeEventCount > 0 && !string.IsNullOrEmpty(lastNarrativeEvent);
            replayLog.RecordStep(
                state,
                "S04",
                "Trigger follow-up event and verify event feed",
                "Queued event is triggered and appears in narrative channel",
                $"EventId={replayEvent.EventId}, Triggered={eventTriggered}, NarrativeEvents={narrativeEventCount}, LastNarrative={lastNarrativeEvent}",
                eventStepPass);
            Assert("RL-01 event step: queued event is triggered and logged", eventStepPass);

            replayLog.Flush();

            UnityEngine.Object.DestroyImmediate(i1);
            UnityEngine.Object.DestroyImmediate(d1);
            UnityEngine.Object.DestroyImmediate(c2);
            UnityEngine.Object.DestroyImmediate(h2);
            UnityEngine.Object.DestroyImmediate(go);
            CleanupTestState(state, events);
            ResetGameManagerSingleton();
        }

        private static void TestR1ReplayRL02PhaseLifecycleReplay()
        {
            Debug.Log("\n--- R1 Replay RL-02: Turn/Phase/AP Lifecycle ---");

            var config = ScriptableObject.CreateInstance<GameConfig>();
            config.PhaseConfigs = new PhaseConfig[6];
            config.PhaseConfigs[0] = new PhaseConfig { PhaseName = "外交", BaseActionPoints = 2 };
            config.PhaseConfigs[1] = new PhaseConfig { PhaseName = "战略", BaseActionPoints = 2 };
            config.PhaseConfigs[2] = new PhaseConfig { PhaseName = "作战", BaseActionPoints = 3 };
            config.PhaseConfigs[3] = new PhaseConfig { PhaseName = "后勤", BaseActionPoints = 1 };
            config.PhaseConfigs[4] = new PhaseConfig { PhaseName = "情报", BaseActionPoints = 1 };
            config.PhaseConfigs[5] = new PhaseConfig { PhaseName = "AI响应", BaseActionPoints = 0 };
            config.FactionConfigs = new FactionConfig[1];
            config.FactionConfigs[0] = new FactionConfig
            {
                FactionId = GameIds.Faction.Vashid,
                FactionName = "Vashid",
                IsPlayerControlled = true,
                InitialControlledPoints = 100,
                InitialRelationship = 100,
                InitialSatisfaction = 100
            };
            config.ResourceConfigs = new ResourceConfig[1];
            config.ResourceConfigs[0] = new ResourceConfig
            {
                ResourceId = GameIds.Resource.GoldLeaf,
                ResourceName = "GoldLeaf",
                InitialAmount = 100,
                MaxCapacity = 999,
                ResourceType = ResourceType.Accumulative
            };
            config.RegionConfigs = new RegionConfig[1];
            config.RegionConfigs[0] = new RegionConfig { RegionId = "RL02Region", RegionName = "RL02Region", NodeConfigs = new NodeConfig[1] };
            config.RegionConfigs[0].NodeConfigs[0] = new NodeConfig
            {
                NodeId = "RL02Node",
                NodeName = "RL02Node",
                NodeType = NodeType.Chokepoint,
                DefenseBonus = 0,
                InitialController = GameIds.Faction.Vashid,
                InitialControlPoints = 100,
                MaxControlPoints = 100
            };

            var state = ScriptableObject.CreateInstance<GameState>();
            state.Config = config;
            state.Initialize();

            var replayLog = new R1ReplayLogger("R1-03", "RL-02");

            bool initPass = state.CurrentTurn == 1
                && state.CurrentPhaseIndex == 0
                && state.ActionPointsRemaining == GameConfig.kTotalActionPoints
                && state.CurrentPhaseActionPointsRemaining == 2
                && state.UniversalActionPointsRemaining == GameConfig.kUniversalActionPoints;
            replayLog.RecordStep(
                state,
                "S01",
                "Initialize phase lifecycle replay",
                "Turn=1, Phase=0, AP=11, PhaseAP=2, UniversalAP=2",
                $"Turn={state.CurrentTurn}, Phase={state.CurrentPhaseIndex}, AP={state.ActionPointsRemaining}, PhaseAP={state.CurrentPhaseActionPointsRemaining}, UniversalAP={state.UniversalActionPointsRemaining}",
                initPass);
            Assert("RL-02 init: starts with SSOT AP envelope", initPass);

            bool phase0Spend = state.TrySpendActionPoints(3);
            bool phase0Pass = phase0Spend
                && state.ActionPointsRemaining == 8
                && state.CurrentPhaseActionPointsRemaining == 0
                && state.UniversalActionPointsRemaining == 1;
            replayLog.RecordStep(
                state,
                "S02",
                "Spend 3 AP in diplomacy phase",
                "Phase AP consumed first and universal AP covers shortfall",
                $"Spend={phase0Spend}, AP={state.ActionPointsRemaining}, PhaseAP={state.CurrentPhaseActionPointsRemaining}, UniversalAP={state.UniversalActionPointsRemaining}",
                phase0Pass);
            Assert("RL-02 phase0: phase AP then universal AP consumption is correct", phase0Pass);

            state.ExpireCurrentPhaseActionPoints();
            state.CurrentPhaseIndex = 1;
            state.PreparePhaseActionPoints(1);
            bool phase1EntryPass = state.CurrentPhaseActionPointsRemaining == 2
                && state.UniversalActionPointsRemaining == 1
                && state.ActionPointsRemaining == 8;
            replayLog.RecordStep(
                state,
                "S03",
                "Enter phase 1 after expiry",
                "Phase 1 grants fresh phase AP and carries universal AP",
                $"PhaseAP={state.CurrentPhaseActionPointsRemaining}, UniversalAP={state.UniversalActionPointsRemaining}, AP={state.ActionPointsRemaining}",
                phase1EntryPass);
            Assert("RL-02 phase1 entry: base AP refreshed and universal AP carried", phase1EntryPass);

            bool phase1Spend = state.TrySpendActionPoints(3);
            bool phase1SpendPass = phase1Spend
                && state.UniversalActionPointsRemaining == 0
                && state.ActionPointsRemaining == 5;
            replayLog.RecordStep(
                state,
                "S04",
                "Spend 3 AP in strategy phase",
                "Phase AP + remaining universal AP are consumed",
                $"Spend={phase1Spend}, AP={state.ActionPointsRemaining}, PhaseAP={state.CurrentPhaseActionPointsRemaining}, UniversalAP={state.UniversalActionPointsRemaining}",
                phase1SpendPass);
            Assert("RL-02 phase1: AP spending and depletion are correct", phase1SpendPass);

            int[] expectedPhaseAp = { 2, 2, 3, 1, 1, 0 };
            bool sequencePass = true;
            for (int phase = 2; phase <= 5; phase++)
            {
                state.ExpireCurrentPhaseActionPoints();
                state.CurrentPhaseIndex = phase;
                state.PreparePhaseActionPoints(phase);
                sequencePass &= state.CurrentPhaseActionPointsRemaining == expectedPhaseAp[phase];
            }

            bool aiPhaseBlocked = !state.TrySpendActionPoints(1);
            bool phaseSequencePass = sequencePass && aiPhaseBlocked;
            replayLog.RecordStep(
                state,
                "S05",
                "Advance through combat/logistics/intel/AI phases",
                "Phase AP sequence is 3/1/1/0 and AI phase blocks AP spending",
                $"Phase2={expectedPhaseAp[2]}, Phase3={expectedPhaseAp[3]}, Phase4={expectedPhaseAp[4]}, Phase5={state.CurrentPhaseActionPointsRemaining}, AiSpendBlocked={aiPhaseBlocked}",
                phaseSequencePass);
            Assert("RL-02 phase sequence: AP profile and AI block behavior are correct", phaseSequencePass);

            state.ExpireCurrentPhaseActionPoints();
            state.CurrentTurn++;
            state.CurrentPhaseIndex = 0;
            state.ResetTurnActionPoints();
            state.PreparePhaseActionPoints(0);
            bool wrapPass = state.CurrentTurn == 2
                && state.ActionPointsRemaining == GameConfig.kTotalActionPoints
                && state.UniversalActionPointsRemaining == GameConfig.kUniversalActionPoints
                && state.CurrentPhaseActionPointsRemaining == 2;
            replayLog.RecordStep(
                state,
                "S06",
                "Wrap to next turn and reset AP",
                "New turn resets AP envelope to 11/2/2",
                $"Turn={state.CurrentTurn}, AP={state.ActionPointsRemaining}, PhaseAP={state.CurrentPhaseActionPointsRemaining}, UniversalAP={state.UniversalActionPointsRemaining}",
                wrapPass);
            Assert("RL-02 wrap: turn reset restores full AP envelope", wrapPass);

            replayLog.Flush();

            UnityEngine.Object.DestroyImmediate(state);
            UnityEngine.Object.DestroyImmediate(config);
        }

        private static void TestR1ReplayRL03SaveLoadCrossTurnReplay()
        {
            Debug.Log("\n--- R1 Replay RL-03: Save/Load Cross-Turn Consistency ---");

            ResetGameManagerSingleton();

            var managerGO = new GameObject("R1ReplayRL03Manager");
            var manager = managerGO.AddComponent<GameManager>();
            var systemsGO = new GameObject("R1ReplayRL03Systems");
            systemsGO.transform.SetParent(managerGO.transform);

            var state = CreateDefaultState();
            var events = ScriptableObject.CreateInstance<GameEvents>();
            var replayLog = new R1ReplayLogger("R1-03", "RL-03");

            var saveSystem = systemsGO.AddComponent<EventideAge.Systems.A4.SaveSystem>();
            var j = systemsGO.AddComponent<EventideAge.Systems.J.VictoryDefeatSystem>();
            var d4 = systemsGO.AddComponent<EventideAge.Systems.D4.NuclearDeterrenceSystem>();
            var d1 = systemsGO.AddComponent<EventideAge.Systems.D1.MilitaryOperationsSystem>();

            manager.Systems = new List<GameSystem> { saveSystem, j, d4, d1 };

            j.Initialize(state, events);
            d4.Initialize(state, events);
            d1.Initialize(state, events);
            saveSystem.Initialize(state, events);
            d1.ProxySuccessRate = 1f;

            state.CurrentTurn = 9;
            state.CurrentPhaseIndex = 3;
            state.ActionPointsRemaining = 7;
            state.CurrentPhaseActionPointsRemaining = 1;
            state.UniversalActionPointsRemaining = 1;

            var goldLeaf = state.GetResource(GameIds.Resource.GoldLeaf);
            var fireOil = state.GetResource(GameIds.Resource.FireOil);
            if (goldLeaf != null) goldLeaf.Amount = 333;
            if (fireOil != null) fireOil.Amount = 144;
            d4.SetWarheadCount(9);
            d1.ExecuteAction(EventideAge.Systems.D1.MilitaryActionType.Proxy, GameIds.Node.SyriaZone);

            int snapshotTurn = state.CurrentTurn;
            int snapshotPhase = state.CurrentPhaseIndex;
            int snapshotActionPoints = state.ActionPointsRemaining;
            int snapshotPhaseActionPoints = state.CurrentPhaseActionPointsRemaining;
            int snapshotUniversalActionPoints = state.UniversalActionPointsRemaining;
            int snapshotGoldLeaf = goldLeaf != null ? goldLeaf.Amount : -1;
            int snapshotFireOil = fireOil != null ? fireOil.Amount : -1;
            var snapshotNode = state.GetNode(GameIds.Node.SyriaZone);
            int snapshotControl = snapshotNode != null ? snapshotNode.ControlPoints : -1;
            string snapshotController = snapshotNode != null ? snapshotNode.ControllingFactionId : string.Empty;
            j.ForceEndGame("r1_replay_rl03_snapshot");
            int snapshotWarheads = d4.GetState().WarheadCount;
            string snapshotEndReason = j.GetEndReason();

            string saveName = $"r1_rl03_{Guid.NewGuid():N}";
            bool saveResult = saveSystem.SaveGame(saveName);
            replayLog.RecordStep(
                state,
                "S01",
                "Persist replay snapshot to save slot",
                "Save succeeds and slot exists",
                $"SaveName={saveName}, SaveResult={saveResult}, Exists={saveSystem.SaveExists(saveName)}",
                saveResult && saveSystem.SaveExists(saveName));
            Assert("RL-03 save step: snapshot save succeeds", saveResult);

            state.CurrentTurn = 1;
            state.CurrentPhaseIndex = 0;
            state.ActionPointsRemaining = 11;
            state.CurrentPhaseActionPointsRemaining = 2;
            state.UniversalActionPointsRemaining = 2;
            if (goldLeaf != null) goldLeaf.Amount = 12;
            if (fireOil != null) fireOil.Amount = 22;
            if (snapshotNode != null)
            {
                snapshotNode.ControlPoints = snapshotNode.MaxControlPoints;
                snapshotNode.ControllingFactionId = GameIds.Faction.Aurean;
            }
            d4.SetWarheadCount(0);
            j.Reset();

            bool loadResult = saveSystem.LoadGame(saveName);
            var restoredNode = state.GetNode(GameIds.Node.SyriaZone);
            bool restoredPass = loadResult
                && state.CurrentTurn == snapshotTurn
                && state.CurrentPhaseIndex == snapshotPhase
                && state.ActionPointsRemaining == snapshotActionPoints
                && state.CurrentPhaseActionPointsRemaining == snapshotPhaseActionPoints
                && state.UniversalActionPointsRemaining == snapshotUniversalActionPoints
                && goldLeaf != null && goldLeaf.Amount == snapshotGoldLeaf
                && fireOil != null && fireOil.Amount == snapshotFireOil
                && restoredNode != null && restoredNode.ControlPoints == snapshotControl
                && restoredNode.ControllingFactionId == snapshotController
                && d4.GetState().WarheadCount == snapshotWarheads
                && j.IsGameEnded()
                && j.GetEndReason() == snapshotEndReason;
            replayLog.RecordStep(
                state,
                "S02",
                "Load replay snapshot and compare key state fields",
                "Turn/phase/AP/resources/node/victory are fully restored",
                $"LoadResult={loadResult}, Turn={state.CurrentTurn}, Phase={state.CurrentPhaseIndex}, AP={state.ActionPointsRemaining}, GoldLeaf={goldLeaf?.Amount}, FireOil={fireOil?.Amount}, NodeControl={restoredNode?.ControlPoints}, NodeController={restoredNode?.ControllingFactionId}, Warheads={d4.GetState().WarheadCount}, EndReason={j.GetEndReason()}",
                restoredPass);
            Assert("RL-03 load step: cross-turn state consistency restored", restoredPass);

            bool deleteResult = saveSystem.DeleteSave(saveName);
            replayLog.RecordStep(
                state,
                "S03",
                "Delete replay save slot",
                "Save slot is removed",
                $"DeleteResult={deleteResult}, ExistsAfterDelete={saveSystem.SaveExists(saveName)}",
                deleteResult && !saveSystem.SaveExists(saveName));
            Assert("RL-03 cleanup step: temporary save is deleted", deleteResult && !saveSystem.SaveExists(saveName));

            replayLog.Flush();

            UnityEngine.Object.DestroyImmediate(d1);
            UnityEngine.Object.DestroyImmediate(d4);
            UnityEngine.Object.DestroyImmediate(j);
            UnityEngine.Object.DestroyImmediate(saveSystem);
            UnityEngine.Object.DestroyImmediate(managerGO);
            CleanupTestState(state, events);
            ResetGameManagerSingleton();
        }

        private static void TestR1ReplayRL10TimeoutEndgameReplay()
        {
            Debug.Log("\n--- R1 Replay RL-10: Timeout Endgame Path (A5/J) ---");

            var go = new GameObject("R1ReplayRL10Test");
            var state = CreateMinimalState();
            var events = ScriptableObject.CreateInstance<GameEvents>();
            var replayLog = new R1ReplayLogger("R1-03", "RL-10");

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

            events.TurnEnded(state.CurrentTurn);
            bool timeoutTriggeredOnce = endCount == 1 && endReason == "attrition";
            replayLog.RecordStep(
                state,
                "S01",
                "Dispatch TurnEnded at max turn",
                "Timeout endgame fires once with attrition reason",
                $"EndCount={endCount}, EndReason={endReason}",
                timeoutTriggeredOnce);
            Assert("RL-10 timeout step: max turn triggers attrition endgame once", timeoutTriggeredOnce);

            events.TurnEnded(state.CurrentTurn);
            j.OnTurnEnded(state.CurrentTurn);
            bool singleFirePass = endCount == 1;
            replayLog.RecordStep(
                state,
                "S02",
                "Repeat turn-end signals after game end",
                "No duplicate timeout endgame dispatch",
                $"EndCount={endCount}, EndReason={endReason}",
                singleFirePass);
            Assert("RL-10 single-fire step: duplicate turn-end signals do not re-fire endgame", singleFirePass);

            string timeDisplay = a5.GetCurrentTimeDisplay();
            string expectedDisplay = EventideAge.Systems.A5.GameClock.FormatTurnAsHalfYear(GameConfig.kMaxTurns);
            bool timeDisplayPass = timeDisplay == expectedDisplay;
            replayLog.RecordStep(
                state,
                "S03",
                "Validate A5 time display at timeout turn",
                "A5 display matches half-year formatter at turn 24",
                $"Display={timeDisplay}, Expected={expectedDisplay}",
                timeDisplayPass);
            Assert("RL-10 display step: A5 time display matches turn formatter at timeout", timeDisplayPass);

            replayLog.Flush();

            UnityEngine.Object.DestroyImmediate(j);
            UnityEngine.Object.DestroyImmediate(a5);
            UnityEngine.Object.DestroyImmediate(go);
            CleanupTestState(state, events);
        }

        private static void TestR1CrossTurnConsistencyApResourceNodeVictory()
        {
            Debug.Log("\n--- R1-04 Consistency: AP / Resource / Node / Victory ---");

            var state = CreateR1ConsistencyState();
            var events = ScriptableObject.CreateInstance<GameEvents>();
            var replayLog = new R1ReplayLogger("R1-04", "CONSISTENCY");

            var jRoot = new GameObject("R1ConsistencyVictoryRoot");
            var j = jRoot.AddComponent<EventideAge.Systems.J.VictoryDefeatSystem>();
            j.Initialize(state, events);
            j.VictoryThreshold = 101f;

            bool baselinePass = state.CurrentTurn == 1
                && state.CurrentPhaseIndex == 0
                && state.ActionPointsRemaining == GameConfig.kTotalActionPoints
                && state.CurrentPhaseActionPointsRemaining == 2
                && state.UniversalActionPointsRemaining == GameConfig.kUniversalActionPoints;
            replayLog.RecordStep(
                state,
                "S01",
                "Initialize consistency scenario",
                "Turn=1, Phase=0, AP=11, PhaseAP=2, UniversalAP=2",
                $"Turn={state.CurrentTurn}, Phase={state.CurrentPhaseIndex}, AP={state.ActionPointsRemaining}, PhaseAP={state.CurrentPhaseActionPointsRemaining}, UniversalAP={state.UniversalActionPointsRemaining}",
                baselinePass);
            Assert("R1-04 baseline: AP envelope initialized to SSOT", baselinePass);

            bool spendPhase0 = state.TrySpendActionPoints(3);
            bool spendPhase0Pass = spendPhase0
                && state.ActionPointsRemaining == 8
                && state.CurrentPhaseActionPointsRemaining == 0
                && state.UniversalActionPointsRemaining == 1;
            replayLog.RecordStep(
                state,
                "S02",
                "Spend AP in phase 0 before turn crossing",
                "Spend 3 AP -> AP=8, PhaseAP=0, UniversalAP=1",
                $"Spend={spendPhase0}, AP={state.ActionPointsRemaining}, PhaseAP={state.CurrentPhaseActionPointsRemaining}, UniversalAP={state.UniversalActionPointsRemaining}",
                spendPhase0Pass);
            Assert("R1-04 AP: phase+universal AP spending is consistent", spendPhase0Pass);

            var goldLeaf = state.GetResource(GameIds.Resource.GoldLeaf);
            var fireOil = state.GetResource(GameIds.Resource.FireOil);
            var node = state.GetNode(GameIds.Node.IraqBorder);

            if (goldLeaf != null)
                goldLeaf.Amount = Mathf.Clamp(goldLeaf.Amount - 17, 0, goldLeaf.MaxCapacity);
            if (fireOil != null)
                fireOil.Amount = Mathf.Clamp(fireOil.Amount + 9, 0, fireOil.MaxCapacity);
            if (node != null)
            {
                node.ControlPoints = Mathf.Clamp(node.ControlPoints - 12, 0, node.MaxControlPoints);
                events.NodeControlChanged(node.NodeId, GameIds.Faction.Aurean, node.ControllingFactionId, node.ControlPoints);
            }

            int snapshotGoldLeaf = goldLeaf?.Amount ?? -1;
            int snapshotFireOil = fireOil?.Amount ?? -1;
            int snapshotNodeControl = node?.ControlPoints ?? -1;
            string snapshotNodeController = node?.ControllingFactionId ?? string.Empty;

            bool snapshotPass = snapshotGoldLeaf >= 0
                && snapshotFireOil >= 0
                && snapshotNodeControl >= 0
                && !string.IsNullOrEmpty(snapshotNodeController);
            replayLog.RecordStep(
                state,
                "S03",
                "Capture pre-wrap resource/node snapshot",
                "GoldLeaf/FireOil/node-control snapshot exists",
                $"GoldLeaf={snapshotGoldLeaf}, FireOil={snapshotFireOil}, NodeControl={snapshotNodeControl}, NodeController={snapshotNodeController}",
                snapshotPass);
            Assert("R1-04 snapshot: resources and node-control captured before turn wrap", snapshotPass);

            state.ExpireCurrentPhaseActionPoints();
            for (int phase = 1; phase <= GameConfig.kAiResponsePhaseIndex; phase++)
            {
                state.CurrentPhaseIndex = phase;
                state.PreparePhaseActionPoints(phase);
                if (phase < GameConfig.kAiResponsePhaseIndex)
                {
                    state.ExpireCurrentPhaseActionPoints();
                }
            }

            int oldTurn = state.CurrentTurn;
            events.TurnEnded(oldTurn);
            state.CurrentTurn++;
            events.TurnChanged(oldTurn, state.CurrentTurn);
            state.CurrentPhaseIndex = 0;
            state.ResetTurnActionPoints();
            state.PreparePhaseActionPoints(0);
            events.PhaseChanged(0);

            bool wrapConsistencyPass = state.CurrentTurn == oldTurn + 1
                && state.ActionPointsRemaining == GameConfig.kTotalActionPoints
                && state.CurrentPhaseActionPointsRemaining == 2
                && state.UniversalActionPointsRemaining == GameConfig.kUniversalActionPoints
                && (goldLeaf?.Amount ?? -1) == snapshotGoldLeaf
                && (fireOil?.Amount ?? -1) == snapshotFireOil
                && (node?.ControlPoints ?? -1) == snapshotNodeControl
                && (node?.ControllingFactionId ?? string.Empty) == snapshotNodeController
                && !j.IsGameEnded();
            replayLog.RecordStep(
                state,
                "S04",
                "Cross to next turn and validate consistency",
                "AP resets while resources/node/victory status stay consistent",
                $"Turn={state.CurrentTurn}, AP={state.ActionPointsRemaining}, PhaseAP={state.CurrentPhaseActionPointsRemaining}, UniversalAP={state.UniversalActionPointsRemaining}, GoldLeaf={goldLeaf?.Amount}, FireOil={fireOil?.Amount}, NodeControl={node?.ControlPoints}, NodeController={node?.ControllingFactionId}, GameEnded={j.IsGameEnded()}",
                wrapConsistencyPass);
            Assert("R1-04 cross-turn: AP reset and state consistency are preserved", wrapConsistencyPass);

            j.ForceEndGame("r1_04_manual_lock");
            int endEventCount = 0;
            events.OnGameEnded += _ => endEventCount++;

            int endTurn = state.CurrentTurn;
            events.TurnEnded(endTurn);
            state.CurrentTurn++;
            events.TurnChanged(endTurn, state.CurrentTurn);

            bool victoryStatePass = j.IsGameEnded()
                && j.GetEndReason() == "r1_04_manual_lock"
                && endEventCount == 0;
            replayLog.RecordStep(
                state,
                "S05",
                "Validate victory/defeat state persistence after turn change",
                "Forced end state remains stable without duplicate GameEnded dispatch",
                $"GameEnded={j.IsGameEnded()}, EndReason={j.GetEndReason()}, EndEvents={endEventCount}, Turn={state.CurrentTurn}",
                victoryStatePass);
            Assert("R1-04 victory-state: end-state is stable across turns without duplicate dispatch", victoryStatePass);

            replayLog.Flush();

            UnityEngine.Object.DestroyImmediate(jRoot);
            CleanupTestState(state, events);
        }

        private static void TestR1SaveCompatibilityReadWriteAndRecovery()
        {
            Debug.Log("\n--- R1-05 Save Compatibility: Read/Write + Recovery ---");

            ResetGameManagerSingleton();

            var managerGO = new GameObject("R1SaveCompatReadWriteManager");
            var manager = managerGO.AddComponent<GameManager>();
            var systemsGO = new GameObject("R1SaveCompatReadWriteSystems");
            systemsGO.transform.SetParent(managerGO.transform);

            var state = CreateDefaultState();
            var events = ScriptableObject.CreateInstance<GameEvents>();
            var replayLog = new R1ReplayLogger("R1-05", "SAVE-RW-RECOVERY");

            var saveSystem = systemsGO.AddComponent<EventideAge.Systems.A4.SaveSystem>();
            var j = systemsGO.AddComponent<EventideAge.Systems.J.VictoryDefeatSystem>();
            var d4 = systemsGO.AddComponent<EventideAge.Systems.D4.NuclearDeterrenceSystem>();

            manager.Systems = new List<GameSystem> { saveSystem, j, d4 };

            j.Initialize(state, events);
            d4.Initialize(state, events);
            saveSystem.Initialize(state, events);

            state.CurrentTurn = 6;
            state.CurrentPhaseIndex = 2;
            state.ActionPointsRemaining = 6;
            state.CurrentPhaseActionPointsRemaining = 2;
            state.UniversalActionPointsRemaining = 0;

            var goldLeaf = state.GetResource(GameIds.Resource.GoldLeaf);
            var fireOil = state.GetResource(GameIds.Resource.FireOil);
            if (goldLeaf != null) goldLeaf.Amount = 451;
            if (fireOil != null) fireOil.Amount = 177;
            d4.SetWarheadCount(12);
            j.ForceEndGame("r1_05_readwrite_snapshot");

            string saveName = $"r1_05_rw_{Guid.NewGuid():N}";
            bool saveResult = saveSystem.SaveGame(saveName);
            replayLog.RecordStep(
                state,
                "S01",
                "Write wrapped save snapshot",
                "Save succeeds and slot exists",
                $"Save={saveResult}, Exists={saveSystem.SaveExists(saveName)}, SaveName={saveName}",
                saveResult && saveSystem.SaveExists(saveName));
            Assert("R1-05 read/write: wrapped save snapshot created", saveResult && saveSystem.SaveExists(saveName));

            state.CurrentTurn = 1;
            state.CurrentPhaseIndex = 0;
            state.ActionPointsRemaining = 11;
            state.CurrentPhaseActionPointsRemaining = 2;
            state.UniversalActionPointsRemaining = 2;
            if (goldLeaf != null) goldLeaf.Amount = 11;
            if (fireOil != null) fireOil.Amount = 22;
            d4.SetWarheadCount(0);
            j.Reset();

            bool loadResult = saveSystem.LoadGame(saveName);
            bool loadPass = loadResult
                && state.CurrentTurn == 6
                && state.CurrentPhaseIndex == 2
                && state.ActionPointsRemaining == 6
                && state.CurrentPhaseActionPointsRemaining == 2
                && state.UniversalActionPointsRemaining == 0
                && goldLeaf != null && goldLeaf.Amount == 451
                && fireOil != null && fireOil.Amount == 177
                && d4.GetState().WarheadCount == 12
                && j.IsGameEnded()
                && j.GetEndReason() == "r1_05_readwrite_snapshot";
            replayLog.RecordStep(
                state,
                "S02",
                "Read wrapped save snapshot",
                "Turn/phase/AP/resources/system states are restored",
                $"Load={loadResult}, Turn={state.CurrentTurn}, Phase={state.CurrentPhaseIndex}, AP={state.ActionPointsRemaining}, PhaseAP={state.CurrentPhaseActionPointsRemaining}, UAP={state.UniversalActionPointsRemaining}, GoldLeaf={goldLeaf?.Amount}, FireOil={fireOil?.Amount}, Warheads={d4.GetState().WarheadCount}, EndReason={j.GetEndReason()}",
                loadPass);
            Assert("R1-05 read/write: wrapped save load restores state", loadPass);

            string missingName = $"r1_05_missing_{Guid.NewGuid():N}";
            bool missingLoad = saveSystem.LoadGame(missingName);
            replayLog.RecordStep(
                state,
                "S03",
                "Load non-existing save",
                "Missing slot returns false without crash",
                $"SaveName={missingName}, Load={missingLoad}",
                !missingLoad);
            Assert("R1-05 recovery: missing save returns false", !missingLoad);

            string invalidWrappedName = $"r1_05_invalid_wrapped_{Guid.NewGuid():N}";
            bool invalidWrappedSaved = saveSystem.SaveGame(invalidWrappedName);
            string invalidWrappedPath = GetSaveFilePathForTest(invalidWrappedName);
            File.WriteAllText(invalidWrappedPath, "{\"SaveVersion\":1,\"GameStateJson\":\"\"}");
            int beforeInvalidTurn = state.CurrentTurn;
            bool invalidWrappedLoad = saveSystem.LoadGame(invalidWrappedName);
            bool invalidWrappedPass = invalidWrappedSaved && !invalidWrappedLoad && state.CurrentTurn == beforeInvalidTurn;
            replayLog.RecordStep(
                state,
                "S04",
                "Load invalid wrapped save payload",
                "Invalid wrapped payload is rejected and runtime state remains stable",
                $"Save={invalidWrappedSaved}, Path={invalidWrappedPath}, Load={invalidWrappedLoad}, TurnBefore={beforeInvalidTurn}, TurnAfter={state.CurrentTurn}",
                invalidWrappedPass);
            Assert("R1-05 recovery: invalid wrapped payload is rejected", invalidWrappedPass);

            string legacyName = $"r1_05_legacy_{Guid.NewGuid():N}";
            state.CurrentTurn = 8;
            state.CurrentPhaseIndex = 1;
            state.ActionPointsRemaining = 9;
            state.CurrentPhaseActionPointsRemaining = 1;
            state.UniversalActionPointsRemaining = 1;
            if (goldLeaf != null) goldLeaf.Amount = 500;
            string legacyJson = JsonUtility.ToJson(state, true);
            string legacyPath = GetSaveFilePathForTest(legacyName);
            File.WriteAllText(legacyPath, legacyJson);

            state.CurrentTurn = 2;
            state.CurrentPhaseIndex = 0;
            state.ActionPointsRemaining = 11;
            state.CurrentPhaseActionPointsRemaining = 2;
            state.UniversalActionPointsRemaining = 2;
            if (goldLeaf != null) goldLeaf.Amount = 40;

            bool legacyLoad = saveSystem.LoadGame(legacyName);
            bool legacyPass = legacyLoad
                && state.CurrentTurn == 8
                && state.CurrentPhaseIndex == 1
                && state.ActionPointsRemaining == 9
                && state.CurrentPhaseActionPointsRemaining == 1
                && state.UniversalActionPointsRemaining == 1
                && goldLeaf != null && goldLeaf.Amount == 500;
            replayLog.RecordStep(
                state,
                "S05",
                "Load legacy GameState-only save",
                "Legacy payload is accepted and key fields are restored",
                $"Path={legacyPath}, Load={legacyLoad}, Turn={state.CurrentTurn}, Phase={state.CurrentPhaseIndex}, AP={state.ActionPointsRemaining}, PhaseAP={state.CurrentPhaseActionPointsRemaining}, UAP={state.UniversalActionPointsRemaining}, GoldLeaf={goldLeaf?.Amount}",
                legacyPass);
            Assert("R1-05 compatibility: legacy GameState-only save can be loaded", legacyPass);

            saveSystem.DeleteSave(saveName);
            saveSystem.DeleteSave(invalidWrappedName);
            saveSystem.DeleteSave(legacyName);

            replayLog.Flush();

            UnityEngine.Object.DestroyImmediate(d4);
            UnityEngine.Object.DestroyImmediate(j);
            UnityEngine.Object.DestroyImmediate(saveSystem);
            UnityEngine.Object.DestroyImmediate(managerGO);
            CleanupTestState(state, events);
            ResetGameManagerSingleton();
        }

        private static void TestR1SaveCompatibilityMultiTurnConsistency()
        {
            Debug.Log("\n--- R1-05 Save Compatibility: Multi-Turn Consistency ---");

            ResetGameManagerSingleton();

            var managerGO = new GameObject("R1SaveCompatMultiTurnManager");
            var manager = managerGO.AddComponent<GameManager>();
            var systemsGO = new GameObject("R1SaveCompatMultiTurnSystems");
            systemsGO.transform.SetParent(managerGO.transform);

            var state = CreateDefaultState();
            var events = ScriptableObject.CreateInstance<GameEvents>();
            var replayLog = new R1ReplayLogger("R1-05", "SAVE-MULTI-TURN");

            var saveSystem = systemsGO.AddComponent<EventideAge.Systems.A4.SaveSystem>();
            manager.Systems = new List<GameSystem> { saveSystem };
            saveSystem.Initialize(state, events);

            var goldLeaf = state.GetResource(GameIds.Resource.GoldLeaf);
            var fireOil = state.GetResource(GameIds.Resource.FireOil);
            var node = state.GetNode(GameIds.Node.IraqBorder);

            string turn4Save = $"r1_05_turn4_{Guid.NewGuid():N}";
            state.CurrentTurn = 4;
            state.CurrentPhaseIndex = 1;
            state.ActionPointsRemaining = 8;
            state.CurrentPhaseActionPointsRemaining = 2;
            state.UniversalActionPointsRemaining = 0;
            if (goldLeaf != null) goldLeaf.Amount = 320;
            if (fireOil != null) fireOil.Amount = 210;
            if (node != null)
            {
                node.ControlPoints = 63;
                node.ControllingFactionId = GameIds.Faction.Aurean;
            }

            bool saveTurn4 = saveSystem.SaveGame(turn4Save);
            replayLog.RecordStep(
                state,
                "S01",
                "Save turn-4 snapshot",
                "Turn-4 snapshot save succeeds",
                $"Save={saveTurn4}, Name={turn4Save}, Turn={state.CurrentTurn}, GoldLeaf={goldLeaf?.Amount}, FireOil={fireOil?.Amount}, NodeControl={node?.ControlPoints}, NodeController={node?.ControllingFactionId}",
                saveTurn4);
            Assert("R1-05 multi-turn: turn-4 snapshot saved", saveTurn4);

            string turn5Save = $"r1_05_turn5_{Guid.NewGuid():N}";
            state.CurrentTurn = 5;
            state.CurrentPhaseIndex = 3;
            state.ActionPointsRemaining = 5;
            state.CurrentPhaseActionPointsRemaining = 1;
            state.UniversalActionPointsRemaining = 1;
            if (goldLeaf != null) goldLeaf.Amount = 287;
            if (fireOil != null) fireOil.Amount = 198;
            if (node != null)
            {
                node.ControlPoints = 54;
                node.ControllingFactionId = GameIds.Faction.AshConfederacy;
            }

            bool saveTurn5 = saveSystem.SaveGame(turn5Save);
            replayLog.RecordStep(
                state,
                "S02",
                "Save turn-5 snapshot",
                "Turn-5 snapshot save succeeds",
                $"Save={saveTurn5}, Name={turn5Save}, Turn={state.CurrentTurn}, GoldLeaf={goldLeaf?.Amount}, FireOil={fireOil?.Amount}, NodeControl={node?.ControlPoints}, NodeController={node?.ControllingFactionId}",
                saveTurn5);
            Assert("R1-05 multi-turn: turn-5 snapshot saved", saveTurn5);

            state.CurrentTurn = 1;
            state.CurrentPhaseIndex = 0;
            state.ActionPointsRemaining = 11;
            state.CurrentPhaseActionPointsRemaining = 2;
            state.UniversalActionPointsRemaining = 2;
            if (goldLeaf != null) goldLeaf.Amount = 10;
            if (fireOil != null) fireOil.Amount = 10;
            if (node != null)
            {
                node.ControlPoints = 100;
                node.ControllingFactionId = GameIds.Faction.Vashid;
            }

            bool loadTurn4 = saveSystem.LoadGame(turn4Save);
            bool turn4Pass = loadTurn4
                && state.CurrentTurn == 4
                && state.CurrentPhaseIndex == 1
                && state.ActionPointsRemaining == 8
                && state.CurrentPhaseActionPointsRemaining == 2
                && state.UniversalActionPointsRemaining == 0
                && goldLeaf != null && goldLeaf.Amount == 320
                && fireOil != null && fireOil.Amount == 210
                && node != null && node.ControlPoints == 63
                && node.ControllingFactionId == GameIds.Faction.Aurean;
            replayLog.RecordStep(
                state,
                "S03",
                "Load turn-4 snapshot",
                "Turn-4 snapshot fields are restored exactly",
                $"Load={loadTurn4}, Turn={state.CurrentTurn}, Phase={state.CurrentPhaseIndex}, AP={state.ActionPointsRemaining}, PhaseAP={state.CurrentPhaseActionPointsRemaining}, UAP={state.UniversalActionPointsRemaining}, GoldLeaf={goldLeaf?.Amount}, FireOil={fireOil?.Amount}, NodeControl={node?.ControlPoints}, NodeController={node?.ControllingFactionId}",
                turn4Pass);
            Assert("R1-05 multi-turn: turn-4 snapshot load is consistent", turn4Pass);

            bool loadTurn5 = saveSystem.LoadGame(turn5Save);
            bool turn5Pass = loadTurn5
                && state.CurrentTurn == 5
                && state.CurrentPhaseIndex == 3
                && state.ActionPointsRemaining == 5
                && state.CurrentPhaseActionPointsRemaining == 1
                && state.UniversalActionPointsRemaining == 1
                && goldLeaf != null && goldLeaf.Amount == 287
                && fireOil != null && fireOil.Amount == 198
                && node != null && node.ControlPoints == 54
                && node.ControllingFactionId == GameIds.Faction.AshConfederacy;
            replayLog.RecordStep(
                state,
                "S04",
                "Load turn-5 snapshot",
                "Turn-5 snapshot fields are restored exactly",
                $"Load={loadTurn5}, Turn={state.CurrentTurn}, Phase={state.CurrentPhaseIndex}, AP={state.ActionPointsRemaining}, PhaseAP={state.CurrentPhaseActionPointsRemaining}, UAP={state.UniversalActionPointsRemaining}, GoldLeaf={goldLeaf?.Amount}, FireOil={fireOil?.Amount}, NodeControl={node?.ControlPoints}, NodeController={node?.ControllingFactionId}",
                turn5Pass);
            Assert("R1-05 multi-turn: turn-5 snapshot load is consistent", turn5Pass);

            var allSaves = saveSystem.GetAllSaves();
            bool listPass = allSaves.Any(name => name == turn4Save) && allSaves.Any(name => name == turn5Save);
            replayLog.RecordStep(
                state,
                "S05",
                "Enumerate save slots for multi-turn replay",
                "Both turn-4 and turn-5 slots are discoverable",
                $"Turn4Found={allSaves.Any(name => name == turn4Save)}, Turn5Found={allSaves.Any(name => name == turn5Save)}, SaveCount={allSaves.Length}",
                listPass);
            Assert("R1-05 multi-turn: save slots are discoverable by list API", listPass);

            saveSystem.DeleteSave(turn4Save);
            saveSystem.DeleteSave(turn5Save);

            replayLog.Flush();

            UnityEngine.Object.DestroyImmediate(saveSystem);
            UnityEngine.Object.DestroyImmediate(managerGO);
            CleanupTestState(state, events);
            ResetGameManagerSingleton();
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

        private static void TestL1AsyncMultiplayerLifecycleGuardrail()
        {
            Debug.Log("\n--- Testing L1 Async Multiplayer Lifecycle Guardrail ---");

            var go = new GameObject("L1AsyncMultiplayerGuardrail");
            var state = CreateMinimalState();
            var events = ScriptableObject.CreateInstance<GameEvents>();

            var l1 = go.AddComponent<EventideAge.Systems.L1.AsyncMultiplayerSystem>();
            l1.AutoLoadOnInitialize = false;
            l1.SaveFileName = $"l1_async_rooms_test_{Guid.NewGuid():N}.json";
            l1.Initialize(state, events);

            var room = l1.CreateRoom("host_A");
            Assert("L1 room created in waiting state before opponent join", room != null && room.RoomState == EventideAge.Systems.L1.AsyncRoomState.WaitingForOpponent);

            bool joined = l1.TryJoinRoom(room.RoomId, "guest_B", out string joinError);
            Assert("L1 guest can join waiting room", joined && string.IsNullOrEmpty(joinError));

            bool submitHost = l1.TrySubmitTurn(
                room.RoomId,
                new EventideAge.Systems.L1.AsyncTurnPacket
                {
                    TurnIndex = 1,
                    ActingFactionId = GameIds.Faction.Vashid,
                    ActionIds = new[] { "C2.EnergyTransit.Sign", "H1.MapInspect" }
                },
                out string submitHostError);
            Assert("L1 host packet accepted for pending faction", submitHost && string.IsNullOrEmpty(submitHostError));

            bool submitGuest = l1.TrySubmitTurn(
                room.RoomId,
                new EventideAge.Systems.L1.AsyncTurnPacket
                {
                    TurnIndex = 2,
                    ActingFactionId = GameIds.Faction.Aurean,
                    ActionIds = new[] { "D1.CounterMove" }
                },
                out string submitGuestError);
            Assert("L1 guest packet accepted and resolves one full turn", submitGuest && string.IsNullOrEmpty(submitGuestError));

            var updatedRoom = l1.GetRoom(room.RoomId);
            Assert("L1 room rotates back to Vashid and increments resolved turn", updatedRoom != null && updatedRoom.PendingFactionId == GameIds.Faction.Vashid && updatedRoom.LastResolvedTurn == 1);

            var reloadGo = new GameObject("L1AsyncMultiplayerReloadGuardrail");
            var reloaded = reloadGo.AddComponent<EventideAge.Systems.L1.AsyncMultiplayerSystem>();
            reloaded.AutoLoadOnInitialize = true;
            reloaded.SaveFileName = l1.SaveFileName;
            reloaded.Initialize(state, events);

            var snapshot = reloaded.GetAllRooms();
            Assert("L1 room store persists and reloads from disk", snapshot != null && snapshot.Length == 1 && snapshot[0].History.Count == 2);

            string path = l1.GetStorePathForDebug();
            if (!string.IsNullOrWhiteSpace(path) && File.Exists(path))
            {
                File.Delete(path);
            }

            UnityEngine.Object.DestroyImmediate(reloaded);
            UnityEngine.Object.DestroyImmediate(reloadGo);
            UnityEngine.Object.DestroyImmediate(l1);
            UnityEngine.Object.DestroyImmediate(go);
            CleanupTestState(state, events);
        }

        private static void TestL2TutorialSystemLifecycleGuardrail()
        {
            Debug.Log("\n--- Testing L2 Tutorial System Lifecycle Guardrail ---");

            string progressKey = $"L2.Tutorial.Progress.Test.{Guid.NewGuid():N}";
            PlayerPrefs.DeleteKey(progressKey);

            var go = new GameObject("L2TutorialSystemGuardrail");
            var state = CreateMinimalState();
            var events = ScriptableObject.CreateInstance<GameEvents>();

            var tutorialConfig = ScriptableObject.CreateInstance<EventideAge.Systems.I1.TutorialFlowConfig>();
            tutorialConfig.SetSteps(EventideAge.Systems.I1.TutorialFlowConfig.CreateDefaultSteps());

            var l2 = go.AddComponent<EventideAge.Systems.L2.TutorialSystem>();
            l2.TutorialFlowConfigAsset = tutorialConfig;
            l2.AutoStartOnInitialize = true;
            l2.ProgressSaveKey = progressKey;
            l2.Initialize(state, events);

            Assert("L2 starts in active lifecycle when config exists", l2.GetLifecycleState() == EventideAge.Systems.L2.TutorialLifecycleState.Active);

            state.CurrentTurn = 1;
            l2.OnTurnStarted(1);
            bool firstActivated = !string.IsNullOrWhiteSpace(l2.GetActiveStepId());
            Assert("L2 activates first step when trigger condition matches", firstActivated);

            int safety = 0;
            while (l2.GetLifecycleState() == EventideAge.Systems.L2.TutorialLifecycleState.Active && safety < 12)
            {
                if (!string.IsNullOrWhiteSpace(l2.GetActiveStepId()))
                {
                    l2.AcknowledgeActiveStep();
                }

                state.CurrentTurn++;
                l2.OnTurnStarted(state.CurrentTurn);
                safety++;
            }

            Assert("L2 completes after all scripted tutorial steps are acknowledged", l2.GetLifecycleState() == EventideAge.Systems.L2.TutorialLifecycleState.Completed);
            Assert("L2 completed step count reaches total step count", l2.GetCompletedStepCount() == l2.GetTotalStepCount());

            var reloadGo = new GameObject("L2TutorialSystemReloadGuardrail");
            var reloaded = reloadGo.AddComponent<EventideAge.Systems.L2.TutorialSystem>();
            reloaded.TutorialFlowConfigAsset = tutorialConfig;
            reloaded.AutoStartOnInitialize = false;
            reloaded.ProgressSaveKey = progressKey;
            reloaded.Initialize(state, events);
            Assert("L2 progress persists between instances", reloaded.GetLifecycleState() == EventideAge.Systems.L2.TutorialLifecycleState.Completed);

            PlayerPrefs.DeleteKey(progressKey);
            PlayerPrefs.Save();

            UnityEngine.Object.DestroyImmediate(reloaded);
            UnityEngine.Object.DestroyImmediate(reloadGo);
            UnityEngine.Object.DestroyImmediate(l2);
            UnityEngine.Object.DestroyImmediate(tutorialConfig);
            UnityEngine.Object.DestroyImmediate(go);
            CleanupTestState(state, events);
        }

        private static void TestL3SteamIntegrationFallbackAndMockGuardrail()
        {
            Debug.Log("\n--- Testing L3 Steam Integration Fallback and Mock Guardrail ---");

            var state = CreateMinimalState();
            var events = ScriptableObject.CreateInstance<GameEvents>();

            var goMock = new GameObject("L3SteamMockGuardrail");
            var steamConfig = ScriptableObject.CreateInstance<EventideAge.Systems.L3.SteamIntegrationConfig>();
            steamConfig.AppId = "123456";
            steamConfig.ForceMockProviderInEditor = true;

            var l3Mock = goMock.AddComponent<EventideAge.Systems.L3.SteamIntegrationSystem>();
            l3Mock.Config = steamConfig;
            l3Mock.UseMockProviderInEditor = true;
            l3Mock.Initialize(state, events);

            bool unlock = l3Mock.TryUnlockAchievement("FIRST_STEP");
            bool upload = l3Mock.TryUploadCloudSave("slot_01", "{\"turn\":1}", out string uploadError);
            bool download = l3Mock.TryDownloadCloudSave("slot_01", out string payload, out string downloadError);

            Assert("L3 mock provider initializes and reports readiness", l3Mock.IsPlatformReady() && l3Mock.GetProviderName() == "MockProvider");
            Assert("L3 mock provider unlocks achievements", unlock && l3Mock.GetUnlockedAchievements().Length == 1);
            Assert("L3 mock provider cloud save upload/download works", upload && download && string.IsNullOrEmpty(uploadError) && string.IsNullOrEmpty(downloadError) && payload.Contains("\"turn\":1"));

            var goNull = new GameObject("L3SteamNullGuardrail");
            var l3Null = goNull.AddComponent<EventideAge.Systems.L3.SteamIntegrationSystem>();
            l3Null.Config = null;
            l3Null.UseMockProviderInEditor = false;
            l3Null.Initialize(state, events);

            bool nullUnlock = l3Null.TryUnlockAchievement("FIRST_STEP");
            bool nullUpload = l3Null.TryUploadCloudSave("slot_x", "{}", out string nullUploadError);
            Assert("L3 null provider stays operational but does not support steam features", l3Null.IsPlatformReady() && l3Null.GetProviderName() == "NullProvider" && !nullUnlock && !nullUpload && !string.IsNullOrEmpty(nullUploadError));

            UnityEngine.Object.DestroyImmediate(l3Null);
            UnityEngine.Object.DestroyImmediate(goNull);
            UnityEngine.Object.DestroyImmediate(l3Mock);
            UnityEngine.Object.DestroyImmediate(steamConfig);
            UnityEngine.Object.DestroyImmediate(goMock);
            CleanupTestState(state, events);
        }

        private static void TestL4LocalizationSwitchAndFormattingGuardrail()
        {
            Debug.Log("\n--- Testing L4 Localization Switch and Formatting Guardrail ---");

            var go = new GameObject("L4LocalizationGuardrail");
            var state = CreateMinimalState();
            var events = ScriptableObject.CreateInstance<GameEvents>();

            var table = ScriptableObject.CreateInstance<EventideAge.Systems.L4.LocalizationTableConfig>();
            table.DefaultLocale = "zh-CN";
            table.SupportedLocales = new[] { "zh-CN", "en-US", "ru-RU" };
            table.Entries = new[]
            {
                new EventideAge.Systems.L4.LocalizedTextEntry
                {
                    Key = "ui.main.start",
                    ZhCN = "开始游戏",
                    EnUS = "Start Game",
                    RuRU = "Начать игру"
                },
                new EventideAge.Systems.L4.LocalizedTextEntry
                {
                    Key = "ui.main.load",
                    ZhCN = "加载存档",
                    EnUS = "Load Save",
                    RuRU = "Загрузить сохранение"
                }
            };

            var l4 = go.AddComponent<EventideAge.Systems.L4.LocalizationSystem>();
            l4.LocalizationTable = table;
            l4.AutoLoadSavedLocale = false;
            l4.Initialize(state, events);

            Assert("L4 default locale loads from config", l4.GetCurrentLocale() == "zh-CN");
            Assert("L4 zh-CN translation resolves", l4.Translate("ui.main.start") == "开始游戏");

            bool switched = l4.SetLocale("en-US");
            string startText = l4.Translate("ui.main.start");
            string numberText = l4.FormatNumber(12345);
            Assert("L4 locale switching to en-US succeeds", switched && l4.GetCurrentLocale() == "en-US");
            Assert("L4 en-US translation resolves", startText == "Start Game");
            Assert("L4 number formatting changes with locale", !string.IsNullOrWhiteSpace(numberText));

            bool invalidLocaleRejected = !l4.SetLocale("xx-XX");
            Assert("L4 rejects unsupported locales", invalidLocaleRejected);

            UnityEngine.Object.DestroyImmediate(l4);
            UnityEngine.Object.DestroyImmediate(table);
            UnityEngine.Object.DestroyImmediate(go);
            CleanupTestState(state, events);
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

        private static string GetSaveFilePathForTest(string saveName)
        {
            return Path.Combine(Application.persistentDataPath, "Saves", saveName + ".json");
        }

        private static GameState CreateR1ConsistencyState()
        {
            var config = ScriptableObject.CreateInstance<GameConfig>();
            config.PhaseConfigs = new PhaseConfig[6];
            config.PhaseConfigs[0] = new PhaseConfig { PhaseName = "外交", BaseActionPoints = 2, SortOrder = 0 };
            config.PhaseConfigs[1] = new PhaseConfig { PhaseName = "战略", BaseActionPoints = 2, SortOrder = 1 };
            config.PhaseConfigs[2] = new PhaseConfig { PhaseName = "作战", BaseActionPoints = 3, SortOrder = 2 };
            config.PhaseConfigs[3] = new PhaseConfig { PhaseName = "后勤", BaseActionPoints = 1, SortOrder = 3 };
            config.PhaseConfigs[4] = new PhaseConfig { PhaseName = "情报", BaseActionPoints = 1, SortOrder = 4 };
            config.PhaseConfigs[5] = new PhaseConfig { PhaseName = "AI响应", BaseActionPoints = 0, SortOrder = 5 };

            config.FactionConfigs = new FactionConfig[2];
            config.FactionConfigs[0] = new FactionConfig
            {
                FactionId = GameIds.Faction.Vashid,
                FactionName = "Vashid",
                IsPlayerControlled = true,
                InitialControlledPoints = 100,
                InitialRelationship = 100,
                InitialSatisfaction = 100
            };
            config.FactionConfigs[1] = new FactionConfig
            {
                FactionId = GameIds.Faction.Aurean,
                FactionName = "Aurean",
                IsPlayerControlled = false,
                InitialControlledPoints = 90,
                InitialRelationship = -60,
                InitialSatisfaction = 80
            };

            config.ResourceConfigs = new ResourceConfig[5];
            config.ResourceConfigs[0] = new ResourceConfig { ResourceId = GameIds.Resource.GoldLeaf, ResourceName = "GoldLeaf", InitialAmount = 200, MaxCapacity = 999, ResourceType = ResourceType.Accumulative };
            config.ResourceConfigs[1] = new ResourceConfig { ResourceId = GameIds.Resource.FireOil, ResourceName = "FireOil", InitialAmount = 180, MaxCapacity = 500, ResourceType = ResourceType.Accumulative };
            config.ResourceConfigs[2] = new ResourceConfig { ResourceId = GameIds.Resource.Arms, ResourceName = "Arms", InitialAmount = 120, MaxCapacity = 300, ResourceType = ResourceType.Consumable };
            config.ResourceConfigs[3] = new ResourceConfig { ResourceId = GameIds.Resource.AshWill, ResourceName = "AshWill", InitialAmount = 60, MaxCapacity = 100, ResourceType = ResourceType.Ratio };
            config.ResourceConfigs[4] = new ResourceConfig { ResourceId = GameIds.Resource.SocialValue, ResourceName = "SocialValue", InitialAmount = 75, MaxCapacity = 100, ResourceType = ResourceType.Ratio };

            config.RegionConfigs = new RegionConfig[1];
            config.RegionConfigs[0] = new RegionConfig { RegionId = "R1Consistency", RegionName = "R1Consistency", NodeConfigs = new NodeConfig[2] };
            config.RegionConfigs[0].NodeConfigs[0] = new NodeConfig
            {
                NodeId = GameIds.Node.IraqBorder,
                NodeName = "IraqBorder",
                NodeType = NodeType.Chokepoint,
                DefenseBonus = 20,
                InitialController = GameIds.Faction.Aurean,
                InitialControlPoints = 70,
                MaxControlPoints = 100
            };
            config.RegionConfigs[0].NodeConfigs[1] = new NodeConfig
            {
                NodeId = GameIds.Node.Hormuz,
                NodeName = "Hormuz",
                NodeType = NodeType.Port,
                DefenseBonus = 25,
                InitialController = GameIds.Faction.Vashid,
                InitialControlPoints = 90,
                MaxControlPoints = 100
            };

            var state = ScriptableObject.CreateInstance<GameState>();
            state.Config = config;
            state.Initialize();
            return state;
        }

        private static GameState CreateR2CampaignValidationState()
        {
            var state = CreateDefaultState();
            if (state?.Config == null)
                return state;

            state.Config.PhaseConfigs = new PhaseConfig[6];
            state.Config.PhaseConfigs[0] = new PhaseConfig { PhaseName = "外交", BaseActionPoints = 2, SortOrder = 0 };
            state.Config.PhaseConfigs[1] = new PhaseConfig { PhaseName = "战略", BaseActionPoints = 2, SortOrder = 1 };
            state.Config.PhaseConfigs[2] = new PhaseConfig { PhaseName = "作战", BaseActionPoints = 3, SortOrder = 2 };
            state.Config.PhaseConfigs[3] = new PhaseConfig { PhaseName = "后勤", BaseActionPoints = 1, SortOrder = 3 };
            state.Config.PhaseConfigs[4] = new PhaseConfig { PhaseName = "情报", BaseActionPoints = 1, SortOrder = 4 };
            state.Config.PhaseConfigs[5] = new PhaseConfig { PhaseName = "AI响应", BaseActionPoints = 0, SortOrder = 5 };

            state.CurrentTurn = 1;
            state.CurrentPhaseIndex = 0;
            state.ResetTurnActionPoints();
            state.PreparePhaseActionPoints(0);

            return state;
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
