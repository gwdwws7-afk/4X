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
using EventideAge.Systems.H2;
using EventideAge.Systems.H3;
using EventideAge.Systems.I1;
using EventideAge.Systems.J;
using EventideAge.UI;

namespace EventideAge.Editor
{
    public class SceneSetup : EditorWindow
    {
        private const string kDefaultConfigPath = "Assets/ScriptableObjects/GameConfig_Default.asset";
        private const string kDefaultStatePath = "Assets/ScriptableObjects/GameState.asset";
        private const string kDefaultEventsPath = "Assets/ScriptableObjects/GameEvents.asset";
        private const string kDefaultPrefabPath = "Assets/Prefabs/GameManager.prefab";

        private string _configPath = kDefaultConfigPath;
        private string _statePath = kDefaultStatePath;
        private string _eventsPath = kDefaultEventsPath;
        private string _prefabPath = kDefaultPrefabPath;

        [MenuItem("EventideAge/Setup Complete Scene")]
        public static void SetupCompleteSceneQuick()
        {
            RunOneClickSetup();
        }

        [MenuItem("EventideAge/Setup Complete Scene (Window)")]
        public static void ShowWindow()
        {
            var window = GetWindow<SceneSetup>("Scene Setup");
            window.minSize = new Vector2(420, 320);
        }

        public static void RunOneClickSetup()
        {
            var setup = CreateInstance<SceneSetup>();
            setup._configPath = kDefaultConfigPath;
            setup._statePath = kDefaultStatePath;
            setup._eventsPath = kDefaultEventsPath;
            setup._prefabPath = kDefaultPrefabPath;

            try
            {
                setup.CreateScriptableObjects();
                setup.CreateGameManagerInScene(false);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                Debug.Log("[SceneSetup] One-click setup completed in current scene.");
            }
            finally
            {
                DestroyImmediate(setup);
            }
        }

