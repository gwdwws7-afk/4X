using UnityEngine;
using EventideAge.Core;

namespace EventideAge.Config
{
    [CreateAssetMenu(fileName = "DefaultGameConfig", menuName = "EventideAge/DefaultGameConfig")]
    public class DefaultGameConfig : GameConfig
    {
        public static GameConfig CreateDefault()
        {
            var config = ScriptableObject.CreateInstance<DefaultGameConfig>();
            config.ApplyDefaultsToBaseFields(force: true);
            return config;
        }

        private void OnEnable()
        {
            ApplyDefaultsToBaseFields(force: false);
        }

        private void ApplyDefaultsToBaseFields(bool force)
        {
            if (force || PhaseConfigs == null || PhaseConfigs.Length == 0)
                PhaseConfigs = _defaultPhaseConfigs;

            if (force || FactionConfigs == null || FactionConfigs.Length == 0)
                FactionConfigs = _defaultFactionConfigs;

            if (force || ResourceConfigs == null || ResourceConfigs.Length == 0)
                ResourceConfigs = _defaultResourceConfigs;

            if (force || RegionConfigs == null || RegionConfigs.Length == 0)
                RegionConfigs = _defaultRegionConfigs;
        }

        [Header("Phases")]
        [SerializeField] private PhaseConfig[] _defaultPhaseConfigs = new PhaseConfig[]
        {
            new PhaseConfig { PhaseName = "外交", BaseActionPoints = 2, SortOrder = 0 },
            new PhaseConfig { PhaseName = "战略", BaseActionPoints = 2, SortOrder = 1 },
            new PhaseConfig { PhaseName = "作战", BaseActionPoints = 3, SortOrder = 2 },
            new PhaseConfig { PhaseName = "后勤", BaseActionPoints = 1, SortOrder = 3 },
            new PhaseConfig { PhaseName = "情报", BaseActionPoints = 1, SortOrder = 4 },
            new PhaseConfig { PhaseName = "AI响应", BaseActionPoints = 0, SortOrder = 5 }
        };
        
        [Header("Factions")]
        [SerializeField] private FactionConfig[] _defaultFactionConfigs = new FactionConfig[]
        {
            new FactionConfig 
            { 
                FactionId = GameIds.Faction.Vashid, 
                FactionName = "瓦希德帝国", 
                IsPlayerControlled = true, 
                InitialControlledPoints = 100, 
                InitialRelationship = 100 
            },
            new FactionConfig 
            { 
                FactionId = GameIds.Faction.Aurean, 
                FactionName = "黄金领", 
                IsPlayerControlled = false, 
                InitialControlledPoints = 100, 
                InitialRelationship = -100 
            },
            new FactionConfig 
            { 
                FactionId = GameIds.Faction.SacredFire, 
                FactionName = "圣火序", 
                IsPlayerControlled = false, 
                InitialControlledPoints = 50, 
                InitialRelationship = -80 
            },
            new FactionConfig 
            { 
                FactionId = GameIds.Faction.GoldenHord, 
                FactionName = "金帐合众", 
                IsPlayerControlled = false, 
                InitialControlledPoints = 80, 
                InitialRelationship = -60 
            },
            new FactionConfig 
            { 
                FactionId = GameIds.Faction.AshConfederacy, 
                FactionName = "灰烬众", 
                IsPlayerControlled = false, 
                InitialControlledPoints = 30, 
                InitialRelationship = 70 
            }
        };
        
        [Header("Resources")]
        [SerializeField] private ResourceConfig[] _defaultResourceConfigs = new ResourceConfig[]
        {
            new ResourceConfig { ResourceId = GameIds.Resource.Arms, ResourceName = "战械", InitialAmount = 50, MaxCapacity = 100, ResourceType = ResourceType.Consumable },
            new ResourceConfig { ResourceId = GameIds.Resource.FireOil, ResourceName = "火油", InitialAmount = 80, MaxCapacity = 200, ResourceType = ResourceType.Accumulative },
            new ResourceConfig { ResourceId = GameIds.Resource.GoldLeaf, ResourceName = "金叶", InitialAmount = 60, MaxCapacity = 150, ResourceType = ResourceType.Accumulative },
            new ResourceConfig { ResourceId = GameIds.Resource.TradeToken, ResourceName = "商盟券", InitialAmount = 30, MaxCapacity = 100, ResourceType = ResourceType.Accumulative },
            new ResourceConfig { ResourceId = GameIds.Resource.SocialValue, ResourceName = "社稷值", InitialAmount = 100, MaxCapacity = 100, ResourceType = ResourceType.Ratio },
            new ResourceConfig { ResourceId = GameIds.Resource.AshWill, ResourceName = "灰烬志", InitialAmount = 50, MaxCapacity = 100, ResourceType = ResourceType.Ratio },
            new ResourceConfig { ResourceId = GameIds.Resource.TributeOrder, ResourceName = "朝贡序", InitialAmount = 0, MaxCapacity = 100, ResourceType = ResourceType.Ratio }
        };
        
