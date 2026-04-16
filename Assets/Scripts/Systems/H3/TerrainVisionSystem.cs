using System.Collections.Generic;
using UnityEngine;
using EventideAge.Core;
using EventideAge.Systems.F1;
using EventideAge.Systems.H2;

namespace EventideAge.Systems.H3
{
    public enum TerrainType
    {
        Plain,
        Urban,
        Mountain,
        Coastal,
        Desert
    }

    public enum VisionLevel
    {
        Unknown,
        Detected,
        Observed,
        Controlled
    }

    public class TerrainVisionNodeState
    {
        public string NodeId;
        public TerrainType TerrainType;
        public VisionLevel VisionLevel;
        public float MovementModifier;
        public float DefenseModifier;
        public float IntelConfidence;
        public int LastUpdatedTurn;
    }

    public class TerrainVisionSystem : GameSystem
    {
        public NodeNetworkSystem NodeNetworkSystem { get; set; }
        public IntelligenceSystem IntelligenceSystem { get; set; }

        [Header("Vision")]
        public int SecondaryVisionDepth = 1;
        public float IntelDecayPerTurn = 0.2f;

        private readonly Dictionary<string, TerrainVisionNodeState> _nodeStates = new Dictionary<string, TerrainVisionNodeState>();

        public override void Initialize(GameState state, GameEvents events)
        {
            base.Initialize(state, events);
            BuildTerrainStates();
            RecalculateVision();

            Events.OnTurnEnded += HandleTurnEnded;
            Events.OnNodeControlChanged += HandleNodeControlChanged;
            Debug.Log("[TerrainVisionSystem] Initialized");
        }

        private void OnDestroy()
        {
            if (Events != null)
            {
                Events.OnTurnEnded -= HandleTurnEnded;
                Events.OnNodeControlChanged -= HandleNodeControlChanged;
            }
        }

        public void RecalculateVision()
        {
            if (_nodeStates.Count == 0)
                BuildTerrainStates();

            foreach (var pair in _nodeStates)
            {
                var nodeState = pair.Value;
                nodeState.VisionLevel = GetVisionFromIntelConfidence(nodeState.IntelConfidence);
            }

            foreach (var controlledNodeId in GetControlledNodes(GameIds.Faction.Vashid))
            {
                if (_nodeStates.TryGetValue(controlledNodeId, out var nodeState))
                {
                    nodeState.VisionLevel = VisionLevel.Controlled;
                    nodeState.IntelConfidence = Mathf.Max(nodeState.IntelConfidence, 1f);
                    nodeState.LastUpdatedTurn = State.CurrentTurn;
                }
            }

            ApplyDepthAwareVision();
        }

        public void ApplyIntelligenceReport(string nodeId, FogLevel fogLevel, float reliability)
        {
            nodeId = GameIds.ResolveNodeId(nodeId);
            if (!_nodeStates.TryGetValue(nodeId, out var nodeState))
                return;

            nodeState.IntelConfidence = Mathf.Clamp01(Mathf.Max(nodeState.IntelConfidence, reliability));
            nodeState.VisionLevel = MaxVision(nodeState.VisionLevel, ConvertFogToVision(fogLevel));
            nodeState.LastUpdatedTurn = State.CurrentTurn;
        }

        public VisionLevel GetVisionLevel(string nodeId)
        {
            nodeId = GameIds.ResolveNodeId(nodeId);
            if (_nodeStates.TryGetValue(nodeId, out var nodeState))
                return nodeState.VisionLevel;
            return VisionLevel.Unknown;
        }

        public bool IsVisibleToPlayer(string nodeId)
        {
            nodeId = GameIds.ResolveNodeId(nodeId);
            return GetVisionLevel(nodeId) != VisionLevel.Unknown;
        }

        public float GetMovementModifier(string nodeId)
        {
            nodeId = GameIds.ResolveNodeId(nodeId);
            if (_nodeStates.TryGetValue(nodeId, out var nodeState))
                return nodeState.MovementModifier;
            return 1f;
        }

        public float GetDefenseModifier(string nodeId)
        {
            nodeId = GameIds.ResolveNodeId(nodeId);
            if (_nodeStates.TryGetValue(nodeId, out var nodeState))
                return nodeState.DefenseModifier;
            return 1f;
        }

        public TerrainVisionNodeState GetNodeState(string nodeId)
        {
            nodeId = GameIds.ResolveNodeId(nodeId);
            if (_nodeStates.TryGetValue(nodeId, out var state))
                return state;
            return null;
        }

        private void BuildTerrainStates()
        {
            _nodeStates.Clear();
            if (State?.Map?.Regions == null)
                return;

            foreach (var region in State.Map.Regions)
            {
                if (region?.Nodes == null)
                    continue;

                foreach (var node in region.Nodes)
                {
                    if (node == null || string.IsNullOrEmpty(node.NodeId))
                        continue;

                    string canonicalNodeId = GameIds.ResolveNodeId(node.NodeId);
                    var terrain = ResolveTerrainType(node, region.RegionId);
                    var nodeState = new TerrainVisionNodeState
                    {
                        NodeId = canonicalNodeId,
                        TerrainType = terrain,
                        VisionLevel = VisionLevel.Unknown,
                        MovementModifier = ResolveMovementModifier(terrain),
                        DefenseModifier = ResolveDefenseModifier(terrain),
                        IntelConfidence = 0f,
                        LastUpdatedTurn = State.CurrentTurn
                    };

                    _nodeStates[canonicalNodeId] = nodeState;
                }
            }
        }

