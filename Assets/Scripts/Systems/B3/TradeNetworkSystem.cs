using UnityEngine;
using System.Collections.Generic;
using EventideAge.Core;
using EventideAge.Systems.B1;

namespace EventideAge.Systems.B3
{
    public enum RouteStatus
    {
        Open,
        Pressured,
        Blocked,
        Alternative
    }
    
    public class RouteState
    {
        public string RouteId;
        public string RouteName;
        public string[] NodeIds;
        public float Efficiency;
        public RouteStatus Status;
        public string ControllingFaction;
        public float IncomeBonus;
    }
    
    public class TradeAllocation
    {
        public float TotalAvailable;
        public float GoldLeaves;
        public float TradeNotes;
        public float NorthCoins;
        public float Barter;
        public float GreyMarket;
    }
    
    public class AlternativeRoute
    {
        public string RouteId;
        public string RouteName;
        public float Efficiency;
    }
    
    public class TradeNetworkSystem : GameSystem
    {
        [Header("Trade Routes Configuration")]
        public string[] PersianGulfRoute = new string[] { GameIds.Node.Bushehr, GameIds.Node.Hormuz, GameIds.Node.Mediterranean };
        public string[] NorthernRoute = new string[] { GameIds.Node.Caspian, GameIds.Node.Caucasus, GameIds.Node.TradeHub };
        public string[] TigrisRoute = new string[] { GameIds.Node.Caspian, GameIds.Node.IraqBorder, GameIds.Node.TradeHub };
        
        [Header("Route Parameters")]
        public float ExportRatio = 0.7f;
        public float EndNodeBonus = 0.2f;
        public float FullControlBonus = 0.3f;
        public float MainRouteBonus = 0.1f;
        public float AlternativeRoutePenalty = 0.1f;
        public float FriendlyRelationBonus = 0.1f;
        
        [Header("Node Efficiency")]
        public float FriendlyNodeEfficiency = 1.0f;
        public float NeutralNodeEfficiency = 0.8f;
        public float ContestedNodeEfficiency = 0.6f;
        public float DeepContestedNodeEfficiency = 0.2f;
        public float EnemyNodeEfficiency = 0.0f;
        
        [Header("Alternative Route Efficiencies")]
        public float NorthernRouteEfficiency = 0.6f;
        public float TigrisRouteEfficiency = 0.55f;
        public float GreyMarketEfficiency = 0.4f;
        
        private Dictionary<string, RouteState> _routes = new Dictionary<string, RouteState>();
        private string _primaryRouteId = GameIds.Route.PersianGulf;
        
        public override void Initialize(GameState state, GameEvents events)
        {
            base.Initialize(state, events);
            
            InitializeRoutes();
            
            Events.OnNodeControlChanged += HandleNodeControlChanged;
            Events.OnTurnEnded += HandleTurnEnded;
            
            Debug.Log("[TradeNetworkSystem] Initialized");
        }
        
        private void OnDestroy()
        {
            Events.OnNodeControlChanged -= HandleNodeControlChanged;
            Events.OnTurnEnded -= HandleTurnEnded;
        }
        
        private void InitializeRoutes()
        {
            _routes[GameIds.Route.PersianGulf] = new RouteState
            {
                RouteId = GameIds.Route.PersianGulf,
                RouteName = "Persian Gulf Route",
                NodeIds = ResolveNodeIds(PersianGulfRoute),
                Status = RouteStatus.Open,
                ControllingFaction = GameIds.ResolveFactionId(GameIds.Faction.Vashid)
            };
            
            _routes[GameIds.Route.Northern] = new RouteState
            {
                RouteId = GameIds.Route.Northern,
                RouteName = "Northern Route",
                NodeIds = ResolveNodeIds(NorthernRoute),
                Status = RouteStatus.Open,
                ControllingFaction = GameIds.ResolveFactionId(GameIds.Faction.GoldenHord)
            };
            
            _routes[GameIds.Route.Tigris] = new RouteState
            {
                RouteId = GameIds.Route.Tigris,
                RouteName = "Tigris Corridor",
                NodeIds = ResolveNodeIds(TigrisRoute),
                Status = RouteStatus.Open,
                ControllingFaction = GameIds.ResolveFactionId(GameIds.Faction.Neutral)
            };
        }
        
