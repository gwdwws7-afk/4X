using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using EventideAge.Core;
using EventideAge.Config;
using EventideAge.Systems.J;

namespace EventideAge.Tests
{
    public static class R3SimulationRunner
    {
        private enum SimulationDifficulty
        {
            Easy,
            Standard,
            Hard
        }

        private sealed class SimulationRecord
        {
            public string BatchId;
            public string RunId;
            public string RunDate;
            public string BuildRef;
            public string Difficulty;
            public int Seed;
            public string EndType;
            public string EndReason;
            public string VictoryPath;
            public int EndTurn;
            public float DurationMinutes;
            public float GoldLeafCv;
            public float FireOilCv;
            public float ArmsCv;
            public int AshWillMin;
            public int FireOilMin;
            public int GoldLeafMin;
            public string Notes;
        }

        private const int kTotalRuns = 1000;
        private const int kEasyRuns = 300;
        private const int kStandardRuns = 400;
        private const int kHardRuns = 300;
        private const string kBatchId = "R3-02-B001";
        private const string kBuildRef = "workspace-head";

        public static void RunBatch1000()
        {
            var records = new List<SimulationRecord>(kTotalRuns);
            var plan = BuildDifficultyPlan();
            string runDate = DateTime.Now.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

            for (int i = 0; i < plan.Count; i++)
            {
                int seed = 200000 + i;
                var record = SimulateSingleRun(i + 1, plan[i], seed, runDate);
                records.Add(record);

                if ((i + 1) % 100 == 0)
                {
                    Debug.Log($"[R3Sim] Progress: {i + 1}/{kTotalRuns}");
                }
            }

            string evidenceDir = GetEvidenceDirectory();
            string dateToken = DateTime.Now.ToString("yyyyMMdd", CultureInfo.InvariantCulture);
            string csvPath = Path.Combine(evidenceDir, $"{dateToken}_R3-02_SIMULATION-RUN-001.csv");
            string summaryPath = Path.Combine(evidenceDir, $"{dateToken}_R3-02_KPI-EVALUATION-001.md");

            WriteSimulationCsv(records, csvPath);
            WriteSummaryMarkdown(records, summaryPath, csvPath);
            WriteEvidenceReadme(evidenceDir, csvPath, summaryPath);

            Debug.Log($"[R3Sim] Completed batch={kBatchId} runs={records.Count}");
            Debug.Log($"[R3Sim] CSV: {csvPath}");
            Debug.Log($"[R3Sim] Summary: {summaryPath}");
        }

        private static List<SimulationDifficulty> BuildDifficultyPlan()
        {
            var plan = new List<SimulationDifficulty>(kTotalRuns);
            for (int i = 0; i < kEasyRuns; i++) plan.Add(SimulationDifficulty.Easy);
            for (int i = 0; i < kStandardRuns; i++) plan.Add(SimulationDifficulty.Standard);
            for (int i = 0; i < kHardRuns; i++) plan.Add(SimulationDifficulty.Hard);
            return plan;
        }

