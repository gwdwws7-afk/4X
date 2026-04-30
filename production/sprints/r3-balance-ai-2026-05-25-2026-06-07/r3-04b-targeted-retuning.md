# R3-04B Targeted Retuning

- Stage: R3 Balance and AI Tuning (2026-05-25 ~ 2026-06-07)
- Task: R3-04B targeted retuning before R3-06
- Execution Date: 2026-04-20
- Result: PARTIAL (KPI from 4/13 -> 6/13, still below sign-off gate)

## 1. Scope Delivered

1. Added targeted retuning entry point (`RunR304TargetedRetuning`) and menu/context hooks.
2. Ran three targeted cycles (R04~R12 exploration), each cycle 3 x 1000 simulations.
3. Iteratively updated profile knobs around pacing, difficulty separation, volatility, and path diversity.
4. Selected latest best profile for validation base: `R12 (winrate-recover-diversify)`.

## 2. Code Changes

| File | Change |
|---|---|
| `Assets/Scripts/Tests/R3TuningRunner.cs` | Added targeted retuning pipeline, targeted profile sets, and R3-05 base-profile resolver |
| `Assets/Editor/TestMenu.cs` | Added menu `EventideAge/Run R3 Targeted Retuning (R3-04B)` |
| `Assets/Scripts/Tests/TestRunner.cs` | Added context menu `Run R3 Targeted Retuning (R3-04B)` |

## 3. Latest Cycle Result (R10/R11/R12)

| Round | KPI Pass | Win(E/S/H) | Avg End Turn | Attrition | Path Monopoly |
|---|---:|---:|---:|---:|---:|
| R10 | 4/13 | 0.2667 / 0.6475 / 0.3267 | 13.5620 | 0.2680 | 0.8787 |
| R11 | 4/13 | 0.1833 / 0.7375 / 0.4100 | 12.9920 | 0.2370 | 0.8753 |
| R12 | 6/13 | 0.1767 / 0.5000 / 0.4133 | 14.3930 | 0.2430 | 0.8249 |

Selected best round: `R12 (6/13)`.

## 4. Remaining KPI Gaps (R12)

- `WIN_RATE_EASY`
- `WIN_RATE_HARD`
- `WIN_RATE_OVERALL`
- `AVG_END_TURN_STANDARD`
- `RESOURCE_CV_FIREOIL`
- `RESOURCE_CV_ARMS`
- `SINGLE_PATH_VICTORY_MONOPOLY`

## 5. Artifacts

| Artifact | Path |
|---|---|
| Targeted impact report | `production/evidence/r3/tuning/20260420_R3-04B_TARGETED-IMPACT-REPORT.md` |
| R10 raw results | `production/evidence/r3/tuning/20260420_R3-04B_TARGETED-R10.csv` |
| R10 KPI | `production/evidence/r3/tuning/20260420_R3-04B_TARGETED-R10-KPI.md` |
| R11 raw results | `production/evidence/r3/tuning/20260420_R3-04B_TARGETED-R11.csv` |
| R11 KPI | `production/evidence/r3/tuning/20260420_R3-04B_TARGETED-R11-KPI.md` |
| R12 raw results | `production/evidence/r3/tuning/20260420_R3-04B_TARGETED-R12.csv` |
| R12 KPI | `production/evidence/r3/tuning/20260420_R3-04B_TARGETED-R12-KPI.md` |
| Cycle logs | `production/evidence/r3/tuning/20260420_R3-04B_BATCH-RUN.log`, `production/evidence/r3/tuning/20260420_R3-04B-R02_BATCH-RUN.log`, `production/evidence/r3/tuning/20260420_R3-04B-R03_BATCH-RUN.log` |

## 6. Exit Status

1. Targeted retuning loop is operational and repeatable.
2. KPI pass count improved versus previous R3-05 baseline (4/13 -> 6~7/13 window).
3. Not ready for R3-06 sign-off; continue targeted tuning on the 7 residual KPI gaps.
