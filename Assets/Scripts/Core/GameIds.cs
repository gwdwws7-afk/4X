using System;
using System.Collections.Generic;

namespace EventideAge.Core
{
    public static class GameIds
    {
        public static class Faction
        {
            public const string Vashid = "Vashid";
            public const string Aurean = "Aurean";
            public const string SacredFire = "SacredFire";
            public const string GoldenHord = "GoldenHord";
            public const string AshConfederacy = "Ash Confederacy";
            public const string Neutral = "Neutral";
        }

        public static class Resource
        {
            public const string Arms = "Arms";
            public const string FireOil = "FireOil";
            public const string GoldLeaf = "GoldLeaf";
            public const string TradeToken = "TradeToken";
            public const string SocialValue = "SocialValue";
            public const string AshWill = "AshWill";
            public const string TributeOrder = "TributeOrder";
        }

        public static class Node
        {
            public const string Hormuz = "Hormuz";
            public const string Bushehr = "Bushehr";
            public const string IraqBorder = "IraqBorder";
            public const string SyriaZone = "SyriaZone";
            public const string Caspian = "Caspian";
            public const string Caucasus = "Caucasus";
            public const string RedSea = "RedSea";
            public const string GulfBase = "GulfBase";
            public const string Mediterranean = "Mediterranean";
            public const string IsraelCore = "IsraelCore";
            public const string Afghanistan = "Afghanistan";
            public const string TradeHub = "TradeHub";
        }

        public static class Route
        {
            public const string PersianGulf = "route_persian_gulf";
            public const string Northern = "route_northern";
            public const string Tigris = "route_tigris";
        }

        private static readonly Dictionary<string, string> kFactionAliases = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "Vahid", Faction.Vashid },
            { "GoldLeader", Faction.Aurean },
            { "HolyFire", Faction.SacredFire },
            { "AshCloud", Faction.AshConfederacy },
            { "ResistanceAxis", Faction.AshConfederacy },
            { "AureanDominion", Faction.Aurean }
        };

        private static readonly Dictionary<string, string> kResourceAliases = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "Energy", Resource.FireOil },
            { "Prestige", Resource.TributeOrder },
            { "Social", Resource.SocialValue },
            { "TradeNotes", Resource.TradeToken },
            { "NorthCoins", Resource.TradeToken },
            { "GoldLeaves", Resource.GoldLeaf },
            { "GoldLeafReserve", Resource.GoldLeaf }
        };

        private static readonly Dictionary<string, string> kNodeAliases = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "Tigris", Node.IraqBorder },
            { "TigrisFederal", Node.IraqBorder },
            { "Damascus", Node.SyriaZone },
            { "Beirut", Node.Mediterranean },
            { "Kirkuk", Node.Caspian },
            { "EastTradeRoute", Node.TradeHub }
        };

        private static readonly Dictionary<string, string> kRouteAliases = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "PersianGulf", Route.PersianGulf },
            { "Northern", Route.Northern },
            { "Tigris", Route.Tigris }
        };

        public static string ResolveFactionId(string factionId)
        {
            return ResolveAlias(factionId, kFactionAliases);
        }

        public static string ResolveResourceId(string resourceId)
        {
            return ResolveAlias(resourceId, kResourceAliases);
        }

        public static string ResolveNodeId(string nodeId)
        {
            return ResolveAlias(nodeId, kNodeAliases);
        }

        public static string ResolveRouteId(string routeId)
        {
            return ResolveAlias(routeId, kRouteAliases);
        }

        public static List<string> GetFactionIdCandidates(string factionId)
        {
            return BuildCandidates(factionId, kFactionAliases);
        }

        public static List<string> GetResourceIdCandidates(string resourceId)
        {
            return BuildCandidates(resourceId, kResourceAliases);
        }

        public static List<string> GetNodeIdCandidates(string nodeId)
        {
            return BuildCandidates(nodeId, kNodeAliases);
        }

        public static List<string> GetRouteIdCandidates(string routeId)
        {
            return BuildCandidates(routeId, kRouteAliases);
        }

        private static List<string> BuildCandidates(string inputId, Dictionary<string, string> aliases)
        {
            var candidates = new List<string>();
            if (string.IsNullOrWhiteSpace(inputId))
                return candidates;

            string canonical = ResolveAlias(inputId, aliases);
            AddUnique(candidates, canonical);
            AddUnique(candidates, inputId);

            foreach (var pair in aliases)
            {
                if (string.Equals(pair.Value, canonical, StringComparison.OrdinalIgnoreCase))
                {
                    AddUnique(candidates, pair.Key);
                }
            }

            return candidates;
        }

        private static string ResolveAlias(string inputId, Dictionary<string, string> aliases)
        {
            if (string.IsNullOrWhiteSpace(inputId))
                return inputId;

            if (aliases.TryGetValue(inputId, out var canonical))
                return canonical;

            return inputId;
        }

        private static void AddUnique(List<string> values, string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return;

            foreach (var existing in values)
            {
                if (string.Equals(existing, value, StringComparison.OrdinalIgnoreCase))
                    return;
            }

            values.Add(value);
        }
    }
}
