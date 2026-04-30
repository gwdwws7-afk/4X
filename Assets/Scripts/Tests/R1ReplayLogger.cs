using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using EventideAge.Core;

namespace EventideAge.Tests
{
    internal sealed class R1ReplayLogger
    {
        private readonly string _runId;
        private readonly string _chainId;
        private readonly List<string> _lines = new List<string>();

        public R1ReplayLogger(string runId, string chainId)
        {
            _runId = string.IsNullOrWhiteSpace(runId) ? "R1-03" : runId.Trim();
            _chainId = string.IsNullOrWhiteSpace(chainId) ? "UNKNOWN" : chainId.Trim();

            RecordMeta("START", "Replay chain started");
        }

        public void RecordStep(
            GameState state,
            string stepId,
            string action,
            string expected,
            string actual,
            bool pass)
        {
            string line = string.Join("|", new[]
            {
                $"ts={DateTime.UtcNow:O}",
                $"run={Escape(_runId)}",
                $"chain={Escape(_chainId)}",
                $"step={Escape(stepId)}",
                $"result={(pass ? "PASS" : "FAIL")}",
                $"turn={state?.CurrentTurn ?? -1}",
                $"phase={state?.CurrentPhaseIndex ?? -1}",
                $"ap={state?.ActionPointsRemaining ?? -1}",
                $"phaseAp={state?.CurrentPhaseActionPointsRemaining ?? -1}",
                $"uap={state?.UniversalActionPointsRemaining ?? -1}",
                $"action={Escape(action)}",
                $"expected={Escape(expected)}",
                $"actual={Escape(actual)}"
            });

            _lines.Add(line);
            Debug.Log($"[R1-REPLAY] {line}");
        }

        public void Flush()
        {
            RecordMeta("END", "Replay chain finished");

            try
            {
                string outputDirectory = ResolveOutputDirectory();
                Directory.CreateDirectory(outputDirectory);

                string fileName = $"{DateTime.Now:yyyyMMdd_HHmmss}_{SanitizeFileToken(_runId)}_{SanitizeFileToken(_chainId)}.log";
                string filePath = Path.Combine(outputDirectory, fileName);
                File.WriteAllLines(filePath, _lines);

                Debug.Log($"[R1-REPLAY] Log saved: {filePath}");
            }
            catch (Exception ex)
            {
                Debug.LogWarning($"[R1-REPLAY] Failed to write replay log file: {ex.Message}");
            }
        }

        private void RecordMeta(string marker, string message)
        {
            string line = string.Join("|", new[]
            {
                $"ts={DateTime.UtcNow:O}",
                $"run={Escape(_runId)}",
                $"chain={Escape(_chainId)}",
                $"meta={Escape(marker)}",
                $"message={Escape(message)}"
            });

            _lines.Add(line);
            Debug.Log($"[R1-REPLAY] {line}");
        }

        private static string ResolveOutputDirectory()
        {
            if (string.IsNullOrWhiteSpace(Application.dataPath))
            {
                return Path.Combine(Directory.GetCurrentDirectory(), "production", "evidence", "r1", "replay-logs");
            }

            string projectRoot = Path.GetFullPath(Path.Combine(Application.dataPath, ".."));
            return Path.Combine(projectRoot, "production", "evidence", "r1", "replay-logs");
        }

        private static string Escape(string value)
        {
            if (string.IsNullOrEmpty(value))
                return string.Empty;

            return value
                .Replace("\\", "\\\\")
                .Replace("|", "/")
                .Replace("\r", " ")
                .Replace("\n", " ")
                .Trim();
        }

        private static string SanitizeFileToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                return "unknown";

            char[] invalidChars = Path.GetInvalidFileNameChars();
            var chars = token.Trim().ToCharArray();
            for (int i = 0; i < chars.Length; i++)
            {
                if (Array.IndexOf(invalidChars, chars[i]) >= 0)
                {
                    chars[i] = '_';
                }
            }

            return new string(chars);
        }
    }
}