        private List<string> GetControlledNodes(string factionId)
        {
            factionId = GameIds.ResolveFactionId(factionId);
            if (NodeNetworkSystem != null)
                return NodeNetworkSystem.GetNodesControlledBy(factionId);

            var nodes = new List<string>();
            if (State?.Map?.Regions == null)
                return nodes;

            foreach (var region in State.Map.Regions)
            {
                if (region?.Nodes == null)
                    continue;

                foreach (var node in region.Nodes)
                {
                    if (node != null && GameIds.ResolveFactionId(node.ControllingFactionId) == factionId)
                        nodes.Add(GameIds.ResolveNodeId(node.NodeId));
                }
            }

            return nodes;
        }

        private List<string> GetAdjacentNodes(string nodeId)
        {
            nodeId = GameIds.ResolveNodeId(nodeId);
            if (NodeNetworkSystem != null)
                return NodeNetworkSystem.GetAdjacentNodes(nodeId);

            var adjacent = new List<string>();
            if (State?.Map?.Regions == null)
                return adjacent;

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

        private void ApplyDepthAwareVision()
        {
            int maxDepthFromControlled = Mathf.Max(1, SecondaryVisionDepth + 1);
            var controlledNodes = GetControlledNodes(GameIds.Faction.Vashid);

            for (int i = 0; i < controlledNodes.Count; i++)
            {
                var startNodeId = controlledNodes[i];
                var visited = new HashSet<string> { startNodeId };
                var queue = new Queue<(string nodeId, int depth)>();
                queue.Enqueue((startNodeId, 0));

                while (queue.Count > 0)
                {
                    var current = queue.Dequeue();
                    if (current.depth >= maxDepthFromControlled)
                        continue;

                    var neighbors = GetAdjacentNodes(current.nodeId);
                    for (int n = 0; n < neighbors.Count; n++)
                    {
                        var neighborId = neighbors[n];
                        if (!visited.Add(neighborId))
                            continue;

                        int depth = current.depth + 1;
                        if (!_nodeStates.TryGetValue(neighborId, out var state))
                        {
                            queue.Enqueue((neighborId, depth));
                            continue;
                        }

                        if (depth == 1)
                        {
                            state.VisionLevel = MaxVision(state.VisionLevel, VisionLevel.Observed);
                            state.IntelConfidence = Mathf.Max(state.IntelConfidence, 0.7f);
                        }
                        else
                        {
                            state.VisionLevel = MaxVision(state.VisionLevel, VisionLevel.Detected);
                            state.IntelConfidence = Mathf.Max(state.IntelConfidence, 0.4f);
                        }

                        state.LastUpdatedTurn = State.CurrentTurn;
                        queue.Enqueue((neighborId, depth));
                    }
                }
            }
        }

        private TerrainType ResolveTerrainType(NodeState node, string regionId)
        {
            switch (node.NodeType)
            {
                case NodeType.Port:
                    return TerrainType.Coastal;
                case NodeType.City:
                    return TerrainType.Urban;
                case NodeType.Chokepoint:
                    return TerrainType.Mountain;
                case NodeType.ResourceNode:
                    return TerrainType.Desert;
                default:
                    return TerrainType.Plain;
            }
        }

        private float ResolveMovementModifier(TerrainType terrain)
        {
            switch (terrain)
            {
                case TerrainType.Mountain: return 0.75f;
                case TerrainType.Desert: return 0.85f;
                case TerrainType.Coastal: return 0.9f;
                case TerrainType.Urban: return 0.95f;
                default: return 1f;
            }
        }

        private float ResolveDefenseModifier(TerrainType terrain)
        {
            switch (terrain)
            {
                case TerrainType.Mountain: return 1.25f;
                case TerrainType.Urban: return 1.15f;
                case TerrainType.Coastal: return 1.1f;
                case TerrainType.Desert: return 0.95f;
                default: return 1f;
            }
        }

        private VisionLevel ConvertFogToVision(FogLevel fogLevel)
        {
            switch (fogLevel)
            {
                case FogLevel.Clear:
                    return VisionLevel.Observed;
                case FogLevel.Partial:
                    return VisionLevel.Detected;
                case FogLevel.Obscured:
                    return VisionLevel.Detected;
                default:
                    return VisionLevel.Unknown;
            }
        }

        private VisionLevel GetVisionFromIntelConfidence(float confidence)
        {
            if (confidence >= 0.95f) return VisionLevel.Controlled;
            if (confidence >= 0.65f) return VisionLevel.Observed;
            if (confidence >= 0.25f) return VisionLevel.Detected;
            return VisionLevel.Unknown;
        }

        private VisionLevel MaxVision(VisionLevel current, VisionLevel incoming)
        {
            return (VisionLevel)Mathf.Max((int)current, (int)incoming);
        }

        private void HandleTurnEnded(int turnNumber)
        {
            foreach (var pair in _nodeStates)
            {
                pair.Value.IntelConfidence = Mathf.Max(0f, pair.Value.IntelConfidence - IntelDecayPerTurn);
            }

            RecalculateVision();
        }

        private void HandleNodeControlChanged(string nodeId, string oldController, string newController, int controlPoints)
        {
            RecalculateVision();
        }
    }
}
