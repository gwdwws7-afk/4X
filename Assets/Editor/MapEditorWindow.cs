#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using EventideAge.Core;
using System.Collections.Generic;

namespace EventideAge.Editor
{
    public class MapEditorWindow : EditorWindow
    {
        private GameConfig _targetConfig;
        private Vector2 _scrollPosition;
        private int _selectedRegionIndex = -1;
        private int _selectedNodeIndex = -1;
        private string[] _factionIds = new string[] { "Vashid", "GoldLeader", "HolyFire", "NorthAlliance", "EastTrader", "AshCloud", "Neutral" };

        [MenuItem("EventideAge/Map Editor")]
        public static void ShowWindow()
        {
            var window = GetWindow<MapEditorWindow>("地图编辑器");
            window.minSize = new Vector2(800, 600);
        }

        public void OnEnable()
        {
            _targetConfig = Resources.Load<GameConfig>("Config/GameConfig_Default");
            if (_targetConfig == null)
            {
                var guids = AssetDatabase.FindAssets("t:GameConfig");
                if (guids.Length > 0)
                {
                    _targetConfig = AssetDatabase.LoadAssetAtPath<GameConfig>(AssetDatabase.GUIDToAssetPath(guids[0]));
                }
            }
        }

        public void OnGUI()
        {
            EditorGUILayout.Space(10);
            EditorGUILayout.LabelField("瓦希德帝国 - 地图编辑器", EditorStyles.boldLabel);
            EditorGUILayout.Space(5);

            EditorGUILayout.BeginHorizontal();
            _targetConfig = (GameConfig)EditorGUILayout.ObjectField("游戏配置:", _targetConfig, typeof(GameConfig), false);
            if (GUILayout.Button("加载配置", GUILayout.Width(80)))
            {
                LoadConfig();
            }
            if (GUILayout.Button("保存配置", GUILayout.Width(80)))
            {
                SaveConfig();
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space(10);

            if (_targetConfig == null)
            {
                EditorGUILayout.HelpBox("请先创建或加载 GameConfig!", MessageType.Warning);
                EditorGUILayout.Space();
                if (GUILayout.Button("创建默认配置"))
                {
                    CreateDefaultConfig();
                }
                return;
            }

            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.BeginVertical(GUI.skin.box, GUILayout.Width(250));
            DrawRegionList();
            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginVertical(GUI.skin.box, GUILayout.Width(250));
            DrawNodeList();
            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginVertical(GUI.skin.box);
            DrawNodeDetails();
            EditorGUILayout.EndVertical();

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space(10);
            DrawBottomControls();
        }

        private void LoadConfig()
        {
            string path = EditorUtility.OpenFilePanel("加载配置", "Assets", "asset");
            if (!string.IsNullOrEmpty(path))
            {
                path = "Assets" + path.Substring(Application.dataPath.Length);
                _targetConfig = AssetDatabase.LoadAssetAtPath<GameConfig>(path);
                _selectedRegionIndex = -1;
                _selectedNodeIndex = -1;
            }
        }

        private void SaveConfig()
        {
            if (_targetConfig == null) return;
            EditorUtility.SetDirty(_targetConfig);
            AssetDatabase.SaveAssets();
            EditorUtility.DisplayDialog("保存成功", "配置已保存!", "OK");
        }

        private void CreateDefaultConfig()
        {
            var config = ScriptableObject.CreateInstance<GameConfig>();

            config.PhaseConfigs = new PhaseConfig[]
            {
                new PhaseConfig { PhaseName = "外交", BaseActionPoints = 2, SortOrder = 0 },
                new PhaseConfig { PhaseName = "战略", BaseActionPoints = 2, SortOrder = 1 },
                new PhaseConfig { PhaseName = "作战", BaseActionPoints = 3, SortOrder = 2 },
                new PhaseConfig { PhaseName = "后勤", BaseActionPoints = 2, SortOrder = 3 },
                new PhaseConfig { PhaseName = "情报", BaseActionPoints = 1, SortOrder = 4 },
                new PhaseConfig { PhaseName = "AI响应", BaseActionPoints = 1, SortOrder = 5 }
            };

            config.FactionConfigs = new FactionConfig[]
            {
                new FactionConfig { FactionId = "Vashid", FactionName = "瓦希德帝国", IsPlayerControlled = true, InitialControlledPoints = 100, InitialRelationship = 100 },
                new FactionConfig { FactionId = "GoldLeader", FactionName = "黄金领", IsPlayerControlled = false, InitialControlledPoints = 80, InitialRelationship = -80 },
                new FactionConfig { FactionId = "HolyFire", FactionName = "圣火序", IsPlayerControlled = false, InitialControlledPoints = 60, InitialRelationship = -60 },
                new FactionConfig { FactionId = "NorthAlliance", FactionName = "北境共主", IsPlayerControlled = false, InitialControlledPoints = 50, InitialRelationship = 40 },
                new FactionConfig { FactionId = "EastTrader", FactionName = "东土商盟", IsPlayerControlled = false, InitialControlledPoints = 40, InitialRelationship = 30 },
                new FactionConfig { FactionId = "AshCloud", FactionName = "灰烬众", IsPlayerControlled = false, InitialControlledPoints = 30, InitialRelationship = 70 }
            };

            config.ResourceConfigs = new ResourceConfig[]
            {
                new ResourceConfig { ResourceId = "GoldLeaf", ResourceName = "金叶", InitialAmount = 200, MaxCapacity = 2000, ResourceType = ResourceType.Consumable },
                new ResourceConfig { ResourceId = "GoldLeafReserve", ResourceName = "金叶储备", InitialAmount = 500, MaxCapacity = 5000, ResourceType = ResourceType.Accumulative },
                new ResourceConfig { ResourceId = "Arms", ResourceName = "战械", InitialAmount = 80, MaxCapacity = 300, ResourceType = ResourceType.Consumable },
                new ResourceConfig { ResourceId = "Energy", ResourceName = "能源", InitialAmount = 200, MaxCapacity = 1000, ResourceType = ResourceType.Accumulative },
                new ResourceConfig { ResourceId = "SocialValue", ResourceName = "社稷值", InitialAmount = 60, MaxCapacity = 100, ResourceType = ResourceType.Ratio },
                new ResourceConfig { ResourceId = "AshWill", ResourceName = "灰烬志", InitialAmount = 50, MaxCapacity = 100, ResourceType = ResourceType.Ratio },
                new ResourceConfig { ResourceId = "TradeToken", ResourceName = "商盟券", InitialAmount = 40, MaxCapacity = 300, ResourceType = ResourceType.Consumable },
                new ResourceConfig { ResourceId = "NorthCoins", ResourceName = "北境银", InitialAmount = 30, MaxCapacity = 200, ResourceType = ResourceType.Consumable },
                new ResourceConfig { ResourceId = "Prestige", ResourceName = "朝贡序", InitialAmount = 30, MaxCapacity = 100, ResourceType = ResourceType.Ratio }
            };

            string path = EditorUtility.SaveFilePanelInProject("保存配置", "GameConfig_Custom", "asset", "选择保存位置");
            if (!string.IsNullOrEmpty(path))
            {
                AssetDatabase.CreateAsset(config, path);
                AssetDatabase.SaveAssets();
                _targetConfig = config;
                EditorUtility.DisplayDialog("创建成功", "配置已创建!", "OK");
            }
        }

        private void DrawRegionList()
        {
            EditorGUILayout.LabelField("地区列表", EditorStyles.boldLabel);

            if (_targetConfig.RegionConfigs == null || _targetConfig.RegionConfigs.Length == 0)
            {
                EditorGUILayout.HelpBox("没有地区数据", MessageType.Info);
                EditorGUILayout.Space();
                if (GUILayout.Button("添加地区"))
                {
                    AddNewRegion();
                }
                return;
            }

            _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);

            for (int i = 0; i < _targetConfig.RegionConfigs.Length; i++)
            {
                if (_targetConfig.RegionConfigs[i] == null) continue;

                EditorGUILayout.BeginHorizontal();
                bool isSelected = (_selectedRegionIndex == i);

                string buttonText = _targetConfig.RegionConfigs[i].RegionName;
                if (GUILayout.Button(buttonText, isSelected ? EditorStyles.foldoutHeader : GUI.skin.button))
                {
                    _selectedRegionIndex = i;
                    _selectedNodeIndex = -1;
                }

                if (GUILayout.Button("X", GUILayout.Width(25)))
                {
                    DeleteRegion(i);
                    GUIUtility.ExitGUI();
                    return;
                }
                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.EndScrollView();

            EditorGUILayout.Space(5);
            if (GUILayout.Button("添加地区"))
            {
                AddNewRegion();
            }
        }

        private void DrawNodeList()
        {
            EditorGUILayout.LabelField("节点列表", EditorStyles.boldLabel);

            if (_selectedRegionIndex < 0 || _targetConfig.RegionConfigs == null ||
                _selectedRegionIndex >= _targetConfig.RegionConfigs.Length ||
                _targetConfig.RegionConfigs[_selectedRegionIndex] == null)
            {
                EditorGUILayout.HelpBox("请先选择一个地区", MessageType.Info);
                return;
            }

            var region = _targetConfig.RegionConfigs[_selectedRegionIndex];

            if (region.NodeConfigs == null || region.NodeConfigs.Length == 0)
            {
                EditorGUILayout.HelpBox("该地区没有节点", MessageType.Info);
                EditorGUILayout.Space();
                if (GUILayout.Button("添加节点"))
                {
                    AddNewNode();
                }
                return;
            }

            _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);

            for (int i = 0; i < region.NodeConfigs.Length; i++)
            {
                if (region.NodeConfigs[i] == null) continue;

                EditorGUILayout.BeginHorizontal();
                bool isSelected = (_selectedNodeIndex == i);

                string buttonText = $"[{region.NodeConfigs[i].NodeType}] {region.NodeConfigs[i].NodeName}";
                if (GUILayout.Button(buttonText, isSelected ? EditorStyles.foldoutHeader : GUI.skin.button))
                {
                    _selectedNodeIndex = i;
                }

                if (GUILayout.Button("X", GUILayout.Width(25)))
                {
                    DeleteNode(i);
                    GUIUtility.ExitGUI();
                    return;
                }
                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.EndScrollView();

            EditorGUILayout.Space(5);
            if (GUILayout.Button("添加节点"))
            {
                AddNewNode();
            }
        }

        private void DrawNodeDetails()
        {
            EditorGUILayout.LabelField("节点详情", EditorStyles.boldLabel);

            if (_selectedRegionIndex < 0 || _selectedNodeIndex < 0)
            {
                EditorGUILayout.HelpBox("请先选择一个地区和节点", MessageType.Info);
                return;
            }

            var region = _targetConfig.RegionConfigs[_selectedRegionIndex];
            if (region == null || region.NodeConfigs == null || _selectedNodeIndex >= region.NodeConfigs.Length)
                return;

            var node = region.NodeConfigs[_selectedNodeIndex];
            if (node == null) return;

            EditorGUILayout.Space(5);

            node.NodeId = EditorGUILayout.TextField("节点ID:", node.NodeId);
            node.NodeName = EditorGUILayout.TextField("节点名称:", node.NodeName);

            EditorGUILayout.Space(3);
            EditorGUILayout.LabelField("节点类型:", EditorStyles.miniLabel);
            node.NodeType = (NodeType)EditorGUILayout.EnumPopup(node.NodeType);

            node.DefenseBonus = EditorGUILayout.IntField("防御加成:", node.DefenseBonus);

            EditorGUILayout.Space(3);
            EditorGUILayout.LabelField("控制势力:", EditorStyles.miniLabel);
            int factionIndex = System.Array.IndexOf(_factionIds, node.InitialController);
            if (factionIndex < 0) factionIndex = 0;
            factionIndex = EditorGUILayout.Popup(factionIndex, _factionIds);
            node.InitialController = _factionIds[factionIndex];

            node.InitialControlPoints = EditorGUILayout.IntSlider("控制点数:", node.InitialControlPoints, 0, 100);
            node.MaxControlPoints = EditorGUILayout.IntSlider("最大控制点:", node.MaxControlPoints, 1, 100);

            EditorGUILayout.Space(10);
            if (GUILayout.Button("应用到GameState"))
            {
                ApplyToGameState();
            }
        }

        private void DrawBottomControls()
        {
            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("创建测试GameState"))
            {
                CreateTestGameState();
            }

            if (GUILayout.Button("导出地图数据"))
            {
                ExportMapData();
            }

            if (GUILayout.Button("验证数据"))
            {
                ValidateMapData();
            }

            EditorGUILayout.EndHorizontal();
        }

