#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace EventideAge.Editor
{
    public static class PlayablePrototypeSceneGenerator
    {
        public const string kScenePath = "Assets/Scenes/PlayablePrototype.unity";

        [MenuItem("EventideAge/Create Playable Prototype Scene")]
        public static void CreatePlayablePrototypeScene()
        {
            CreateOrUpdateScene();
            EditorSceneManager.OpenScene(kScenePath);
            Debug.Log($"[PlayablePrototypeSceneGenerator] Scene ready: {kScenePath}");
        }

        public static void CreatePlayablePrototypeSceneBatch()
        {
            CreateOrUpdateScene();
            Debug.Log($"[PlayablePrototypeSceneGenerator] Scene generated in batch mode: {kScenePath}");
        }

        private static void CreateOrUpdateScene()
        {
            EnsureFolder("Assets/Scenes");

            var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);

            var cameraGo = new GameObject("Main Camera");
            cameraGo.tag = "MainCamera";
            var camera = cameraGo.AddComponent<Camera>();
            camera.orthographic = true;
            camera.orthographicSize = 5.2f;
            camera.transform.position = new Vector3(-0.3f, 0.8f, -10f);
            camera.transform.rotation = Quaternion.identity;
            camera.backgroundColor = new Color(0.06f, 0.08f, 0.11f, 1f);

            var lightGo = new GameObject("Directional Light");
            var light = lightGo.AddComponent<Light>();
            light.type = LightType.Directional;
            light.intensity = 0.9f;
            light.transform.rotation = Quaternion.Euler(40f, -25f, 0f);

            var prototypeRoot = new GameObject("PlayablePrototype");
            AttachPrototypeController(prototypeRoot);

            EditorSceneManager.SaveScene(scene, kScenePath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        private static void AttachPrototypeController(GameObject root)
        {
            const string kControllerTypeName = "EventideAge.Prototype.PlayablePrototypeController, EventideAge.Prototype";
            const string kFallbackTypeName = "EventideAge.Prototype.PlayablePrototypeController, Assembly-CSharp";

            Type controllerType = Type.GetType(kControllerTypeName) ?? Type.GetType(kFallbackTypeName);
            if (controllerType == null)
            {
                Debug.LogError("[PlayablePrototypeSceneGenerator] Cannot find EventideAge.Prototype.PlayablePrototypeController. Ensure prototype scripts compile first.");
                return;
            }

            root.AddComponent(controllerType);
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
