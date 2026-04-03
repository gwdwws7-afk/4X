using UnityEngine;
using UnityEngine.UI;
using EventideAge.Core;
using EventideAge.Systems.D4;

namespace EventideAge.UI
{
    public class NuclearDeterrenceUI : GameSystem
    {
        [Header("UI References")]
        public GameObject NuclearStatusPanel;
        
        [Header("Status Display")]
        public Text WarheadCountText;
        public Text CapabilityLevelText;
        public Text CooldownText;
        public Image FullWarLockIndicator;
        
        [Header("Action Buttons")]
        public Button DisplayDeterrenceButton;
        public Text DisplayDeterrenceCostText;
        
        public override void Initialize(GameState state, GameEvents events)
        {
            base.Initialize(state, events);
            
            if (NuclearStatusPanel != null)
                NuclearStatusPanel.SetActive(false);
        }
        
        public void ShowNuclearStatus()
        {
            if (NuclearStatusPanel == null) return;
            NuclearStatusPanel.SetActive(true);
            RefreshStatus();
        }
        
        public void HideNuclearStatus()
        {
            if (NuclearStatusPanel != null)
                NuclearStatusPanel.SetActive(false);
        }
        
        public void RefreshStatus()
        {
            var nuclearSystem = GetComponent<NuclearDeterrenceSystem>();
            if (nuclearSystem == null) return;
            
            var state = nuclearSystem.GetState();
            if (state == null) return;
            
            if (WarheadCountText != null)
                WarheadCountText.text = $"弹头数量: {state.WarheadCount}";
            
            if (CapabilityLevelText != null)
                CapabilityLevelText.text = $"威慑等级: {GetCapabilityName(state.CapabilityLevel)}";
            
            if (CooldownText != null)
            {
                if (state.DisplayCooldown > 0)
                    CooldownText.text = $"冷却: {state.DisplayCooldown} 回合";
                else
                    CooldownText.text = "就绪";
            }
            
            if (FullWarLockIndicator != null)
                FullWarLockIndicator.gameObject.SetActive(state.IsFullWarLockActive);
            
            if (DisplayDeterrenceButton != null)
            {
                DisplayDeterrenceButton.interactable = nuclearSystem.CanDisplayDeterrence();
            }
            
            if (DisplayDeterrenceCostText != null)
            {
                DisplayDeterrenceCostText.text = GetDisplayCostText(state.CapabilityLevel);
            }
        }
        
        public void OnDisplayDeterrenceClicked()
        {
            var nuclearSystem = GetComponent<NuclearDeterrenceSystem>();
            if (nuclearSystem != null)
            {
                nuclearSystem.ExecuteDeterrenceDisplay();
                RefreshStatus();
            }
        }
        
        private string GetCapabilityName(NuclearCapabilityLevel level)
        {
            switch (level)
            {
                case NuclearCapabilityLevel.None: return "无";
                case NuclearCapabilityLevel.Limited: return "有限";
                case NuclearCapabilityLevel.Credible: return "可信";
                case NuclearCapabilityLevel.Enhanced: return "强化";
                case NuclearCapabilityLevel.Absolute: return "绝对";
                default: return "未知";
            }
        }
        
        private string GetDisplayCostText(NuclearCapabilityLevel level)
        {
            switch (level)
            {
                case NuclearCapabilityLevel.Limited: return "-10 灰烬志";
                case NuclearCapabilityLevel.Credible: return "-15 灰烬志, -10 商盟券";
                case NuclearCapabilityLevel.Enhanced: return "-20 灰烬志, -15 商盟券";
                case NuclearCapabilityLevel.Absolute: return "-25 灰烬志, -20 商盟券";
                default: return "无法展示";
            }
        }
    }
}