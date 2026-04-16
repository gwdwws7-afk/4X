using System.Collections.Generic;
using UnityEngine;
using EventideAge.Core;

namespace EventideAge.Systems.H2
{
    public class NodeNetworkSystem : GameSystem
    {
        private readonly Dictionary<string, HashSet<string>> _adjacency = new Dictionary<string, HashSet<string>>();
        private readonly Dictionary<string, NodeState> _nodesById = new Dictionary<string, NodeState>();
        private readonly Dictionary<string, string> _nodeRegionById = new Dictionary<string, string>();
        private static readonly (string from, string to)[] kDefaultInterRegionLinks = new[]
        {
            (GameIds.Node.Hormuz, GameIds.Node.IraqBorder),
            (GameIds.Node.Bushehr, GameIds.Node.Caucasus),
            (GameIds.Node.Caspian, GameIds.Node.IraqBorder),
            (GameIds.Node.SyriaZone, GameIds.Node.Mediterranean),
            (GameIds.Node.Mediterranean, GameIds.Node.RedSea),
            (GameIds.Node.GulfBase, GameIds.Node.TradeHub),
            (GameIds.Node.Afghanistan, GameIds.Node.Caucasus)
        };

        public override void Initialize(GameState state, GameEvents events)
        {
            base.Initialize(state, events);
            RebuildNetwork();

            Events.OnNodeControlChanged += HandleNodeControlChanged;
            Debug.Log("[NodeNetworkSystem] Initialized");
        }

        private void OnDestroy()
        {
            if (Events != null)
            {
                Events.OnNodeControlChanged -= HandleNodeControlChanged;
            }
        }

        public void RebuildNetwork()
        {
            _adjacency.Clear();
            _nodesById.Clear();
            _nodeRegionById.Clear();

            if (State?.Map?.Regions == null)
                return;

            foreach (var region in State.Map.Regions)
            {
                if (region?.Nodes == null)
                    continue;

                for (int i = 0; i < region.Nodes.Length; i++)
                {
                    var node = region.Nodes[i];
                    if (node == null || string.IsNullOrEmpty(node.NodeId))
                        continue;

                    string canonicalNodeId = GameIds.ResolveNodeId(node.NodeId);
                    EnsureNode(canonicalNodeId);
                    _nodesById[canonicalNodeId] = node;
                    _nodeRegionById[canonicalNodeId] = region.RegionId;
                }

                for (int i = 0; i < region.Nodes.Length; i++)
                {
                    var nodeA = region.Nodes[i];
                    if (nodeA == null || string.IsNullOrEmpty(nodeA.NodeId))
                        continue;

                    for (int j = i + 1; j < region.Nodes.Length; j++)
                    {
                        var nodeB = region.Nodes[j];
                        if (nodeB == null || string.IsNullOrEmpty(nodeB.NodeId))
                            continue;

                        Connect(GameIds.ResolveNodeId(nodeA.NodeId), GameIds.ResolveNodeId(nodeB.NodeId));
                    }
                }
            }

            AddDefaultInterRegionLinks();
        }

        public bool HasNode(string nodeId)
        {
            nodeId = GameIds.ResolveNodeId(nodeId);
            return _nodesById.ContainsKey(nodeId);
        }

        public int GetNodeCount()
        {
            return _nodesById.Count;
        }

        public int GetConnectionCount()
        {
            int edgeCount = 0;
            foreach (var pair in _adjacency)
            {
                edgeCount += pair.Value.Count;
            }

            return edgeCount / 2;
        }

        public bool LinkNodes(string nodeA, string nodeB)
        {
            nodeA = GameIds.ResolveNodeId(nodeA);
            nodeB = GameIds.ResolveNodeId(nodeB);
            if (string.IsNullOrEmpty(nodeA) || string.IsNullOrEmpty(nodeB) || nodeA == nodeB)
                return false;

            if (!_nodesById.ContainsKey(nodeA) || !_nodesById.ContainsKey(nodeB))
                return false;

            Connect(nodeA, nodeB);
            return true;
        }

        public NodeState GetNodeState(string nodeId)
        {
            nodeId = GameIds.ResolveNodeId(nodeId);
            if (_nodesById.TryGetValue(nodeId, out var node))
                return node;
            return null;
        }

        public string GetNodeRegionId(string nodeId)
        {
            nodeId = GameIds.ResolveNodeId(nodeId);
            if (_nodeRegionById.TryGetValue(nodeId, out var regionId))
                return regionId;
            return string.Empty;
        }

