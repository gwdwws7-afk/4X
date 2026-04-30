#if UNITY_EDITOR
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using EventideAge.Core;

namespace EventideAge.Editor
{
    public static class SceneCreator
    {
        public const string kBootScenePath = "Assets/Scenes/BootScene.unity";
        public const string kMainGameScenePath = "Assets/Scenes/MainGameScene.unity";

        [MenuItem("EventideAge/Create Main Game Scene")]
        public static void CreateMainGameScene()
        {
            CreateOrUpdateMainGameScene(openAfterCreate: true);
            Debug.Log($"[SceneCreator] Main game scene ready: {kMainGameScenePath}");
        }

        [MenuItem("EventideAge/Create Boot Scene")]
        public static void CreateBootScene()
        {
            CreateOrUpdateBootScene(openAfterCreate: true);
            Debug.Log($"[SceneCreator] Boot scene ready: {kBootScenePath}");
        }

        [MenuItem("EventideAge/Setup Baseline Scenes & Config")]
        public static void SetupBaselineScenesAndConfig()
        {
            GameConfigGenerator.GenerateBaselineScriptableObjects();
            CreateOrUpdateMainGameScene(openAfterCreate: false);
            CreateOrUpdateBootScene(openAfterCreate: false);
            PlayablePrototypeSceneGenerator.CreatePlayablePrototypeSceneBatch();
            UpdateBuildSettings();

            EditorSceneManager.OpenScene(kMainGameScenePath, OpenSceneMode.Single);
            Debug.Log("[SceneCreator] Baseline scenes/config setup complete (Boot/Main/PlayablePrototype + Build Settings).");
        }

        public static void CreateOrUpdateMainGameScene(bool openAfterCreate)
        {
            EnsureFolder("Assets/Scenes");

            var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
            SceneSetup.RunOneClickSetup();
            EditorSceneManager.SaveScene(scene, kMainGameScenePath);

            if (openAfterCreate)
            {
                EditorSceneManager.OpenScene(kMainGameScenePath, OpenSceneMode.Single);
            }

            UpdateBuildSettings();
        }

        public static void CreateOrUpdateBootScene(bool openAfterCreate)
        {
            EnsureFolder("Assets/Scenes");

            var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
            var bootstrap = new GameObject("Bootstrap");
            bootstrap.AddComponent<BootSceneLoader>();

            EditorSceneManager.SaveScene(scene, kBootScenePath);
            if (openAfterCreate)
            {
                EditorSceneManager.OpenScene(kBootScenePath, OpenSceneMode.Single);
            }

            UpdateBuildSettings();
        }

        private static void UpdateBuildSettings()
        {
            var buildScenes = new List<EditorBuildSettingsScene>
            {
                new EditorBuildSettingsScene(kBootScenePath, true),
                new EditorBuildSettingsScene(kMainGameScenePath, true)
            };

            if (File.Exists(PlayablePrototypeSceneGenerator.kScenePath))
            {
                buildScenes.Add(new EditorBuildSettingsScene(PlayablePrototypeSceneGenerator.kScenePath, true));
            }

            EditorBuildSettings.scenes = buildScenes.ToArray();
        }

        private static void EnsureFolder(string path)
        {
            if (AssetDatabase.IsValidFolder(path))
            {
                return;
            }

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