        private void HandleNodeControlChanged(string nodeId, string oldController, string newController, int controlPoints)
        {
            RecalculateAllRoutes();
        }
        
        private void HandleTurnEnded(int turnNumber)
        {
            RecalculateAllRoutes();
        }
        
        public void RecalculateAllRoutes()
        {
            foreach (var route in _routes.Values)
            {
                route.Efficiency = CalculateRouteEfficiency(route);
                route.Status = GetRouteStatus(route.Efficiency);
            }
        }
        
        private float CalculateRouteEfficiency(RouteState route)
        {
            if (route.NodeIds == null || route.NodeIds.Length == 0)
                return 0f;
            
            float totalEfficiency = 1f;
            int controllableNodes = 0;
            
            foreach (string nodeId in route.NodeIds)
            {
                var node = State.GetNode(GameIds.ResolveNodeId(nodeId));
                if (node == null) continue;
                
                float nodeEff = GetNodeEfficiency(node);
                totalEfficiency *= nodeEff;
                controllableNodes++;
            }
            
            float lengthModifier = Mathf.Pow(0.9f, route.NodeIds.Length - 1);
            totalEfficiency *= lengthModifier;
            
            float routeBonus = GetRouteIncomeBonus(route);
            totalEfficiency *= (1f + routeBonus);
            
            return Mathf.Clamp01(totalEfficiency);
        }
        
        private float GetNodeEfficiency(NodeState node)
        {
            string controller = GameIds.ResolveFactionId(node.ControllingFactionId);
            if (controller == GameIds.Faction.Vashid)
                return FriendlyNodeEfficiency;
            else if (string.IsNullOrEmpty(controller) || controller == GameIds.Faction.Neutral)
                return NeutralNodeEfficiency;
            else if (controller == "Contested")
                return ContestedNodeEfficiency;
            else
                return EnemyNodeEfficiency;
        }
        
        private RouteStatus GetRouteStatus(float efficiency)
        {
            if (efficiency >= 0.8f)
                return RouteStatus.Open;
            else if (efficiency >= 0.3f)
                return RouteStatus.Pressured;
            else if (efficiency > 0f)
                return RouteStatus.Alternative;
            else
                return RouteStatus.Blocked;
        }
        
        private float GetRouteIncomeBonus(RouteState route)
        {
            float bonus = 0f;
            
            if (route.RouteId == GameIds.Route.PersianGulf)
                bonus += MainRouteBonus;
            
            string lastNodeId = route.NodeIds[route.NodeIds.Length - 1];
            var lastNode = State.GetNode(lastNodeId);
            if (lastNode != null && GameIds.ResolveFactionId(lastNode.ControllingFactionId) == GameIds.Faction.Vashid)
                bonus += EndNodeBonus;
            
            bool allVashid = true;
            foreach (string nodeId in route.NodeIds)
            {
                var node = State.GetNode(nodeId);
                if (node == null || GameIds.ResolveFactionId(node.ControllingFactionId) != GameIds.Faction.Vashid)
                {
                    allVashid = false;
                    break;
                }
            }
            if (allVashid)
                bonus += FullControlBonus;
            
            return bonus;
        }
        
        public RouteStatus GetRouteStatus(string routeId)
        {
            routeId = GameIds.ResolveRouteId(routeId);
            if (_routes.TryGetValue(routeId, out var route))
                return route.Status;
            return RouteStatus.Blocked;
        }
        
        public float GetRouteEfficiency(string routeId)
        {
            routeId = GameIds.ResolveRouteId(routeId);
            if (_routes.TryGetValue(routeId, out var route))
                return route.Efficiency;
            return 0f;
        }
        
        public RouteState[] GetAllRouteStatuses()
        {
            var result = new List<RouteState>();
            foreach (var route in _routes.Values)
            {
                result.Add(route);
            }
            return result.ToArray();
        }
        
