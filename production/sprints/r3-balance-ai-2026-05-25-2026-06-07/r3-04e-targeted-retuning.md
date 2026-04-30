# R3-04E Targeted Retuning

- Stage: R3 Balance and AI Tuning (2026-05-25 ~ 2026-06-07)
- Task: R3-04E targeted retuning (focus on path monopoly + win-floor recovery)
- Execution Date: 2026-04-21
- Result: PARTIAL (best round improved to 9/13, not sign-off ready)

## 1. Delivered Scope

1. Added dedicated R3-04E retuning runner (`RunR304ERetuning`) and profile builder (`BuildR304EProfiles`).
2. Added R3-04E menu/context entries for repeatable execution.
3. Executed 3 x 1000-run rounds (`R22/R23/R24`) and generated CSV/KPI/impact artifacts.
4. Updated R3-05 base profile resolver priority to include R3-04E (`R23 -> R24 -> R22`).
5. Executed full `RunAllTests` regression after R3-04E changes.

## 2. Code Changes

| File | Change |
|---|---|
| `Assets/Scripts/Tests/R3TuningRunner.cs` | Added `RunR304ERetuning`, `BuildR304EProfiles`, and updated `ResolveR305BaseProfile` priority (`R23 -> R24 -> R22`) |
| `Assets/Editor/TestMenu.cs` | Added menu `EventideAge/Run R3 Targeted Retuning (R3-04E)` |
| `Assets/Scripts/Tests/TestRunner.cs` | Added context menu `Run R3 Targeted Retuning (R3-04E)` |

## 3. Round Results (R3-04E)

| Round | KPI Pass | Win(E/S/H) | Avg End Turn | Attrition | Path Monopoly |
|---|---:|---:|---:|---:|---:|
| R22 | 9/13 | 0.3933 / 0.3350 / 0.6067 | 14.3010 | 0.2560 | 0.6014 |
| R23 | 9/13 | 0.6067 / 0.4675 / 0.6467 | 13.8410 | 0.1940 | 0.6767 |
| R24 | 9/13 | 0.4467 / 0.3400 / 0.6467 | 14.5890 | 0.2460 | 0.6034 |

Selected best round (by current gate rule): `R23 (9/13)`.

## 4. Remaining Gaps (Best Round R23)

- `WIN_RATE_EASY`
- `WIN_RATE_HARD`
- `AVG_END_TURN_OVERALL`
- `DEFEAT_SHARE_OVERALL`

## 5. Regression Status

- `RunAllTests` after R3-04E: PASS (`400 passed, 0 failed`).

## 6. Artifacts

| Artifact | Path |
|---|---|
| R3-04E impact report | `production/evidence/r3/tuning/20260421_R3-04E_TARGETED-IMPACT-REPORT.md` |
| R22 raw results | `production/evidence/r3/tuning/20260421_R3-04E_TARGETED-R22.csv` |
| R22 KPI | `production/evidence/r3/tuning/20260421_R3-04E_TARGETED-R22-KPI.md` |
| R23 raw results | `production/evidence/r3/tuning/20260421_R3-04E_TARGETED-R23.csv` |
| R23 KPI | `production/evidence/r3/tuning/20260421_R3-04E_TARGETED-R23-KPI.md` |
| R24 raw results | `production/evidence/r3/tuning/20260421_R3-04E_TARGETED-R24.csv` |
| R24 KPI | `production/evidence/r3/tuning/20260421_R3-04E_TARGETED-R24-KPI.md` |
| Batch runtime log | `production/evidence/r3/tuning/20260421_R3-04E_BATCH-RUN.log` |
| RunAll regression log | `production/evidence/r3/tuning/20260421_R3-04E_RUNALL-REGRESSION.log` |
| Profile lock | `production/sprints/r3-balance-ai-2026-05-25-2026-06-07/r3-04e-targeted-profiles-v1.json` |

## 7. Conclusion

R3-04E significantly reduced single-path victory monopoly into KPI target range (`< 0.70`) while improving total KPI score (`8/13 -> 9/13`). Remaining blockers are now concentrated on difficulty distribution shape (`easy low`, `hard high`) and end-state balance (`defeat share low`).
