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

        public void ApplyDesignBaseline()
        {
            ApplyDefaultsToBaseFields(force: true);
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
        [SerializeField] private PhaseConfig[] _defaultPhaseConfigs =
        {
            new PhaseConfig { PhaseName = "\u5916\u4EA4", BaseActionPoints = 2, SortOrder = 0 },
            new PhaseConfig { PhaseName = "\u6218\u7565", BaseActionPoints = 2, SortOrder = 1 },
            new PhaseConfig { PhaseName = "\u4F5C\u6218", BaseActionPoints = 3, SortOrder = 2 },
            new PhaseConfig { PhaseName = "\u540E\u52E4", BaseActionPoints = 1, SortOrder = 3 },
            new PhaseConfig { PhaseName = "\u60C5\u62A5", BaseActionPoints = 1, SortOrder = 4 },
            new PhaseConfig { PhaseName = "AI\u54CD\u5E94", BaseActionPoints = 0, SortOrder = 5 }
        };

        [Header("Factions")]
        [SerializeField] private FactionConfig[] _defaultFactionConfigs =
        {
            new FactionConfig
            {
                FactionId = GameIds.Faction.Vashid,
                FactionName = "\u74E6\u5E0C\u5FB7\u5E1D\u56FD",
                IsPlayerControlled = true,
                InitialControlledPoints = 100,
                InitialRelationship = 100
            },
            new FactionConfig
            {
                FactionId = GameIds.Faction.Aurean,
                FactionName = "\u9EC4\u91D1\u9886",
                IsPlayerControlled = false,
                InitialControlledPoints = 100,
                InitialRelationship = -100
            },
            new FactionConfig
            {
                FactionId = GameIds.Faction.SacredFire,
                FactionName = "\u5723\u706B\u5E8F",
                IsPlayerControlled = false,
                InitialControlledPoints = 50,
                InitialRelationship = -80
            },
            new FactionConfig
            {
                FactionId = GameIds.Faction.GoldenHord,
                FactionName = "\u91D1\u5E10\u5408\u4F17",
                IsPlayerControlled = false,
                InitialControlledPoints = 80,
                InitialRelationship = -60
            },
            new FactionConfig
            {
                FactionId = GameIds.Faction.AshConfederacy,
                FactionName = "\u7070\u70EC\u4F17",
                IsPlayerControlled = false,
                InitialControlledPoints = 30,
                InitialRelationship = 70
            }
        };

        [Header("Resources")]
        [SerializeField] private ResourceConfig[] _defaultResourceConfigs =
        {
            new ResourceConfig { ResourceId = GameIds.Resource.Arms, ResourceName = "\u6218\u68B0", InitialAmount = 50, MaxCapacity = 100, ResourceType = ResourceType.Consumable },
            new ResourceConfig { ResourceId = GameIds.Resource.FireOil, ResourceName = "\u706B\u6CB9", InitialAmount = 80, MaxCapacity = 200, ResourceType = ResourceType.Accumulative },
            new ResourceConfig { ResourceId = GameIds.Resource.GoldLeaf, ResourceName = "\u91D1\u53F6", InitialAmount = 60, MaxCapacity = 150, ResourceType = ResourceType.Accumulative },
            new ResourceConfig { ResourceId = GameIds.Resource.TradeToken, ResourceName = "\u5546\u76DF\u5238", InitialAmount = 30, MaxCapacity = 100, ResourceType = ResourceType.Accumulative },
            new ResourceConfig { ResourceId = GameIds.Resource.SocialValue, ResourceName = "\u793E\u7A37\u503C", InitialAmount = 100, MaxCapacity = 100, ResourceType = ResourceType.Ratio },
            new ResourceConfig { ResourceId = GameIds.Resource.AshWill, ResourceName = "\u7070\u70EC\u5FD7", InitialAmount = 50, MaxCapacity = 100, ResourceType = ResourceType.Ratio },
            new ResourceConfig { ResourceId = GameIds.Resource.TributeOrder, ResourceName = "\u671D\u8D21\u5E8F", InitialAmount = 0, MaxCapacity = 100, ResourceType = ResourceType.Ratio }
        };

        [Header("Regions & Nodes")]
        [SerializeField] private RegionConfig[] _defaultRegionConfigs =
        {
            new RegionConfig
            {
                RegionId = "PersianGulf",
                RegionName = "\u6CE2\u65AF\u6E7E",
                NodeConfigs = new[]
                {
                    new NodeConfig { NodeId = GameIds.Node.Hormuz, NodeName = "\u970D\u5C14\u6728\u5179", NodeType = NodeType.Chokepoint, DefenseBonus = 30, InitialController = GameIds.Faction.Vashid, InitialControlPoints = 100, MaxControlPoints = 100 },
                    new NodeConfig { NodeId = GameIds.Node.Bushehr, NodeName = "\u5E03\u4EC0\u5C14", NodeType = NodeType.Port, DefenseBonus = 20, InitialController = GameIds.Faction.Vashid, InitialControlPoints = 80, MaxControlPoints = 100 }
                }
            },
            new RegionConfig
            {
                RegionId = "WesternFront",
                RegionName = "\u897F\u7EBF",
                NodeConfigs = new[]
                {
                    new NodeConfig { NodeId = GameIds.Node.IraqBorder, NodeName = "\u4F0A\u62C9\u514B\u8FB9\u5883", NodeType = NodeType.Chokepoint, DefenseBonus = 25, InitialController = GameIds.Faction.AshConfederacy, InitialControlPoints = 60, MaxControlPoints = 100 },
                    new NodeConfig { NodeId = GameIds.Node.SyriaZone, NodeName = "\u53D9\u5229\u4E9A\u533A\u57DF", NodeType = NodeType.City, DefenseBonus = 15, InitialController = GameIds.Faction.AshConfederacy, InitialControlPoints = 50, MaxControlPoints = 100 }
                }
            },
            new RegionConfig
            {
                RegionId = "NorthernTerritory",
                RegionName = "\u5317\u5883",
                NodeConfigs = new[]
                {
                    new NodeConfig { NodeId = GameIds.Node.Caspian, NodeName = "\u91CC\u6D77\u6CB9\u7530", NodeType = NodeType.ResourceNode, DefenseBonus = 10, InitialController = GameIds.Faction.Vashid, InitialControlPoints = 70, MaxControlPoints = 100 },
                    new NodeConfig { NodeId = GameIds.Node.Caucasus, NodeName = "\u9AD8\u52A0\u7D22\u901A\u9053", NodeType = NodeType.Chokepoint, DefenseBonus = 35, InitialController = GameIds.Faction.Neutral, InitialControlPoints = 30, MaxControlPoints = 100 }
                }
            },
            new RegionConfig
            {
                RegionId = "ArabianPeninsula",
                RegionName = "\u963F\u62C9\u4F2F\u534A\u5C9B",
                NodeConfigs = new[]
                {
                    new NodeConfig { NodeId = GameIds.Node.RedSea, NodeName = "\u7EA2\u6D77\u901A\u9053", NodeType = NodeType.Chokepoint, DefenseBonus = 30, InitialController = GameIds.Faction.GoldenHord, InitialControlPoints = 80, MaxControlPoints = 100 },
                    new NodeConfig { NodeId = GameIds.Node.GulfBase, NodeName = "\u6D77\u6E7E\u57FA\u5730", NodeType = NodeType.Port, DefenseBonus = 20, InitialController = GameIds.Faction.Aurean, InitialControlPoints = 90, MaxControlPoints = 100 }
                }
            },
            new RegionConfig
            {
                RegionId = "Levant",
                RegionName = "\u9ECE\u51E1\u7279",
                NodeConfigs = new[]
                {
                    new NodeConfig { NodeId = GameIds.Node.Mediterranean, NodeName = "\u5730\u4E2D\u6D77\u4E1C\u5CB8", NodeType = NodeType.Chokepoint, DefenseBonus = 25, InitialController = GameIds.Faction.SacredFire, InitialControlPoints = 70, MaxControlPoints = 100 },
                    new NodeConfig { NodeId = GameIds.Node.IsraelCore, NodeName = "\u4EE5\u8272\u5217\u6838\u5FC3", NodeType = NodeType.City, DefenseBonus = 40, InitialController = GameIds.Faction.SacredFire, InitialControlPoints = 100, MaxControlPoints = 100 }
                }
            },
            new RegionConfig
            {
                RegionId = "CentralAsia",
                RegionName = "\u4E2D\u4E9A",
                NodeConfigs = new[]
                {
                    new NodeConfig { NodeId = GameIds.Node.Afghanistan, NodeName = "\u963F\u5BCC\u6C57\u8D70\u5ECA", NodeType = NodeType.Chokepoint, DefenseBonus = 20, InitialController = GameIds.Faction.Neutral, InitialControlPoints = 40, MaxControlPoints = 100 },
                    new NodeConfig { NodeId = GameIds.Node.TradeHub, NodeName = "\u8D38\u6613\u67A2\u7EBD", NodeType = NodeType.ResourceNode, DefenseBonus = 10, InitialController = GameIds.Faction.Neutral, InitialControlPoints = 50, MaxControlPoints = 100 }
                }
            }
        };
    }
}
