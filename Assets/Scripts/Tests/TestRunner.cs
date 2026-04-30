using UnityEngine;
using EventideAge.Tests;

namespace EventideAge.Tests
{
    public class TestRunner : MonoBehaviour
    {
        [ContextMenu("Run All Tests")]
        public void RunTests()
        {
            StandaloneTest.RunAll();
        }
        
        [ContextMenu("Run GameConfig Tests")]
        public void RunGameConfigTests()
        {
            Debug.Log("=== GameConfig Tests ===");
            StandaloneTest.RunAll();
        }

        [ContextMenu("Run R1 Replay Scenarios (RL-01/02/03/10)")]
        public void RunR1ReplayScenarios()
        {
            StandaloneTest.RunR1ReplayScenarios();
        }

        [ContextMenu("Run R1 Cross-Turn Consistency Checks (R1-04)")]
        public void RunR1CrossTurnConsistencyChecks()
        {
            StandaloneTest.RunR1CrossTurnConsistencyChecks();
        }

        [ContextMenu("Run R1 Save Compatibility Checks (R1-05)")]
        public void RunR1SaveCompatibilityChecks()
        {
            StandaloneTest.RunR1SaveCompatibilityChecks();
        }

        [ContextMenu("Run R2 Event Pool Checks (R2-02)")]
        public void RunR2EventPoolChecks()
        {
            StandaloneTest.RunR2EventPoolChecks();
        }

        [ContextMenu("Run R2 Map Config V1 Lock Checks (R2-03)")]
        public void RunR2MapConfigV1LockChecks()
        {
            StandaloneTest.RunR2MapConfigV1LockChecks();
        }

        [ContextMenu("Run R2 AI Strategy Checks (R2-04)")]
        public void RunR2AIStrategyChecks()
        {
            StandaloneTest.RunR2AIStrategyChecks();
        }

        [ContextMenu("Run R2 Tutorial Flow Checks (R2-05)")]
        public void RunR2TutorialFlowChecks()
        {
            StandaloneTest.RunR2TutorialFlowChecks();
        }

        [ContextMenu("Run R2 Campaign Completion Checks (R2-06)")]
        public void RunR2CampaignCompletionChecks()
        {
            StandaloneTest.RunR2CampaignCompletionChecks();
        }

        [ContextMenu("Run R3 Simulation Batch (R3-02, 1000)")]
        public void RunR3SimulationBatch()
        {
            R3SimulationRunner.RunBatch1000();
        }

        [ContextMenu("Run R3 AI Difficulty Checks (R3-03)")]
        public void RunR3AIDifficultyChecks()
        {
            StandaloneTest.RunR3AIDifficultyChecks();
        }

        [ContextMenu("Run R3 Tuning Iterations (R3-04)")]
        public void RunR3TuningIterations()
        {
            R3TuningRunner.RunR304Iterations();
        }

        [ContextMenu("Run R3 Targeted Retuning (R3-04B)")]
        public void RunR3TargetedRetuning()
        {
            R3TuningRunner.RunR304TargetedRetuning();
        }

        [ContextMenu("Run R3 Targeted Retuning (R3-04C)")]
        public void RunR3TargetedRetuningC()
        {
            R3TuningRunner.RunR304CRetuning();
        }

        [ContextMenu("Run R3 Targeted Retuning (R3-04D)")]
        public void RunR3TargetedRetuningD()
        {
            R3TuningRunner.RunR304DRetuning();
        }

        [ContextMenu("Run R3 Targeted Retuning (R3-04E)")]
        public void RunR3TargetedRetuningE()
        {
            R3TuningRunner.RunR304ERetuning();
        }

        [ContextMenu("Run R3 Targeted Retuning (R3-04F)")]
        public void RunR3TargetedRetuningF()
        {
            R3TuningRunner.RunR304FRetuning();
        }

        [ContextMenu("Run R3 Targeted Retuning (R3-04G)")]
        public void RunR3TargetedRetuningG()
        {
            R3TuningRunner.RunR304GRetuning();
        }

        [ContextMenu("Run R3 Targeted Retuning (R3-04H)")]
        public void RunR3TargetedRetuningH()
        {
            R3TuningRunner.RunR304HRetuning();
        }

        [ContextMenu("Run R3 Targeted Retuning (R3-04I)")]
        public void RunR3TargetedRetuningI()
        {
            R3TuningRunner.RunR304IRetuning();
        }

        [ContextMenu("Run R3 Targeted Retuning (R3-04J)")]
        public void RunR3TargetedRetuningJ()
        {
            R3TuningRunner.RunR304JRetuning();
        }

        [ContextMenu("Run R3 Targeted Retuning (R3-04K)")]
        public void RunR3TargetedRetuningK()
        {
            R3TuningRunner.RunR304KRetuning();
        }

        [ContextMenu("Run R3 Targeted Retuning (R3-04L)")]
        public void RunR3TargetedRetuningL()
        {
            R3TuningRunner.RunR304LRetuning();
        }

        [ContextMenu("Run R3 Targeted Retuning (R3-04M)")]
        public void RunR3TargetedRetuningM()
        {
            R3TuningRunner.RunR304MRetuning();
        }

