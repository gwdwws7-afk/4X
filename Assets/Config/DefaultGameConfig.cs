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
            return config;
        }

        [Header("Phases")]
        public PhaseConfig[] PhaseConfigs = new PhaseConfig[]
        {
            new PhaseConfig { PhaseName = "外交", BaseActionPoints = 2, SortOrder = 0 },
            new PhaseConfig { PhaseName = "战略", BaseActionPoints = 2, SortOrder = 1 },
            new PhaseConfig { PhaseName = "作战", BaseActionPoints = 2, SortOrder = 2 },
            new PhaseConfig { PhaseName = "后勤", BaseActionPoints = 1, SortOrder = 3 },
            new PhaseConfig { PhaseName = "情报", BaseActionPoints = 1, SortOrder = 4 },
            new PhaseConfig { PhaseName = "AI响应", BaseActionPoints = 0, SortOrder = 5 }
        };
        
        [Header("Factions")]
        public FactionConfig[] FactionConfigs = new FactionConfig[]
        {
            new FactionConfig 
            { 
                FactionId = "Vashid", 
                FactionName = "瓦希德帝国", 
                IsPlayerControlled = true, 
                InitialControlledPoints = 100, 
                InitialRelationship = 100 
            },
            new FactionConfig 
            { 
                FactionId = "Aurean", 
                FactionName = "黄金领", 
                IsPlayerControlled = false, 
                InitialControlledPoints = 100, 
                InitialRelationship = -100 
            },
            new FactionConfig 
            { 
                FactionId = "SacredFire", 
                FactionName = "圣火序", 
                IsPlayerControlled = false, 
                InitialControlledPoints = 50, 
                InitialRelationship = -80 
            },
            new FactionConfig 
            { 
                FactionId = "GoldenHord", 
                FactionName = "金帐合众", 
                IsPlayerControlled = false, 
                InitialControlledPoints = 80, 
                InitialRelationship = -60 
            },
            new FactionConfig 
            { 
                FactionId = "Ash Confederacy", 
                FactionName = "灰烬众", 
                IsPlayerControlled = false, 
                InitialControlledPoints = 30, 
                InitialRelationship = 70 
            }
        };
        
        [Header("Resources")]
        public ResourceConfig[] ResourceConfigs = new ResourceConfig[]
        {
            new ResourceConfig { ResourceId = "Arms", ResourceName = "战械", InitialAmount = 50, MaxCapacity = 100, ResourceType = ResourceType.Consumable },
            new ResourceConfig { ResourceId = "FireOil", ResourceName = "火油", InitialAmount = 80, MaxCapacity = 200, ResourceType = ResourceType.Accumulative },
            new ResourceConfig { ResourceId = "GoldLeaf", ResourceName = "金叶", InitialAmount = 60, MaxCapacity = 150, ResourceType = ResourceType.Accumulative },
            new ResourceConfig { ResourceId = "TradeToken", ResourceName = "商盟券", InitialAmount = 30, MaxCapacity = 100, ResourceType = ResourceType.Accumulative },
            new ResourceConfig { ResourceId = "SocialValue", ResourceName = "社稷值", InitialAmount = 100, MaxCapacity = 100, ResourceType = ResourceType.Ratio },
            new ResourceConfig { ResourceId = "AshWill", ResourceName = "灰烬志", InitialAmount = 50, MaxCapacity = 100, ResourceType = ResourceType.Ratio },
            new ResourceConfig { ResourceId = "TributeOrder", ResourceName = "朝贡序", InitialAmount = 0, MaxCapacity = 100, ResourceType = ResourceType.Ratio }
        };
        
        [Header("Regions & Nodes")]
        public RegionConfig[] RegionConfigs = new RegionConfig[]
        {
            new RegionConfig
            {
                RegionId = "PersianGulf",
                RegionName = "波斯湾",
                NodeConfigs = new NodeConfig[]
                {
                    new NodeConfig { NodeId = "Hormuz", NodeName = "霍尔木兹", NodeType = NodeType.Chokepoint, DefenseBonus = 30, InitialController = "Vashid", InitialControlPoints = 100, MaxControlPoints = 100 },
                    new NodeConfig { NodeId = "Bushehr", NodeName = "布什尔", NodeType = NodeType.Port, DefenseBonus = 20, InitialController = "Vashid", InitialControlPoints = 80, MaxControlPoints = 100 }
                }
            },
            new RegionConfig
            {
                RegionId = "WesternFront",
                RegionName = "西线",
                NodeConfigs = new NodeConfig[]
                {
                    new NodeConfig { NodeId = "IraqBorder", NodeName = "伊拉克边境", NodeType = NodeType.Chokepoint, DefenseBonus = 25, InitialController = "Ash Confederacy", InitialControlPoints = 60, MaxControlPoints = 100 },
                    new NodeConfig { NodeId = "SyriaZone", NodeName = "叙利亚区域", NodeType = NodeType.City, DefenseBonus = 15, InitialController = "Ash Confederacy", InitialControlPoints = 50, MaxControlPoints = 100 }
                }
            },
            new RegionConfig
            {
                RegionId = "NorthernTerritory",
                RegionName = "北境",
                NodeConfigs = new NodeConfig[]
                {
                    new NodeConfig { NodeId = "Caspian", NodeName = "里海油田", NodeType = NodeType.ResourceNode, DefenseBonus = 10, InitialController = "Vashid", InitialControlPoints = 70, MaxControlPoints = 100 },
                    new NodeConfig { NodeId = "Caucasus", NodeName = "高加索通道", NodeType = NodeType.Chokepoint, DefenseBonus = 35, InitialController = "Neutral", InitialControlPoints = 30, MaxControlPoints = 100 }
                }
            },
            new RegionConfig
            {
                RegionId = "ArabianPeninsula",
                RegionName = "阿拉伯半岛",
                NodeConfigs = new NodeConfig[]
                {
                    new NodeConfig { NodeId = "RedSea", NodeName = "红海通道", NodeType = NodeType.Chokepoint, DefenseBonus = 30, InitialController = "GoldenHord", InitialControlPoints = 80, MaxControlPoints = 100 },
                    new NodeConfig { NodeId = "GulfBase", NodeName = "海湾基地", NodeType = NodeType.Port, DefenseBonus = 20, InitialController = "Aurean", InitialControlPoints = 90, MaxControlPoints = 100 }
                }
            },
            new RegionConfig
            {
                RegionId = "Levant",
                RegionName = "黎凡特",
                NodeConfigs = new NodeConfig[]
                {
                    new NodeConfig { NodeId = "Mediterranean", NodeName = "地中海东岸", NodeType = NodeType.Chokepoint, DefenseBonus = 25, InitialController = "SacredFire", InitialControlPoints = 70, MaxControlPoints = 100 },
                    new NodeConfig { NodeId = "IsraelCore", NodeName = "以色列核心", NodeType = NodeType.City, DefenseBonus = 40, InitialController = "SacredFire", InitialControlPoints = 100, MaxControlPoints = 100 }
                }
            },
            new RegionConfig
            {
                RegionId = "CentralAsia",
                RegionName = "中亚",
                NodeConfigs = new NodeConfig[]
                {
                    new NodeConfig { NodeId = "Afghanistan", NodeName = "阿富汗走廊", NodeType = NodeType.Chokepoint, DefenseBonus = 20, InitialController = "Neutral", InitialControlPoints = 40, MaxControlPoints = 100 },
                    new NodeConfig { NodeId = "TradeHub", NodeName = "贸易枢纽", NodeType = NodeType.ResourceNode, DefenseBonus = 10, InitialController = "Neutral", InitialControlPoints = 50, MaxControlPoints = 100 }
                }
            }
        };
    }
}
