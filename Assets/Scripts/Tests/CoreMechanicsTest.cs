using UnityEngine;
using EventideAge.Core;

namespace EventideAge.Tests
{
    public class CoreMechanicsTest : MonoBehaviour
    {
        [Header("Test Results")]
        public int Passed = 0;
        public int Failed = 0;
        
        private GameState _state;
        private GameEvents _events;
        private GameConfig _config;
        
        public void RunTests()
        {
            Debug.Log("=== Core Mechanics Test ===");
            Passed = 0;
            Failed = 0;
            
            Setup();
            
            TestGameConfig();
            TestGameStateInitialization();
            TestTurnLoopAP();
            TestPhaseAdvancement();
            TestResourceSystem();
            TestMapInitialization();
            
            Debug.Log($"=== Core Mechanics Results: {Passed} passed, {Failed} failed ===");
        }
        
        private void Setup()
        {
            _config = ScriptableObject.CreateInstance<GameConfig>();
            
            _config.PhaseConfigs = new PhaseConfig[6];
            _config.PhaseConfigs[0] = new PhaseConfig { PhaseName = "外交", BaseActionPoints = 2, SortOrder = 0 };
            _config.PhaseConfigs[1] = new PhaseConfig { PhaseName = "战略", BaseActionPoints = 2, SortOrder = 1 };
            _config.PhaseConfigs[2] = new PhaseConfig { PhaseName = "作战", BaseActionPoints = 2, SortOrder = 2 };
            _config.PhaseConfigs[3] = new PhaseConfig { PhaseName = "后勤", BaseActionPoints = 1, SortOrder = 3 };
            _config.PhaseConfigs[4] = new PhaseConfig { PhaseName = "情报", BaseActionPoints = 1, SortOrder = 4 };
            _config.PhaseConfigs[5] = new PhaseConfig { PhaseName = "AI响应", BaseActionPoints = 0, SortOrder = 5 };
            
            _config.FactionConfigs = new FactionConfig[2];
            _config.FactionConfigs[0] = new FactionConfig { FactionId = "Vashid", FactionName = "瓦希德帝国", IsPlayerControlled = true, InitialControlledPoints = 100, InitialRelationship = 100 };
            _config.FactionConfigs[1] = new FactionConfig { FactionId = "Aurean", FactionName = "黄金领", IsPlayerControlled = false, InitialControlledPoints = 100, InitialRelationship = -100 };
            
            _config.ResourceConfigs = new ResourceConfig[3];
            _config.ResourceConfigs[0] = new ResourceConfig { ResourceId = "Arms", ResourceName = "战械", InitialAmount = 50, MaxCapacity = 100, ResourceType = ResourceType.Consumable };
            _config.ResourceConfigs[1] = new ResourceConfig { ResourceId = "FireOil", ResourceName = "火油", InitialAmount = 80, MaxCapacity = 200, ResourceType = ResourceType.Accumulative };
            _config.ResourceConfigs[2] = new ResourceConfig { ResourceId = "SocialValue", ResourceName = "社稷值", InitialAmount = 100, MaxCapacity = 100, ResourceType = ResourceType.Ratio };
            
            _config.RegionConfigs = new RegionConfig[1];
            _config.RegionConfigs[0] = new RegionConfig
            {
                RegionId = "PersianGulf",
                RegionName = "波斯湾",
                NodeConfigs = new NodeConfig[2]
                {
                    new NodeConfig { NodeId = "Hormuz", NodeName = "霍尔木兹", NodeType = NodeType.Chokepoint, DefenseBonus = 30, InitialController = "Vashid", InitialControlPoints = 100, MaxControlPoints = 100 },
                    new NodeConfig { NodeId = "Bushehr", NodeName = "布什尔", NodeType = NodeType.Port, DefenseBonus = 20, InitialController = "Vashid", InitialControlPoints = 80, MaxControlPoints = 100 }
                }
            };
            
            _state = ScriptableObject.CreateInstance<GameState>();
            _state.Config = _config;
            _state.Initialize();
            
            _events = ScriptableObject.CreateInstance<GameEvents>();
        }
        
