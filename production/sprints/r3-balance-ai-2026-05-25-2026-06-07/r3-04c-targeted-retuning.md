# R3-04C Targeted Retuning

- Stage: R3 Balance and AI Tuning (2026-05-25 ~ 2026-06-07)
- Task: R3-04C targeted retuning (focus on 7 residual KPI gaps)
- Execution Date: 2026-04-20
- Result: PARTIAL (best round improved to 7/13, not sign-off ready)

## 1. Delivered Scope

1. Added dedicated R3-04C retuning runner (`RunR304CRetuning`) with independent output naming.
2. Added R3-04C menu/context entries for repeatable execution.
3. Executed 3 x 1000-run rounds (`R13/R14/R15`) and generated CSV/KPI/impact artifacts.
4. Updated R3-05 base profile resolver preference to prioritize R3-04C best candidate order.
5. Executed full `RunAllTests` regression after R3-04C changes.

## 2. Code Changes

| File | Change |
|---|---|
| `Assets/Scripts/Tests/R3TuningRunner.cs` | Added `RunR304CRetuning`, `BuildR304CProfiles`, and updated `ResolveR305BaseProfile` priority (`R14 -> R15 -> R13`) |
| `Assets/Editor/TestMenu.cs` | Added menu `EventideAge/Run R3 Targeted Retuning (R3-04C)` |
| `Assets/Scripts/Tests/TestRunner.cs` | Added context menu `Run R3 Targeted Retuning (R3-04C)` |

## 3. Round Results (R3-04C)

| Round | KPI Pass | Win(E/S/H) | Avg End Turn | Attrition | Path Monopoly |
|---|---:|---:|---:|---:|---:|
| R13 | 4/13 | 0.5467 / 0.2550 / 0.0667 | 13.9360 | 0.2660 | 1.0000 |
| R14 | 7/13 | 0.3400 / 0.0625 / 0.0233 | 14.5540 | 0.2450 | 1.0000 |
| R15 | 5/13 | 0.6333 / 0.2425 / 0.0333 | 13.7840 | 0.2640 | 1.0000 |

Selected best round (by current gate rule): `R14 (7/13)`.

## 4. Remaining Gaps (Best Round R14)

- `WIN_RATE_EASY`
- `WIN_RATE_STANDARD`
- `WIN_RATE_HARD`
- `WIN_RATE_OVERALL`
- `VICTORY_SHARE_OVERALL`
- `SINGLE_PATH_VICTORY_MONOPOLY`

## 5. Regression Status

- `RunAllTests` after R3-04C: PASS (`400 passed, 0 failed`).

## 6. Artifacts

| Artifact | Path |
|---|---|
| R3-04C impact report | `production/evidence/r3/tuning/20260420_R3-04C_TARGETED-IMPACT-REPORT.md` |
| R13 raw results | `production/evidence/r3/tuning/20260420_R3-04C_TARGETED-R13.csv` |
| R13 KPI | `production/evidence/r3/tuning/20260420_R3-04C_TARGETED-R13-KPI.md` |
| R14 raw results | `production/evidence/r3/tuning/20260420_R3-04C_TARGETED-R14.csv` |
| R14 KPI | `production/evidence/r3/tuning/20260420_R3-04C_TARGETED-R14-KPI.md` |
| R15 raw results | `production/evidence/r3/tuning/20260420_R3-04C_TARGETED-R15.csv` |
| R15 KPI | `production/evidence/r3/tuning/20260420_R3-04C_TARGETED-R15-KPI.md` |
| Batch runtime log | `production/evidence/r3/tuning/20260420_R3-04C_BATCH-RUN.log` |
| RunAll regression log | `production/evidence/r3/tuning/20260420_R3-04C_RUNALL-REGRESSION.log` |

## 7. Conclusion

R3-04C improved absolute KPI pass count (`6/13` in R3-04B best -> `7/13` in R3-04C best), but the win-rate distribution and victory-path concentration remain the primary blockers before R3-06 sign-off.
