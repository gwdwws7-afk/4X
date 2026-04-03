using UnityEngine;
using UnityEngine.UI;
using EventideAge.Core;
using EventideAge.Systems.D6;

namespace EventideAge.UI
{
    public class MilitaryTechUI : GameSystem
    {
        [Header("UI References")]
        public GameObject TechTreePanel;
        public Transform TechListContainer;
        public GameObject TechListItemPrefab;
        
        [Header("Research Info")]
        public Text CurrentResearchText;
        public Text ResearchProgressText;
        public Image ResearchProgressFill;
        public Button CancelResearchButton;
        
        private MilitaryTechSystem _techSystem;
        
        public override void Initialize(GameState state, GameEvents events)
        {
            base.Initialize(state, events);
            
            if (TechTreePanel != null)
                TechTreePanel.SetActive(false);
        }
        
        public void ShowTechTree()
        {
            if (TechTreePanel == null) return;
            TechTreePanel.SetActive(true);
            RefreshTechList();
        }
        
        public void HideTechTree()
        {
            if (TechTreePanel != null)
                TechTreePanel.SetActive(false);
        }
        
        public void RefreshTechList()
        {
            if (TechListContainer == null) return;
            
            foreach (Transform child in TechListContainer)
            {
                Destroy(child.gameObject);
            }
            
            var techSystem = GetComponent<MilitaryTechSystem>();
            if (techSystem == null) return;
            
            var availableTechs = techSystem.GetAvailableTechs();
            foreach (var tech in availableTechs)
            {
                var item = Instantiate(TechListItemPrefab, TechListContainer);
                var techItem = item.GetComponent<TechListItem>();
                if (techItem != null)
                {
                    techItem.Setup(tech, () => OnTechSelected(tech.TechId));
                }
            }
            
            UpdateResearchInfo();
        }
        
        private void OnTechSelected(string techId)
        {
            var techSystem = GetComponent<MilitaryTechSystem>();
            if (techSystem != null)
            {
                techSystem.StartResearch(techId);
                RefreshTechList();
            }
        }
        
        private void UpdateResearchInfo()
        {
            var techSystem = GetComponent<MilitaryTechSystem>();
            if (techSystem == null) return;
            
            var research = techSystem.GetCurrentResearch();
            if (research != null)
            {
                if (CurrentResearchText != null)
                    CurrentResearchText.text = $"研究: {research.TechId}";
                
                if (ResearchProgressText != null)
                    ResearchProgressText.text = $"{research.Progress}/? 回合";
                
                if (ResearchProgressFill != null)
                    ResearchProgressFill.fillAmount = techSystem.GetResearchProgress();
                
                if (CancelResearchButton != null)
                    CancelResearchButton.gameObject.SetActive(true);
            }
            else
            {
                if (CurrentResearchText != null)
                    CurrentResearchText.text = "无进行中研究";
                
                if (ResearchProgressText != null)
                    ResearchProgressText.text = "";
                
                if (ResearchProgressFill != null)
                    ResearchProgressFill.fillAmount = 0;
                
                if (CancelResearchButton != null)
                    CancelResearchButton.gameObject.SetActive(false);
            }
        }
        
        public void OnCancelResearchClicked()
        {
            var techSystem = GetComponent<MilitaryTechSystem>();
            if (techSystem != null)
            {
                techSystem.CancelResearch();
                RefreshTechList();
            }
        }
    }
    
    public class TechListItem : MonoBehaviour
    {
        public Text TechNameText;
        public Text DescriptionText;
        public Text CostText;
        public Button SelectButton;
        
        private System.Action _onSelect;
        
        public void Setup(TechDefinition tech, System.Action onSelect)
        {
            if (TechNameText != null)
                TechNameText.text = tech.TechName;
            if (DescriptionText != null)
                DescriptionText.text = tech.Description;
            if (CostText != null)
                CostText.text = $"{tech.GoldLeafCost} 金叶";
            
            _onSelect = onSelect;
            if (SelectButton != null)
                SelectButton.onClick.AddListener(() => _onSelect?.Invoke());
        }
    }
}