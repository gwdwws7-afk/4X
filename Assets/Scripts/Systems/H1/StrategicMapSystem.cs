using UnityEngine;
using System.Collections.Generic;
using EventideAge.Core;

namespace EventideAge.Systems.H1
{
    public class StrategicMapSystem : GameSystem
    {
        [Header("Map Visuals")]
        public Transform MapContainer;
        public GameObject RegionPrefab;
        public GameObject NodePrefab;
        public GameObject ConnectionLinePrefab;
        
        private Dictionary<string, StrategicNodeView> _nodeViews = new Dictionary<string, StrategicNodeView>();
        private Dictionary<string, StrategicRegionView> _regionViews = new Dictionary<string, StrategicRegionView>();
        
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
                _nodeViews[node.NodeId] = view;
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
        }
        
        private void HandleNodeControlChanged(string nodeId, int controlPoints1, int controlPoints2)
        {
            if (_nodeViews.TryGetValue(nodeId, out var view))
            {
                view.UpdateControl(nodeId, controlPoints1);
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
                    if (_nodeViews.TryGetValue(node.NodeId, out var view))
                    {
                        view.UpdateFromState(node);
                    }
                }
            }
        }
        
        public StrategicNodeView GetNodeView(string nodeId)
        {
            return _nodeViews.TryGetValue(nodeId, out var view) ? view : null;
        }
        
        public List<string> GetNodesControlledBy(string factionId)
        {
            var result = new List<string>();
            foreach (var region in State.Map.Regions)
            {
                foreach (var node in region.Nodes)
                {
                    if (node.ControllingFactionId == factionId)
                    {
                        result.Add(node.NodeId);
                    }
                }
            }
            return result;
        }
        
        public int GetTotalControlPoints(string factionId)
        {
            int total = 0;
            foreach (var region in State.Map.Regions)
            {
                foreach (var node in region.Nodes)
                {
                    if (node.ControllingFactionId == factionId)
                    {
                        total += node.ControlPoints;
                    }
                }
            }
            return total;
        }
    }
}
