#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using EventideAge.Core;
using EventideAge.Config;

namespace EventideAge.Editor
{
    public class ProjectSetup : EditorWindow
    {
        [MenuItem("EventideAge/Setup Project")]
        public static void ShowWindow()
        {
            GetWindow<ProjectSetup>("EventideAge Setup");
        }
        
        public static void SetupProject()
        {
            CreateCoreAssets();
            CreateConfigAssets();
            SetupGameManager();
            SetupScene();
            AssetDatabase.SaveAssets();
            Debug.Log("[EventideAge] Project setup complete!");
        }
        
        private static void CreateCoreAssets()
        {
            string corePath = "Assets/ScriptableObjects/Core";
            EnsureDirectory(corePath);
            
            var state = CreateAsset<GameState>(corePath, "GameState");
            var events = CreateAsset<GameEvents>(corePath, "GameEvents");
            
            Debug.Log($"[Setup] Created GameState and GameEvents at {corePath}");
        }
        
        private static void CreateConfigAssets()
        {
            string configPath = "Assets/ScriptableObjects/Config";
            EnsureDirectory(configPath);
            
            var config = CreateAsset<DefaultGameConfig>(configPath, "DefaultGameConfig");
            
            GameState state = Resources.Load<GameState>("Core/GameState");
            GameEvents events = Resources.Load<GameEvents>("Core/GameEvents");
            
            if (state != null)
            {
                state.Config = config;
                EditorUtility.SetDirty(state);
            }
            
            Debug.Log($"[Setup] Created DefaultGameConfig at {configPath}");
        }
        
        private static void SetupGameManager()
        {
            var gameManager = GameObject.Find("GameManager");
            if (gameManager == null)
            {
                gameManager = new GameObject("GameManager");
                Undo.RegisterCreatedObjectUndo(gameManager, "Create GameManager");
            }
            
            var gm = gameManager.GetComponent<Core.GameManager>();
            if (gm == null)
            {
                gm = gameManager.AddComponent<Core.GameManager>();
            }
            
            var turnLoop = gameManager.GetComponent<Systems.A1.TurnLoopSystem>();
            if (turnLoop == null) turnLoop = gameManager.AddComponent<Systems.A1.TurnLoopSystem>();
            
            var phaseEngine = gameManager.GetComponent<Systems.A2.PhaseEngine>();
            if (phaseEngine == null) phaseEngine = gameManager.AddComponent<Systems.A2.PhaseEngine>();
            
            var resourceSystem = gameManager.GetComponent<Systems.A3.ResourceSystem>();
            if (resourceSystem == null) resourceSystem = gameManager.AddComponent<Systems.A3.ResourceSystem>();
            
            var saveSystem = gameManager.GetComponent<Systems.A4.SaveSystem>();
            if (saveSystem == null) saveSystem = gameManager.AddComponent<Systems.A4.SaveSystem>();
            
            var gameClock = gameManager.GetComponent<Systems.A5.GameClock>();
            if (gameClock == null) gameClock = gameManager.AddComponent<Systems.A5.GameClock>();
            
            var mapSystem = gameManager.GetComponent<Systems.H1.StrategicMapSystem>();
            if (mapSystem == null) mapSystem = gameManager.AddComponent<Systems.H1.StrategicMapSystem>();
            
            var uiManager = gameManager.GetComponent<UI.UIManager>();
            if (uiManager == null) uiManager = gameManager.AddComponent<UI.UIManager>();
            
            string soPath = "Assets/ScriptableObjects";
            var stateSO = Resources.Load<GameState>("Core/GameState");
            var eventsSO = Resources.Load<GameEvents>("Core/GameEvents");
            var configSO = Resources.Load<DefaultGameConfig>("Config/DefaultGameConfig");
            
            if (stateSO != null) gm.State = stateSO;
            if (eventsSO != null) gm.Events = eventsSO;
            if (configSO != null) gm.Config = configSO;
            
            var systems = new Core.GameSystem[]
            {
                turnLoop, phaseEngine, resourceSystem, saveSystem,
                gameClock, mapSystem, uiManager
            };
            
            var systemList = new System.Collections.Generic.List<Core.GameSystem>();
            foreach (var s in systems)
            {
                if (s != null) systemList.Add(s);
            }
            gm.Systems = systemList;
            
            Selection.activeGameObject = gameManager;
            EditorUtility.SetDirty(gm);
            
            Debug.Log("[Setup] GameManager configured with all systems");
        }
        
        private static void SetupScene()
        {
            if (Camera.main != null)
            {
                Camera.main.transform.position = new Vector3(0, 10, -10);
                Camera.main.transform.LookAt(Vector3.zero);
            }
            
            var canvas = GameObject.Find("UICanvas");
            if (canvas == null)
            {
                var canvasObj = new GameObject("UICanvas");
                var canvasComp = canvasObj.AddComponent<UnityEngine.Canvas>();
                canvasComp.renderMode = RenderMode.ScreenSpaceOverlay;
                canvasObj.AddComponent<UnityEngine.UI.CanvasScaler>();
                canvasObj.AddComponent<UnityEngine.UI.GraphicRaycaster>();
                
                Undo.RegisterCreatedObjectUndo(canvasObj, "Create UICanvas");
            }
            
            var eventSystem = GameObject.Find("EventSystem");
            if (eventSystem == null)
            {
                eventSystem = new GameObject("EventSystem");
                eventSystem.AddComponent<UnityEngine.EventSystems.EventSystem>();
                eventSystem.AddComponent<UnityEngine.EventSystems.StandaloneInputModule>();
                Undo.RegisterCreatedObjectUndo(eventSystem, "Create EventSystem");
            }
            
            Debug.Log("[Setup] Scene UI configured");
        }
        
        private static T CreateAsset<T>(string path, string name) where T : ScriptableObject
        {
            EnsureDirectory(path);
            string fullPath = $"{path}/{name}.asset";
            
            T asset = AssetDatabase.LoadAssetAtPath<T>(fullPath);
            if (asset == null)
            {
                asset = CreateInstance<T>();
                AssetDatabase.CreateAsset(asset, fullPath);
                EditorUtility.SetDirty(asset);
            }
            return asset;
        }
        
        private static void EnsureDirectory(string path)
        {
            if (!AssetDatabase.IsValidFolder(path))
            {
                string parent = System.IO.Path.GetDirectoryName(path);
                if (!string.IsNullOrEmpty(parent) && !AssetDatabase.IsValidFolder(parent))
                {
                    EnsureDirectory(parent);
                }
                AssetDatabase.CreateFolder(parent == "" ? "Assets" : parent, System.IO.Path.GetFileName(path));
            }
        }
        
        private void OnGUI()
        {
            GUILayout.Label("EventideAge Project Setup", EditorStyles.boldLabel);
            GUILayout.Space(10);
            
            if (GUILayout.Button("Setup Entire Project", GUILayout.Height(40)))
            {
                SetupProject();
            }
            
            GUILayout.Space(10);
            EditorGUILayout.HelpBox(
                "This will:\n" +
                "1. Create ScriptableObject assets (GameState, GameEvents)\n" +
                "2. Create DefaultGameConfig\n" +
                "3. Configure GameManager with all systems\n" +
                "4. Setup basic UI Canvas",
                MessageType.Info);
        }
    }
}
#endif
