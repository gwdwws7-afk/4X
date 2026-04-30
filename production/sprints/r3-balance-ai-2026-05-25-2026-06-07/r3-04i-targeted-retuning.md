# R3-04I Targeted Retuning

- Stage: R3 Balance and AI Tuning (2026-05-25 ~ 2026-06-07)
- Task: R3-04I targeted retuning (close remaining R3-05 residual gaps)
- Execution Date: 2026-04-21
- Result: PARTIAL PASS (best validation candidate narrowed residual gaps to 2 KPI items)

## 1. Delivered Scope

1. Added R3-04I runner and execution entry points for repeatable retuning loops.
2. Executed I-batch iterations (`B001` ~ `B006`) and compared KPI drift.
3. Ran R3-05 validation against I candidates and selected current best baseline (`R99`).
4. Updated R3-05 base-profile resolver priority to `R99 -> R97 -> R98 -> R82 -> ...`.
5. Ran post-validation `RunAllTests` regression.

## 2. Code Changes

| File | Change |
|---|---|
| `Assets/Scripts/Tests/R3TuningRunner.cs` | Added `RunR304IRetuning`, `BuildR304IProfiles`, updated resolver order for R3-05 base profile |
| `Assets/Editor/TestMenu.cs` | Added menu `EventideAge/Run R3 Targeted Retuning (R3-04I)` |
| `Assets/Scripts/Tests/TestRunner.cs` | Added context menu `Run R3 Targeted Retuning (R3-04I)` |

## 3. Iteration Summary

| Batch | Candidate Set | Best Round | Best KPI Pass | Notes |
|---|---|---|---:|---|
| B001 | R85/R86/R87 | R85 | 11/13 | Initial stability attempt, pacing floor still low |
| B002 | R88/R89/R90 | R89 | 10/13 | Improved pacing, but easy-floor regression risk |
| B003 | R91/R92/R93 | R93 | 12/13 | Single-run closest to gate, pacing floor still low |
| B004 | R94/R95/R96 | R95 | 11/13 | Gate-balance attempt did not beat B003 |
| B005 | R97/R98/R99 | R99 | 12/13 | Best practical validation baseline so far |
| B006 | R100/R101/R102 | R102 | 10/13 | Gap-closing over-pivot; not selected |

## 4. Selected Baseline

- Selected round: `R99 (r82-early-suppress-c)` from `B005`.
- R3-05 (R99 base) run-level scores: `11/13`, `11/13`, `11/13`.
- Residual blockers after R3-05:
  - `AVG_END_TURN_OVERALL` (floor miss)
  - `DEFEAT_SHARE_OVERALL` (floor miss)

## 5. Regression Status

- Post-R99 validation `RunAllTests`: PASS (`400 passed, 0 failed`).

## 6. Artifacts

| Artifact | Path |
|---|---|
| I impact report (latest run) | `production/evidence/r3/tuning/20260421_R3-04I_TARGETED-IMPACT-REPORT.md` |
| R99 raw results | `production/evidence/r3/tuning/20260421_R3-04I_TARGETED-R99.csv` |
| R99 KPI | `production/evidence/r3/tuning/20260421_R3-04I_TARGETED-R99-KPI.md` |
| B005 runtime log | `production/evidence/r3/tuning/20260421_R3-04I_BATCH-RUN_B005.log` |
| R3-05 final report (R99) | `production/evidence/r3/validation/20260421_R3-05_FINAL-BALANCE-REPORT-R99.md` |
| R3-05 summary (R99) | `production/evidence/r3/validation/20260421_R3-05_VALIDATION-SUMMARY-R99.json` |
| R3-05 RunAll regression | `production/evidence/r3/validation/20260421_R3-05-R99_RUNALL-REGRESSION_retry.log` |

## 7. Conclusion

R3-04I has reduced R3-05 residuals from 5 KPI failures (`R82` base) to 2 KPI failures (`R99` base). The remaining work is now focused and bounded to pacing floor + defeat share floor convergence.