        private static SimulationRecord SimulateSingleRun(int index, SimulationDifficulty difficulty, int seed, string runDate)
        {
            var rng = new System.Random(seed);
            var config = DefaultGameConfig.CreateDefault();
            var state = ScriptableObject.CreateInstance<GameState>();
            state.Config = config;
            state.Initialize();

            ApplyDifficultyInitialization(state, difficulty, rng);

            var events = ScriptableObject.CreateInstance<GameEvents>();
            var jGo = new GameObject($"R3Sim_{index:D4}");
            var j = jGo.AddComponent<VictoryDefeatSystem>();
            ConfigureDifficultyThresholds(j, difficulty);
            j.Initialize(state, events);

            var goldSeries = new List<float>(GameConfig.kMaxTurns);
            var fireOilSeries = new List<float>(GameConfig.kMaxTurns);
            var armsSeries = new List<float>(GameConfig.kMaxTurns);

            int ashWillMin = int.MaxValue;
            int fireOilMin = int.MaxValue;
            int goldLeafMin = int.MaxValue;
            string notes = string.Empty;

            for (int turn = 1; turn <= GameConfig.kMaxTurns; turn++)
            {
                state.CurrentTurn = turn;
                ApplyTurnDynamics(state, j, difficulty, rng, turn);

                int goldLeaf = GetResourceAmount(state, GameIds.Resource.GoldLeaf);
                int fireOil = GetResourceAmount(state, GameIds.Resource.FireOil);
                int arms = GetResourceAmount(state, GameIds.Resource.Arms);
                int ashWill = GetResourceAmount(state, GameIds.Resource.AshWill);

                goldSeries.Add(goldLeaf);
                fireOilSeries.Add(fireOil);
                armsSeries.Add(arms);

                ashWillMin = Mathf.Min(ashWillMin, ashWill);
                fireOilMin = Mathf.Min(fireOilMin, fireOil);
                goldLeafMin = Mathf.Min(goldLeafMin, goldLeaf);

                events.TurnEnded(turn);
                if (j.IsGameEnded())
                {
                    break;
                }
            }

            if (!j.IsGameEnded())
            {
                state.CurrentTurn = GameConfig.kMaxTurns;
                events.TurnEnded(GameConfig.kMaxTurns);
            }

            int endTurn = Mathf.Clamp(state.CurrentTurn, 1, GameConfig.kMaxTurns);
            string reason = string.IsNullOrWhiteSpace(j.GetEndReason()) ? "unknown" : j.GetEndReason().Trim();
            string endType = ResolveEndType(reason);
            string victoryPath = endType == "victory" ? reason : string.Empty;
            float durationMinutes = EstimateDurationMinutes(endTurn, difficulty, rng);

            if (!j.IsGameEnded())
            {
                notes = "No endgame dispatch; forced timeout check path.";
            }

            var record = new SimulationRecord
            {
                BatchId = kBatchId,
                RunId = $"R3-02-{index:D6}",
                RunDate = runDate,
                BuildRef = kBuildRef,
                Difficulty = DifficultyToString(difficulty),
                Seed = seed,
                EndType = endType,
                EndReason = reason,
                VictoryPath = victoryPath,
                EndTurn = endTurn,
                DurationMinutes = durationMinutes,
                GoldLeafCv = ComputeCoefficientOfVariation(goldSeries),
                FireOilCv = ComputeCoefficientOfVariation(fireOilSeries),
                ArmsCv = ComputeCoefficientOfVariation(armsSeries),
                AshWillMin = ashWillMin == int.MaxValue ? GetResourceAmount(state, GameIds.Resource.AshWill) : ashWillMin,
                FireOilMin = fireOilMin == int.MaxValue ? GetResourceAmount(state, GameIds.Resource.FireOil) : fireOilMin,
                GoldLeafMin = goldLeafMin == int.MaxValue ? GetResourceAmount(state, GameIds.Resource.GoldLeaf) : goldLeafMin,
                Notes = notes
            };

            UnityEngine.Object.DestroyImmediate(jGo);
            UnityEngine.Object.DestroyImmediate(events);
            UnityEngine.Object.DestroyImmediate(state);
            UnityEngine.Object.DestroyImmediate(config);

            return record;
        }

        private static void ApplyDifficultyInitialization(GameState state, SimulationDifficulty difficulty, System.Random rng)
        {
            int goldLeaf = 0;
            int fireOil = 0;
            int arms = 0;
            int ashWill = 0;
            int social = 0;
            int tradeToken = 0;
            int tributeOrder = 0;

            switch (difficulty)
            {
                case SimulationDifficulty.Easy:
                    goldLeaf = NextInt(rng, 130, 190);
                    fireOil = NextInt(rng, 100, 150);
                    arms = NextInt(rng, 60, 95);
                    ashWill = NextInt(rng, 58, 80);
                    social = NextInt(rng, 45, 70);
                    tradeToken = NextInt(rng, 18, 35);
                    tributeOrder = NextInt(rng, 18, 40);
                    break;
                case SimulationDifficulty.Standard:
                    goldLeaf = NextInt(rng, 95, 155);
                    fireOil = NextInt(rng, 80, 125);
                    arms = NextInt(rng, 45, 80);
                    ashWill = NextInt(rng, 46, 66);
                    social = NextInt(rng, 34, 58);
                    tradeToken = NextInt(rng, 12, 26);
                    tributeOrder = NextInt(rng, 12, 30);
                    break;
                case SimulationDifficulty.Hard:
                    goldLeaf = NextInt(rng, 70, 125);
                    fireOil = NextInt(rng, 58, 105);
                    arms = NextInt(rng, 35, 68);
                    ashWill = NextInt(rng, 35, 56);
                    social = NextInt(rng, 24, 46);
                    tradeToken = NextInt(rng, 8, 20);
                    tributeOrder = NextInt(rng, 8, 24);
                    break;
            }

            SetResourceAmount(state, GameIds.Resource.GoldLeaf, goldLeaf);
            SetResourceAmount(state, GameIds.Resource.FireOil, fireOil);
            SetResourceAmount(state, GameIds.Resource.Arms, arms);
            SetResourceAmount(state, GameIds.Resource.AshWill, ashWill);
            SetResourceAmount(state, GameIds.Resource.SocialValue, social);
            SetResourceAmount(state, GameIds.Resource.TradeToken, tradeToken);
            SetResourceAmount(state, GameIds.Resource.TributeOrder, tributeOrder);
        }

