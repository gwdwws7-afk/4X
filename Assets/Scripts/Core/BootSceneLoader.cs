using UnityEngine;
using UnityEngine.SceneManagement;

namespace EventideAge.Core
{
    public class BootSceneLoader : MonoBehaviour
    {
        [SerializeField] private string _mainSceneName = "MainGameScene";
        [SerializeField] private bool _dontDestroyOnLoad = true;
        [SerializeField] private bool _loadOnStart = true;

        private void Awake()
        {
            if (_dontDestroyOnLoad)
            {
                DontDestroyOnLoad(gameObject);
            }
        }

        private void Start()
        {
            if (_loadOnStart)
            {
                LoadMainScene();
            }
        }

        public void LoadMainScene()
        {
            if (string.IsNullOrWhiteSpace(_mainSceneName))
            {
                Debug.LogWarning("[BootSceneLoader] Main scene name is empty. Skip loading.");
                return;
            }

            if (SceneManager.GetActiveScene().name == _mainSceneName)
            {
                return;
            }

            SceneManager.LoadScene(_mainSceneName);
        }
    }
}
