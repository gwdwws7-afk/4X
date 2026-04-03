#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using EventideAge.Core;
using EventideAge.Config;
using EventideAge.Systems;
using EventideAge.Systems.A1;
using EventideAge.Systems.A2;
using EventideAge.Systems.A3;
using EventideAge.Systems.A4;
using EventideAge.Systems.A5;
using EventideAge.Systems.B1;
using EventideAge.Systems.B2;
using EventideAge.Systems.B3;
using EventideAge.Systems.B4;
using EventideAge.Systems.B5;
using EventideAge.Systems.C1;
using EventideAge.Systems.C2;
using EventideAge.Systems.C3;
using EventideAge.Systems.C4;
using EventideAge.Systems.C5;
using EventideAge.Systems.D1;
using EventideAge.Systems.D2;
using EventideAge.Systems.D3;
using EventideAge.Systems.D4;
using EventideAge.Systems.D5;
using EventideAge.Systems.D6;
using EventideAge.Systems.E;
using EventideAge.Systems.F1;
using EventideAge.Systems.G;
using EventideAge.Systems.H1;
using EventideAge.Systems.I1;
using EventideAge.Systems.J;
using EventideAge.UI;

namespace EventideAge.Editor
{
    public class SceneSetup : EditorWindow
    {
        private string _configPath = "Assets/ScriptableObjects/GameConfig_Default.asset";
        private string _statePath = "Assets/ScriptableObjects/GameState.asset";
        private string _eventsPath = "Assets/ScriptableObjects/GameEvents.asset";
        private string _prefabPath = "Assets/Prefabs/GameManager.prefab";

        [MenuItem("EventideAge/Setup Complete Scene")]
        public static void ShowWindow()
        {
            var window = GetWindow<SceneSetup>("Scene Setup");
            window.minSize = new Vector2(400, 300);
        }