        private static void ConfigureDifficultyThresholds(VictoryDefeatSystem j, SimulationDifficulty difficulty)
        {
            switch (difficulty)
            {
                case SimulationDifficulty.Easy:
                    j.VictoryThreshold = 72f;
                    j.MilitaryCollapseAshWillThreshold = 22;
                    j.InternalDivisionSatisfactionThreshold = 10;
                    break;
                case SimulationDifficulty.Standard:
                    j.VictoryThreshold = 80f;
                    j.MilitaryCollapseAshWillThreshold = 30;
                    j.InternalDivisionSatisfactionThreshold = 15;
                    break;
                case SimulationDifficulty.Hard:
                    j.VictoryThreshold = 88f;
                    j.MilitaryCollapseAshWillThreshold = 36;
                    j.InternalDivisionSatisfactionThreshold = 20;
                    break;
            }
        }

        private static void ApplyTurnDynamics(GameState state, VictoryDefeatSystem j, SimulationDifficulty difficulty, System.Random rng, int turn)
        {
            float bias = GetDifficultyBias(difficulty);
            float pressureSignal = 0.45f - bias + NextFloat(rng, -0.25f, 0.25f) + (turn / (float)GameConfig.kMaxTurns) * 0.15f;
            var blockadeLevel = ResolveBlockadeLevel(pressureSignal);
            j.SetBlockadeLevel(blockadeLevel);

            bool conflict = rng.NextDouble() < (0.35f - (bias * 0.45f) + (turn * 0.01f));
            j.SetLargeScaleConflictActive(conflict);
            if (conflict && rng.NextDouble() < 0.40f + (bias * 0.30f))
            {
                j.RecordEnemyKeyNodeLoss();
            }

            if (blockadeLevel >= BlockadeLevel.Multilateral && rng.NextDouble() < 0.25f)
            {
                j.RecordBlockadePostponement();
            }

            ApplyNodeControlDynamics(state, difficulty, rng, conflict);

            float routeBonus = ComputeRouteControlBonus(state);
            int goldDelta = Mathf.RoundToInt(14f + routeBonus * 8f + bias * 18f + NextFloat(rng, -22f, 22f) - GetGoldPenalty(blockadeLevel) - (conflict ? 6f : 0f));
            int fireOilDelta = Mathf.RoundToInt(5f + routeBonus * 4f + bias * 5f + NextFloat(rng, -9f, 9f) - GetFireOilPenalty(blockadeLevel) - (conflict ? 3f : 0f));
            int armsDelta = Mathf.RoundToInt(3f + bias * 4f + NextFloat(rng, -7f, 7f) + (conflict ? -4f : 2f));
            int ashDelta = Mathf.RoundToInt(NextFloat(rng, -6f, 6f) + (goldDelta >= 0 ? 2f : -3f) + (conflict ? -3f : 1f) + bias * 2f);
            int socialDelta = Mathf.RoundToInt(NextFloat(rng, -7f, 7f) + (blockadeLevel <= BlockadeLevel.Unilateral ? 2f : -2f) + (goldDelta >= 0 ? 1f : -1f) + bias * 1.5f);
            int tradeTokenDelta = Mathf.RoundToInt(NextFloat(rng, -5f, 6f) + (blockadeLevel <= BlockadeLevel.Unilateral ? 3f : -2f) + routeBonus * 3f);
            int tributeDelta = Mathf.RoundToInt(NextFloat(rng, -4f, 5f) + (blockadeLevel == BlockadeLevel.None ? 2f : 0f) + bias * 2f);

            ApplyResourceDelta(state, GameIds.Resource.GoldLeaf, goldDelta);
            ApplyResourceDelta(state, GameIds.Resource.FireOil, fireOilDelta);
            ApplyResourceDelta(state, GameIds.Resource.Arms, armsDelta);
            ApplyResourceDelta(state, GameIds.Resource.AshWill, ashDelta);
            ApplyResourceDelta(state, GameIds.Resource.SocialValue, socialDelta);
            ApplyResourceDelta(state, GameIds.Resource.TradeToken, tradeTokenDelta);
            ApplyResourceDelta(state, GameIds.Resource.TributeOrder, tributeDelta);

            int sacredFireDelta = Mathf.RoundToInt(NextFloat(rng, -5f, 5f) + bias * 2f + (blockadeLevel == BlockadeLevel.None ? 1f : -1f));
            int aureanDelta = Mathf.RoundToInt(NextFloat(rng, -4f, 4f) - bias * 2f - (conflict ? 2f : 0f));
            ApplyRelationDelta(state, GameIds.Faction.SacredFire, sacredFireDelta);
            ApplyRelationDelta(state, GameIds.Faction.Aurean, aureanDelta);

            ApplySatisfactionDynamics(state, difficulty, rng, conflict, blockadeLevel);
        }

