using UnityEngine;
using UnityEngine.UI;
using EventideAge.Core;

namespace EventideAge.UI
{
    public class PhaseIndicatorUI : MonoBehaviour
    {
        [Header("Phase Buttons")]
        public Button[] PhaseButtons;
        public Text[] PhaseLabels;
        
        [Header("Colors")]
        public Color ActivePhaseColor = new Color(1f, 0.8f, 0.2f);
        public Color InactivePhaseColor = new Color(0.4f, 0.4f, 0.4f);
        public Color CompletedPhaseColor = new Color(0.3f, 0.6f, 0.3f);
        
        private int _currentPhaseIndex;
        
        public void Initialize(int totalPhases)
        {
            _currentPhaseIndex = 0;
            UpdatePhaseDisplay();
        }
        
        public void SetCurrentPhase(int phaseIndex)
        {
            _currentPhaseIndex = phaseIndex;
            UpdatePhaseDisplay();
        }
        
        private void UpdatePhaseDisplay()
        {
            if (PhaseButtons == null) return;
            
            for (int i = 0; i < PhaseButtons.Length; i++)
            {
                if (PhaseButtons[i] == null) continue;
                
                var colors = PhaseButtons[i].colors;
                
                if (i < _currentPhaseIndex)
                {
                    colors.normalColor = CompletedPhaseColor;
                    colors.highlightedColor = CompletedPhaseColor;
                }
                else if (i == _currentPhaseIndex)
                {
                    colors.normalColor = ActivePhaseColor;
                    colors.highlightedColor = ActivePhaseColor;
                }
                else
                {
                    colors.normalColor = InactivePhaseColor;
                    colors.highlightedColor = InactivePhaseColor;
                }
                
                PhaseButtons[i].colors = colors;
            }
            
            if (PhaseLabels != null)
            {
                for (int i = 0; i < PhaseLabels.Length && i < PhaseButtons.Length; i++)
                {
                    if (PhaseLabels[i] != null && PhaseButtons[i] != null)
                    {
                        PhaseLabels[i].color = PhaseButtons[i].colors.normalColor;
                    }
                }
            }
        }
    }
}
