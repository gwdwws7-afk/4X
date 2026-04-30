using System;
using UnityEngine;

namespace EventideAge.Systems.G
{
    public enum AIDifficultyLevel
    {
        Easy,
        Standard,
        Hard
    }

    [Serializable]
    public class AIDifficultyProfile
    {
        public AIDifficultyLevel Difficulty = AIDifficultyLevel.Standard;

        [Header("Personality Multipliers")]
        [Range(0.5f, 2f)] public float AggressiveAggression = 1.5f;
        [Range(0.5f, 2f)] public float DefensiveDefense = 1.5f;
        [Range(0.5f, 2f)] public float DiplomaticDiplomacy = 1.5f;
        [Range(0.5f, 2f)] public float ExpansionistExpansion = 1.5f;

        [Header("Cadence and Coordination")]
        [Min(1)] public int AIUpdateInterval = 1;
        [Min(0)] public int BaseCoordinationCost = 10;

        [Header("Decision Thresholds")]
        [Range(0f, 1f)] public float MilitaryActionThreshold = 0.5f;
        [Range(0f, 1f)] public float DiplomaticActionThreshold = 0.4f;
        [Range(0f, 1f)] public float EconomicActionThreshold = 0.3f;

        [Header("Resource Gating")]
        [Min(0)] public int MinGoldLeafForAttack = 30;
        [Min(0)] public int MinArmsForMilitaryAction = 5;
        [Range(0f, 1f)] public float LowResourceThreshold = 0.3f;

        public AIDifficultyProfile Clone()
        {
            return new AIDifficultyProfile
            {
                Difficulty = Difficulty,
                AggressiveAggression = AggressiveAggression,
                DefensiveDefense = DefensiveDefense,
                DiplomaticDiplomacy = DiplomaticDiplomacy,
                ExpansionistExpansion = ExpansionistExpansion,
                AIUpdateInterval = AIUpdateInterval,
                BaseCoordinationCost = BaseCoordinationCost,
                MilitaryActionThreshold = MilitaryActionThreshold,
                DiplomaticActionThreshold = DiplomaticActionThreshold,
                EconomicActionThreshold = EconomicActionThreshold,
                MinGoldLeafForAttack = MinGoldLeafForAttack,
                MinArmsForMilitaryAction = MinArmsForMilitaryAction,
                LowResourceThreshold = LowResourceThreshold
            };
        }
    }

    [CreateAssetMenu(fileName = "AIDifficultySet", menuName = "EventideAge/AIDifficultySet")]
    public class AIDifficultySet : ScriptableObject
    {
        [SerializeField] private AIDifficultyProfile[] _profiles = Array.Empty<AIDifficultyProfile>();

        public AIDifficultyProfile[] Profiles
        {
            get { return _profiles ?? Array.Empty<AIDifficultyProfile>(); }
        }

        public void SetProfiles(AIDifficultyProfile[] profiles)
        {
            _profiles = profiles ?? Array.Empty<AIDifficultyProfile>();
        }

        public AIDifficultyProfile GetProfile(AIDifficultyLevel difficulty)
        {
            var profiles = Profiles;
            for (int i = 0; i < profiles.Length; i++)
            {
                var profile = profiles[i];
                if (profile == null)
                    continue;

                if (profile.Difficulty == difficulty)
                    return profile;
            }

            return null;
        }

        public static AIDifficultyProfile CreateDefaultProfile(AIDifficultyLevel difficulty)
        {
            switch (difficulty)
            {
                case AIDifficultyLevel.Easy:
                    return CreateEasyProfile();
                case AIDifficultyLevel.Hard:
                    return CreateHardProfile();
                default:
                    return CreateStandardProfile();
            }
        }

        public static AIDifficultyProfile[] CreateDefaultProfiles()
        {
            return new[]
            {
                CreateEasyProfile(),
                CreateStandardProfile(),
                CreateHardProfile()
            };
        }

        private static AIDifficultyProfile CreateEasyProfile()
        {
            return new AIDifficultyProfile
            {
                Difficulty = AIDifficultyLevel.Easy,
                AggressiveAggression = 1.2f,
                DefensiveDefense = 1.25f,
                DiplomaticDiplomacy = 1.2f,
                ExpansionistExpansion = 1.2f,
                AIUpdateInterval = 2,
                BaseCoordinationCost = 14,
                MilitaryActionThreshold = 0.62f,
                DiplomaticActionThreshold = 0.52f,
                EconomicActionThreshold = 0.42f,
                MinGoldLeafForAttack = 45,
                MinArmsForMilitaryAction = 10,
                LowResourceThreshold = 0.45f
            };
        }

        private static AIDifficultyProfile CreateStandardProfile()
        {
            return new AIDifficultyProfile
            {
                Difficulty = AIDifficultyLevel.Standard,
                AggressiveAggression = 1.5f,
                DefensiveDefense = 1.5f,
                DiplomaticDiplomacy = 1.5f,
                ExpansionistExpansion = 1.5f,
                AIUpdateInterval = 1,
                BaseCoordinationCost = 10,
                MilitaryActionThreshold = 0.5f,
                DiplomaticActionThreshold = 0.4f,
                EconomicActionThreshold = 0.3f,
                MinGoldLeafForAttack = 30,
                MinArmsForMilitaryAction = 5,
                LowResourceThreshold = 0.3f
            };
        }

        private static AIDifficultyProfile CreateHardProfile()
        {
            return new AIDifficultyProfile
            {
                Difficulty = AIDifficultyLevel.Hard,
                AggressiveAggression = 1.75f,
                DefensiveDefense = 1.7f,
                DiplomaticDiplomacy = 1.7f,
                ExpansionistExpansion = 1.75f,
                AIUpdateInterval = 1,
                BaseCoordinationCost = 8,
                MilitaryActionThreshold = 0.42f,
                DiplomaticActionThreshold = 0.32f,
                EconomicActionThreshold = 0.24f,
                MinGoldLeafForAttack = 20,
                MinArmsForMilitaryAction = 3,
                LowResourceThreshold = 0.2f
            };
        }
    }
}