        private static void ApplyNodeControlDynamics(GameState state, SimulationDifficulty difficulty, System.Random rng, bool conflict)
        {
            float captureChance = 0.2f;
            float lossChance = 0.1f;
            float medCaptureChance = 0.1f;

            switch (difficulty)
            {
                case SimulationDifficulty.Easy:
                    captureChance = 0.28f;
                    lossChance = 0.08f;
                    medCaptureChance = 0.16f;
                    break;
                case SimulationDifficulty.Standard:
                    captureChance = 0.20f;
                    lossChance = 0.12f;
                    medCaptureChance = 0.11f;
                    break;
                case SimulationDifficulty.Hard:
                    captureChance = 0.13f;
                    lossChance = 0.18f;
                    medCaptureChance = 0.06f;
                    break;
            }

            if (!conflict)
            {
                captureChance *= 0.4f;
                lossChance *= 0.5f;
                medCaptureChance *= 0.5f;
            }

            ResolveNodeContest(state, rng, GameIds.Node.IraqBorder, captureChance, lossChance, GameIds.Faction.Aurean);
            ResolveNodeContest(state, rng, GameIds.Node.SyriaZone, captureChance, lossChance, GameIds.Faction.Aurean);
            ResolveNodeContest(state, rng, GameIds.Node.Mediterranean, medCaptureChance, lossChance * 1.1f, GameIds.Faction.SacredFire);
        }

        private static void ResolveNodeContest(GameState state, System.Random rng, string nodeId, float captureChance, float lossChance, string fallbackEnemyFaction)
        {
            var node = state.GetNode(nodeId);
            if (node == null)
            {
                return;
            }

            float roll = (float)rng.NextDouble();
            if (roll < captureChance)
            {
                node.ControllingFactionId = rng.NextDouble() < 0.55 ? GameIds.Faction.AshConfederacy : GameIds.Faction.Vashid;
                node.ControlPoints = Mathf.Clamp(node.ControlPoints + NextInt(rng, 8, 22), 0, node.MaxControlPoints);
            }
            else if (roll < captureChance + lossChance)
            {
                node.ControllingFactionId = fallbackEnemyFaction;
                node.ControlPoints = Mathf.Clamp(node.ControlPoints - NextInt(rng, 8, 22), 0, node.MaxControlPoints);
            }
            else
            {
                int drift = NextInt(rng, -6, 6);
                node.ControlPoints = Mathf.Clamp(node.ControlPoints + drift, 0, node.MaxControlPoints);
            }
        }

