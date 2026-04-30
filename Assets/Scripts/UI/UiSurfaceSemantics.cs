using System;
using EventideAge.Core;

namespace EventideAge.UI
{
    internal enum UiStatusTag
    {
        Stable = 0,
        Warning = 1,
        Critical = 2,
        Locked = 3
    }

    internal enum UiSurfaceTarget
    {
        None = 0,
        Map = 1,
        Diplomacy = 2,
        BattleReport = 3,
        Event = 4
    }

    internal static class UiSurfaceSemantics
    {
        private static readonly string[] kLockedTokens =
        {
            "blocked",
            "unavailable",
            "insufficient ap",
            "requirement not met",
            "locked"
        };

        public static string AppendMeta(
            string line,
            FeedbackSeverity severity,
            string sourceId,
            string message,
            UiSurfaceTarget fallbackTarget)
        {
            UiStatusTag status = ResolveStatusTag(severity, message);
            UiSurfaceTarget jumpTarget = ResolveJumpTarget(sourceId, message);
            if (jumpTarget == UiSurfaceTarget.None)
            {
                jumpTarget = fallbackTarget;
            }

            string hint = ResolveNextHint(status, jumpTarget, message);
            string cue = ResolveCue(status);

            if (status == UiStatusTag.Locked)
            {
                string reason = ExtractLockReason(message);
                if (!string.IsNullOrWhiteSpace(reason))
                {
                    return $"{line} | status:{status} reason:{reason} | jump:{jumpTarget} hint:{hint} cue:{cue}";
                }
            }

            return $"{line} | status:{status} | jump:{jumpTarget} hint:{hint} cue:{cue}";
        }

        public static UiStatusTag ResolveStatusTag(FeedbackSeverity severity, string message)
        {
            if (LooksLocked(message))
            {
                return UiStatusTag.Locked;
            }

            switch (severity)
            {
                case FeedbackSeverity.Critical:
                    return UiStatusTag.Critical;
                case FeedbackSeverity.Warning:
                    return UiStatusTag.Warning;
                default:
                    return UiStatusTag.Stable;
            }
        }

        public static UiSurfaceTarget ResolveJumpTarget(string sourceId, string message)
        {
            if (!string.IsNullOrWhiteSpace(sourceId))
            {
                if (sourceId.StartsWith("I", StringComparison.OrdinalIgnoreCase))
                {
                    return UiSurfaceTarget.Event;
                }

                if (sourceId.StartsWith("C", StringComparison.OrdinalIgnoreCase))
                {
                    return UiSurfaceTarget.Diplomacy;
                }

                if (sourceId.StartsWith("D2.", StringComparison.OrdinalIgnoreCase)
                    || sourceId.StartsWith("D5.", StringComparison.OrdinalIgnoreCase))
                {
                    return UiSurfaceTarget.BattleReport;
                }

                if (sourceId.StartsWith("D", StringComparison.OrdinalIgnoreCase)
                    || sourceId.StartsWith("H", StringComparison.OrdinalIgnoreCase)
                    || sourceId.StartsWith("B2.", StringComparison.OrdinalIgnoreCase)
                    || sourceId.StartsWith("MAP.", StringComparison.OrdinalIgnoreCase))
                {
                    return UiSurfaceTarget.Map;
                }
            }

            if (!string.IsNullOrWhiteSpace(message))
            {
                string normalized = message.Trim().ToLowerInvariant();
                if (normalized.Contains("event") || normalized.Contains("narrative"))
                {
                    return UiSurfaceTarget.Event;
                }

                if (normalized.Contains("diplom") || normalized.Contains("relation"))
                {
                    return UiSurfaceTarget.Diplomacy;
                }

                if (normalized.Contains("battle") || normalized.Contains("casualt"))
                {
                    return UiSurfaceTarget.BattleReport;
                }

                if (normalized.Contains("node") || normalized.Contains("route") || normalized.Contains("control"))
                {
                    return UiSurfaceTarget.Map;
                }
            }

            return UiSurfaceTarget.None;
        }

        private static bool LooksLocked(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                return false;
            }

            string normalized = message.Trim().ToLowerInvariant();
            for (int i = 0; i < kLockedTokens.Length; i++)
            {
                if (normalized.Contains(kLockedTokens[i]))
                {
                    return true;
                }
            }

            return false;
        }

        private static string ExtractLockReason(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                return string.Empty;
            }

            int colon = message.IndexOf(':');
            if (colon >= 0 && colon + 1 < message.Length)
            {
                return message.Substring(colon + 1).Trim();
            }

            return "action unavailable";
        }

        private static string ResolveNextHint(UiStatusTag status, UiSurfaceTarget jumpTarget, string message)
        {
            if (status == UiStatusTag.Locked)
            {
                return ResolveLockedHint(message);
            }

            if (status == UiStatusTag.Critical)
            {
                switch (jumpTarget)
                {
                    case UiSurfaceTarget.Map:
                        return "stabilize-map-hotspot";
                    case UiSurfaceTarget.Diplomacy:
                        return "stabilize-diplomacy";
                    case UiSurfaceTarget.BattleReport:
                        return "resolve-battle-risk";
                    case UiSurfaceTarget.Event:
                        return "resolve-event-now";
                    default:
                        return "resolve-critical-risk";
                }
            }

            if (status == UiStatusTag.Warning)
            {
                switch (jumpTarget)
                {
                    case UiSurfaceTarget.Map:
                        return "review-map-update";
                    case UiSurfaceTarget.Diplomacy:
                        return "review-diplomacy-update";
                    case UiSurfaceTarget.BattleReport:
                        return "review-battle-report";
                    case UiSurfaceTarget.Event:
                        return "review-event-update";
                    default:
                        return "review-update";
                }
            }

            return "continue-turn-plan";
        }

        private static string ResolveLockedHint(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                return "unlock-action";
            }

            string normalized = message.Trim().ToLowerInvariant();
            if (normalized.Contains("insufficient ap"))
            {
                return "recover-ap";
            }

            if (normalized.Contains("requirement not met") || normalized.Contains("prerequisite"))
            {
                return "meet-requirement";
            }

            if (normalized.Contains("phase"))
            {
                return "switch-phase";
            }

            return "unlock-action";
        }

        private static string ResolveCue(UiStatusTag status)
        {
            switch (status)
            {
                case UiStatusTag.Critical:
                case UiStatusTag.Locked:
                    return "AlertPulse";
                case UiStatusTag.Warning:
                    return "SoftPulse";
                default:
                    return "None";
            }
        }
    }
}
