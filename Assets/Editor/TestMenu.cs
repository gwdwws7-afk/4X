#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace EventideAge.Editor
{
    public static class TestMenu
    {
        [MenuItem("EventideAge/Run All Tests")]
        public static void RunAllTests()
        {
            Debug.Log("=== EventideAge: Run All Tests ===");
            EventideAge.Tests.StandaloneTest.RunAll();
        }

        [MenuItem("EventideAge/Run R1 Replay Scenarios")]
        public static void RunR1ReplayScenarios()
        {
            Debug.Log("=== EventideAge: Run R1 Replay Scenarios ===");
            EventideAge.Tests.StandaloneTest.RunR1ReplayScenarios();
        }

        [MenuItem("EventideAge/Run R1 Cross-Turn Consistency Checks")]
        public static void RunR1CrossTurnConsistencyChecks()
        {
            Debug.Log("=== EventideAge: Run R1 Cross-Turn Consistency Checks ===");
            EventideAge.Tests.StandaloneTest.RunR1CrossTurnConsistencyChecks();
        }

        [MenuItem("EventideAge/Run R1 Save Compatibility Checks")]
        public static void RunR1SaveCompatibilityChecks()
        {
            Debug.Log("=== EventideAge: Run R1 Save Compatibility Checks ===");
            EventideAge.Tests.StandaloneTest.RunR1SaveCompatibilityChecks();
        }

        [MenuItem("EventideAge/Run R2 Event Pool Checks")]
        public static void RunR2EventPoolChecks()
        {
            Debug.Log("=== EventideAge: Run R2 Event Pool Checks ===");
            EventideAge.Tests.StandaloneTest.RunR2EventPoolChecks();
        }

        [MenuItem("EventideAge/Run R2 Map Config V1 Lock Checks")]
        public static void RunR2MapConfigV1LockChecks()
        {
            Debug.Log("=== EventideAge: Run R2 Map Config V1 Lock Checks ===");
            EventideAge.Tests.StandaloneTest.RunR2MapConfigV1LockChecks();
        }

        [MenuItem("EventideAge/Run R2 AI Strategy Checks")]
        public static void RunR2AIStrategyChecks()
        {
            Debug.Log("=== EventideAge: Run R2 AI Strategy Checks ===");
            EventideAge.Tests.StandaloneTest.RunR2AIStrategyChecks();
        }

        [MenuItem("EventideAge/Run R2 Tutorial Flow Checks")]
        public static void RunR2TutorialFlowChecks()
        {
            Debug.Log("=== EventideAge: Run R2 Tutorial Flow Checks ===");
            EventideAge.Tests.StandaloneTest.RunR2TutorialFlowChecks();
        }

        [MenuItem("EventideAge/Run R2 Campaign Completion Checks")]
        public static void RunR2CampaignCompletionChecks()
        {
            Debug.Log("=== EventideAge: Run R2 Campaign Completion Checks ===");
            EventideAge.Tests.StandaloneTest.RunR2CampaignCompletionChecks();
        }

        [MenuItem("EventideAge/Run R3 Simulation Batch (1000)")]
        public static void RunR3SimulationBatch1000()
        {
            Debug.Log("=== EventideAge: Run R3 Simulation Batch (1000) ===");
            EventideAge.Tests.R3SimulationRunner.RunBatch1000();
        }

        [MenuItem("EventideAge/Run R3 AI Difficulty Checks")]
        public static void RunR3AIDifficultyChecks()
        {
            Debug.Log("=== EventideAge: Run R3 AI Difficulty Checks ===");
            EventideAge.Tests.StandaloneTest.RunR3AIDifficultyChecks();
        }

        [MenuItem("EventideAge/Run R3 Tuning Iterations (R3-04)")]
        public static void RunR3TuningIterations()
        {
            Debug.Log("=== EventideAge: Run R3 Tuning Iterations (R3-04) ===");
            EventideAge.Tests.R3TuningRunner.RunR304Iterations();
        }

        [MenuItem("EventideAge/Run R3 Targeted Retuning (R3-04B)")]
        public static void RunR3TargetedRetuning()
        {
            Debug.Log("=== EventideAge: Run R3 Targeted Retuning (R3-04B) ===");
            EventideAge.Tests.R3TuningRunner.RunR304TargetedRetuning();
        }

        [MenuItem("EventideAge/Run R3 Targeted Retuning (R3-04C)")]
        public static void RunR3TargetedRetuningC()
        {
            Debug.Log("=== EventideAge: Run R3 Targeted Retuning (R3-04C) ===");
            EventideAge.Tests.R3TuningRunner.RunR304CRetuning();
        }

        [MenuItem("EventideAge/Run R3 Targeted Retuning (R3-04D)")]
        public static void RunR3TargetedRetuningD()
        {
            Debug.Log("=== EventideAge: Run R3 Targeted Retuning (R3-04D) ===");
            EventideAge.Tests.R3TuningRunner.RunR304DRetuning();
        }

        [MenuItem("EventideAge/Run R3 Targeted Retuning (R3-04E)")]
        public static void RunR3TargetedRetuningE()
        {
            Debug.Log("=== EventideAge: Run R3 Targeted Retuning (R3-04E) ===");
            EventideAge.Tests.R3TuningRunner.RunR304ERetuning();
        }

        [MenuItem("EventideAge/Run R3 Targeted Retuning (R3-04F)")]
        public static void RunR3TargetedRetuningF()
        {
            Debug.Log("=== EventideAge: Run R3 Targeted Retuning (R3-04F) ===");
            EventideAge.Tests.R3TuningRunner.RunR304FRetuning();
        }

        [MenuItem("EventideAge/Run R3 Targeted Retuning (R3-04G)")]
        public static void RunR3TargetedRetuningG()
        {
            Debug.Log("=== EventideAge: Run R3 Targeted Retuning (R3-04G) ===");
            EventideAge.Tests.R3TuningRunner.RunR304GRetuning();
        }

        [MenuItem("EventideAge/Run R3 Targeted Retuning (R3-04H)")]
        public static void RunR3TargetedRetuningH()
        {
            Debug.Log("=== EventideAge: Run R3 Targeted Retuning (R3-04H) ===");
            EventideAge.Tests.R3TuningRunner.RunR304HRetuning();
        }

        [MenuItem("EventideAge/Run R3 Targeted Retuning (R3-04I)")]
        public static void RunR3TargetedRetuningI()
        {
            Debug.Log("=== EventideAge: Run R3 Targeted Retuning (R3-04I) ===");
            EventideAge.Tests.R3TuningRunner.RunR304IRetuning();
        }

        [MenuItem("EventideAge/Run R3 Targeted Retuning (R3-04J)")]
        public static void RunR3TargetedRetuningJ()
        {
            Debug.Log("=== EventideAge: Run R3 Targeted Retuning (R3-04J) ===");
            EventideAge.Tests.R3TuningRunner.RunR304JRetuning();
        }

        [MenuItem("EventideAge/Run R3 Targeted Retuning (R3-04K)")]
        public static void RunR3TargetedRetuningK()
        {
            Debug.Log("=== EventideAge: Run R3 Targeted Retuning (R3-04K) ===");
            EventideAge.Tests.R3TuningRunner.RunR304KRetuning();
        }

        [MenuItem("EventideAge/Run R3 Targeted Retuning (R3-04L)")]
        public static void RunR3TargetedRetuningL()
        {
            Debug.Log("=== EventideAge: Run R3 Targeted Retuning (R3-04L) ===");
            EventideAge.Tests.R3TuningRunner.RunR304LRetuning();
        }

        [MenuItem("EventideAge/Run R3 Targeted Retuning (R3-04M)")]
        public static void RunR3TargetedRetuningM()
        {
            Debug.Log("=== EventideAge: Run R3 Targeted Retuning (R3-04M) ===");
            EventideAge.Tests.R3TuningRunner.RunR304MRetuning();
        }

        [MenuItem("EventideAge/Run R3 Targeted Retuning (R3-04N)")]
        public static void RunR3TargetedRetuningN()
        {
            Debug.Log("=== EventideAge: Run R3 Targeted Retuning (R3-04N) ===");
            EventideAge.Tests.R3TuningRunner.RunR304NRetuning();
        }

        [MenuItem("EventideAge/Run R3 Targeted Retuning (R3-04O)")]
        public static void RunR3TargetedRetuningO()
        {
            Debug.Log("=== EventideAge: Run R3 Targeted Retuning (R3-04O) ===");
            EventideAge.Tests.R3TuningRunner.RunR304ORetuning();
        }

        [MenuItem("EventideAge/Run R3 Targeted Retuning (R3-04P)")]
        public static void RunR3TargetedRetuningP()
        {
            Debug.Log("=== EventideAge: Run R3 Targeted Retuning (R3-04P) ===");
            EventideAge.Tests.R3TuningRunner.RunR304PRetuning();
        }

        [MenuItem("EventideAge/Run R3 Targeted Retuning (R3-04Q)")]
        public static void RunR3TargetedRetuningQ()
        {
            Debug.Log("=== EventideAge: Run R3 Targeted Retuning (R3-04Q) ===");
            EventideAge.Tests.R3TuningRunner.RunR304QRetuning();
        }

        [MenuItem("EventideAge/Run R3 Targeted Retuning (R3-04R)")]
        public static void RunR3TargetedRetuningR()
        {
            Debug.Log("=== EventideAge: Run R3 Targeted Retuning (R3-04R) ===");
            EventideAge.Tests.R3TuningRunner.RunR304RRetuning();
        }

        [MenuItem("EventideAge/Run R3 Targeted Retuning (R3-04S)")]
        public static void RunR3TargetedRetuningS()
        {
            Debug.Log("=== EventideAge: Run R3 Targeted Retuning (R3-04S) ===");
            EventideAge.Tests.R3TuningRunner.RunR304SRetuning();
        }

        [MenuItem("EventideAge/Run R3 Targeted Retuning (R3-04T)")]
        public static void RunR3TargetedRetuningT()
        {
            Debug.Log("=== EventideAge: Run R3 Targeted Retuning (R3-04T) ===");
            EventideAge.Tests.R3TuningRunner.RunR304TRetuning();
        }

        [MenuItem("EventideAge/Run R3 Targeted Retuning (R3-04U)")]
        public static void RunR3TargetedRetuningU()
        {
            Debug.Log("=== EventideAge: Run R3 Targeted Retuning (R3-04U) ===");
            EventideAge.Tests.R3TuningRunner.RunR304URetuning();
        }

        [MenuItem("EventideAge/Run R3 Targeted Retuning (R3-04V)")]
        public static void RunR3TargetedRetuningV()
        {
            Debug.Log("=== EventideAge: Run R3 Targeted Retuning (R3-04V) ===");
            EventideAge.Tests.R3TuningRunner.RunR304VRetuning();
        }

        [MenuItem("EventideAge/Run R3 Targeted Retuning (R3-04W)")]
        public static void RunR3TargetedRetuningW()
        {
            Debug.Log("=== EventideAge: Run R3 Targeted Retuning (R3-04W) ===");
            EventideAge.Tests.R3TuningRunner.RunR304WRetuning();
        }

        [MenuItem("EventideAge/Run R3 Targeted Retuning (R3-04X)")]
        public static void RunR3TargetedRetuningX()
        {
            Debug.Log("=== EventideAge: Run R3 Targeted Retuning (R3-04X) ===");
            EventideAge.Tests.R3TuningRunner.RunR304XRetuning();
        }

        [MenuItem("EventideAge/Run R3 Targeted Retuning (R3-04Y)")]
        public static void RunR3TargetedRetuningY()
        {
            Debug.Log("=== EventideAge: Run R3 Targeted Retuning (R3-04Y) ===");
            EventideAge.Tests.R3TuningRunner.RunR304YRetuning();
        }

        [MenuItem("EventideAge/Run R3 Targeted Retuning (R3-04Z)")]
        public static void RunR3TargetedRetuningZ()
        {
            Debug.Log("=== EventideAge: Run R3 Targeted Retuning (R3-04Z) ===");
            EventideAge.Tests.R3TuningRunner.RunR304ZRetuning();
        }

        [MenuItem("EventideAge/Run R3 Targeted Retuning (R3-04AA)")]
        public static void RunR3TargetedRetuningAA()
        {
            Debug.Log("=== EventideAge: Run R3 Targeted Retuning (R3-04AA) ===");
            EventideAge.Tests.R3TuningRunner.RunR304AARetuning();
        }

        [MenuItem("EventideAge/Run R3 Targeted Retuning (R3-04AB)")]
        public static void RunR3TargetedRetuningAB()
        {
            Debug.Log("=== EventideAge: Run R3 Targeted Retuning (R3-04AB) ===");
            EventideAge.Tests.R3TuningRunner.RunR304ABRetuning();
        }

        [MenuItem("EventideAge/Run R3 Targeted Retuning (R3-04AC)")]
        public static void RunR3TargetedRetuningAC()
        {
            Debug.Log("=== EventideAge: Run R3 Targeted Retuning (R3-04AC) ===");
            EventideAge.Tests.R3TuningRunner.RunR304ACRetuning();
        }

        [MenuItem("EventideAge/Run R3 Targeted Retuning (R3-04AD)")]
        public static void RunR3TargetedRetuningAD()
        {
            Debug.Log("=== EventideAge: Run R3 Targeted Retuning (R3-04AD) ===");
            EventideAge.Tests.R3TuningRunner.RunR304ADRetuning();
        }

        [MenuItem("EventideAge/Run R3 Targeted Retuning (R3-04AE)")]
        public static void RunR3TargetedRetuningAE()
        {
            Debug.Log("=== EventideAge: Run R3 Targeted Retuning (R3-04AE) ===");
            EventideAge.Tests.R3TuningRunner.RunR304AERetuning();
        }

        [MenuItem("EventideAge/Run R3 Targeted Retuning (R3-04AF)")]
        public static void RunR3TargetedRetuningAF()
        {
            Debug.Log("=== EventideAge: Run R3 Targeted Retuning (R3-04AF) ===");
            EventideAge.Tests.R3TuningRunner.RunR304AFRetuning();
        }

        [MenuItem("EventideAge/Run R3 Targeted Retuning (R3-04AG)")]
        public static void RunR3TargetedRetuningAG()
        {
            Debug.Log("=== EventideAge: Run R3 Targeted Retuning (R3-04AG) ===");
            EventideAge.Tests.R3TuningRunner.RunR304AGRetuning();
        }

        [MenuItem("EventideAge/Run R3 Validation (R3-05)")]
        public static void RunR3Validation()
        {
            Debug.Log("=== EventideAge: Run R3 Validation (R3-05) ===");
            EventideAge.Tests.R3TuningRunner.RunR305Validation();
        }

        [MenuItem("EventideAge/Run R4 UI Productization Checks (R4-02)")]
        public static void RunR4UiProductizationChecks()
        {
            Debug.Log("=== EventideAge: Run R4 UI Productization Checks (R4-02) ===");
            EventideAge.Tests.StandaloneTest.RunR4UiProductizationChecks();
        }

        [MenuItem("EventideAge/Run R4 UI Interaction Hint Checks (R4-03)")]
        public static void RunR4UiInteractionHintChecks()
        {
            Debug.Log("=== EventideAge: Run R4 UI Interaction Hint Checks (R4-03) ===");
            EventideAge.Tests.StandaloneTest.RunR4UiInteractionHintChecks();
        }

        [MenuItem("EventideAge/Run R4 UX Visual Audit (R4-06)")]
        public static void RunR4UxVisualAudit()
        {
            Debug.Log("=== EventideAge: Run R4 UX Visual Audit (R4-06) ===");
            R4UxVisualAuditRunner.RunR406VisualAudit();
        }

        [MenuItem("EventideAge/Run R5 Performance Baseline (R5-01)")]
        public static void RunR5PerformanceBaseline()
        {
            Debug.Log("=== EventideAge: Run R5 Performance Baseline (R5-01) ===");
            EventideAge.Tests.R5StabilityRunner.RunPerformanceBaseline();
        }

        [MenuItem("EventideAge/Run R5 Soak Baseline (R5-04)")]
        public static void RunR5SoakBaseline()
        {
            Debug.Log("=== EventideAge: Run R5 Soak Baseline (R5-04) ===");
            EventideAge.Tests.R5StabilityRunner.RunSoakBaseline();
        }

        [MenuItem("EventideAge/Run L Meta Checks (L1-L4)")]
        public static void RunLMetaChecks()
        {
            Debug.Log("=== EventideAge: Run L Meta Checks (L1-L4) ===");
            EventideAge.Tests.StandaloneTest.RunLMetaChecks();
        }
    }
}
#endif