        private static void ApplySatisfactionDynamics(GameState state, SimulationDifficulty difficulty, System.Random rng, bool conflict, BlockadeLevel blockadeLevel)
        {
            float bias = GetDifficultyBias(difficulty);
            int ashWill = GetResourceAmount(state, GameIds.Resource.AshWill);
            int socialValue = GetResourceAmount(state, GameIds.Resource.SocialValue);
            int pressurePenalty = 0;
            if (blockadeLevel >= BlockadeLevel.Multilateral) pressurePenalty += 2;
            if (conflict) pressurePenalty += 2;

            for (int i = 0; i < state.Factions.Length; i++)
            {
                var faction = state.Factions[i];
                if (faction == null)
                {
                    continue;
                }

                int adjustment = Mathf.RoundToInt(
                    NextFloat(rng, -4f, 4f)
                    + bias * 2f
                    - pressurePenalty
                    + (ashWill - 50) / 25f
                    + (socialValue - 50) / 30f);

                faction.Satisfaction = Mathf.Clamp(faction.Satisfaction + adjustment, 0, 100);
            }
        }

        private static float GetDifficultyBias(SimulationDifficulty difficulty)
        {
            switch (difficulty)
            {
                case SimulationDifficulty.Easy: return 0.18f;
                case SimulationDifficulty.Hard: return -0.18f;
                default: return 0f;
            }
        }

        private static BlockadeLevel ResolveBlockadeLevel(float pressureSignal)
        {
            if (pressureSignal < 0.25f) return BlockadeLevel.None;
            if (pressureSignal < 0.55f) return BlockadeLevel.Unilateral;
            if (pressureSignal < 0.85f) return BlockadeLevel.Multilateral;
            return BlockadeLevel.Total;
        }

        private static float GetGoldPenalty(BlockadeLevel level)
        {
            switch (level)
            {
                case BlockadeLevel.None: return 0f;
                case BlockadeLevel.Unilateral: return 8f;
                case BlockadeLevel.Multilateral: return 20f;
                case BlockadeLevel.Total: return 32f;
                default: return 0f;
            }
        }

        private static float GetFireOilPenalty(BlockadeLevel level)
        {
            switch (level)
            {
                case BlockadeLevel.None: return 0f;
                case BlockadeLevel.Unilateral: return 2f;
                case BlockadeLevel.Multilateral: return 5f;
                case BlockadeLevel.Total: return 8f;
                default: return 0f;
            }
        }

        private static float ComputeRouteControlBonus(GameState state)
        {
            int controlled = 0;
            controlled += IsNodeControlledByPlayerAxis(state, GameIds.Node.Hormuz) ? 1 : 0;
            controlled += IsNodeControlledByPlayerAxis(state, GameIds.Node.Bushehr) ? 1 : 0;
            controlled += IsNodeControlledByPlayerAxis(state, GameIds.Node.Caspian) ? 1 : 0;
            controlled += IsNodeControlledByPlayerAxis(state, GameIds.Node.TradeHub) ? 1 : 0;
            return controlled / 4f;
        }

        private static bool IsNodeControlledByPlayerAxis(GameState state, string nodeId)
        {
            var node = state.GetNode(nodeId);
            if (node == null)
            {
                return false;
            }

            string factionId = GameIds.ResolveFactionId(node.ControllingFactionId);
            return factionId == GameIds.Faction.Vashid || factionId == GameIds.Faction.AshConfederacy;
        }

        private static void ApplyRelationDelta(GameState state, string factionId, int delta)
        {
            var faction = state.GetFaction(factionId);
            if (faction == null)
            {
                return;
            }
            faction.RelationshipWithPlayer = Mathf.Clamp(faction.RelationshipWithPlayer + delta, -100, 100);
        }

        private static void ApplyResourceDelta(GameState state, string resourceId, int delta)
        {
            var resource = state.GetResource(resourceId);
            if (resource == null)
            {
                return;
            }

            int next = resource.Amount + delta;
            if (resource.ResourceType == ResourceType.Ratio)
            {
                resource.Amount = Mathf.Clamp(next, 0, 100);
            }
            else
            {
                resource.Amount = Mathf.Clamp(next, 0, resource.MaxCapacity);
            }
        }

        private static int GetResourceAmount(GameState state, string resourceId)
        {
            var resource = state.GetResource(resourceId);
            return resource != null ? resource.Amount : 0;
        }