        private void AddNewRegion()
        {
            if (_targetConfig.RegionConfigs == null)
                _targetConfig.RegionConfigs = new RegionConfig[0];

            int newIndex = _targetConfig.RegionConfigs.Length;
            System.Array.Resize(ref _targetConfig.RegionConfigs, newIndex + 1);

            _targetConfig.RegionConfigs[newIndex] = new RegionConfig
            {
                RegionId = $"Region_{newIndex}",
                RegionName = $"新地区 {newIndex + 1}",
                NodeConfigs = new NodeConfig[0]
            };

            _selectedRegionIndex = newIndex;
            _selectedNodeIndex = -1;
            EditorUtility.SetDirty(_targetConfig);
        }

        private void DeleteRegion(int index)
        {
            if (_targetConfig.RegionConfigs == null || index < 0 || index >= _targetConfig.RegionConfigs.Length)
                return;

            if (!EditorUtility.DisplayDialog("确认删除", $"确定要删除地区 '{_targetConfig.RegionConfigs[index].RegionName}' 吗?", "删除", "取消"))
                return;

            var list = new List<RegionConfig>(_targetConfig.RegionConfigs);
            list.RemoveAt(index);
            _targetConfig.RegionConfigs = list.ToArray();

            _selectedRegionIndex = -1;
            _selectedNodeIndex = -1;
            EditorUtility.SetDirty(_targetConfig);
        }

