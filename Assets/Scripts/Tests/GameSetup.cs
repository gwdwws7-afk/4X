using UnityEngine;
using EventideAge.Core;

namespace EventideAge.Tests
{
    public class GameSetup : MonoBehaviour
    {
        [Header("Required Assets")]
        public GameConfig Config;
        
        [Header("Optional")]
        public bool AutoStartTest = true;
        
        private void Awake()
        {
            SetupGameManager();
        }
        
        private void Start()
        {
            if (AutoStartTest)
            {
                var test = FindObjectOfType<IntegrationTest>();
                if (test != null && test.RunOnStart)
                {
                    test.RunAllTests();
                }
            }
        }
        
        public void SetupGameManager()
        {
            var gm = FindObjectOfType<GameManager>();
            if (gm == null)
            {
                var go = new GameObject("GameManager");
                gm = go.AddComponent<GameManager>();
            }
            
            if (Config != null)
            {
                gm.Config = Config;
            }
            
            if (gm.State == null)
            {
                var stateSO = ScriptableObject.CreateInstance<GameState>();
                gm.State = stateSO;
            }
            
            if (gm.Events == null)
            {
                var eventsSO = ScriptableObject.CreateInstance<GameEvents>();
                gm.Events = eventsSO;
            }
            
            gm.InitializeGame();
            
            var test = gm.GetComponent<IntegrationTest>();
            if (test == null)
            {
                test = gm.gameObject.AddComponent<IntegrationTest>();
            }
            test.GameManager = gm;
        }
    }
}
