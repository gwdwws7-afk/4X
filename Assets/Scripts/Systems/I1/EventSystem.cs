using UnityEngine;
using System.Collections.Generic;
using EventideAge.Core;

namespace EventideAge.Systems.I1
{
    public enum EventType
    {
        Narrative,
        PlayerResponse,
        Random,
        Endgame
    }
    
    public enum EventTrigger
    {
        TurnBased,
        ActionBased,
        ConditionBased,
        Random
    }
    
    public class GameEvent
    {
        public string EventId;
        public string EventName;
        public EventType Type;
        public EventTrigger Trigger;
        public int TriggerTurn;
        public string TriggerCondition;
        public float Probability;
        public int TimeoutTurns;
        public List<string> Effects;
        public bool IsExpired;
    }
    
    public class EventSystem : GameSystem
    {
        [Header("Event Probabilities")]
        public float RandomEventBaseProbability = 0.15f;
        public float EndgameEventProbability = 0.3f;
        
        [Header("Event Timeout")]
        public int DefaultEventTimeout = 5;
        
        private List<GameEvent> _eventQueue = new List<GameEvent>();
        private List<GameEvent> _triggeredEvents = new List<GameEvent>();
        private int _eventIdCounter = 0;
        
        public override void Initialize(GameState state, GameEvents events)
        {
            base.Initialize(state, events);
            
            Events.OnTurnEnded += HandleTurnEnded;
            
            Debug.Log("[EventSystem] Initialized");
        }
        
        private void OnDestroy()
        {
            Events.OnTurnEnded -= HandleTurnEnded;
        }
        
        private void HandleTurnEnded(int turnNumber)
        {
            ProcessEventQueue();
            CheckTriggeredEvents(turnNumber);
            DecayEventProbabilities();
        }
        
        public void QueueEvent(GameEvent gameEvent)
        {
            gameEvent.EventId = $"event_{_eventIdCounter++}";
            _eventQueue.Add(gameEvent);
            
            Debug.Log($"[EventSystem] Event queued: {gameEvent.EventName} ({gameEvent.Type})");
        }
        
        public void TriggerEvent(string eventId)
        {
            var evt = _eventQueue.Find(e => e.EventId == eventId);
            if (evt == null) return;
            
            ApplyEventEffects(evt);
            _triggeredEvents.Add(evt);
            _eventQueue.Remove(evt);
            
            Debug.Log($"[EventSystem] Event triggered: {evt.EventName}");
        }
        
        private void ProcessEventQueue()
        {
            for (int i = _eventQueue.Count - 1; i >= 0; i--)
            {
                var evt = _eventQueue[i];
                
                if (evt.Type == EventType.Random && evt.Trigger == EventTrigger.Random)
                {
                    if (Random.value < evt.Probability)
                    {
                        TriggerEvent(evt.EventId);
                    }
                }
                else if (evt.Type == EventType.Endgame)
                {
                    if (ShouldTriggerEndgameEvent())
                    {
                        TriggerEvent(evt.EventId);
                    }
                }
                
                evt.TimeoutTurns--;
                if (evt.TimeoutTurns <= 0)
                {
                    _eventQueue.RemoveAt(i);
                    Debug.Log($"[EventSystem] Event expired: {evt.EventName}");
                }
            }
        }
        
        private void CheckTriggeredEvents(int turnNumber)
        {
            foreach (var evt in _triggeredEvents)
            {
                evt.TimeoutTurns--;
                if (evt.TimeoutTurns <= 0)
                {
                    evt.IsExpired = true;
                }
            }
            
            _triggeredEvents.RemoveAll(e => e.IsExpired);
        }
        
        private void DecayEventProbabilities()
        {
            foreach (var evt in _eventQueue)
            {
                evt.Probability *= 0.98f;
            }
        }
        
        private bool ShouldTriggerEndgameEvent()
        {
            if (State.CurrentTurn >= 20)
            {
                return Random.value < EndgameEventProbability;
            }
            return false;
        }
        
        private void ApplyEventEffects(GameEvent evt)
        {
            foreach (var effect in evt.Effects)
            {
                ApplyEffect(effect);
            }
        }
        
        private void ApplyEffect(string effect)
        {
            if (effect.StartsWith("relation_change:"))
            {
                var parts = effect.Split(':');
                if (parts.Length >= 3)
                {
                    string factionId = parts[1];
                    int delta = int.Parse(parts[2]);
                    Events.RelationshipChanged(factionId, delta);
                }
            }
            else if (effect.StartsWith("resource_change:"))
            {
                var parts = effect.Split(':');
                if (parts.Length >= 3)
                {
                    string resourceId = parts[1];
                    int delta = int.Parse(parts[2]);
                    var resource = State.GetResource(resourceId);
                    if (resource != null)
                    {
                        resource.Amount = Mathf.Clamp(resource.Amount + delta, 0, resource.MaxCapacity);
                        Events.ResourceChanged(resourceId, resource.Amount - delta, resource.Amount);
                    }
                }
            }
            else if (effect == "end_game")
            {
                Events.GameEnded("endgame_event");
            }
        }
        
        public GameEvent CreateRandomEvent(string name, float probability, string[] effects)
        {
            return new GameEvent
            {
                EventName = name,
                Type = EventType.Random,
                Trigger = EventTrigger.Random,
                Probability = probability,
                Effects = new List<string>(effects),
                TimeoutTurns = DefaultEventTimeout
            };
        }
        
        public GameEvent CreateTurnBasedEvent(string name, int turn, string[] effects)
        {
            return new GameEvent
            {
                EventName = name,
                Type = EventType.Narrative,
                Trigger = EventTrigger.TurnBased,
                TriggerTurn = turn,
                Probability = 1.0f,
                Effects = new List<string>(effects),
                TimeoutTurns = DefaultEventTimeout
            };
        }
        
        public GameEvent CreateResponseEvent(string name, string condition, string[] effects)
        {
            return new GameEvent
            {
                EventName = name,
                Type = EventType.PlayerResponse,
                Trigger = EventTrigger.ConditionBased,
                TriggerCondition = condition,
                Probability = 1.0f,
                Effects = new List<string>(effects),
                TimeoutTurns = DefaultEventTimeout
            };
        }
        
        public GameEvent[] GetQueuedEvents()
        {
            return _eventQueue.ToArray();
        }
        
        public GameEvent[] GetTriggeredEvents()
        {
            return _triggeredEvents.ToArray();
        }
    }
}
