using UnityEngine;
using EventideAge.Core;

namespace EventideAge
{
    public class GameBootstrap : MonoBehaviour
    {
        [Header("Scene Configuration")]
        [SerializeField] private string mainSceneName = "MainGameScene";
        
        [Header("Auto Setup")]
        [SerializeField] private bool autoSetupOnStart = false;
        [SerializeField] private GameConfig config;
        [SerializeField] private GameState state;
        [SerializeField] private GameEvents events;
        
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
        
        private void Start()
        {
            if (autoSetupOnStart)
            {
                SetupGameManager();
            }
        }
        
        public void SetupGameManager()
        {
            var existing = FindObjectOfType<GameManager>();
            if (existing != null)
            {
                Debug.Log("[GameBootstrap] GameManager already exists");
                return;
            }
            
            var go = new GameObject("GameManager");
            var manager = go.AddComponent<GameManager>();
            
            if (config != null)
                manager.Config = config;
            if (state != null)
                manager.State = state;
            if (events != null)
                manager.Events = events;
            
            Debug.Log("[GameBootstrap] GameManager created");
        }
        
        public void LoadMainScene()
        {
            if (!string.IsNullOrEmpty(mainSceneName))
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(mainSceneName);
            }
        }
    }
}