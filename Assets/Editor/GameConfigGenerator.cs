#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEngine;
using EventideAge.Core;
using EventideAge.Config;
using EventideAge.Systems.I1;
using EventideAge.Systems.G;
using EventideAge.Systems.L3;
using EventideAge.Systems.L4;

namespace EventideAge.Editor
{
    public class GameConfigGenerator : EditorWindow
    {
        public const string kDefaultConfigPath = "Assets/ScriptableObjects/Config/DefaultGameConfig.asset";
        public const string kDefaultStatePath = "Assets/ScriptableObjects/Core/GameState.asset";
        public const string kDefaultEventsPath = "Assets/ScriptableObjects/Core/GameEvents.asset";

        [MenuItem("EventideAge/Generate Baseline ScriptableObjects")]
        public static void GenerateBaselineScriptableObjects()
        {
            var config = CreateOrUpdateDefaultConfigAsset();
            var state = CreateOrUpdateDefaultGameStateAsset(config);
            var events = CreateOrUpdateGameEventsAsset();

            PersistAndSelect(state != null ? (Object)state : config);
            Debug.Log("[GameConfigGenerator] Baseline ScriptableObjects ready (DefaultGameConfig/GameState/GameEvents).");
        }

        [MenuItem("EventideAge/Generate Default GameConfig")]
        public static void GenerateDefaultConfig()
        {
            var config = CreateOrUpdateDefaultConfigAsset();
            PersistAndSelect(config);
            Debug.Log($"[GameConfigGenerator] Default config ready at: {kDefaultConfigPath}");
        }
        
        [MenuItem("EventideAge/Generate GameState with Default Config")]
        public static void GenerateDefaultGameState()
        {
            var config = CreateOrUpdateDefaultConfigAsset();
            var state = CreateOrUpdateDefaultGameStateAsset(config);
            CreateOrUpdateGameEventsAsset();

            PersistAndSelect(state);
            Debug.Log($"[GameConfigGenerator] Default state ready at: {kDefaultStatePath}");
        }

        [MenuItem("EventideAge/Generate Default EventPoolConfig")]
        public static void GenerateDefaultEventPoolConfig()
        {
            var eventPool = ScriptableObject.CreateInstance<EventPoolConfig>();
            eventPool.SetTemplates(EventPoolConfig.CreateDefaultTemplates());

            string path = EditorUtility.SaveFilePanelInProject(
                "Save EventPoolConfig",
                "EventPool_Default",
                "asset",
                "Select where to save the default EventPoolConfig"
            );

            if (!string.IsNullOrEmpty(path))
            {
                AssetDatabase.CreateAsset(eventPool, path);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                Selection.activeObject = eventPool;
                EditorUtility.FocusProjectWindow();
                Debug.Log($"[GameConfigGenerator] Created default event pool at: {path}");
            }
            else
            {
                DestroyImmediate(eventPool);
            }
        }

        [MenuItem("EventideAge/Generate Default AIStrategySet")]
        public static void GenerateDefaultAIStrategySet()
        {
            var strategySet = ScriptableObject.CreateInstance<AIStrategySet>();
            strategySet.SetProfiles(AIStrategySet.CreateDefaultProfiles());

            string path = EditorUtility.SaveFilePanelInProject(
                "Save AIStrategySet",
                "AIStrategySet_Default",
                "asset",
                "Select where to save the default AIStrategySet"
            );

            if (!string.IsNullOrEmpty(path))
            {
                AssetDatabase.CreateAsset(strategySet, path);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                Selection.activeObject = strategySet;
                EditorUtility.FocusProjectWindow();
                Debug.Log($"[GameConfigGenerator] Created default AI strategy set at: {path}");
            }
            else
            {
                DestroyImmediate(strategySet);
            }
        }

        [MenuItem("EventideAge/Generate Default AIDifficultySet")]
        public static void GenerateDefaultAIDifficultySet()
        {
            var difficultySet = ScriptableObject.CreateInstance<AIDifficultySet>();
            difficultySet.SetProfiles(AIDifficultySet.CreateDefaultProfiles());

            string path = EditorUtility.SaveFilePanelInProject(
                "Save AIDifficultySet",
                "AIDifficultySet_Default",
                "asset",
                "Select where to save the default AIDifficultySet"
            );

            if (!string.IsNullOrEmpty(path))
            {
                AssetDatabase.CreateAsset(difficultySet, path);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                Selection.activeObject = difficultySet;
                EditorUtility.FocusProjectWindow();
                Debug.Log($"[GameConfigGenerator] Created default AI difficulty set at: {path}");
            }
            else
            {
                DestroyImmediate(difficultySet);
            }
        }

