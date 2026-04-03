#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

namespace EventideAge.Editor
{
    public class SceneCreator
    {
        [MenuItem("EventideAge/Create Main Game Scene")]
        public static void CreateMainGameScene()
        {
            string path = EditorUtility.SaveFilePanelInProject(
                "Save Main Game Scene",
                "MainGameScene",
                "unity",
                "Select where to save the main game scene"
            );

            if (!string.IsNullOrEmpty(path))
            {
                var scene = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Additive);
                EditorSceneManager.SaveScene(scene, path);
                EditorSceneManager.OpenScene(path);
                
                Selection.activeObject = scene;
                Debug.Log($"[SceneCreator] Created main game scene at: {path}");
            }
        }

        [MenuItem("EventideAge/Create Boot Scene")]
        public static void CreateBootScene()
        {
            string path = EditorUtility.SaveFilePanelInProject(
                "Save Boot Scene",
                "BootScene",
                "unity",
                "Select where to save the boot scene"
            );

            if (!string.IsNullOrEmpty(path))
            {
                var scene = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Additive);
                
                var go = new GameObject("Bootstrap");
                go.AddComponent<Bootstrap>();
                
                EditorSceneManager.SaveScene(scene, path);
                EditorSceneManager.OpenScene(path);
                
                Selection.activeObject = scene;
                Debug.Log($"[SceneCreator] Created boot scene at: {path}");
            }
        }
    }

    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private string mainScenePath = "Assets/Scenes/MainGameScene.unity";

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            if (!string.IsNullOrEmpty(mainScenePath))
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(mainScenePath);
            }
        }
    }
}
#endif