using UnityEngine;
using UnityEngine.UI;
using EventideAge.Core;
using EventideAge.Systems.J;

namespace EventideAge.UI
{
    public class VictoryProgressUI : GameSystem
    {
        [Header("UI References")]
        public GameObject VictoryProgressPanel;
        public Transform ProgressBarContainer;
        public GameObject ProgressBarPrefab;
        
        [Header("Defeat Warning")]
        public GameObject DefeatWarningPanel;
        public Text DefeatWarningText;
        
        private GameObject[] _progressBars;
        
        public override void Initialize(GameState state, GameEvents events)
        {
            base.Initialize(state, events);
            
            if (VictoryProgressPanel != null)
                VictoryProgressPanel.SetActive(false);
            if (DefeatWarningPanel != null)
                DefeatWarningPanel.SetActive(false);
        }
        
        public void ShowVictoryProgress()
        {
            if (VictoryProgressPanel == null) return;
            VictoryProgressPanel.SetActive(true);
            
            var victorySystem = FindVictorySystem();
            if (victorySystem == null || ProgressBarContainer == null) return;
            
            var paths = victorySystem.GetAllVictoryPaths();
            
            foreach (Transform child in ProgressBarContainer)
            {
                Destroy(child.gameObject);
            }
            
            foreach (var path in paths)
            {
                var bar = Instantiate(ProgressBarPrefab, ProgressBarContainer);
                var barUI = bar.GetComponent<VictoryPathBar>();
                if (barUI != null)
                {
                    barUI.Setup(path.Name, path.Progress / 100f);
                }
            }
        }
        
        public void UpdateProgress()
        {
            var victorySystem = FindVictorySystem();
            if (victorySystem == null) return;
            
            var paths = victorySystem.GetAllVictoryPaths();
            int index = 0;
            
            foreach (Transform child in ProgressBarContainer)
            {
                if (index < paths.Length)
                {
                    var barUI = child.GetComponent<VictoryPathBar>();
                    if (barUI != null)
                    {
                        barUI.UpdateProgress(paths[index].Progress / 100f);
                    }
                }
                index++;
            }
            
            CheckDefeatRisks();
        }
        
        private void CheckDefeatRisks()
        {
            var victorySystem = FindVictorySystem();
            if (victorySystem == null || DefeatWarningPanel == null) return;
            
            var risks = victorySystem.GetCurrentDefeatRisks();
            if (risks != null && risks.Length > 0)
            {
                DefeatWarningPanel.SetActive(true);
                if (DefeatWarningText != null)
                {
                    DefeatWarningText.text = risks[0].WarningMessage;
                }
            }
            else
            {
                DefeatWarningPanel.SetActive(false);
            }
        }
        
        private VictoryDefeatSystem FindVictorySystem()
        {
            var go = GetComponent<VictoryDefeatSystem>();
            return go;
        }
    }
    
    public class VictoryPathBar : MonoBehaviour
    {
        public Text PathNameText;
        public Image ProgressFill;
        public Text ProgressText;
        
        public void Setup(string name, float progress)
        {
            if (PathNameText != null)
                PathNameText.text = name;
            UpdateProgress(progress);
        }
        
        public void UpdateProgress(float progress)
        {
            if (ProgressFill != null)
                ProgressFill.fillAmount = Mathf.Clamp01(progress);
            if (ProgressText != null)
                ProgressText.text = $"{progress * 100:F0}%";
        }
    }
}