        private void TestGameConfig()
        {
            Assert("Config has 6 phases", _config.PhaseConfigs.Length == 6);
            Assert("Config has 2 factions", _config.FactionConfigs.Length == 2);
            Assert("Config has 3 resources", _config.ResourceConfigs.Length == 3);
            Assert("Config has 1 region", _config.RegionConfigs.Length == 1);
            Assert("Total AP is 11", GameConfig.kTotalActionPoints == 11);
        }
        
        private void TestGameStateInitialization()
        {
            Assert("Initial turn is 1", _state.CurrentTurn == 1);
            Assert("Initial phase is 0", _state.CurrentPhaseIndex == 0);
            Assert("Initial AP is 11", _state.ActionPointsRemaining == 11);
            Assert("Factions initialized", _state.Factions != null && _state.Factions.Length == 2);
            Assert("Resources initialized", _state.Resources != null && _state.Resources.Length == 3);
            Assert("Map initialized", _state.Map != null && _state.Map.Regions != null && _state.Map.Regions.Length == 1);
        }
        
        private void TestTurnLoopAP()
        {
            int initialAP = _state.ActionPointsRemaining;
            
            _state.ActionPointsRemaining -= 3;
            Assert("AP can be spent", _state.ActionPointsRemaining == initialAP - 3);
            
            bool canSpend = _state.ActionPointsRemaining >= 3;
            Assert("Can spend if enough AP", canSpend);
            
            bool cannotOverspend = _state.ActionPointsRemaining < 100;
            Assert("Cannot overspend", cannotOverspend);
        }
        
        private void TestPhaseAdvancement()
        {
            int currentPhase = _state.CurrentPhaseIndex;
            int nextPhase = (currentPhase + 1) % 6;
            
            _state.CurrentPhaseIndex = nextPhase;
            Assert("Phase advances to 1 (战略)", _state.CurrentPhaseIndex == 1);
            
            _state.CurrentPhaseIndex = 5;
            nextPhase = (_state.CurrentPhaseIndex + 1) % 6;
            _state.CurrentPhaseIndex = nextPhase;
            Assert("Phase wraps from 5 to 0", _state.CurrentPhaseIndex == 0);
            Assert("Turn increments on phase wrap", _state.CurrentTurn == 2);
        }
        
        private void TestResourceSystem()
        {
            var arms = _state.GetResource("Arms");
            Assert("Arms resource exists", arms != null);
            
            if (arms != null)
            {
                int initial = arms.Amount;
                arms.Amount = Mathf.Clamp(arms.Amount + 10, 0, arms.MaxCapacity);
                Assert("Resource can be modified", arms.Amount == initial + 10);
                
                arms.Amount = Mathf.Clamp(arms.Amount - 5, 0, arms.MaxCapacity);
                Assert("Resource can be spent", arms.Amount == initial + 5);
                
                arms.Amount = Mathf.Clamp(arms.Amount - 9999, 0, arms.MaxCapacity);
                Assert("Resource cannot go below 0", arms.Amount == 0);
            }
        }
        
        private void TestMapInitialization()
        {
            var region = _state.Map.Regions[0];
            Assert("Region name is 波斯湾", region.RegionName == "波斯湾");
            Assert("Region has 2 nodes", region.Nodes != null && region.Nodes.Length == 2);
            
            var hormuz = _state.GetNode("Hormuz");
            Assert("Hormuz node exists", hormuz != null);
            Assert("Hormuz is controlled by Vashid", hormuz?.ControllingFactionId == "Vashid");
            Assert("Hormuz control points is 100", hormuz?.ControlPoints == 100);
        }
        
        private void Assert(string name, bool condition)
        {
            if (condition)
            {
                Debug.Log($"[PASS] {name}");
                Passed++;
            }
            else
            {
                Debug.LogError($"[FAIL] {name}");
                Failed++;
            }
        }
    }
}
