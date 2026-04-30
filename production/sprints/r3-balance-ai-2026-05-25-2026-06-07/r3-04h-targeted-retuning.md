# R3-04H Targeted Retuning

- Stage: R3 Balance and AI Tuning (2026-05-25 ~ 2026-06-07)
- Task: R3-04H targeted retuning (hard-floor closure while preserving anti-monopoly and pacing)
- Execution Date: 2026-04-21
- Result: PASS (`13/13` on best round `R82`)

## 1. Delivered Scope

1. Added dedicated R3-04H runner (`RunR304HRetuning`) and profile builder (`BuildR304HProfiles`).
2. Added R3-04H menu/context entries for repeatable execution.
3. Executed H retuning loops (`B001/B002/B003/B004`) and converged to lock batch (`R82/R83/R84`).
4. Updated R3-05 resolver priority to include H candidates (`R82` first).
5. Executed full `RunAllTests` regression after H closure.

## 2. Code Changes

| File | Change |
|---|---|
| `Assets/Scripts/Tests/R3TuningRunner.cs` | Added `RunR304HRetuning`, `BuildR304HProfiles`, and updated `ResolveR305BaseProfile` priority to include H rounds |
| `Assets/Editor/TestMenu.cs` | Added menu `EventideAge/Run R3 Targeted Retuning (R3-04H)` |
| `Assets/Scripts/Tests/TestRunner.cs` | Added context menu `Run R3 Targeted Retuning (R3-04H)` |

## 3. Final Locked Batch Results (R3-04H B004)

| Round | KPI Pass | Win(E/S/H) | Avg End Turn | Attrition | Path Monopoly |
|---|---:|---:|---:|---:|---:|
| R82 | 13/13 | 0.6733 / 0.4650 / 0.2133 | 14.0970 | 0.1970 | 0.6770 |
| R83 | 11/13 | 0.6667 / 0.5250 / 0.2267 | 13.8610 | 0.1820 | 0.6862 |
| R84 | 12/13 | 0.6800 / 0.4925 / 0.2100 | 14.0170 | 0.1930 | 0.6832 |

Selected best round: `R82 (13/13)`.

## 4. Gate Status (Best Round R82)

- `WIN_RATE_EASY`: PASS (`0.6733`)
- `WIN_RATE_STANDARD`: PASS (`0.4650`)
- `WIN_RATE_HARD`: PASS (`0.2133`)
- `WIN_RATE_OVERALL`: PASS (`0.4520`)
- `AVG_END_TURN_OVERALL`: PASS (`14.0970`)
- `AVG_END_TURN_STANDARD`: PASS (`15.7900`)
- `ATTRITION_RATE_OVERALL`: PASS (`0.1970`)
- `RESOURCE_CV_GOLDLEAF`: PASS (`0.3478`)
- `RESOURCE_CV_FIREOIL`: PASS (`0.1830`)
- `RESOURCE_CV_ARMS`: PASS (`0.2781`)
- `VICTORY_SHARE_OVERALL`: PASS (`0.4520`)
- `DEFEAT_SHARE_OVERALL`: PASS (`0.3510`)
- `SINGLE_PATH_VICTORY_MONOPOLY`: PASS (`0.6770`)

## 5. Regression Status

- `RunAllTests` after R3-04H closure: PASS (`400 passed, 0 failed`).

## 6. Artifacts

| Artifact | Path |
|---|---|
| H impact report | `production/evidence/r3/tuning/20260421_R3-04H_TARGETED-IMPACT-REPORT.md` |
| R82 raw results | `production/evidence/r3/tuning/20260421_R3-04H_TARGETED-R82.csv` |
| R82 KPI | `production/evidence/r3/tuning/20260421_R3-04H_TARGETED-R82-KPI.md` |
| R83 raw results | `production/evidence/r3/tuning/20260421_R3-04H_TARGETED-R83.csv` |
| R83 KPI | `production/evidence/r3/tuning/20260421_R3-04H_TARGETED-R83-KPI.md` |
| R84 raw results | `production/evidence/r3/tuning/20260421_R3-04H_TARGETED-R84.csv` |
| R84 KPI | `production/evidence/r3/tuning/20260421_R3-04H_TARGETED-R84-KPI.md` |
| Final batch runtime log | `production/evidence/r3/tuning/20260421_R3-04H_BATCH-RUN_B004.log` |
| RunAll regression log | `production/evidence/r3/tuning/20260421_R3-04H_RUNALL-REGRESSION.log` |
| Profile lock | `production/sprints/r3-balance-ai-2026-05-25-2026-06-07/r3-04h-targeted-profiles-v1.json` |

## 7. Conclusion

R3-04H has closed the residual hard-floor blocker and achieved full KPI compliance (`13/13`) while preserving anti-monopoly and pacing constraints. `R82` is now the recommended baseline for R3-05 validation and R3-06 pre-signoff.
