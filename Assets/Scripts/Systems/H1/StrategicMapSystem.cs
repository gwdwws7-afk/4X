using UnityEngine;
using System.Collections.Generic;
using EventideAge.Core;
using EventideAge.Systems.H2;

namespace EventideAge.Systems.H1
{
    public class StrategicMapSystem : GameSystem
    {
        public NodeNetworkSystem NodeNetworkSystem { get; set; }

        [Header("Map Visuals")]
        public Transform MapContainer;
        public GameObject RegionPrefab;
        public GameObject NodePrefab;
        public GameObject ConnectionLinePrefab;
        
        private Dictionary<string, StrategicNodeView> _nodeViews = new Dictionary<string, StrategicNodeView>();
        private Dictionary<string, StrategicRegionView> _regionViews = new Dictionary<string, StrategicRegionView>();
        private readonly List<GameObject> _connectionViews = new List<GameObject>();
        
        public override void Initialize(GameState state, GameEvents events)
        {
            base.Initialize(state, events);
            Events.OnNodeControlChanged += HandleNodeControlChanged;
            Events.OnTurnChanged += HandleTurnChanged;
        }
        
        private void OnDestroy()
        {
            Events.OnNodeControlChanged -= HandleNodeControlChanged;
            Events.OnTurnChanged -= HandleTurnChanged;
        }
        
        public override void OnPhaseEntered(int phaseIndex)
        {
            UpdateAllNodeVisuals();
        }
        
        public void BuildMap()
        {
            if (MapContainer == null)
            {
                Debug.LogWarning("[StrategicMap] MapContainer not assigned");
                return;
            }
            
            ClearMap();
            
            foreach (var region in State.Map.Regions)
            {
                CreateRegionView(region);
                
                foreach (var node in region.Nodes)
                {
                    CreateNodeView(node);
                }
            }

            BuildConnectionViews();
            
            Debug.Log($"[StrategicMap] Map built with {State.Map.Regions.Length} regions");
        }
        
        private void CreateRegionView(RegionState region)
        {
            if (RegionPrefab == null) return;
            
            var go = Instantiate(RegionPrefab, MapContainer);
            var view = go.GetComponent<StrategicRegionView>();
            if (view != null)
            {
                view.Initialize(region);
                _regionViews[region.RegionId] = view;
            }
        }
        
        private void CreateNodeView(NodeState node)
        {
            if (NodePrefab == null) return;
            
            var go = Instantiate(NodePrefab, MapContainer);
            var view = go.GetComponent<StrategicNodeView>();
            if (view != null)
            {
                view.Initialize(node);
                _nodeViews[GameIds.ResolveNodeId(node.NodeId)] = view;
            }
        }
        
        private void ClearMap()
        {
            foreach (var view in _nodeViews.Values)
            {
                if (view != null) Destroy(view.gameObject);
            }
            _nodeViews.Clear();
            
            foreach (var view in _regionViews.Values)
            {
                if (view != null) Destroy(view.gameObject);
            }
            _regionViews.Clear();

            foreach (var connectionView in _connectionViews)
            {
                if (connectionView != null) Destroy(connectionView);
            }
            _connectionViews.Clear();
        }
        
        private void HandleNodeControlChanged(string nodeId, string oldController, string newController, int controlPoints)
        {
            nodeId = GameIds.ResolveNodeId(nodeId);
            newController = GameIds.ResolveFactionId(newController);
            if (_nodeViews.TryGetValue(nodeId, out var view))
            {
                view.UpdateControl(newController, controlPoints);
            }
        }
        
        private void HandleTurnChanged(int oldTurn, int newTurn)
        {
            UpdateAllNodeVisuals();
        }
        
        private void UpdateAllNodeVisuals()
        {
            foreach (var region in State.Map.Regions)
            {
                foreach (var node in region.Nodes)
                {
                    string canonicalNodeId = GameIds.ResolveNodeId(node.NodeId);
                    if (_nodeViews.TryGetValue(canonicalNodeId, out var view))
                    {
                        view.UpdateFromState(node);
                    }
                }
            }
        }

        private void BuildConnectionViews()
        {
            foreach (var connectionView in _connectionViews)
            {
                if (connectionView != null) Destroy(connectionView);
            }
            _connectionViews.Clear();

            if (ConnectionLinePrefab == null || MapContainer == null)
                return;

            var builtEdges = new HashSet<string>();
            foreach (var pair in _nodeViews)
            {
                string nodeId = pair.Key;
                var sourceView = pair.Value;
                if (sourceView == null)
                    continue;

                var adjacentNodes = GetAdjacentNodes(nodeId);
                foreach (var adjacentId in adjacentNodes)
                {
                    if (!_nodeViews.ContainsKey(adjacentId))
                        continue;

                    string edgeKey = string.CompareOrdinal(nodeId, adjacentId) < 0
                        ? $"{nodeId}|{adjacentId}"
                        : $"{adjacentId}|{nodeId}";

                    if (builtEdges.Contains(edgeKey))
                        continue;
                    builtEdges.Add(edgeKey);

                    var line = Instantiate(ConnectionLinePrefab, MapContainer);
                    line.name = $"Connection_{nodeId}_{adjacentId}";
                    line.transform.position = (sourceView.transform.position + _nodeViews[adjacentId].transform.position) * 0.5f;
                    _connectionViews.Add(line);
                }
            }
        }

        public List<string> GetAdjacentNodes(string nodeId)
        {
            nodeId = GameIds.ResolveNodeId(nodeId);
            if (NodeNetworkSystem != null)
                return NodeNetworkSystem.GetAdjacentNodes(nodeId);

            var adjacent = new List<string>();
            foreach (var region in State.Map.Regions)
            {
                if (region?.Nodes == null)
                    continue;

                bool inRegion = false;
                foreach (var node in region.Nodes)
                {
                    if (node != null && GameIds.ResolveNodeId(node.NodeId) == nodeId)
                    {
                        inRegion = true;
                        break;
                    }
                }

                if (!inRegion)
                    continue;

                foreach (var node in region.Nodes)
                {
                    if (node != null && GameIds.ResolveNodeId(node.NodeId) != nodeId)
                        adjacent.Add(GameIds.ResolveNodeId(node.NodeId));
                }

                break;
            }

            return adjacent;
        }
        
        public StrategicNodeView GetNodeView(string nodeId)
        {
            nodeId = GameIds.ResolveNodeId(nodeId);
            return _nodeViews.TryGetValue(nodeId, out var view) ? view : null;
        }
        
        public List<string> GetNodesControlledBy(string factionId)
        {
            factionId = GameIds.ResolveFactionId(factionId);
            var result = new List<string>();
            foreach (var region in State.Map.Regions)
            {
                foreach (var node in region.Nodes)
                {
                    if (GameIds.ResolveFactionId(node.ControllingFactionId) == factionId)
                    {
                        result.Add(GameIds.ResolveNodeId(node.NodeId));
                    }
                }
            }
            return result;
        }
        
        public int GetTotalControlPoints(string factionId)
        {
            factionId = GameIds.ResolveFactionId(factionId);
            int total = 0;
            foreach (var region in State.Map.Regions)
            {
                foreach (var node in region.Nodes)
                {
                    if (GameIds.ResolveFactionId(node.ControllingFactionId) == factionId)
                    {
                        total += node.ControlPoints;
                    }
                }
            }
            return total;
        }
    }
}
