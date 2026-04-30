using System;
using System.Collections.Generic;
using UnityEngine;
using EventideAge.Core;

namespace EventideAge.Systems.I1
{
    [Serializable]
    public class EventTemplate
    {
        public string TemplateId;
        public string EventName;
        [TextArea(2, 5)] public string Narrative;
        public EventType Type = EventType.Narrative;
        public EventTrigger Trigger = EventTrigger.TurnBased;
        public int TriggerTurn = 1;
        public string TriggerCondition;
        [Range(0f, 1f)] public float Probability = 1f;
        public int TimeoutTurns = 5;
        public bool Enabled = true;
        public bool AllowRepeat = false;
        public string[] Effects = Array.Empty<string>();

        public GameEvent ToGameEvent(int fallbackTimeout, float fallbackRandomProbability)
        {
            string normalizedTemplateId = string.IsNullOrWhiteSpace(TemplateId)
                ? BuildFallbackTemplateId()
                : TemplateId.Trim();

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

            float probability = Probability;
            if (Trigger == EventTrigger.Random && probability <= 0f)
                probability = fallbackRandomProbability;

            int timeout = TimeoutTurns > 0 ? TimeoutTurns : Mathf.Max(1, fallbackTimeout);

            return new GameEvent
            {
                TemplateId = normalizedTemplateId,
                CanonicalKey = $"template:{normalizedTemplateId.ToLowerInvariant()}",
                EventName = string.IsNullOrWhiteSpace(EventName) ? "UnnamedEvent" : EventName.Trim(),
                Narrative = Narrative,
                Type = Type,
                Trigger = Trigger,
                TriggerTurn = TriggerTurn,
                TriggerCondition = TriggerCondition,
                Probability = probability,
                TimeoutTurns = timeout,
                Effects = normalizedEffects,
                AllowRepeat = AllowRepeat
            };
        }

        private string BuildFallbackTemplateId()
        {
            string nameToken = NormalizeIdToken(EventName);
            if (string.IsNullOrEmpty(nameToken))
                nameToken = "event";

            return $"{nameToken}_{Type.ToString().ToLowerInvariant()}_{Trigger.ToString().ToLowerInvariant()}";
        }

        private static string NormalizeIdToken(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return string.Empty;

            var chars = new char[value.Length];
            int index = 0;
            for (int i = 0; i < value.Length; i++)
            {
                char c = value[i];
                if (char.IsLetterOrDigit(c))
                {
                    chars[index++] = char.ToLowerInvariant(c);
                }
                else if (c == ' ' || c == '-' || c == '_')
                {
                    chars[index++] = '_';
                }
            }

            return index > 0 ? new string(chars, 0, index) : string.Empty;
        }
    }

    [CreateAssetMenu(fileName = "EventPoolConfig", menuName = "EventideAge/EventPoolConfig")]
    public class EventPoolConfig : ScriptableObject
    {
        [SerializeField] private EventTemplate[] _templates = Array.Empty<EventTemplate>();

        public EventTemplate[] Templates
        {
            get { return _templates ?? Array.Empty<EventTemplate>(); }
        }

        public void SetTemplates(EventTemplate[] templates)
        {
            _templates = templates ?? Array.Empty<EventTemplate>();
        }

        public int CountEnabledTemplates()
        {
            int count = 0;
            var templates = Templates;
            for (int i = 0; i < templates.Length; i++)
            {
                if (templates[i] != null && templates[i].Enabled)
                    count++;
            }

            return count;
        }

