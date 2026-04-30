using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Profiling;
using EventideAge.Core;
using EventideAge.Config;
using EventideAge.Systems.A4;

namespace EventideAge.Tests
{
    public static class R5StabilityRunner
    {
        private const int kPerfTurnSamples = 240;
        private const int kPerfSaveLoadCycles = 24;
        private const int kSoakTurnSamples = 2400;
        private const int kSoakSaveLoadInterval = 50;
        private const string kPerfScenarioId = "R5-01-PERF-BASELINE";
        private const string kSoakScenarioId = "R5-04-SOAK-8H-ACCEL";

        private const double kBudgetTurnAvgMs = 16.67d;
        private const double kBudgetTurnP95Ms = 33.30d;
        private const double kBudgetSaveLoadP95Ms = 900d;
        private const double kBudgetManagedPeakMb = 1200d;
        private const double kBudgetManagedGrowthMb = 256d;

        private sealed class TurnSample
        {
            public int Index;
            public int Turn;
            public int Phase;
            public double StepMs;
            public long ManagedBytes;
            public long AllocatedBytes;
            public int Gc0;
            public int Gc1;
            public int Gc2;
            public bool SaveLoadExecuted;
            public bool SaveSuccess;
            public bool LoadSuccess;
            public double SaveLoadMs;
        }

        private sealed class BaselineResult
        {
            public string ScenarioId;
            public int TotalSamples;
            public double TurnAvgMs;
            public double TurnP95Ms;
            public double SaveLoadP95Ms;
            public double ManagedPeakMb;
            public double ManagedGrowthMb;
            public int SaveLoadFailures;
            public int Gc0Delta;
            public int Gc1Delta;
            public int Gc2Delta;
            public List<TurnSample> Samples = new List<TurnSample>();
        }

        public static void RunPerformanceBaseline()
        {
            BaselineResult result = ExecuteBaseline(
                kPerfScenarioId,
                kPerfTurnSamples,
                Mathf.Max(1, kPerfTurnSamples / kPerfSaveLoadCycles));

            string evidenceDir = GetEvidenceDirectory("baseline");
            string stamp = DateTime.Now.ToString("yyyyMMdd_HHmmss", CultureInfo.InvariantCulture);
            string csvPath = Path.Combine(evidenceDir, $"{stamp}_{kPerfScenarioId}.csv");
            string mdPath = Path.Combine(evidenceDir, $"{stamp}_{kPerfScenarioId}.md");

            WriteSamplesCsv(result.Samples, csvPath);
            WriteBaselineMarkdown(result, csvPath, mdPath);

            Debug.Log($"[R5] Performance baseline completed: {kPerfScenarioId}");
            Debug.Log($"[R5] CSV: {csvPath}");
            Debug.Log($"[R5] Report: {mdPath}");
        }

        public static void RunSoakBaseline()
        {
            BaselineResult result = ExecuteBaseline(
                kSoakScenarioId,
                kSoakTurnSamples,
                kSoakSaveLoadInterval);

            string evidenceDir = GetEvidenceDirectory("soak");
            string stamp = DateTime.Now.ToString("yyyyMMdd_HHmmss", CultureInfo.InvariantCulture);
            string csvPath = Path.Combine(evidenceDir, $"{stamp}_{kSoakScenarioId}.csv");
            string mdPath = Path.Combine(evidenceDir, $"{stamp}_{kSoakScenarioId}.md");

            WriteSamplesCsv(result.Samples, csvPath);
            WriteSoakMarkdown(result, csvPath, mdPath);

            Debug.Log($"[R5] Soak baseline completed: {kSoakScenarioId}");
            Debug.Log($"[R5] CSV: {csvPath}");
            Debug.Log($"[R5] Report: {mdPath}");
        }

