using UnityEngine;
using EventideAge.Core;

namespace EventideAge.Systems.A3
{
    public class ResourceSystem : GameSystem
    {
        public override void Initialize(GameState state, GameEvents events)
        {
            base.Initialize(state, events);
            Events.OnResourceChanged += HandleResourceChanged;
        }
        
        private void OnDestroy()
        {
            Events.OnResourceChanged -= HandleResourceChanged;
        }
        
        public bool ModifyResource(string resourceId, int delta)
        {
            var resource = State.GetResource(resourceId);
            if (resource == null)
            {
                Debug.LogWarning($"[ResourceSystem] Resource not found: {resourceId}");
                return false;
            }
            
            int oldAmount = resource.Amount;
            resource.Amount = Mathf.Clamp(resource.Amount + delta, 0, resource.MaxCapacity);
            
            if (oldAmount != resource.Amount)
            {
                Events.ResourceChanged(resourceId, oldAmount, resource.Amount);
                Debug.Log($"[ResourceSystem] {resource.ResourceName}: {oldAmount} → {resource.Amount} ({delta:+#;-#;0})");
            }
            
            return true;
        }
        
        public bool CanAfford(string resourceId, int cost)
        {
            var resource = State.GetResource(resourceId);
            return resource != null && resource.Amount >= cost;
        }
        
        public bool SpendResource(string resourceId, int cost)
        {
            if (!CanAfford(resourceId, cost))
            {
                Debug.LogWarning($"[ResourceSystem] Cannot afford {resourceId}: need {cost}, have {State.GetResource(resourceId)?.Amount ?? 0}");
                return false;
            }
            return ModifyResource(resourceId, -cost);
        }
        
        public int GetResourceAmount(string resourceId)
        {
            return State.GetResource(resourceId)?.Amount ?? 0;
        }
        
        public float GetResourcePercentage(string resourceId)
        {
            var resource = State.GetResource(resourceId);
            if (resource == null || resource.MaxCapacity == 0) return 0f;
            return (float)resource.Amount / resource.MaxCapacity;
        }
        
        private void HandleResourceChanged(string resourceId, int oldAmount, int newAmount)
        {
            // Can trigger UI updates or other systems here
        }
        
        public override void OnPhaseEntered(int phaseIndex)
        {
            if (phaseIndex == 3) // Logistics phase - resource regeneration
            {
                RegenerateResources();
            }
        }
        
        private void RegenerateResources()
        {
            foreach (var resource in State.Resources)
            {
                if (resource.ResourceType == ResourceType.Accumulative)
                {
                    int regen = Mathf.CeilToInt(resource.MaxCapacity * 0.1f);
                    ModifyResource(resource.ResourceId, regen);
                }
            }
        }
    }
}
