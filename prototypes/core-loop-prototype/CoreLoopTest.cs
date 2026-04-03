using UnityEngine;

namespace Prototypes.CoreLoop
{
    /// <summary>
    /// Test harness for core loop prototype
    /// Run in Unity Editor to validate architecture
    /// </summary>
    public class CoreLoopTest : MonoBehaviour
    {
        [Header("References")]
        public GameState State;
        public GameEvents Events;
        public TurnLoopSystem TurnLoop;
        
        [Header("Test Config")]
        public bool RunAutoTest = false;
        
        private void Start()
        {
            if (State == null || Events == null || TurnLoop == null)
            {
                Debug.LogError("CoreLoopTest: Missing references!");
                return;
            }
            
            TurnLoop.Initialize(State, Events);
            
            if (RunAutoTest)
            {
                RunBasicValidation();
            }
        }
        
        public void RunBasicValidation()
        {
            Debug.Log("=== Core Loop Prototype Validation ===");
            
            // Test 1: Initial state
            Debug.Log($"Initial: Turn {State.CurrentTurn}, Phase {State.CurrentPhase}, AP {State.ActionPointsRemaining}");
            ValidationAssert(State.CurrentTurn == 1, "Initial turn is 1");
            ValidationAssert(State.CurrentPhase == PhaseType.Diplomacy, "Initial phase is Diplomacy");
            ValidationAssert(State.ActionPointsRemaining == 11, "Initial AP is 11");
            
            // Test 2: Action point spending
            bool spent = TurnLoop.SpendActionPoints(3, PhaseType.Diplomacy);
            ValidationAssert(spent == true, "Can spend 3 AP in Diplomacy phase");
            ValidationAssert(State.ActionPointsRemaining == 8, "8 AP remaining after spending 3");
            
            // Test 3: Cannot overspend
            bool overspend = TurnLoop.SpendActionPoints(100, PhaseType.Operations);
            ValidationAssert(overspend == false, "Cannot overspend AP");
            ValidationAssert(State.ActionPointsRemaining == 8, "AP unchanged after failed spend");
            
            // Test 4: Phase change (simulate)
            State.CurrentPhase = PhaseType.Operations;
            Events.PhaseChanged(PhaseType.Operations);
            
            // Test 5: Resources
            var resource = new ResourceState { ResourceId = "Arms", Amount = 50, MaxCapacity = 100 };
            Events.ResourceChanged(resource.ResourceId, resource.Amount);
            
            Debug.Log("=== Validation Complete ===");
        }
        
        private void ValidationAssert(bool condition, string message)
        {
            if (condition)
                Debug.Log($"[PASS] {message}");
            else
                Debug.LogError($"[FAIL] {message}");
        }
    }
}