        public static EventTemplate[] CreateDefaultTemplates()
        {
            return new[]
            {
                new EventTemplate
                {
                    TemplateId = "evt_hormuz_pressure_t04",
                    EventName = "HormuzPressureEscalation",
                    Narrative = "Pressure rises around the strait and external rhetoric hardens.",
                    Type = EventType.Narrative,
                    Trigger = EventTrigger.TurnBased,
                    TriggerTurn = 4,
                    Effects = new[] { $"relation_change:{GameIds.Faction.Aurean}:-4", $"resource_change:{GameIds.Resource.TributeOrder}:3" }
                },
                new EventTemplate
                {
                    TemplateId = "evt_axis_signal_t06",
                    EventName = "AxisSignalBoost",
                    Narrative = "Regional proxy channels amplify support narratives.",
                    Type = EventType.Narrative,
                    Trigger = EventTrigger.TurnBased,
                    TriggerTurn = 6,
                    Effects = new[] { $"resource_change:{GameIds.Resource.AshWill}:5" }
                },
                new EventTemplate
                {
                    TemplateId = "evt_trade_shock_t08",
                    EventName = "TradeShock",
                    Narrative = "Insurance and shipping costs spike unexpectedly.",
                    Type = EventType.Narrative,
                    Trigger = EventTrigger.TurnBased,
                    TriggerTurn = 8,
                    Effects = new[] { $"resource_change:{GameIds.Resource.GoldLeaf}:-10" }
                },
                new EventTemplate
                {
                    TemplateId = "evt_fireoil_shortage_response",
                    EventName = "FireOilShortageResponse",
                    Narrative = "Domestic pressure rises as energy reserves tighten.",
                    Type = EventType.PlayerResponse,
                    Trigger = EventTrigger.ConditionBased,
                    TriggerCondition = $"resource:{GameIds.Resource.FireOil}<=90&&turn>=2",
                    Effects = new[] { $"resource_change:{GameIds.Resource.SocialValue}:-4" }
                },
                new EventTemplate
                {
                    TemplateId = "evt_syria_control_shift",
                    EventName = "SyriaControlShift",
                    Narrative = "Control developments around Syria force a diplomatic response.",
                    Type = EventType.PlayerResponse,
                    Trigger = EventTrigger.ConditionBased,
                    TriggerCondition = $"node_control:{GameIds.Node.SyriaZone}={GameIds.Faction.Aurean}&&turn>=2",
                    Effects = new[] { $"relation_change:{GameIds.Faction.Aurean}:-2" }
                },
                new EventTemplate
                {
                    TemplateId = "evt_black_market_windfall",
                    EventName = "BlackMarketWindfall",
                    Narrative = "Informal channels temporarily improve market liquidity.",
                    Type = EventType.Random,
                    Trigger = EventTrigger.Random,
                    Probability = 0.18f,
                    AllowRepeat = true,
                    Effects = new[] { $"resource_change:{GameIds.Resource.GoldLeaf}:5", $"resource_change:{GameIds.Resource.TradeToken}:2" }
                },
                new EventTemplate
                {
                    TemplateId = "evt_supply_sabotage_wave",
                    EventName = "SupplySabotageWave",
                    Narrative = "A coordinated disruption campaign hits energy convoys.",
                    Type = EventType.Random,
                    Trigger = EventTrigger.Random,
                    Probability = 0.12f,
                    AllowRepeat = true,
                    Effects = new[] { $"resource_change:{GameIds.Resource.FireOil}:-5" }
                },
                new EventTemplate
                {
                    TemplateId = "evt_endgame_warning",
                    EventName = "EndgameWarning",
                    Narrative = "A prolonged crisis narrative starts to dominate global coverage.",
                    Type = EventType.Endgame,
                    Trigger = EventTrigger.ConditionBased,
                    TriggerCondition = $"turn>=20&&resource:{GameIds.Resource.FireOil}<=120",
                    Effects = new[] { $"resource_change:{GameIds.Resource.AshWill}:-3" }
                },
                new EventTemplate
                {
                    TemplateId = "evt_endgame_collapse",
                    EventName = "EndgameCollapse",
                    Narrative = "Systemic collapse conditions are met.",
                    Type = EventType.Endgame,
                    Trigger = EventTrigger.ConditionBased,
                    TriggerCondition = $"turn>=24&&resource:{GameIds.Resource.AshWill}<=20",
                    Effects = new[] { "end_game" }
                }
            };
        }
    }
}
