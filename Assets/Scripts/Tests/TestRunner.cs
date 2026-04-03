using UnityEngine;
using EventideAge.Tests;

namespace EventideAge.Tests
{
    public class TestRunner : MonoBehaviour
    {
        [ContextMenu("Run All Tests")]
        public void RunTests()
        {
            StandaloneTest.RunAll();
        }
        
        [ContextMenu("Run GameConfig Tests")]
        public void RunGameConfigTests()
        {
            Debug.Log("=== GameConfig Tests ===");
            int p = 0, f = 0;
            StandaloneTest.RunAll();
        }
    }
}