        public void OnGUI()
        {
            GUILayout.Label("EventideAge 场景设置工具", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            GUILayout.Label("资源路径设置:", EditorStyles.boldLabel);
            _configPath = EditorGUILayout.TextField("GameConfig 路径:", _configPath);
            _statePath = EditorGUILayout.TextField("GameState 路径:", _statePath);
            _eventsPath = EditorGUILayout.TextField("GameEvents 路径:", _eventsPath);
            _prefabPath = EditorGUILayout.TextField("预制体路径:", _prefabPath);

            EditorGUILayout.Space();

            if (GUILayout.Button("1. 创建 ScriptableObject 资产", GUILayout.Height(30)))
            {
                CreateScriptableObjects();
            }

            if (GUILayout.Button("2. 创建 GameManager 预制体", GUILayout.Height(30)))
            {
                CreateGameManagerPrefab();
            }

            if (GUILayout.Button("3. 在当前场景中创建 GameManager", GUILayout.Height(30)))
            {
                CreateGameManagerInScene();
            }

            EditorGUILayout.Space();
            GUILayout.Label("执行顺序: 1 -> 2 -> 3", EditorStyles.helpBox);
        }

        private void CreateScriptableObjects()
        {
            EnsureDirectoryExists("Assets/ScriptableObjects");

            var config = DefaultGameConfig.CreateDefault();
            SaveAsset(config, _configPath);

            var state = ScriptableObject.CreateInstance<GameState>();
            state.Config = config;
            state.Initialize();
            SaveAsset(state, _statePath);

            var events = ScriptableObject.CreateInstance<GameEvents>();
            SaveAsset(events, _eventsPath);

            Selection.activeObject = config;
            EditorUtility.FocusProjectWindow();
            Debug.Log("[SceneSetup] ScriptableObjects created successfully");
        }

        private void CreateGameManagerPrefab()
        {
            EnsureDirectoryExists("Assets/Prefabs");

            var gameManagerGO = CreateGameManagerGameObject();
            var prefab = PrefabUtility.SaveAsPrefabAsset(gameManagerGO, _prefabPath);

            if (prefab != null)
            {
                Selection.activeObject = prefab;
                EditorUtility.FocusProjectWindow();
                Debug.Log($"[SceneSetup] GameManager prefab created at: {_prefabPath}");
            }
            else
            {
                Debug.LogError("[SceneSetup] Failed to create prefab");
            }

            DestroyImmediate(gameManagerGO);
        }

        private void CreateGameManagerInScene()
        {
            var existing = GameObject.Find("GameManager");
            if (existing != null)
            {
                if (!EditorUtility.DisplayDialog("警告", "场景中已存在 GameManager。是否替换?", "替换", "取消"))
                    return;
                DestroyImmediate(existing);
            }

            var gameManagerGO = CreateGameManagerGameObject();
            gameManagerGO.name = "GameManager";

            Selection.activeObject = gameManagerGO;
            SceneView.lastActiveSceneView?.Focus();
            Debug.Log("[SceneSetup] GameManager created in scene");
        }

        private GameObject CreateGameManagerGameObject()
        {
            var go = new GameObject("GameManager");

            var gameManager = go.AddComponent<GameManager>();

            var config = AssetDatabase.LoadAssetAtPath<GameConfig>(_configPath);
            var state = AssetDatabase.LoadAssetAtPath<GameState>(_statePath);
            var events = AssetDatabase.LoadAssetAtPath<GameEvents>(_eventsPath);

            if (config == null || state == null || events == null)
            {
                Debug.LogError("[SceneSetup] ScriptableObjects not found. Run 'Create ScriptableObject Assets' first.");
                return go;
            }

            gameManager.Config = config;
            gameManager.State = state;
            gameManager.Events = events;

            var systemsGO = new GameObject("Systems");
            systemsGO.transform.SetParent(go.transform);

            var systemsList = new System.Collections.Generic.List<GameSystem>();

            AddSystemChild<TurnLoopSystem>(systemsGO, systemsList);
            AddSystemChild<PhaseEngine>(systemsGO, systemsList);
            AddSystemChild<ResourceSystem>(systemsGO, systemsList);
            AddSystemChild<SaveSystem>(systemsGO, systemsList);
            AddSystemChild<GameClock>(systemsGO, systemsList);

            AddSystemChild<FinanceSystem>(systemsGO, systemsList);
            AddSystemChild<BlockadeSystem>(systemsGO, systemsList);
            AddSystemChild<TradeNetworkSystem>(systemsGO, systemsList);
            AddSystemChild<ExchangeRateSystem>(systemsGO, systemsList);
            AddSystemChild<EconomicSettlementSystem>(systemsGO, systemsList);

            AddSystemChild<DiplomaticRelationsSystem>(systemsGO, systemsList);
            AddSystemChild<DiplomaticProtocolsSystem>(systemsGO, systemsList);
            AddSystemChild<IdeologySystem>(systemsGO, systemsList);
            AddSystemChild<AllianceSystem>(systemsGO, systemsList);
            AddSystemChild<InternationalOrgsSystem>(systemsGO, systemsList);

            AddSystemChild<MilitaryOperationsSystem>(systemsGO, systemsList);
            AddSystemChild<MilitaryPoliticalLinkageSystem>(systemsGO, systemsList);
            AddSystemChild<ProxyCivilAffairsSystem>(systemsGO, systemsList);
            AddSystemChild<NuclearDeterrenceSystem>(systemsGO, systemsList);
            AddSystemChild<WarResolutionSystem>(systemsGO, systemsList);
            AddSystemChild<MilitaryTechSystem>(systemsGO, systemsList);

            AddSystemChild<InternalPoliticsSystem>(systemsGO, systemsList);
            AddSystemChild<IntelligenceSystem>(systemsGO, systemsList);
            AddSystemChild<FactionAISystem>(systemsGO, systemsList);

            AddSystemChild<StrategicMapSystem>(systemsGO, systemsList);

            AddSystemChild<EventSystem>(systemsGO, systemsList);
            AddSystemChild<VictoryDefeatSystem>(systemsGO, systemsList);

            gameManager.Systems = systemsList;

            SetupSystemReferences(gameManager);

            var uiGO = new GameObject("UI");
            uiGO.transform.SetParent(go.transform);
            var uiManager = uiGO.AddComponent<UIManager>();
            uiManager.Initialize(state, events);

            Debug.Log($"[SceneSetup] GameManager created with {systemsList.Count} systems");
            return go;
        }

        private void AddSystemChild<T>(GameObject parent, System.Collections.Generic.List<GameSystem> systemsList) where T : GameSystem
        {
            var go = new GameObject(typeof(T).Name);
            go.transform.SetParent(parent.transform);
            var system = go.AddComponent<T>();
            systemsList.Add(system);
        }

        private void SetupSystemReferences(GameManager gameManager)
        {
            var d1 = gameManager.GetComponentInChildren<MilitaryOperationsSystem>()?.GetComponent<MilitaryOperationsSystem>();
            var d2 = gameManager.GetComponentInChildren<MilitaryPoliticalLinkageSystem>()?.GetComponent<MilitaryPoliticalLinkageSystem>();
            var d4 = gameManager.GetComponentInChildren<NuclearDeterrenceSystem>()?.GetComponent<NuclearDeterrenceSystem>();
            var d5 = gameManager.GetComponentInChildren<WarResolutionSystem>()?.GetComponent<WarResolutionSystem>();
            var d6 = gameManager.GetComponentInChildren<MilitaryTechSystem>()?.GetComponent<MilitaryTechSystem>();
            var j = gameManager.GetComponentInChildren<VictoryDefeatSystem>()?.GetComponent<VictoryDefeatSystem>();
            var b2 = gameManager.GetComponentInChildren<BlockadeSystem>()?.GetComponent<BlockadeSystem>();

            if (d1 != null)
            {
                d1.PoliticalLinkageSystem = d2;
                d1.MilitaryTechSystem = d6;
                d1.VictoryDefeatSystem = j;
            }

            if (d5 != null)
            {
                d5.VictoryDefeatSystem = j;
            }

            if (b2 != null)
            {
                b2.VictoryDefeatSystem = j;
            }

            Debug.Log("[SceneSetup] System references wired");
        }

        private void SaveAsset(Object obj, string path)
        {
            if (AssetDatabase.Contains(obj))
            {
                EditorUtility.SetDirty(obj);
                AssetDatabase.SaveAssets();
            }
            else
            {
                AssetDatabase.CreateAsset(obj, path);
                AssetDatabase.SaveAssets();
            }
        }

        private void EnsureDirectoryExists(string path)
        {
            if (!AssetDatabase.IsValidFolder(path))
            {
                var parts = path.Split('/');
                string current = parts[0];
                for (int i = 1; i < parts.Length; i++)
                {
                    if (!AssetDatabase.IsValidFolder(current + "/" + parts[i]))
                    {
                        AssetDatabase.CreateFolder(current, parts[i]);
                    }
                    current += "/" + parts[i];
                }
            }
        }
    }
}
#endif