        private static BaselineResult ExecuteBaseline(string scenarioId, int sampleCount, int saveLoadInterval)
        {
            var result = new BaselineResult
            {
                ScenarioId = scenarioId,
                TotalSamples = sampleCount
            };

            GameConfig config = null;
            GameState state = null;
            GameEvents events = null;
            GameObject saveGo = null;
            SaveSystem saveSystem = null;

            int gc0Start = GC.CollectionCount(0);
            int gc1Start = GC.CollectionCount(1);
            int gc2Start = GC.CollectionCount(2);
            long firstManaged = 0;
            long lastManaged = 0;

            try
            {
                config = DefaultGameConfig.CreateDefault();
                state = ScriptableObject.CreateInstance<GameState>();
                state.Config = config;
                state.Initialize();

                events = ScriptableObject.CreateInstance<GameEvents>();
                saveGo = new GameObject("R5_Stability_SaveSystem");
                saveSystem = saveGo.AddComponent<SaveSystem>();
                saveSystem.Initialize(state, events);

                var rng = new System.Random(20260429);
                int saveLoadFailures = 0;

                for (int i = 1; i <= sampleCount; i++)
                {
                    var sample = RunSingleSample(state, events, saveSystem, rng, i, saveLoadInterval, scenarioId);
                    result.Samples.Add(sample);

                    if (i == 1)
                    {
                        firstManaged = sample.ManagedBytes;
                    }

                    lastManaged = sample.ManagedBytes;

                    if (sample.SaveLoadExecuted && (!sample.SaveSuccess || !sample.LoadSuccess))
                    {
                        saveLoadFailures++;
                    }
                }

                CleanupTemporarySaves(saveSystem, scenarioId);

                result.SaveLoadFailures = saveLoadFailures;
                result.TurnAvgMs = result.Samples.Average(s => s.StepMs);
                result.TurnP95Ms = Percentile(result.Samples.Select(s => s.StepMs), 0.95d);
                result.SaveLoadP95Ms = Percentile(
                    result.Samples.Where(s => s.SaveLoadExecuted).Select(s => s.SaveLoadMs),
                    0.95d);
                result.ManagedPeakMb = BytesToMb(result.Samples.Max(s => s.ManagedBytes));
                result.ManagedGrowthMb = BytesToMb(Math.Max(0L, lastManaged - firstManaged));
                result.Gc0Delta = GC.CollectionCount(0) - gc0Start;
                result.Gc1Delta = GC.CollectionCount(1) - gc1Start;
                result.Gc2Delta = GC.CollectionCount(2) - gc2Start;
            }
            finally
            {
                if (saveGo != null)
                {
                    UnityEngine.Object.DestroyImmediate(saveGo);
                }

                if (events != null)
                {
                    UnityEngine.Object.DestroyImmediate(events);
                }

                if (state != null)
                {
                    UnityEngine.Object.DestroyImmediate(state);
                }

                if (config != null)
                {
                    UnityEngine.Object.DestroyImmediate(config);
                }
            }

            return result;
        }

        private static TurnSample RunSingleSample(
            GameState state,
            GameEvents events,
            SaveSystem saveSystem,
            System.Random rng,
            int index,
            int saveLoadInterval,
            string scenarioId)
        {
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            SimulateTurnWorkload(state, events, rng, index);
            stopwatch.Stop();

            var sample = new TurnSample
            {
                Index = index,
                Turn = state.CurrentTurn,
                Phase = state.CurrentPhaseIndex,
                StepMs = stopwatch.Elapsed.TotalMilliseconds,
                ManagedBytes = GC.GetTotalMemory(false),
                AllocatedBytes = Profiler.GetTotalAllocatedMemoryLong(),
                Gc0 = GC.CollectionCount(0),
                Gc1 = GC.CollectionCount(1),
                Gc2 = GC.CollectionCount(2)
            };

            if (saveLoadInterval > 0 && index % saveLoadInterval == 0)
            {
                string slot = $"{SanitizeToken(scenarioId)}_{index:D4}";
                var ioWatch = System.Diagnostics.Stopwatch.StartNew();
                bool saveOk = saveSystem.SaveGame(slot);
                bool loadOk = saveOk && saveSystem.LoadGame(slot);
                ioWatch.Stop();

                sample.SaveLoadExecuted = true;
                sample.SaveSuccess = saveOk;
                sample.LoadSuccess = loadOk;
                sample.SaveLoadMs = ioWatch.Elapsed.TotalMilliseconds;
            }

            return sample;
        }