        private void AddNewNode()
        {
            if (_selectedRegionIndex < 0 || _targetConfig.RegionConfigs[_selectedRegionIndex] == null)
                return;

            var region = _targetConfig.RegionConfigs[_selectedRegionIndex];

            if (region.NodeConfigs == null)
                region.NodeConfigs = new NodeConfig[0];

            int newIndex = region.NodeConfigs.Length;
            System.Array.Resize(ref region.NodeConfigs, newIndex + 1);

            region.NodeConfigs[newIndex] = new NodeConfig
            {
                NodeId = $"Node_{_selectedRegionIndex}_{newIndex}",
                NodeName = $"新节点 {newIndex + 1}",
                NodeType = NodeType.City,
                DefenseBonus = 10,
                InitialController = "Neutral",
                InitialControlPoints = 50,
                MaxControlPoints = 100
            };

            _selectedNodeIndex = newIndex;
            EditorUtility.SetDirty(_targetConfig);
        }

        private void DeleteNode(int index)
        {
            if (_selectedRegionIndex < 0)
                return;

            var region = _targetConfig.RegionConfigs[_selectedRegionIndex];
            if (region == null || region.NodeConfigs == null || index < 0 || index >= region.NodeConfigs.Length)
                return;

            if (!EditorUtility.DisplayDialog("确认删除", $"确定要删除节点 '{region.NodeConfigs[index].NodeName}' 吗?", "删除", "取消"))
                return;

            var list = new List<NodeConfig>(region.NodeConfigs);
            list.RemoveAt(index);
            region.NodeConfigs = list.ToArray();

            _selectedNodeIndex = -1;
            EditorUtility.SetDirty(_targetConfig);
        }

