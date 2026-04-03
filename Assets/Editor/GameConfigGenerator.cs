#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using EventideAge.Core;
using EventideAge.Config;

namespace EventideAge.Editor
{
    public class GameConfigGenerator : EditorWindow
    {
        [MenuItem("EventideAge/Generate Default GameConfig")]
        public static void GenerateDefaultConfig()
        {
            var config = DefaultGameConfig.CreateDefault();
            
            string path = EditorUtility.SaveFilePanelInProject(
                "Save GameConfig",
                "GameConfig_Default",
                "asset",
                "Select where to save the default GameConfig"
            );
            
            if (!string.IsNullOrEmpty(path))
            {
                AssetDatabase.CreateAsset(config, path);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                Selection.activeObject = config;
                EditorUtility.FocusProjectWindow();
                Debug.Log($"[GameConfigGenerator] Created default config at: {path}");
            }
            else
            {
                DestroyImmediate(config);
            }
        }
        
        [MenuItem("EventideAge/Generate GameState with Default Config")]
        public static void GenerateDefaultGameState()
        {
            var config = DefaultGameConfig.CreateDefault();
            
            var state = ScriptableObject.CreateInstance<GameState>();
            state.Config = config;
            state.Initialize();
            
            string path = EditorUtility.SaveFilePanelInProject(
                "Save GameState",
                "GameState_Default",
                "asset",
                "Select where to save the default GameState"
            );
            
            if (!string.IsNullOrEmpty(path))
            {
                AssetDatabase.CreateAsset(state, path);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                Selection.activeObject = state;
                EditorUtility.FocusProjectWindow();
                Debug.Log($"[GameConfigGenerator] Created default state at: {path}");
            }
            else
            {
                DestroyImmediate(state);
                DestroyImmediate(config);
            }
        }
    }
}
#endif