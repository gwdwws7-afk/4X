using System;
using UnityEngine;
using EventideAge.Core;

namespace EventideAge.Systems.G
{
    [Serializable]
    public class AIPhaseActionPreference
    {
        [Range(0, GameConfig.kAiResponsePhaseIndex)]
        public int PhaseIndex = 0;

        [Min(0f)] public float MilitaryWeight = 1f;
        [Min(0f)] public float DiplomaticWeight = 1f;
        [Min(0f)] public float EconomicWeight = 1f;

        public float GetWeight(AIDecisionType decisionType)
        {
            switch (decisionType)
            {
                case AIDecisionType.Military:
                    return Mathf.Max(0f, MilitaryWeight);
                case AIDecisionType.Diplomatic:
                    return Mathf.Max(0f, DiplomaticWeight);
                case AIDecisionType.Economic:
                    return Mathf.Max(0f, EconomicWeight);
                default:
                    return 1f;
            }
        }
    }

    [Serializable]
    public class AIFactionStrategyProfile
    {
        public string FactionId;
        public AIPersonality Personality = AIPersonality.Aggressive;
        [Range(0f, 1.5f)] public float AggressionLevel = 0.5f;
        [Range(0f, 1.5f)] public float DefenseLevel = 0.5f;
        [Range(0f, 1.5f)] public float DiplomaticLevel = 0.5f;
        [Range(0f, 1.5f)] public float ExpansionLevel = 0.5f;
        [Range(0f, 1.5f)] public float ThreatPerception = 0.5f;
        [Range(0f, 1.5f)] public float OpportunityPerception = 0.5f;
        public string[] ActiveGoals = Array.Empty<string>();
        public AIPhaseActionPreference[] PhasePreferences = Array.Empty<AIPhaseActionPreference>();

        public float GetPhaseWeight(AIDecisionType decisionType, int phaseIndex)
        {
            var phasePreferences = PhasePreferences;
            if (phasePreferences == null || phasePreferences.Length == 0)
                return 1f;

            for (int i = 0; i < phasePreferences.Length; i++)
            {
                var preference = phasePreferences[i];
                if (preference == null || preference.PhaseIndex != phaseIndex)
                    continue;
                return preference.GetWeight(decisionType);
            }

            return 1f;
        }

        public AIFactionStrategyProfile Clone()
        {
            var clonedGoals = ActiveGoals == null ? Array.Empty<string>() : (string[])ActiveGoals.Clone();

            var phasePreferences = PhasePreferences;
            AIPhaseActionPreference[] clonedPhasePreferences;
            if (phasePreferences == null || phasePreferences.Length == 0)
            {
                clonedPhasePreferences = Array.Empty<AIPhaseActionPreference>();
            }
            else
            {
                clonedPhasePreferences = new AIPhaseActionPreference[phasePreferences.Length];
                for (int i = 0; i < phasePreferences.Length; i++)
                {
                    var preference = phasePreferences[i];
                    clonedPhasePreferences[i] = preference == null
                        ? null
                        : new AIPhaseActionPreference
                        {
                            PhaseIndex = preference.PhaseIndex,
                            MilitaryWeight = preference.MilitaryWeight,
                            DiplomaticWeight = preference.DiplomaticWeight,
                            EconomicWeight = preference.EconomicWeight
                        };
                }
            }

            return new AIFactionStrategyProfile
            {
                FactionId = FactionId,
                Personality = Personality,
                AggressionLevel = AggressionLevel,
                DefenseLevel = DefenseLevel,
                DiplomaticLevel = DiplomaticLevel,
                ExpansionLevel = ExpansionLevel,
                ThreatPerception = ThreatPerception,
                OpportunityPerception = OpportunityPerception,
                ActiveGoals = clonedGoals,
                PhasePreferences = clonedPhasePreferences
            };
        }
    }