        private void ApplyToGameState()
        {
            var state = Resources.Load<GameState>("GameState_Default");
            if (state == null)
            {
                var guids = AssetDatabase.FindAssets("t:GameState");
                if (guids.Length > 0)
                {
                    state = AssetDatabase.LoadAssetAtPath<GameState>(AssetDatabase.GUIDToAssetPath(guids[0]));
                }
            }

            if (state == null)
            {
                EditorUtility.DisplayDialog("错误", "找不到GameState资源!", "OK");
                return;
            }

            state.Initialize();
            EditorUtility.SetDirty(state);
            AssetDatabase.SaveAssets();
            EditorUtility.DisplayDialog("成功", "地图数据已应用到GameState!", "OK");
        }

        private void CreateTestGameState()
        {
            var state = ScriptableObject.CreateInstance<GameState>();
            state.Config = _targetConfig;
            state.Initialize();

            string path = EditorUtility.SaveFilePanelInProject("保存GameState", "GameState_Test", "asset", "选择保存位置");
            if (!string.IsNullOrEmpty(path))
            {
                AssetDatabase.CreateAsset(state, path);
                AssetDatabase.SaveAssets();
                EditorUtility.DisplayDialog("创建成功", "测试GameState已创建!", "OK");
            }
        }

        private void ExportMapData()
        {
            if (_targetConfig.RegionConfigs == null || _targetConfig.RegionConfigs.Length == 0)
            {
                EditorUtility.DisplayDialog("导出失败", "没有地区数据可导出!", "OK");
                return;
            }

            string path = EditorUtility.SaveFilePanel("导出地图数据", Application.dataPath, "map_data", "json");
            if (string.IsNullOrEmpty(path))
                return;

            var exportData = new MapExportData();
            exportData.Regions = new List<RegionExportData>();

            foreach (var region in _targetConfig.RegionConfigs)
            {
                if (region == null) continue;

                var regionData = new RegionExportData
                {
                    RegionId = region.RegionId,
                    RegionName = region.RegionName,
                    Nodes = new List<NodeExportData>()
                };

                if (region.NodeConfigs != null)
                {
                    foreach (var node in region.NodeConfigs)
                    {
                        if (node == null) continue;
                        regionData.Nodes.Add(new NodeExportData
                        {
                            NodeId = node.NodeId,
                            NodeName = node.NodeName,
                            NodeType = node.NodeType.ToString(),
                            DefenseBonus = node.DefenseBonus,
                            InitialController = node.InitialController,
                            InitialControlPoints = node.InitialControlPoints,
                            MaxControlPoints = node.MaxControlPoints
                        });
                    }
                }

                exportData.Regions.Add(regionData);
            }

            string json = JsonUtility.ToJson(exportData, true);
            System.IO.File.WriteAllText(path, json);
            EditorUtility.DisplayDialog("导出成功", $"地图数据已导出到:\n{path}", "OK");
        }

