using UnityEngine;
using EventideAge.Core;

namespace EventideAge.Config
{
    public class ConfigLoader : MonoBehaviour
    {
        [Header("Load from Resources")]
        public string ConfigPath = "Config/DefaultGameConfig";
        
        private void Awake()
        {
            LoadConfig();
        }
        
        public void LoadConfig()
        {
            var config = Resources.Load<DefaultGameConfig>(ConfigPath);
            if (config != null)
            {
                var gm = FindObjectOfType<GameManager>();
                if (gm != null)
                {
                    gm.Config = config;
                    if (gm.State != null)
                    {
                        gm.State.Config = config;
                    }
                }
                Debug.Log($"[ConfigLoader] Loaded config from Resources/{ConfigPath}");
            }
            else
            {
                Debug.LogWarning($"[ConfigLoader] Config not found at Resources/{ConfigPath}. Using runtime defaults.");
            }
        }
    }
}
