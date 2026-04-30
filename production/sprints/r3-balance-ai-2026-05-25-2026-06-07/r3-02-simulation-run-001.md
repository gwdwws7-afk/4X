# R3-02 Simulation Run 001

- Stage: R3 Balance and AI Tuning (2026-05-25 ~ 2026-06-07)
- Task: R3-02 Build 1000-run simulation pipeline and generate first batch results
- Execution Date: 2026-04-20
- Result: COMPLETE (pipeline and first batch generated)

## 1. Delivered Scope

1. Added dedicated 1000-run simulation runner (`R3SimulationRunner`).
2. Added Unity menu/context entry points for batch execution.
3. Generated first batch CSV (`1000` runs) in template-compatible schema.
4. Generated KPI evaluation markdown against R3-01 baseline.
5. Archived Unity batch runtime log for reproducibility.

## 2. Code Changes

| File | Change |
|---|---|
| `Assets/Scripts/Tests/R3SimulationRunner.cs` | New R3-02 simulation runner, CSV writer, KPI evaluator, evidence README generator |
| `Assets/Editor/TestMenu.cs` | Added menu entry `EventideAge/Run R3 Simulation Batch (1000)` |
| `Assets/Scripts/Tests/TestRunner.cs` | Added context menu entry `Run R3 Simulation Batch (R3-02, 1000)` |

## 3. Batch Outputs

| Artifact | Path |
|---|---|
| Raw simulation results (1000 runs) | `production/evidence/r3/simulation/20260420_R3-02_SIMULATION-RUN-001.csv` |
| KPI evaluation (run 001) | `production/evidence/r3/simulation/20260420_R3-02_KPI-EVALUATION-001.md` |
| Unity batch runtime log | `production/evidence/r3/simulation/20260420_R3-02_BATCH-RUN.log` |
| Evidence index | `production/evidence/r3/simulation/README.md` |

## 4. Run 001 Snapshot

- Total runs: `1000`
- Distribution:
  - victory: `688` (`0.6880`)
  - defeat: `308` (`0.3080`)
  - timeout(attrition): `4` (`0.0040`)
- Average end turn (overall): `3.4620`
- Win rates:
  - easy: `1.0000`
  - standard: `0.9700`
  - hard: `0.0000`
- Victory path concentration:
  - energyliberation: `623 / 688` (`0.9055`)
  - axisvictory: `65 / 688` (`0.0945`)

## 5. KPI Baseline Comparison

- Current run **does not meet** R3-01 target bands (multiple hard gates are FAIL).
- Primary deviation pattern:
  1. Endgame occurs too early (avg turn far below target 14~20).
  2. Easy/Standard are over-winning; Hard under-winning.
  3. Attrition share is too low.
  4. Single victory path monopoly is too high.

## 6. Next Action (R3-03 input)

1. Reduce early `energyliberation` dominance (raise gate difficulty and/or lower early Social/TradeToken momentum).
2. Re-balance difficulty profiles to target Easy/Standard/Hard win-rate bands.
3. Increase mid/late game survival variance so end-turn distribution shifts toward 14~20.
4. Run `R3-02` batch 002 after parameter update and compare against run 001.
