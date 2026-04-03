using UnityEngine;
using UnityEngine.UI;
using EventideAge.Core;

namespace EventideAge.UI
{
    public class UIManager : GameSystem
    {
        [Header("UI Panels")]
        public GameObject MainMenuPanel;
        public GameObject GameHUDPanel;
        public GameObject ResourcePanel;
        public GameObject PhaseIndicatorPanel;
        public GameObject MapPanel;
        public GameObject ActionPanel;
        public GameObject DiplomacyPanel;
        public GameObject SettingsPanel;
        public GameObject VictoryProgressPanel;
        public GameObject TechTreePanel;
        public GameObject NuclearStatusPanel;
        public GameObject ProxyAffairsPanel;
        
        [Header("HUD Elements")]
        public Text TurnText;
        public Text TimeText;
        public Text PhaseText;
        public Text ActionPointsText;
        public Transform ResourceBarContainer;
        
        [Header("Resource Item Prefab")]
        public GameObject ResourceItemPrefab;
        
        public override void Initialize(GameState state, GameEvents events)
        {
            base.Initialize(state, events);
            Events.OnTurnChanged += HandleTurnChanged;
            Events.OnPhaseChanged += HandlePhaseChanged;
            Events.OnActionPointsChanged += HandleAPChanged;
            Events.OnResourceChanged += HandleResourceChanged;
            
            InitializeResourceBar();
            UpdateAllUI();
        }
        
        private void OnDestroy()
        {
            Events.OnTurnChanged -= HandleTurnChanged;
            Events.OnPhaseChanged -= HandlePhaseChanged;
            Events.OnActionPointsChanged -= HandleAPChanged;
            Events.OnResourceChanged -= HandleResourceChanged;
        }
        
        private void InitializeResourceBar()
        {
            if (ResourceBarContainer == null || ResourceItemPrefab == null) return;
            
            foreach (Transform child in ResourceBarContainer)
            {
                Destroy(child.gameObject);
            }
            
            foreach (var resource in State.Resources)
            {
                var go = Instantiate(ResourceItemPrefab, ResourceBarContainer);
                var ui = go.GetComponent<ResourceItemUI>();
                if (ui != null)
                {
                    ui.Initialize(resource);
                }
            }
        }
        
        private void HandleTurnChanged(int oldTurn, int newTurn)
        {
            UpdateTurnDisplay();
        }
        
        private void HandlePhaseChanged(int newPhaseIndex)
        {
            UpdatePhaseDisplay();
        }
        
        private void HandleAPChanged(int remaining)
        {
            UpdateAPDisplay();
        }
        
        private void HandleResourceChanged(string resourceId, int oldAmount, int newAmount)
        {
            UpdateResourceDisplay(resourceId);
        }
        
        private void UpdateAllUI()
        {
            UpdateTurnDisplay();
            UpdatePhaseDisplay();
            UpdateAPDisplay();
        }
        
        private void UpdateTurnDisplay()
        {
            if (TurnText != null)
                TurnText.text = $"回合 {State.CurrentTurn}";
        }
        
        private void UpdatePhaseDisplay()
        {
            if (PhaseText != null && State.Config?.PhaseConfigs != null)
            {
                string phaseName = State.CurrentPhaseIndex < State.Config.PhaseConfigs.Length
                    ? State.Config.PhaseConfigs[State.CurrentPhaseIndex].PhaseName
                    : $"Phase_{State.CurrentPhaseIndex}";
                PhaseText.text = phaseName;
            }
        }
        
        private void UpdateAPDisplay()
        {
            if (ActionPointsText != null)
            {
                ActionPointsText.text = $"AP: {State.ActionPointsRemaining}";
            }
        }
        
        private void UpdateResourceDisplay(string resourceId)
        {
            if (ResourceBarContainer == null) return;
            
            foreach (Transform child in ResourceBarContainer)
            {
                var ui = child.GetComponent<ResourceItemUI>();
                if (ui != null && ui.ResourceId == resourceId)
                {
                    var resource = State.GetResource(resourceId);
                    if (resource != null)
                    {
                        ui.UpdateDisplay(resource.Amount, resource.MaxCapacity);
                    }
                    break;
                }
            }
        }
        
        public void ShowPanel(GameObject panel)
        {
            if (panel != null)
                panel.SetActive(true);
        }
        
        public void HidePanel(GameObject panel)
        {
            if (panel != null)
                panel.SetActive(false);
        }
        
        public void ShowGameHUD()
        {
            if (GameHUDPanel != null)
                GameHUDPanel.SetActive(true);
            if (MainMenuPanel != null)
                MainMenuPanel.SetActive(false);
        }
        
        public void ShowMainMenu()
        {
            if (GameHUDPanel != null)
                GameHUDPanel.SetActive(false);
            if (MainMenuPanel != null)
                MainMenuPanel.SetActive(true);
        }
        
        public void ShowVictoryProgress()
        {
            if (VictoryProgressPanel != null)
                VictoryProgressPanel.SetActive(true);
        }
        
        public void HideVictoryProgress()
        {
            if (VictoryProgressPanel != null)
                VictoryProgressPanel.SetActive(false);
        }
        
        public void ShowTechTree()
        {
            if (TechTreePanel != null)
                TechTreePanel.SetActive(true);
        }
        
        public void HideTechTree()
        {
            if (TechTreePanel != null)
                TechTreePanel.SetActive(false);
        }
        
        public void ShowNuclearStatus()
        {
            if (NuclearStatusPanel != null)
                NuclearStatusPanel.SetActive(true);
        }
        
        public void HideNuclearStatus()
        {
            if (NuclearStatusPanel != null)
                NuclearStatusPanel.SetActive(false);
        }
        
        public void ShowProxyAffairs()
        {
            if (ProxyAffairsPanel != null)
                ProxyAffairsPanel.SetActive(true);
        }
        
        public void HideProxyAffairs()
        {
            if (ProxyAffairsPanel != null)
                ProxyAffairsPanel.SetActive(false);
        }
    }
}
