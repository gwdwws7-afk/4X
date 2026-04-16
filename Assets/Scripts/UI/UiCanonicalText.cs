using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using EventideAge.Core;

namespace EventideAge.UI
{
    internal static class UiCanonicalText
    {
        private static readonly Regex kTokenRegex = new Regex(@"\b[A-Za-z_][A-Za-z0-9_]*\b", RegexOptions.Compiled);
        private static readonly HashSet<string> kCanonicalFactionIds = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            GameIds.Faction.Vashid,
            GameIds.Faction.Aurean,
            GameIds.Faction.SacredFire,
            GameIds.Faction.GoldenHord,
            GameIds.Faction.AshConfederacy,
            GameIds.Faction.Neutral
        };

        private static readonly HashSet<string> kCanonicalNodeIds = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            GameIds.Node.Hormuz,
            GameIds.Node.Bushehr,
            GameIds.Node.IraqBorder,
            GameIds.Node.SyriaZone,
            GameIds.Node.Caspian,
            GameIds.Node.Caucasus,
            GameIds.Node.RedSea,
            GameIds.Node.GulfBase,
            GameIds.Node.Mediterranean,
            GameIds.Node.IsraelCore,
            GameIds.Node.Afghanistan,
            GameIds.Node.TradeHub
        };

        private static readonly HashSet<string> kCanonicalRouteIds = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            GameIds.Route.PersianGulf,
            GameIds.Route.Northern,
            GameIds.Route.Tigris
        };

        public static string CanonicalizeFactionId(string factionId)
        {
            if (string.IsNullOrWhiteSpace(factionId))
                return factionId;

            return GameIds.ResolveFactionId(factionId);
        }

        public static string CanonicalizeNodeId(string nodeId)
        {
            if (string.IsNullOrWhiteSpace(nodeId))
                return nodeId;

            return GameIds.ResolveNodeId(nodeId);
        }

        public static string CanonicalizeSourceId(string sourceId)
        {
            return CanonicalizeTokens(sourceId, includeRouteAliases: true);
        }

        public static string CanonicalizeMessage(string message)
        {
            return CanonicalizeTokens(message, includeRouteAliases: false);
        }

        private static string CanonicalizeTokens(string text, bool includeRouteAliases)
        {
            if (string.IsNullOrWhiteSpace(text))
                return text;

            return kTokenRegex.Replace(text, match =>
            {
                string token = match.Value;
                string canonical = CanonicalizeToken(token, includeRouteAliases);
                return string.IsNullOrEmpty(canonical) ? token : canonical;
            });
        }

        private static string CanonicalizeToken(string token, bool includeRouteAliases)
        {
            if (string.IsNullOrWhiteSpace(token))
                return token;

            string factionId = GameIds.ResolveFactionId(token);
            if (!string.Equals(factionId, token, StringComparison.OrdinalIgnoreCase) || kCanonicalFactionIds.Contains(token))
                return factionId;

            string nodeId = GameIds.ResolveNodeId(token);
            if (!string.Equals(nodeId, token, StringComparison.OrdinalIgnoreCase) || kCanonicalNodeIds.Contains(token))
                return nodeId;

            if (includeRouteAliases)
            {
                string routeId = GameIds.ResolveRouteId(token);
                if (!string.Equals(routeId, token, StringComparison.OrdinalIgnoreCase) || kCanonicalRouteIds.Contains(token))
                    return routeId;
            }

            return token;
        }
    }
}
