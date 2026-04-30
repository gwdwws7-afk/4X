using System;
using System.Collections.Generic;
using UnityEngine;
using EventideAge.Core;

namespace EventideAge.Systems.L3
{
    internal interface IPlatformProvider
    {
        string ProviderName { get; }
        bool SupportsOverlay { get; }
        bool IsInitialized { get; }

        bool Initialize(string appId);
        bool UnlockAchievement(string achievementId);
        bool UploadCloudSave(string slotId, string payload, out string error);
        bool DownloadCloudSave(string slotId, out string payload, out string error);
        string[] GetUnlockedAchievements();
    }

    internal sealed class NullPlatformProvider : IPlatformProvider
    {
        public string ProviderName => "NullProvider";
        public bool SupportsOverlay => false;
        public bool IsInitialized { get; private set; }

        public bool Initialize(string appId)
        {
            IsInitialized = true;
            return true;
        }

        public bool UnlockAchievement(string achievementId)
        {
            return false;
        }

        public bool UploadCloudSave(string slotId, string payload, out string error)
        {
            error = "cloud_not_supported";
            return false;
        }

        public bool DownloadCloudSave(string slotId, out string payload, out string error)
        {
            payload = string.Empty;
            error = "cloud_not_supported";
            return false;
        }

        public string[] GetUnlockedAchievements()
        {
            return Array.Empty<string>();
        }
    }

    internal sealed class MockPlatformProvider : IPlatformProvider
    {
        private readonly HashSet<string> _unlockedAchievements = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        private readonly Dictionary<string, string> _cloudSlots = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        public string ProviderName => "MockProvider";
        public bool SupportsOverlay => true;
        public bool IsInitialized { get; private set; }

        public bool Initialize(string appId)
        {
            IsInitialized = !string.IsNullOrWhiteSpace(appId);
            return IsInitialized;
        }

        public bool UnlockAchievement(string achievementId)
        {
            if (string.IsNullOrWhiteSpace(achievementId))
            {
                return false;
            }

            _unlockedAchievements.Add(achievementId.Trim());
            return true;
        }

        public bool UploadCloudSave(string slotId, string payload, out string error)
        {
            error = string.Empty;
            if (string.IsNullOrWhiteSpace(slotId))
            {
                error = "invalid_slot";
                return false;
            }

            _cloudSlots[slotId.Trim()] = payload ?? string.Empty;
            return true;
        }

        public bool DownloadCloudSave(string slotId, out string payload, out string error)
        {
            payload = string.Empty;
            error = string.Empty;
            if (string.IsNullOrWhiteSpace(slotId))
            {
                error = "invalid_slot";
                return false;
            }

            if (!_cloudSlots.TryGetValue(slotId.Trim(), out payload))
            {
                error = "slot_not_found";
                return false;
            }

            return true;
        }

        public string[] GetUnlockedAchievements()
        {
            var snapshot = new string[_unlockedAchievements.Count];
            _unlockedAchievements.CopyTo(snapshot);
            return snapshot;
        }
    }

    public class SteamIntegrationSystem : GameSystem
    {
        [Header("L3 Platform")]
        public SteamIntegrationConfig Config;
        public bool UseMockProviderInEditor = true;
        public bool AutoInitializeOnStart = true;

        private IPlatformProvider _provider;
        private bool _platformReady;

        public override void Initialize(GameState state, GameEvents events)
        {
            base.Initialize(state, events);

            _provider = SelectProvider();
            if (AutoInitializeOnStart)
            {
                _platformReady = _provider.Initialize(GetAppId());
                Events.ActionLogAdded(
                    "L3",
                    $"Platform provider ready: {_provider.ProviderName} (initialized={_platformReady})",
                    _platformReady ? FeedbackSeverity.Info : FeedbackSeverity.Warning);
            }
        }

        public string GetProviderName()
        {
            return _provider != null ? _provider.ProviderName : "Uninitialized";
        }

        public bool IsPlatformReady()
        {
            return _platformReady && _provider != null && _provider.IsInitialized;
        }

        public bool SupportsOverlay()
        {
            return _provider != null && _provider.SupportsOverlay;
        }

        public bool TryInitializePlatform()
        {
            _provider = _provider ?? SelectProvider();
            _platformReady = _provider.Initialize(GetAppId());
            return _platformReady;
        }

        public bool TryUnlockAchievement(string achievementId)
        {
            if (!EnsureReady())
            {
                Events.AlertAdded("L3", "Platform not ready. Achievement unlock deferred.", FeedbackSeverity.Warning);
                return false;
            }

            bool unlocked = _provider.UnlockAchievement(achievementId);
            if (unlocked)
            {
                Events.NotificationAdded("L3", $"Achievement unlocked: {achievementId}", FeedbackSeverity.Info);
            }
            else
            {
                Events.AlertAdded("L3", $"Achievement unlock failed: {achievementId}", FeedbackSeverity.Warning);
            }

            return unlocked;
        }

        public bool TryUploadCloudSave(string slotId, string payload, out string error)
        {
            if (!EnsureReady())
            {
                error = "platform_not_ready";
                return false;
            }

            bool ok = _provider.UploadCloudSave(slotId, payload, out error);
            if (ok)
            {
                Events.ActionLogAdded("L3", $"Cloud save uploaded: {slotId}", FeedbackSeverity.Info);
            }
            else
            {
                Events.AlertAdded("L3", $"Cloud save upload failed: {slotId} ({error})", FeedbackSeverity.Warning);
            }

            return ok;
        }

        public bool TryDownloadCloudSave(string slotId, out string payload, out string error)
        {
            payload = string.Empty;
            if (!EnsureReady())
            {
                error = "platform_not_ready";
                return false;
            }

            bool ok = _provider.DownloadCloudSave(slotId, out payload, out error);
            if (ok)
            {
                Events.ActionLogAdded("L3", $"Cloud save downloaded: {slotId}", FeedbackSeverity.Info);
            }
            else
            {
                Events.AlertAdded("L3", $"Cloud save download failed: {slotId} ({error})", FeedbackSeverity.Warning);
            }

            return ok;
        }

        public string[] GetUnlockedAchievements()
        {
            if (_provider == null)
            {
                return Array.Empty<string>();
            }

            return _provider.GetUnlockedAchievements();
        }

        private bool EnsureReady()
        {
            if (_provider == null)
            {
                _provider = SelectProvider();
            }

            if (_platformReady && _provider.IsInitialized)
            {
                return true;
            }

            _platformReady = _provider.Initialize(GetAppId());
            return _platformReady && _provider.IsInitialized;
        }

        private IPlatformProvider SelectProvider()
        {
            bool useMock = Application.isEditor && UseMockProviderInEditor;
            if (Config != null && Config.ForceMockProviderInEditor && Application.isEditor)
            {
                useMock = true;
            }

            return useMock ? (IPlatformProvider)new MockPlatformProvider() : new NullPlatformProvider();
        }

        private string GetAppId()
        {
            if (Config == null || string.IsNullOrWhiteSpace(Config.AppId))
            {
                return "000000";
            }

            return Config.AppId.Trim();
        }
    }
}
