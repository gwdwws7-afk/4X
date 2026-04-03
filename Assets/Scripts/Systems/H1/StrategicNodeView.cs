using UnityEngine;
using UnityEngine.UI;
using EventideAge.Core;

namespace EventideAge.Systems.H1
{
    public class StrategicNodeView : MonoBehaviour
    {
        [Header("UI References")]
        public Image NodeIcon;
        public Image ControlFill;
        public Text NodeNameText;
        public Text ControlPointsText;
        public Image[] ResourceIndicators;
        
        [Header("Colors")]
        public Color PlayerColor = new Color(0.2f, 0.6f, 0.3f);
        public Color EnemyColor = new Color(0.8f, 0.2f, 0.2f);
        public Color NeutralColor = new Color(0.5f, 0.5f, 0.5f);
        public Color ContestedColor = new Color(0.9f, 0.7f, 0.1f);
        
        private NodeState _nodeState;
        private NodeType _nodeType;
        
        public void Initialize(NodeState node)
        {
            _nodeState = node;
            _nodeType = node.NodeType;
            
            UpdateFromState(node);
        }
        
        public void UpdateFromState(NodeState node)
        {
            _nodeState = node;
            
            if (NodeNameText != null)
                NodeNameText.text = node.NodeName;
            
            if (ControlFill != null)
                ControlFill.fillAmount = (float)node.ControlPoints / node.MaxControlPoints;
            
            if (ControlPointsText != null)
                ControlPointsText.text = $"{node.ControlPoints}/{node.MaxControlPoints}";
            
            UpdateNodeColor();
        }
        
        public void UpdateControl(string controllerId, int controlPoints)
        {
            if (_nodeState != null)
            {
                _nodeState.ControllingFactionId = controllerId;
                _nodeState.ControlPoints = controlPoints;
            }
            UpdateNodeColor();
        }
        
        private void UpdateNodeColor()
        {
            if (_nodeState == null || NodeIcon == null) return;
            
            string controller = _nodeState.ControllingFactionId;
            Color color;
            
            if (string.IsNullOrEmpty(controller) || controller == "Neutral")
            {
                color = NeutralColor;
            }
            else if (controller == "Vashid")
            {
                color = PlayerColor;
            }
            else
            {
                color = EnemyColor;
            }
            
            NodeIcon.color = color;
            
            if (ControlFill != null)
            {
                ControlFill.color = color;
            }
        }
        
        public NodeState GetNodeState() => _nodeState;
        public string GetNodeId() => _nodeState?.NodeId;
    }
}
