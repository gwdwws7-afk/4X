# R3-04G Targeted Retuning

- Stage: R3 Balance and AI Tuning (2026-05-25 ~ 2026-06-07)
- Task: R3-04G targeted retuning (focus on closing R23 four residual KPI gaps while preserving anti-monopoly)
- Execution Date: 2026-04-21
- Result: PARTIAL SUCCESS (best round `12/13`, not yet R3-06 sign-off ready)

## 1. Delivered Scope

1. Added dedicated R3-04G retuning runner (`RunR304GRetuning`) and profile builder (`BuildR304GProfiles`).
2. Added R3-04G menu/context entries for repeatable execution.
3. Executed multi-batch targeted loops (`R28`~`R72`) and retained latest lock batch (`R70/R71/R72`).
4. Updated R3-05 base profile resolver priority to prefer latest best-known G baseline (`R70`).
5. Executed full `RunAllTests` regression after R3-04G iteration closure.

## 2. Code Changes

| File | Change |
|---|---|
| `Assets/Scripts/Tests/R3TuningRunner.cs` | Added/iterated `RunR304GRetuning`, maintained G profiles, and updated `ResolveR305BaseProfile` priority (`R70` first) |
| `Assets/Editor/TestMenu.cs` | Added menu `EventideAge/Run R3 Targeted Retuning (R3-04G)` |
| `Assets/Scripts/Tests/TestRunner.cs` | Added context menu `Run R3 Targeted Retuning (R3-04G)` |
| `Assets/Scripts/Systems/J/VictoryDefeatSystem.cs` | Corrected combined-victory precedence (`completedPaths >= 2` checked before single-path dispatch) |

## 3. Final Locked Batch Results (R3-04G B015)

| Round | KPI Pass | Win(E/S/H) | Avg End Turn | Attrition | Path Monopoly |
|---|---:|---:|---:|---:|---:|
| R70 | 12/13 | 0.6800 / 0.4500 / 0.1833 | 14.0580 | 0.1970 | 0.6788 |
| R71 | 9/13 | 0.6367 / 0.4425 / 0.1500 | 14.8240 | 0.2090 | 0.7312 |
| R72 | 10/13 | 0.6167 / 0.4500 / 0.1600 | 14.5800 | 0.2050 | 0.7385 |

Selected best round: `R70 (12/13)`.

## 4. R23 Gap Closure Status (Best Round R70)

- `WIN_RATE_EASY`: PASS (`0.6800`)
- `WIN_RATE_HARD`: FAIL (`0.1833`, target `>= 0.2000`)
- `AVG_END_TURN_OVERALL`: PASS (`14.0580`)
- `DEFEAT_SHARE_OVERALL`: PASS (`0.3640`)
- Anti-monopoly guard `SINGLE_PATH_VICTORY_MONOPOLY`: PASS (`0.6788`)

## 5. Regression Status

- `RunAllTests` after R3-04G closure: PASS (`400 passed, 0 failed`).

## 6. Artifacts

| Artifact | Path |
|---|---|
| Final impact report | `production/evidence/r3/tuning/20260421_R3-04G_TARGETED-IMPACT-REPORT.md` |
| R70 raw results | `production/evidence/r3/tuning/20260421_R3-04G_TARGETED-R70.csv` |
| R70 KPI | `production/evidence/r3/tuning/20260421_R3-04G_TARGETED-R70-KPI.md` |
| R71 raw results | `production/evidence/r3/tuning/20260421_R3-04G_TARGETED-R71.csv` |
| R71 KPI | `production/evidence/r3/tuning/20260421_R3-04G_TARGETED-R71-KPI.md` |
| R72 raw results | `production/evidence/r3/tuning/20260421_R3-04G_TARGETED-R72.csv` |
| R72 KPI | `production/evidence/r3/tuning/20260421_R3-04G_TARGETED-R72-KPI.md` |
| Final batch runtime log | `production/evidence/r3/tuning/20260421_R3-04G_BATCH-RUN_G15.log` |
| Post-batch RunAll regression log | `production/evidence/r3/tuning/20260421_R3-04G_RUNALL-REGRESSION_G15.log` |
| Profile lock | `production/sprints/r3-balance-ai-2026-05-25-2026-06-07/r3-04g-targeted-profiles-v1.json` |

## 7. Conclusion

R3-04G has effectively closed 3 out of R23's 4 residual KPI gaps while preserving anti-monopoly. Remaining blocker is a narrow hard-win floor miss (`0.1833` vs `0.2000`). Keep `R70` as next-loop baseline and run one more hard-floor micro-cycle before R3-05 final validation.