        private static void SetResourceAmount(GameState state, string resourceId, int amount)
        {
            var resource = state.GetResource(resourceId);
            if (resource == null)
            {
                return;
            }

            if (resource.ResourceType == ResourceType.Ratio)
            {
                resource.Amount = Mathf.Clamp(amount, 0, 100);
            }
            else
            {
                resource.Amount = Mathf.Clamp(amount, 0, resource.MaxCapacity);
            }
        }

        private static string ResolveEndType(string reason)
        {
            string normalized = string.IsNullOrWhiteSpace(reason) ? string.Empty : reason.Trim().ToLowerInvariant();
            if (normalized == "attrition")
            {
                return "timeout";
            }

            if (normalized == "military_collapse" || normalized == "economic_collapse" || normalized == "internal_division")
            {
                return "defeat";
            }

            if (normalized.StartsWith("faction_crisis", StringComparison.Ordinal))
            {
                return "defeat";
            }

            return "victory";
        }

        private static float EstimateDurationMinutes(int endTurn, SimulationDifficulty difficulty, System.Random rng)
        {
            float basePerTurn = 26f;
            switch (difficulty)
            {
                case SimulationDifficulty.Easy: basePerTurn = 22f; break;
                case SimulationDifficulty.Hard: basePerTurn = 30f; break;
            }

            float jitter = NextFloat(rng, -3f, 3f);
            return Mathf.Max(30f, endTurn * (basePerTurn + jitter));
        }

        private static float ComputeCoefficientOfVariation(List<float> values)
        {
            if (values == null || values.Count == 0)
            {
                return 0f;
            }

            double mean = values.Average(v => (double)v);
            if (Math.Abs(mean) <= 0.0001)
            {
                return 0f;
            }

            double variance = values.Sum(v =>
            {
                double diff = v - mean;
                return diff * diff;
            }) / values.Count;

            double std = Math.Sqrt(variance);
            return (float)(std / Math.Abs(mean));
        }

        private static void WriteSimulationCsv(List<SimulationRecord> records, string csvPath)
        {
            var sb = new StringBuilder();
            sb.AppendLine("batch_id,run_id,run_date,build_ref,difficulty,seed,end_type,end_reason,victory_path,end_turn,duration_minutes,goldleaf_cv,fireoil_cv,arms_cv,ashwill_min,fireoil_min,goldleaf_min,notes");

            foreach (var r in records)
            {
                sb
                    .Append(Csv(r.BatchId)).Append(',')
                    .Append(Csv(r.RunId)).Append(',')
                    .Append(Csv(r.RunDate)).Append(',')
                    .Append(Csv(r.BuildRef)).Append(',')
                    .Append(Csv(r.Difficulty)).Append(',')
                    .Append(r.Seed.ToString(CultureInfo.InvariantCulture)).Append(',')
                    .Append(Csv(r.EndType)).Append(',')
                    .Append(Csv(r.EndReason)).Append(',')
                    .Append(Csv(r.VictoryPath)).Append(',')
                    .Append(r.EndTurn.ToString(CultureInfo.InvariantCulture)).Append(',')
                    .Append(r.DurationMinutes.ToString("0.00", CultureInfo.InvariantCulture)).Append(',')
                    .Append(r.GoldLeafCv.ToString("0.0000", CultureInfo.InvariantCulture)).Append(',')
                    .Append(r.FireOilCv.ToString("0.0000", CultureInfo.InvariantCulture)).Append(',')
                    .Append(r.ArmsCv.ToString("0.0000", CultureInfo.InvariantCulture)).Append(',')
                    .Append(r.AshWillMin.ToString(CultureInfo.InvariantCulture)).Append(',')
                    .Append(r.FireOilMin.ToString(CultureInfo.InvariantCulture)).Append(',')
                    .Append(r.GoldLeafMin.ToString(CultureInfo.InvariantCulture)).Append(',')
                    .Append(Csv(r.Notes))
                    .AppendLine();
            }

            File.WriteAllText(csvPath, sb.ToString(), new UTF8Encoding(false));
        }

