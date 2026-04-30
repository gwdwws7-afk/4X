using System;
using UnityEngine;

namespace EventideAge.Systems.L3
{
    [Serializable]
    public class SteamAchievementDefinition
    {
        public string AchievementId;
        public string DisplayName;
        [TextArea(1, 3)] public string Description;
    }

    [CreateAssetMenu(fileName = "SteamIntegrationConfig", menuName = "EventideAge/SteamIntegrationConfig")]
    public class SteamIntegrationConfig : ScriptableObject
    {
        [Header("Provider")]
        public bool ForceMockProviderInEditor = true;
        public string AppId = "000000";
        public string BuildChannel = "dev";

        [Header("Achievements")]
        public SteamAchievementDefinition[] Achievements =
        {
            new SteamAchievementDefinition
            {
                AchievementId = "FIRST_STEP",
                DisplayName = "First Step",
                Description = "Complete the first turn."
            },
            new SteamAchievementDefinition
            {
                AchievementId = "ENERGY_LIBERATION",
                DisplayName = "Energy Liberation",
                Description = "Reach energy liberation victory."
            },
            new SteamAchievementDefinition
            {
                AchievementId = "SURVIVOR",
                DisplayName = "Survivor",
                Description = "Reach turn 20."
            }
        };
    }
}
