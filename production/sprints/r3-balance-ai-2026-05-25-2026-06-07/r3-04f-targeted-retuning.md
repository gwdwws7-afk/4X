# R3-04F Targeted Retuning

- Stage: R3 Balance and AI Tuning (2026-05-25 ~ 2026-06-07)
- Task: R3-04F targeted retuning (focus on hard-win clamp + defeat-share lift)
- Execution Date: 2026-04-21
- Result: REGRESSION (best round 7/13, lower than R3-04E 9/13)

## 1. Delivered Scope

1. Added dedicated R3-04F retuning runner (`RunR304FRetuning`) and profile builder (`BuildR304FProfiles`).
2. Added R3-04F menu/context entries for repeatable execution.
3. Executed 3 x 1000-run rounds (`R25/R26/R27`) and generated CSV/KPI/impact artifacts.
4. Updated R3-05 base profile resolver to include R3-04F profiles, while keeping best-known base priority on R3-04E (`R23 -> R24 -> R22`).
5. Executed full `RunAllTests` regression after R3-04F changes.

## 2. Code Changes

| File | Change |
|---|---|
| `Assets/Scripts/Tests/R3TuningRunner.cs` | Added `RunR304FRetuning`, `BuildR304FProfiles`, and updated `ResolveR305BaseProfile` order to keep R3-04E best baseline first |
| `Assets/Editor/TestMenu.cs` | Added menu `EventideAge/Run R3 Targeted Retuning (R3-04F)` |
| `Assets/Scripts/Tests/TestRunner.cs` | Added context menu `Run R3 Targeted Retuning (R3-04F)` |

## 3. Round Results (R3-04F)

| Round | KPI Pass | Win(E/S/H) | Avg End Turn | Attrition | Path Monopoly |
|---|---:|---:|---:|---:|---:|
| R25 | 7/13 | 0.6833 / 0.2900 / 0.0033 | 12.1380 | 0.1700 | 0.9565 |
| R26 | 6/13 | 0.6367 / 0.3700 / 0.0100 | 13.0780 | 0.2000 | 0.9240 |
| R27 | 6/13 | 0.4800 / 0.2950 / 0.0067 | 12.9200 | 0.2030 | 0.9621 |

Selected best round (by current gate rule): `R25 (7/13)`.

## 4. Remaining Gaps (Best Round R25)

- `WIN_RATE_STANDARD`
- `WIN_RATE_HARD`
- `WIN_RATE_OVERALL`
- `AVG_END_TURN_OVERALL`
- `VICTORY_SHARE_OVERALL`
- `SINGLE_PATH_VICTORY_MONOPOLY`

## 5. Regression Status

- `RunAllTests` after R3-04F: PASS (`400 passed, 0 failed`).

## 6. Artifacts

| Artifact | Path |
|---|---|
| R3-04F impact report | `production/evidence/r3/tuning/20260421_R3-04F_TARGETED-IMPACT-REPORT.md` |
| R25 raw results | `production/evidence/r3/tuning/20260421_R3-04F_TARGETED-R25.csv` |
| R25 KPI | `production/evidence/r3/tuning/20260421_R3-04F_TARGETED-R25-KPI.md` |
| R26 raw results | `production/evidence/r3/tuning/20260421_R3-04F_TARGETED-R26.csv` |
| R26 KPI | `production/evidence/r3/tuning/20260421_R3-04F_TARGETED-R26-KPI.md` |
| R27 raw results | `production/evidence/r3/tuning/20260421_R3-04F_TARGETED-R27.csv` |
| R27 KPI | `production/evidence/r3/tuning/20260421_R3-04F_TARGETED-R27-KPI.md` |
| Batch runtime log | `production/evidence/r3/tuning/20260421_R3-04F_BATCH-RUN.log` |
| RunAll regression log | `production/evidence/r3/tuning/20260421_R3-04F_RUNALL-REGRESSION.log` |
| Profile lock | `production/sprints/r3-balance-ai-2026-05-25-2026-06-07/r3-04f-targeted-profiles-v1.json` |

## 7. Conclusion

R3-04F successfully raised defeat share, but overshot difficulty suppression and caused hard-win collapse plus path monopoly rebound. This round should be retained as negative evidence; R3-05 base profile should continue using R3-04E best (`R23`) until a new targeted loop restores both monopoly and win-rate bands together.
