# R3-04D Targeted Retuning

- Stage: R3 Balance and AI Tuning (2026-05-25 ~ 2026-06-07)
- Task: R3-04D targeted retuning (focus on win-floor and path-monopoly pressure)
- Execution Date: 2026-04-20
- Result: PARTIAL (best round improved to 8/13, not sign-off ready)

## 1. Delivered Scope

1. Added dedicated R3-04D retuning runner (`RunR304DRetuning`) and profile builder (`BuildR304DProfiles`).
2. Added R3-04D menu/context entries for repeatable execution.
3. Executed 3 x 1000-run rounds (`R19/R20/R21`) and generated CSV/KPI/impact artifacts.
4. Updated R3-05 base profile resolver preference to prioritize R3-04D best candidate order (`R21 -> R19 -> R20`).
5. Executed full `RunAllTests` regression after R3-04D changes.

## 2. Code Changes

| File | Change |
|---|---|
| `Assets/Scripts/Tests/R3TuningRunner.cs` | Added `RunR304DRetuning`, `BuildR304DProfiles`, and updated `ResolveR305BaseProfile` priority (`R21 -> R19 -> R20`) |
| `Assets/Editor/TestMenu.cs` | Added menu `EventideAge/Run R3 Targeted Retuning (R3-04D)` |
| `Assets/Scripts/Tests/TestRunner.cs` | Added context menu `Run R3 Targeted Retuning (R3-04D)` |

## 3. Round Results (R3-04D)

| Round | KPI Pass | Win(E/S/H) | Avg End Turn | Attrition | Path Monopoly |
|---|---:|---:|---:|---:|---:|
| R19 | 7/13 | 0.5333 / 0.3525 / 0.1133 | 14.9600 | 0.2270 | 0.9761 |
| R20 | 7/13 | 0.4033 / 0.2425 / 0.0633 | 14.5700 | 0.2340 | 0.9873 |
| R21 | 8/13 | 0.5967 / 0.4350 / 0.1433 | 14.5180 | 0.2010 | 0.9773 |

Selected best round (by current gate rule): `R21 (8/13)`.

## 4. Remaining Gaps (Best Round R21)

- `WIN_RATE_EASY`
- `WIN_RATE_STANDARD`
- `WIN_RATE_HARD`
- `WIN_RATE_OVERALL`
- `SINGLE_PATH_VICTORY_MONOPOLY`

## 5. Regression Status

- `RunAllTests` after R3-04D: PASS (`400 passed, 0 failed`).

## 6. Artifacts

| Artifact | Path |
|---|---|
| R3-04D impact report | `production/evidence/r3/tuning/20260420_R3-04D_TARGETED-IMPACT-REPORT.md` |
| R19 raw results | `production/evidence/r3/tuning/20260420_R3-04D_TARGETED-R19.csv` |
| R19 KPI | `production/evidence/r3/tuning/20260420_R3-04D_TARGETED-R19-KPI.md` |
| R20 raw results | `production/evidence/r3/tuning/20260420_R3-04D_TARGETED-R20.csv` |
| R20 KPI | `production/evidence/r3/tuning/20260420_R3-04D_TARGETED-R20-KPI.md` |
| R21 raw results | `production/evidence/r3/tuning/20260420_R3-04D_TARGETED-R21.csv` |
| R21 KPI | `production/evidence/r3/tuning/20260420_R3-04D_TARGETED-R21-KPI.md` |
| Batch runtime log (D2 profiles) | `production/evidence/r3/tuning/20260420_R3-04D_BATCH-RUN_D2.log` |
| RunAll regression log | `production/evidence/r3/tuning/20260420_R3-04D_RUNALL-REGRESSION.log` |
| Profile lock | `production/sprints/r3-balance-ai-2026-05-25-2026-06-07/r3-04d-targeted-profiles-v1.json` |

## 7. Conclusion

R3-04D raised the best score from `7/13` to `8/13` and pulled overall pacing into target bands, but win-rate floors and single-path concentration remain blocking items for R3-05 final acceptance.
