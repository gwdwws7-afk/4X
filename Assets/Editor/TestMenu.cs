#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace EventideAge.Editor
{
    public static class TestMenu
    {
        [MenuItem("EventideAge/Run All Tests")]
        public static void RunAllTests()
        {
            Debug.Log("=== EventideAge: Run All Tests ===");
            EventideAge.Tests.StandaloneTest.RunAll();
        }
    }
}
#endif