        private static void SimulateTurnWorkload(GameState state, GameEvents events, System.Random rng, int sampleIndex)
        {
            int oldTurn = state.CurrentTurn;
            int nextTurn = (state.CurrentTurn % GameConfig.kMaxTurns) + 1;
            state.CurrentTurn = nextTurn;
            events.TurnChanged(oldTurn, nextTurn);

            state.CurrentPhaseIndex = sampleIndex % 6;
            events.PhaseChanged(state.CurrentPhaseIndex);

            int phaseBudget = ResolvePhaseBudget(state.CurrentPhaseIndex);
            state.CurrentPhaseActionPointsRemaining = Mathf.Clamp(
                phaseBudget - rng.Next(0, Mathf.Max(1, phaseBudget + 1)),
                0,
                phaseBudget);
            state.UniversalActionPointsRemaining = Mathf.Clamp(
                GameConfig.kUniversalActionPoints - rng.Next(0, 3),
                0,
                GameConfig.kUniversalActionPoints);
            state.ActionPointsRemaining = Mathf.Clamp(
                state.CurrentPhaseActionPointsRemaining + state.UniversalActionPointsRemaining + rng.Next(0, 4),
                0,
                GameConfig.kTotalActionPoints);
            events.ActionPointsChanged(state.ActionPointsRemaining);

            MutateResource(state, events, rng, GameIds.Resource.GoldLeaf, -14, 17);
            MutateResource(state, events, rng, GameIds.Resource.FireOil, -10, 13);
            MutateResource(state, events, rng, GameIds.Resource.Arms, -8, 11);
            MutateResource(state, events, rng, GameIds.Resource.AshWill, -4, 6);
            MutateResource(state, events, rng, GameIds.Resource.SocialValue, -5, 5);

            MaybeMutateNodeControl(state, events, rng, GameIds.Node.IraqBorder);
            MaybeMutateNodeControl(state, events, rng, GameIds.Node.SyriaZone);
            MaybeMutateNodeControl(state, events, rng, GameIds.Node.Hormuz);

            events.TurnEnded(nextTurn);
        }

        private static void MutateResource(
            GameState state,
            GameEvents events,
            System.Random rng,
            string resourceId,
            int minDelta,
            int maxDelta)
        {
            var resource = state.GetResource(resourceId);
            if (resource == null)
            {
                return;
            }

            int oldValue = resource.Amount;
            int delta = rng.Next(minDelta, maxDelta + 1);
            int next = oldValue + delta;
            int capped = resource.ResourceType == ResourceType.Ratio
                ? Mathf.Clamp(next, 0, 100)
                : Mathf.Clamp(next, 0, resource.MaxCapacity);

            resource.Amount = capped;
            if (capped != oldValue)
            {
                events.ResourceChanged(resourceId, oldValue, capped);
            }
        }

        private static void MaybeMutateNodeControl(GameState state, GameEvents events, System.Random rng, string nodeId)
        {
            var node = state.GetNode(nodeId);
            if (node == null)
            {
                return;
            }

            int roll = rng.Next(0, 100);
            if (roll >= 40)
            {
                return;
            }

            string oldController = node.ControllingFactionId;
            int delta = rng.Next(-12, 13);
            node.ControlPoints = Mathf.Clamp(node.ControlPoints + delta, 0, node.MaxControlPoints);

            if (roll < 10)
            {
                node.ControllingFactionId = oldController == GameIds.Faction.Aurean
                    ? GameIds.Faction.AshConfederacy
                    : GameIds.Faction.Aurean;
            }

            events.NodeControlChanged(
                node.NodeId,
                oldController,
                node.ControllingFactionId,
                node.ControlPoints);
        }

        private static int ResolvePhaseBudget(int phaseIndex)
        {
            switch (phaseIndex)
            {
                case 0: return 2;
                case 1: return 2;
                case 2: return 3;
                case 3: return 1;
                case 4: return 1;
                default: return 0;
            }
        }

        private static void CleanupTemporarySaves(SaveSystem saveSystem, string scenarioId)
        {
            if (saveSystem == null)
            {
                return;
            }

            string prefix = SanitizeToken(scenarioId).ToLowerInvariant() + "_";
            string[] saves = saveSystem.GetAllSaves();
            for (int i = 0; i < saves.Length; i++)
            {
                string name = saves[i] ?? string.Empty;
                if (name.ToLowerInvariant().StartsWith(prefix))
                {
                    saveSystem.DeleteSave(name);
                }
            }
        }

