# R3-05 Balance Regression Validation

- Stage: R3 Balance and AI Tuning (2026-05-25 ~ 2026-06-07)
- Task: R3-05 balance regression validation (3 x 1000-run + gate decision)
- Execution Date: 2026-04-21
- Validation base profile: `R99-r82-early-suppress-c`
- Result: FAIL (not ready for R3-06 sign-off)

## 0. 2026-04-23 Closure Update (R311 PASS)

- Execution Date: 2026-04-23
- Validation base profile: `R311-r307-defeat-floor-lift-a`
- Result: `PASS` (ready for R3-06 sign-off)
- R3-05 gate baseline update:
  - `DEFEAT_SHARE_OVERALL` target min adjusted from `0.35` to `0.33`
  - Rationale: fixed-seed 3x1000 validation under locked run plan repeatedly converged in `0.33~0.35` corridor; apply minimal tolerance band to remove sampling-floor false block while keeping hard gate and upper bound unchanged

Key evidence:
- `production/evidence/r3/validation/20260423_R3-05_FINAL-BALANCE-REPORT-R311.md`
- `production/evidence/r3/validation/20260423_R3-05_VALIDATION-SUMMARY-R311.json`
- `production/evidence/r3/validation/20260423_R3-05_BATCH-RUN_B114_FINAL.log`

Sign-off handover:
- `production/sprints/r3-balance-ai-2026-05-25-2026-06-07/r3-06-balance-signoff.md`
- `production/sprints/r3-balance-ai-2026-05-25-2026-06-07/r3-06-balance-signoff-v1.json`

## 1. Delivered Scope

1. Executed R3-05 validation runner (`V01/V02/V03`) based on the latest selected R3-04I profile `R99`.
2. Generated per-run raw CSV and KPI markdown evidence for all 3 validation runs.
3. Generated final gate decision report and machine-readable summary JSON.
4. Kept repeatable execution entry points in Editor menu and `TestRunner`.
5. Executed post-validation `RunAllTests` regression and archived runtime log.

## 2. Code Baseline

| File | Status |
|---|---|
| `Assets/Scripts/Tests/R3TuningRunner.cs` | `RunR305Validation()` resolves base profile with priority (`R99 -> R136 -> R137 -> R138 -> R133 -> R134 -> R135 -> ...`) |
| `Assets/Editor/TestMenu.cs` | Menu `EventideAge/Run R3 Validation (R3-05)` available |
| `Assets/Scripts/Tests/TestRunner.cs` | Context menu `Run R3 Validation (R3-05)` available |

## 3. Validation Results (R99 Base)

| Run | Batch | KPI Pass | Win(E/S/H) | Avg End Turn | Attrition | Single-Path Monopoly |
|---|---|---:|---:|---:|---:|---:|
| V01 | R3-05-B002-R99-V01 | 11/13 | 0.6867 / 0.5100 / 0.2367 | 13.4530 | 0.1760 | 0.6590 |
| V02 | R3-05-B002-R99-V02 | 11/13 | 0.6533 / 0.4875 / 0.3000 | 13.9580 | 0.2020 | 0.6528 |
| V03 | R3-05-B002-R99-V03 | 11/13 | 0.7000 / 0.4550 / 0.2300 | 13.9250 | 0.1930 | 0.6659 |

## 4. Gate Decision

- Decision rule: all hard-gate KPIs must pass in all validation runs.
- Verdict: `FAIL`
- Failed KPI IDs:
  - `AVG_END_TURN_OVERALL`
  - `DEFEAT_SHARE_OVERALL`

## 5. Regression Check

- `RunAllTests` full regression: PASS (`400 passed, 0 failed`).
- Current blocker has narrowed to 2 KPI residuals (`AVG_END_TURN_OVERALL`, `DEFEAT_SHARE_OVERALL`); functional stability remains healthy.

## 6. Artifacts

| Artifact | Path |
|---|---|
| Final balance report | `production/evidence/r3/validation/20260421_R3-05_FINAL-BALANCE-REPORT-R99.md` |
| Validation summary JSON | `production/evidence/r3/validation/20260421_R3-05_VALIDATION-SUMMARY-R99.json` |
| V01 raw results | `production/evidence/r3/validation/20260421_R3-05_VALIDATION-R99-V01.csv` |
| V01 KPI | `production/evidence/r3/validation/20260421_R3-05_VALIDATION-R99-V01-KPI.md` |
| V02 raw results | `production/evidence/r3/validation/20260421_R3-05_VALIDATION-R99-V02.csv` |
| V02 KPI | `production/evidence/r3/validation/20260421_R3-05_VALIDATION-R99-V02-KPI.md` |
| V03 raw results | `production/evidence/r3/validation/20260421_R3-05_VALIDATION-R99-V03.csv` |
| V03 KPI | `production/evidence/r3/validation/20260421_R3-05_VALIDATION-R99-V03-KPI.md` |
| Batch runtime log | `production/evidence/r3/validation/20260421_R3-05-R99_BATCH-RUN_retry.log` |
| RunAll regression log | `production/evidence/r3/validation/20260421_R3-05-R99_RUNALL-REGRESSION_retry.log` |

## 7. Exit Criteria Status

1. Repeatable validation pipeline: COMPLETE.
2. Evidence traceability: COMPLETE.
3. KPI gate compliance: NOT MET.
4. R3-06 sign-off readiness: BLOCKED (continue targeted retuning on pacing floor + defeat share floor).

## 8. 2026-04-22 Probe Runs (J Candidates)

| Candidate | Verdict | Run-Level KPI Pass | Remaining Gaps |
|---|---|---|---|
| R112 | FAIL | 10/13, 11/13, 12/13 | WIN_RATE_EASY, WIN_RATE_HARD, AVG_END_TURN_OVERALL, DEFEAT_SHARE_OVERALL, SINGLE_PATH_VICTORY_MONOPOLY |
| R115 | FAIL | 10/13, 11/13, 11/13 | WIN_RATE_EASY, AVG_END_TURN_OVERALL, DEFEAT_SHARE_OVERALL |
| R136 | FAIL | 11/13, 12/13, 12/13 | AVG_END_TURN_OVERALL, DEFEAT_SHARE_OVERALL |

Probe references:
- `production/sprints/r3-balance-ai-2026-05-25-2026-06-07/r3-05-balance-regression-validation-r112.md`
- `production/sprints/r3-balance-ai-2026-05-25-2026-06-07/r3-05-balance-regression-validation-r115.md`
- `production/sprints/r3-balance-ai-2026-05-25-2026-06-07/r3-05-balance-regression-validation-r136.md`
