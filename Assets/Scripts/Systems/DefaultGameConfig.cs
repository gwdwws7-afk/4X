using UnityEngine;
using EventideAge.Core;

namespace EventideAge.Systems
{
    public static class DefaultGameConfig
    {
        public static GameConfig CreateDefault()
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
                new FactionConfig
                {
                    FactionId = "Vashid",
                    FactionName = "瓦希德帝国",
                    IsPlayerControlled = true,
                    InitialControlledPoints = 100,
                    InitialRelationship = 100,
                    InitialSatisfaction = 100
                },
                new FactionConfig
                {
                    FactionId = "GoldLeader",
                    FactionName = "黄金领",
                    IsPlayerControlled = false,
                    InitialControlledPoints = 80,
                    InitialRelationship = -80,
                    InitialSatisfaction = 70
                },
                new FactionConfig
                {
                    FactionId = "HolyFire",
                    FactionName = "圣火序",
                    IsPlayerControlled = false,
                    InitialControlledPoints = 60,
                    InitialRelationship = -60,
                    InitialSatisfaction = 60
                },
                new FactionConfig
                {
                    FactionId = "NorthAlliance",
                    FactionName = "北境共主",
                    IsPlayerControlled = false,
                    InitialControlledPoints = 50,
                    InitialRelationship = 40,
                    InitialSatisfaction = 80
                },
                new FactionConfig
                {
                    FactionId = "EastTrader",
                    FactionName = "东土商盟",
                    IsPlayerControlled = false,
                    InitialControlledPoints = 40,
                    InitialRelationship = 30,
                    InitialSatisfaction = 75
                },
                new FactionConfig
                {
                    FactionId = "AshCloud",
                    FactionName = "灰烬众",
                    IsPlayerControlled = false,
                    InitialControlledPoints = 30,
                    InitialRelationship = 70,
                    InitialSatisfaction = 90
                }
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
            
            config.RegionConfigs = new RegionConfig[]
            {
                CreateTigrisRegion(),
                CreateLevantRegion(),
                CreateMediterraneanRegion(),
                CreatePersianGulfRegion()
            };
            
            return config;
        }
        
        private static RegionConfig CreateTigrisRegion()
        {
            var region = new RegionConfig
            {
                RegionId = "TigrisRegion",
                RegionName = "两河流域",
                NodeConfigs = new NodeConfig[]
                {
                    new NodeConfig
                    {
                        NodeId = "Tehran",
                        NodeName = "德黑兰",
                        NodeType = NodeType.City,
                        DefenseBonus = 20,
                        InitialController = "Vashid",
                        InitialControlPoints = 100,
                        MaxControlPoints = 100
                    },
                    new NodeConfig
                    {
                        NodeId = "Kermanshah",
                        NodeName = "克尔曼沙阿",
                        NodeType = NodeType.Chokepoint,
                        DefenseBonus = 15,
                        InitialController = "Vashid",
                        InitialControlPoints = 70,
                        MaxControlPoints = 100
                    },
                    new NodeConfig
                    {
                        NodeId = "Kirkuk",
                        NodeName = "基尔库克",
                        NodeType = NodeType.ResourceNode,
                        DefenseBonus = 10,
                        InitialController = "Vashid",
                        InitialControlPoints = 60,
                        MaxControlPoints = 80
                    },
                    new NodeConfig
                    {
                        NodeId = "Baghdad",
                        NodeName = "巴格达",
                        NodeType = NodeType.City,
                        DefenseBonus = 12,
                        InitialController = "GoldLeader",
                        InitialControlPoints = 50,
                        MaxControlPoints = 100
                    },
                    new NodeConfig
                    {
                        NodeId = "Basra",
                        NodeName = "巴士拉",
                        NodeType = NodeType.Port,
                        DefenseBonus = 8,
                        InitialController = "GoldLeader",
                        InitialControlPoints = 40,
                        MaxControlPoints = 80
                    }
                }
            };
            return region;
        }
        
        private static RegionConfig CreateLevantRegion()
        {
            var region = new RegionConfig
            {
                RegionId = "LevantRegion",
                RegionName = "黎凡特地区",
                NodeConfigs = new NodeConfig[]
                {
                    new NodeConfig
                    {
                        NodeId = "Damascus",
                        NodeName = "大马士革",
                        NodeType = NodeType.City,
                        DefenseBonus = 15,
                        InitialController = "HolyFire",
                        InitialControlPoints = 70,
                        MaxControlPoints = 100
                    },
                    new NodeConfig
                    {
                        NodeId = "Aleppo",
                        NodeName = "阿勒颇",
                        NodeType = NodeType.Chokepoint,
                        DefenseBonus = 12,
                        InitialController = "HolyFire",
                        InitialControlPoints = 55,
                        MaxControlPoints = 80
                    },
                    new NodeConfig
                    {
                        NodeId = "Beirut",
                        NodeName = "贝鲁特",
                        NodeType = NodeType.Port,
                        DefenseBonus = 10,
                        InitialController = "HolyFire",
                        InitialControlPoints = 50,
                        MaxControlPoints = 80
                    }
                }
            };
            return region;
        }
        
        private static RegionConfig CreateMediterraneanRegion()
        {
            var region = new RegionConfig
            {
                RegionId = "MediterraneanRegion",
                RegionName = "地中海东部",
                NodeConfigs = new NodeConfig[]
                {
                    new NodeConfig
                    {
                        NodeId = "MediterraneanChokepoint",
                        NodeName = "海峡咽喉",
                        NodeType = NodeType.Chokepoint,
                        DefenseBonus = 25,
                        InitialController = "GoldLeader",
                        InitialControlPoints = 80,
                        MaxControlPoints = 100
                    },
                    new NodeConfig
                    {
                        NodeId = "Cyprus",
                        NodeName = "塞浦路斯",
                        NodeType = NodeType.Port,
                        DefenseBonus = 8,
                        InitialController = "GoldLeader",
                        InitialControlPoints = 40,
                        MaxControlPoints = 60
                    }
                }
            };
            return region;
        }
        
        private static RegionConfig CreatePersianGulfRegion()
        {
            var region = new RegionConfig
            {
                RegionId = "PersianGulfRegion",
                RegionName = "波斯湾",
                NodeConfigs = new NodeConfig[]
                {
                    new NodeConfig
                    {
                        NodeId = "Hormuz",
                        NodeName = "霍尔木兹",
                        NodeType = NodeType.Chokepoint,
                        DefenseBonus = 30,
                        InitialController = "Vashid",
                        InitialControlPoints = 90,
                        MaxControlPoints = 100
                    },
                    new NodeConfig
                    {
                        NodeId = "GulfPort",
                        NodeName = "波斯湾港",
                        NodeType = NodeType.Port,
                        DefenseBonus = 5,
                        InitialController = "Vashid",
                        InitialControlPoints = 50,
                        MaxControlPoints = 80
                    }
                }
            };
            return region;
        }
    }
}