        public List<string> GetAdjacentNodes(string nodeId)
        {
            nodeId = GameIds.ResolveNodeId(nodeId);
            if (_adjacency.TryGetValue(nodeId, out var neighbors))
            {
                return new List<string>(neighbors);
            }
            return new List<string>();
        }

        public bool AreAdjacent(string nodeA, string nodeB)
        {
            nodeA = GameIds.ResolveNodeId(nodeA);
            nodeB = GameIds.ResolveNodeId(nodeB);
            if (string.IsNullOrEmpty(nodeA) || string.IsNullOrEmpty(nodeB))
                return false;

            if (nodeA == nodeB)
                return true;

            return _adjacency.TryGetValue(nodeA, out var neighbors) && neighbors.Contains(nodeB);
        }

        public int GetDistance(string startNodeId, string endNodeId, int maxDepth = 12)
        {
            startNodeId = GameIds.ResolveNodeId(startNodeId);
            endNodeId = GameIds.ResolveNodeId(endNodeId);
            if (string.IsNullOrEmpty(startNodeId) || string.IsNullOrEmpty(endNodeId))
                return -1;

            if (startNodeId == endNodeId)
                return 0;

            if (!_adjacency.ContainsKey(startNodeId) || !_adjacency.ContainsKey(endNodeId))
                return -1;

            var visited = new HashSet<string> { startNodeId };
            var queue = new Queue<(string nodeId, int depth)>();
            queue.Enqueue((startNodeId, 0));

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                if (current.depth >= maxDepth)
                    continue;

                foreach (var neighbor in _adjacency[current.nodeId])
                {
                    if (visited.Contains(neighbor))
                        continue;

                    if (neighbor == endNodeId)
                        return current.depth + 1;

                    visited.Add(neighbor);
                    queue.Enqueue((neighbor, current.depth + 1));
                }
            }

            return -1;
        }

        public List<string> GetNodesControlledBy(string factionId)
        {
            factionId = GameIds.ResolveFactionId(factionId);
            var nodes = new List<string>();
            foreach (var pair in _nodesById)
            {
                if (pair.Value != null && GameIds.ResolveFactionId(pair.Value.ControllingFactionId) == factionId)
                {
                    nodes.Add(pair.Key);
                }
            }
            return nodes;
        }

        public List<string> GetContestedNodes()
        {
            var nodes = new List<string>();
            foreach (var pair in _nodesById)
            {
                var node = pair.Value;
                if (node == null)
                    continue;

                if (node.ControlPoints > 0 && node.ControlPoints < node.MaxControlPoints)
                {
                    nodes.Add(pair.Key);
                }
            }
            return nodes;
        }

        public bool IsNodeAccessibleToFaction(string nodeId, string factionId)
        {
            nodeId = GameIds.ResolveNodeId(nodeId);
            factionId = GameIds.ResolveFactionId(factionId);
            var node = GetNodeState(nodeId);
            if (node == null)
                return false;

            if (GameIds.ResolveFactionId(node.ControllingFactionId) == factionId)
                return true;

            var controlledNodes = GetNodesControlledBy(factionId);
            foreach (var controlled in controlledNodes)
            {
                if (AreAdjacent(nodeId, controlled))
                    return true;
            }

            return false;
        }

        private void EnsureNode(string nodeId)
        {
            nodeId = GameIds.ResolveNodeId(nodeId);
            if (!_adjacency.ContainsKey(nodeId))
            {
                _adjacency[nodeId] = new HashSet<string>();
            }
        }

        private void Connect(string nodeA, string nodeB)
        {
            nodeA = GameIds.ResolveNodeId(nodeA);
            nodeB = GameIds.ResolveNodeId(nodeB);
            EnsureNode(nodeA);
            EnsureNode(nodeB);
            _adjacency[nodeA].Add(nodeB);
            _adjacency[nodeB].Add(nodeA);
        }

        private void AddDefaultInterRegionLinks()
        {
            // Only apply the canonical world-map links when the full node registry is present.
            if (_nodesById.Count < 10)
                return;

            for (int i = 0; i < kDefaultInterRegionLinks.Length; i++)
            {
                var link = kDefaultInterRegionLinks[i];
                LinkNodes(link.from, link.to);
            }
        }

        private void HandleNodeControlChanged(string nodeId, string oldController, string newController, int controlPoints)
        {
            nodeId = GameIds.ResolveNodeId(nodeId);
            if (_nodesById.TryGetValue(nodeId, out var node) && node != null)
            {
                node.ControllingFactionId = GameIds.ResolveFactionId(newController);
                node.ControlPoints = controlPoints;
            }
        }
    }
}