        private static void WriteSummaryMarkdown(List<SimulationRecord> records, string summaryPath, string csvPath)
        {
            double totalRuns = records.Count;
            var easy = records.Where(r => r.Difficulty == "easy").ToList();
            var standard = records.Where(r => r.Difficulty == "standard").ToList();
            var hard = records.Where(r => r.Difficulty == "hard").ToList();

            double overallWinRate = records.Count(r => r.EndType == "victory") / totalRuns;
            double easyWinRate = easy.Count > 0 ? easy.Count(r => r.EndType == "victory") / (double)easy.Count : 0d;
            double standardWinRate = standard.Count > 0 ? standard.Count(r => r.EndType == "victory") / (double)standard.Count : 0d;
            double hardWinRate = hard.Count > 0 ? hard.Count(r => r.EndType == "victory") / (double)hard.Count : 0d;

            double avgEndTurnOverall = records.Average(r => r.EndTurn);
            double avgEndTurnStandard = standard.Count > 0 ? standard.Average(r => r.EndTurn) : 0d;
            double attritionRate = records.Count(r => string.Equals(r.EndReason, "attrition", StringComparison.OrdinalIgnoreCase)) / totalRuns;

            double avgGoldCv = records.Average(r => r.GoldLeafCv);
            double avgFireOilCv = records.Average(r => r.FireOilCv);
            double avgArmsCv = records.Average(r => r.ArmsCv);

            double victoryShare = records.Count(r => r.EndType == "victory") / totalRuns;
            double defeatShare = records.Count(r => r.EndType == "defeat") / totalRuns;

            var victoryRecords = records.Where(r => r.EndType == "victory").ToList();
            double maxSinglePathShare = 0d;
            if (victoryRecords.Count > 0)
            {
                maxSinglePathShare = victoryRecords
                    .GroupBy(r => string.IsNullOrWhiteSpace(r.VictoryPath) ? "unknown" : r.VictoryPath)
                    .Max(g => g.Count() / (double)victoryRecords.Count);
            }

            var sb = new StringBuilder();
            sb.AppendLine("# R3-02 KPI Evaluation (Run 001)");
            sb.AppendLine();
            sb.AppendLine($"- Batch: `{kBatchId}`");
            sb.AppendLine($"- Runs: `{records.Count}`");
            sb.AppendLine($"- Source CSV: `{csvPath}`");
            sb.AppendLine();
            sb.AppendLine("## KPI Snapshot");
            sb.AppendLine();
            sb.AppendLine("| KPI | Actual | Target | Status |");
            sb.AppendLine("|---|---:|---:|---|");
            AppendKpiRow(sb, "WIN_RATE_EASY", easyWinRate, 0.65, 0.80);
            AppendKpiRow(sb, "WIN_RATE_STANDARD", standardWinRate, 0.45, 0.55);
            AppendKpiRow(sb, "WIN_RATE_HARD", hardWinRate, 0.20, 0.35);
            AppendKpiRow(sb, "WIN_RATE_OVERALL", overallWinRate, 0.40, 0.60);
            AppendKpiRow(sb, "AVG_END_TURN_OVERALL", avgEndTurnOverall, 14.0, 20.0);
            AppendKpiRow(sb, "AVG_END_TURN_STANDARD", avgEndTurnStandard, 15.0, 19.0);
            AppendKpiRow(sb, "ATTRITION_RATE_OVERALL", attritionRate, 0.10, 0.35);
            AppendKpiRow(sb, "RESOURCE_CV_GOLDLEAF", avgGoldCv, 0.18, 0.45);
            AppendKpiRow(sb, "RESOURCE_CV_FIREOIL", avgFireOilCv, 0.15, 0.40);
            AppendKpiRow(sb, "RESOURCE_CV_ARMS", avgArmsCv, 0.20, 0.55);
            AppendKpiRow(sb, "VICTORY_SHARE_OVERALL", victoryShare, 0.35, 0.65);
            AppendKpiRow(sb, "DEFEAT_SHARE_OVERALL", defeatShare, 0.35, 0.65);
            AppendKpiRow(sb, "SINGLE_PATH_VICTORY_MONOPOLY", maxSinglePathShare, 0.00, 0.70);
            sb.AppendLine();
            sb.AppendLine("## Endgame Distribution");
            sb.AppendLine();
            sb.AppendLine("| End Type | Count | Share |");
            sb.AppendLine("|---|---:|---:|");
            AppendDistributionRow(sb, "victory", records.Count(r => r.EndType == "victory"), records.Count);
            AppendDistributionRow(sb, "defeat", records.Count(r => r.EndType == "defeat"), records.Count);
            AppendDistributionRow(sb, "timeout", records.Count(r => r.EndType == "timeout"), records.Count);
            sb.AppendLine();
            sb.AppendLine("## Victory Path Distribution");
            sb.AppendLine();
            sb.AppendLine("| Path | Count | Share in Victories |");
            sb.AppendLine("|---|---:|---:|");
            if (victoryRecords.Count == 0)
            {
                sb.AppendLine("| (none) | 0 | 0.0000 |");
            }
            else
            {
                foreach (var group in victoryRecords
                    .GroupBy(r => string.IsNullOrWhiteSpace(r.VictoryPath) ? "unknown" : r.VictoryPath)
                    .OrderByDescending(g => g.Count()))
                {
                    double share = group.Count() / (double)victoryRecords.Count;
                    sb.AppendLine($"| {group.Key} | {group.Count()} | {share.ToString("0.0000", CultureInfo.InvariantCulture)} |");
                }
            }

            File.WriteAllText(summaryPath, sb.ToString(), new UTF8Encoding(false));
        }