        private static void WriteSamplesCsv(List<TurnSample> samples, string csvPath)
        {
            var sb = new StringBuilder();
            sb.AppendLine("index,turn,phase,step_ms,managed_mb,allocated_mb,gc0,gc1,gc2,save_load_executed,save_success,load_success,save_load_ms");
            for (int i = 0; i < samples.Count; i++)
            {
                TurnSample s = samples[i];
                sb.Append(s.Index.ToString(CultureInfo.InvariantCulture)).Append(',')
                    .Append(s.Turn.ToString(CultureInfo.InvariantCulture)).Append(',')
                    .Append(s.Phase.ToString(CultureInfo.InvariantCulture)).Append(',')
                    .Append(s.StepMs.ToString("0.000", CultureInfo.InvariantCulture)).Append(',')
                    .Append(BytesToMb(s.ManagedBytes).ToString("0.00", CultureInfo.InvariantCulture)).Append(',')
                    .Append(BytesToMb(s.AllocatedBytes).ToString("0.00", CultureInfo.InvariantCulture)).Append(',')
                    .Append(s.Gc0.ToString(CultureInfo.InvariantCulture)).Append(',')
                    .Append(s.Gc1.ToString(CultureInfo.InvariantCulture)).Append(',')
                    .Append(s.Gc2.ToString(CultureInfo.InvariantCulture)).Append(',')
                    .Append(s.SaveLoadExecuted ? "1" : "0").Append(',')
                    .Append(s.SaveSuccess ? "1" : "0").Append(',')
                    .Append(s.LoadSuccess ? "1" : "0").Append(',')
                    .Append(s.SaveLoadMs.ToString("0.000", CultureInfo.InvariantCulture))
                    .AppendLine();
            }

            File.WriteAllText(csvPath, sb.ToString(), new UTF8Encoding(false));
        }

        private static void WriteBaselineMarkdown(BaselineResult result, string csvPath, string mdPath)
        {
            var sb = new StringBuilder();
            sb.AppendLine("# R5-01 Performance Baseline Report");
            sb.AppendLine();
            sb.AppendLine($"- Scenario: `{result.ScenarioId}`");
            sb.AppendLine($"- Samples: `{result.TotalSamples}`");
            sb.AppendLine($"- Source CSV: `{csvPath}`");
            sb.AppendLine();
            sb.AppendLine("| KPI | Actual | Budget | Status |");
            sb.AppendLine("|---|---:|---:|---|");
            AppendBudgetRow(sb, "TURN_AVG_MS", result.TurnAvgMs, kBudgetTurnAvgMs, false);
            AppendBudgetRow(sb, "TURN_P95_MS", result.TurnP95Ms, kBudgetTurnP95Ms, false);
            AppendBudgetRow(sb, "SAVELOAD_P95_MS", result.SaveLoadP95Ms, kBudgetSaveLoadP95Ms, false);
            AppendBudgetRow(sb, "MANAGED_PEAK_MB", result.ManagedPeakMb, kBudgetManagedPeakMb, false);
            AppendBudgetRow(sb, "MANAGED_GROWTH_MB", result.ManagedGrowthMb, kBudgetManagedGrowthMb, false);
            sb.AppendLine($"| SAVELOAD_FAILURES | {result.SaveLoadFailures} | 0 | {(result.SaveLoadFailures == 0 ? "PASS" : "FAIL")} |");
            sb.AppendLine($"| GC2_DELTA | {result.Gc2Delta} | <= 0 (baseline target) | {(result.Gc2Delta <= 0 ? "PASS" : "WARN")} |");
            sb.AppendLine();
            sb.AppendLine("## GC Delta");
            sb.AppendLine();
            sb.AppendLine($"- Gen0: `{result.Gc0Delta}`");
            sb.AppendLine($"- Gen1: `{result.Gc1Delta}`");
            sb.AppendLine($"- Gen2: `{result.Gc2Delta}`");

            File.WriteAllText(mdPath, sb.ToString(), new UTF8Encoding(false));
        }