        [ContextMenu("Run R3 Targeted Retuning (R3-04N)")]
        public void RunR3TargetedRetuningN()
        {
            R3TuningRunner.RunR304NRetuning();
        }

        [ContextMenu("Run R3 Targeted Retuning (R3-04O)")]
        public void RunR3TargetedRetuningO()
        {
            R3TuningRunner.RunR304ORetuning();
        }

        [ContextMenu("Run R3 Targeted Retuning (R3-04P)")]
        public void RunR3TargetedRetuningP()
        {
            R3TuningRunner.RunR304PRetuning();
        }

        [ContextMenu("Run R3 Targeted Retuning (R3-04Q)")]
        public void RunR3TargetedRetuningQ()
        {
            R3TuningRunner.RunR304QRetuning();
        }

        [ContextMenu("Run R3 Targeted Retuning (R3-04R)")]
        public void RunR3TargetedRetuningR()
        {
            R3TuningRunner.RunR304RRetuning();
        }

        [ContextMenu("Run R3 Targeted Retuning (R3-04S)")]
        public void RunR3TargetedRetuningS()
        {
            R3TuningRunner.RunR304SRetuning();
        }

        [ContextMenu("Run R3 Targeted Retuning (R3-04T)")]
        public void RunR3TargetedRetuningT()
        {
            R3TuningRunner.RunR304TRetuning();
        }

        [ContextMenu("Run R3 Targeted Retuning (R3-04U)")]
        public void RunR3TargetedRetuningU()
        {
            R3TuningRunner.RunR304URetuning();
        }

        [ContextMenu("Run R3 Targeted Retuning (R3-04V)")]
        public void RunR3TargetedRetuningV()
        {
            R3TuningRunner.RunR304VRetuning();
        }

        [ContextMenu("Run R3 Targeted Retuning (R3-04W)")]
        public void RunR3TargetedRetuningW()
        {
            R3TuningRunner.RunR304WRetuning();
        }

        [ContextMenu("Run R3 Targeted Retuning (R3-04X)")]
        public void RunR3TargetedRetuningX()
        {
            R3TuningRunner.RunR304XRetuning();
        }

        [ContextMenu("Run R3 Targeted Retuning (R3-04Y)")]
        public void RunR3TargetedRetuningY()
        {
            R3TuningRunner.RunR304YRetuning();
        }

        [ContextMenu("Run R3 Targeted Retuning (R3-04Z)")]
        public void RunR3TargetedRetuningZ()
        {
            R3TuningRunner.RunR304ZRetuning();
        }

        [ContextMenu("Run R3 Targeted Retuning (R3-04AA)")]
        public void RunR3TargetedRetuningAA()
        {
            R3TuningRunner.RunR304AARetuning();
        }

        [ContextMenu("Run R3 Targeted Retuning (R3-04AB)")]
        public void RunR3TargetedRetuningAB()
        {
            R3TuningRunner.RunR304ABRetuning();
        }

        [ContextMenu("Run R3 Targeted Retuning (R3-04AC)")]
        public void RunR3TargetedRetuningAC()
        {
            R3TuningRunner.RunR304ACRetuning();
        }

        [ContextMenu("Run R3 Targeted Retuning (R3-04AD)")]
        public void RunR3TargetedRetuningAD()
        {
            R3TuningRunner.RunR304ADRetuning();
        }

        [ContextMenu("Run R3 Targeted Retuning (R3-04AE)")]
        public void RunR3TargetedRetuningAE()
        {
            R3TuningRunner.RunR304AERetuning();
        }

        [ContextMenu("Run R3 Targeted Retuning (R3-04AF)")]
        public void RunR3TargetedRetuningAF()
        {
            R3TuningRunner.RunR304AFRetuning();
        }

        [ContextMenu("Run R3 Targeted Retuning (R3-04AG)")]
        public void RunR3TargetedRetuningAG()
        {
            R3TuningRunner.RunR304AGRetuning();
        }

        [ContextMenu("Run R3 Validation (R3-05)")]
        public void RunR3Validation()
        {
            R3TuningRunner.RunR305Validation();
        }

        [ContextMenu("Run R4 UI Productization Checks (R4-02)")]
        public void RunR4UiProductizationChecks()
        {
            StandaloneTest.RunR4UiProductizationChecks();
        }

        [ContextMenu("Run R4 UI Interaction Hint Checks (R4-03)")]
        public void RunR4UiInteractionHintChecks()
        {
            StandaloneTest.RunR4UiInteractionHintChecks();
        }

        [ContextMenu("Run R5 Performance Baseline (R5-01)")]
        public void RunR5PerformanceBaseline()
        {
            R5StabilityRunner.RunPerformanceBaseline();
        }

        [ContextMenu("Run R5 Soak Baseline (R5-04)")]
        public void RunR5SoakBaseline()
        {
            R5StabilityRunner.RunSoakBaseline();
        }

        [ContextMenu("Run L Meta Checks (L1-L4)")]
        public void RunLMetaChecks()
        {
            StandaloneTest.RunLMetaChecks();
        }
    }
}