        public void OnGUI()
        {
            GUILayout.Label("EventideAge Scene Setup", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            GUILayout.Label("Asset Paths", EditorStyles.boldLabel);
            _configPath = EditorGUILayout.TextField("GameConfig Path", _configPath);
            _statePath = EditorGUILayout.TextField("GameState Path", _statePath);
            _eventsPath = EditorGUILayout.TextField("GameEvents Path", _eventsPath);
            _prefabPath = EditorGUILayout.TextField("Prefab Path", _prefabPath);

            EditorGUILayout.Space();

            if (GUILayout.Button("1. Create/Update ScriptableObjects", GUILayout.Height(30)))
            {
                CreateScriptableObjects();
            }

            if (GUILayout.Button("2. Create/Update GameManager Prefab", GUILayout.Height(30)))
            {
                CreateGameManagerPrefab();
            }

            if (GUILayout.Button("3. Create/Replace GameManager In Current Scene", GUILayout.Height(30)))
            {
                CreateGameManagerInScene(true);
            }

            if (GUILayout.Button("Run One-Click Setup (Recommended)", GUILayout.Height(34)))
            {
                CreateScriptableObjects();
                CreateGameManagerInScene(true);
            }

            EditorGUILayout.Space();
            EditorGUILayout.HelpBox(
                "Recommended path:\n" +
                "1) EventideAge -> Setup Complete Scene (one click)\n" +
                "2) EventideAge -> Run All Tests\n" +
                "This window remains available for fine-grained setup.",
                MessageType.Info);
        }

        private void CreateScriptableObjects()
        {
            EnsureDirectoryExists("Assets/ScriptableObjects");

            var config = AssetDatabase.LoadAssetAtPath<GameConfig>(_configPath);
            if (config == null)
            {
                config = DefaultGameConfig.CreateDefault();
                SaveAsset(config, _configPath);
            }
            else
            {
                EditorUtility.SetDirty(config);
            }

            var state = AssetDatabase.LoadAssetAtPath<GameState>(_statePath);
            if (state == null)
            {
                state = ScriptableObject.CreateInstance<GameState>();
                SaveAsset(state, _statePath);
            }

            state.Config = config;
            state.Initialize();
            EditorUtility.SetDirty(state);

            var events = AssetDatabase.LoadAssetAtPath<GameEvents>(_eventsPath);
            if (events == null)
            {
                events = ScriptableObject.CreateInstance<GameEvents>();
                SaveAsset(events, _eventsPath);
            }
            else
            {
                EditorUtility.SetDirty(events);
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Selection.activeObject = state;
            EditorUtility.FocusProjectWindow();
            Debug.Log("[SceneSetup] ScriptableObjects ready (GameConfig/GameState/GameEvents).");
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
                Debug.Log($"[SceneSetup] GameManager prefab updated: {_prefabPath}");
            }
            else
            {
                Debug.LogError("[SceneSetup] Failed to create/update GameManager prefab.");
            }

            DestroyImmediate(gameManagerGO);
        }

        private void CreateGameManagerInScene(bool askBeforeReplace)
        {
            var existing = GameObject.Find("GameManager");
            if (existing != null)
            {
                if (askBeforeReplace)
                {
                    bool replace = EditorUtility.DisplayDialog(
                        "Replace Existing GameManager?",
                        "A GameManager already exists in this scene. Replace it with a fresh one?",
                        "Replace",
                        "Cancel");
                    if (!replace)
                        return;
                }

                DestroyImmediate(existing);
            }

            var gameManagerGO = CreateGameManagerGameObject();
            gameManagerGO.name = "GameManager";

            Selection.activeObject = gameManagerGO;
            SceneView.lastActiveSceneView?.Focus();
            Debug.Log("[SceneSetup] GameManager created in current scene.");
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
                Debug.LogError("[SceneSetup] Missing ScriptableObjects. Create assets first.");
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
            AddSystemChild<NodeNetworkSystem>(systemsGO, systemsList);
            AddSystemChild<TerrainVisionSystem>(systemsGO, systemsList);

            AddSystemChild<EventSystem>(systemsGO, systemsList);
            AddSystemChild<VictoryDefeatSystem>(systemsGO, systemsList);

            gameManager.Systems = systemsList;

            SetupSystemReferences(gameManager);

            var uiGO = new GameObject("UI");
            uiGO.transform.SetParent(go.transform);
            AddSystemChild<UIManager>(uiGO, systemsList);
            AddSystemChild<ActionLogUI>(uiGO, systemsList);
            AddSystemChild<ConsequencePanelUI>(uiGO, systemsList);
            AddSystemChild<GlobalAlertUI>(uiGO, systemsList);
            AddSystemChild<ActionPanelUI>(uiGO, systemsList);
            AddSystemChild<DiplomacyPanelUI>(uiGO, systemsList);
            AddSystemChild<MapPanelUI>(uiGO, systemsList);
            AddSystemChild<NotificationPanelUI>(uiGO, systemsList);
            AddSystemChild<AlertPanelUI>(uiGO, systemsList);
            AddSystemChild<EventPanelUI>(uiGO, systemsList);
            AddSystemChild<IntelPanelUI>(uiGO, systemsList);

            Debug.Log($"[SceneSetup] GameManager graph built with {systemsList.Count} systems/components.");
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
            var d5 = gameManager.GetComponentInChildren<WarResolutionSystem>()?.GetComponent<WarResolutionSystem>();
            var d6 = gameManager.GetComponentInChildren<MilitaryTechSystem>()?.GetComponent<MilitaryTechSystem>();
            var j = gameManager.GetComponentInChildren<VictoryDefeatSystem>()?.GetComponent<VictoryDefeatSystem>();
            var b2 = gameManager.GetComponentInChildren<BlockadeSystem>()?.GetComponent<BlockadeSystem>();
            var g = gameManager.GetComponentInChildren<FactionAISystem>()?.GetComponent<FactionAISystem>();
            var h1 = gameManager.GetComponentInChildren<StrategicMapSystem>()?.GetComponent<StrategicMapSystem>();
            var h2 = gameManager.GetComponentInChildren<NodeNetworkSystem>()?.GetComponent<NodeNetworkSystem>();
            var h3 = gameManager.GetComponentInChildren<TerrainVisionSystem>()?.GetComponent<TerrainVisionSystem>();
            var f1 = gameManager.GetComponentInChildren<IntelligenceSystem>()?.GetComponent<IntelligenceSystem>();

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

            if (g != null)
            {
                g.NodeNetworkSystem = h2;
            }

            if (h1 != null)
            {
                h1.NodeNetworkSystem = h2;
            }

            if (h3 != null)
            {
                h3.NodeNetworkSystem = h2;
                h3.IntelligenceSystem = f1;
            }

            if (f1 != null)
            {
                f1.TerrainVisionSystem = h3;
            }

            Debug.Log("[SceneSetup] Cross-system references wired.");
        }

        private void SaveAsset(Object obj, string path)
        {
            if (AssetDatabase.Contains(obj))
            {
                EditorUtility.SetDirty(obj);
                AssetDatabase.SaveAssets();
                return;
            }

            var existing = AssetDatabase.LoadMainAssetAtPath(path);
            if (existing != null)
            {
                Object.DestroyImmediate(obj);
                EditorUtility.SetDirty(existing);
                AssetDatabase.SaveAssets();
                return;
            }

            AssetDatabase.CreateAsset(obj, path);
            AssetDatabase.SaveAssets();
        }

        private void EnsureDirectoryExists(string path)
        {
            if (AssetDatabase.IsValidFolder(path))
                return;

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
#endif