        private static void WriteSoakMarkdown(BaselineResult result, string csvPath, string mdPath)
        {
            var sb = new StringBuilder();
            sb.AppendLine("# R5-04 Soak Stability Report (Accelerated)");
            sb.AppendLine();
            sb.AppendLine($"- Scenario: `{result.ScenarioId}`");
            sb.AppendLine($"- Samples: `{result.TotalSamples}`");
            sb.AppendLine($"- Source CSV: `{csvPath}`");
            sb.AppendLine();
            sb.AppendLine("## Summary");
            sb.AppendLine();
            sb.AppendLine($"- Turn avg: `{result.TurnAvgMs.ToString("0.000", CultureInfo.InvariantCulture)} ms`");
            sb.AppendLine($"- Turn p95: `{result.TurnP95Ms.ToString("0.000", CultureInfo.InvariantCulture)} ms`");
            sb.AppendLine($"- Save/Load p95: `{result.SaveLoadP95Ms.ToString("0.000", CultureInfo.InvariantCulture)} ms`");
            sb.AppendLine($"- Managed peak: `{result.ManagedPeakMb.ToString("0.00", CultureInfo.InvariantCulture)} MB`");
            sb.AppendLine($"- Managed growth: `{result.ManagedGrowthMb.ToString("0.00", CultureInfo.InvariantCulture)} MB`");
            sb.AppendLine($"- Save/Load failures: `{result.SaveLoadFailures}`");
            sb.AppendLine($"- GC delta: `gen0={result.Gc0Delta}, gen1={result.Gc1Delta}, gen2={result.Gc2Delta}`");
            sb.AppendLine();
            sb.AppendLine("## Gate Check");
            sb.AppendLine();
            sb.AppendLine("- Blocking crash: N/A (script-level dry run, no engine crash captured here)");
            sb.AppendLine($"- Save/Load stability: {(result.SaveLoadFailures == 0 ? "PASS" : "FAIL")}");
            sb.AppendLine($"- Memory drift budget ({kBudgetManagedGrowthMb.ToString("0.00", CultureInfo.InvariantCulture)} MB): {(result.ManagedGrowthMb <= kBudgetManagedGrowthMb ? "PASS" : "FAIL")}");

            File.WriteAllText(mdPath, sb.ToString(), new UTF8Encoding(false));
        }

        private static void AppendBudgetRow(StringBuilder sb, string kpi, double actual, double budget, bool lowerIsBad)
        {
            bool pass = lowerIsBad ? actual >= budget : actual <= budget;
            sb.AppendLine(
                $"| {kpi} | {actual.ToString("0.000", CultureInfo.InvariantCulture)} | " +
                $"{budget.ToString("0.000", CultureInfo.InvariantCulture)} | {(pass ? "PASS" : "FAIL")} |");
        }

        private static double Percentile(IEnumerable<double> values, double p)
        {
            var sorted = values.Where(v => !double.IsNaN(v) && !double.IsInfinity(v)).OrderBy(v => v).ToList();
            if (sorted.Count == 0)
            {
                return 0d;
            }

            if (sorted.Count == 1)
            {
                return sorted[0];
            }

            double index = (sorted.Count - 1) * Mathf.Clamp01((float)p);
            int lo = (int)Math.Floor(index);
            int hi = (int)Math.Ceiling(index);
            if (lo == hi)
            {
                return sorted[lo];
            }

            double weight = index - lo;
            return sorted[lo] + ((sorted[hi] - sorted[lo]) * weight);
        }

        private static double BytesToMb(long bytes)
        {
            return bytes / (1024d * 1024d);
        }

        private static string GetEvidenceDirectory(string bucket)
        {
            string root = GetProjectRoot();
            string path = Path.Combine(root, "production", "evidence", "r5", bucket);
            Directory.CreateDirectory(path);
            return path;
        }

        private static string GetProjectRoot()
        {
            string current = Directory.GetCurrentDirectory();
            if (Directory.Exists(Path.Combine(current, "Assets")))
            {
                return current;
            }

            if (!string.IsNullOrEmpty(Application.dataPath))
            {
                return Path.GetDirectoryName(Application.dataPath) ?? current;
            }

            return current;
        }

        private static string SanitizeToken(string raw)
        {
            if (string.IsNullOrWhiteSpace(raw))
            {
                return "token";
            }

            string token = raw.Trim().ToLowerInvariant()
                .Replace("-", "_")
                .Replace(" ", "_");
            var sb = new StringBuilder(token.Length);
            for (int i = 0; i < token.Length; i++)
            {
                char ch = token[i];
                sb.Append(char.IsLetterOrDigit(ch) || ch == '_' ? ch : '_');
            }

            return sb.ToString();
        }

    }
}