        private static void WriteEvidenceReadme(string evidenceDir, string csvPath, string summaryPath)
        {
            string readmePath = Path.Combine(evidenceDir, "README.md");
            string csvFile = Path.GetFileName(csvPath);
            string summaryFile = Path.GetFileName(summaryPath);

            var sb = new StringBuilder();
            sb.AppendLine("# R3 Simulation Evidence");
            sb.AppendLine();
            sb.AppendLine("- Stage: R3 Balance and AI Tuning");
            sb.AppendLine("- Task: R3-02 1000-run simulation pipeline");
            sb.AppendLine("- Date: " + DateTime.Now.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
            sb.AppendLine();
            sb.AppendLine("## Evidence Index");
            sb.AppendLine();
            sb.AppendLine("| Evidence | Description | Path |");
            sb.AppendLine("|---|---|---|");
            sb.AppendLine($"| Simulation run 001 | 1000-run raw simulation result table | `production/evidence/r3/simulation/{csvFile}` |");
            sb.AppendLine($"| KPI evaluation 001 | Computed KPI snapshot against R3-01 baseline | `production/evidence/r3/simulation/{summaryFile}` |");

            File.WriteAllText(readmePath, sb.ToString(), new UTF8Encoding(false));
        }

        private static void AppendKpiRow(StringBuilder sb, string kpiId, double actual, double min, double max)
        {
            bool pass = actual >= min && actual <= max;
            string status = pass ? "PASS" : "FAIL";
            sb.AppendLine(
                $"| {kpiId} | {actual.ToString("0.0000", CultureInfo.InvariantCulture)} | " +
                $"[{min.ToString("0.0000", CultureInfo.InvariantCulture)}, {max.ToString("0.0000", CultureInfo.InvariantCulture)}] | {status} |");
        }

        private static void AppendDistributionRow(StringBuilder sb, string label, int count, int total)
        {
            double share = total > 0 ? count / (double)total : 0d;
            sb.AppendLine($"| {label} | {count} | {share.ToString("0.0000", CultureInfo.InvariantCulture)} |");
        }

        private static string GetEvidenceDirectory()
        {
            string projectRoot = GetProjectRoot();
            string path = Path.Combine(projectRoot, "production", "evidence", "r3", "simulation");
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

        private static int NextInt(System.Random rng, int minInclusive, int maxInclusive)
        {
            return rng.Next(minInclusive, maxInclusive + 1);
        }

        private static float NextFloat(System.Random rng, float minInclusive, float maxInclusive)
        {
            return minInclusive + (float)rng.NextDouble() * (maxInclusive - minInclusive);
        }

        private static string DifficultyToString(SimulationDifficulty difficulty)
        {
            switch (difficulty)
            {
                case SimulationDifficulty.Easy: return "easy";
                case SimulationDifficulty.Standard: return "standard";
                case SimulationDifficulty.Hard: return "hard";
                default: return "standard";
            }
        }

        private static string Csv(string value)
        {
            string safe = value ?? string.Empty;
            return $"\"{safe.Replace("\"", "\"\"")}\"";
        }
    }
}
