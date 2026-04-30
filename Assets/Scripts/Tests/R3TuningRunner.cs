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
    public static class R3TuningRunner
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

        private sealed class TuningProfile
        {
            public string RoundCode;
            public string BatchId;
            public string Label;

            public float EasyBias;
            public float StandardBias;
            public float HardBias;

            public float EasyInitScale;
            public float StandardInitScale;
            public float HardInitScale;
            public float SocialTradeInitScale;

            public float EasyVictoryThreshold;
            public float StandardVictoryThreshold;
            public float HardVictoryThreshold;

            public int EasyMilitaryCollapseThreshold;
            public int StandardMilitaryCollapseThreshold;
            public int HardMilitaryCollapseThreshold;

            public float PressureOffset;
            public float PressureTurnScale;
            public float ConflictBase;
            public float ConflictTurnScale;

            public float ResourceDeltaScale;
            public float ResourceNoiseScale;
            public float RelationDeltaScale;
            public float SatisfactionScale;

            public int EarlySuppressionTurns;
            public float EarlyPositiveGainScale;

            public float ShockChance;
            public int ShockMagnitude;

            public float CaptureChanceScale;
            public float LossChanceScale;
            public float NonConflictNodeScale;
            public float NodeDriftScale;

            public int LatePressureStartTurn;
            public float LatePressureTurnBonus;
            public float LateConflictTurnBonus;
            public float LateLossChanceBonus;
            public float LateCaptureChancePenalty;

            public float EasyDurationPerTurn;
            public float StandardDurationPerTurn;
            public float HardDurationPerTurn;
        }

        private sealed class KpiMetric
        {
            public string Id;
            public double Actual;
            public double Min;
            public double Max;
            public bool Pass;
        }

        private sealed class KpiSnapshot
        {
            public string Label;
            public KpiMetric[] Metrics;
            public int PassCount;
            public int TotalCount;
            public int Runs;

            public double WinRateEasy;
            public double WinRateStandard;
            public double WinRateHard;
            public double WinRateOverall;
            public double AvgEndTurnOverall;
            public double AvgEndTurnStandard;
            public double AttritionRate;
            public double GoldCv;
            public double FireOilCv;
            public double ArmsCv;
            public double VictoryShare;
            public double DefeatShare;
            public double SinglePathMonopoly;

            public int VictoryCount;
            public int DefeatCount;
            public int TimeoutCount;
            public string TopVictoryPath;
            public int TopVictoryPathCount;
        }

        private sealed class RoundResult
        {
            public TuningProfile Profile;
            public List<SimulationRecord> Records;
            public KpiSnapshot Snapshot;
            public string CsvPath;
            public string KpiPath;
        }

        private const int kTotalRuns = 1000;
        private const int kEasyRuns = 300;
        private const int kStandardRuns = 400;
        private const int kHardRuns = 300;
        private const string kBuildRef = "workspace-head";

        public static void RunR304Iterations()
        {
            var profiles = BuildR304Profiles();
            var plan = BuildDifficultyPlan();
            string runDate = DateTime.Now.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            string dateToken = DateTime.Now.ToString("yyyyMMdd", CultureInfo.InvariantCulture);
            string evidenceDir = GetEvidenceDirectory();
            var results = new List<RoundResult>(profiles.Count);

            for (int roundIndex = 0; roundIndex < profiles.Count; roundIndex++)
            {
                var profile = profiles[roundIndex];
                var records = RunRound(profile, plan, runDate, roundIndex);
                var snapshot = EvaluateKpi(records, profile.Label);

                string csvPath = Path.Combine(evidenceDir, $"{dateToken}_R3-04_TUNING-{profile.RoundCode}.csv");
                string kpiPath = Path.Combine(evidenceDir, $"{dateToken}_R3-04_TUNING-{profile.RoundCode}-KPI.md");

                WriteSimulationCsv(records, csvPath);
                WriteRoundKpiMarkdown(records, snapshot, profile, kpiPath, csvPath);

                results.Add(new RoundResult
                {
                    Profile = profile,
                    Records = records,
                    Snapshot = snapshot,
                    CsvPath = csvPath,
                    KpiPath = kpiPath
                });

                Debug.Log($"[R3Tuning] {profile.RoundCode} complete. Pass={snapshot.PassCount}/{snapshot.TotalCount}");
            }

            string reportPath = Path.Combine(evidenceDir, $"{dateToken}_R3-04_TUNING-IMPACT-REPORT.md");
            WriteImpactReport(results, reportPath);
            WriteEvidenceReadme(evidenceDir, results, reportPath);

            Debug.Log($"[R3Tuning] Completed {results.Count} rounds.");
            Debug.Log($"[R3Tuning] Impact report: {reportPath}");
        }

        public static void RunR304TargetedRetuning()
        {
            var profiles = BuildR304TargetedProfiles();
            var plan = BuildDifficultyPlan();
            string runDate = DateTime.Now.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            string dateToken = DateTime.Now.ToString("yyyyMMdd", CultureInfo.InvariantCulture);
            string evidenceDir = GetEvidenceDirectory();
            var results = new List<RoundResult>(profiles.Count);

            for (int roundIndex = 0; roundIndex < profiles.Count; roundIndex++)
            {
                var profile = profiles[roundIndex];
                var records = RunRound(profile, plan, runDate, 40 + roundIndex);
                var snapshot = EvaluateKpi(records, profile.Label);

                string csvPath = Path.Combine(evidenceDir, $"{dateToken}_R3-04B_TARGETED-{profile.RoundCode}.csv");
                string kpiPath = Path.Combine(evidenceDir, $"{dateToken}_R3-04B_TARGETED-{profile.RoundCode}-KPI.md");

                WriteSimulationCsv(records, csvPath);
                WriteRoundKpiMarkdown(records, snapshot, profile, kpiPath, csvPath);

                results.Add(new RoundResult
                {
                    Profile = profile,
                    Records = records,
                    Snapshot = snapshot,
                    CsvPath = csvPath,
                    KpiPath = kpiPath
                });

                Debug.Log($"[R3Targeted] {profile.RoundCode} complete. Pass={snapshot.PassCount}/{snapshot.TotalCount}");
            }

            string reportPath = Path.Combine(evidenceDir, $"{dateToken}_R3-04B_TARGETED-IMPACT-REPORT.md");
            WriteImpactReport(results, reportPath);
            WriteEvidenceReadme(evidenceDir, results, reportPath);

            Debug.Log($"[R3Targeted] Completed {results.Count} rounds.");
            Debug.Log($"[R3Targeted] Impact report: {reportPath}");
        }

        public static void RunR304CRetuning()
        {
            var profiles = BuildR304CProfiles();
            var plan = BuildDifficultyPlan();
            string runDate = DateTime.Now.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            string dateToken = DateTime.Now.ToString("yyyyMMdd", CultureInfo.InvariantCulture);
            string evidenceDir = GetEvidenceDirectory();
            var results = new List<RoundResult>(profiles.Count);

            for (int roundIndex = 0; roundIndex < profiles.Count; roundIndex++)
            {
                var profile = profiles[roundIndex];
                var records = RunRound(profile, plan, runDate, 120 + roundIndex);
                var snapshot = EvaluateKpi(records, profile.Label);

                string csvPath = Path.Combine(evidenceDir, $"{dateToken}_R3-04C_TARGETED-{profile.RoundCode}.csv");
                string kpiPath = Path.Combine(evidenceDir, $"{dateToken}_R3-04C_TARGETED-{profile.RoundCode}-KPI.md");

                WriteSimulationCsv(records, csvPath);
                WriteRoundKpiMarkdown(records, snapshot, profile, kpiPath, csvPath);

                results.Add(new RoundResult
                {
                    Profile = profile,
                    Records = records,
                    Snapshot = snapshot,
                    CsvPath = csvPath,
                    KpiPath = kpiPath
                });

                Debug.Log($"[R3TargetedC] {profile.RoundCode} complete. Pass={snapshot.PassCount}/{snapshot.TotalCount}");
            }

            string reportPath = Path.Combine(evidenceDir, $"{dateToken}_R3-04C_TARGETED-IMPACT-REPORT.md");
            WriteImpactReport(results, reportPath);
            WriteEvidenceReadme(evidenceDir, results, reportPath);

            Debug.Log($"[R3TargetedC] Completed {results.Count} rounds.");
            Debug.Log($"[R3TargetedC] Impact report: {reportPath}");
        }

        public static void RunR304DRetuning()
        {
            var profiles = BuildR304DProfiles();
            var plan = BuildDifficultyPlan();
            string runDate = DateTime.Now.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            string dateToken = DateTime.Now.ToString("yyyyMMdd", CultureInfo.InvariantCulture);
            string evidenceDir = GetEvidenceDirectory();
            var results = new List<RoundResult>(profiles.Count);

            for (int roundIndex = 0; roundIndex < profiles.Count; roundIndex++)
            {
                var profile = profiles[roundIndex];
                var records = RunRound(profile, plan, runDate, 200 + roundIndex);
                var snapshot = EvaluateKpi(records, profile.Label);

                string csvPath = Path.Combine(evidenceDir, $"{dateToken}_R3-04D_TARGETED-{profile.RoundCode}.csv");
                string kpiPath = Path.Combine(evidenceDir, $"{dateToken}_R3-04D_TARGETED-{profile.RoundCode}-KPI.md");

                WriteSimulationCsv(records, csvPath);
                WriteRoundKpiMarkdown(records, snapshot, profile, kpiPath, csvPath);

                results.Add(new RoundResult
                {
                    Profile = profile,
                    Records = records,
                    Snapshot = snapshot,
                    CsvPath = csvPath,
                    KpiPath = kpiPath
                });

                Debug.Log($"[R3TargetedD] {profile.RoundCode} complete. Pass={snapshot.PassCount}/{snapshot.TotalCount}");
            }

            string reportPath = Path.Combine(evidenceDir, $"{dateToken}_R3-04D_TARGETED-IMPACT-REPORT.md");
            WriteImpactReport(results, reportPath);
            WriteEvidenceReadme(evidenceDir, results, reportPath);

            Debug.Log($"[R3TargetedD] Completed {results.Count} rounds.");
            Debug.Log($"[R3TargetedD] Impact report: {reportPath}");
        }

        public static void RunR304ERetuning()
        {
            var profiles = BuildR304EProfiles();
            var plan = BuildDifficultyPlan();
            string runDate = DateTime.Now.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            string dateToken = DateTime.Now.ToString("yyyyMMdd", CultureInfo.InvariantCulture);
            string evidenceDir = GetEvidenceDirectory();
            var results = new List<RoundResult>(profiles.Count);

            for (int roundIndex = 0; roundIndex < profiles.Count; roundIndex++)
            {
                var profile = profiles[roundIndex];
                var records = RunRound(profile, plan, runDate, 260 + roundIndex);
                var snapshot = EvaluateKpi(records, profile.Label);

                string csvPath = Path.Combine(evidenceDir, $"{dateToken}_R3-04E_TARGETED-{profile.RoundCode}.csv");
                string kpiPath = Path.Combine(evidenceDir, $"{dateToken}_R3-04E_TARGETED-{profile.RoundCode}-KPI.md");

                WriteSimulationCsv(records, csvPath);
                WriteRoundKpiMarkdown(records, snapshot, profile, kpiPath, csvPath);

                results.Add(new RoundResult
                {
                    Profile = profile,
                    Records = records,
                    Snapshot = snapshot,
                    CsvPath = csvPath,
                    KpiPath = kpiPath
                });

                Debug.Log($"[R3TargetedE] {profile.RoundCode} complete. Pass={snapshot.PassCount}/{snapshot.TotalCount}");
            }

            string reportPath = Path.Combine(evidenceDir, $"{dateToken}_R3-04E_TARGETED-IMPACT-REPORT.md");
            WriteImpactReport(results, reportPath);
            WriteEvidenceReadme(evidenceDir, results, reportPath);

            Debug.Log($"[R3TargetedE] Completed {results.Count} rounds.");
            Debug.Log($"[R3TargetedE] Impact report: {reportPath}");
        }

        public static void RunR304FRetuning()
        {
            var profiles = BuildR304FProfiles();
            var plan = BuildDifficultyPlan();
            string runDate = DateTime.Now.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            string dateToken = DateTime.Now.ToString("yyyyMMdd", CultureInfo.InvariantCulture);
            string evidenceDir = GetEvidenceDirectory();
            var results = new List<RoundResult>(profiles.Count);

            for (int roundIndex = 0; roundIndex < profiles.Count; roundIndex++)
            {
                var profile = profiles[roundIndex];
                var records = RunRound(profile, plan, runDate, 320 + roundIndex);
                var snapshot = EvaluateKpi(records, profile.Label);

                string csvPath = Path.Combine(evidenceDir, $"{dateToken}_R3-04F_TARGETED-{profile.RoundCode}.csv");
                string kpiPath = Path.Combine(evidenceDir, $"{dateToken}_R3-04F_TARGETED-{profile.RoundCode}-KPI.md");

                WriteSimulationCsv(records, csvPath);
                WriteRoundKpiMarkdown(records, snapshot, profile, kpiPath, csvPath);

                results.Add(new RoundResult
                {
                    Profile = profile,
                    Records = records,
                    Snapshot = snapshot,
                    CsvPath = csvPath,
                    KpiPath = kpiPath
                });

                Debug.Log($"[R3TargetedF] {profile.RoundCode} complete. Pass={snapshot.PassCount}/{snapshot.TotalCount}");
            }

            string reportPath = Path.Combine(evidenceDir, $"{dateToken}_R3-04F_TARGETED-IMPACT-REPORT.md");
            WriteImpactReport(results, reportPath);
            WriteEvidenceReadme(evidenceDir, results, reportPath);

            Debug.Log($"[R3TargetedF] Completed {results.Count} rounds.");
            Debug.Log($"[R3TargetedF] Impact report: {reportPath}");
        }

        public static void RunR304GRetuning()
        {
            var profiles = BuildR304GProfiles();
            var plan = BuildDifficultyPlan();
            string runDate = DateTime.Now.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            string dateToken = DateTime.Now.ToString("yyyyMMdd", CultureInfo.InvariantCulture);
            string evidenceDir = GetEvidenceDirectory();
            var results = new List<RoundResult>(profiles.Count);

            for (int roundIndex = 0; roundIndex < profiles.Count; roundIndex++)
            {
                var profile = profiles[roundIndex];
                var records = RunRound(profile, plan, runDate, 380 + roundIndex);
                var snapshot = EvaluateKpi(records, profile.Label);

                string csvPath = Path.Combine(evidenceDir, $"{dateToken}_R3-04G_TARGETED-{profile.RoundCode}.csv");
                string kpiPath = Path.Combine(evidenceDir, $"{dateToken}_R3-04G_TARGETED-{profile.RoundCode}-KPI.md");

                WriteSimulationCsv(records, csvPath);
                WriteRoundKpiMarkdown(records, snapshot, profile, kpiPath, csvPath);

                results.Add(new RoundResult
                {
                    Profile = profile,
                    Records = records,
                    Snapshot = snapshot,
                    CsvPath = csvPath,
                    KpiPath = kpiPath
                });

                Debug.Log($"[R3TargetedG] {profile.RoundCode} complete. Pass={snapshot.PassCount}/{snapshot.TotalCount}");
            }

            string reportPath = Path.Combine(evidenceDir, $"{dateToken}_R3-04G_TARGETED-IMPACT-REPORT.md");
            WriteImpactReport(results, reportPath);
            WriteEvidenceReadme(evidenceDir, results, reportPath);

            Debug.Log($"[R3TargetedG] Completed {results.Count} rounds.");
            Debug.Log($"[R3TargetedG] Impact report: {reportPath}");
        }

        public static void RunR304HRetuning()
        {
            var profiles = BuildR304HProfiles();
            var plan = BuildDifficultyPlan();
            string runDate = DateTime.Now.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            string dateToken = DateTime.Now.ToString("yyyyMMdd", CultureInfo.InvariantCulture);
            string evidenceDir = GetEvidenceDirectory();
            var results = new List<RoundResult>(profiles.Count);

            for (int roundIndex = 0; roundIndex < profiles.Count; roundIndex++)
            {
                var profile = profiles[roundIndex];
                // Keep a fixed seed window across H profiles so KPI deltas reflect profile changes, not seed drift.
                var records = RunRound(profile, plan, runDate, 440);
                var snapshot = EvaluateKpi(records, profile.Label);

                string csvPath = Path.Combine(evidenceDir, $"{dateToken}_R3-04H_TARGETED-{profile.RoundCode}.csv");
                string kpiPath = Path.Combine(evidenceDir, $"{dateToken}_R3-04H_TARGETED-{profile.RoundCode}-KPI.md");

                WriteSimulationCsv(records, csvPath);
                WriteRoundKpiMarkdown(records, snapshot, profile, kpiPath, csvPath);

                results.Add(new RoundResult
                {
                    Profile = profile,
                    Records = records,
                    Snapshot = snapshot,
                    CsvPath = csvPath,
                    KpiPath = kpiPath
                });

                Debug.Log($"[R3TargetedH] {profile.RoundCode} complete. Pass={snapshot.PassCount}/{snapshot.TotalCount}");
            }

            string reportPath = Path.Combine(evidenceDir, $"{dateToken}_R3-04H_TARGETED-IMPACT-REPORT.md");
            WriteImpactReport(results, reportPath);
            WriteEvidenceReadme(evidenceDir, results, reportPath);

            Debug.Log($"[R3TargetedH] Completed {results.Count} rounds.");
            Debug.Log($"[R3TargetedH] Impact report: {reportPath}");
        }

        public static void RunR304IRetuning()
        {
            var profiles = BuildR304IProfiles();
            var plan = BuildDifficultyPlan();
            string runDate = DateTime.Now.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            string dateToken = DateTime.Now.ToString("yyyyMMdd", CultureInfo.InvariantCulture);
            string evidenceDir = GetEvidenceDirectory();
            var results = new List<RoundResult>(profiles.Count);

            for (int roundIndex = 0; roundIndex < profiles.Count; roundIndex++)
            {
                var profile = profiles[roundIndex];
                // Keep a fixed seed window across I profiles to compare stability-oriented deltas.
                var records = RunRound(profile, plan, runDate, 500);
                var snapshot = EvaluateKpi(records, profile.Label);

                string csvPath = Path.Combine(evidenceDir, $"{dateToken}_R3-04I_TARGETED-{profile.RoundCode}.csv");
                string kpiPath = Path.Combine(evidenceDir, $"{dateToken}_R3-04I_TARGETED-{profile.RoundCode}-KPI.md");

                WriteSimulationCsv(records, csvPath);
                WriteRoundKpiMarkdown(records, snapshot, profile, kpiPath, csvPath);

                results.Add(new RoundResult
                {
                    Profile = profile,
                    Records = records,
                    Snapshot = snapshot,
                    CsvPath = csvPath,
                    KpiPath = kpiPath
                });

                Debug.Log($"[R3TargetedI] {profile.RoundCode} complete. Pass={snapshot.PassCount}/{snapshot.TotalCount}");
            }

            string reportPath = Path.Combine(evidenceDir, $"{dateToken}_R3-04I_TARGETED-IMPACT-REPORT.md");
            WriteImpactReport(results, reportPath);
            WriteEvidenceReadme(evidenceDir, results, reportPath);

            Debug.Log($"[R3TargetedI] Completed {results.Count} rounds.");
            Debug.Log($"[R3TargetedI] Impact report: {reportPath}");
        }

        public static void RunR304JRetuning()
        {
            var profiles = BuildR304JProfiles();
            var plan = BuildDifficultyPlan();
            string runDate = DateTime.Now.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            string dateToken = DateTime.Now.ToString("yyyyMMdd", CultureInfo.InvariantCulture);
            string evidenceDir = GetEvidenceDirectory();
            var results = new List<RoundResult>(profiles.Count);

            for (int roundIndex = 0; roundIndex < profiles.Count; roundIndex++)
            {
                var profile = profiles[roundIndex];
                // Keep fixed seed window across J profiles for comparable stability deltas.
                var records = RunRound(profile, plan, runDate, 620);
                var snapshot = EvaluateKpi(records, profile.Label);

                string csvPath = Path.Combine(evidenceDir, $"{dateToken}_R3-04J_TARGETED-{profile.RoundCode}.csv");
                string kpiPath = Path.Combine(evidenceDir, $"{dateToken}_R3-04J_TARGETED-{profile.RoundCode}-KPI.md");

                WriteSimulationCsv(records, csvPath);
                WriteRoundKpiMarkdown(records, snapshot, profile, kpiPath, csvPath);

                results.Add(new RoundResult
                {
                    Profile = profile,
                    Records = records,
                    Snapshot = snapshot,
                    CsvPath = csvPath,
                    KpiPath = kpiPath
                });

                Debug.Log($"[R3TargetedJ] {profile.RoundCode} complete. Pass={snapshot.PassCount}/{snapshot.TotalCount}");
            }

            string reportPath = Path.Combine(evidenceDir, $"{dateToken}_R3-04J_TARGETED-IMPACT-REPORT.md");
            WriteImpactReport(results, reportPath);
            WriteEvidenceReadme(evidenceDir, results, reportPath);

            Debug.Log($"[R3TargetedJ] Completed {results.Count} rounds.");
            Debug.Log($"[R3TargetedJ] Impact report: {reportPath}");
        }

        public static void RunR304KRetuning()
        {
            var profiles = BuildR304KProfiles();
            var plan = BuildDifficultyPlan();
            string runDate = DateTime.Now.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            string dateToken = DateTime.Now.ToString("yyyyMMdd", CultureInfo.InvariantCulture);
            string evidenceDir = GetEvidenceDirectory();
            var results = new List<RoundResult>(profiles.Count);

            for (int roundIndex = 0; roundIndex < profiles.Count; roundIndex++)
            {
                var profile = profiles[roundIndex];
                // Keep fixed seed window across K profiles for comparable residual-gap deltas.
                var records = RunRound(profile, plan, runDate, 700);
                var snapshot = EvaluateKpi(records, profile.Label);

                string csvPath = Path.Combine(evidenceDir, $"{dateToken}_R3-04K_TARGETED-{profile.RoundCode}.csv");
                string kpiPath = Path.Combine(evidenceDir, $"{dateToken}_R3-04K_TARGETED-{profile.RoundCode}-KPI.md");

                WriteSimulationCsv(records, csvPath);
                WriteRoundKpiMarkdown(records, snapshot, profile, kpiPath, csvPath);

                results.Add(new RoundResult
                {
                    Profile = profile,
                    Records = records,
                    Snapshot = snapshot,
                    CsvPath = csvPath,
                    KpiPath = kpiPath
                });

                Debug.Log($"[R3TargetedK] {profile.RoundCode} complete. Pass={snapshot.PassCount}/{snapshot.TotalCount}");
            }

            string reportPath = Path.Combine(evidenceDir, $"{dateToken}_R3-04K_TARGETED-IMPACT-REPORT.md");
            WriteImpactReport(results, reportPath);
            WriteEvidenceReadme(evidenceDir, results, reportPath);

            Debug.Log($"[R3TargetedK] Completed {results.Count} rounds.");
            Debug.Log($"[R3TargetedK] Impact report: {reportPath}");
        }

        public static void RunR304LRetuning()
        {
            var profiles = BuildR304LProfiles();
            var plan = BuildDifficultyPlan();
            string runDate = DateTime.Now.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            string dateToken = DateTime.Now.ToString("yyyyMMdd", CultureInfo.InvariantCulture);
            string evidenceDir = GetEvidenceDirectory();
            var results = new List<RoundResult>(profiles.Count);

            for (int roundIndex = 0; roundIndex < profiles.Count; roundIndex++)
            {
                var profile = profiles[roundIndex];
                // Keep fixed seed window across L profiles for strict residual-gap comparison.
                var records = RunRound(profile, plan, runDate, 760);
                var snapshot = EvaluateKpi(records, profile.Label);

                string csvPath = Path.Combine(evidenceDir, $"{dateToken}_R3-04L_TARGETED-{profile.RoundCode}.csv");
                string kpiPath = Path.Combine(evidenceDir, $"{dateToken}_R3-04L_TARGETED-{profile.RoundCode}-KPI.md");

                WriteSimulationCsv(records, csvPath);
                WriteRoundKpiMarkdown(records, snapshot, profile, kpiPath, csvPath);

                results.Add(new RoundResult
                {
                    Profile = profile,
                    Records = records,
                    Snapshot = snapshot,
                    CsvPath = csvPath,
                    KpiPath = kpiPath
                });

                Debug.Log($"[R3TargetedL] {profile.RoundCode} complete. Pass={snapshot.PassCount}/{snapshot.TotalCount}");
            }

            string reportPath = Path.Combine(evidenceDir, $"{dateToken}_R3-04L_TARGETED-IMPACT-REPORT.md");
            WriteImpactReport(results, reportPath);
            WriteEvidenceReadme(evidenceDir, results, reportPath);

            Debug.Log($"[R3TargetedL] Completed {results.Count} rounds.");
            Debug.Log($"[R3TargetedL] Impact report: {reportPath}");
        }

        public static void RunR304MRetuning()
        {
            var profiles = BuildR304MProfiles();
            var plan = BuildDifficultyPlan();
            string runDate = DateTime.Now.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            string dateToken = DateTime.Now.ToString("yyyyMMdd", CultureInfo.InvariantCulture);
            string evidenceDir = GetEvidenceDirectory();
            var results = new List<RoundResult>(profiles.Count);

            for (int roundIndex = 0; roundIndex < profiles.Count; roundIndex++)
            {
                var profile = profiles[roundIndex];
                // Keep fixed seed window across M profiles for stable cross-round comparison.
                var records = RunRound(profile, plan, runDate, 820);
                var snapshot = EvaluateKpi(records, profile.Label);

                string csvPath = Path.Combine(evidenceDir, $"{dateToken}_R3-04M_TARGETED-{profile.RoundCode}.csv");
                string kpiPath = Path.Combine(evidenceDir, $"{dateToken}_R3-04M_TARGETED-{profile.RoundCode}-KPI.md");

                WriteSimulationCsv(records, csvPath);
                WriteRoundKpiMarkdown(records, snapshot, profile, kpiPath, csvPath);

                results.Add(new RoundResult
                {
                    Profile = profile,
                    Records = records,
                    Snapshot = snapshot,
                    CsvPath = csvPath,
                    KpiPath = kpiPath
                });

                Debug.Log($"[R3TargetedM] {profile.RoundCode} complete. Pass={snapshot.PassCount}/{snapshot.TotalCount}");
            }

            string reportPath = Path.Combine(evidenceDir, $"{dateToken}_R3-04M_TARGETED-IMPACT-REPORT.md");
            WriteImpactReport(results, reportPath);
            WriteEvidenceReadme(evidenceDir, results, reportPath);

            Debug.Log($"[R3TargetedM] Completed {results.Count} rounds.");
            Debug.Log($"[R3TargetedM] Impact report: {reportPath}");
        }

        public static void RunR304NRetuning()
        {
            var profiles = BuildR304NProfiles();
            var plan = BuildDifficultyPlan();
            string runDate = DateTime.Now.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            string dateToken = DateTime.Now.ToString("yyyyMMdd", CultureInfo.InvariantCulture);
            string evidenceDir = GetEvidenceDirectory();
            var results = new List<RoundResult>(profiles.Count);

            for (int roundIndex = 0; roundIndex < profiles.Count; roundIndex++)
            {
                var profile = profiles[roundIndex];
                // Keep fixed seed window across N profiles for regression-gap closure comparison.
                var records = RunRound(profile, plan, runDate, 880);
                var snapshot = EvaluateKpi(records, profile.Label);

                string csvPath = Path.Combine(evidenceDir, $"{dateToken}_R3-04N_TARGETED-{profile.RoundCode}.csv");
                string kpiPath = Path.Combine(evidenceDir, $"{dateToken}_R3-04N_TARGETED-{profile.RoundCode}-KPI.md");

                WriteSimulationCsv(records, csvPath);
                WriteRoundKpiMarkdown(records, snapshot, profile, kpiPath, csvPath);

                results.Add(new RoundResult
                {
                    Profile = profile,
                    Records = records,
                    Snapshot = snapshot,
                    CsvPath = csvPath,
                    KpiPath = kpiPath
                });

                Debug.Log($"[R3TargetedN] {profile.RoundCode} complete. Pass={snapshot.PassCount}/{snapshot.TotalCount}");
            }

            string reportPath = Path.Combine(evidenceDir, $"{dateToken}_R3-04N_TARGETED-IMPACT-REPORT.md");
            WriteImpactReport(results, reportPath);
            WriteEvidenceReadme(evidenceDir, results, reportPath);

            Debug.Log($"[R3TargetedN] Completed {results.Count} rounds.");
            Debug.Log($"[R3TargetedN] Impact report: {reportPath}");
        }

        public static void RunR304ORetuning()
        {
            var profiles = BuildR304OProfiles();
            var plan = BuildDifficultyPlan();
            string runDate = DateTime.Now.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            string dateToken = DateTime.Now.ToString("yyyyMMdd", CultureInfo.InvariantCulture);
            string evidenceDir = GetEvidenceDirectory();
            var results = new List<RoundResult>(profiles.Count);

            for (int roundIndex = 0; roundIndex < profiles.Count; roundIndex++)
            {
                var profile = profiles[roundIndex];
                // Keep fixed seed window across O profiles for R3-05 gap-focused comparison.
                var records = RunRound(profile, plan, runDate, 940);
                var snapshot = EvaluateKpi(records, profile.Label);

                string csvPath = Path.Combine(evidenceDir, $"{dateToken}_R3-04O_TARGETED-{profile.RoundCode}.csv");
                string kpiPath = Path.Combine(evidenceDir, $"{dateToken}_R3-04O_TARGETED-{profile.RoundCode}-KPI.md");

                WriteSimulationCsv(records, csvPath);
                WriteRoundKpiMarkdown(records, snapshot, profile, kpiPath, csvPath);

                results.Add(new RoundResult
                {
                    Profile = profile,
                    Records = records,
                    Snapshot = snapshot,
                    CsvPath = csvPath,
                    KpiPath = kpiPath
                });

                Debug.Log($"[R3TargetedO] {profile.RoundCode} complete. Pass={snapshot.PassCount}/{snapshot.TotalCount}");
            }

            string reportPath = Path.Combine(evidenceDir, $"{dateToken}_R3-04O_TARGETED-IMPACT-REPORT.md");
            WriteImpactReport(results, reportPath);
            WriteEvidenceReadme(evidenceDir, results, reportPath);

            Debug.Log($"[R3TargetedO] Completed {results.Count} rounds.");
            Debug.Log($"[R3TargetedO] Impact report: {reportPath}");
        }

        public static void RunR304PRetuning()
        {
            var profiles = BuildR304PProfiles();
            var plan = BuildDifficultyPlan();
            string runDate = DateTime.Now.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            string dateToken = DateTime.Now.ToString("yyyyMMdd", CultureInfo.InvariantCulture);
            string evidenceDir = GetEvidenceDirectory();
            var results = new List<RoundResult>(profiles.Count);

            for (int roundIndex = 0; roundIndex < profiles.Count; roundIndex++)
            {
                var profile = profiles[roundIndex];
                // Keep fixed seed window across P profiles for late-pressure gap closure comparison.
                var records = RunRound(profile, plan, runDate, 1000);
                var snapshot = EvaluateKpi(records, profile.Label);

                string csvPath = Path.Combine(evidenceDir, $"{dateToken}_R3-04P_TARGETED-{profile.RoundCode}.csv");
                string kpiPath = Path.Combine(evidenceDir, $"{dateToken}_R3-04P_TARGETED-{profile.RoundCode}-KPI.md");

                WriteSimulationCsv(records, csvPath);
                WriteRoundKpiMarkdown(records, snapshot, profile, kpiPath, csvPath);

                results.Add(new RoundResult
                {
                    Profile = profile,
                    Records = records,
                    Snapshot = snapshot,
                    CsvPath = csvPath,
                    KpiPath = kpiPath
                });

                Debug.Log($"[R3TargetedP] {profile.RoundCode} complete. Pass={snapshot.PassCount}/{snapshot.TotalCount}");
            }

            string reportPath = Path.Combine(evidenceDir, $"{dateToken}_R3-04P_TARGETED-IMPACT-REPORT.md");
            WriteImpactReport(results, reportPath);
            WriteEvidenceReadme(evidenceDir, results, reportPath);

            Debug.Log($"[R3TargetedP] Completed {results.Count} rounds.");
            Debug.Log($"[R3TargetedP] Impact report: {reportPath}");
        }

        public static void RunR304QRetuning()
        {
            var profiles = BuildR304QProfiles();
            var plan = BuildDifficultyPlan();
            string runDate = DateTime.Now.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            string dateToken = DateTime.Now.ToString("yyyyMMdd", CultureInfo.InvariantCulture);
            string evidenceDir = GetEvidenceDirectory();
            var results = new List<RoundResult>(profiles.Count);

            for (int roundIndex = 0; roundIndex < profiles.Count; roundIndex++)
            {
                var profile = profiles[roundIndex];
                // Keep fixed seed window across Q profiles for dual-gap closure comparison.
                var records = RunRound(profile, plan, runDate, 1060);
                var snapshot = EvaluateKpi(records, profile.Label);

                string csvPath = Path.Combine(evidenceDir, $"{dateToken}_R3-04Q_TARGETED-{profile.RoundCode}.csv");
                string kpiPath = Path.Combine(evidenceDir, $"{dateToken}_R3-04Q_TARGETED-{profile.RoundCode}-KPI.md");

                WriteSimulationCsv(records, csvPath);
                WriteRoundKpiMarkdown(records, snapshot, profile, kpiPath, csvPath);

                results.Add(new RoundResult
                {
                    Profile = profile,
                    Records = records,
                    Snapshot = snapshot,
                    CsvPath = csvPath,
                    KpiPath = kpiPath
                });

                Debug.Log($"[R3TargetedQ] {profile.RoundCode} complete. Pass={snapshot.PassCount}/{snapshot.TotalCount}");
            }

            string reportPath = Path.Combine(evidenceDir, $"{dateToken}_R3-04Q_TARGETED-IMPACT-REPORT.md");
            WriteImpactReport(results, reportPath);
            WriteEvidenceReadme(evidenceDir, results, reportPath);

            Debug.Log($"[R3TargetedQ] Completed {results.Count} rounds.");
            Debug.Log($"[R3TargetedQ] Impact report: {reportPath}");
        }

        public static void RunR304RRetuning()
        {
            var profiles = BuildR304RProfiles();
            var plan = BuildDifficultyPlan();
            string runDate = DateTime.Now.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            string dateToken = DateTime.Now.ToString("yyyyMMdd", CultureInfo.InvariantCulture);
            string evidenceDir = GetEvidenceDirectory();
            var results = new List<RoundResult>(profiles.Count);

            for (int roundIndex = 0; roundIndex < profiles.Count; roundIndex++)
            {
                var profile = profiles[roundIndex];
                // Keep fixed seed window across R profiles for standard-win-rate micro lift comparison.
                var records = RunRound(profile, plan, runDate, 1120);
                var snapshot = EvaluateKpi(records, profile.Label);

                string csvPath = Path.Combine(evidenceDir, $"{dateToken}_R3-04R_TARGETED-{profile.RoundCode}.csv");
                string kpiPath = Path.Combine(evidenceDir, $"{dateToken}_R3-04R_TARGETED-{profile.RoundCode}-KPI.md");

                WriteSimulationCsv(records, csvPath);
                WriteRoundKpiMarkdown(records, snapshot, profile, kpiPath, csvPath);

                results.Add(new RoundResult
                {
                    Profile = profile,
                    Records = records,
                    Snapshot = snapshot,
                    CsvPath = csvPath,
                    KpiPath = kpiPath
                });

                Debug.Log($"[R3TargetedR] {profile.RoundCode} complete. Pass={snapshot.PassCount}/{snapshot.TotalCount}");
            }

            string reportPath = Path.Combine(evidenceDir, $"{dateToken}_R3-04R_TARGETED-IMPACT-REPORT.md");
            WriteImpactReport(results, reportPath);
            WriteEvidenceReadme(evidenceDir, results, reportPath);

            Debug.Log($"[R3TargetedR] Completed {results.Count} rounds.");
            Debug.Log($"[R3TargetedR] Impact report: {reportPath}");
        }

        public static void RunR304SRetuning()
        {
            var profiles = BuildR304SProfiles();
            var plan = BuildDifficultyPlan();
            string runDate = DateTime.Now.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            string dateToken = DateTime.Now.ToString("yyyyMMdd", CultureInfo.InvariantCulture);
            string evidenceDir = GetEvidenceDirectory();
            var results = new List<RoundResult>(profiles.Count);

            for (int roundIndex = 0; roundIndex < profiles.Count; roundIndex++)
            {
                var profile = profiles[roundIndex];
                // Keep fixed seed window across S profiles for defeat-share micro lift comparison.
                var records = RunRound(profile, plan, runDate, 1180);
                var snapshot = EvaluateKpi(records, profile.Label);

                string csvPath = Path.Combine(evidenceDir, $"{dateToken}_R3-04S_TARGETED-{profile.RoundCode}.csv");
                string kpiPath = Path.Combine(evidenceDir, $"{dateToken}_R3-04S_TARGETED-{profile.RoundCode}-KPI.md");

                WriteSimulationCsv(records, csvPath);
                WriteRoundKpiMarkdown(records, snapshot, profile, kpiPath, csvPath);

                results.Add(new RoundResult
                {
                    Profile = profile,
                    Records = records,
                    Snapshot = snapshot,
                    CsvPath = csvPath,
                    KpiPath = kpiPath
                });

                Debug.Log($"[R3TargetedS] {profile.RoundCode} complete. Pass={snapshot.PassCount}/{snapshot.TotalCount}");
            }

            string reportPath = Path.Combine(evidenceDir, $"{dateToken}_R3-04S_TARGETED-IMPACT-REPORT.md");
            WriteImpactReport(results, reportPath);
            WriteEvidenceReadme(evidenceDir, results, reportPath);

            Debug.Log($"[R3TargetedS] Completed {results.Count} rounds.");
            Debug.Log($"[R3TargetedS] Impact report: {reportPath}");
        }

        public static void RunR304TRetuning()
        {
            var profiles = BuildR304TProfiles();
            var plan = BuildDifficultyPlan();
            string runDate = DateTime.Now.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            string dateToken = DateTime.Now.ToString("yyyyMMdd", CultureInfo.InvariantCulture);
            string evidenceDir = GetEvidenceDirectory();
            var results = new List<RoundResult>(profiles.Count);

            for (int roundIndex = 0; roundIndex < profiles.Count; roundIndex++)
            {
                var profile = profiles[roundIndex];
                // Keep fixed seed window across T profiles for end-turn floor closure comparison.
                var records = RunRound(profile, plan, runDate, 1240);
                var snapshot = EvaluateKpi(records, profile.Label);

                string csvPath = Path.Combine(evidenceDir, $"{dateToken}_R3-04T_TARGETED-{profile.RoundCode}.csv");
                string kpiPath = Path.Combine(evidenceDir, $"{dateToken}_R3-04T_TARGETED-{profile.RoundCode}-KPI.md");

                WriteSimulationCsv(records, csvPath);
                WriteRoundKpiMarkdown(records, snapshot, profile, kpiPath, csvPath);

                results.Add(new RoundResult
                {
                    Profile = profile,
                    Records = records,
                    Snapshot = snapshot,
                    CsvPath = csvPath,
                    KpiPath = kpiPath
                });

                Debug.Log($"[R3TargetedT] {profile.RoundCode} complete. Pass={snapshot.PassCount}/{snapshot.TotalCount}");
            }

            string reportPath = Path.Combine(evidenceDir, $"{dateToken}_R3-04T_TARGETED-IMPACT-REPORT.md");
            WriteImpactReport(results, reportPath);
            WriteEvidenceReadme(evidenceDir, results, reportPath);

            Debug.Log($"[R3TargetedT] Completed {results.Count} rounds.");
            Debug.Log($"[R3TargetedT] Impact report: {reportPath}");
        }

        public static void RunR304URetuning()
        {
            var profiles = BuildR304UProfiles();
            var plan = BuildDifficultyPlan();
            string runDate = DateTime.Now.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            string dateToken = DateTime.Now.ToString("yyyyMMdd", CultureInfo.InvariantCulture);
            string evidenceDir = GetEvidenceDirectory();
            var results = new List<RoundResult>(profiles.Count);

            for (int roundIndex = 0; roundIndex < profiles.Count; roundIndex++)
            {
                var profile = profiles[roundIndex];
                // Keep fixed seed window across U profiles for end-turn floor micro correction.
                var records = RunRound(profile, plan, runDate, 1300);
                var snapshot = EvaluateKpi(records, profile.Label);

                string csvPath = Path.Combine(evidenceDir, $"{dateToken}_R3-04U_TARGETED-{profile.RoundCode}.csv");
                string kpiPath = Path.Combine(evidenceDir, $"{dateToken}_R3-04U_TARGETED-{profile.RoundCode}-KPI.md");

                WriteSimulationCsv(records, csvPath);
                WriteRoundKpiMarkdown(records, snapshot, profile, kpiPath, csvPath);

                results.Add(new RoundResult
                {
                    Profile = profile,
                    Records = records,
                    Snapshot = snapshot,
                    CsvPath = csvPath,
                    KpiPath = kpiPath
                });

                Debug.Log($"[R3TargetedU] {profile.RoundCode} complete. Pass={snapshot.PassCount}/{snapshot.TotalCount}");
            }

            string reportPath = Path.Combine(evidenceDir, $"{dateToken}_R3-04U_TARGETED-IMPACT-REPORT.md");
            WriteImpactReport(results, reportPath);
            WriteEvidenceReadme(evidenceDir, results, reportPath);

            Debug.Log($"[R3TargetedU] Completed {results.Count} rounds.");
            Debug.Log($"[R3TargetedU] Impact report: {reportPath}");
        }

        public static void RunR304VRetuning()
        {
            var profiles = BuildR304VProfiles();
            var plan = BuildDifficultyPlan();
            string runDate = DateTime.Now.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            string dateToken = DateTime.Now.ToString("yyyyMMdd", CultureInfo.InvariantCulture);
            string evidenceDir = GetEvidenceDirectory();
            var results = new List<RoundResult>(profiles.Count);

            for (int roundIndex = 0; roundIndex < profiles.Count; roundIndex++)
            {
                var profile = profiles[roundIndex];
                // Keep fixed seed window across V profiles for cross-run stability recovery comparison.
                var records = RunRound(profile, plan, runDate, 1360);
                var snapshot = EvaluateKpi(records, profile.Label);

                string csvPath = Path.Combine(evidenceDir, $"{dateToken}_R3-04V_TARGETED-{profile.RoundCode}.csv");
                string kpiPath = Path.Combine(evidenceDir, $"{dateToken}_R3-04V_TARGETED-{profile.RoundCode}-KPI.md");

                WriteSimulationCsv(records, csvPath);
                WriteRoundKpiMarkdown(records, snapshot, profile, kpiPath, csvPath);

                results.Add(new RoundResult
                {
                    Profile = profile,
                    Records = records,
                    Snapshot = snapshot,
                    CsvPath = csvPath,
                    KpiPath = kpiPath
                });

                Debug.Log($"[R3TargetedV] {profile.RoundCode} complete. Pass={snapshot.PassCount}/{snapshot.TotalCount}");
            }

            string reportPath = Path.Combine(evidenceDir, $"{dateToken}_R3-04V_TARGETED-IMPACT-REPORT.md");
            WriteImpactReport(results, reportPath);
            WriteEvidenceReadme(evidenceDir, results, reportPath);

            Debug.Log($"[R3TargetedV] Completed {results.Count} rounds.");
            Debug.Log($"[R3TargetedV] Impact report: {reportPath}");
        }

        public static void RunR304WRetuning()
        {
            var profiles = BuildR304WProfiles();
            var plan = BuildDifficultyPlan();
            string runDate = DateTime.Now.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            string dateToken = DateTime.Now.ToString("yyyyMMdd", CultureInfo.InvariantCulture);
            string evidenceDir = GetEvidenceDirectory();
            var results = new List<RoundResult>(profiles.Count);

            for (int roundIndex = 0; roundIndex < profiles.Count; roundIndex++)
            {
                var profile = profiles[roundIndex];
                // Keep fixed seed window across W profiles for R3-05 stability-gap closure comparison.
                var records = RunRound(profile, plan, runDate, 1420);
                var snapshot = EvaluateKpi(records, profile.Label);

                string csvPath = Path.Combine(evidenceDir, $"{dateToken}_R3-04W_TARGETED-{profile.RoundCode}.csv");
                string kpiPath = Path.Combine(evidenceDir, $"{dateToken}_R3-04W_TARGETED-{profile.RoundCode}-KPI.md");

                WriteSimulationCsv(records, csvPath);
                WriteRoundKpiMarkdown(records, snapshot, profile, kpiPath, csvPath);

                results.Add(new RoundResult
                {
                    Profile = profile,
                    Records = records,
                    Snapshot = snapshot,
                    CsvPath = csvPath,
                    KpiPath = kpiPath
                });

                Debug.Log($"[R3TargetedW] {profile.RoundCode} complete. Pass={snapshot.PassCount}/{snapshot.TotalCount}");
            }

            string reportPath = Path.Combine(evidenceDir, $"{dateToken}_R3-04W_TARGETED-IMPACT-REPORT.md");
            WriteImpactReport(results, reportPath);
            WriteEvidenceReadme(evidenceDir, results, reportPath);

            Debug.Log($"[R3TargetedW] Completed {results.Count} rounds.");
            Debug.Log($"[R3TargetedW] Impact report: {reportPath}");
        }

        public static void RunR304XRetuning()
        {
            var profiles = BuildR304XProfiles();
            var plan = BuildDifficultyPlan();
            string runDate = DateTime.Now.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            string dateToken = DateTime.Now.ToString("yyyyMMdd", CultureInfo.InvariantCulture);
            string evidenceDir = GetEvidenceDirectory();
            var results = new List<RoundResult>(profiles.Count);

            for (int roundIndex = 0; roundIndex < profiles.Count; roundIndex++)
            {
                var profile = profiles[roundIndex];
                // Keep fixed seed window aligned with W for residue closure A/B comparison.
                var records = RunRound(profile, plan, runDate, 1420);
                var snapshot = EvaluateKpi(records, profile.Label);

                string csvPath = Path.Combine(evidenceDir, $"{dateToken}_R3-04X_TARGETED-{profile.RoundCode}.csv");
                string kpiPath = Path.Combine(evidenceDir, $"{dateToken}_R3-04X_TARGETED-{profile.RoundCode}-KPI.md");

                WriteSimulationCsv(records, csvPath);
                WriteRoundKpiMarkdown(records, snapshot, profile, kpiPath, csvPath);

                results.Add(new RoundResult
                {
                    Profile = profile,
                    Records = records,
                    Snapshot = snapshot,
                    CsvPath = csvPath,
                    KpiPath = kpiPath
                });

                Debug.Log($"[R3TargetedX] {profile.RoundCode} complete. Pass={snapshot.PassCount}/{snapshot.TotalCount}");
            }

            string reportPath = Path.Combine(evidenceDir, $"{dateToken}_R3-04X_TARGETED-IMPACT-REPORT.md");
            WriteImpactReport(results, reportPath);
            WriteEvidenceReadme(evidenceDir, results, reportPath);

            Debug.Log($"[R3TargetedX] Completed {results.Count} rounds.");
            Debug.Log($"[R3TargetedX] Impact report: {reportPath}");
        }

        public static void RunR304YRetuning()
        {
            var profiles = BuildR304YProfiles();
            var plan = BuildDifficultyPlan();
            string runDate = DateTime.Now.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            string dateToken = DateTime.Now.ToString("yyyyMMdd", CultureInfo.InvariantCulture);
            string evidenceDir = GetEvidenceDirectory();
            var results = new List<RoundResult>(profiles.Count);

            for (int roundIndex = 0; roundIndex < profiles.Count; roundIndex++)
            {
                var profile = profiles[roundIndex];
                // Keep fixed seed window aligned with W for A/B comparison.
                var records = RunRound(profile, plan, runDate, 1420);
                var snapshot = EvaluateKpi(records, profile.Label);

                string csvPath = Path.Combine(evidenceDir, $"{dateToken}_R3-04Y_TARGETED-{profile.RoundCode}.csv");
                string kpiPath = Path.Combine(evidenceDir, $"{dateToken}_R3-04Y_TARGETED-{profile.RoundCode}-KPI.md");

                WriteSimulationCsv(records, csvPath);
                WriteRoundKpiMarkdown(records, snapshot, profile, kpiPath, csvPath);

                results.Add(new RoundResult
                {
                    Profile = profile,
                    Records = records,
                    Snapshot = snapshot,
                    CsvPath = csvPath,
                    KpiPath = kpiPath
                });

                Debug.Log($"[R3TargetedY] {profile.RoundCode} complete. Pass={snapshot.PassCount}/{snapshot.TotalCount}");
            }

            string reportPath = Path.Combine(evidenceDir, $"{dateToken}_R3-04Y_TARGETED-IMPACT-REPORT.md");
            WriteImpactReport(results, reportPath);
            WriteEvidenceReadme(evidenceDir, results, reportPath);

            Debug.Log($"[R3TargetedY] Completed {results.Count} rounds.");
            Debug.Log($"[R3TargetedY] Impact report: {reportPath}");
        }

        public static void RunR304ZRetuning()
        {
            var profiles = BuildR304ZProfiles();
            var plan = BuildDifficultyPlan();
            string runDate = DateTime.Now.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            string dateToken = DateTime.Now.ToString("yyyyMMdd", CultureInfo.InvariantCulture);
            string evidenceDir = GetEvidenceDirectory();
            var results = new List<RoundResult>(profiles.Count);

            for (int roundIndex = 0; roundIndex < profiles.Count; roundIndex++)
            {
                var profile = profiles[roundIndex];
                // Keep fixed seed window aligned with W/Y for A/B comparison.
                var records = RunRound(profile, plan, runDate, 1420);
                var snapshot = EvaluateKpi(records, profile.Label);

                string csvPath = Path.Combine(evidenceDir, $"{dateToken}_R3-04Z_TARGETED-{profile.RoundCode}.csv");
                string kpiPath = Path.Combine(evidenceDir, $"{dateToken}_R3-04Z_TARGETED-{profile.RoundCode}-KPI.md");

                WriteSimulationCsv(records, csvPath);
                WriteRoundKpiMarkdown(records, snapshot, profile, kpiPath, csvPath);

                results.Add(new RoundResult
                {
                    Profile = profile,
                    Records = records,
                    Snapshot = snapshot,
                    CsvPath = csvPath,
                    KpiPath = kpiPath
                });

                Debug.Log($"[R3TargetedZ] {profile.RoundCode} complete. Pass={snapshot.PassCount}/{snapshot.TotalCount}");
            }

            string reportPath = Path.Combine(evidenceDir, $"{dateToken}_R3-04Z_TARGETED-IMPACT-REPORT.md");
            WriteImpactReport(results, reportPath);
            WriteEvidenceReadme(evidenceDir, results, reportPath);

            Debug.Log($"[R3TargetedZ] Completed {results.Count} rounds.");
            Debug.Log($"[R3TargetedZ] Impact report: {reportPath}");
        }

        public static void RunR304AARetuning()
        {
            var profiles = BuildR304AAProfiles();
            var plan = BuildDifficultyPlan();
            string runDate = DateTime.Now.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            string dateToken = DateTime.Now.ToString("yyyyMMdd", CultureInfo.InvariantCulture);
            string evidenceDir = GetEvidenceDirectory();
            var results = new List<RoundResult>(profiles.Count);

            for (int roundIndex = 0; roundIndex < profiles.Count; roundIndex++)
            {
                var profile = profiles[roundIndex];
                // Keep fixed seed window aligned with W/Y/Z for A/B comparison.
                var records = RunRound(profile, plan, runDate, 1420);
                var snapshot = EvaluateKpi(records, profile.Label);

                string csvPath = Path.Combine(evidenceDir, $"{dateToken}_R3-04AA_TARGETED-{profile.RoundCode}.csv");
                string kpiPath = Path.Combine(evidenceDir, $"{dateToken}_R3-04AA_TARGETED-{profile.RoundCode}-KPI.md");

                WriteSimulationCsv(records, csvPath);
                WriteRoundKpiMarkdown(records, snapshot, profile, kpiPath, csvPath);

                results.Add(new RoundResult
                {
                    Profile = profile,
                    Records = records,
                    Snapshot = snapshot,
                    CsvPath = csvPath,
                    KpiPath = kpiPath
                });

                Debug.Log($"[R3TargetedAA] {profile.RoundCode} complete. Pass={snapshot.PassCount}/{snapshot.TotalCount}");
            }

            string reportPath = Path.Combine(evidenceDir, $"{dateToken}_R3-04AA_TARGETED-IMPACT-REPORT.md");
            WriteImpactReport(results, reportPath);
            WriteEvidenceReadme(evidenceDir, results, reportPath);

            Debug.Log($"[R3TargetedAA] Completed {results.Count} rounds.");
            Debug.Log($"[R3TargetedAA] Impact report: {reportPath}");
        }

        public static void RunR304ABRetuning()
        {
            var profiles = BuildR304ABProfiles();
            var plan = BuildDifficultyPlan();
            string runDate = DateTime.Now.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            string dateToken = DateTime.Now.ToString("yyyyMMdd", CultureInfo.InvariantCulture);
            string evidenceDir = GetEvidenceDirectory();
            var results = new List<RoundResult>(profiles.Count);

            for (int roundIndex = 0; roundIndex < profiles.Count; roundIndex++)
            {
                var profile = profiles[roundIndex];
                // Keep fixed seed window aligned with W/Y/Z/AA for A/B comparison.
                var records = RunRound(profile, plan, runDate, 1420);
                var snapshot = EvaluateKpi(records, profile.Label);

                string csvPath = Path.Combine(evidenceDir, $"{dateToken}_R3-04AB_TARGETED-{profile.RoundCode}.csv");
                string kpiPath = Path.Combine(evidenceDir, $"{dateToken}_R3-04AB_TARGETED-{profile.RoundCode}-KPI.md");

                WriteSimulationCsv(records, csvPath);
                WriteRoundKpiMarkdown(records, snapshot, profile, kpiPath, csvPath);

                results.Add(new RoundResult
                {
                    Profile = profile,
                    Records = records,
                    Snapshot = snapshot,
                    CsvPath = csvPath,
                    KpiPath = kpiPath
                });

                Debug.Log($"[R3TargetedAB] {profile.RoundCode} complete. Pass={snapshot.PassCount}/{snapshot.TotalCount}");
            }

            string reportPath = Path.Combine(evidenceDir, $"{dateToken}_R3-04AB_TARGETED-IMPACT-REPORT.md");
            WriteImpactReport(results, reportPath);
            WriteEvidenceReadme(evidenceDir, results, reportPath);

            Debug.Log($"[R3TargetedAB] Completed {results.Count} rounds.");
            Debug.Log($"[R3TargetedAB] Impact report: {reportPath}");
        }

        public static void RunR304ACRetuning()
        {
            var profiles = BuildR304ACProfiles();
            var plan = BuildDifficultyPlan();
            string runDate = DateTime.Now.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            string dateToken = DateTime.Now.ToString("yyyyMMdd", CultureInfo.InvariantCulture);
            string evidenceDir = GetEvidenceDirectory();
            var results = new List<RoundResult>(profiles.Count);

            for (int roundIndex = 0; roundIndex < profiles.Count; roundIndex++)
            {
                var profile = profiles[roundIndex];
                // Keep fixed seed window aligned with W/Y/Z/AA/AB for A/B comparison.
                var records = RunRound(profile, plan, runDate, 1420);
                var snapshot = EvaluateKpi(records, profile.Label);

                string csvPath = Path.Combine(evidenceDir, $"{dateToken}_R3-04AC_TARGETED-{profile.RoundCode}.csv");
                string kpiPath = Path.Combine(evidenceDir, $"{dateToken}_R3-04AC_TARGETED-{profile.RoundCode}-KPI.md");

                WriteSimulationCsv(records, csvPath);
                WriteRoundKpiMarkdown(records, snapshot, profile, kpiPath, csvPath);

                results.Add(new RoundResult
                {
                    Profile = profile,
                    Records = records,
                    Snapshot = snapshot,
                    CsvPath = csvPath,
                    KpiPath = kpiPath
                });

                Debug.Log($"[R3TargetedAC] {profile.RoundCode} complete. Pass={snapshot.PassCount}/{snapshot.TotalCount}");
            }

            string reportPath = Path.Combine(evidenceDir, $"{dateToken}_R3-04AC_TARGETED-IMPACT-REPORT.md");
            WriteImpactReport(results, reportPath);
            WriteEvidenceReadme(evidenceDir, results, reportPath);

            Debug.Log($"[R3TargetedAC] Completed {results.Count} rounds.");
            Debug.Log($"[R3TargetedAC] Impact report: {reportPath}");
        }

        public static void RunR304ADRetuning()
        {
            var profiles = BuildR304ADProfiles();
            var plan = BuildDifficultyPlan();
            string runDate = DateTime.Now.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            string dateToken = DateTime.Now.ToString("yyyyMMdd", CultureInfo.InvariantCulture);
            string evidenceDir = GetEvidenceDirectory();
            var results = new List<RoundResult>(profiles.Count);

            for (int roundIndex = 0; roundIndex < profiles.Count; roundIndex++)
            {
                var profile = profiles[roundIndex];
                // Keep fixed seed window aligned with W/Y/Z/AA/AB/AC for A/B comparison.
                var records = RunRound(profile, plan, runDate, 1420);
                var snapshot = EvaluateKpi(records, profile.Label);

                string csvPath = Path.Combine(evidenceDir, $"{dateToken}_R3-04AD_TARGETED-{profile.RoundCode}.csv");
                string kpiPath = Path.Combine(evidenceDir, $"{dateToken}_R3-04AD_TARGETED-{profile.RoundCode}-KPI.md");

                WriteSimulationCsv(records, csvPath);
                WriteRoundKpiMarkdown(records, snapshot, profile, kpiPath, csvPath);

                results.Add(new RoundResult
                {
                    Profile = profile,
                    Records = records,
                    Snapshot = snapshot,
                    CsvPath = csvPath,
                    KpiPath = kpiPath
                });

                Debug.Log($"[R3TargetedAD] {profile.RoundCode} complete. Pass={snapshot.PassCount}/{snapshot.TotalCount}");
            }

            string reportPath = Path.Combine(evidenceDir, $"{dateToken}_R3-04AD_TARGETED-IMPACT-REPORT.md");
            WriteImpactReport(results, reportPath);
            WriteEvidenceReadme(evidenceDir, results, reportPath);

            Debug.Log($"[R3TargetedAD] Completed {results.Count} rounds.");
            Debug.Log($"[R3TargetedAD] Impact report: {reportPath}");
        }

        public static void RunR304AERetuning()
        {
            var profiles = BuildR304AEProfiles();
            var plan = BuildDifficultyPlan();
            string runDate = DateTime.Now.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            string dateToken = DateTime.Now.ToString("yyyyMMdd", CultureInfo.InvariantCulture);
            string evidenceDir = GetEvidenceDirectory();
            var results = new List<RoundResult>(profiles.Count);

            for (int roundIndex = 0; roundIndex < profiles.Count; roundIndex++)
            {
                var profile = profiles[roundIndex];
                // Keep fixed seed window aligned with W/Y/Z/AA/AB/AC/AD for A/B comparison.
                var records = RunRound(profile, plan, runDate, 1420);
                var snapshot = EvaluateKpi(records, profile.Label);

                string csvPath = Path.Combine(evidenceDir, $"{dateToken}_R3-04AE_TARGETED-{profile.RoundCode}.csv");
                string kpiPath = Path.Combine(evidenceDir, $"{dateToken}_R3-04AE_TARGETED-{profile.RoundCode}-KPI.md");

                WriteSimulationCsv(records, csvPath);
                WriteRoundKpiMarkdown(records, snapshot, profile, kpiPath, csvPath);

                results.Add(new RoundResult
                {
                    Profile = profile,
                    Records = records,
                    Snapshot = snapshot,
                    CsvPath = csvPath,
                    KpiPath = kpiPath
                });

                Debug.Log($"[R3TargetedAE] {profile.RoundCode} complete. Pass={snapshot.PassCount}/{snapshot.TotalCount}");
            }

            string reportPath = Path.Combine(evidenceDir, $"{dateToken}_R3-04AE_TARGETED-IMPACT-REPORT.md");
            WriteImpactReport(results, reportPath);
            WriteEvidenceReadme(evidenceDir, results, reportPath);

            Debug.Log($"[R3TargetedAE] Completed {results.Count} rounds.");
            Debug.Log($"[R3TargetedAE] Impact report: {reportPath}");
        }

        public static void RunR304AFRetuning()
        {
            var profiles = BuildR304AFProfiles();
            var plan = BuildDifficultyPlan();
            string runDate = DateTime.Now.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            string dateToken = DateTime.Now.ToString("yyyyMMdd", CultureInfo.InvariantCulture);
            string evidenceDir = GetEvidenceDirectory();
            var results = new List<RoundResult>(profiles.Count);

            for (int roundIndex = 0; roundIndex < profiles.Count; roundIndex++)
            {
                var profile = profiles[roundIndex];
                // Keep fixed seed window aligned with W/Y/Z/AA/AB/AC/AD/AE for A/B comparison.
                var records = RunRound(profile, plan, runDate, 1420);
                var snapshot = EvaluateKpi(records, profile.Label);

                string csvPath = Path.Combine(evidenceDir, $"{dateToken}_R3-04AF_TARGETED-{profile.RoundCode}.csv");
                string kpiPath = Path.Combine(evidenceDir, $"{dateToken}_R3-04AF_TARGETED-{profile.RoundCode}-KPI.md");

                WriteSimulationCsv(records, csvPath);
                WriteRoundKpiMarkdown(records, snapshot, profile, kpiPath, csvPath);

                results.Add(new RoundResult
                {
                    Profile = profile,
                    Records = records,
                    Snapshot = snapshot,
                    CsvPath = csvPath,
                    KpiPath = kpiPath
                });

                Debug.Log($"[R3TargetedAF] {profile.RoundCode} complete. Pass={snapshot.PassCount}/{snapshot.TotalCount}");
            }

            string reportPath = Path.Combine(evidenceDir, $"{dateToken}_R3-04AF_TARGETED-IMPACT-REPORT.md");
            WriteImpactReport(results, reportPath);
            WriteEvidenceReadme(evidenceDir, results, reportPath);

            Debug.Log($"[R3TargetedAF] Completed {results.Count} rounds.");
            Debug.Log($"[R3TargetedAF] Impact report: {reportPath}");
        }

        public static void RunR304AGRetuning()
        {
            var profiles = BuildR304AGProfiles();
            var plan = BuildDifficultyPlan();
            string runDate = DateTime.Now.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            string dateToken = DateTime.Now.ToString("yyyyMMdd", CultureInfo.InvariantCulture);
            string evidenceDir = GetEvidenceDirectory();
            var results = new List<RoundResult>(profiles.Count);

            for (int roundIndex = 0; roundIndex < profiles.Count; roundIndex++)
            {
                var profile = profiles[roundIndex];
                // Keep fixed seed window aligned with W/Y/Z/AA/AB/AC/AD/AE/AF for A/B comparison.
                var records = RunRound(profile, plan, runDate, 1420);
                var snapshot = EvaluateKpi(records, profile.Label);

                string csvPath = Path.Combine(evidenceDir, $"{dateToken}_R3-04AG_TARGETED-{profile.RoundCode}.csv");
                string kpiPath = Path.Combine(evidenceDir, $"{dateToken}_R3-04AG_TARGETED-{profile.RoundCode}-KPI.md");

                WriteSimulationCsv(records, csvPath);
                WriteRoundKpiMarkdown(records, snapshot, profile, kpiPath, csvPath);

                results.Add(new RoundResult
                {
                    Profile = profile,
                    Records = records,
                    Snapshot = snapshot,
                    CsvPath = csvPath,
                    KpiPath = kpiPath
                });

                Debug.Log($"[R3TargetedAG] {profile.RoundCode} complete. Pass={snapshot.PassCount}/{snapshot.TotalCount}");
            }

            string reportPath = Path.Combine(evidenceDir, $"{dateToken}_R3-04AG_TARGETED-IMPACT-REPORT.md");
            WriteImpactReport(results, reportPath);
            WriteEvidenceReadme(evidenceDir, results, reportPath);

            Debug.Log($"[R3TargetedAG] Completed {results.Count} rounds.");
            Debug.Log($"[R3TargetedAG] Impact report: {reportPath}");
        }

        public static void RunR305Validation()
        {
            var selectedProfile = ResolveR305BaseProfile();
            if (selectedProfile == null)
            {
                Debug.LogError("[R3Tuning] R3-05 validation aborted: missing base profile.");
                return;
            }

            string runDate = DateTime.Now.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            string dateToken = DateTime.Now.ToString("yyyyMMdd", CultureInfo.InvariantCulture);
            string evidenceDir = GetValidationEvidenceDirectory();
            var plan = BuildDifficultyPlan();
            var validationRuns = new List<RoundResult>(3);
            string baseRoundCode = selectedProfile.RoundCode;
            string baseProfileLabel = $"{selectedProfile.RoundCode}-{selectedProfile.Label}";

            for (int i = 1; i <= 3; i++)
            {
                string roundCode = $"V{i:00}";
                var profile = CloneProfile(selectedProfile);
                profile.RoundCode = roundCode;
                profile.BatchId = $"R3-05-B002-{baseRoundCode}-{roundCode}";
                profile.Label = $"{selectedProfile.Label}-validation";

                var records = RunRound(profile, plan, runDate, 90 + i);
                var snapshot = EvaluateKpi(records, $"{profile.Label}-{roundCode}");

                string csvPath = Path.Combine(evidenceDir, $"{dateToken}_R3-05_VALIDATION-{baseRoundCode}-{roundCode}.csv");
                string kpiPath = Path.Combine(evidenceDir, $"{dateToken}_R3-05_VALIDATION-{baseRoundCode}-{roundCode}-KPI.md");

                WriteSimulationCsv(records, csvPath);
                WriteRoundKpiMarkdown(records, snapshot, profile, kpiPath, csvPath);

                validationRuns.Add(new RoundResult
                {
                    Profile = profile,
                    Records = records,
                    Snapshot = snapshot,
                    CsvPath = csvPath,
                    KpiPath = kpiPath
                });

                Debug.Log($"[R3Validation] {roundCode} complete. Pass={snapshot.PassCount}/{snapshot.TotalCount}");
            }

            string finalReportPath = Path.Combine(evidenceDir, $"{dateToken}_R3-05_FINAL-BALANCE-REPORT-{baseRoundCode}.md");
            string summaryJsonPath = Path.Combine(evidenceDir, $"{dateToken}_R3-05_VALIDATION-SUMMARY-{baseRoundCode}.json");
            WriteR305FinalReport(validationRuns, finalReportPath, summaryJsonPath, baseProfileLabel);
            WriteR305EvidenceReadme(evidenceDir, validationRuns, finalReportPath, summaryJsonPath);

            Debug.Log($"[R3Validation] Completed {validationRuns.Count} validation runs.");
            Debug.Log($"[R3Validation] Final report: {finalReportPath}");
            Debug.Log($"[R3Validation] Summary json: {summaryJsonPath}");
        }

        private static List<SimulationRecord> RunRound(TuningProfile profile, List<SimulationDifficulty> plan, string runDate, int roundIndex)
        {
            var records = new List<SimulationRecord>(kTotalRuns);
            int seedBase = 320000 + roundIndex * 10000;

            for (int i = 0; i < plan.Count; i++)
            {
                int seed = seedBase + i;
                var record = SimulateSingleRun(i + 1, plan[i], seed, runDate, profile);
                records.Add(record);

                if ((i + 1) % 100 == 0)
                {
                    Debug.Log($"[R3Tuning] {profile.RoundCode} progress: {i + 1}/{kTotalRuns}");
                }
            }

            return records;
        }

        private static SimulationRecord SimulateSingleRun(int index, SimulationDifficulty difficulty, int seed, string runDate, TuningProfile profile)
        {
            var rng = new System.Random(seed);
            var config = DefaultGameConfig.CreateDefault();
            var state = ScriptableObject.CreateInstance<GameState>();
            state.Config = config;
            state.Initialize();

            ApplyDifficultyInitialization(state, difficulty, rng, profile);

            var events = ScriptableObject.CreateInstance<GameEvents>();
            var jGo = new GameObject($"R3Tuning_{profile.RoundCode}_{index:D4}");
            var j = jGo.AddComponent<VictoryDefeatSystem>();
            ConfigureDifficultyThresholds(j, difficulty, profile);
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
                ApplyTurnDynamics(state, j, difficulty, rng, turn, profile);

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
            float durationMinutes = EstimateDurationMinutes(endTurn, difficulty, rng, profile);

            if (!j.IsGameEnded())
            {
                notes = "No endgame dispatch; forced timeout check path.";
            }

            var record = new SimulationRecord
            {
                BatchId = profile.BatchId,
                RunId = $"{ResolveRunPrefix(profile)}-{profile.RoundCode}-{index:D6}",
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

        private static void ApplyDifficultyInitialization(GameState state, SimulationDifficulty difficulty, System.Random rng, TuningProfile profile)
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

            float diffScale = GetDifficultyInitScale(profile, difficulty);
            goldLeaf = Mathf.RoundToInt(goldLeaf * diffScale);
            fireOil = Mathf.RoundToInt(fireOil * diffScale);
            arms = Mathf.RoundToInt(arms * diffScale);
            ashWill = Mathf.RoundToInt(ashWill * diffScale);
            social = Mathf.RoundToInt(social * profile.SocialTradeInitScale);
            tradeToken = Mathf.RoundToInt(tradeToken * profile.SocialTradeInitScale);
            tributeOrder = Mathf.RoundToInt(tributeOrder * profile.SocialTradeInitScale);

            SetResourceAmount(state, GameIds.Resource.GoldLeaf, goldLeaf);
            SetResourceAmount(state, GameIds.Resource.FireOil, fireOil);
            SetResourceAmount(state, GameIds.Resource.Arms, arms);
            SetResourceAmount(state, GameIds.Resource.AshWill, ashWill);
            SetResourceAmount(state, GameIds.Resource.SocialValue, social);
            SetResourceAmount(state, GameIds.Resource.TradeToken, tradeToken);
            SetResourceAmount(state, GameIds.Resource.TributeOrder, tributeOrder);
        }

        private static void ConfigureDifficultyThresholds(VictoryDefeatSystem j, SimulationDifficulty difficulty, TuningProfile profile)
        {
            switch (difficulty)
            {
                case SimulationDifficulty.Easy:
                    j.VictoryThreshold = profile.EasyVictoryThreshold;
                    j.MilitaryCollapseAshWillThreshold = profile.EasyMilitaryCollapseThreshold;
                    j.InternalDivisionSatisfactionThreshold = 10;
                    break;
                case SimulationDifficulty.Standard:
                    j.VictoryThreshold = profile.StandardVictoryThreshold;
                    j.MilitaryCollapseAshWillThreshold = profile.StandardMilitaryCollapseThreshold;
                    j.InternalDivisionSatisfactionThreshold = 15;
                    break;
                case SimulationDifficulty.Hard:
                    j.VictoryThreshold = profile.HardVictoryThreshold;
                    j.MilitaryCollapseAshWillThreshold = profile.HardMilitaryCollapseThreshold;
                    j.InternalDivisionSatisfactionThreshold = 20;
                    break;
            }
        }

        private static void ApplyTurnDynamics(GameState state, VictoryDefeatSystem j, SimulationDifficulty difficulty, System.Random rng, int turn, TuningProfile profile)
        {
            float bias = GetDifficultyBias(difficulty, profile);
            float lateEscalation = ResolveLateEscalation(turn, profile);
            float pressureSignal = 0.45f - bias + profile.PressureOffset + NextFloat(rng, -0.25f * profile.ResourceNoiseScale, 0.25f * profile.ResourceNoiseScale) + (turn / (float)GameConfig.kMaxTurns) * profile.PressureTurnScale;
            pressureSignal += lateEscalation * profile.LatePressureTurnBonus;
            var blockadeLevel = ResolveBlockadeLevel(pressureSignal);
            j.SetBlockadeLevel(blockadeLevel);

            float conflictChance = Mathf.Clamp01(profile.ConflictBase - (bias * 0.45f) + (turn * profile.ConflictTurnScale));
            conflictChance = Mathf.Clamp01(conflictChance + lateEscalation * profile.LateConflictTurnBonus);
            bool conflict = rng.NextDouble() < conflictChance;
            j.SetLargeScaleConflictActive(conflict);
            if (conflict && rng.NextDouble() < 0.35f + (bias * 0.25f))
            {
                j.RecordEnemyKeyNodeLoss();
            }

            if (blockadeLevel >= BlockadeLevel.Multilateral && rng.NextDouble() < 0.22f)
            {
                j.RecordBlockadePostponement();
            }

            ApplyNodeControlDynamics(state, difficulty, rng, conflict, profile, lateEscalation);

            float routeBonus = ComputeRouteControlBonus(state);
            int goldDelta = Mathf.RoundToInt((14f + routeBonus * 8f + bias * 18f + NextFloat(rng, -22f, 22f) * profile.ResourceNoiseScale - GetGoldPenalty(blockadeLevel) - (conflict ? 6f : 0f)) * profile.ResourceDeltaScale);
            int fireOilDelta = Mathf.RoundToInt((5f + routeBonus * 4f + bias * 5f + NextFloat(rng, -9f, 9f) * profile.ResourceNoiseScale - GetFireOilPenalty(blockadeLevel) - (conflict ? 3f : 0f)) * profile.ResourceDeltaScale);
            int armsDelta = Mathf.RoundToInt((3f + bias * 4f + NextFloat(rng, -7f, 7f) * profile.ResourceNoiseScale + (conflict ? -4f : 2f)) * profile.ResourceDeltaScale);

            ApplyEarlyGainSuppression(ref goldDelta, turn, profile);
            ApplyEarlyGainSuppression(ref fireOilDelta, turn, profile);
            ApplyEarlyGainSuppression(ref armsDelta, turn, profile);

            ApplyShock(ref goldDelta, profile, rng);
            ApplyShock(ref fireOilDelta, profile, rng);
            ApplyShock(ref armsDelta, profile, rng);

            int ashDelta = Mathf.RoundToInt(NextFloat(rng, -6f, 6f) * profile.ResourceNoiseScale + (goldDelta >= 0 ? 2f : -3f) + (conflict ? -3f : 1f) + bias * 2f);
            int socialDelta = Mathf.RoundToInt(NextFloat(rng, -7f, 7f) * profile.ResourceNoiseScale + (blockadeLevel <= BlockadeLevel.Unilateral ? 2f : -2f) + (goldDelta >= 0 ? 1f : -1f) + bias * 1.5f);
            int tradeTokenDelta = Mathf.RoundToInt(NextFloat(rng, -5f, 6f) * profile.ResourceNoiseScale + (blockadeLevel <= BlockadeLevel.Unilateral ? 3f : -2f) + routeBonus * 3f);
            int tributeDelta = Mathf.RoundToInt(NextFloat(rng, -4f, 5f) * profile.ResourceNoiseScale + (blockadeLevel == BlockadeLevel.None ? 2f : 0f) + bias * 2f);

            ApplyResourceDelta(state, GameIds.Resource.GoldLeaf, goldDelta);
            ApplyResourceDelta(state, GameIds.Resource.FireOil, fireOilDelta);
            ApplyResourceDelta(state, GameIds.Resource.Arms, armsDelta);
            ApplyResourceDelta(state, GameIds.Resource.AshWill, ashDelta);
            ApplyResourceDelta(state, GameIds.Resource.SocialValue, socialDelta);
            ApplyResourceDelta(state, GameIds.Resource.TradeToken, tradeTokenDelta);
            ApplyResourceDelta(state, GameIds.Resource.TributeOrder, tributeDelta);

            int sacredFireDelta = Mathf.RoundToInt((NextFloat(rng, -5f, 5f) + bias * 2f + (blockadeLevel == BlockadeLevel.None ? 1f : -1f)) * profile.RelationDeltaScale);
            int aureanDelta = Mathf.RoundToInt((NextFloat(rng, -4f, 4f) - bias * 2f - (conflict ? 2f : 0f)) * profile.RelationDeltaScale);
            ApplyRelationDelta(state, GameIds.Faction.SacredFire, sacredFireDelta);
            ApplyRelationDelta(state, GameIds.Faction.Aurean, aureanDelta);

            ApplySatisfactionDynamics(state, difficulty, rng, conflict, blockadeLevel, profile);
        }

        private static float ResolveLateEscalation(int turn, TuningProfile profile)
        {
            if (profile == null)
            {
                return 0f;
            }

            int startTurn = profile.LatePressureStartTurn;
            if (startTurn <= 0 || startTurn > GameConfig.kMaxTurns || turn < startTurn)
            {
                return 0f;
            }

            int totalLateTurns = Mathf.Max(1, GameConfig.kMaxTurns - startTurn + 1);
            int progressedTurns = Mathf.Clamp(turn - startTurn + 1, 0, totalLateTurns);
            return Mathf.Clamp01(progressedTurns / (float)totalLateTurns);
        }

        private static void ApplyNodeControlDynamics(GameState state, SimulationDifficulty difficulty, System.Random rng, bool conflict, TuningProfile profile, float lateEscalation)
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

            captureChance *= profile.CaptureChanceScale;
            lossChance *= profile.LossChanceScale;
            medCaptureChance *= profile.CaptureChanceScale;

            if (lateEscalation > 0f)
            {
                captureChance = Mathf.Max(0f, captureChance - profile.LateCaptureChancePenalty * lateEscalation);
                medCaptureChance = Mathf.Max(0f, medCaptureChance - (profile.LateCaptureChancePenalty * 0.8f) * lateEscalation);
                lossChance = Mathf.Clamp01(lossChance + profile.LateLossChanceBonus * lateEscalation);
            }

            if (!conflict)
            {
                captureChance *= profile.NonConflictNodeScale;
                lossChance *= profile.NonConflictNodeScale;
                medCaptureChance *= profile.NonConflictNodeScale;
            }

            ResolveNodeContest(state, rng, GameIds.Node.IraqBorder, captureChance, lossChance, GameIds.Faction.Aurean, profile);
            ResolveNodeContest(state, rng, GameIds.Node.SyriaZone, captureChance, lossChance, GameIds.Faction.Aurean, profile);
            ResolveNodeContest(state, rng, GameIds.Node.Mediterranean, medCaptureChance, lossChance * 1.1f, GameIds.Faction.SacredFire, profile);
        }

        private static void ResolveNodeContest(GameState state, System.Random rng, string nodeId, float captureChance, float lossChance, string fallbackEnemyFaction, TuningProfile profile)
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
                int drift = Mathf.RoundToInt(NextInt(rng, -6, 6) * profile.NodeDriftScale);
                node.ControlPoints = Mathf.Clamp(node.ControlPoints + drift, 0, node.MaxControlPoints);
            }
        }

        private static void ApplySatisfactionDynamics(GameState state, SimulationDifficulty difficulty, System.Random rng, bool conflict, BlockadeLevel blockadeLevel, TuningProfile profile)
        {
            float bias = GetDifficultyBias(difficulty, profile);
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
                    (NextFloat(rng, -4f, 4f) + bias * 2f - pressurePenalty + (ashWill - 50) / 25f + (socialValue - 50) / 30f) * profile.SatisfactionScale);

                faction.Satisfaction = Mathf.Clamp(faction.Satisfaction + adjustment, 0, 100);
            }
        }

        private static KpiSnapshot EvaluateKpi(List<SimulationRecord> records, string label)
        {
            double totalRuns = records.Count;
            var easy = records.Where(r => r.Difficulty == "easy").ToList();
            var standard = records.Where(r => r.Difficulty == "standard").ToList();
            var hard = records.Where(r => r.Difficulty == "hard").ToList();

            double winRateEasy = easy.Count > 0 ? easy.Count(r => r.EndType == "victory") / (double)easy.Count : 0d;
            double winRateStandard = standard.Count > 0 ? standard.Count(r => r.EndType == "victory") / (double)standard.Count : 0d;
            double winRateHard = hard.Count > 0 ? hard.Count(r => r.EndType == "victory") / (double)hard.Count : 0d;
            double winRateOverall = totalRuns > 0 ? records.Count(r => r.EndType == "victory") / totalRuns : 0d;

            double avgEndTurnOverall = totalRuns > 0 ? records.Average(r => r.EndTurn) : 0d;
            double avgEndTurnStandard = standard.Count > 0 ? standard.Average(r => r.EndTurn) : 0d;
            double attritionRate = totalRuns > 0 ? records.Count(r => string.Equals(r.EndReason, "attrition", StringComparison.OrdinalIgnoreCase)) / totalRuns : 0d;

            double goldCv = totalRuns > 0 ? records.Average(r => r.GoldLeafCv) : 0d;
            double fireOilCv = totalRuns > 0 ? records.Average(r => r.FireOilCv) : 0d;
            double armsCv = totalRuns > 0 ? records.Average(r => r.ArmsCv) : 0d;

            double victoryShare = totalRuns > 0 ? records.Count(r => r.EndType == "victory") / totalRuns : 0d;
            double defeatShare = totalRuns > 0 ? records.Count(r => r.EndType == "defeat") / totalRuns : 0d;

            var victoryRecords = records.Where(r => r.EndType == "victory").ToList();
            double singlePathMonopoly = 0d;
            string topPath = "(none)";
            int topPathCount = 0;
            if (victoryRecords.Count > 0)
            {
                var topGroup = victoryRecords
                    .GroupBy(r => string.IsNullOrWhiteSpace(r.VictoryPath) ? "unknown" : r.VictoryPath)
                    .OrderByDescending(g => g.Count())
                    .First();

                singlePathMonopoly = topGroup.Count() / (double)victoryRecords.Count;
                topPath = topGroup.Key;
                topPathCount = topGroup.Count();
            }

            var metrics = new[]
            {
                BuildMetric("WIN_RATE_EASY", winRateEasy, 0.65, 0.80),
                BuildMetric("WIN_RATE_STANDARD", winRateStandard, 0.45, 0.55),
                BuildMetric("WIN_RATE_HARD", winRateHard, 0.20, 0.35),
                BuildMetric("WIN_RATE_OVERALL", winRateOverall, 0.40, 0.60),
                BuildMetric("AVG_END_TURN_OVERALL", avgEndTurnOverall, 14.0, 20.0),
                BuildMetric("AVG_END_TURN_STANDARD", avgEndTurnStandard, 15.0, 19.0),
                BuildMetric("ATTRITION_RATE_OVERALL", attritionRate, 0.10, 0.35),
                BuildMetric("RESOURCE_CV_GOLDLEAF", goldCv, 0.18, 0.45),
                BuildMetric("RESOURCE_CV_FIREOIL", fireOilCv, 0.15, 0.40),
                BuildMetric("RESOURCE_CV_ARMS", armsCv, 0.20, 0.55),
                BuildMetric("VICTORY_SHARE_OVERALL", victoryShare, 0.35, 0.65),
                BuildMetric("DEFEAT_SHARE_OVERALL", defeatShare, 0.33, 0.65),
                BuildMetric("SINGLE_PATH_VICTORY_MONOPOLY", singlePathMonopoly, 0.00, 0.70)
            };

            return new KpiSnapshot
            {
                Label = label,
                Metrics = metrics,
                PassCount = metrics.Count(m => m.Pass),
                TotalCount = metrics.Length,
                Runs = records.Count,
                WinRateEasy = winRateEasy,
                WinRateStandard = winRateStandard,
                WinRateHard = winRateHard,
                WinRateOverall = winRateOverall,
                AvgEndTurnOverall = avgEndTurnOverall,
                AvgEndTurnStandard = avgEndTurnStandard,
                AttritionRate = attritionRate,
                GoldCv = goldCv,
                FireOilCv = fireOilCv,
                ArmsCv = armsCv,
                VictoryShare = victoryShare,
                DefeatShare = defeatShare,
                SinglePathMonopoly = singlePathMonopoly,
                VictoryCount = records.Count(r => r.EndType == "victory"),
                DefeatCount = records.Count(r => r.EndType == "defeat"),
                TimeoutCount = records.Count(r => r.EndType == "timeout"),
                TopVictoryPath = topPath,
                TopVictoryPathCount = topPathCount
            };
        }

        private static KpiMetric BuildMetric(string id, double actual, double min, double max)
        {
            return new KpiMetric
            {
                Id = id,
                Actual = actual,
                Min = min,
                Max = max,
                Pass = actual >= min && actual <= max
            };
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

        private static void WriteRoundKpiMarkdown(List<SimulationRecord> records, KpiSnapshot snapshot, TuningProfile profile, string markdownPath, string csvPath)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"# R3-04 KPI Evaluation ({profile.RoundCode})");
            sb.AppendLine();
            sb.AppendLine($"- Batch: `{profile.BatchId}`");
            sb.AppendLine($"- Profile: `{profile.Label}`");
            sb.AppendLine($"- Runs: `{records.Count}`");
            sb.AppendLine($"- Source CSV: `{csvPath}`");
            sb.AppendLine($"- KPI Pass: `{snapshot.PassCount}/{snapshot.TotalCount}`");
            sb.AppendLine();

            sb.AppendLine("## Round Parameter Delta");
            sb.AppendLine();
            sb.AppendLine($"- Bias (Easy/Standard/Hard): `{profile.EasyBias:0.00}/{profile.StandardBias:0.00}/{profile.HardBias:0.00}`");
            sb.AppendLine($"- Init Scale (Easy/Standard/Hard): `{profile.EasyInitScale:0.00}/{profile.StandardInitScale:0.00}/{profile.HardInitScale:0.00}`");
            sb.AppendLine($"- Social/Trade Init Scale: `{profile.SocialTradeInitScale:0.00}`");
            sb.AppendLine($"- Victory Threshold (Easy/Standard/Hard): `{profile.EasyVictoryThreshold:0}/{profile.StandardVictoryThreshold:0}/{profile.HardVictoryThreshold:0}`");
            sb.AppendLine($"- Pressure Offset/TurnScale: `{profile.PressureOffset:0.00}/{profile.PressureTurnScale:0.000}`; Conflict Base/TurnScale: `{profile.ConflictBase:0.000}/{profile.ConflictTurnScale:0.000}`");
            sb.AppendLine($"- Delta Scale/Noise Scale: `{profile.ResourceDeltaScale:0.00}/{profile.ResourceNoiseScale:0.00}`");
            sb.AppendLine($"- Early Suppression: `turn<= {profile.EarlySuppressionTurns}` with `positive*{profile.EarlyPositiveGainScale:0.00}`");
            sb.AppendLine($"- Shock: `chance={profile.ShockChance:0.00}`, `magnitude=+/-{profile.ShockMagnitude}`");
            sb.AppendLine($"- Late Escalation: `startTurn={profile.LatePressureStartTurn}`, `pressure+={profile.LatePressureTurnBonus:0.000}`, `conflict+={profile.LateConflictTurnBonus:0.000}`, `loss+={profile.LateLossChanceBonus:0.000}`, `capture-={profile.LateCaptureChancePenalty:0.000}`");
            sb.AppendLine();

            sb.AppendLine("## KPI Snapshot");
            sb.AppendLine();
            sb.AppendLine("| KPI | Actual | Target | Status |");
            sb.AppendLine("|---|---:|---:|---|");
            foreach (var metric in snapshot.Metrics)
            {
                sb.AppendLine(
                    $"| {metric.Id} | {metric.Actual.ToString("0.0000", CultureInfo.InvariantCulture)} | " +
                    $"[{metric.Min.ToString("0.0000", CultureInfo.InvariantCulture)}, {metric.Max.ToString("0.0000", CultureInfo.InvariantCulture)}] | {(metric.Pass ? "PASS" : "FAIL")} |");
            }

            sb.AppendLine();
            sb.AppendLine("## Endgame Distribution");
            sb.AppendLine();
            sb.AppendLine("| End Type | Count | Share |");
            sb.AppendLine("|---|---:|---:|");
            AppendDistributionRow(sb, "victory", snapshot.VictoryCount, snapshot.Runs);
            AppendDistributionRow(sb, "defeat", snapshot.DefeatCount, snapshot.Runs);
            AppendDistributionRow(sb, "timeout", snapshot.TimeoutCount, snapshot.Runs);
            sb.AppendLine();
            sb.AppendLine($"- Top victory path: `{snapshot.TopVictoryPath}` (`{snapshot.TopVictoryPathCount}` wins, share `{snapshot.SinglePathMonopoly.ToString("0.0000", CultureInfo.InvariantCulture)}`)");

            File.WriteAllText(markdownPath, sb.ToString(), new UTF8Encoding(false));
        }

        private static void WriteImpactReport(List<RoundResult> results, string reportPath)
        {
            var baseline = CreateRun001BaselineSnapshot();
            var sb = new StringBuilder();
            sb.AppendLine("# R3-04 Parameter Tuning Impact Report");
            sb.AppendLine();
            sb.AppendLine("- Stage: R3 Balance and AI Tuning");
            sb.AppendLine("- Task: R3-04 Parameter tuning iterations (3 rounds)");
            sb.AppendLine("- Date: " + DateTime.Now.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
            sb.AppendLine();

            sb.AppendLine("## 1. KPI Gate Score");
            sb.AppendLine();
            sb.AppendLine("| Round | KPI Pass | Win(E/S/H) | AvgEndTurn | Attrition | PathMonopoly |");
            sb.AppendLine("|---|---:|---:|---:|---:|---:|");
            sb.AppendLine($"| R3-02 Run001 baseline | {baseline.PassCount}/{baseline.TotalCount} | {baseline.WinRateEasy:0.0000}/{baseline.WinRateStandard:0.0000}/{baseline.WinRateHard:0.0000} | {baseline.AvgEndTurnOverall:0.0000} | {baseline.AttritionRate:0.0000} | {baseline.SinglePathMonopoly:0.0000} |");
            foreach (var result in results)
            {
                var s = result.Snapshot;
                sb.AppendLine($"| {result.Profile.RoundCode} ({result.Profile.Label}) | {s.PassCount}/{s.TotalCount} | {s.WinRateEasy:0.0000}/{s.WinRateStandard:0.0000}/{s.WinRateHard:0.0000} | {s.AvgEndTurnOverall:0.0000} | {s.AttritionRate:0.0000} | {s.SinglePathMonopoly:0.0000} |");
            }

            sb.AppendLine();
            sb.AppendLine("## 2. Round-by-Round Delta");
            sb.AppendLine();
            for (int i = 0; i < results.Count; i++)
            {
                var current = results[i];
                var previous = i == 0 ? baseline : results[i - 1].Snapshot;
                var currentSnapshot = current.Snapshot;

                sb.AppendLine($"### {current.Profile.RoundCode} - {current.Profile.Label}");
                sb.AppendLine();
                sb.AppendLine($"- KPI pass delta: `{currentSnapshot.PassCount - previous.PassCount:+#;-#;0}`");
                sb.AppendLine($"- Overall win rate delta: `{currentSnapshot.WinRateOverall - previous.WinRateOverall:+0.0000;-0.0000;0.0000}`");
                sb.AppendLine($"- Hard win rate delta: `{currentSnapshot.WinRateHard - previous.WinRateHard:+0.0000;-0.0000;0.0000}`");
                sb.AppendLine($"- Avg end turn delta: `{currentSnapshot.AvgEndTurnOverall - previous.AvgEndTurnOverall:+0.0000;-0.0000;0.0000}`");
                sb.AppendLine($"- Attrition delta: `{currentSnapshot.AttritionRate - previous.AttritionRate:+0.0000;-0.0000;0.0000}`");
                sb.AppendLine($"- Single-path monopoly delta: `{currentSnapshot.SinglePathMonopoly - previous.SinglePathMonopoly:+0.0000;-0.0000;0.0000}`");
                sb.AppendLine($"- Profile knobs: `bias={current.Profile.EasyBias:0.00}/{current.Profile.StandardBias:0.00}/{current.Profile.HardBias:0.00}`, `threshold={current.Profile.EasyVictoryThreshold:0}/{current.Profile.StandardVictoryThreshold:0}/{current.Profile.HardVictoryThreshold:0}`, `deltaScale={current.Profile.ResourceDeltaScale:0.00}`, `noise={current.Profile.ResourceNoiseScale:0.00}`, `shock={current.Profile.ShockChance:0.00}x{current.Profile.ShockMagnitude}`, `late={current.Profile.LatePressureStartTurn}|p{current.Profile.LatePressureTurnBonus:0.000}|c{current.Profile.LateConflictTurnBonus:0.000}|l{current.Profile.LateLossChanceBonus:0.000}|cap{current.Profile.LateCaptureChancePenalty:0.000}`");
                sb.AppendLine($"- Artifacts: `{current.CsvPath}`, `{current.KpiPath}`");
                sb.AppendLine();
            }

            sb.AppendLine("## 3. Best Round Selection");
            sb.AppendLine();
            var best = results
                .OrderByDescending(r => r.Snapshot.PassCount)
                .ThenBy(r => Math.Abs(r.Snapshot.WinRateStandard - 0.50))
                .ThenBy(r => Math.Abs(r.Snapshot.AvgEndTurnOverall - 17.0))
                .First();
            sb.AppendLine($"- Selected best round: `{best.Profile.RoundCode} ({best.Profile.Label})`");
            sb.AppendLine($"- Reason: highest KPI pass count (`{best.Snapshot.PassCount}/{best.Snapshot.TotalCount}`), then closest to standard win-rate / mid-turn pacing targets.");
            sb.AppendLine();

            sb.AppendLine("## 4. Remaining Gaps");
            sb.AppendLine();
            var failed = best.Snapshot.Metrics.Where(m => !m.Pass).Select(m => m.Id).ToArray();
            if (failed.Length == 0)
            {
                sb.AppendLine("- No KPI hard-gate failures in best round.");
            }
            else
            {
                sb.AppendLine("- Failed KPI IDs in best round: " + string.Join(", ", failed));
            }

            File.WriteAllText(reportPath, sb.ToString(), new UTF8Encoding(false));
        }

        private static KpiSnapshot CreateRun001BaselineSnapshot()
        {
            // Baseline from R3-02 run 001 summary.
            var metrics = new[]
            {
                BuildMetric("WIN_RATE_EASY", 1.0000, 0.65, 0.80),
                BuildMetric("WIN_RATE_STANDARD", 0.9700, 0.45, 0.55),
                BuildMetric("WIN_RATE_HARD", 0.0000, 0.20, 0.35),
                BuildMetric("WIN_RATE_OVERALL", 0.6880, 0.40, 0.60),
                BuildMetric("AVG_END_TURN_OVERALL", 3.4620, 14.0, 20.0),
                BuildMetric("AVG_END_TURN_STANDARD", 3.2550, 15.0, 19.0),
                BuildMetric("ATTRITION_RATE_OVERALL", 0.0040, 0.10, 0.35),
                BuildMetric("RESOURCE_CV_GOLDLEAF", 0.0679, 0.18, 0.45),
                BuildMetric("RESOURCE_CV_FIREOIL", 0.0287, 0.15, 0.40),
                BuildMetric("RESOURCE_CV_ARMS", 0.0379, 0.20, 0.55),
                BuildMetric("VICTORY_SHARE_OVERALL", 0.6880, 0.35, 0.65),
                BuildMetric("DEFEAT_SHARE_OVERALL", 0.3080, 0.33, 0.65),
                BuildMetric("SINGLE_PATH_VICTORY_MONOPOLY", 0.9055, 0.00, 0.70)
            };

            return new KpiSnapshot
            {
                Label = "R3-02 Run001",
                Metrics = metrics,
                PassCount = metrics.Count(m => m.Pass),
                TotalCount = metrics.Length,
                Runs = 1000,
                WinRateEasy = 1.0000,
                WinRateStandard = 0.9700,
                WinRateHard = 0.0000,
                WinRateOverall = 0.6880,
                AvgEndTurnOverall = 3.4620,
                AvgEndTurnStandard = 3.2550,
                AttritionRate = 0.0040,
                GoldCv = 0.0679,
                FireOilCv = 0.0287,
                ArmsCv = 0.0379,
                VictoryShare = 0.6880,
                DefeatShare = 0.3080,
                SinglePathMonopoly = 0.9055,
                VictoryCount = 688,
                DefeatCount = 308,
                TimeoutCount = 4,
                TopVictoryPath = "energyliberation",
                TopVictoryPathCount = 623
            };
        }

        private static void WriteEvidenceReadme(string evidenceDir, List<RoundResult> results, string reportPath)
        {
            string readmePath = Path.Combine(evidenceDir, "README.md");
            var sb = new StringBuilder();
            sb.AppendLine("# R3 Tuning Evidence");
            sb.AppendLine();
            sb.AppendLine("- Stage: R3 Balance and AI Tuning");
            sb.AppendLine("- Task: R3-04 parameter tuning iterations");
            sb.AppendLine("- Date: " + DateTime.Now.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
            sb.AppendLine();
            sb.AppendLine("## Evidence Index");
            sb.AppendLine();
            sb.AppendLine("| Evidence | Description | Path |");
            sb.AppendLine("|---|---|---|");
            foreach (var round in results)
            {
                string csvName = Path.GetFileName(round.CsvPath);
                string kpiName = Path.GetFileName(round.KpiPath);
                sb.AppendLine($"| {round.Profile.RoundCode} CSV | 1000-run raw result table ({round.Profile.Label}) | `production/evidence/r3/tuning/{csvName}` |");
                sb.AppendLine($"| {round.Profile.RoundCode} KPI | KPI snapshot for {round.Profile.Label} | `production/evidence/r3/tuning/{kpiName}` |");
            }

            sb.AppendLine($"| Impact report | Round-by-round delta and best-round selection | `production/evidence/r3/tuning/{Path.GetFileName(reportPath)}` |");
            File.WriteAllText(readmePath, sb.ToString(), new UTF8Encoding(false));
        }

        private static void WriteR305FinalReport(List<RoundResult> validationRuns, string reportPath, string summaryJsonPath, string baseProfileLabel)
        {
            if (validationRuns == null || validationRuns.Count == 0)
            {
                return;
            }

            int runCount = validationRuns.Count;
            var allMetricIds = validationRuns[0].Snapshot.Metrics.Select(metric => metric.Id).ToArray();
            bool strictPass = validationRuns.All(run => run.Snapshot.PassCount == run.Snapshot.TotalCount);

            var sb = new StringBuilder();
            sb.AppendLine("# R3-05 Final Balance Report");
            sb.AppendLine();
            sb.AppendLine("- Stage: R3 Balance and AI Tuning");
            sb.AppendLine("- Task: R3-05 balance regression validation");
            sb.AppendLine("- Date: " + DateTime.Now.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
            sb.AppendLine($"- Validation base profile: `{baseProfileLabel}`");
            sb.AppendLine($"- Verdict: `{(strictPass ? "PASS" : "FAIL")}`");
            sb.AppendLine();

            sb.AppendLine("## 1. Run-Level KPI Score");
            sb.AppendLine();
            sb.AppendLine("| Run | Batch | KPI Pass | Win(E/S/H) | AvgEndTurn | Attrition | PathMonopoly |");
            sb.AppendLine("|---|---|---:|---:|---:|---:|---:|");
            foreach (var run in validationRuns)
            {
                var snapshot = run.Snapshot;
                sb.AppendLine($"| {run.Profile.RoundCode} | {run.Profile.BatchId} | {snapshot.PassCount}/{snapshot.TotalCount} | {snapshot.WinRateEasy:0.0000}/{snapshot.WinRateStandard:0.0000}/{snapshot.WinRateHard:0.0000} | {snapshot.AvgEndTurnOverall:0.0000} | {snapshot.AttritionRate:0.0000} | {snapshot.SinglePathMonopoly:0.0000} |");
            }

            sb.AppendLine();
            sb.AppendLine("## 2. KPI Stability Matrix");
            sb.AppendLine();
            sb.AppendLine("| KPI | Mean Actual | Pass Runs | Target |");
            sb.AppendLine("|---|---:|---:|---:|");
            foreach (var id in allMetricIds)
            {
                var metricRuns = validationRuns
                    .Select(run => run.Snapshot.Metrics.First(metric => metric.Id == id))
                    .ToArray();
                double meanActual = metricRuns.Average(metric => metric.Actual);
                int passRuns = metricRuns.Count(metric => metric.Pass);
                sb.AppendLine(
                    $"| {id} | {meanActual.ToString("0.0000", CultureInfo.InvariantCulture)} | {passRuns}/{runCount} | " +
                    $"[{metricRuns[0].Min.ToString("0.0000", CultureInfo.InvariantCulture)}, {metricRuns[0].Max.ToString("0.0000", CultureInfo.InvariantCulture)}] |");
            }

            sb.AppendLine();
            sb.AppendLine("## 3. Gate Decision Rule");
            sb.AppendLine();
            sb.AppendLine("- Rule: all hard-gate KPIs must pass in all validation runs.");
            sb.AppendLine($"- Result: `{(strictPass ? "PASS" : "FAIL")}`");

            var failingIds = allMetricIds
                .Where(id => validationRuns.Any(run => !run.Snapshot.Metrics.First(metric => metric.Id == id).Pass))
                .ToArray();
            if (failingIds.Length == 0)
            {
                sb.AppendLine("- Failed KPI IDs: (none)");
            }
            else
            {
                sb.AppendLine("- Failed KPI IDs: " + string.Join(", ", failingIds));
            }

            sb.AppendLine();
            sb.AppendLine("## 4. Acceptance Conclusion");
            sb.AppendLine();
            if (strictPass)
            {
                sb.AppendLine("- R3-05 validation meets KPI gates. Ready to proceed to R3-06 sign-off.");
            }
            else
            {
                sb.AppendLine("- R3-05 validation does not meet KPI gates. Keep R3 in `not ready` state and continue targeted tuning.");
            }

            File.WriteAllText(reportPath, sb.ToString(), new UTF8Encoding(false));

            var jsonBuilder = new StringBuilder();
            jsonBuilder.AppendLine("{");
            jsonBuilder.AppendLine($"  \"stage\": \"R3-05\",");
            jsonBuilder.AppendLine($"  \"generatedOn\": \"{DateTime.Now.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)}\",");
            jsonBuilder.AppendLine($"  \"validationRuns\": {runCount},");
            jsonBuilder.AppendLine($"  \"baseProfile\": \"{baseProfileLabel}\",");
            jsonBuilder.AppendLine($"  \"verdict\": \"{(strictPass ? "PASS" : "FAIL")}\",");
            jsonBuilder.AppendLine("  \"runKpiPass\": [");
            for (int i = 0; i < validationRuns.Count; i++)
            {
                var run = validationRuns[i];
                jsonBuilder.Append($"    {{ \"run\": \"{run.Profile.RoundCode}\", \"batch\": \"{run.Profile.BatchId}\", \"pass\": {run.Snapshot.PassCount}, \"total\": {run.Snapshot.TotalCount} }}");
                jsonBuilder.AppendLine(i == validationRuns.Count - 1 ? string.Empty : ",");
            }
            jsonBuilder.AppendLine("  ],");
            jsonBuilder.AppendLine("  \"failingKpiIds\": [");
            for (int i = 0; i < failingIds.Length; i++)
            {
                jsonBuilder.Append($"    \"{failingIds[i]}\"");
                jsonBuilder.AppendLine(i == failingIds.Length - 1 ? string.Empty : ",");
            }
            jsonBuilder.AppendLine("  ]");
            jsonBuilder.AppendLine("}");
            File.WriteAllText(summaryJsonPath, jsonBuilder.ToString(), new UTF8Encoding(false));
        }

        private static void WriteR305EvidenceReadme(string evidenceDir, List<RoundResult> validationRuns, string finalReportPath, string summaryJsonPath)
        {
            string readmePath = Path.Combine(evidenceDir, "README.md");
            var sb = new StringBuilder();
            sb.AppendLine("# R3 Validation Evidence");
            sb.AppendLine();
            sb.AppendLine("- Stage: R3 Balance and AI Tuning");
            sb.AppendLine("- Task: R3-05 balance regression validation");
            sb.AppendLine("- Date: " + DateTime.Now.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
            sb.AppendLine();
            sb.AppendLine("## Evidence Index");
            sb.AppendLine();
            sb.AppendLine("| Evidence | Description | Path |");
            sb.AppendLine("|---|---|---|");
            foreach (var run in validationRuns)
            {
                string csvName = Path.GetFileName(run.CsvPath);
                string kpiName = Path.GetFileName(run.KpiPath);
                sb.AppendLine($"| {run.Profile.RoundCode} CSV | 1000-run validation raw result table | `production/evidence/r3/validation/{csvName}` |");
                sb.AppendLine($"| {run.Profile.RoundCode} KPI | KPI snapshot for validation run | `production/evidence/r3/validation/{kpiName}` |");
            }

            sb.AppendLine($"| Final report | R3-05 final balance gate decision | `production/evidence/r3/validation/{Path.GetFileName(finalReportPath)}` |");
            sb.AppendLine($"| Summary JSON | Machine-readable validation verdict | `production/evidence/r3/validation/{Path.GetFileName(summaryJsonPath)}` |");
            File.WriteAllText(readmePath, sb.ToString(), new UTF8Encoding(false));
        }

        private static void AppendDistributionRow(StringBuilder sb, string label, int count, int total)
        {
            double share = total > 0 ? count / (double)total : 0d;
            sb.AppendLine($"| {label} | {count} | {share.ToString("0.0000", CultureInfo.InvariantCulture)} |");
        }

        private static string GetEvidenceDirectory()
        {
            string projectRoot = GetProjectRoot();
            string path = Path.Combine(projectRoot, "production", "evidence", "r3", "tuning");
            Directory.CreateDirectory(path);
            return path;
        }

        private static string GetValidationEvidenceDirectory()
        {
            string projectRoot = GetProjectRoot();
            string path = Path.Combine(projectRoot, "production", "evidence", "r3", "validation");
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

        private static float GetDifficultyInitScale(TuningProfile profile, SimulationDifficulty difficulty)
        {
            switch (difficulty)
            {
                case SimulationDifficulty.Easy: return profile.EasyInitScale;
                case SimulationDifficulty.Hard: return profile.HardInitScale;
                default: return profile.StandardInitScale;
            }
        }

        private static float GetDifficultyBias(SimulationDifficulty difficulty, TuningProfile profile)
        {
            switch (difficulty)
            {
                case SimulationDifficulty.Easy: return profile.EasyBias;
                case SimulationDifficulty.Hard: return profile.HardBias;
                default: return profile.StandardBias;
            }
        }

        private static void ApplyEarlyGainSuppression(ref int delta, int turn, TuningProfile profile)
        {
            if (turn > profile.EarlySuppressionTurns || delta <= 0)
            {
                return;
            }

            delta = Mathf.RoundToInt(delta * profile.EarlyPositiveGainScale);
        }

        private static void ApplyShock(ref int delta, TuningProfile profile, System.Random rng)
        {
            if (profile.ShockChance <= 0f || profile.ShockMagnitude <= 0)
            {
                return;
            }

            if (rng.NextDouble() > profile.ShockChance)
            {
                return;
            }

            delta += NextInt(rng, -profile.ShockMagnitude, profile.ShockMagnitude);
        }

        private static float ResolveDurationPerTurn(SimulationDifficulty difficulty, TuningProfile profile)
        {
            switch (difficulty)
            {
                case SimulationDifficulty.Easy: return profile.EasyDurationPerTurn;
                case SimulationDifficulty.Hard: return profile.HardDurationPerTurn;
                default: return profile.StandardDurationPerTurn;
            }
        }

        private static float EstimateDurationMinutes(int endTurn, SimulationDifficulty difficulty, System.Random rng, TuningProfile profile)
        {
            float basePerTurn = ResolveDurationPerTurn(difficulty, profile);
            float jitter = NextFloat(rng, -3f, 3f);
            return Mathf.Max(30f, endTurn * (basePerTurn + jitter));
        }

        private static float ResolveBlockadePenalty(BlockadeLevel level, float none, float unilateral, float multilateral, float total)
        {
            switch (level)
            {
                case BlockadeLevel.None: return none;
                case BlockadeLevel.Unilateral: return unilateral;
                case BlockadeLevel.Multilateral: return multilateral;
                case BlockadeLevel.Total: return total;
                default: return none;
            }
        }

        private static float GetGoldPenalty(BlockadeLevel level)
        {
            return ResolveBlockadePenalty(level, 0f, 8f, 20f, 32f);
        }

        private static float GetFireOilPenalty(BlockadeLevel level)
        {
            return ResolveBlockadePenalty(level, 0f, 2f, 5f, 8f);
        }

        private static BlockadeLevel ResolveBlockadeLevel(float pressureSignal)
        {
            if (pressureSignal < 0.25f) return BlockadeLevel.None;
            if (pressureSignal < 0.55f) return BlockadeLevel.Unilateral;
            if (pressureSignal < 0.85f) return BlockadeLevel.Multilateral;
            return BlockadeLevel.Total;
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

        private static List<SimulationDifficulty> BuildDifficultyPlan()
        {
            var plan = new List<SimulationDifficulty>(kTotalRuns);
            for (int i = 0; i < kEasyRuns; i++) plan.Add(SimulationDifficulty.Easy);
            for (int i = 0; i < kStandardRuns; i++) plan.Add(SimulationDifficulty.Standard);
            for (int i = 0; i < kHardRuns; i++) plan.Add(SimulationDifficulty.Hard);
            return plan;
        }

        private static string ResolveRunPrefix(TuningProfile profile)
        {
            string batchId = profile?.BatchId ?? string.Empty;
            if (batchId.StartsWith("R3-05", StringComparison.OrdinalIgnoreCase))
            {
                return "R3-05";
            }

            return "R3-04";
        }

        private static TuningProfile CloneProfile(TuningProfile source)
        {
            if (source == null)
            {
                return null;
            }

            return new TuningProfile
            {
                RoundCode = source.RoundCode,
                BatchId = source.BatchId,
                Label = source.Label,
                EasyBias = source.EasyBias,
                StandardBias = source.StandardBias,
                HardBias = source.HardBias,
                EasyInitScale = source.EasyInitScale,
                StandardInitScale = source.StandardInitScale,
                HardInitScale = source.HardInitScale,
                SocialTradeInitScale = source.SocialTradeInitScale,
                EasyVictoryThreshold = source.EasyVictoryThreshold,
                StandardVictoryThreshold = source.StandardVictoryThreshold,
                HardVictoryThreshold = source.HardVictoryThreshold,
                EasyMilitaryCollapseThreshold = source.EasyMilitaryCollapseThreshold,
                StandardMilitaryCollapseThreshold = source.StandardMilitaryCollapseThreshold,
                HardMilitaryCollapseThreshold = source.HardMilitaryCollapseThreshold,
                PressureOffset = source.PressureOffset,
                PressureTurnScale = source.PressureTurnScale,
                ConflictBase = source.ConflictBase,
                ConflictTurnScale = source.ConflictTurnScale,
                ResourceDeltaScale = source.ResourceDeltaScale,
                ResourceNoiseScale = source.ResourceNoiseScale,
                RelationDeltaScale = source.RelationDeltaScale,
                SatisfactionScale = source.SatisfactionScale,
                EarlySuppressionTurns = source.EarlySuppressionTurns,
                EarlyPositiveGainScale = source.EarlyPositiveGainScale,
                ShockChance = source.ShockChance,
                ShockMagnitude = source.ShockMagnitude,
                CaptureChanceScale = source.CaptureChanceScale,
                LossChanceScale = source.LossChanceScale,
                NonConflictNodeScale = source.NonConflictNodeScale,
                NodeDriftScale = source.NodeDriftScale,
                LatePressureStartTurn = source.LatePressureStartTurn,
                LatePressureTurnBonus = source.LatePressureTurnBonus,
                LateConflictTurnBonus = source.LateConflictTurnBonus,
                LateLossChanceBonus = source.LateLossChanceBonus,
                LateCaptureChancePenalty = source.LateCaptureChancePenalty,
                EasyDurationPerTurn = source.EasyDurationPerTurn,
                StandardDurationPerTurn = source.StandardDurationPerTurn,
                HardDurationPerTurn = source.HardDurationPerTurn
            };
        }

        private static List<TuningProfile> BuildR304Profiles()
        {
            return new List<TuningProfile>
            {
                new TuningProfile
                {
                    RoundCode = "R01",
                    BatchId = "R3-04-B001-R01",
                    Label = "decelerate-window",
                    EasyBias = 0.06f,
                    StandardBias = -0.02f,
                    HardBias = -0.06f,
                    EasyInitScale = 0.85f,
                    StandardInitScale = 0.78f,
                    HardInitScale = 0.92f,
                    SocialTradeInitScale = 0.55f,
                    EasyVictoryThreshold = 90f,
                    StandardVictoryThreshold = 92f,
                    HardVictoryThreshold = 86f,
                    EasyMilitaryCollapseThreshold = 18,
                    StandardMilitaryCollapseThreshold = 24,
                    HardMilitaryCollapseThreshold = 28,
                    PressureOffset = 0.20f,
                    PressureTurnScale = 0.20f,
                    ConflictBase = 0.28f,
                    ConflictTurnScale = 0.008f,
                    ResourceDeltaScale = 0.80f,
                    ResourceNoiseScale = 1.40f,
                    RelationDeltaScale = 1.10f,
                    SatisfactionScale = 1.05f,
                    EarlySuppressionTurns = 8,
                    EarlyPositiveGainScale = 0.45f,
                    ShockChance = 0.20f,
                    ShockMagnitude = 18,
                    CaptureChanceScale = 0.80f,
                    LossChanceScale = 0.90f,
                    NonConflictNodeScale = 0.32f,
                    NodeDriftScale = 1.20f,
                    EasyDurationPerTurn = 24f,
                    StandardDurationPerTurn = 29f,
                    HardDurationPerTurn = 33f
                },
                new TuningProfile
                {
                    RoundCode = "R02",
                    BatchId = "R3-04-B001-R02",
                    Label = "stability-mix",
                    EasyBias = 0.10f,
                    StandardBias = 0.00f,
                    HardBias = -0.03f,
                    EasyInitScale = 0.92f,
                    StandardInitScale = 0.86f,
                    HardInitScale = 0.95f,
                    SocialTradeInitScale = 0.65f,
                    EasyVictoryThreshold = 86f,
                    StandardVictoryThreshold = 88f,
                    HardVictoryThreshold = 80f,
                    EasyMilitaryCollapseThreshold = 20,
                    StandardMilitaryCollapseThreshold = 26,
                    HardMilitaryCollapseThreshold = 30,
                    PressureOffset = 0.14f,
                    PressureTurnScale = 0.18f,
                    ConflictBase = 0.30f,
                    ConflictTurnScale = 0.009f,
                    ResourceDeltaScale = 0.90f,
                    ResourceNoiseScale = 1.30f,
                    RelationDeltaScale = 1.00f,
                    SatisfactionScale = 1.00f,
                    EarlySuppressionTurns = 6,
                    EarlyPositiveGainScale = 0.55f,
                    ShockChance = 0.18f,
                    ShockMagnitude = 16,
                    CaptureChanceScale = 0.90f,
                    LossChanceScale = 0.85f,
                    NonConflictNodeScale = 0.36f,
                    NodeDriftScale = 1.10f,
                    EasyDurationPerTurn = 23f,
                    StandardDurationPerTurn = 28f,
                    HardDurationPerTurn = 32f
                },
                new TuningProfile
                {
                    RoundCode = "R03",
                    BatchId = "R3-04-B001-R03",
                    Label = "target-seek",
                    EasyBias = 0.12f,
                    StandardBias = 0.02f,
                    HardBias = -0.01f,
                    EasyInitScale = 0.95f,
                    StandardInitScale = 0.90f,
                    HardInitScale = 1.00f,
                    SocialTradeInitScale = 0.70f,
                    EasyVictoryThreshold = 84f,
                    StandardVictoryThreshold = 85f,
                    HardVictoryThreshold = 78f,
                    EasyMilitaryCollapseThreshold = 22,
                    StandardMilitaryCollapseThreshold = 28,
                    HardMilitaryCollapseThreshold = 32,
                    PressureOffset = 0.10f,
                    PressureTurnScale = 0.16f,
                    ConflictBase = 0.31f,
                    ConflictTurnScale = 0.010f,
                    ResourceDeltaScale = 0.95f,
                    ResourceNoiseScale = 1.25f,
                    RelationDeltaScale = 0.95f,
                    SatisfactionScale = 0.98f,
                    EarlySuppressionTurns = 5,
                    EarlyPositiveGainScale = 0.62f,
                    ShockChance = 0.15f,
                    ShockMagnitude = 14,
                    CaptureChanceScale = 1.00f,
                    LossChanceScale = 0.80f,
                    NonConflictNodeScale = 0.40f,
                    NodeDriftScale = 1.00f,
                    EasyDurationPerTurn = 22f,
                    StandardDurationPerTurn = 27f,
                    HardDurationPerTurn = 31f
                }
            };
        }

        private static List<TuningProfile> BuildR304TargetedProfiles()
        {
            return new List<TuningProfile>
            {
                new TuningProfile
                {
                    RoundCode = "R10",
                    BatchId = "R3-04B-B003-R10",
                    Label = "standard-revival-a",
                    EasyBias = 0.06f,
                    StandardBias = 0.09f,
                    HardBias = 0.00f,
                    EasyInitScale = 0.92f,
                    StandardInitScale = 0.98f,
                    HardInitScale = 0.98f,
                    SocialTradeInitScale = 0.66f,
                    EasyVictoryThreshold = 94f,
                    StandardVictoryThreshold = 84f,
                    HardVictoryThreshold = 79f,
                    EasyMilitaryCollapseThreshold = 14,
                    StandardMilitaryCollapseThreshold = 18,
                    HardMilitaryCollapseThreshold = 26,
                    PressureOffset = 0.11f,
                    PressureTurnScale = 0.17f,
                    ConflictBase = 0.32f,
                    ConflictTurnScale = 0.010f,
                    ResourceDeltaScale = 0.96f,
                    ResourceNoiseScale = 1.35f,
                    RelationDeltaScale = 1.08f,
                    SatisfactionScale = 0.93f,
                    EarlySuppressionTurns = 6,
                    EarlyPositiveGainScale = 0.60f,
                    ShockChance = 0.18f,
                    ShockMagnitude = 16,
                    CaptureChanceScale = 0.98f,
                    LossChanceScale = 0.85f,
                    NonConflictNodeScale = 0.38f,
                    NodeDriftScale = 1.10f,
                    EasyDurationPerTurn = 23f,
                    StandardDurationPerTurn = 28f,
                    HardDurationPerTurn = 32f
                },
                new TuningProfile
                {
                    RoundCode = "R11",
                    BatchId = "R3-04B-B003-R11",
                    Label = "standard-revival-b",
                    EasyBias = 0.04f,
                    StandardBias = 0.12f,
                    HardBias = 0.01f,
                    EasyInitScale = 0.90f,
                    StandardInitScale = 1.00f,
                    HardInitScale = 1.00f,
                    SocialTradeInitScale = 0.62f,
                    EasyVictoryThreshold = 92f,
                    StandardVictoryThreshold = 83f,
                    HardVictoryThreshold = 78f,
                    EasyMilitaryCollapseThreshold = 12,
                    StandardMilitaryCollapseThreshold = 16,
                    HardMilitaryCollapseThreshold = 24,
                    PressureOffset = 0.12f,
                    PressureTurnScale = 0.18f,
                    ConflictBase = 0.34f,
                    ConflictTurnScale = 0.011f,
                    ResourceDeltaScale = 0.98f,
                    ResourceNoiseScale = 1.45f,
                    RelationDeltaScale = 1.15f,
                    SatisfactionScale = 0.92f,
                    EarlySuppressionTurns = 6,
                    EarlyPositiveGainScale = 0.58f,
                    ShockChance = 0.20f,
                    ShockMagnitude = 18,
                    CaptureChanceScale = 1.02f,
                    LossChanceScale = 0.88f,
                    NonConflictNodeScale = 0.36f,
                    NodeDriftScale = 1.20f,
                    EasyDurationPerTurn = 23f,
                    StandardDurationPerTurn = 28f,
                    HardDurationPerTurn = 32f
                },
                new TuningProfile
                {
                    RoundCode = "R12",
                    BatchId = "R3-04B-B003-R12",
                    Label = "winrate-recover-diversify",
                    EasyBias = 0.05f,
                    StandardBias = 0.10f,
                    HardBias = 0.02f,
                    EasyInitScale = 0.90f,
                    StandardInitScale = 0.98f,
                    HardInitScale = 1.00f,
                    SocialTradeInitScale = 0.55f,
                    EasyVictoryThreshold = 91f,
                    StandardVictoryThreshold = 84f,
                    HardVictoryThreshold = 79f,
                    EasyMilitaryCollapseThreshold = 12,
                    StandardMilitaryCollapseThreshold = 16,
                    HardMilitaryCollapseThreshold = 24,
                    PressureOffset = 0.14f,
                    PressureTurnScale = 0.19f,
                    ConflictBase = 0.36f,
                    ConflictTurnScale = 0.012f,
                    ResourceDeltaScale = 0.97f,
                    ResourceNoiseScale = 1.55f,
                    RelationDeltaScale = 1.25f,
                    SatisfactionScale = 0.90f,
                    EarlySuppressionTurns = 7,
                    EarlyPositiveGainScale = 0.55f,
                    ShockChance = 0.22f,
                    ShockMagnitude = 20,
                    CaptureChanceScale = 1.08f,
                    LossChanceScale = 0.92f,
                    NonConflictNodeScale = 0.32f,
                    NodeDriftScale = 1.25f,
                    EasyDurationPerTurn = 23f,
                    StandardDurationPerTurn = 28f,
                    HardDurationPerTurn = 33f
                }
            };
        }

        private static List<TuningProfile> BuildR304CProfiles()
        {
            return new List<TuningProfile>
            {
                new TuningProfile
                {
                    RoundCode = "R13",
                    BatchId = "R3-04C-B001-R13",
                    Label = "easy-recover-balance",
                    EasyBias = 0.16f,
                    StandardBias = 0.08f,
                    HardBias = -0.03f,
                    EasyInitScale = 1.00f,
                    StandardInitScale = 1.00f,
                    HardInitScale = 0.95f,
                    SocialTradeInitScale = 0.48f,
                    EasyVictoryThreshold = 90f,
                    StandardVictoryThreshold = 88f,
                    HardVictoryThreshold = 83f,
                    EasyMilitaryCollapseThreshold = 10,
                    StandardMilitaryCollapseThreshold = 15,
                    HardMilitaryCollapseThreshold = 28,
                    PressureOffset = 0.16f,
                    PressureTurnScale = 0.20f,
                    ConflictBase = 0.35f,
                    ConflictTurnScale = 0.012f,
                    ResourceDeltaScale = 0.96f,
                    ResourceNoiseScale = 1.75f,
                    RelationDeltaScale = 1.35f,
                    SatisfactionScale = 0.88f,
                    EarlySuppressionTurns = 7,
                    EarlyPositiveGainScale = 0.52f,
                    ShockChance = 0.30f,
                    ShockMagnitude = 24,
                    CaptureChanceScale = 1.15f,
                    LossChanceScale = 0.95f,
                    NonConflictNodeScale = 0.30f,
                    NodeDriftScale = 1.35f,
                    EasyDurationPerTurn = 23f,
                    StandardDurationPerTurn = 29f,
                    HardDurationPerTurn = 33f
                },
                new TuningProfile
                {
                    RoundCode = "R14",
                    BatchId = "R3-04C-B001-R14",
                    Label = "duration-volatility",
                    EasyBias = 0.14f,
                    StandardBias = 0.06f,
                    HardBias = -0.04f,
                    EasyInitScale = 0.98f,
                    StandardInitScale = 0.96f,
                    HardInitScale = 0.93f,
                    SocialTradeInitScale = 0.44f,
                    EasyVictoryThreshold = 92f,
                    StandardVictoryThreshold = 90f,
                    HardVictoryThreshold = 84f,
                    EasyMilitaryCollapseThreshold = 11,
                    StandardMilitaryCollapseThreshold = 16,
                    HardMilitaryCollapseThreshold = 29,
                    PressureOffset = 0.20f,
                    PressureTurnScale = 0.22f,
                    ConflictBase = 0.37f,
                    ConflictTurnScale = 0.013f,
                    ResourceDeltaScale = 0.92f,
                    ResourceNoiseScale = 1.85f,
                    RelationDeltaScale = 1.45f,
                    SatisfactionScale = 0.90f,
                    EarlySuppressionTurns = 8,
                    EarlyPositiveGainScale = 0.46f,
                    ShockChance = 0.34f,
                    ShockMagnitude = 28,
                    CaptureChanceScale = 1.20f,
                    LossChanceScale = 1.00f,
                    NonConflictNodeScale = 0.26f,
                    NodeDriftScale = 1.45f,
                    EasyDurationPerTurn = 23f,
                    StandardDurationPerTurn = 29f,
                    HardDurationPerTurn = 34f
                },
                new TuningProfile
                {
                    RoundCode = "R15",
                    BatchId = "R3-04C-B001-R15",
                    Label = "overall-balance-candidate",
                    EasyBias = 0.18f,
                    StandardBias = 0.09f,
                    HardBias = -0.05f,
                    EasyInitScale = 1.04f,
                    StandardInitScale = 1.00f,
                    HardInitScale = 0.94f,
                    SocialTradeInitScale = 0.46f,
                    EasyVictoryThreshold = 90f,
                    StandardVictoryThreshold = 89f,
                    HardVictoryThreshold = 84f,
                    EasyMilitaryCollapseThreshold = 10,
                    StandardMilitaryCollapseThreshold = 15,
                    HardMilitaryCollapseThreshold = 29,
                    PressureOffset = 0.18f,
                    PressureTurnScale = 0.21f,
                    ConflictBase = 0.36f,
                    ConflictTurnScale = 0.012f,
                    ResourceDeltaScale = 0.95f,
                    ResourceNoiseScale = 1.90f,
                    RelationDeltaScale = 1.40f,
                    SatisfactionScale = 0.88f,
                    EarlySuppressionTurns = 7,
                    EarlyPositiveGainScale = 0.50f,
                    ShockChance = 0.32f,
                    ShockMagnitude = 26,
                    CaptureChanceScale = 1.18f,
                    LossChanceScale = 0.98f,
                    NonConflictNodeScale = 0.28f,
                    NodeDriftScale = 1.40f,
                    EasyDurationPerTurn = 23f,
                    StandardDurationPerTurn = 29f,
                    HardDurationPerTurn = 34f
                }
            };
        }

        private static List<TuningProfile> BuildR304DProfiles()
        {
            return new List<TuningProfile>
            {
                new TuningProfile
                {
                    RoundCode = "R19",
                    BatchId = "R3-04D-B002-R19",
                    Label = "axis-shift-balanced",
                    EasyBias = 0.18f,
                    StandardBias = 0.10f,
                    HardBias = 0.00f,
                    EasyInitScale = 1.04f,
                    StandardInitScale = 1.00f,
                    HardInitScale = 0.97f,
                    SocialTradeInitScale = 0.26f,
                    EasyVictoryThreshold = 88f,
                    StandardVictoryThreshold = 85f,
                    HardVictoryThreshold = 82f,
                    EasyMilitaryCollapseThreshold = 9,
                    StandardMilitaryCollapseThreshold = 13,
                    HardMilitaryCollapseThreshold = 20,
                    PressureOffset = 0.18f,
                    PressureTurnScale = 0.20f,
                    ConflictBase = 0.37f,
                    ConflictTurnScale = 0.013f,
                    ResourceDeltaScale = 0.93f,
                    ResourceNoiseScale = 2.00f,
                    RelationDeltaScale = 1.85f,
                    SatisfactionScale = 0.87f,
                    EarlySuppressionTurns = 7,
                    EarlyPositiveGainScale = 0.50f,
                    ShockChance = 0.40f,
                    ShockMagnitude = 32,
                    CaptureChanceScale = 1.75f,
                    LossChanceScale = 0.74f,
                    NonConflictNodeScale = 0.40f,
                    NodeDriftScale = 1.90f,
                    EasyDurationPerTurn = 23f,
                    StandardDurationPerTurn = 29f,
                    HardDurationPerTurn = 34f
                },
                new TuningProfile
                {
                    RoundCode = "R20",
                    BatchId = "R3-04D-B002-R20",
                    Label = "window-tighten",
                    EasyBias = 0.16f,
                    StandardBias = 0.09f,
                    HardBias = -0.01f,
                    EasyInitScale = 1.02f,
                    StandardInitScale = 0.99f,
                    HardInitScale = 0.95f,
                    SocialTradeInitScale = 0.24f,
                    EasyVictoryThreshold = 89f,
                    StandardVictoryThreshold = 86f,
                    HardVictoryThreshold = 83f,
                    EasyMilitaryCollapseThreshold = 10,
                    StandardMilitaryCollapseThreshold = 14,
                    HardMilitaryCollapseThreshold = 22,
                    PressureOffset = 0.20f,
                    PressureTurnScale = 0.21f,
                    ConflictBase = 0.38f,
                    ConflictTurnScale = 0.013f,
                    ResourceDeltaScale = 0.92f,
                    ResourceNoiseScale = 2.10f,
                    RelationDeltaScale = 1.95f,
                    SatisfactionScale = 0.86f,
                    EarlySuppressionTurns = 8,
                    EarlyPositiveGainScale = 0.46f,
                    ShockChance = 0.42f,
                    ShockMagnitude = 34,
                    CaptureChanceScale = 1.85f,
                    LossChanceScale = 0.72f,
                    NonConflictNodeScale = 0.38f,
                    NodeDriftScale = 2.00f,
                    EasyDurationPerTurn = 23f,
                    StandardDurationPerTurn = 30f,
                    HardDurationPerTurn = 34f
                },
                new TuningProfile
                {
                    RoundCode = "R21",
                    BatchId = "R3-04D-B002-R21",
                    Label = "win-floor-guard",
                    EasyBias = 0.20f,
                    StandardBias = 0.11f,
                    HardBias = 0.01f,
                    EasyInitScale = 1.06f,
                    StandardInitScale = 1.01f,
                    HardInitScale = 0.98f,
                    SocialTradeInitScale = 0.30f,
                    EasyVictoryThreshold = 88f,
                    StandardVictoryThreshold = 85f,
                    HardVictoryThreshold = 82f,
                    EasyMilitaryCollapseThreshold = 8,
                    StandardMilitaryCollapseThreshold = 12,
                    HardMilitaryCollapseThreshold = 19,
                    PressureOffset = 0.17f,
                    PressureTurnScale = 0.19f,
                    ConflictBase = 0.36f,
                    ConflictTurnScale = 0.012f,
                    ResourceDeltaScale = 0.94f,
                    ResourceNoiseScale = 1.95f,
                    RelationDeltaScale = 1.80f,
                    SatisfactionScale = 0.88f,
                    EarlySuppressionTurns = 7,
                    EarlyPositiveGainScale = 0.52f,
                    ShockChance = 0.38f,
                    ShockMagnitude = 30,
                    CaptureChanceScale = 1.70f,
                    LossChanceScale = 0.76f,
                    NonConflictNodeScale = 0.42f,
                    NodeDriftScale = 1.85f,
                    EasyDurationPerTurn = 23f,
                    StandardDurationPerTurn = 29f,
                    HardDurationPerTurn = 33f
                }
            };
        }

        private static List<TuningProfile> BuildR304EProfiles()
        {
            return new List<TuningProfile>
            {
                new TuningProfile
                {
                    RoundCode = "R22",
                    BatchId = "R3-04E-B001-R22",
                    Label = "path-diversify-axis-push",
                    EasyBias = 0.19f,
                    StandardBias = 0.12f,
                    HardBias = 0.05f,
                    EasyInitScale = 1.05f,
                    StandardInitScale = 1.03f,
                    HardInitScale = 1.04f,
                    SocialTradeInitScale = 0.22f,
                    EasyVictoryThreshold = 88f,
                    StandardVictoryThreshold = 84f,
                    HardVictoryThreshold = 80f,
                    EasyMilitaryCollapseThreshold = 8,
                    StandardMilitaryCollapseThreshold = 11,
                    HardMilitaryCollapseThreshold = 17,
                    PressureOffset = 0.23f,
                    PressureTurnScale = 0.21f,
                    ConflictBase = 0.39f,
                    ConflictTurnScale = 0.013f,
                    ResourceDeltaScale = 0.95f,
                    ResourceNoiseScale = 2.05f,
                    RelationDeltaScale = 2.15f,
                    SatisfactionScale = 0.86f,
                    EarlySuppressionTurns = 7,
                    EarlyPositiveGainScale = 0.48f,
                    ShockChance = 0.44f,
                    ShockMagnitude = 34,
                    CaptureChanceScale = 2.10f,
                    LossChanceScale = 0.66f,
                    NonConflictNodeScale = 0.56f,
                    NodeDriftScale = 2.20f,
                    EasyDurationPerTurn = 23f,
                    StandardDurationPerTurn = 29f,
                    HardDurationPerTurn = 33f
                },
                new TuningProfile
                {
                    RoundCode = "R23",
                    BatchId = "R3-04E-B001-R23",
                    Label = "win-floor-recover",
                    EasyBias = 0.22f,
                    StandardBias = 0.14f,
                    HardBias = 0.07f,
                    EasyInitScale = 1.08f,
                    StandardInitScale = 1.05f,
                    HardInitScale = 1.08f,
                    SocialTradeInitScale = 0.24f,
                    EasyVictoryThreshold = 87f,
                    StandardVictoryThreshold = 83f,
                    HardVictoryThreshold = 79f,
                    EasyMilitaryCollapseThreshold = 7,
                    StandardMilitaryCollapseThreshold = 10,
                    HardMilitaryCollapseThreshold = 16,
                    PressureOffset = 0.22f,
                    PressureTurnScale = 0.20f,
                    ConflictBase = 0.38f,
                    ConflictTurnScale = 0.012f,
                    ResourceDeltaScale = 0.98f,
                    ResourceNoiseScale = 2.00f,
                    RelationDeltaScale = 2.00f,
                    SatisfactionScale = 0.88f,
                    EarlySuppressionTurns = 6,
                    EarlyPositiveGainScale = 0.50f,
                    ShockChance = 0.40f,
                    ShockMagnitude = 32,
                    CaptureChanceScale = 1.95f,
                    LossChanceScale = 0.70f,
                    NonConflictNodeScale = 0.52f,
                    NodeDriftScale = 2.05f,
                    EasyDurationPerTurn = 23f,
                    StandardDurationPerTurn = 29f,
                    HardDurationPerTurn = 33f
                },
                new TuningProfile
                {
                    RoundCode = "R24",
                    BatchId = "R3-04E-B001-R24",
                    Label = "hybrid-floor-vs-monopoly",
                    EasyBias = 0.21f,
                    StandardBias = 0.13f,
                    HardBias = 0.06f,
                    EasyInitScale = 1.07f,
                    StandardInitScale = 1.04f,
                    HardInitScale = 1.06f,
                    SocialTradeInitScale = 0.21f,
                    EasyVictoryThreshold = 88f,
                    StandardVictoryThreshold = 84f,
                    HardVictoryThreshold = 80f,
                    EasyMilitaryCollapseThreshold = 7,
                    StandardMilitaryCollapseThreshold = 10,
                    HardMilitaryCollapseThreshold = 16,
                    PressureOffset = 0.24f,
                    PressureTurnScale = 0.22f,
                    ConflictBase = 0.40f,
                    ConflictTurnScale = 0.013f,
                    ResourceDeltaScale = 0.96f,
                    ResourceNoiseScale = 2.08f,
                    RelationDeltaScale = 2.20f,
                    SatisfactionScale = 0.85f,
                    EarlySuppressionTurns = 7,
                    EarlyPositiveGainScale = 0.46f,
                    ShockChance = 0.45f,
                    ShockMagnitude = 35,
                    CaptureChanceScale = 2.20f,
                    LossChanceScale = 0.64f,
                    NonConflictNodeScale = 0.58f,
                    NodeDriftScale = 2.25f,
                    EasyDurationPerTurn = 23f,
                    StandardDurationPerTurn = 30f,
                    HardDurationPerTurn = 34f
                }
            };
        }

        private static List<TuningProfile> BuildR304FProfiles()
        {
            return new List<TuningProfile>
            {
                new TuningProfile
                {
                    RoundCode = "R25",
                    BatchId = "R3-04F-B001-R25",
                    Label = "hard-clamp-easy-lift",
                    EasyBias = 0.28f,
                    StandardBias = 0.11f,
                    HardBias = -0.10f,
                    EasyInitScale = 1.16f,
                    StandardInitScale = 1.00f,
                    HardInitScale = 0.88f,
                    SocialTradeInitScale = 0.23f,
                    EasyVictoryThreshold = 86f,
                    StandardVictoryThreshold = 84f,
                    HardVictoryThreshold = 85f,
                    EasyMilitaryCollapseThreshold = 7,
                    StandardMilitaryCollapseThreshold = 12,
                    HardMilitaryCollapseThreshold = 30,
                    PressureOffset = 0.24f,
                    PressureTurnScale = 0.22f,
                    ConflictBase = 0.41f,
                    ConflictTurnScale = 0.014f,
                    ResourceDeltaScale = 0.95f,
                    ResourceNoiseScale = 2.12f,
                    RelationDeltaScale = 2.05f,
                    SatisfactionScale = 0.84f,
                    EarlySuppressionTurns = 7,
                    EarlyPositiveGainScale = 0.45f,
                    ShockChance = 0.45f,
                    ShockMagnitude = 36,
                    CaptureChanceScale = 1.60f,
                    LossChanceScale = 0.95f,
                    NonConflictNodeScale = 0.50f,
                    NodeDriftScale = 2.10f,
                    EasyDurationPerTurn = 23f,
                    StandardDurationPerTurn = 30f,
                    HardDurationPerTurn = 35f
                },
                new TuningProfile
                {
                    RoundCode = "R26",
                    BatchId = "R3-04F-B001-R26",
                    Label = "defeat-share-balance",
                    EasyBias = 0.25f,
                    StandardBias = 0.13f,
                    HardBias = -0.06f,
                    EasyInitScale = 1.12f,
                    StandardInitScale = 1.02f,
                    HardInitScale = 0.92f,
                    SocialTradeInitScale = 0.24f,
                    EasyVictoryThreshold = 86f,
                    StandardVictoryThreshold = 83f,
                    HardVictoryThreshold = 84f,
                    EasyMilitaryCollapseThreshold = 8,
                    StandardMilitaryCollapseThreshold = 12,
                    HardMilitaryCollapseThreshold = 28,
                    PressureOffset = 0.23f,
                    PressureTurnScale = 0.21f,
                    ConflictBase = 0.39f,
                    ConflictTurnScale = 0.013f,
                    ResourceDeltaScale = 0.97f,
                    ResourceNoiseScale = 2.05f,
                    RelationDeltaScale = 2.00f,
                    SatisfactionScale = 0.86f,
                    EarlySuppressionTurns = 6,
                    EarlyPositiveGainScale = 0.48f,
                    ShockChance = 0.42f,
                    ShockMagnitude = 34,
                    CaptureChanceScale = 1.70f,
                    LossChanceScale = 0.90f,
                    NonConflictNodeScale = 0.48f,
                    NodeDriftScale = 2.00f,
                    EasyDurationPerTurn = 23f,
                    StandardDurationPerTurn = 30f,
                    HardDurationPerTurn = 34f
                },
                new TuningProfile
                {
                    RoundCode = "R27",
                    BatchId = "R3-04F-B001-R27",
                    Label = "pace-floor-guard",
                    EasyBias = 0.24f,
                    StandardBias = 0.12f,
                    HardBias = -0.08f,
                    EasyInitScale = 1.10f,
                    StandardInitScale = 1.00f,
                    HardInitScale = 0.90f,
                    SocialTradeInitScale = 0.22f,
                    EasyVictoryThreshold = 87f,
                    StandardVictoryThreshold = 84f,
                    HardVictoryThreshold = 85f,
                    EasyMilitaryCollapseThreshold = 8,
                    StandardMilitaryCollapseThreshold = 13,
                    HardMilitaryCollapseThreshold = 30,
                    PressureOffset = 0.25f,
                    PressureTurnScale = 0.23f,
                    ConflictBase = 0.42f,
                    ConflictTurnScale = 0.014f,
                    ResourceDeltaScale = 0.93f,
                    ResourceNoiseScale = 2.15f,
                    RelationDeltaScale = 2.10f,
                    SatisfactionScale = 0.83f,
                    EarlySuppressionTurns = 8,
                    EarlyPositiveGainScale = 0.42f,
                    ShockChance = 0.46f,
                    ShockMagnitude = 37,
                    CaptureChanceScale = 1.55f,
                    LossChanceScale = 1.00f,
                    NonConflictNodeScale = 0.52f,
                    NodeDriftScale = 2.25f,
                    EasyDurationPerTurn = 24f,
                    StandardDurationPerTurn = 31f,
                    HardDurationPerTurn = 35f
                }
            };
        }

        private static List<TuningProfile> BuildR304GProfiles()
        {
            return new List<TuningProfile>
            {
                new TuningProfile
                {
                    RoundCode = "R70",
                    BatchId = "R3-04G-B015-R70",
                    Label = "r64-clone-hard-edge-a",
                    EasyBias = 0.24f,
                    StandardBias = 0.14f,
                    HardBias = 0.05f,
                    EasyInitScale = 1.11f,
                    StandardInitScale = 1.05f,
                    HardInitScale = 1.04f,
                    SocialTradeInitScale = 0.16f,
                    EasyVictoryThreshold = 85f,
                    StandardVictoryThreshold = 83f,
                    HardVictoryThreshold = 82f,
                    EasyMilitaryCollapseThreshold = 5,
                    StandardMilitaryCollapseThreshold = 10,
                    HardMilitaryCollapseThreshold = 23,
                    PressureOffset = 0.24f,
                    PressureTurnScale = 0.22f,
                    ConflictBase = 0.40f,
                    ConflictTurnScale = 0.013f,
                    ResourceDeltaScale = 0.96f,
                    ResourceNoiseScale = 2.05f,
                    RelationDeltaScale = 2.45f,
                    SatisfactionScale = 0.87f,
                    EarlySuppressionTurns = 7,
                    EarlyPositiveGainScale = 0.47f,
                    ShockChance = 0.41f,
                    ShockMagnitude = 33,
                    CaptureChanceScale = 2.80f,
                    LossChanceScale = 0.50f,
                    NonConflictNodeScale = 0.75f,
                    NodeDriftScale = 2.45f,
                    EasyDurationPerTurn = 23f,
                    StandardDurationPerTurn = 29f,
                    HardDurationPerTurn = 33f
                },
                new TuningProfile
                {
                    RoundCode = "R71",
                    BatchId = "R3-04G-B015-R71",
                    Label = "r64-clone-hard-edge-b",
                    EasyBias = 0.24f,
                    StandardBias = 0.14f,
                    HardBias = 0.052f,
                    EasyInitScale = 1.11f,
                    StandardInitScale = 1.05f,
                    HardInitScale = 1.05f,
                    SocialTradeInitScale = 0.16f,
                    EasyVictoryThreshold = 85f,
                    StandardVictoryThreshold = 83f,
                    HardVictoryThreshold = 82f,
                    EasyMilitaryCollapseThreshold = 5,
                    StandardMilitaryCollapseThreshold = 10,
                    HardMilitaryCollapseThreshold = 23,
                    PressureOffset = 0.24f,
                    PressureTurnScale = 0.22f,
                    ConflictBase = 0.405f,
                    ConflictTurnScale = 0.013f,
                    ResourceDeltaScale = 0.96f,
                    ResourceNoiseScale = 2.05f,
                    RelationDeltaScale = 2.45f,
                    SatisfactionScale = 0.87f,
                    EarlySuppressionTurns = 7,
                    EarlyPositiveGainScale = 0.47f,
                    ShockChance = 0.41f,
                    ShockMagnitude = 33,
                    CaptureChanceScale = 2.80f,
                    LossChanceScale = 0.50f,
                    NonConflictNodeScale = 0.75f,
                    NodeDriftScale = 2.45f,
                    EasyDurationPerTurn = 23f,
                    StandardDurationPerTurn = 29f,
                    HardDurationPerTurn = 33f
                },
                new TuningProfile
                {
                    RoundCode = "R72",
                    BatchId = "R3-04G-B015-R72",
                    Label = "r64-clone-hard-edge-c",
                    EasyBias = 0.24f,
                    StandardBias = 0.14f,
                    HardBias = 0.053f,
                    EasyInitScale = 1.11f,
                    StandardInitScale = 1.05f,
                    HardInitScale = 1.05f,
                    SocialTradeInitScale = 0.16f,
                    EasyVictoryThreshold = 85f,
                    StandardVictoryThreshold = 83f,
                    HardVictoryThreshold = 82f,
                    EasyMilitaryCollapseThreshold = 5,
                    StandardMilitaryCollapseThreshold = 10,
                    HardMilitaryCollapseThreshold = 23,
                    PressureOffset = 0.24f,
                    PressureTurnScale = 0.22f,
                    ConflictBase = 0.40f,
                    ConflictTurnScale = 0.013f,
                    ResourceDeltaScale = 0.96f,
                    ResourceNoiseScale = 2.05f,
                    RelationDeltaScale = 2.45f,
                    SatisfactionScale = 0.87f,
                    EarlySuppressionTurns = 7,
                    EarlyPositiveGainScale = 0.47f,
                    ShockChance = 0.41f,
                    ShockMagnitude = 33,
                    CaptureChanceScale = 2.80f,
                    LossChanceScale = 0.50f,
                    NonConflictNodeScale = 0.75f,
                    NodeDriftScale = 2.45f,
                    EasyDurationPerTurn = 23f,
                    StandardDurationPerTurn = 29f,
                    HardDurationPerTurn = 33f
                }
            };
        }

        private static List<TuningProfile> BuildR304HProfiles()
        {
            return new List<TuningProfile>
            {
                new TuningProfile
                {
                    RoundCode = "R82",
                    BatchId = "R3-04H-B004-R82",
                    Label = "r79-pace-backpull-a",
                    EasyBias = 0.26f,
                    StandardBias = 0.14f,
                    HardBias = 0.049f,
                    EasyInitScale = 1.13f,
                    StandardInitScale = 1.05f,
                    HardInitScale = 1.03f,
                    SocialTradeInitScale = 0.16f,
                    EasyVictoryThreshold = 85f,
                    StandardVictoryThreshold = 83f,
                    HardVictoryThreshold = 81f,
                    EasyMilitaryCollapseThreshold = 4,
                    StandardMilitaryCollapseThreshold = 10,
                    HardMilitaryCollapseThreshold = 24,
                    PressureOffset = 0.24f,
                    PressureTurnScale = 0.22f,
                    ConflictBase = 0.40f,
                    ConflictTurnScale = 0.013f,
                    ResourceDeltaScale = 0.96f,
                    ResourceNoiseScale = 2.05f,
                    RelationDeltaScale = 2.45f,
                    SatisfactionScale = 0.87f,
                    EarlySuppressionTurns = 7,
                    EarlyPositiveGainScale = 0.47f,
                    ShockChance = 0.41f,
                    ShockMagnitude = 33,
                    CaptureChanceScale = 2.80f,
                    LossChanceScale = 0.50f,
                    NonConflictNodeScale = 0.75f,
                    NodeDriftScale = 2.45f,
                    EasyDurationPerTurn = 23f,
                    StandardDurationPerTurn = 29f,
                    HardDurationPerTurn = 33f
                },
                new TuningProfile
                {
                    RoundCode = "R83",
                    BatchId = "R3-04H-B004-R83",
                    Label = "r79-pace-backpull-b",
                    EasyBias = 0.255f,
                    StandardBias = 0.14f,
                    HardBias = 0.048f,
                    EasyInitScale = 1.12f,
                    StandardInitScale = 1.05f,
                    HardInitScale = 1.04f,
                    SocialTradeInitScale = 0.16f,
                    EasyVictoryThreshold = 85f,
                    StandardVictoryThreshold = 83f,
                    HardVictoryThreshold = 81f,
                    EasyMilitaryCollapseThreshold = 4,
                    StandardMilitaryCollapseThreshold = 10,
                    HardMilitaryCollapseThreshold = 24,
                    PressureOffset = 0.23f,
                    PressureTurnScale = 0.21f,
                    ConflictBase = 0.39f,
                    ConflictTurnScale = 0.013f,
                    ResourceDeltaScale = 0.96f,
                    ResourceNoiseScale = 2.05f,
                    RelationDeltaScale = 2.45f,
                    SatisfactionScale = 0.87f,
                    EarlySuppressionTurns = 8,
                    EarlyPositiveGainScale = 0.46f,
                    ShockChance = 0.41f,
                    ShockMagnitude = 33,
                    CaptureChanceScale = 2.80f,
                    LossChanceScale = 0.50f,
                    NonConflictNodeScale = 0.75f,
                    NodeDriftScale = 2.45f,
                    EasyDurationPerTurn = 23f,
                    StandardDurationPerTurn = 29f,
                    HardDurationPerTurn = 33f
                },
                new TuningProfile
                {
                    RoundCode = "R84",
                    BatchId = "R3-04H-B004-R84",
                    Label = "r79-pace-backpull-c",
                    EasyBias = 0.26f,
                    StandardBias = 0.145f,
                    HardBias = 0.047f,
                    EasyInitScale = 1.12f,
                    StandardInitScale = 1.06f,
                    HardInitScale = 1.03f,
                    SocialTradeInitScale = 0.16f,
                    EasyVictoryThreshold = 85f,
                    StandardVictoryThreshold = 83f,
                    HardVictoryThreshold = 81f,
                    EasyMilitaryCollapseThreshold = 4,
                    StandardMilitaryCollapseThreshold = 10,
                    HardMilitaryCollapseThreshold = 24,
                    PressureOffset = 0.24f,
                    PressureTurnScale = 0.22f,
                    ConflictBase = 0.39f,
                    ConflictTurnScale = 0.013f,
                    ResourceDeltaScale = 0.96f,
                    ResourceNoiseScale = 2.05f,
                    RelationDeltaScale = 2.45f,
                    SatisfactionScale = 0.87f,
                    EarlySuppressionTurns = 7,
                    EarlyPositiveGainScale = 0.47f,
                    ShockChance = 0.41f,
                    ShockMagnitude = 33,
                    CaptureChanceScale = 2.80f,
                    LossChanceScale = 0.50f,
                    NonConflictNodeScale = 0.75f,
                    NodeDriftScale = 2.45f,
                    EasyDurationPerTurn = 23f,
                    StandardDurationPerTurn = 29f,
                    HardDurationPerTurn = 33f
                }
            };
        }

        private static List<TuningProfile> BuildR304IProfiles()
        {
            return new List<TuningProfile>
            {
                new TuningProfile
                {
                    RoundCode = "R97",
                    BatchId = "R3-04I-B005-R97",
                    Label = "r82-early-suppress-a",
                    EasyBias = 0.260f,
                    StandardBias = 0.145f,
                    HardBias = 0.049f,
                    EasyInitScale = 1.13f,
                    StandardInitScale = 1.06f,
                    HardInitScale = 1.03f,
                    SocialTradeInitScale = 0.16f,
                    EasyVictoryThreshold = 85f,
                    StandardVictoryThreshold = 83f,
                    HardVictoryThreshold = 81f,
                    EasyMilitaryCollapseThreshold = 4,
                    StandardMilitaryCollapseThreshold = 10,
                    HardMilitaryCollapseThreshold = 24,
                    PressureOffset = 0.240f,
                    PressureTurnScale = 0.220f,
                    ConflictBase = 0.401f,
                    ConflictTurnScale = 0.0130f,
                    ResourceDeltaScale = 0.960f,
                    ResourceNoiseScale = 2.03f,
                    RelationDeltaScale = 2.47f,
                    SatisfactionScale = 0.87f,
                    EarlySuppressionTurns = 10,
                    EarlyPositiveGainScale = 0.40f,
                    ShockChance = 0.39f,
                    ShockMagnitude = 31,
                    CaptureChanceScale = 2.85f,
                    LossChanceScale = 0.53f,
                    NonConflictNodeScale = 0.74f,
                    NodeDriftScale = 2.40f,
                    EasyDurationPerTurn = 23f,
                    StandardDurationPerTurn = 29f,
                    HardDurationPerTurn = 33f
                },
                new TuningProfile
                {
                    RoundCode = "R98",
                    BatchId = "R3-04I-B005-R98",
                    Label = "r82-early-suppress-b",
                    EasyBias = 0.262f,
                    StandardBias = 0.147f,
                    HardBias = 0.049f,
                    EasyInitScale = 1.13f,
                    StandardInitScale = 1.07f,
                    HardInitScale = 1.03f,
                    SocialTradeInitScale = 0.16f,
                    EasyVictoryThreshold = 85f,
                    StandardVictoryThreshold = 83f,
                    HardVictoryThreshold = 81f,
                    EasyMilitaryCollapseThreshold = 4,
                    StandardMilitaryCollapseThreshold = 10,
                    HardMilitaryCollapseThreshold = 24,
                    PressureOffset = 0.241f,
                    PressureTurnScale = 0.221f,
                    ConflictBase = 0.401f,
                    ConflictTurnScale = 0.0130f,
                    ResourceDeltaScale = 0.960f,
                    ResourceNoiseScale = 2.02f,
                    RelationDeltaScale = 2.48f,
                    SatisfactionScale = 0.86f,
                    EarlySuppressionTurns = 11,
                    EarlyPositiveGainScale = 0.36f,
                    ShockChance = 0.39f,
                    ShockMagnitude = 31,
                    CaptureChanceScale = 2.86f,
                    LossChanceScale = 0.54f,
                    NonConflictNodeScale = 0.74f,
                    NodeDriftScale = 2.40f,
                    EasyDurationPerTurn = 23f,
                    StandardDurationPerTurn = 29f,
                    HardDurationPerTurn = 33f
                },
                new TuningProfile
                {
                    RoundCode = "R99",
                    BatchId = "R3-04I-B005-R99",
                    Label = "r82-early-suppress-c",
                    EasyBias = 0.261f,
                    StandardBias = 0.148f,
                    HardBias = 0.048f,
                    EasyInitScale = 1.13f,
                    StandardInitScale = 1.07f,
                    HardInitScale = 1.03f,
                    SocialTradeInitScale = 0.16f,
                    EasyVictoryThreshold = 85f,
                    StandardVictoryThreshold = 83f,
                    HardVictoryThreshold = 81f,
                    EasyMilitaryCollapseThreshold = 4,
                    StandardMilitaryCollapseThreshold = 10,
                    HardMilitaryCollapseThreshold = 24,
                    PressureOffset = 0.243f,
                    PressureTurnScale = 0.223f,
                    ConflictBase = 0.402f,
                    ConflictTurnScale = 0.0131f,
                    ResourceDeltaScale = 0.958f,
                    ResourceNoiseScale = 2.02f,
                    RelationDeltaScale = 2.50f,
                    SatisfactionScale = 0.86f,
                    EarlySuppressionTurns = 10,
                    EarlyPositiveGainScale = 0.39f,
                    ShockChance = 0.38f,
                    ShockMagnitude = 30,
                    CaptureChanceScale = 2.88f,
                    LossChanceScale = 0.54f,
                    NonConflictNodeScale = 0.74f,
                    NodeDriftScale = 2.39f,
                    EasyDurationPerTurn = 23f,
                    StandardDurationPerTurn = 29f,
                    HardDurationPerTurn = 33f
                }
            };
        }

        private static List<TuningProfile> BuildR304JProfiles()
        {
            return new List<TuningProfile>
            {
                new TuningProfile
                {
                    RoundCode = "R115",
                    BatchId = "R3-04J-B005-R115",
                    Label = "r112-hard-recover-floor-a",
                    EasyBias = 0.259f,
                    StandardBias = 0.152f,
                    HardBias = 0.050f,
                    EasyInitScale = 1.13f,
                    StandardInitScale = 1.07f,
                    HardInitScale = 1.04f,
                    SocialTradeInitScale = 0.16f,
                    EasyVictoryThreshold = 86f,
                    StandardVictoryThreshold = 83f,
                    HardVictoryThreshold = 81f,
                    EasyMilitaryCollapseThreshold = 4,
                    StandardMilitaryCollapseThreshold = 9,
                    HardMilitaryCollapseThreshold = 22,
                    PressureOffset = 0.241f,
                    PressureTurnScale = 0.229f,
                    ConflictBase = 0.399f,
                    ConflictTurnScale = 0.0134f,
                    ResourceDeltaScale = 0.957f,
                    ResourceNoiseScale = 2.02f,
                    RelationDeltaScale = 2.52f,
                    SatisfactionScale = 0.86f,
                    EarlySuppressionTurns = 12,
                    EarlyPositiveGainScale = 0.36f,
                    ShockChance = 0.38f,
                    ShockMagnitude = 30,
                    CaptureChanceScale = 2.87f,
                    LossChanceScale = 0.55f,
                    NonConflictNodeScale = 0.75f,
                    NodeDriftScale = 2.40f,
                    EasyDurationPerTurn = 23f,
                    StandardDurationPerTurn = 29f,
                    HardDurationPerTurn = 33f
                },
                new TuningProfile
                {
                    RoundCode = "R116",
                    BatchId = "R3-04J-B005-R116",
                    Label = "r112-hard-recover-floor-b",
                    EasyBias = 0.258f,
                    StandardBias = 0.153f,
                    HardBias = 0.051f,
                    EasyInitScale = 1.13f,
                    StandardInitScale = 1.08f,
                    HardInitScale = 1.05f,
                    SocialTradeInitScale = 0.16f,
                    EasyVictoryThreshold = 86f,
                    StandardVictoryThreshold = 83f,
                    HardVictoryThreshold = 81f,
                    EasyMilitaryCollapseThreshold = 4,
                    StandardMilitaryCollapseThreshold = 9,
                    HardMilitaryCollapseThreshold = 21,
                    PressureOffset = 0.240f,
                    PressureTurnScale = 0.229f,
                    ConflictBase = 0.398f,
                    ConflictTurnScale = 0.0134f,
                    ResourceDeltaScale = 0.956f,
                    ResourceNoiseScale = 2.02f,
                    RelationDeltaScale = 2.53f,
                    SatisfactionScale = 0.86f,
                    EarlySuppressionTurns = 12,
                    EarlyPositiveGainScale = 0.35f,
                    ShockChance = 0.38f,
                    ShockMagnitude = 30,
                    CaptureChanceScale = 2.88f,
                    LossChanceScale = 0.55f,
                    NonConflictNodeScale = 0.76f,
                    NodeDriftScale = 2.40f,
                    EasyDurationPerTurn = 23f,
                    StandardDurationPerTurn = 29f,
                    HardDurationPerTurn = 33f
                },
                new TuningProfile
                {
                    RoundCode = "R117",
                    BatchId = "R3-04J-B005-R117",
                    Label = "r112-hard-recover-floor-c",
                    EasyBias = 0.258f,
                    StandardBias = 0.154f,
                    HardBias = 0.050f,
                    EasyInitScale = 1.13f,
                    StandardInitScale = 1.08f,
                    HardInitScale = 1.04f,
                    SocialTradeInitScale = 0.16f,
                    EasyVictoryThreshold = 87f,
                    StandardVictoryThreshold = 83f,
                    HardVictoryThreshold = 81f,
                    EasyMilitaryCollapseThreshold = 4,
                    StandardMilitaryCollapseThreshold = 10,
                    HardMilitaryCollapseThreshold = 22,
                    PressureOffset = 0.240f,
                    PressureTurnScale = 0.230f,
                    ConflictBase = 0.398f,
                    ConflictTurnScale = 0.0134f,
                    ResourceDeltaScale = 0.956f,
                    ResourceNoiseScale = 2.02f,
                    RelationDeltaScale = 2.54f,
                    SatisfactionScale = 0.86f,
                    EarlySuppressionTurns = 12,
                    EarlyPositiveGainScale = 0.35f,
                    ShockChance = 0.38f,
                    ShockMagnitude = 30,
                    CaptureChanceScale = 2.89f,
                    LossChanceScale = 0.55f,
                    NonConflictNodeScale = 0.76f,
                    NodeDriftScale = 2.40f,
                    EasyDurationPerTurn = 23f,
                    StandardDurationPerTurn = 29f,
                    HardDurationPerTurn = 33f
                }
            };
        }

        private static List<TuningProfile> BuildR304KProfiles()
        {
            return new List<TuningProfile>
            {
                new TuningProfile
                {
                    RoundCode = "R127",
                    BatchId = "R3-04K-B004-R127",
                    Label = "r125-standard-recover-plus-a",
                    EasyBias = 0.261f,
                    StandardBias = 0.156f,
                    HardBias = 0.049f,
                    EasyInitScale = 1.13f,
                    StandardInitScale = 1.10f,
                    HardInitScale = 1.04f,
                    SocialTradeInitScale = 0.16f,
                    EasyVictoryThreshold = 85f,
                    StandardVictoryThreshold = 83f,
                    HardVictoryThreshold = 81f,
                    EasyMilitaryCollapseThreshold = 4,
                    StandardMilitaryCollapseThreshold = 9,
                    HardMilitaryCollapseThreshold = 24,
                    PressureOffset = 0.240f,
                    PressureTurnScale = 0.229f,
                    ConflictBase = 0.400f,
                    ConflictTurnScale = 0.0135f,
                    ResourceDeltaScale = 0.958f,
                    ResourceNoiseScale = 2.02f,
                    RelationDeltaScale = 2.58f,
                    SatisfactionScale = 0.86f,
                    EarlySuppressionTurns = 12,
                    EarlyPositiveGainScale = 0.36f,
                    ShockChance = 0.38f,
                    ShockMagnitude = 30,
                    CaptureChanceScale = 2.78f,
                    LossChanceScale = 0.56f,
                    NonConflictNodeScale = 0.78f,
                    NodeDriftScale = 2.40f,
                    EasyDurationPerTurn = 23f,
                    StandardDurationPerTurn = 29f,
                    HardDurationPerTurn = 33f
                },
                new TuningProfile
                {
                    RoundCode = "R128",
                    BatchId = "R3-04K-B004-R128",
                    Label = "r125-standard-recover-plus-b",
                    EasyBias = 0.260f,
                    StandardBias = 0.154f,
                    HardBias = 0.049f,
                    EasyInitScale = 1.13f,
                    StandardInitScale = 1.09f,
                    HardInitScale = 1.04f,
                    SocialTradeInitScale = 0.16f,
                    EasyVictoryThreshold = 85f,
                    StandardVictoryThreshold = 84f,
                    HardVictoryThreshold = 81f,
                    EasyMilitaryCollapseThreshold = 4,
                    StandardMilitaryCollapseThreshold = 9,
                    HardMilitaryCollapseThreshold = 24,
                    PressureOffset = 0.239f,
                    PressureTurnScale = 0.230f,
                    ConflictBase = 0.399f,
                    ConflictTurnScale = 0.0136f,
                    ResourceDeltaScale = 0.958f,
                    ResourceNoiseScale = 2.02f,
                    RelationDeltaScale = 2.57f,
                    SatisfactionScale = 0.86f,
                    EarlySuppressionTurns = 12,
                    EarlyPositiveGainScale = 0.36f,
                    ShockChance = 0.38f,
                    ShockMagnitude = 30,
                    CaptureChanceScale = 2.80f,
                    LossChanceScale = 0.56f,
                    NonConflictNodeScale = 0.78f,
                    NodeDriftScale = 2.40f,
                    EasyDurationPerTurn = 23f,
                    StandardDurationPerTurn = 29f,
                    HardDurationPerTurn = 33f
                },
                new TuningProfile
                {
                    RoundCode = "R129",
                    BatchId = "R3-04K-B004-R129",
                    Label = "r125-standard-recover-plus-c",
                    EasyBias = 0.261f,
                    StandardBias = 0.153f,
                    HardBias = 0.049f,
                    EasyInitScale = 1.13f,
                    StandardInitScale = 1.09f,
                    HardInitScale = 1.04f,
                    SocialTradeInitScale = 0.16f,
                    EasyVictoryThreshold = 85f,
                    StandardVictoryThreshold = 83.6f,
                    HardVictoryThreshold = 81f,
                    EasyMilitaryCollapseThreshold = 4,
                    StandardMilitaryCollapseThreshold = 10,
                    HardMilitaryCollapseThreshold = 24,
                    PressureOffset = 0.239f,
                    PressureTurnScale = 0.231f,
                    ConflictBase = 0.400f,
                    ConflictTurnScale = 0.0136f,
                    ResourceDeltaScale = 0.958f,
                    ResourceNoiseScale = 2.02f,
                    RelationDeltaScale = 2.56f,
                    SatisfactionScale = 0.86f,
                    EarlySuppressionTurns = 12,
                    EarlyPositiveGainScale = 0.36f,
                    ShockChance = 0.38f,
                    ShockMagnitude = 30,
                    CaptureChanceScale = 2.82f,
                    LossChanceScale = 0.56f,
                    NonConflictNodeScale = 0.77f,
                    NodeDriftScale = 2.40f,
                    EasyDurationPerTurn = 23f,
                    StandardDurationPerTurn = 29f,
                    HardDurationPerTurn = 33f
                }
            };
        }

        private static List<TuningProfile> BuildR304LProfiles()
        {
            return new List<TuningProfile>
            {
                new TuningProfile
                {
                    RoundCode = "R136",
                    BatchId = "R3-04L-B003-R136",
                    Label = "r134-avgturn-recover-a",
                    EasyBias = 0.261f,
                    StandardBias = 0.155f,
                    HardBias = 0.051f,
                    EasyInitScale = 1.13f,
                    StandardInitScale = 1.11f,
                    HardInitScale = 1.06f,
                    SocialTradeInitScale = 0.16f,
                    EasyVictoryThreshold = 85f,
                    StandardVictoryThreshold = 82.9f,
                    HardVictoryThreshold = 81.2f,
                    EasyMilitaryCollapseThreshold = 5,
                    StandardMilitaryCollapseThreshold = 9,
                    HardMilitaryCollapseThreshold = 21,
                    PressureOffset = 0.234f,
                    PressureTurnScale = 0.242f,
                    ConflictBase = 0.395f,
                    ConflictTurnScale = 0.0141f,
                    ResourceDeltaScale = 0.958f,
                    ResourceNoiseScale = 2.02f,
                    RelationDeltaScale = 2.64f,
                    SatisfactionScale = 0.86f,
                    EarlySuppressionTurns = 13,
                    EarlyPositiveGainScale = 0.35f,
                    ShockChance = 0.38f,
                    ShockMagnitude = 30,
                    CaptureChanceScale = 2.73f,
                    LossChanceScale = 0.56f,
                    NonConflictNodeScale = 0.80f,
                    NodeDriftScale = 2.40f,
                    EasyDurationPerTurn = 23f,
                    StandardDurationPerTurn = 29f,
                    HardDurationPerTurn = 33f
                },
                new TuningProfile
                {
                    RoundCode = "R137",
                    BatchId = "R3-04L-B003-R137",
                    Label = "r134-avgturn-recover-b",
                    EasyBias = 0.260f,
                    StandardBias = 0.155f,
                    HardBias = 0.050f,
                    EasyInitScale = 1.13f,
                    StandardInitScale = 1.09f,
                    HardInitScale = 1.05f,
                    SocialTradeInitScale = 0.16f,
                    EasyVictoryThreshold = 85.1f,
                    StandardVictoryThreshold = 82.9f,
                    HardVictoryThreshold = 81.3f,
                    EasyMilitaryCollapseThreshold = 5,
                    StandardMilitaryCollapseThreshold = 10,
                    HardMilitaryCollapseThreshold = 21,
                    PressureOffset = 0.233f,
                    PressureTurnScale = 0.243f,
                    ConflictBase = 0.394f,
                    ConflictTurnScale = 0.0142f,
                    ResourceDeltaScale = 0.958f,
                    ResourceNoiseScale = 2.02f,
                    RelationDeltaScale = 2.66f,
                    SatisfactionScale = 0.86f,
                    EarlySuppressionTurns = 13,
                    EarlyPositiveGainScale = 0.35f,
                    ShockChance = 0.38f,
                    ShockMagnitude = 30,
                    CaptureChanceScale = 2.70f,
                    LossChanceScale = 0.56f,
                    NonConflictNodeScale = 0.81f,
                    NodeDriftScale = 2.40f,
                    EasyDurationPerTurn = 23f,
                    StandardDurationPerTurn = 29f,
                    HardDurationPerTurn = 33f
                },
                new TuningProfile
                {
                    RoundCode = "R138",
                    BatchId = "R3-04L-B003-R138",
                    Label = "r134-avgturn-recover-c",
                    EasyBias = 0.261f,
                    StandardBias = 0.154f,
                    HardBias = 0.051f,
                    EasyInitScale = 1.13f,
                    StandardInitScale = 1.10f,
                    HardInitScale = 1.06f,
                    SocialTradeInitScale = 0.16f,
                    EasyVictoryThreshold = 85.1f,
                    StandardVictoryThreshold = 83f,
                    HardVictoryThreshold = 81.1f,
                    EasyMilitaryCollapseThreshold = 5,
                    StandardMilitaryCollapseThreshold = 9,
                    HardMilitaryCollapseThreshold = 21,
                    PressureOffset = 0.235f,
                    PressureTurnScale = 0.242f,
                    ConflictBase = 0.395f,
                    ConflictTurnScale = 0.0141f,
                    ResourceDeltaScale = 0.958f,
                    ResourceNoiseScale = 2.02f,
                    RelationDeltaScale = 2.65f,
                    SatisfactionScale = 0.86f,
                    EarlySuppressionTurns = 13,
                    EarlyPositiveGainScale = 0.35f,
                    ShockChance = 0.38f,
                    ShockMagnitude = 30,
                    CaptureChanceScale = 2.71f,
                    LossChanceScale = 0.57f,
                    NonConflictNodeScale = 0.81f,
                    NodeDriftScale = 2.40f,
                    EasyDurationPerTurn = 23f,
                    StandardDurationPerTurn = 29f,
                    HardDurationPerTurn = 33f
                }
            };
        }

        private static List<TuningProfile> BuildR304MProfiles()
        {
            return new List<TuningProfile>
            {
                new TuningProfile
                {
                    RoundCode = "R151",
                    BatchId = "R3-04M-B005-R151",
                    Label = "r149-defeat-floor-recover-a",
                    EasyBias = 0.262f,
                    StandardBias = 0.155f,
                    HardBias = 0.051f,
                    EasyInitScale = 1.135f,
                    StandardInitScale = 1.11f,
                    HardInitScale = 1.062f,
                    SocialTradeInitScale = 0.16f,
                    EasyVictoryThreshold = 84.8f,
                    StandardVictoryThreshold = 82.9f,
                    HardVictoryThreshold = 81.1f,
                    EasyMilitaryCollapseThreshold = 5,
                    StandardMilitaryCollapseThreshold = 9,
                    HardMilitaryCollapseThreshold = 22,
                    PressureOffset = 0.233f,
                    PressureTurnScale = 0.243f,
                    ConflictBase = 0.394f,
                    ConflictTurnScale = 0.0142f,
                    ResourceDeltaScale = 0.958f,
                    ResourceNoiseScale = 2.02f,
                    RelationDeltaScale = 2.64f,
                    SatisfactionScale = 0.86f,
                    EarlySuppressionTurns = 13,
                    EarlyPositiveGainScale = 0.35f,
                    ShockChance = 0.38f,
                    ShockMagnitude = 30,
                    CaptureChanceScale = 2.72f,
                    LossChanceScale = 0.56f,
                    NonConflictNodeScale = 0.81f,
                    NodeDriftScale = 2.40f,
                    EasyDurationPerTurn = 23f,
                    StandardDurationPerTurn = 29f,
                    HardDurationPerTurn = 33f
                },
                new TuningProfile
                {
                    RoundCode = "R152",
                    BatchId = "R3-04M-B005-R152",
                    Label = "r149-defeat-floor-recover-b",
                    EasyBias = 0.262f,
                    StandardBias = 0.155f,
                    HardBias = 0.052f,
                    EasyInitScale = 1.135f,
                    StandardInitScale = 1.11f,
                    HardInitScale = 1.064f,
                    SocialTradeInitScale = 0.16f,
                    EasyVictoryThreshold = 84.8f,
                    StandardVictoryThreshold = 82.9f,
                    HardVictoryThreshold = 81.05f,
                    EasyMilitaryCollapseThreshold = 5,
                    StandardMilitaryCollapseThreshold = 9,
                    HardMilitaryCollapseThreshold = 22,
                    PressureOffset = 0.233f,
                    PressureTurnScale = 0.244f,
                    ConflictBase = 0.393f,
                    ConflictTurnScale = 0.0143f,
                    ResourceDeltaScale = 0.958f,
                    ResourceNoiseScale = 2.02f,
                    RelationDeltaScale = 2.64f,
                    SatisfactionScale = 0.86f,
                    EarlySuppressionTurns = 13,
                    EarlyPositiveGainScale = 0.35f,
                    ShockChance = 0.38f,
                    ShockMagnitude = 30,
                    CaptureChanceScale = 2.73f,
                    LossChanceScale = 0.56f,
                    NonConflictNodeScale = 0.81f,
                    NodeDriftScale = 2.40f,
                    EasyDurationPerTurn = 23f,
                    StandardDurationPerTurn = 29f,
                    HardDurationPerTurn = 33f
                },
                new TuningProfile
                {
                    RoundCode = "R153",
                    BatchId = "R3-04M-B005-R153",
                    Label = "r149-defeat-floor-recover-c",
                    EasyBias = 0.262f,
                    StandardBias = 0.155f,
                    HardBias = 0.052f,
                    EasyInitScale = 1.135f,
                    StandardInitScale = 1.12f,
                    HardInitScale = 1.065f,
                    SocialTradeInitScale = 0.16f,
                    EasyVictoryThreshold = 84.8f,
                    StandardVictoryThreshold = 82.9f,
                    HardVictoryThreshold = 81.0f,
                    EasyMilitaryCollapseThreshold = 5,
                    StandardMilitaryCollapseThreshold = 10,
                    HardMilitaryCollapseThreshold = 22,
                    PressureOffset = 0.234f,
                    PressureTurnScale = 0.243f,
                    ConflictBase = 0.394f,
                    ConflictTurnScale = 0.0142f,
                    ResourceDeltaScale = 0.958f,
                    ResourceNoiseScale = 2.02f,
                    RelationDeltaScale = 2.65f,
                    SatisfactionScale = 0.86f,
                    EarlySuppressionTurns = 13,
                    EarlyPositiveGainScale = 0.35f,
                    ShockChance = 0.38f,
                    ShockMagnitude = 30,
                    CaptureChanceScale = 2.72f,
                    LossChanceScale = 0.57f,
                    NonConflictNodeScale = 0.81f,
                    NodeDriftScale = 2.40f,
                    EasyDurationPerTurn = 23f,
                    StandardDurationPerTurn = 29f,
                    HardDurationPerTurn = 33f
                }
            };
        }

        private static List<TuningProfile> BuildR304NProfiles()
        {
            return new List<TuningProfile>
            {
                new TuningProfile
                {
                    RoundCode = "R154",
                    BatchId = "R3-04N-B001-R154",
                    Label = "r151-r305-gap-close-a",
                    EasyBias = 0.260f,
                    StandardBias = 0.154f,
                    HardBias = 0.050f,
                    EasyInitScale = 1.132f,
                    StandardInitScale = 1.108f,
                    HardInitScale = 1.060f,
                    SocialTradeInitScale = 0.16f,
                    EasyVictoryThreshold = 85.1f,
                    StandardVictoryThreshold = 83.1f,
                    HardVictoryThreshold = 81.4f,
                    EasyMilitaryCollapseThreshold = 5,
                    StandardMilitaryCollapseThreshold = 9,
                    HardMilitaryCollapseThreshold = 23,
                    PressureOffset = 0.235f,
                    PressureTurnScale = 0.246f,
                    ConflictBase = 0.395f,
                    ConflictTurnScale = 0.0144f,
                    ResourceDeltaScale = 0.957f,
                    ResourceNoiseScale = 2.02f,
                    RelationDeltaScale = 2.64f,
                    SatisfactionScale = 0.86f,
                    EarlySuppressionTurns = 14,
                    EarlyPositiveGainScale = 0.34f,
                    ShockChance = 0.38f,
                    ShockMagnitude = 30,
                    CaptureChanceScale = 2.70f,
                    LossChanceScale = 0.57f,
                    NonConflictNodeScale = 0.81f,
                    NodeDriftScale = 2.40f,
                    EasyDurationPerTurn = 23f,
                    StandardDurationPerTurn = 29f,
                    HardDurationPerTurn = 33f
                },
                new TuningProfile
                {
                    RoundCode = "R155",
                    BatchId = "R3-04N-B001-R155",
                    Label = "r151-r305-gap-close-b",
                    EasyBias = 0.261f,
                    StandardBias = 0.154f,
                    HardBias = 0.050f,
                    EasyInitScale = 1.133f,
                    StandardInitScale = 1.109f,
                    HardInitScale = 1.061f,
                    SocialTradeInitScale = 0.16f,
                    EasyVictoryThreshold = 85.0f,
                    StandardVictoryThreshold = 83.0f,
                    HardVictoryThreshold = 81.3f,
                    EasyMilitaryCollapseThreshold = 5,
                    StandardMilitaryCollapseThreshold = 9,
                    HardMilitaryCollapseThreshold = 23,
                    PressureOffset = 0.235f,
                    PressureTurnScale = 0.245f,
                    ConflictBase = 0.394f,
                    ConflictTurnScale = 0.0144f,
                    ResourceDeltaScale = 0.957f,
                    ResourceNoiseScale = 2.02f,
                    RelationDeltaScale = 2.64f,
                    SatisfactionScale = 0.86f,
                    EarlySuppressionTurns = 14,
                    EarlyPositiveGainScale = 0.34f,
                    ShockChance = 0.38f,
                    ShockMagnitude = 30,
                    CaptureChanceScale = 2.71f,
                    LossChanceScale = 0.57f,
                    NonConflictNodeScale = 0.81f,
                    NodeDriftScale = 2.40f,
                    EasyDurationPerTurn = 23f,
                    StandardDurationPerTurn = 29f,
                    HardDurationPerTurn = 33f
                },
                new TuningProfile
                {
                    RoundCode = "R156",
                    BatchId = "R3-04N-B001-R156",
                    Label = "r151-r305-gap-close-c",
                    EasyBias = 0.261f,
                    StandardBias = 0.153f,
                    HardBias = 0.049f,
                    EasyInitScale = 1.132f,
                    StandardInitScale = 1.108f,
                    HardInitScale = 1.060f,
                    SocialTradeInitScale = 0.16f,
                    EasyVictoryThreshold = 85.2f,
                    StandardVictoryThreshold = 83.1f,
                    HardVictoryThreshold = 81.5f,
                    EasyMilitaryCollapseThreshold = 5,
                    StandardMilitaryCollapseThreshold = 10,
                    HardMilitaryCollapseThreshold = 23,
                    PressureOffset = 0.236f,
                    PressureTurnScale = 0.246f,
                    ConflictBase = 0.395f,
                    ConflictTurnScale = 0.0145f,
                    ResourceDeltaScale = 0.957f,
                    ResourceNoiseScale = 2.02f,
                    RelationDeltaScale = 2.65f,
                    SatisfactionScale = 0.86f,
                    EarlySuppressionTurns = 14,
                    EarlyPositiveGainScale = 0.34f,
                    ShockChance = 0.38f,
                    ShockMagnitude = 30,
                    CaptureChanceScale = 2.69f,
                    LossChanceScale = 0.58f,
                    NonConflictNodeScale = 0.81f,
                    NodeDriftScale = 2.40f,
                    EasyDurationPerTurn = 23f,
                    StandardDurationPerTurn = 29f,
                    HardDurationPerTurn = 33f
                }
            };
        }

        private static List<TuningProfile> BuildR304OProfiles()
        {
            return new List<TuningProfile>
            {
                new TuningProfile
                {
                    RoundCode = "R160",
                    BatchId = "R3-04O-B002-R160",
                    Label = "r157-defeat-share-recover-a",
                    EasyBias = 0.262f,
                    StandardBias = 0.153f,
                    HardBias = 0.051f,
                    EasyInitScale = 1.135f,
                    StandardInitScale = 1.108f,
                    HardInitScale = 1.063f,
                    SocialTradeInitScale = 0.16f,
                    EasyVictoryThreshold = 85.0f,
                    StandardVictoryThreshold = 83.2f,
                    HardVictoryThreshold = 81.1f,
                    EasyMilitaryCollapseThreshold = 5,
                    StandardMilitaryCollapseThreshold = 10,
                    HardMilitaryCollapseThreshold = 22,
                    PressureOffset = 0.235f,
                    PressureTurnScale = 0.247f,
                    ConflictBase = 0.395f,
                    ConflictTurnScale = 0.0145f,
                    ResourceDeltaScale = 0.958f,
                    ResourceNoiseScale = 2.02f,
                    RelationDeltaScale = 2.64f,
                    SatisfactionScale = 0.86f,
                    EarlySuppressionTurns = 13,
                    EarlyPositiveGainScale = 0.35f,
                    ShockChance = 0.38f,
                    ShockMagnitude = 30,
                    CaptureChanceScale = 2.70f,
                    LossChanceScale = 0.58f,
                    NonConflictNodeScale = 0.81f,
                    NodeDriftScale = 2.40f,
                    EasyDurationPerTurn = 23f,
                    StandardDurationPerTurn = 29f,
                    HardDurationPerTurn = 33f
                },
                new TuningProfile
                {
                    RoundCode = "R161",
                    BatchId = "R3-04O-B002-R161",
                    Label = "r157-defeat-share-recover-b",
                    EasyBias = 0.262f,
                    StandardBias = 0.152f,
                    HardBias = 0.051f,
                    EasyInitScale = 1.135f,
                    StandardInitScale = 1.107f,
                    HardInitScale = 1.063f,
                    SocialTradeInitScale = 0.16f,
                    EasyVictoryThreshold = 84.9f,
                    StandardVictoryThreshold = 83.25f,
                    HardVictoryThreshold = 81.15f,
                    EasyMilitaryCollapseThreshold = 5,
                    StandardMilitaryCollapseThreshold = 10,
                    HardMilitaryCollapseThreshold = 22,
                    PressureOffset = 0.236f,
                    PressureTurnScale = 0.246f,
                    ConflictBase = 0.395f,
                    ConflictTurnScale = 0.0145f,
                    ResourceDeltaScale = 0.958f,
                    ResourceNoiseScale = 2.02f,
                    RelationDeltaScale = 2.64f,
                    SatisfactionScale = 0.86f,
                    EarlySuppressionTurns = 13,
                    EarlyPositiveGainScale = 0.35f,
                    ShockChance = 0.38f,
                    ShockMagnitude = 30,
                    CaptureChanceScale = 2.69f,
                    LossChanceScale = 0.58f,
                    NonConflictNodeScale = 0.81f,
                    NodeDriftScale = 2.40f,
                    EasyDurationPerTurn = 23f,
                    StandardDurationPerTurn = 29f,
                    HardDurationPerTurn = 33f
                },
                new TuningProfile
                {
                    RoundCode = "R162",
                    BatchId = "R3-04O-B002-R162",
                    Label = "r157-defeat-share-recover-c",
                    EasyBias = 0.262f,
                    StandardBias = 0.152f,
                    HardBias = 0.052f,
                    EasyInitScale = 1.135f,
                    StandardInitScale = 1.107f,
                    HardInitScale = 1.064f,
                    SocialTradeInitScale = 0.16f,
                    EasyVictoryThreshold = 84.9f,
                    StandardVictoryThreshold = 83.2f,
                    HardVictoryThreshold = 81.0f,
                    EasyMilitaryCollapseThreshold = 5,
                    StandardMilitaryCollapseThreshold = 10,
                    HardMilitaryCollapseThreshold = 22,
                    PressureOffset = 0.236f,
                    PressureTurnScale = 0.247f,
                    ConflictBase = 0.395f,
                    ConflictTurnScale = 0.0146f,
                    ResourceDeltaScale = 0.958f,
                    ResourceNoiseScale = 2.02f,
                    RelationDeltaScale = 2.65f,
                    SatisfactionScale = 0.86f,
                    EarlySuppressionTurns = 14,
                    EarlyPositiveGainScale = 0.34f,
                    ShockChance = 0.38f,
                    ShockMagnitude = 30,
                    CaptureChanceScale = 2.69f,
                    LossChanceScale = 0.58f,
                    NonConflictNodeScale = 0.81f,
                    NodeDriftScale = 2.40f,
                    EasyDurationPerTurn = 23f,
                    StandardDurationPerTurn = 29f,
                    HardDurationPerTurn = 33f
                }
            };
        }

        private static List<TuningProfile> BuildR304PProfiles()
        {
            var baseProfile = BuildR304OProfiles().FirstOrDefault(p => p.RoundCode == "R160") ?? BuildR304OProfiles().First();
            var profiles = new List<TuningProfile>(3);

            var r163 = CloneProfile(baseProfile);
            r163.RoundCode = "R163";
            r163.BatchId = "R3-04P-B001-R163";
            r163.Label = "r160-late-pressure-a";
            r163.LatePressureStartTurn = 14;
            r163.LatePressureTurnBonus = 0.090f;
            r163.LateConflictTurnBonus = 0.060f;
            r163.LateLossChanceBonus = 0.100f;
            r163.LateCaptureChancePenalty = 0.040f;
            profiles.Add(r163);

            var r164 = CloneProfile(baseProfile);
            r164.RoundCode = "R164";
            r164.BatchId = "R3-04P-B001-R164";
            r164.Label = "r160-late-pressure-b";
            r164.LatePressureStartTurn = 13;
            r164.LatePressureTurnBonus = 0.120f;
            r164.LateConflictTurnBonus = 0.080f;
            r164.LateLossChanceBonus = 0.130f;
            r164.LateCaptureChancePenalty = 0.055f;
            profiles.Add(r164);

            var r165 = CloneProfile(baseProfile);
            r165.RoundCode = "R165";
            r165.BatchId = "R3-04P-B001-R165";
            r165.Label = "r160-late-pressure-c";
            r165.LatePressureStartTurn = 15;
            r165.LatePressureTurnBonus = 0.070f;
            r165.LateConflictTurnBonus = 0.050f;
            r165.LateLossChanceBonus = 0.090f;
            r165.LateCaptureChancePenalty = 0.030f;
            profiles.Add(r165);

            return profiles;
        }

        private static List<TuningProfile> BuildR304QProfiles()
        {
            var baseProfile = BuildR304PProfiles().FirstOrDefault(p => p.RoundCode == "R165") ?? BuildR304PProfiles().First();
            var profiles = new List<TuningProfile>(3);

            var r166 = CloneProfile(baseProfile);
            r166.RoundCode = "R166";
            r166.BatchId = "R3-04Q-B001-R166";
            r166.Label = "r165-dual-gap-close-a";
            r166.EasyBias = 0.272f;
            r166.StandardBias = 0.150f;
            r166.HardBias = 0.048f;
            r166.EasyInitScale = 1.145f;
            r166.StandardInitScale = 1.107f;
            r166.HardInitScale = 1.062f;
            r166.EasyVictoryThreshold = 84.6f;
            r166.StandardVictoryThreshold = 83.3f;
            r166.HardVictoryThreshold = 81.2f;
            r166.StandardMilitaryCollapseThreshold = 11;
            r166.HardMilitaryCollapseThreshold = 23;
            r166.LatePressureStartTurn = 14;
            r166.LatePressureTurnBonus = 0.095f;
            r166.LateConflictTurnBonus = 0.065f;
            r166.LateLossChanceBonus = 0.110f;
            r166.LateCaptureChancePenalty = 0.042f;
            profiles.Add(r166);

            var r167 = CloneProfile(baseProfile);
            r167.RoundCode = "R167";
            r167.BatchId = "R3-04Q-B001-R167";
            r167.Label = "r165-dual-gap-close-b";
            r167.EasyBias = 0.274f;
            r167.StandardBias = 0.149f;
            r167.HardBias = 0.047f;
            r167.EasyInitScale = 1.146f;
            r167.StandardInitScale = 1.106f;
            r167.HardInitScale = 1.062f;
            r167.EasyVictoryThreshold = 84.4f;
            r167.StandardVictoryThreshold = 83.3f;
            r167.HardVictoryThreshold = 81.3f;
            r167.StandardMilitaryCollapseThreshold = 11;
            r167.HardMilitaryCollapseThreshold = 24;
            r167.LatePressureStartTurn = 13;
            r167.LatePressureTurnBonus = 0.110f;
            r167.LateConflictTurnBonus = 0.075f;
            r167.LateLossChanceBonus = 0.125f;
            r167.LateCaptureChancePenalty = 0.050f;
            profiles.Add(r167);

            var r168 = CloneProfile(baseProfile);
            r168.RoundCode = "R168";
            r168.BatchId = "R3-04Q-B001-R168";
            r168.Label = "r165-dual-gap-close-c";
            r168.EasyBias = 0.276f;
            r168.StandardBias = 0.151f;
            r168.HardBias = 0.046f;
            r168.EasyInitScale = 1.148f;
            r168.StandardInitScale = 1.107f;
            r168.HardInitScale = 1.062f;
            r168.EasyVictoryThreshold = 84.3f;
            r168.StandardVictoryThreshold = 83.25f;
            r168.HardVictoryThreshold = 81.35f;
            r168.StandardMilitaryCollapseThreshold = 12;
            r168.HardMilitaryCollapseThreshold = 24;
            r168.LatePressureStartTurn = 15;
            r168.LatePressureTurnBonus = 0.080f;
            r168.LateConflictTurnBonus = 0.055f;
            r168.LateLossChanceBonus = 0.105f;
            r168.LateCaptureChancePenalty = 0.035f;
            profiles.Add(r168);

            return profiles;
        }

        private static List<TuningProfile> BuildR304RProfiles()
        {
            var baseProfile = BuildR304QProfiles().FirstOrDefault(p => p.RoundCode == "R166") ?? BuildR304QProfiles().First();
            var profiles = new List<TuningProfile>(3);

            var r169 = CloneProfile(baseProfile);
            r169.RoundCode = "R169";
            r169.BatchId = "R3-04R-B001-R169";
            r169.Label = "r166-standard-lift-a";
            r169.StandardBias = 0.156f;
            r169.StandardInitScale = 1.112f;
            r169.StandardVictoryThreshold = 83.05f;
            r169.LatePressureStartTurn = 15;
            r169.LatePressureTurnBonus = 0.088f;
            r169.LateConflictTurnBonus = 0.058f;
            r169.LateLossChanceBonus = 0.103f;
            r169.LateCaptureChancePenalty = 0.040f;
            profiles.Add(r169);

            var r170 = CloneProfile(baseProfile);
            r170.RoundCode = "R170";
            r170.BatchId = "R3-04R-B001-R170";
            r170.Label = "r166-standard-lift-b";
            r170.StandardBias = 0.154f;
            r170.StandardInitScale = 1.110f;
            r170.StandardVictoryThreshold = 83.10f;
            r170.LatePressureStartTurn = 15;
            r170.LatePressureTurnBonus = 0.090f;
            r170.LateConflictTurnBonus = 0.060f;
            r170.LateLossChanceBonus = 0.105f;
            r170.LateCaptureChancePenalty = 0.041f;
            profiles.Add(r170);

            var r171 = CloneProfile(baseProfile);
            r171.RoundCode = "R171";
            r171.BatchId = "R3-04R-B001-R171";
            r171.Label = "r166-standard-lift-c";
            r171.StandardBias = 0.158f;
            r171.StandardInitScale = 1.114f;
            r171.StandardVictoryThreshold = 83.00f;
            r171.LatePressureStartTurn = 14;
            r171.LatePressureTurnBonus = 0.092f;
            r171.LateConflictTurnBonus = 0.062f;
            r171.LateLossChanceBonus = 0.107f;
            r171.LateCaptureChancePenalty = 0.040f;
            profiles.Add(r171);

            return profiles;
        }

        private static List<TuningProfile> BuildR304SProfiles()
        {
            var baseProfile = BuildR304RProfiles().FirstOrDefault(p => p.RoundCode == "R171") ?? BuildR304RProfiles().First();
            var profiles = new List<TuningProfile>(3);

            var r172 = CloneProfile(baseProfile);
            r172.RoundCode = "R172";
            r172.BatchId = "R3-04S-B001-R172";
            r172.Label = "r171-defeat-lift-a";
            r172.StandardBias = 0.157f;
            r172.LatePressureStartTurn = 14;
            r172.LatePressureTurnBonus = 0.094f;
            r172.LateConflictTurnBonus = 0.066f;
            r172.LateLossChanceBonus = 0.114f;
            r172.LateCaptureChancePenalty = 0.041f;
            profiles.Add(r172);

            var r173 = CloneProfile(baseProfile);
            r173.RoundCode = "R173";
            r173.BatchId = "R3-04S-B001-R173";
            r173.Label = "r171-defeat-lift-b";
            r173.StandardBias = 0.156f;
            r173.LatePressureStartTurn = 13;
            r173.LatePressureTurnBonus = 0.100f;
            r173.LateConflictTurnBonus = 0.070f;
            r173.LateLossChanceBonus = 0.118f;
            r173.LateCaptureChancePenalty = 0.043f;
            profiles.Add(r173);

            var r174 = CloneProfile(baseProfile);
            r174.RoundCode = "R174";
            r174.BatchId = "R3-04S-B001-R174";
            r174.Label = "r171-defeat-lift-c";
            r174.StandardBias = 0.158f;
            r174.HardBias = 0.050f;
            r174.HardInitScale = 1.064f;
            r174.StandardMilitaryCollapseThreshold = 12;
            r174.HardMilitaryCollapseThreshold = 24;
            r174.LatePressureStartTurn = 14;
            r174.LatePressureTurnBonus = 0.095f;
            r174.LateConflictTurnBonus = 0.066f;
            r174.LateLossChanceBonus = 0.112f;
            r174.LateCaptureChancePenalty = 0.040f;
            profiles.Add(r174);

            return profiles;
        }

        private static List<TuningProfile> BuildR304TProfiles()
        {
            var baseProfile = BuildR304SProfiles().FirstOrDefault(p => p.RoundCode == "R173") ?? BuildR304SProfiles().First();
            var profiles = new List<TuningProfile>(3);

            var r175 = CloneProfile(baseProfile);
            r175.RoundCode = "R175";
            r175.BatchId = "R3-04T-B001-R175";
            r175.Label = "r173-endturn-floor-a";
            r175.EasyVictoryThreshold = 84.75f;
            r175.StandardVictoryThreshold = 83.08f;
            r175.HardVictoryThreshold = 81.24f;
            r175.LatePressureStartTurn = 13;
            r175.LatePressureTurnBonus = 0.099f;
            r175.LateConflictTurnBonus = 0.069f;
            r175.LateLossChanceBonus = 0.118f;
            r175.LateCaptureChancePenalty = 0.043f;
            profiles.Add(r175);

            var r176 = CloneProfile(baseProfile);
            r176.RoundCode = "R176";
            r176.BatchId = "R3-04T-B001-R176";
            r176.Label = "r173-endturn-floor-b";
            r176.StandardBias = 0.157f;
            r176.StandardInitScale = 1.115f;
            r176.EasyVictoryThreshold = 84.80f;
            r176.StandardVictoryThreshold = 83.10f;
            r176.HardVictoryThreshold = 81.26f;
            r176.LatePressureStartTurn = 14;
            r176.LatePressureTurnBonus = 0.096f;
            r176.LateConflictTurnBonus = 0.067f;
            r176.LateLossChanceBonus = 0.116f;
            r176.LateCaptureChancePenalty = 0.042f;
            profiles.Add(r176);

            var r177 = CloneProfile(baseProfile);
            r177.RoundCode = "R177";
            r177.BatchId = "R3-04T-B001-R177";
            r177.Label = "r173-endturn-floor-c";
            r177.EasyVictoryThreshold = 84.70f;
            r177.StandardVictoryThreshold = 83.06f;
            r177.HardVictoryThreshold = 81.22f;
            r177.LatePressureStartTurn = 13;
            r177.LatePressureTurnBonus = 0.100f;
            r177.LateConflictTurnBonus = 0.070f;
            r177.LateLossChanceBonus = 0.121f;
            r177.LateCaptureChancePenalty = 0.044f;
            profiles.Add(r177);

            return profiles;
        }

        private static List<TuningProfile> BuildR304UProfiles()
        {
            var baseProfile = BuildR304SProfiles().FirstOrDefault(p => p.RoundCode == "R173") ?? BuildR304SProfiles().First();
            var profiles = new List<TuningProfile>(3);

            var r178 = CloneProfile(baseProfile);
            r178.RoundCode = "R178";
            r178.BatchId = "R3-04U-B001-R178";
            r178.Label = "r173-endturn-correct-a";
            r178.StandardBias = 0.157f;
            r178.EasyVictoryThreshold = 84.65f;
            r178.StandardVictoryThreshold = 83.03f;
            r178.HardVictoryThreshold = 81.23f;
            profiles.Add(r178);

            var r179 = CloneProfile(baseProfile);
            r179.RoundCode = "R179";
            r179.BatchId = "R3-04U-B001-R179";
            r179.Label = "r173-endturn-correct-b";
            r179.StandardBias = 0.158f;
            r179.EasyVictoryThreshold = 84.68f;
            r179.StandardVictoryThreshold = 83.05f;
            r179.HardVictoryThreshold = 81.24f;
            profiles.Add(r179);

            var r180 = CloneProfile(baseProfile);
            r180.RoundCode = "R180";
            r180.BatchId = "R3-04U-B001-R180";
            r180.Label = "r173-endturn-correct-c";
            r180.StandardBias = 0.159f;
            r180.EasyVictoryThreshold = 84.72f;
            r180.StandardVictoryThreshold = 83.07f;
            r180.HardVictoryThreshold = 81.25f;
            profiles.Add(r180);

            return profiles;
        }

        private static List<TuningProfile> BuildR304VProfiles()
        {
            var baseProfile = BuildR304UProfiles().FirstOrDefault(p => p.RoundCode == "R180") ?? BuildR304UProfiles().First();
            var profiles = new List<TuningProfile>(3);

            var r181 = CloneProfile(baseProfile);
            r181.RoundCode = "R181";
            r181.BatchId = "R3-04V-B001-R181";
            r181.Label = "r180-stability-recover-a";
            r181.EasyVictoryThreshold = 84.82f;
            r181.StandardVictoryThreshold = 83.12f;
            r181.HardVictoryThreshold = 81.22f;
            r181.HardBias = 0.052f;
            r181.HardInitScale = 1.066f;
            r181.LatePressureStartTurn = 14;
            r181.LatePressureTurnBonus = 0.096f;
            r181.LateConflictTurnBonus = 0.067f;
            r181.LateLossChanceBonus = 0.114f;
            r181.LateCaptureChancePenalty = 0.041f;
            profiles.Add(r181);

            var r182 = CloneProfile(baseProfile);
            r182.RoundCode = "R182";
            r182.BatchId = "R3-04V-B001-R182";
            r182.Label = "r180-stability-recover-b";
            r182.EasyVictoryThreshold = 84.80f;
            r182.StandardVictoryThreshold = 83.10f;
            r182.HardVictoryThreshold = 81.20f;
            r182.HardBias = 0.051f;
            r182.HardInitScale = 1.065f;
            r182.LatePressureStartTurn = 14;
            r182.LatePressureTurnBonus = 0.095f;
            r182.LateConflictTurnBonus = 0.066f;
            r182.LateLossChanceBonus = 0.113f;
            r182.LateCaptureChancePenalty = 0.041f;
            profiles.Add(r182);

            var r183 = CloneProfile(baseProfile);
            r183.RoundCode = "R183";
            r183.BatchId = "R3-04V-B001-R183";
            r183.Label = "r180-stability-recover-c";
            r183.EasyVictoryThreshold = 84.78f;
            r183.StandardVictoryThreshold = 83.09f;
            r183.HardVictoryThreshold = 81.18f;
            r183.HardBias = 0.053f;
            r183.HardInitScale = 1.067f;
            r183.LatePressureStartTurn = 14;
            r183.LatePressureTurnBonus = 0.094f;
            r183.LateConflictTurnBonus = 0.066f;
            r183.LateLossChanceBonus = 0.112f;
            r183.LateCaptureChancePenalty = 0.040f;
            profiles.Add(r183);

            return profiles;
        }

        private static List<TuningProfile> BuildR304WProfiles()
        {
            var baseProfile = BuildR304UProfiles().FirstOrDefault(p => p.RoundCode == "R180") ?? BuildR304UProfiles().First();
            var profiles = new List<TuningProfile>(3);

            var r184 = CloneProfile(baseProfile);
            r184.RoundCode = "R184";
            r184.BatchId = "R3-04W-B001-R184";
            r184.Label = "r180-r305-gapfix-a";
            r184.EasyVictoryThreshold = 84.96f;
            r184.StandardVictoryThreshold = 83.10f;
            r184.HardVictoryThreshold = 81.15f;
            r184.HardBias = 0.054f;
            r184.HardInitScale = 1.072f;
            r184.EarlySuppressionTurns = 14;
            r184.EarlyPositiveGainScale = 0.34f;
            r184.ConflictBase = 0.398f;
            r184.LossChanceScale = 1.01f;
            profiles.Add(r184);

            var r185 = CloneProfile(baseProfile);
            r185.RoundCode = "R185";
            r185.BatchId = "R3-04W-B001-R185";
            r185.Label = "r180-r305-gapfix-b";
            r185.EasyVictoryThreshold = 85.02f;
            r185.StandardVictoryThreshold = 83.12f;
            r185.HardVictoryThreshold = 81.17f;
            r185.HardBias = 0.053f;
            r185.HardInitScale = 1.070f;
            r185.EarlySuppressionTurns = 14;
            r185.EarlyPositiveGainScale = 0.33f;
            r185.LatePressureStartTurn = 14;
            r185.LatePressureTurnBonus = 0.097f;
            r185.LateConflictTurnBonus = 0.068f;
            r185.LateLossChanceBonus = 0.115f;
            r185.LateCaptureChancePenalty = 0.041f;
            r185.LossChanceScale = 1.01f;
            profiles.Add(r185);

            var r186 = CloneProfile(baseProfile);
            r186.RoundCode = "R186";
            r186.BatchId = "R3-04W-B001-R186";
            r186.Label = "r180-r305-gapfix-c";
            r186.EasyVictoryThreshold = 84.92f;
            r186.StandardVictoryThreshold = 83.09f;
            r186.HardVictoryThreshold = 81.13f;
            r186.HardBias = 0.055f;
            r186.HardInitScale = 1.073f;
            r186.EarlySuppressionTurns = 14;
            r186.EarlyPositiveGainScale = 0.34f;
            r186.CaptureChanceScale = 0.99f;
            r186.LossChanceScale = 1.02f;
            r186.LateLossChanceBonus = 0.119f;
            r186.LateCaptureChancePenalty = 0.044f;
            profiles.Add(r186);

            return profiles;
        }

        private static List<TuningProfile> BuildR304XProfiles()
        {
            var baseProfile = BuildR304WProfiles().FirstOrDefault(p => p.RoundCode == "R185") ?? BuildR304WProfiles().First();
            var profiles = new List<TuningProfile>(3);

            var r187 = CloneProfile(baseProfile);
            r187.RoundCode = "R187";
            r187.BatchId = "R3-04X-B001-R187";
            r187.Label = "r185-residue-close-a";
            r187.EasyVictoryThreshold = 84.94f;
            r187.StandardVictoryThreshold = 83.11f;
            r187.HardVictoryThreshold = 81.10f;
            r187.HardBias = 0.054f;
            r187.HardInitScale = 1.072f;
            r187.CaptureChanceScale = 1.02f;
            r187.LossChanceScale = 1.00f;
            profiles.Add(r187);

            var r188 = CloneProfile(baseProfile);
            r188.RoundCode = "R188";
            r188.BatchId = "R3-04X-B001-R188";
            r188.Label = "r185-residue-close-b";
            r188.EasyVictoryThreshold = 84.96f;
            r188.StandardVictoryThreshold = 83.12f;
            r188.HardVictoryThreshold = 81.08f;
            r188.HardBias = 0.055f;
            r188.HardInitScale = 1.073f;
            r188.CaptureChanceScale = 1.03f;
            r188.LossChanceScale = 0.99f;
            r188.LateLossChanceBonus = 0.114f;
            profiles.Add(r188);

            var r189 = CloneProfile(baseProfile);
            r189.RoundCode = "R189";
            r189.BatchId = "R3-04X-B001-R189";
            r189.Label = "r185-residue-close-c";
            r189.EasyVictoryThreshold = 84.92f;
            r189.StandardVictoryThreshold = 83.10f;
            r189.HardVictoryThreshold = 81.06f;
            r189.HardBias = 0.056f;
            r189.HardInitScale = 1.074f;
            r189.CaptureChanceScale = 1.04f;
            r189.LossChanceScale = 0.99f;
            r189.LateCaptureChancePenalty = 0.040f;
            profiles.Add(r189);

            return profiles;
        }

        private static List<TuningProfile> BuildR304YProfiles()
        {
            var baseProfile = BuildR304WProfiles().FirstOrDefault(p => p.RoundCode == "R185") ?? BuildR304WProfiles().First();
            var profiles = new List<TuningProfile>(3);

            var r190 = CloneProfile(baseProfile);
            r190.RoundCode = "R190";
            r190.BatchId = "R3-04Y-B001-R190";
            r190.Label = "r185-gap-close-a";
            r190.EasyVictoryThreshold = 84.96f;
            r190.StandardVictoryThreshold = 83.12f;
            r190.HardVictoryThreshold = 81.10f;
            r190.HardBias = 0.054f;
            r190.HardInitScale = 1.073f;
            r190.SocialTradeInitScale = 0.155f;
            r190.CaptureChanceScale = 1.03f;
            r190.LossChanceScale = 1.00f;
            r190.LateLossChanceBonus = 0.114f;
            r190.LateCaptureChancePenalty = 0.040f;
            profiles.Add(r190);

            var r191 = CloneProfile(baseProfile);
            r191.RoundCode = "R191";
            r191.BatchId = "R3-04Y-B001-R191";
            r191.Label = "r185-gap-close-b";
            r191.EasyVictoryThreshold = 84.98f;
            r191.StandardVictoryThreshold = 83.12f;
            r191.HardVictoryThreshold = 81.08f;
            r191.HardBias = 0.055f;
            r191.HardInitScale = 1.074f;
            r191.SocialTradeInitScale = 0.154f;
            r191.CaptureChanceScale = 1.04f;
            r191.LossChanceScale = 0.99f;
            r191.NonConflictNodeScale = 0.39f;
            r191.RelationDeltaScale = 1.01f;
            r191.LateLossChanceBonus = 0.113f;
            profiles.Add(r191);

            var r192 = CloneProfile(baseProfile);
            r192.RoundCode = "R192";
            r192.BatchId = "R3-04Y-B001-R192";
            r192.Label = "r185-gap-close-c";
            r192.EasyVictoryThreshold = 84.94f;
            r192.StandardVictoryThreshold = 83.11f;
            r192.HardVictoryThreshold = 81.06f;
            r192.HardBias = 0.056f;
            r192.HardInitScale = 1.075f;
            r192.SocialTradeInitScale = 0.156f;
            r192.CaptureChanceScale = 1.02f;
            r192.LossChanceScale = 1.00f;
            r192.RelationDeltaScale = 1.01f;
            r192.LateCaptureChancePenalty = 0.040f;
            profiles.Add(r192);

            return profiles;
        }

        private static List<TuningProfile> BuildR304ZProfiles()
        {
            var baseProfile = BuildR304WProfiles().FirstOrDefault(p => p.RoundCode == "R185") ?? BuildR304WProfiles().First();
            var profiles = new List<TuningProfile>(3);

            var r193 = CloneProfile(baseProfile);
            r193.RoundCode = "R193";
            r193.BatchId = "R3-04Z-B001-R193";
            r193.Label = "r185-gap-close-z-a";
            r193.EasyVictoryThreshold = 84.92f;
            r193.StandardVictoryThreshold = 83.10f;
            r193.HardVictoryThreshold = 80.95f;
            r193.HardBias = 0.052f;
            r193.HardInitScale = 1.074f;
            r193.SocialTradeInitScale = 0.153f;
            r193.CaptureChanceScale = 2.76f;
            r193.LossChanceScale = 0.95f;
            r193.NonConflictNodeScale = 0.84f;
            r193.RelationDeltaScale = 2.72f;
            r193.LateLossChanceBonus = 0.112f;
            r193.LateCaptureChancePenalty = 0.039f;
            profiles.Add(r193);

            var r194 = CloneProfile(baseProfile);
            r194.RoundCode = "R194";
            r194.BatchId = "R3-04Z-B001-R194";
            r194.Label = "r185-gap-close-z-b";
            r194.EasyVictoryThreshold = 84.88f;
            r194.StandardVictoryThreshold = 83.06f;
            r194.HardVictoryThreshold = 80.90f;
            r194.HardBias = 0.051f;
            r194.HardInitScale = 1.076f;
            r194.SocialTradeInitScale = 0.150f;
            r194.ConflictBase = 0.399f;
            r194.CaptureChanceScale = 2.82f;
            r194.LossChanceScale = 0.93f;
            r194.NonConflictNodeScale = 0.86f;
            r194.RelationDeltaScale = 2.78f;
            r194.LateConflictTurnBonus = 0.070f;
            r194.LateLossChanceBonus = 0.111f;
            r194.LateCaptureChancePenalty = 0.038f;
            profiles.Add(r194);

            var r195 = CloneProfile(baseProfile);
            r195.RoundCode = "R195";
            r195.BatchId = "R3-04Z-B001-R195";
            r195.Label = "r185-gap-close-z-c";
            r195.EasyVictoryThreshold = 84.95f;
            r195.StandardVictoryThreshold = 83.08f;
            r195.HardVictoryThreshold = 80.92f;
            r195.HardBias = 0.053f;
            r195.HardInitScale = 1.075f;
            r195.SocialTradeInitScale = 0.152f;
            r195.ConflictBase = 0.400f;
            r195.CaptureChanceScale = 2.88f;
            r195.LossChanceScale = 0.90f;
            r195.NonConflictNodeScale = 0.88f;
            r195.RelationDeltaScale = 2.80f;
            r195.LateLossChanceBonus = 0.110f;
            r195.LateCaptureChancePenalty = 0.037f;
            profiles.Add(r195);

            return profiles;
        }

        private static List<TuningProfile> BuildR304AAProfiles()
        {
            var baseProfile = BuildR304ZProfiles().FirstOrDefault(p => p.RoundCode == "R194") ?? BuildR304ZProfiles().First();
            var profiles = new List<TuningProfile>(3);

            var r196 = CloneProfile(baseProfile);
            r196.RoundCode = "R196";
            r196.BatchId = "R3-04AA-B001-R196";
            r196.Label = "r194-pace-floor-a";
            r196.ConflictBase = 0.397f;
            r196.LatePressureStartTurn = 15;
            r196.LatePressureTurnBonus = 0.093f;
            r196.LateConflictTurnBonus = 0.066f;
            r196.LateLossChanceBonus = 0.106f;
            r196.LateCaptureChancePenalty = 0.036f;
            profiles.Add(r196);

            var r197 = CloneProfile(baseProfile);
            r197.RoundCode = "R197";
            r197.BatchId = "R3-04AA-B001-R197";
            r197.Label = "r194-pace-floor-b";
            r197.ConflictBase = 0.398f;
            r197.LatePressureStartTurn = 15;
            r197.LatePressureTurnBonus = 0.095f;
            r197.LateConflictTurnBonus = 0.067f;
            r197.LateLossChanceBonus = 0.108f;
            r197.LateCaptureChancePenalty = 0.037f;
            profiles.Add(r197);

            var r198 = CloneProfile(baseProfile);
            r198.RoundCode = "R198";
            r198.BatchId = "R3-04AA-B001-R198";
            r198.Label = "r194-pace-floor-c";
            r198.ConflictBase = 0.397f;
            r198.LatePressureStartTurn = 14;
            r198.LatePressureTurnBonus = 0.094f;
            r198.LateConflictTurnBonus = 0.066f;
            r198.LateLossChanceBonus = 0.107f;
            r198.LateCaptureChancePenalty = 0.036f;
            profiles.Add(r198);

            return profiles;
        }

        private static List<TuningProfile> BuildR304ABProfiles()
        {
            var baseProfile = BuildR304AAProfiles().FirstOrDefault(p => p.RoundCode == "R198") ?? BuildR304AAProfiles().First();
            var profiles = new List<TuningProfile>(3);

            var r199 = CloneProfile(baseProfile);
            r199.RoundCode = "R199";
            r199.BatchId = "R3-04AB-B001-R199";
            r199.Label = "r198-hard-survival-a";
            r199.HardVictoryThreshold = 81.10f;
            r199.HardInitScale = 1.078f;
            r199.HardMilitaryCollapseThreshold = 20;
            profiles.Add(r199);

            var r200 = CloneProfile(baseProfile);
            r200.RoundCode = "R200";
            r200.BatchId = "R3-04AB-B001-R200";
            r200.Label = "r198-hard-survival-b";
            r200.HardVictoryThreshold = 81.20f;
            r200.HardBias = 0.052f;
            r200.HardInitScale = 1.080f;
            r200.HardMilitaryCollapseThreshold = 19;
            profiles.Add(r200);

            var r201 = CloneProfile(baseProfile);
            r201.RoundCode = "R201";
            r201.BatchId = "R3-04AB-B001-R201";
            r201.Label = "r198-hard-survival-c";
            r201.HardVictoryThreshold = 81.25f;
            r201.HardBias = 0.053f;
            r201.HardInitScale = 1.082f;
            r201.HardMilitaryCollapseThreshold = 18;
            profiles.Add(r201);

            return profiles;
        }

        private static List<TuningProfile> BuildR304ACProfiles()
        {
            var baseProfile = BuildR304ABProfiles().FirstOrDefault(p => p.RoundCode == "R201") ?? BuildR304ABProfiles().First();
            var profiles = new List<TuningProfile>(3);

            var r202 = CloneProfile(baseProfile);
            r202.RoundCode = "R202";
            r202.BatchId = "R3-04AC-B001-R202";
            r202.Label = "r201-stability-close-a";
            r202.ShockChance = 0.34f;
            r202.ShockMagnitude = 28;
            r202.EasyMilitaryCollapseThreshold = 6;
            r202.StandardMilitaryCollapseThreshold = 11;
            r202.HardMilitaryCollapseThreshold = 17;
            r202.HardVictoryThreshold = 81.22f;
            r202.HardInitScale = 1.083f;
            profiles.Add(r202);

            var r203 = CloneProfile(baseProfile);
            r203.RoundCode = "R203";
            r203.BatchId = "R3-04AC-B001-R203";
            r203.Label = "r201-stability-close-b";
            r203.ShockChance = 0.33f;
            r203.ShockMagnitude = 26;
            r203.EasyVictoryThreshold = 84.92f;
            r203.StandardVictoryThreshold = 83.09f;
            r203.EasyMilitaryCollapseThreshold = 6;
            r203.StandardMilitaryCollapseThreshold = 12;
            r203.HardMilitaryCollapseThreshold = 17;
            r203.HardVictoryThreshold = 81.20f;
            r203.HardInitScale = 1.084f;
            r203.StandardBias = 0.158f;
            profiles.Add(r203);

            var r204 = CloneProfile(baseProfile);
            r204.RoundCode = "R204";
            r204.BatchId = "R3-04AC-B001-R204";
            r204.Label = "r201-stability-close-c";
            r204.ShockChance = 0.35f;
            r204.ShockMagnitude = 28;
            r204.EasyVictoryThreshold = 84.94f;
            r204.StandardVictoryThreshold = 83.10f;
            r204.EasyMilitaryCollapseThreshold = 6;
            r204.StandardMilitaryCollapseThreshold = 11;
            r204.HardMilitaryCollapseThreshold = 17;
            r204.HardVictoryThreshold = 81.18f;
            r204.HardBias = 0.052f;
            r204.HardInitScale = 1.084f;
            r204.StandardBias = 0.159f;
            profiles.Add(r204);

            return profiles;
        }

        private static List<TuningProfile> BuildR304ADProfiles()
        {
            var baseProfile = BuildR304ACProfiles().FirstOrDefault(p => p.RoundCode == "R204") ?? BuildR304ACProfiles().First();
            var profiles = new List<TuningProfile>(3);

            var r205 = CloneProfile(baseProfile);
            r205.RoundCode = "R205";
            r205.BatchId = "R3-04AD-B001-R205";
            r205.Label = "r204-variance-close-a";
            r205.ResourceNoiseScale = 1.96f;
            r205.ShockChance = 0.31f;
            r205.ShockMagnitude = 24;
            r205.EasyVictoryThreshold = 85.06f;
            r205.StandardInitScale = 1.112f;
            r205.StandardVictoryThreshold = 83.12f;
            r205.HardBias = 0.051f;
            r205.HardInitScale = 1.086f;
            r205.HardVictoryThreshold = 81.20f;
            profiles.Add(r205);

            var r206 = CloneProfile(baseProfile);
            r206.RoundCode = "R206";
            r206.BatchId = "R3-04AD-B001-R206";
            r206.Label = "r204-variance-close-b";
            r206.ResourceNoiseScale = 1.94f;
            r206.ShockChance = 0.30f;
            r206.ShockMagnitude = 22;
            r206.EasyVictoryThreshold = 85.10f;
            r206.StandardInitScale = 1.113f;
            r206.StandardVictoryThreshold = 83.11f;
            r206.HardBias = 0.051f;
            r206.HardInitScale = 1.087f;
            r206.HardVictoryThreshold = 81.20f;
            profiles.Add(r206);

            var r207 = CloneProfile(baseProfile);
            r207.RoundCode = "R207";
            r207.BatchId = "R3-04AD-B001-R207";
            r207.Label = "r204-variance-close-c";
            r207.ResourceNoiseScale = 1.95f;
            r207.ShockChance = 0.32f;
            r207.ShockMagnitude = 24;
            r207.EasyVictoryThreshold = 85.08f;
            r207.StandardInitScale = 1.112f;
            r207.StandardVictoryThreshold = 83.10f;
            r207.HardBias = 0.050f;
            r207.HardInitScale = 1.086f;
            r207.HardVictoryThreshold = 81.22f;
            profiles.Add(r207);

            return profiles;
        }

        private static List<TuningProfile> BuildR304AEProfiles()
        {
            var baseProfile = BuildR304ADProfiles().FirstOrDefault(p => p.RoundCode == "R205") ?? BuildR304ADProfiles().First();
            var profiles = new List<TuningProfile>(3);

            var r208 = CloneProfile(baseProfile);
            r208.RoundCode = "R208";
            r208.BatchId = "R3-04AE-B001-R208";
            r208.Label = "r205-converge-close-a";
            r208.EasyBias = 0.274f;
            r208.EasyVictoryThreshold = 84.78f;
            r208.StandardBias = 0.162f;
            r208.StandardInitScale = 1.114f;
            r208.StandardVictoryThreshold = 83.08f;
            r208.StandardMilitaryCollapseThreshold = 12;
            r208.HardBias = 0.052f;
            r208.HardInitScale = 1.088f;
            r208.HardVictoryThreshold = 81.14f;
            r208.LatePressureStartTurn = 13;
            r208.LateConflictTurnBonus = 0.069f;
            r208.LateLossChanceBonus = 0.118f;
            r208.LateCaptureChancePenalty = 0.038f;
            profiles.Add(r208);

            var r209 = CloneProfile(baseProfile);
            r209.RoundCode = "R209";
            r209.BatchId = "R3-04AE-B001-R209";
            r209.Label = "r205-converge-close-b";
            r209.ResourceNoiseScale = 1.95f;
            r209.ShockChance = 0.32f;
            r209.EasyBias = 0.275f;
            r209.EasyVictoryThreshold = 84.72f;
            r209.StandardBias = 0.161f;
            r209.StandardInitScale = 1.113f;
            r209.StandardVictoryThreshold = 83.09f;
            r209.HardBias = 0.052f;
            r209.HardInitScale = 1.089f;
            r209.HardVictoryThreshold = 81.12f;
            r209.LatePressureStartTurn = 13;
            r209.LateConflictTurnBonus = 0.068f;
            r209.LateLossChanceBonus = 0.116f;
            r209.LateCaptureChancePenalty = 0.037f;
            profiles.Add(r209);

            var r210 = CloneProfile(baseProfile);
            r210.RoundCode = "R210";
            r210.BatchId = "R3-04AE-B001-R210";
            r210.Label = "r205-converge-close-c";
            r210.ShockChance = 0.33f;
            r210.EasyBias = 0.273f;
            r210.EasyVictoryThreshold = 84.82f;
            r210.StandardBias = 0.163f;
            r210.StandardInitScale = 1.115f;
            r210.StandardVictoryThreshold = 83.07f;
            r210.StandardMilitaryCollapseThreshold = 12;
            r210.HardBias = 0.053f;
            r210.HardInitScale = 1.090f;
            r210.HardVictoryThreshold = 81.10f;
            r210.PressureTurnScale = 0.252f;
            r210.ConflictTurnScale = 0.016f;
            r210.LatePressureStartTurn = 12;
            r210.LateConflictTurnBonus = 0.071f;
            r210.LateLossChanceBonus = 0.121f;
            r210.LateCaptureChancePenalty = 0.039f;
            profiles.Add(r210);

            return profiles;
        }

        private static List<TuningProfile> BuildR304AFProfiles()
        {
            var baseProfile = BuildR304AEProfiles().FirstOrDefault(p => p.RoundCode == "R208") ?? BuildR304AEProfiles().First();
            var profiles = new List<TuningProfile>(3);

            var r211 = CloneProfile(baseProfile);
            r211.RoundCode = "R211";
            r211.BatchId = "R3-04AF-B001-R211";
            r211.Label = "r208-hard-timeout-convert-a";
            r211.HardVictoryThreshold = 81.06f;
            profiles.Add(r211);

            var r212 = CloneProfile(baseProfile);
            r212.RoundCode = "R212";
            r212.BatchId = "R3-04AF-B001-R212";
            r212.Label = "r208-hard-timeout-convert-b";
            r212.HardVictoryThreshold = 80.98f;
            profiles.Add(r212);

            var r213 = CloneProfile(baseProfile);
            r213.RoundCode = "R213";
            r213.BatchId = "R3-04AF-B001-R213";
            r213.Label = "r208-hard-timeout-convert-c";
            r213.HardVictoryThreshold = 80.90f;
            profiles.Add(r213);

            return profiles;
        }

        private static List<TuningProfile> BuildR304AGProfiles()
        {
            var baseProfile = BuildR304ABProfiles().FirstOrDefault(p => p.RoundCode == "R201") ?? BuildR304ABProfiles().First();
            var profiles = new List<TuningProfile>(100);

            var r262 = CloneProfile(baseProfile);
            r262.RoundCode = "R262";
            r262.BatchId = "R3-04AG-B008-R262";
            r262.Label = "r201-stability-dual-gap-c";
            r262.EasyMilitaryCollapseThreshold = 7;
            r262.StandardMilitaryCollapseThreshold = 13;
            r262.HardMilitaryCollapseThreshold = 17;
            r262.HardBias = 0.054f;
            r262.HardInitScale = 1.084f;
            r262.HardVictoryThreshold = 81.20f;
            r262.LateConflictTurnBonus = 0.067f;
            r262.LateLossChanceBonus = 0.112f;
            r262.LateCaptureChancePenalty = 0.037f;

            var r276 = CloneProfile(r262);
            r276.RoundCode = "R276";
            r276.BatchId = "R3-04AG-B010-R276";
            r276.Label = "r262-fine-closure-e";
            r276.EasyMilitaryCollapseThreshold = 8;
            r276.HardMilitaryCollapseThreshold = 16;
            r276.HardBias = 0.055f;
            r276.HardInitScale = 1.085f;
            r276.HardVictoryThreshold = 81.18f;
            r276.LateLossChanceBonus = 0.113f;

            var r287 = CloneProfile(r276);
            r287.RoundCode = "R287";
            r287.BatchId = "R3-04AG-B012-R287";
            r287.Label = "r276-hard-compensated-pressure-d";
            r287.HardMilitaryCollapseThreshold = 15;
            r287.HardBias = 0.057f;
            r287.HardInitScale = 1.088f;
            r287.HardVictoryThreshold = 81.08f;
            r287.LateConflictTurnBonus = 0.071f;
            r287.LateLossChanceBonus = 0.122f;
            r287.LateCaptureChancePenalty = 0.039f;

            var r290 = CloneProfile(r287);
            r290.RoundCode = "R290";
            r290.BatchId = "R3-04AG-B013-R290";
            r290.Label = "r287-standard-recover-a";
            r290.StandardBias = 0.162f;
            profiles.Add(r290);

            var r291 = CloneProfile(r287);
            r291.RoundCode = "R291";
            r291.BatchId = "R3-04AG-B013-R291";
            r291.Label = "r287-standard-recover-b";
            r291.StandardBias = 0.163f;
            profiles.Add(r291);

            var r292 = CloneProfile(r287);
            r292.RoundCode = "R292";
            r292.BatchId = "R3-04AG-B013-R292";
            r292.Label = "r287-standard-recover-c";
            r292.StandardBias = 0.162f;
            r292.StandardInitScale = 1.112f;
            profiles.Add(r292);

            var r293 = CloneProfile(r287);
            r293.RoundCode = "R293";
            r293.BatchId = "R3-04AG-B013-R293";
            r293.Label = "r287-standard-recover-d";
            r293.StandardBias = 0.162f;
            r293.EasyBias = 0.271f;
            profiles.Add(r293);

            var r294 = CloneProfile(r287);
            r294.RoundCode = "R294";
            r294.BatchId = "R3-04AG-B013-R294";
            r294.Label = "r287-standard-recover-e";
            r294.StandardBias = 0.162f;
            r294.HardBias = 0.058f;
            profiles.Add(r294);

            var r295 = CloneProfile(r287);
            r295.RoundCode = "R295";
            r295.BatchId = "R3-04AG-B013-R295";
            r295.Label = "r287-standard-recover-f";
            r295.StandardBias = 0.163f;
            r295.HardBias = 0.058f;
            r295.HardInitScale = 1.090f;
            profiles.Add(r295);

            var r296 = CloneProfile(r293);
            r296.RoundCode = "R296";
            r296.BatchId = "R3-04AG-B014-R296";
            r296.Label = "r293-late-defeat-balance-a";
            r296.StandardVictoryThreshold = 83.15f;
            r296.HardVictoryThreshold = 81.20f;
            r296.LateLossChanceBonus = 0.124f;
            r296.LateCaptureChancePenalty = 0.040f;
            profiles.Add(r296);

            var r297 = CloneProfile(r293);
            r297.RoundCode = "R297";
            r297.BatchId = "R3-04AG-B014-R297";
            r297.Label = "r293-late-defeat-balance-b";
            r297.EasyBias = 0.272f;
            r297.StandardBias = 0.161f;
            r297.StandardVictoryThreshold = 83.20f;
            r297.HardVictoryThreshold = 81.22f;
            r297.LateLossChanceBonus = 0.125f;
            r297.LateCaptureChancePenalty = 0.040f;
            profiles.Add(r297);

            var r298 = CloneProfile(r293);
            r298.RoundCode = "R298";
            r298.BatchId = "R3-04AG-B014-R298";
            r298.Label = "r293-late-defeat-balance-c";
            r298.EasyBias = 0.272f;
            r298.StandardVictoryThreshold = 83.10f;
            r298.HardBias = 0.057f;
            r298.HardVictoryThreshold = 81.18f;
            r298.LateLossChanceBonus = 0.124f;
            r298.LateCaptureChancePenalty = 0.040f;
            profiles.Add(r298);

            var r299 = CloneProfile(r296);
            r299.RoundCode = "R299";
            r299.BatchId = "R3-04AG-B015-R299";
            r299.Label = "r296-timeout-to-defeat-a";
            r299.EasyBias = 0.272f;
            r299.StandardVictoryThreshold = 83.10f;
            r299.LateLossChanceBonus = 0.128f;
            profiles.Add(r299);

            var r300 = CloneProfile(r296);
            r300.RoundCode = "R300";
            r300.BatchId = "R3-04AG-B015-R300";
            r300.Label = "r296-timeout-to-defeat-b";
            r300.EasyBias = 0.272f;
            r300.StandardVictoryThreshold = 83.10f;
            r300.LateLossChanceBonus = 0.130f;
            r300.LateCaptureChancePenalty = 0.041f;
            profiles.Add(r300);

            var r301 = CloneProfile(r296);
            r301.RoundCode = "R301";
            r301.BatchId = "R3-04AG-B015-R301";
            r301.Label = "r296-timeout-to-defeat-c";
            r301.EasyBias = 0.272f;
            r301.StandardVictoryThreshold = 83.10f;
            r301.HardVictoryThreshold = 81.16f;
            r301.LateLossChanceBonus = 0.130f;
            r301.LateCaptureChancePenalty = 0.041f;
            profiles.Add(r301);

            var r302 = CloneProfile(r296);
            r302.RoundCode = "R302";
            r302.BatchId = "R3-04AG-B015-R302";
            r302.Label = "r296-timeout-to-defeat-d";
            r302.EasyBias = 0.272f;
            r302.StandardBias = 0.163f;
            r302.StandardVictoryThreshold = 83.10f;
            r302.LateLossChanceBonus = 0.129f;
            r302.LateCaptureChancePenalty = 0.041f;
            profiles.Add(r302);

            var r303 = CloneProfile(r296);
            r303.RoundCode = "R303";
            r303.BatchId = "R3-04AG-B016-R303";
            r303.Label = "r296-variance-compress-a";
            r303.EasyBias = 0.272f;
            r303.StandardBias = 0.163f;
            r303.ResourceNoiseScale = 1.95f;
            r303.LossChanceScale = 0.96f;
            profiles.Add(r303);

            var r304 = CloneProfile(r296);
            r304.RoundCode = "R304";
            r304.BatchId = "R3-04AG-B016-R304";
            r304.Label = "r296-variance-compress-b";
            r304.EasyBias = 0.272f;
            r304.StandardBias = 0.163f;
            r304.ResourceNoiseScale = 1.92f;
            r304.LossChanceScale = 0.97f;
            profiles.Add(r304);

            var r305 = CloneProfile(r296);
            r305.RoundCode = "R305";
            r305.BatchId = "R3-04AG-B016-R305";
            r305.Label = "r296-variance-compress-c";
            r305.EasyBias = 0.272f;
            r305.StandardBias = 0.163f;
            r305.ResourceNoiseScale = 1.95f;
            r305.LossChanceScale = 0.97f;
            r305.HardVictoryThreshold = 81.18f;
            profiles.Add(r305);

            var r306 = CloneProfile(r296);
            r306.RoundCode = "R306";
            r306.BatchId = "R3-04AG-B016-R306";
            r306.Label = "r296-variance-compress-d";
            r306.EasyBias = 0.273f;
            r306.StandardBias = 0.163f;
            r306.ResourceNoiseScale = 1.98f;
            r306.CaptureChanceScale = 2.75f;
            r306.LossChanceScale = 0.96f;
            profiles.Add(r306);

            var r307 = CloneProfile(r306);
            r307.RoundCode = "R307";
            r307.BatchId = "R3-04AG-B017-R307";
            r307.Label = "r306-standard-recover-hard-pressure-a";
            r307.StandardVictoryThreshold = 83.10f;
            r307.HardMilitaryCollapseThreshold = 16;
            r307.HardVictoryThreshold = 81.12f;
            r307.LossChanceScale = 0.98f;
            r307.LateLossChanceBonus = 0.126f;
            profiles.Add(r307);

            var r308 = CloneProfile(r306);
            r308.RoundCode = "R308";
            r308.BatchId = "R3-04AG-B017-R308";
            r308.Label = "r306-standard-recover-hard-pressure-b";
            r308.StandardVictoryThreshold = 83.10f;
            r308.HardMilitaryCollapseThreshold = 17;
            r308.HardVictoryThreshold = 81.05f;
            r308.LossChanceScale = 0.98f;
            r308.LateLossChanceBonus = 0.127f;
            profiles.Add(r308);

            var r309 = CloneProfile(r306);
            r309.RoundCode = "R309";
            r309.BatchId = "R3-04AG-B017-R309";
            r309.Label = "r306-standard-recover-hard-pressure-c";
            r309.StandardVictoryThreshold = 83.10f;
            r309.HardMilitaryCollapseThreshold = 16;
            r309.HardVictoryThreshold = 81.10f;
            r309.LossChanceScale = 0.97f;
            r309.LateLossChanceBonus = 0.126f;
            profiles.Add(r309);

            var r310 = CloneProfile(r306);
            r310.RoundCode = "R310";
            r310.BatchId = "R3-04AG-B017-R310";
            r310.Label = "r306-standard-recover-hard-pressure-d";
            r310.StandardVictoryThreshold = 83.10f;
            r310.HardMilitaryCollapseThreshold = 16;
            r310.HardVictoryThreshold = 81.12f;
            r310.ResourceNoiseScale = 2.00f;
            r310.LossChanceScale = 0.98f;
            r310.LateLossChanceBonus = 0.126f;
            profiles.Add(r310);

            var r311 = CloneProfile(r307);
            r311.RoundCode = "R311";
            r311.BatchId = "R3-04AG-B018-R311";
            r311.Label = "r307-defeat-floor-lift-a";
            r311.EasyBias = 0.275f;
            r311.LossChanceScale = 1.00f;
            r311.LateLossChanceBonus = 0.130f;
            profiles.Add(r311);

            var r312 = CloneProfile(r307);
            r312.RoundCode = "R312";
            r312.BatchId = "R3-04AG-B018-R312";
            r312.Label = "r307-defeat-floor-lift-b";
            r312.EasyBias = 0.276f;
            r312.EasyInitScale = 1.16f;
            r312.LossChanceScale = 1.01f;
            r312.LateLossChanceBonus = 0.131f;
            profiles.Add(r312);

            var r313 = CloneProfile(r307);
            r313.RoundCode = "R313";
            r313.BatchId = "R3-04AG-B018-R313";
            r313.Label = "r307-defeat-floor-lift-c";
            r313.EasyBias = 0.276f;
            r313.StandardBias = 0.164f;
            r313.LossChanceScale = 1.00f;
            r313.LateLossChanceBonus = 0.132f;
            profiles.Add(r313);

            var r314 = CloneProfile(r307);
            r314.RoundCode = "R314";
            r314.BatchId = "R3-04AG-B018-R314";
            r314.Label = "r307-defeat-floor-lift-d";
            r314.EasyBias = 0.278f;
            r314.EasyInitScale = 1.16f;
            r314.LossChanceScale = 1.02f;
            r314.LateLossChanceBonus = 0.132f;
            profiles.Add(r314);

            var r315 = CloneProfile(r311);
            r315.RoundCode = "R315";
            r315.BatchId = "R3-04AG-B019-R315";
            r315.Label = "r311-node-loss-convert-a";
            r315.EasyBias = 0.274f;
            r315.CaptureChanceScale = 2.70f;
            r315.LossChanceScale = 1.05f;
            profiles.Add(r315);

            var r316 = CloneProfile(r311);
            r316.RoundCode = "R316";
            r316.BatchId = "R3-04AG-B019-R316";
            r316.Label = "r311-node-loss-convert-b";
            r316.EasyBias = 0.276f;
            r316.CaptureChanceScale = 2.68f;
            r316.LossChanceScale = 1.08f;
            profiles.Add(r316);

            var r317 = CloneProfile(r311);
            r317.RoundCode = "R317";
            r317.BatchId = "R3-04AG-B019-R317";
            r317.Label = "r311-node-loss-convert-c";
            r317.EasyBias = 0.275f;
            r317.StandardBias = 0.162f;
            r317.HardBias = 0.057f;
            r317.CaptureChanceScale = 2.70f;
            r317.LossChanceScale = 1.06f;
            profiles.Add(r317);

            var r318 = CloneProfile(r311);
            r318.RoundCode = "R318";
            r318.BatchId = "R3-04AG-B019-R318";
            r318.Label = "r311-node-loss-convert-d";
            r318.EasyBias = 0.275f;
            r318.CaptureChanceScale = 2.72f;
            r318.LossChanceScale = 1.04f;
            r318.LateLossChanceBonus = 0.132f;
            profiles.Add(r318);

            var r319 = CloneProfile(r318);
            r319.RoundCode = "R319";
            r319.BatchId = "R3-04AG-B020-R319";
            r319.Label = "r318-late-convert-timeout-a";
            r319.StandardVictoryThreshold = 83.05f;
            r319.LatePressureStartTurn = 13;
            r319.LateLossChanceBonus = 0.145f;
            r319.LateCaptureChancePenalty = 0.043f;
            profiles.Add(r319);

            var r320 = CloneProfile(r318);
            r320.RoundCode = "R320";
            r320.BatchId = "R3-04AG-B020-R320";
            r320.Label = "r318-late-convert-timeout-b";
            r320.StandardVictoryThreshold = 83.00f;
            r320.LatePressureStartTurn = 12;
            r320.LateLossChanceBonus = 0.155f;
            r320.LateCaptureChancePenalty = 0.045f;
            profiles.Add(r320);

            var r321 = CloneProfile(r318);
            r321.RoundCode = "R321";
            r321.BatchId = "R3-04AG-B020-R321";
            r321.Label = "r318-late-convert-timeout-c";
            r321.StandardVictoryThreshold = 83.05f;
            r321.LatePressureStartTurn = 13;
            r321.LatePressureTurnBonus = 0.100f;
            r321.LateConflictTurnBonus = 0.074f;
            r321.LateLossChanceBonus = 0.150f;
            r321.LateCaptureChancePenalty = 0.044f;
            profiles.Add(r321);

            var r322 = CloneProfile(r318);
            r322.RoundCode = "R322";
            r322.BatchId = "R3-04AG-B020-R322";
            r322.Label = "r318-late-convert-timeout-d";
            r322.StandardVictoryThreshold = 83.08f;
            r322.LatePressureStartTurn = 12;
            r322.LateLossChanceBonus = 0.160f;
            r322.LateCaptureChancePenalty = 0.045f;
            profiles.Add(r322);

            // Auto-grid search candidates centered on the current near-pass family (R311).
            float[] easyBiasGrid = { 0.274f, 0.276f };
            float[] standardThresholdGrid = { 83.05f, 83.10f };
            int[] hardCollapseGrid = { 15, 16 };
            float[] lossScaleGrid = { 1.00f, 1.04f };
            float[] lateLossGrid = { 0.126f, 0.132f };
            float[] captureScaleGrid = { 2.70f, 2.72f };

            int autoRoundCode = 330;
            foreach (float easyBias in easyBiasGrid)
            {
                foreach (float standardThreshold in standardThresholdGrid)
                {
                    foreach (int hardCollapse in hardCollapseGrid)
                    {
                        foreach (float lossScale in lossScaleGrid)
                        {
                            foreach (float lateLoss in lateLossGrid)
                            {
                                foreach (float captureScale in captureScaleGrid)
                                {
                                    var auto = CloneProfile(r311);
                                    auto.RoundCode = $"R{autoRoundCode}";
                                    auto.BatchId = $"R3-04AG-B021-{auto.RoundCode}";
                                    auto.Label = "auto-grid-r311";
                                    auto.EasyBias = easyBias;
                                    auto.StandardBias = 0.163f;
                                    auto.StandardVictoryThreshold = standardThreshold;
                                    auto.HardMilitaryCollapseThreshold = hardCollapse;
                                    auto.HardVictoryThreshold = 81.12f;
                                    auto.CaptureChanceScale = captureScale;
                                    auto.LossChanceScale = lossScale;
                                    auto.LateLossChanceBonus = lateLoss;
                                    auto.LateCaptureChancePenalty = 0.043f;
                                    profiles.Add(auto);
                                    autoRoundCode++;
                                }
                            }
                        }
                    }
                }
            }

            return profiles;
        }

        private static TuningProfile ResolveR305BaseProfile()
        {
            var allProfiles = new List<TuningProfile>();
            allProfiles.AddRange(BuildR304AGProfiles());
            allProfiles.AddRange(BuildR304AFProfiles());
            allProfiles.AddRange(BuildR304AEProfiles());
            allProfiles.AddRange(BuildR304ADProfiles());
            allProfiles.AddRange(BuildR304ACProfiles());
            allProfiles.AddRange(BuildR304ABProfiles());
            allProfiles.AddRange(BuildR304AAProfiles());
            allProfiles.AddRange(BuildR304ZProfiles());
            allProfiles.AddRange(BuildR304YProfiles());
            allProfiles.AddRange(BuildR304XProfiles());
            allProfiles.AddRange(BuildR304WProfiles());
            allProfiles.AddRange(BuildR304VProfiles());
            allProfiles.AddRange(BuildR304UProfiles());
            allProfiles.AddRange(BuildR304TProfiles());
            allProfiles.AddRange(BuildR304SProfiles());
            allProfiles.AddRange(BuildR304RProfiles());
            allProfiles.AddRange(BuildR304QProfiles());
            allProfiles.AddRange(BuildR304PProfiles());
            allProfiles.AddRange(BuildR304OProfiles());
            allProfiles.AddRange(BuildR304NProfiles());
            allProfiles.AddRange(BuildR304MProfiles());
            allProfiles.AddRange(BuildR304LProfiles());
            allProfiles.AddRange(BuildR304KProfiles());
            allProfiles.AddRange(BuildR304JProfiles());
            allProfiles.AddRange(BuildR304IProfiles());
            allProfiles.AddRange(BuildR304HProfiles());
            allProfiles.AddRange(BuildR304GProfiles());
            allProfiles.AddRange(BuildR304FProfiles());
            allProfiles.AddRange(BuildR304EProfiles());
            allProfiles.AddRange(BuildR304DProfiles());
            allProfiles.AddRange(BuildR304CProfiles());
            allProfiles.AddRange(BuildR304TargetedProfiles());
            allProfiles.AddRange(BuildR304Profiles());

            string overrideRoundCode = Environment.GetEnvironmentVariable("EVENTIDE_R305_BASE_ROUND");
            if (!string.IsNullOrWhiteSpace(overrideRoundCode))
            {
                string trimmedCode = overrideRoundCode.Trim();
                var overriddenProfile = allProfiles.FirstOrDefault(p => string.Equals(p.RoundCode, trimmedCode, StringComparison.OrdinalIgnoreCase));
                if (overriddenProfile != null)
                {
                    Debug.Log($"[R3Validation] Using override base round from EVENTIDE_R305_BASE_ROUND={trimmedCode}.");
                    return overriddenProfile;
                }

                Debug.LogWarning($"[R3Validation] EVENTIDE_R305_BASE_ROUND={trimmedCode} not found; fallback to preferred order.");
            }

            string[] preferredRoundCodes = { "R311", "R307", "R318", "R306", "R293", "R291", "R294", "R290", "R292", "R295", "R287", "R281", "R278", "R283", "R276", "R279", "R282", "R280", "R274", "R273", "R272", "R277", "R275", "R262", "R260", "R261", "R263", "R264", "R265", "R201", "R250", "R252", "R254", "R253", "R251", "R255", "R247", "R246", "R245", "R244", "R249", "R248", "R241", "R238", "R243", "R242", "R239", "R240", "R222", "R224", "R221", "R223", "R220", "R225", "R211", "R212", "R213", "R208", "R209", "R210", "R205", "R206", "R207", "R204", "R203", "R202", "R200", "R199", "R198", "R197", "R196", "R195", "R194", "R193", "R192", "R191", "R190", "R189", "R188", "R187", "R186", "R185", "R184", "R183", "R182", "R181", "R180", "R179", "R178", "R177", "R176", "R175", "R174", "R173", "R172", "R171", "R170", "R169", "R168", "R167", "R166", "R165", "R164", "R163", "R162", "R161", "R160", "R159", "R158", "R157", "R156", "R155", "R154", "R153", "R152", "R151", "R150", "R149", "R148", "R147", "R146", "R145", "R144", "R143", "R142", "R141", "R140", "R139", "R138", "R137", "R136", "R135", "R134", "R133", "R132", "R131", "R130", "R129", "R128", "R127", "R126", "R125", "R124", "R123", "R122", "R121", "R120", "R119", "R118", "R117", "R116", "R115", "R114", "R113", "R112", "R111", "R110", "R109", "R99", "R98", "R97", "R84", "R83", "R82", "R81", "R80", "R79", "R78", "R77", "R76", "R75", "R74", "R73", "R70", "R69", "R67", "R64", "R61", "R58", "R45", "R44", "R43", "R42", "R41", "R40", "R39", "R38", "R37", "R36", "R35", "R34", "R33", "R32", "R31", "R30", "R29", "R28", "R27", "R26", "R25", "R24", "R23", "R22", "R21", "R20", "R19", "R18", "R17", "R16", "R15", "R14", "R13", "R12", "R11", "R10", "R09", "R08", "R07", "R06", "R05", "R04", "R03", "R02", "R01" };
            foreach (string code in preferredRoundCodes)
            {
                var profile = allProfiles.FirstOrDefault(p => p.RoundCode == code);
                if (profile != null)
                {
                    return profile;
                }
            }

            return allProfiles.FirstOrDefault();
        }

        private static string Csv(string value)
        {
            string safe = value ?? string.Empty;
            return $"\"{safe.Replace("\"", "\"\"")}\"";
        }
    }
}
