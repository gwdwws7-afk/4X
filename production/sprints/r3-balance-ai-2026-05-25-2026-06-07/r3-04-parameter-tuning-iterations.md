# R3-04 Parameter Tuning Iterations

- Stage: R3 Balance and AI Tuning (2026-05-25 ~ 2026-06-07)
- Task: R3-04 Parameter tuning iterations (>=3 rounds) + impact report
- Execution Date: 2026-04-20
- Result: COMPLETE (3 rounds executed, impact report generated)

## 1. Delivered Scope

1. Added scriptable R3-04 tuning runner with three profile rounds (`R01/R02/R03`).
2. Executed 3 x 1000-run batches and generated per-round raw CSV + KPI markdown.
3. Generated consolidated impact report with round deltas and best-round selection.
4. Added R3-04 menu/context execution entries for repeatability.
5. Archived tuning profile lock JSON and evidence index.

## 2. Code Changes

| File | Change |
|---|---|
| `Assets/Scripts/Tests/R3TuningRunner.cs` | New R3-04 tuning runner: 3-round profile pipeline, KPI evaluation, impact report writer |
| `Assets/Editor/TestMenu.cs` | Added menu `EventideAge/Run R3 Tuning Iterations (R3-04)` |
| `Assets/Scripts/Tests/TestRunner.cs` | Added context menu `Run R3 Tuning Iterations (R3-04)` |

## 3. Round Results

| Round | KPI Pass | Win(E/S/H) | Avg End Turn | Attrition | Single-Path Monopoly |
|---|---:|---:|---:|---:|---:|
| Baseline (R3-02 Run001) | 0/13 | 1.0000 / 0.9700 / 0.0000 | 3.4620 | 0.0040 | 0.9055 |
| R01 (decelerate-window) | 4/13 | 0.0000 / 0.0000 / 0.0000 | 14.5310 | 0.1920 | 0.0000 |
| R02 (stability-mix) | 4/13 | 0.3467 / 0.0000 / 0.1933 | 13.9750 | 0.2410 | 0.7469 |
| R03 (target-seek) | 4/13 | 0.9233 / 0.3275 / 0.3100 | 9.7610 | 0.0970 | 0.9182 |

## 4. Best-Round Selection

- Selected: `R03 (target-seek)`
- Rule: highest KPI pass count, then closest to standard win-rate and mid-turn pacing targets.
- Current best score: `4/13` KPI pass.

## 5. Remaining Gaps (Best Round)

- Failed KPI IDs:
  - `WIN_RATE_EASY`
  - `WIN_RATE_STANDARD`
  - `AVG_END_TURN_OVERALL`
  - `AVG_END_TURN_STANDARD`
  - `ATTRITION_RATE_OVERALL`
  - `RESOURCE_CV_GOLDLEAF`
  - `RESOURCE_CV_FIREOIL`
  - `RESOURCE_CV_ARMS`
  - `SINGLE_PATH_VICTORY_MONOPOLY`

## 6. Artifacts

| Artifact | Path |
|---|---|
| R01 raw results | `production/evidence/r3/tuning/20260420_R3-04_TUNING-R01.csv` |
| R01 KPI | `production/evidence/r3/tuning/20260420_R3-04_TUNING-R01-KPI.md` |
| R02 raw results | `production/evidence/r3/tuning/20260420_R3-04_TUNING-R02.csv` |
| R02 KPI | `production/evidence/r3/tuning/20260420_R3-04_TUNING-R02-KPI.md` |
| R03 raw results | `production/evidence/r3/tuning/20260420_R3-04_TUNING-R03.csv` |
| R03 KPI | `production/evidence/r3/tuning/20260420_R3-04_TUNING-R03-KPI.md` |
| Impact report | `production/evidence/r3/tuning/20260420_R3-04_TUNING-IMPACT-REPORT.md` |
| Batch runtime log | `production/evidence/r3/tuning/20260420_R3-04_BATCH-RUN.log` |
| RunAll regression log | `production/evidence/r3/tuning/20260420_R3-04_RUNALL-REGRESSION.log` |
| Profile lock | `production/sprints/r3-balance-ai-2026-05-25-2026-06-07/r3-04-tuning-profiles-v1.json` |

## 7. Acceptance Notes

1. R3-04 execution framework is now one-command repeatable.
2. Three-round tuning evidence is complete and comparable.
3. KPI is improved from baseline (`0/13 -> 4/13`) but not yet sign-off ready.
4. Next loop should target:
   - easy/standard win-rate separation
   - end-turn pacing upshift
   - resource volatility increase
   - energy path monopoly reduction.
