using System;
using System.Collections.Generic;
using UnityEngine;
using EventideAge.Core;

namespace EventideAge.Systems.I1
{
    [Serializable]
    public class TutorialStepTemplate
    {
        public string StepId;
        public string EventName;
        [TextArea(2, 5)] public string Narrative;
        public EventTrigger Trigger = EventTrigger.TurnBased;
        public int TriggerTurn = 1;
        public string TriggerCondition;
        public int TimeoutTurns = 8;
        public bool Enabled = true;
        public string[] Effects = Array.Empty<string>();

        public GameEvent ToGameEvent(int fallbackTimeout)
        {
            string normalizedStepId = NormalizeStepId(StepId, EventName);
            var normalizedEffects = new List<string>();
            if (Effects != null)
            {
                for (int i = 0; i < Effects.Length; i++)
                {
                    if (string.IsNullOrWhiteSpace(Effects[i]))
                        continue;
                    normalizedEffects.Add(Effects[i].Trim());
                }
            }

            return new GameEvent
            {
                TemplateId = normalizedStepId,
                CanonicalKey = $"tutorial:{normalizedStepId.ToLowerInvariant()}",
                EventName = string.IsNullOrWhiteSpace(EventName) ? normalizedStepId : EventName.Trim(),
                Narrative = Narrative,
                Type = EventType.PlayerResponse,
                Trigger = Trigger,
                TriggerTurn = TriggerTurn,
                TriggerCondition = TriggerCondition,
                Probability = 1f,
                TimeoutTurns = TimeoutTurns > 0 ? TimeoutTurns : Mathf.Max(1, fallbackTimeout),
                Effects = normalizedEffects,
                AllowRepeat = false
            };
        }

        private static string NormalizeStepId(string stepId, string eventName)
        {
            string raw = string.IsNullOrWhiteSpace(stepId) ? eventName : stepId;
            if (string.IsNullOrWhiteSpace(raw))
                return "tutorial_step";

            var chars = new char[raw.Length];
            int index = 0;
            for (int i = 0; i < raw.Length; i++)
            {
                char c = raw[i];
                if (char.IsLetterOrDigit(c))
                {
                    chars[index++] = char.ToLowerInvariant(c);
                }
                else if (c == ' ' || c == '-' || c == '_')
                {
                    chars[index++] = '_';
                }
            }

            if (index == 0)
                return "tutorial_step";

            return new string(chars, 0, index);
        }
    }

    [CreateAssetMenu(fileName = "TutorialFlowConfig", menuName = "EventideAge/TutorialFlowConfig")]
    public class TutorialFlowConfig : ScriptableObject
    {
        [SerializeField] private TutorialStepTemplate[] _steps = Array.Empty<TutorialStepTemplate>();

        public TutorialStepTemplate[] Steps
        {
            get { return _steps ?? Array.Empty<TutorialStepTemplate>(); }
        }

        public void SetSteps(TutorialStepTemplate[] steps)
        {
            _steps = steps ?? Array.Empty<TutorialStepTemplate>();
        }

        public static TutorialStepTemplate[] CreateDefaultSteps()
        {
            return new[]
            {
                new TutorialStepTemplate
                {
                    StepId = "tutorial_turn1_map",
                    EventName = "Tutorial 1/5 - Map Recon",
                    Narrative = "Open the map panel and review node controllers around SyriaZone and IraqBorder.",
                    Trigger = EventTrigger.TurnBased,
                    TriggerTurn = 1
                },
                new TutorialStepTemplate
                {
                    StepId = "tutorial_turn2_diplomacy",
                    EventName = "Tutorial 2/5 - Diplomacy Read",
                    Narrative = "Check diplomacy panel deltas before committing AP-heavy military actions.",
                    Trigger = EventTrigger.ConditionBased,
                    TriggerCondition = "turn>=2&&resource:GoldLeaf>=0"
                },
                new TutorialStepTemplate
                {
                    StepId = "tutorial_turn3_battle_report",
                    EventName = "Tutorial 3/5 - Battle Report",
                    Narrative = "Review battle/action logs and confirm control-point changes after each operation.",
                    Trigger = EventTrigger.TurnBased,
                    TriggerTurn = 3
                },
                new TutorialStepTemplate
                {
                    StepId = "tutorial_turn4_event_panel",
                    EventName = "Tutorial 4/5 - Event Reading",
                    Narrative = "Use the event panel to separate story events from system alerts and spot critical entries first.",
                    Trigger = EventTrigger.ConditionBased,
                    TriggerCondition = $"turn>=4&&node_control:{GameIds.Node.SyriaZone}={GameIds.Faction.Aurean}"
                },
                new TutorialStepTemplate
                {
                    StepId = "tutorial_turn5_loop_sync",
                    EventName = "Tutorial 5/5 - Turn Loop Sync",
                    Narrative = "Before ending turn: validate AP, resources, diplomacy, and event feedback for a complete decision loop.",
                    Trigger = EventTrigger.ConditionBased,
                    TriggerCondition = $"turn>=5&&relation:{GameIds.Faction.Aurean}<=0"
                }
            };
        }
    }
}