        [MenuItem("EventideAge/Generate Default TutorialFlowConfig")]
        public static void GenerateDefaultTutorialFlowConfig()
        {
            var tutorialFlow = ScriptableObject.CreateInstance<TutorialFlowConfig>();
            tutorialFlow.SetSteps(TutorialFlowConfig.CreateDefaultSteps());

            string path = EditorUtility.SaveFilePanelInProject(
                "Save TutorialFlowConfig",
                "TutorialFlow_Default",
                "asset",
                "Select where to save the default TutorialFlowConfig"
            );

            if (!string.IsNullOrEmpty(path))
            {
                AssetDatabase.CreateAsset(tutorialFlow, path);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                Selection.activeObject = tutorialFlow;
                EditorUtility.FocusProjectWindow();
                Debug.Log($"[GameConfigGenerator] Created default tutorial flow at: {path}");
            }
            else
            {
                DestroyImmediate(tutorialFlow);
            }
        }

        [MenuItem("EventideAge/Generate Default SteamIntegrationConfig")]
        public static void GenerateDefaultSteamIntegrationConfig()
        {
            var steamConfig = ScriptableObject.CreateInstance<SteamIntegrationConfig>();

            string path = EditorUtility.SaveFilePanelInProject(
                "Save SteamIntegrationConfig",
                "SteamIntegration_Default",
                "asset",
                "Select where to save the default SteamIntegrationConfig"
            );

            if (!string.IsNullOrEmpty(path))
            {
                AssetDatabase.CreateAsset(steamConfig, path);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                Selection.activeObject = steamConfig;
                EditorUtility.FocusProjectWindow();
                Debug.Log($"[GameConfigGenerator] Created default Steam integration config at: {path}");
            }
            else
            {
                DestroyImmediate(steamConfig);
            }
        }

        [MenuItem("EventideAge/Generate Default LocalizationTableConfig")]
        public static void GenerateDefaultLocalizationTableConfig()
        {
            var tableConfig = ScriptableObject.CreateInstance<LocalizationTableConfig>();

            string path = EditorUtility.SaveFilePanelInProject(
                "Save LocalizationTableConfig",
                "LocalizationTable_Default",
                "asset",
                "Select where to save the default LocalizationTableConfig"
            );

            if (!string.IsNullOrEmpty(path))
            {
                AssetDatabase.CreateAsset(tableConfig, path);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                Selection.activeObject = tableConfig;
                EditorUtility.FocusProjectWindow();
                Debug.Log($"[GameConfigGenerator] Created default localization table at: {path}");
            }
            else
            {
                DestroyImmediate(tableConfig);
            }
        }

        private static GameConfig CreateOrUpdateDefaultConfigAsset()
        {
            EnsureAssetDirectory(kDefaultConfigPath);

            var config = AssetDatabase.LoadAssetAtPath<GameConfig>(kDefaultConfigPath);
            if (config == null)
            {
                config = DefaultGameConfig.CreateDefault();
                config.name = "DefaultGameConfig";
                AssetDatabase.CreateAsset(config, kDefaultConfigPath);
                return config;
            }

            if (config is DefaultGameConfig defaultConfig)
            {
                defaultConfig.ApplyDesignBaseline();
                EditorUtility.SetDirty(defaultConfig);
                return defaultConfig;
            }

            EditorUtility.SetDirty(config);
            Debug.LogWarning($"[GameConfigGenerator] Asset at {kDefaultConfigPath} is not DefaultGameConfig. Kept existing GameConfig type: {config.GetType().Name}");
            return config;
        }

        private static GameState CreateOrUpdateDefaultGameStateAsset(GameConfig config)
        {
            EnsureAssetDirectory(kDefaultStatePath);

            var state = AssetDatabase.LoadAssetAtPath<GameState>(kDefaultStatePath);
            if (state == null)
            {
                state = ScriptableObject.CreateInstance<GameState>();
                state.name = "GameState";
                AssetDatabase.CreateAsset(state, kDefaultStatePath);
            }

            state.Config = config;
            state.Initialize();
            EditorUtility.SetDirty(state);
            return state;
        }

        private static GameEvents CreateOrUpdateGameEventsAsset()
        {
            EnsureAssetDirectory(kDefaultEventsPath);

            var events = AssetDatabase.LoadAssetAtPath<GameEvents>(kDefaultEventsPath);
            if (events != null)
            {
                EditorUtility.SetDirty(events);
                return events;
            }

            events = ScriptableObject.CreateInstance<GameEvents>();
            events.name = "GameEvents";
            AssetDatabase.CreateAsset(events, kDefaultEventsPath);
            return events;
        }

        private static void PersistAndSelect(Object selection)
        {
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            if (selection == null)
                return;

            Selection.activeObject = selection;
            EditorUtility.FocusProjectWindow();
        }

        private static void EnsureAssetDirectory(string assetPath)
        {
            string directory = Path.GetDirectoryName(assetPath);
            if (string.IsNullOrEmpty(directory))
                return;

            EnsureFolder(directory.Replace("\\", "/"));
        }

        private static void EnsureFolder(string path)
        {
            if (AssetDatabase.IsValidFolder(path))
                return;

            string[] parts = path.Split('/');
            string current = parts[0];
            for (int i = 1; i < parts.Length; i++)
            {
                string next = current + "/" + parts[i];
                if (!AssetDatabase.IsValidFolder(next))
                {
                    AssetDatabase.CreateFolder(current, parts[i]);
                }

                current = next;
            }
        }
    }
}
#endif