        private void ValidateMapData()
        {
            if (_targetConfig.RegionConfigs == null || _targetConfig.RegionConfigs.Length == 0)
            {
                EditorUtility.DisplayDialog("验证结果", "没有地区数据!", "OK");
                return;
            }

            var errors = new List<string>();
            var warnings = new List<string>();

            var nodeIds = new HashSet<string>();

            for (int i = 0; i < _targetConfig.RegionConfigs.Length; i++)
            {
                var region = _targetConfig.RegionConfigs[i];
                if (region == null)
                {
                    errors.Add($"地区 {i}: 数据为空");
                    continue;
                }

                if (string.IsNullOrEmpty(region.RegionId))
                    errors.Add($"地区 {i}: RegionId 为空");
                if (string.IsNullOrEmpty(region.RegionName))
                    warnings.Add($"地区 {i}: RegionName 为空");

                if (region.NodeConfigs != null)
                {
                    for (int j = 0; j < region.NodeConfigs.Length; j++)
                    {
                        var node = region.NodeConfigs[j];
                        if (node == null)
                        {
                            errors.Add($"地区 {region.RegionId}: 节点 {j} 数据为空");
                            continue;
                        }

                        if (string.IsNullOrEmpty(node.NodeId))
                            errors.Add($"地区 {region.RegionId}: 节点 {j} NodeId 为空");
                        else
                        {
                            if (nodeIds.Contains(node.NodeId))
                                errors.Add($"节点Id重复: {node.NodeId}");
                            nodeIds.Add(node.NodeId);
                        }

                        if (node.InitialControlPoints > node.MaxControlPoints)
                            warnings.Add($"节点 {node.NodeId}: 初始控制点 > 最大控制点");

                        if (string.IsNullOrEmpty(node.InitialController))
                            warnings.Add($"节点 {node.NodeId}: InitialController 为空");
                    }
                }
            }

            string message = "";
            if (errors.Count > 0)
            {
                message += "错误:\n" + string.Join("\n", errors) + "\n\n";
            }
            if (warnings.Count > 0)
            {
                message += "警告:\n" + string.Join("\n", warnings);
            }
            if (errors.Count == 0 && warnings.Count == 0)
            {
                message = "验证通过! 没有发现错误或警告.";
            }

            EditorUtility.DisplayDialog("验证结果", message, "OK");
        }

        [System.Serializable]
        private class MapExportData
        {
            public List<RegionExportData> Regions;
        }

        [System.Serializable]
        private class RegionExportData
        {
            public string RegionId;
            public string RegionName;
            public List<NodeExportData> Nodes;
        }

        [System.Serializable]
        private class NodeExportData
        {
            public string NodeId;
            public string NodeName;
            public string NodeType;
            public int DefenseBonus;
            public string InitialController;
            public int InitialControlPoints;
            public int MaxControlPoints;
        }
    }
}
#endif