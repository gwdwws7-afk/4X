using UnityEngine;
using EventideAge.Core;

namespace EventideAge.Tests
{
    public class IntegrationTest : MonoBehaviour
    {
        [Header("References")]
        public GameManager GameManager;
        
        [Header("Test Config")]
        public bool RunOnStart = true;
        
        private int _passedTests = 0;
        private int _failedTests = 0;
        
        private void Start()
        {
            if (RunOnStart)
            {
                RunAllTests();
            }
        }
        
        public void RunAllTests()
        {
            Debug.Log("=== Integration Test Suite ===");
            _passedTests = 0;
            _failedTests = 0;
            
            TestInitialization();
            TestTurnLoop();
            TestPhaseEngine();
            TestResourceSystem();
            TestActionPointSpending();
            
            Debug.Log($"=== Results: {_passedTests} passed, {_failedTests} failed ===");
        }
        
        private void TestInitialization()
        {
            Assert("GameManager exists", GameManager != null);
            Assert("State exists", GameManager.State != null);
            Assert("Events exists", GameManager.Events != null);
            Assert("Config exists", GameManager.Config != null);
            Assert("Factions initialized", GameManager.State.Factions != null && GameManager.State.Factions.Length > 0);
            Assert("Resources initialized", GameManager.State.Resources != null && GameManager.State.Resources.Length > 0);
            Assert("Map initialized", GameManager.State.Map != null && GameManager.State.Map.Regions != null);
        }
        
        private void TestTurnLoop()
        {
            bool foundTurnLoop = false;
            foreach (var system in GameManager.Systems)
            {
                if (system != null && system.GetType().Name == "TurnLoopSystem")
                {
                    foundTurnLoop = true;
                    break;
                }
            }
            Assert("TurnLoopSystem exists in systems list", foundTurnLoop);
            Assert("Initial turn is 1", GameManager.State.CurrentTurn == 1);
            Assert("Initial AP is 11", GameManager.State.ActionPointsRemaining == 11);
            Assert("Initial phase AP is 2", GameManager.State.CurrentPhaseActionPointsRemaining == 2);
            Assert("Initial universal AP is 2", GameManager.State.UniversalActionPointsRemaining == 2);
        }
        
        private void TestPhaseEngine()
        {
            bool foundPhaseEngine = false;
            foreach (var system in GameManager.Systems)
            {
                if (system != null && system.GetType().Name == "PhaseEngine")
                {
                    foundPhaseEngine = true;
                    break;
                }
            }
            Assert("PhaseEngine exists in systems list", foundPhaseEngine);
            Assert("Initial phase is 0", GameManager.State.CurrentPhaseIndex == 0);
        }
        
        private void TestResourceSystem()
        {
            var resourceSystem = GetSystemByName("ResourceSystem");
            Assert("ResourceSystem exists", resourceSystem != null);
            
            if (resourceSystem != null)
            {
                int initialArms = GameManager.State.GetResource("Arms")?.Amount ?? 0;
                var modifyMethod = resourceSystem.GetType().GetMethod("ModifyResource");
                if (modifyMethod != null)
                {
                    modifyMethod.Invoke(resourceSystem, new object[] { "Arms", 10 });
                    int afterArms = GameManager.State.GetResource("Arms")?.Amount ?? 0;
                    Assert("Resource modify works", afterArms == initialArms + 10);
                }
            }
        }
        
        private void TestActionPointSpending()
        {
            int initialAP = GameManager.State.ActionPointsRemaining;
            bool success = GameManager.SpendActionPoints(3);
            Assert("AP spending works", success);
            Assert("AP correctly deducted", GameManager.State.ActionPointsRemaining == initialAP - 3);
            Assert("Phase AP consumed first", GameManager.State.CurrentPhaseActionPointsRemaining == 0);
            Assert("Universal AP consumed for shortfall", GameManager.State.UniversalActionPointsRemaining == 1);
            
            bool immediateOverspend = GameManager.SpendActionPoints(2);
            Assert("Cannot overspend immediate phase+universal AP", !immediateOverspend);
        }
        
        private Core.GameSystem GetSystemByName(string typeName)
        {
            foreach (var system in GameManager.Systems)
            {
                if (system != null && system.GetType().Name == typeName)
                    return system;
            }
            return null;
        }
        
        private void Assert(string testName, bool condition)
        {
            if (condition)
            {
                Debug.Log($"[PASS] {testName}");
                _passedTests++;
            }
            else
            {
                Debug.LogError($"[FAIL] {testName}");
                _failedTests++;
            }
        }
    }
}
