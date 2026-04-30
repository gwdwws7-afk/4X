using UnityEngine;
using System;
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
    
    [Serializable]
    public class GameEvent
    {
        public string EventId;
        public string TemplateId;
        public string CanonicalKey;
        public string EventName;
        public string Narrative;
        public EventType Type;
        public EventTrigger Trigger;
        public int TriggerTurn;
        public string TriggerCondition;
        public float Probability;
        public int TimeoutTurns;
        public List<string> Effects;
        public bool IsExpired;
        public bool AllowRepeat;
    }
    
    public class EventSystem : GameSystem
    {
        [Header("Event Pool")]
        public EventPoolConfig EventPoolConfigAsset;
        public bool AutoLoadEventPoolOnInitialize = true;

        [Header("Tutorial Flow")]
        public TutorialFlowConfig TutorialFlowConfigAsset;
        public bool AutoLoadTutorialOnInitialize = true;

        [Header("Event Probabilities")]
        public float RandomEventBaseProbability = 0.15f;
        public float EndgameEventProbability = 0.3f;
        
        [Header("Event Timeout")]
        public int DefaultEventTimeout = 5;
        
        private List<GameEvent> _eventQueue = new List<GameEvent>();
        private List<GameEvent> _triggeredEvents = new List<GameEvent>();
        private readonly HashSet<string> _queuedEventDedupKeys = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        private readonly HashSet<string> _triggeredEventDedupKeys = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        private int _eventIdCounter = 0;
        private int _lastPoolLoadDuplicateCount = 0;
        private int _lastPoolLoadEnqueuedCount = 0;
        private int _lastTutorialLoadDuplicateCount = 0;
        private int _lastTutorialLoadEnqueuedCount = 0;
        
        public override void Initialize(GameState state, GameEvents events)
        {
            base.Initialize(state, events);
            
            Events.OnTurnEnded += HandleTurnEnded;

            if (AutoLoadEventPoolOnInitialize && EventPoolConfigAsset != null)
            {
                LoadEventPoolFromConfig(EventPoolConfigAsset, clearQueue: false, resetTriggeredHistory: false);
            }

            if (AutoLoadTutorialOnInitialize && TutorialFlowConfigAsset != null)
            {
                LoadTutorialFlowFromConfig(TutorialFlowConfigAsset, clearQueue: false, resetTriggeredHistory: false);
            }
            
            Debug.Log("[EventSystem] Initialized");
        }
        
        private void OnDestroy()
        {
            if (Events != null)
            {
                Events.OnTurnEnded -= HandleTurnEnded;
            }
        }
        
        private void HandleTurnEnded(int turnNumber)
        {
            ProcessEventQueue();
            CheckTriggeredEvents(turnNumber);
            DecayEventProbabilities();
        }

        public bool QueueEvent(GameEvent gameEvent)
        {
            if (gameEvent == null)
                return false;

            NormalizeEvent(gameEvent);
            string dedupKey = EnsureEventCanonicalKey(gameEvent);

            bool duplicatesTriggeredHistory = !gameEvent.AllowRepeat && _triggeredEventDedupKeys.Contains(dedupKey);
            if (_queuedEventDedupKeys.Contains(dedupKey) || duplicatesTriggeredHistory)
            {
                Debug.LogWarning($"[EventSystem] Duplicate event skipped: {gameEvent.EventName} (key={dedupKey})");
                return false;
            }

            if (string.IsNullOrWhiteSpace(gameEvent.EventId))
            {
                gameEvent.EventId = $"event_{_eventIdCounter++}";
            }

            if (gameEvent.TimeoutTurns <= 0)
                gameEvent.TimeoutTurns = DefaultEventTimeout;

            if (gameEvent.Trigger == EventTrigger.Random && gameEvent.Probability <= 0f)
                gameEvent.Probability = RandomEventBaseProbability;

            _eventQueue.Add(gameEvent);
            _queuedEventDedupKeys.Add(dedupKey);
            
            Debug.Log($"[EventSystem] Event queued: {gameEvent.EventName} ({gameEvent.Type})");
            return true;
        }
        
        public void TriggerEvent(string eventId)
        {
            var evt = _eventQueue.Find(e => e.EventId == eventId);
            if (evt == null) return;

            string dedupKey = EnsureEventCanonicalKey(evt);
            
            ApplyEventEffects(evt);
            _triggeredEvents.Add(evt);
            _eventQueue.Remove(evt);
            _queuedEventDedupKeys.Remove(dedupKey);
            if (!evt.AllowRepeat)
            {
                _triggeredEventDedupKeys.Add(dedupKey);
            }

            FeedbackSeverity severity = evt.Type == EventType.Endgame
                ? FeedbackSeverity.Critical
                : FeedbackSeverity.Info;
            Events.NarrativeEventAdded(evt.EventId, $"{evt.EventName} ({evt.Type})", severity);
            Events.ActionLogAdded("I1", $"Event triggered: {evt.EventName}", severity);
            
            Debug.Log($"[EventSystem] Event triggered: {evt.EventName}");
        }
        
        private void ProcessEventQueue()
        {
            for (int i = _eventQueue.Count - 1; i >= 0; i--)
            {
                var evt = _eventQueue[i];

                if (ShouldTriggerEvent(evt))
                {
                    TriggerEvent(evt.EventId);
                    continue;
                }

                evt.TimeoutTurns--;
                if (evt.TimeoutTurns <= 0)
                {
                    _queuedEventDedupKeys.Remove(EnsureEventCanonicalKey(evt));
                    _eventQueue.RemoveAt(i);
                    Debug.Log($"[EventSystem] Event expired: {evt.EventName}");
                }
            }
        }

        private bool ShouldTriggerEvent(GameEvent evt)
        {
            if (evt == null)
                return false;

            if (evt.Type == EventType.Endgame && ShouldTriggerEndgameEvent())
                return true;

            switch (evt.Trigger)
            {
                case EventTrigger.Random:
                    float probability = evt.Probability > 0f ? evt.Probability : RandomEventBaseProbability;
                    return UnityEngine.Random.value < Mathf.Clamp01(probability);
                case EventTrigger.TurnBased:
                    return evt.TriggerTurn > 0 && State.CurrentTurn >= evt.TriggerTurn;
                case EventTrigger.ConditionBased:
                case EventTrigger.ActionBased:
                    return EvaluateTriggerCondition(evt.TriggerCondition);
                default:
                    return false;
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
                if (evt.Trigger == EventTrigger.Random || evt.Type == EventType.Random)
                {
                    evt.Probability *= 0.98f;
                }
            }
        }
        
        private bool ShouldTriggerEndgameEvent()
        {
            if (State.CurrentTurn >= 20)
            {
                return UnityEngine.Random.value < EndgameEventProbability;
            }
            return false;
        }
        
        private void ApplyEventEffects(GameEvent evt)
        {
            if (evt.Effects == null)
                return;

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
                    if (int.TryParse(parts[2], out int delta))
                    {
                        Events.RelationshipChanged(factionId, delta);
                    }
                }
            }
            else if (effect.StartsWith("resource_change:"))
            {
                var parts = effect.Split(':');
                if (parts.Length >= 3)
                {
                    string resourceId = parts[1];
                    if (!int.TryParse(parts[2], out int delta))
                        return;

                    var resource = State.GetResource(resourceId);
                    if (resource != null)
                    {
                        int oldAmount = resource.Amount;
                        resource.Amount = Mathf.Clamp(resource.Amount + delta, 0, resource.MaxCapacity);
                        Events.ResourceChanged(resourceId, oldAmount, resource.Amount);
                    }
                }
            }
            else if (effect == "end_game")
            {
                Events.GameEnded("endgame_event");
            }
        }

        private bool EvaluateTriggerCondition(string condition)
        {
            if (string.IsNullOrWhiteSpace(condition))
                return false;

            string[] clauses = condition.Split(new[] { "&&" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var rawClause in clauses)
            {
                if (!EvaluateConditionClause(rawClause.Trim()))
                    return false;
            }

            return true;
        }

        private bool EvaluateConditionClause(string clause)
        {
            if (string.IsNullOrWhiteSpace(clause))
                return true;

            if (TryEvaluateNumericClause(clause, "turn", State.CurrentTurn, out bool turnResult))
                return turnResult;

            if (TryEvaluateNumericClause(clause, "phase", State.CurrentPhaseIndex, out bool phaseResult))
                return phaseResult;

            if (TryEvaluateResourceClause(clause, out bool resourceResult))
                return resourceResult;

            if (TryEvaluateRelationClause(clause, out bool relationResult))
                return relationResult;

            if (TryEvaluateNodeControlClause(clause, out bool nodeControlResult))
                return nodeControlResult;

            return false;
        }

        private bool TryEvaluateNumericClause(string clause, string key, int leftValue, out bool result)
        {
            result = false;
            if (!TryParseComparison(clause, out string leftToken, out string comparator, out string rightToken))
                return false;

            if (!string.Equals(leftToken, key, StringComparison.OrdinalIgnoreCase))
                return false;

            if (!int.TryParse(rightToken, out int rightValue))
                return true;

            result = CompareIntegers(leftValue, comparator, rightValue);
            return true;
        }

        private bool TryEvaluateResourceClause(string clause, out bool result)
        {
            result = false;
            if (!TryParseComparison(clause, out string leftToken, out string comparator, out string rightToken))
                return false;

            string resourceId = null;
            if (leftToken.StartsWith("resource:", StringComparison.OrdinalIgnoreCase))
            {
                resourceId = leftToken.Substring("resource:".Length);
            }
            else
            {
                var resourceFallback = State.GetResource(leftToken);
                if (resourceFallback != null)
                    resourceId = leftToken;
            }

            if (string.IsNullOrEmpty(resourceId))
                return false;

            if (!int.TryParse(rightToken, out int expectedValue))
                return true;

            var resource = State.GetResource(resourceId);
            int currentValue = resource?.Amount ?? 0;
            result = CompareIntegers(currentValue, comparator, expectedValue);
            return true;
        }

        private bool TryEvaluateRelationClause(string clause, out bool result)
        {
            result = false;
            if (!TryParseComparison(clause, out string leftToken, out string comparator, out string rightToken))
                return false;

            if (!leftToken.StartsWith("relation:", StringComparison.OrdinalIgnoreCase))
                return false;

            if (!int.TryParse(rightToken, out int expectedValue))
                return true;

            string factionId = leftToken.Substring("relation:".Length);
            var faction = State.GetFaction(factionId);
            int currentRelation = faction?.RelationshipWithPlayer ?? 0;
            result = CompareIntegers(currentRelation, comparator, expectedValue);
            return true;
        }

        private bool TryEvaluateNodeControlClause(string clause, out bool result)
        {
            result = false;
            if (!TryParseComparison(clause, out string leftToken, out string comparator, out string rightToken))
                return false;

            if (!leftToken.StartsWith("node_control:", StringComparison.OrdinalIgnoreCase))
                return false;

            if (comparator != "=" && comparator != "==" && comparator != "!=")
                return true;

            string nodeId = leftToken.Substring("node_control:".Length);
            var node = State.GetNode(nodeId);
            string controller = node?.ControllingFactionId ?? string.Empty;
            string expectedController = GameIds.ResolveFactionId(rightToken);

            bool equals = string.Equals(controller, expectedController, StringComparison.OrdinalIgnoreCase);
            result = comparator == "!=" ? !equals : equals;
            return true;
        }

        private bool TryParseComparison(string clause, out string leftToken, out string comparator, out string rightToken)
        {
            leftToken = null;
            comparator = null;
            rightToken = null;

            string[] comparators = { ">=", "<=", "==", "!=", ">", "<", "=" };
            foreach (var op in comparators)
            {
                int index = clause.IndexOf(op, StringComparison.Ordinal);
                if (index <= 0)
                    continue;

                leftToken = clause.Substring(0, index).Trim();
                comparator = op;
                rightToken = clause.Substring(index + op.Length).Trim();
                return !string.IsNullOrEmpty(leftToken) && !string.IsNullOrEmpty(rightToken);
            }

            return false;
        }

        private bool CompareIntegers(int leftValue, string comparator, int rightValue)
        {
            switch (comparator)
            {
                case ">=": return leftValue >= rightValue;
                case "<=": return leftValue <= rightValue;
                case ">": return leftValue > rightValue;
                case "<": return leftValue < rightValue;
                case "=":
                case "==": return leftValue == rightValue;
                case "!=": return leftValue != rightValue;
                default: return false;
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
                Effects = effects != null ? new List<string>(effects) : new List<string>(),
                TimeoutTurns = DefaultEventTimeout,
                AllowRepeat = true
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
                Effects = effects != null ? new List<string>(effects) : new List<string>(),
                TimeoutTurns = DefaultEventTimeout,
                AllowRepeat = false
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
                Effects = effects != null ? new List<string>(effects) : new List<string>(),
                TimeoutTurns = DefaultEventTimeout,
                AllowRepeat = false
            };
        }

        public int LoadEventPoolFromConfig(bool clearQueue = true, bool resetTriggeredHistory = false)
        {
            return LoadEventPoolFromConfig(EventPoolConfigAsset, clearQueue, resetTriggeredHistory);
        }

        public int LoadEventPoolFromConfig(EventPoolConfig poolConfig, bool clearQueue = true, bool resetTriggeredHistory = false)
        {
            _lastPoolLoadDuplicateCount = 0;
            _lastPoolLoadEnqueuedCount = 0;

            if (clearQueue)
            {
                _eventQueue.Clear();
                _queuedEventDedupKeys.Clear();
            }

            if (resetTriggeredHistory)
            {
                _triggeredEvents.Clear();
                _triggeredEventDedupKeys.Clear();
            }

            if (poolConfig == null || poolConfig.Templates.Length == 0)
            {
                Debug.LogWarning("[EventSystem] Event pool load skipped: no config or templates.");
                return 0;
            }

            foreach (var template in poolConfig.Templates)
            {
                if (template == null || !template.Enabled)
                    continue;

                bool queued = QueueEvent(template.ToGameEvent(DefaultEventTimeout, RandomEventBaseProbability));
                if (queued)
                {
                    _lastPoolLoadEnqueuedCount++;
                }
                else
                {
                    _lastPoolLoadDuplicateCount++;
                }
            }

            Debug.Log($"[EventSystem] Event pool loaded: enqueued={_lastPoolLoadEnqueuedCount}, duplicatesSkipped={_lastPoolLoadDuplicateCount}");
            return _lastPoolLoadEnqueuedCount;
        }

        public int GetLastPoolLoadDuplicateCount()
        {
            return _lastPoolLoadDuplicateCount;
        }

        public int GetLastPoolLoadEnqueuedCount()
        {
            return _lastPoolLoadEnqueuedCount;
        }

        public int LoadTutorialFlowFromConfig(bool clearQueue = false, bool resetTriggeredHistory = false)
        {
            return LoadTutorialFlowFromConfig(TutorialFlowConfigAsset, clearQueue, resetTriggeredHistory);
        }

        public int LoadTutorialFlowFromConfig(TutorialFlowConfig tutorialConfig, bool clearQueue = false, bool resetTriggeredHistory = false)
        {
            _lastTutorialLoadDuplicateCount = 0;
            _lastTutorialLoadEnqueuedCount = 0;

            if (clearQueue)
            {
                _eventQueue.Clear();
                _queuedEventDedupKeys.Clear();
            }

            if (resetTriggeredHistory)
            {
                _triggeredEvents.Clear();
                _triggeredEventDedupKeys.Clear();
            }

            if (tutorialConfig == null || tutorialConfig.Steps.Length == 0)
            {
                Debug.LogWarning("[EventSystem] Tutorial flow load skipped: no config or steps.");
                return 0;
            }

            foreach (var step in tutorialConfig.Steps)
            {
                if (step == null || !step.Enabled)
                    continue;

                bool queued = QueueEvent(step.ToGameEvent(DefaultEventTimeout));
                if (queued)
                {
                    _lastTutorialLoadEnqueuedCount++;
                }
                else
                {
                    _lastTutorialLoadDuplicateCount++;
                }
            }

            Debug.Log($"[EventSystem] Tutorial flow loaded: enqueued={_lastTutorialLoadEnqueuedCount}, duplicatesSkipped={_lastTutorialLoadDuplicateCount}");
            return _lastTutorialLoadEnqueuedCount;
        }

        public int GetLastTutorialLoadDuplicateCount()
        {
            return _lastTutorialLoadDuplicateCount;
        }

        public int GetLastTutorialLoadEnqueuedCount()
        {
            return _lastTutorialLoadEnqueuedCount;
        }
        
        public GameEvent[] GetQueuedEvents()
        {
            return _eventQueue.ToArray();
        }
        
        public GameEvent[] GetTriggeredEvents()
        {
            return _triggeredEvents.ToArray();
        }

        private void NormalizeEvent(GameEvent gameEvent)
        {
            if (gameEvent.Effects == null)
                gameEvent.Effects = new List<string>();

            gameEvent.EventName = string.IsNullOrWhiteSpace(gameEvent.EventName)
                ? "UnnamedEvent"
                : gameEvent.EventName.Trim();
        }

        private string EnsureEventCanonicalKey(GameEvent gameEvent)
        {
            if (gameEvent == null)
                return string.Empty;

            if (!string.IsNullOrWhiteSpace(gameEvent.CanonicalKey))
                return gameEvent.CanonicalKey;

            if (!string.IsNullOrWhiteSpace(gameEvent.TemplateId))
            {
                gameEvent.CanonicalKey = $"template:{gameEvent.TemplateId.Trim().ToLowerInvariant()}";
                return gameEvent.CanonicalKey;
            }

            string effects = gameEvent.Effects != null ? string.Join(";", gameEvent.Effects) : string.Empty;
            gameEvent.CanonicalKey =
                $"{gameEvent.EventName}|{gameEvent.Type}|{gameEvent.Trigger}|{gameEvent.TriggerTurn}|{gameEvent.TriggerCondition}|{effects}"
                .Trim()
                .ToLowerInvariant();
            return gameEvent.CanonicalKey;
        }
    }
}