    [CreateAssetMenu(fileName = "AIStrategySet", menuName = "EventideAge/AIStrategySet")]
    public class AIStrategySet : ScriptableObject
    {
        [SerializeField] private AIFactionStrategyProfile[] _profiles = Array.Empty<AIFactionStrategyProfile>();

        public AIFactionStrategyProfile[] Profiles
        {
            get { return _profiles ?? Array.Empty<AIFactionStrategyProfile>(); }
        }

        public void SetProfiles(AIFactionStrategyProfile[] profiles)
        {
            _profiles = profiles ?? Array.Empty<AIFactionStrategyProfile>();
        }

        public AIFactionStrategyProfile GetProfile(string factionId)
        {
            string canonicalFactionId = GameIds.ResolveFactionId(factionId);
            var profiles = Profiles;
            for (int i = 0; i < profiles.Length; i++)
            {
                var profile = profiles[i];
                if (profile == null || string.IsNullOrWhiteSpace(profile.FactionId))
                    continue;

                if (GameIds.ResolveFactionId(profile.FactionId) == canonicalFactionId)
                    return profile;
            }

            return null;
        }

        public static AIFactionStrategyProfile[] CreateDefaultProfiles()
        {
            return new[]
            {
                new AIFactionStrategyProfile
                {
                    FactionId = GameIds.Faction.Aurean,
                    Personality = AIPersonality.Aggressive,
                    AggressionLevel = 0.8f,
                    DefenseLevel = 0.5f,
                    DiplomaticLevel = 0.3f,
                    ExpansionLevel = 0.7f,
                    ThreatPerception = 0.7f,
                    OpportunityPerception = 0.8f,
                    ActiveGoals = new[] { "maintain_blockade", "protect_allies", "expand_influence" },
                    PhasePreferences = new[]
                    {
                        new AIPhaseActionPreference { PhaseIndex = 0, MilitaryWeight = 0.9f, DiplomaticWeight = 1.2f, EconomicWeight = 0.85f },
                        new AIPhaseActionPreference { PhaseIndex = 1, MilitaryWeight = 1.1f, DiplomaticWeight = 0.85f, EconomicWeight = 1f },
                        new AIPhaseActionPreference { PhaseIndex = 2, MilitaryWeight = 1.35f, DiplomaticWeight = 0.7f, EconomicWeight = 0.9f },
                        new AIPhaseActionPreference { PhaseIndex = 3, MilitaryWeight = 0.75f, DiplomaticWeight = 1f, EconomicWeight = 1.2f },
                        new AIPhaseActionPreference { PhaseIndex = 4, MilitaryWeight = 0.85f, DiplomaticWeight = 1.05f, EconomicWeight = 1.1f },
                        new AIPhaseActionPreference { PhaseIndex = 5, MilitaryWeight = 0f, DiplomaticWeight = 0f, EconomicWeight = 0f }
                    }
                },
                new AIFactionStrategyProfile
                {
                    FactionId = GameIds.Faction.SacredFire,
                    Personality = AIPersonality.Defensive,
                    AggressionLevel = 0.3f,
                    DefenseLevel = 0.9f,
                    DiplomaticLevel = 0.4f,
                    ExpansionLevel = 0.2f,
                    ThreatPerception = 0.9f,
                    OpportunityPerception = 0.3f,
                    ActiveGoals = new[] { "defend_core_territory", "maintain_stability" },
                    PhasePreferences = new[]
                    {
                        new AIPhaseActionPreference { PhaseIndex = 0, MilitaryWeight = 0.7f, DiplomaticWeight = 1.2f, EconomicWeight = 1f },
                        new AIPhaseActionPreference { PhaseIndex = 1, MilitaryWeight = 0.95f, DiplomaticWeight = 1f, EconomicWeight = 1f },
                        new AIPhaseActionPreference { PhaseIndex = 2, MilitaryWeight = 1.1f, DiplomaticWeight = 0.8f, EconomicWeight = 0.9f },
                        new AIPhaseActionPreference { PhaseIndex = 3, MilitaryWeight = 0.7f, DiplomaticWeight = 1f, EconomicWeight = 1.25f },
                        new AIPhaseActionPreference { PhaseIndex = 4, MilitaryWeight = 0.75f, DiplomaticWeight = 1.15f, EconomicWeight = 1f },
                        new AIPhaseActionPreference { PhaseIndex = 5, MilitaryWeight = 0f, DiplomaticWeight = 0f, EconomicWeight = 0f }
                    }
                },
                new AIFactionStrategyProfile
                {
                    FactionId = GameIds.Faction.GoldenHord,
                    Personality = AIPersonality.Diplomatic,
                    AggressionLevel = 0.4f,
                    DefenseLevel = 0.6f,
                    DiplomaticLevel = 0.8f,
                    ExpansionLevel = 0.3f,
                    ThreatPerception = 0.5f,
                    OpportunityPerception = 0.6f,
                    ActiveGoals = new[] { "build_alliances", "trade_expansion", "maintain_balance" },
                    PhasePreferences = new[]
                    {
                        new AIPhaseActionPreference { PhaseIndex = 0, MilitaryWeight = 0.6f, DiplomaticWeight = 1.4f, EconomicWeight = 1.1f },
                        new AIPhaseActionPreference { PhaseIndex = 1, MilitaryWeight = 0.85f, DiplomaticWeight = 1.15f, EconomicWeight = 1f },
                        new AIPhaseActionPreference { PhaseIndex = 2, MilitaryWeight = 0.8f, DiplomaticWeight = 0.95f, EconomicWeight = 0.9f },
                        new AIPhaseActionPreference { PhaseIndex = 3, MilitaryWeight = 0.6f, DiplomaticWeight = 1.05f, EconomicWeight = 1.3f },
                        new AIPhaseActionPreference { PhaseIndex = 4, MilitaryWeight = 0.7f, DiplomaticWeight = 1.2f, EconomicWeight = 1.1f },
                        new AIPhaseActionPreference { PhaseIndex = 5, MilitaryWeight = 0f, DiplomaticWeight = 0f, EconomicWeight = 0f }
                    }
                },
                new AIFactionStrategyProfile
                {
                    FactionId = GameIds.Faction.AshConfederacy,
                    Personality = AIPersonality.Aggressive,
                    AggressionLevel = 0.7f,
                    DefenseLevel = 0.5f,
                    DiplomaticLevel = 0.2f,
                    ExpansionLevel = 0.6f,
                    ThreatPerception = 0.6f,
                    OpportunityPerception = 0.7f,
                    ActiveGoals = new[] { "destabilize_region", "exploit_conflicts", "expand_influence" },
                    PhasePreferences = new[]
                    {
                        new AIPhaseActionPreference { PhaseIndex = 0, MilitaryWeight = 0.8f, DiplomaticWeight = 0.85f, EconomicWeight = 0.9f },
                        new AIPhaseActionPreference { PhaseIndex = 1, MilitaryWeight = 1.2f, DiplomaticWeight = 0.8f, EconomicWeight = 0.95f },
                        new AIPhaseActionPreference { PhaseIndex = 2, MilitaryWeight = 1.4f, DiplomaticWeight = 0.65f, EconomicWeight = 0.85f },
                        new AIPhaseActionPreference { PhaseIndex = 3, MilitaryWeight = 0.8f, DiplomaticWeight = 0.9f, EconomicWeight = 1.1f },
                        new AIPhaseActionPreference { PhaseIndex = 4, MilitaryWeight = 0.9f, DiplomaticWeight = 1f, EconomicWeight = 1.05f },
                        new AIPhaseActionPreference { PhaseIndex = 5, MilitaryWeight = 0f, DiplomaticWeight = 0f, EconomicWeight = 0f }
                    }
                }
            };
        }
    }
}