        public TradeAllocation GetAvailableTradeAmount()
        {
            var allocation = new TradeAllocation();
            
            float totalEnergyOutput = CalculateTotalEnergyOutput();
            float availableForExport = totalEnergyOutput * ExportRatio;
            
            float bestEfficiency = GetBestActiveRouteEfficiency();
            float actualTradeAmount = availableForExport * bestEfficiency;
            
            allocation.TotalAvailable = actualTradeAmount;
            
            if (bestEfficiency > 0.3f)
            {
                allocation.GoldLeaves = actualTradeAmount * 0.5f;
            }
            
            var eastRelation = GetFactionRelation(GameIds.Faction.SacredFire);
            if (eastRelation >= 50)
            {
                allocation.TradeNotes = actualTradeAmount * 0.3f;
            }
            
            var northRelation = GetFactionRelation(GameIds.Faction.GoldenHord);
            if (northRelation >= 60)
            {
                allocation.NorthCoins = actualTradeAmount * 0.2f;
            }
            
            return allocation;
        }
        
        private float CalculateTotalEnergyOutput()
        {
            float total = 0f;
            
            var caspianNode = State.GetNode(GameIds.Node.Caspian);
            if (caspianNode != null && caspianNode.NodeType == NodeType.ResourceNode)
            {
                total += 8f;
            }
            
            var tradeHubNode = State.GetNode(GameIds.Node.TradeHub);
            if (tradeHubNode != null && tradeHubNode.NodeType == NodeType.ResourceNode && GameIds.ResolveFactionId(tradeHubNode.ControllingFactionId) == GameIds.Faction.Vashid)
            {
                total += 6f;
            }
            
            return total;
        }
        
        private float GetBestActiveRouteEfficiency()
        {
            if (_routes.TryGetValue(_primaryRouteId, out var primary))
            {
                if (primary.Status != RouteStatus.Blocked)
                    return primary.Efficiency;
            }
            
            float bestAlt = 0f;
            
            if (_routes.TryGetValue(GameIds.Route.Northern, out var northern))
            {
                if (northern.Status != RouteStatus.Blocked && northern.Efficiency > bestAlt)
                    bestAlt = northern.Efficiency * NorthernRouteEfficiency;
            }
            
            if (_routes.TryGetValue(GameIds.Route.Tigris, out var tigris))
            {
                if (tigris.Status != RouteStatus.Blocked && tigris.Efficiency > bestAlt)
                    bestAlt = tigris.Efficiency * TigrisRouteEfficiency;
            }
            
            return bestAlt > 0f ? bestAlt : GreyMarketEfficiency;
        }
        
        private int GetFactionRelation(string factionId)
        {
            factionId = GameIds.ResolveFactionId(factionId);
            var faction = State.GetFaction(factionId);
            return faction?.RelationshipWithPlayer ?? 0;
        }

        private string[] ResolveNodeIds(string[] nodeIds)
        {
            if (nodeIds == null)
                return new string[0];

            var resolved = new string[nodeIds.Length];
            for (int i = 0; i < nodeIds.Length; i++)
            {
                resolved[i] = GameIds.ResolveNodeId(nodeIds[i]);
            }

            return resolved;
        }
        
        public AlternativeRoute[] GetAlternativeRoutes(string mainRouteId)
        {
            mainRouteId = GameIds.ResolveRouteId(mainRouteId);
            var alternatives = new List<AlternativeRoute>();
            
            if (mainRouteId == GameIds.Route.PersianGulf)
            {
                if (_routes.TryGetValue(GameIds.Route.Northern, out var northern) && northern.Status != RouteStatus.Blocked)
                {
                    alternatives.Add(new AlternativeRoute
                    {
                        RouteId = GameIds.Route.Northern,
                        RouteName = northern.RouteName,
                        Efficiency = northern.Efficiency * NorthernRouteEfficiency
                    });
                }
                
                if (_routes.TryGetValue(GameIds.Route.Tigris, out var tigris) && tigris.Status != RouteStatus.Blocked)
                {
                    alternatives.Add(new AlternativeRoute
                    {
                        RouteId = GameIds.Route.Tigris,
                        RouteName = tigris.RouteName,
                        Efficiency = tigris.Efficiency * TigrisRouteEfficiency
                    });
                }
            }
            
            return alternatives.ToArray();
        }
        
        public float[] GetRouteIncomeBonus()
        {
            var bonuses = new float[_routes.Count];
            int i = 0;
            foreach (var route in _routes.Values)
            {
                bonuses[i++] = route.IncomeBonus;
            }
            return bonuses;
        }
    }
}

