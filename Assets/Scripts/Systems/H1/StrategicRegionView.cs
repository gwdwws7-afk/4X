using UnityEngine;
using UnityEngine.UI;
using EventideAge.Core;

namespace EventideAge.Systems.H1
{
    public class StrategicRegionView : MonoBehaviour
    {
        [Header("UI References")]
        public Text RegionNameText;
        public Image RegionBackground;
        public Transform NodesContainer;
        
        [Header("Colors")]
        public Color ControlledByPlayer = new Color(0.2f, 0.6f, 0.3f, 0.3f);
        public Color ControlledByEnemy = new Color(0.8f, 0.2f, 0.2f, 0.3f);
        public Color Contested = new Color(0.9f, 0.7f, 0.1f, 0.3f);
        public Color Neutral = new Color(0.3f, 0.3f, 0.3f, 0.2f);
        
        private RegionState _regionState;
        
        public void Initialize(RegionState region)
        {
            _regionState = region;
            
            if (RegionNameText != null)
                RegionNameText.text = region.RegionName;
            
            UpdateRegionColor();
        }
        
        public void UpdateRegionColor()
        {
            if (_regionState == null || _regionState.Nodes == null || _regionState.Nodes.Length == 0)
                return;
            
            int playerCount = 0;
            int enemyCount = 0;
            
            foreach (var node in _regionState.Nodes)
            {
                if (node.ControllingFactionId == GameIds.Faction.Vashid)
                    playerCount++;
                else if (!string.IsNullOrEmpty(node.ControllingFactionId) && node.ControllingFactionId != GameIds.Faction.Neutral)
                    enemyCount++;
            }
            
            Color color;
            if (playerCount > 0 && enemyCount == 0)
                color = ControlledByPlayer;
            else if (enemyCount > 0 && playerCount == 0)
                color = ControlledByEnemy;
            else if (playerCount > 0 && enemyCount > 0)
                color = Contested;
            else
                color = Neutral;
            
            if (RegionBackground != null)
                RegionBackground.color = color;
        }
        
        public RegionState GetRegionState() => _regionState;
    }
}