        [Header("Regions & Nodes")]
        [SerializeField] private RegionConfig[] _defaultRegionConfigs = new RegionConfig[]
        {
            new RegionConfig
            {
                RegionId = "PersianGulf",
                RegionName = "波斯湾",
                NodeConfigs = new NodeConfig[]
                {
                    new NodeConfig { NodeId = GameIds.Node.Hormuz, NodeName = "霍尔木兹", NodeType = NodeType.Chokepoint, DefenseBonus = 30, InitialController = GameIds.Faction.Vashid, InitialControlPoints = 100, MaxControlPoints = 100 },
                    new NodeConfig { NodeId = GameIds.Node.Bushehr, NodeName = "布什尔", NodeType = NodeType.Port, DefenseBonus = 20, InitialController = GameIds.Faction.Vashid, InitialControlPoints = 80, MaxControlPoints = 100 }
                }
            },
            new RegionConfig
            {
                RegionId = "WesternFront",
                RegionName = "西线",
                NodeConfigs = new NodeConfig[]
                {
                    new NodeConfig { NodeId = GameIds.Node.IraqBorder, NodeName = "伊拉克边境", NodeType = NodeType.Chokepoint, DefenseBonus = 25, InitialController = GameIds.Faction.AshConfederacy, InitialControlPoints = 60, MaxControlPoints = 100 },
                    new NodeConfig { NodeId = GameIds.Node.SyriaZone, NodeName = "叙利亚区域", NodeType = NodeType.City, DefenseBonus = 15, InitialController = GameIds.Faction.AshConfederacy, InitialControlPoints = 50, MaxControlPoints = 100 }
                }
            },
            new RegionConfig
            {
                RegionId = "NorthernTerritory",
                RegionName = "北境",
                NodeConfigs = new NodeConfig[]
                {
                    new NodeConfig { NodeId = GameIds.Node.Caspian, NodeName = "里海油田", NodeType = NodeType.ResourceNode, DefenseBonus = 10, InitialController = GameIds.Faction.Vashid, InitialControlPoints = 70, MaxControlPoints = 100 },
                    new NodeConfig { NodeId = GameIds.Node.Caucasus, NodeName = "高加索通道", NodeType = NodeType.Chokepoint, DefenseBonus = 35, InitialController = GameIds.Faction.Neutral, InitialControlPoints = 30, MaxControlPoints = 100 }
                }
            },
            new RegionConfig
            {
                RegionId = "ArabianPeninsula",
                RegionName = "阿拉伯半岛",
                NodeConfigs = new NodeConfig[]
                {
                    new NodeConfig { NodeId = GameIds.Node.RedSea, NodeName = "红海通道", NodeType = NodeType.Chokepoint, DefenseBonus = 30, InitialController = GameIds.Faction.GoldenHord, InitialControlPoints = 80, MaxControlPoints = 100 },
                    new NodeConfig { NodeId = GameIds.Node.GulfBase, NodeName = "海湾基地", NodeType = NodeType.Port, DefenseBonus = 20, InitialController = GameIds.Faction.Aurean, InitialControlPoints = 90, MaxControlPoints = 100 }
                }
            },
            new RegionConfig
            {
                RegionId = "Levant",
                RegionName = "黎凡特",
                NodeConfigs = new NodeConfig[]
                {
                    new NodeConfig { NodeId = GameIds.Node.Mediterranean, NodeName = "地中海东岸", NodeType = NodeType.Chokepoint, DefenseBonus = 25, InitialController = GameIds.Faction.SacredFire, InitialControlPoints = 70, MaxControlPoints = 100 },
                    new NodeConfig { NodeId = GameIds.Node.IsraelCore, NodeName = "以色列核心", NodeType = NodeType.City, DefenseBonus = 40, InitialController = GameIds.Faction.SacredFire, InitialControlPoints = 100, MaxControlPoints = 100 }
                }
            },
            new RegionConfig
            {
                RegionId = "CentralAsia",
                RegionName = "中亚",
                NodeConfigs = new NodeConfig[]
                {
                    new NodeConfig { NodeId = GameIds.Node.Afghanistan, NodeName = "阿富汗走廊", NodeType = NodeType.Chokepoint, DefenseBonus = 20, InitialController = GameIds.Faction.Neutral, InitialControlPoints = 40, MaxControlPoints = 100 },
                    new NodeConfig { NodeId = GameIds.Node.TradeHub, NodeName = "贸易枢纽", NodeType = NodeType.ResourceNode, DefenseBonus = 10, InitialController = GameIds.Faction.Neutral, InitialControlPoints = 50, MaxControlPoints = 100 }
                }
            }
        };
    }
}
