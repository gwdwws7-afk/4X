using UnityEngine;
using UnityEngine.UI;
using EventideAge.Core;

namespace EventideAge.UI
{
    public class ResourceItemUI : MonoBehaviour
    {
        [Header("UI Elements")]
        public Image Icon;
        public Text AmountText;
        public Text NameText;
        public Slider FillSlider;
        
        [Header("Colors")]
        public Color LowColor = Color.red;
        public Color MediumColor = Color.yellow;
        public Color HighColor = Color.green;
        
        private ResourceState _resource;
        
        public string ResourceId { get; private set; }
        
        public void Initialize(ResourceState resource)
        {
            _resource = resource;
            ResourceId = resource.ResourceId;
            
            if (NameText != null)
                NameText.text = resource.ResourceName;
            
            UpdateDisplay(resource.Amount, resource.MaxCapacity);
        }
        
        public void UpdateDisplay(int amount, int maxCapacity)
        {
            if (AmountText != null)
                AmountText.text = $"{amount}";
            
            if (FillSlider != null)
            {
                FillSlider.maxValue = maxCapacity;
                FillSlider.value = amount;
                
                float ratio = maxCapacity > 0 ? (float)amount / maxCapacity : 0;
                Color fillColor;
                if (ratio < 0.3f)
                    fillColor = LowColor;
                else if (ratio < 0.6f)
                    fillColor = MediumColor;
                else
                    fillColor = HighColor;
                
                if (FillSlider.fillRect != null)
                    FillSlider.fillRect.GetComponent<Image>().color = fillColor;
            }
        }
    }
}
