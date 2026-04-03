using UnityEngine;
using UnityEngine.UI;
using EventideAge.Core;
using EventideAge.Systems.D3;

namespace EventideAge.UI
{
    public class ProxyAffairsUI : GameSystem
    {
        [Header("UI References")]
        public GameObject ProxyAffairsPanel;
        public Transform RegionListContainer;
        public GameObject RegionStatusPrefab;
        
        [Header("Governance Controls")]
        public Dropdown GovernanceLevelDropdown;
        public Button ApplyGovernanceButton;
        
        private string _selectedRegionId;
        
        public override void Initialize(GameState state, GameEvents events)
        {
            base.Initialize(state, events);
            
            if (ProxyAffairsPanel != null)
                ProxyAffairsPanel.SetActive(false);
        }
        
        public void ShowProxyAffairs()
        {
            if (ProxyAffairsPanel == null) return;
            ProxyAffairsPanel.SetActive(true);
            RefreshRegionList();
        }
        
        public void HideProxyAffairs()
        {
            if (ProxyAffairsPanel != null)
                ProxyAffairsPanel.SetActive(false);
        }
        
        public void RefreshRegionList()
        {
            if (RegionListContainer == null) return;
            
            foreach (Transform child in RegionListContainer)
            {
                Destroy(child.gameObject);
            }
            
            var proxySystem = GetComponent<ProxyCivilAffairsSystem>();
            if (proxySystem == null) return;
            
            foreach (var region in State.Map.Regions)
            {
                foreach (var node in region.Nodes)
                {
                    var proxyRegion = proxySystem.GetProxyRegion(node.NodeId);
                    if (proxyRegion != null)
                    {
                        var item = Instantiate(RegionStatusPrefab, RegionListContainer);
                        var regionItem = item.GetComponent<ProxyRegionItem>();
                        if (regionItem != null)
                        {
                            regionItem.Setup(proxyRegion, this);
                        }
                    }
                }
            }
        }
        
        public void SelectRegion(string regionId)
        {
            _selectedRegionId = regionId;
        }
        
        public void OnApplyGovernanceClicked()
        {
            if (string.IsNullOrEmpty(_selectedRegionId)) return;
            
            var proxySystem = GetComponent<ProxyCivilAffairsSystem>();
            if (proxySystem == null) return;
            
            GovernanceLevel level = GovernanceLevelDropdown != null 
                ? (GovernanceLevel)GovernanceLevelDropdown.value 
                : GovernanceLevel.Basic;
            
            proxySystem.SetGovernanceLevel(_selectedRegionId, level);
            proxySystem.ApplyGovernance(_selectedRegionId);
            RefreshRegionList();
        }
    }
    
    public class ProxyRegionItem : MonoBehaviour
    {
        public Text NodeNameText;
        public Slider StabilitySlider;
        public Slider PublicSupportSlider;
        public Text StatusText;
        public Text OutputModifierText;
        public Button SelectButton;
        
        private ProxyControlRegion _region;
        private ProxyAffairsUI _parentUI;
        
        public void Setup(ProxyControlRegion region, ProxyAffairsUI parentUI)
        {
            _region = region;
            _parentUI = parentUI;
            
            if (NodeNameText != null)
                NodeNameText.text = region.NodeId;
            
            if (StabilitySlider != null)
            {
                StabilitySlider.maxValue = 100;
                StabilitySlider.value = region.Stability;
            }
            
            if (PublicSupportSlider != null)
            {
                PublicSupportSlider.maxValue = 100;
                PublicSupportSlider.value = region.PublicSupport;
            }
            
            if (StatusText != null)
            {
                StatusText.text = GetStatusName(_region.Stability, _region.PublicSupport);
            }
            
            if (OutputModifierText != null && _parentUI != null)
            {
                float mod = _parentUI.GetComponent<ProxyCivilAffairsSystem>()?.GetOutputModifier(region.NodeId) ?? 1f;
                OutputModifierText.text = $"产出: {mod * 100:F0}%";
            }
            
            if (SelectButton != null)
                SelectButton.onClick.AddListener(() => _parentUI?.SelectRegion(region.NodeId));
        }
        
        private string GetStatusName(int stability, int publicSupport)
        {
            if (stability > 70 && publicSupport > 60) return "稳固";
            if (stability > 40 && publicSupport > 40) return "一般";
            if (stability > 20 && publicSupport > 20) return "动荡";
            return "崩溃";
        }
    }
}