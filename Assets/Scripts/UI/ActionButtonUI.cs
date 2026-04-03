using UnityEngine;
using UnityEngine.UI;
using EventideAge.Core;

namespace EventideAge.UI
{
    public class ActionButtonUI : MonoBehaviour
    {
        public Button Button;
        public Text ActionNameText;
        public Text CostText;
        public Image Icon;
        
        private GameAction _action;
        private Systems.A1.TurnLoopSystem _turnLoop;
        
        public void Initialize(GameAction action, Systems.A1.TurnLoopSystem turnLoop)
        {
            _action = action;
            _turnLoop = turnLoop;
            
            if (ActionNameText != null)
                ActionNameText.text = action.ActionName;
            
            if (CostText != null)
                CostText.text = $"AP: {action.Cost}";
            
            if (Button != null)
            {
                Button.onClick.RemoveAllListeners();
                Button.onClick.AddListener(OnButtonClicked);
            }
            
            UpdateButtonState();
        }
        
        private void OnButtonClicked()
        {
            if (_turnLoop != null && _action != null)
            {
                _turnLoop.ExecuteAction(_action);
                UpdateButtonState();
            }
        }
        
        public void UpdateButtonState()
        {
            if (Button == null || _turnLoop == null || _action == null) return;
            
            bool canExecute = _turnLoop.CanExecuteAction(_action);
            Button.interactable = canExecute;
            
            if (!canExecute)
            {
                Button.image.color = new Color(0.5f, 0.5f, 0.5f);
            }
            else
            {
                Button.image.color = Color.white;
            }
        }
    }
}
