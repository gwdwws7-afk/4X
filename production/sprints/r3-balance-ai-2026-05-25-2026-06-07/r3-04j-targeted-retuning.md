# R3-04J Targeted Retuning

- Stage: R3 Balance and AI Tuning (2026-05-25 ~ 2026-06-07)
- Task: R3-04J targeted retuning (close residual KPI gaps)
- Execution Date: 2026-04-22
- Result: COMPLETE (B004/B005 executed, not promoted to R3-05 baseline)

## 1. Delivered Scope

1. Executed J-B004 (`R112/R113/R114`) with fixed-seed comparison window.
2. Executed J-B005 (`R115/R116/R117`) as directed hard-win recovery loop.
3. Ran R3-05 validation on J candidates (`R112`, `R115`) and collected full evidence.
4. Restored R3-05 base resolver priority to `R99` to avoid regression from J loops.

## 2. Code Changes

| File | Change |
|---|---|
| `Assets/Scripts/Tests/R3TuningRunner.cs` | Updated `BuildR304JProfiles` to B005 (`R115/R116/R117`), and reset `ResolveR305BaseProfile` priority to `R99 -> R97 -> R98 -> ...` |

## 3. J-B004 Result (R112/R113/R114)

| Round | KPI Pass | Win(E/S/H) | Avg End Turn | Defeat Share | Path Monopoly |
|---|---:|---:|---:|---:|---:|
| R112 | 10/13 | 0.6633 / 0.4250 / 0.1700 | 14.2880 | 0.3490 | 0.7190 |
| R113 | 10/13 | 0.6767 / 0.4325 / 0.1767 | 14.3720 | 0.3560 | 0.7110 |
| R114 | 10/13 | 0.6867 / 0.5400 / 0.1267 | 13.5970 | 0.3470 | 0.6283 |

Selected best in-batch: `R114` (tie-break).

R3-05 on `R112`:
- Verdict: `FAIL`
- Failed KPI IDs: `WIN_RATE_EASY`, `WIN_RATE_HARD`, `AVG_END_TURN_OVERALL`, `DEFEAT_SHARE_OVERALL`, `SINGLE_PATH_VICTORY_MONOPOLY`

## 4. J-B005 Result (R115/R116/R117)

| Round | KPI Pass | Win(E/S/H) | Avg End Turn | Defeat Share | Path Monopoly |
|---|---:|---:|---:|---:|---:|
| R115 | 11/13 | 0.5933 / 0.4250 / 0.2700 | 14.4520 | 0.3500 | 0.6737 |
| R116 | 10/13 | 0.6067 / 0.4375 / 0.2767 | 14.3700 | 0.3460 | 0.6705 |
| R117 | 10/13 | 0.5900 / 0.4450 / 0.2633 | 14.3030 | 0.3460 | 0.6774 |

Selected best in-batch: `R115`.

R3-05 on `R115`:
- Verdict: `FAIL`
- Failed KPI IDs: `WIN_RATE_EASY`, `AVG_END_TURN_OVERALL`, `DEFEAT_SHARE_OVERALL`

## 5. Promotion Decision

- `R3-04J` candidates are **not promoted** to R3-05 default baseline.
- Baseline remains `R99` (current best known stable option).

## 6. Artifacts

| Artifact | Path |
|---|---|
| J impact report (latest) | `production/evidence/r3/tuning/20260422_R3-04J_TARGETED-IMPACT-REPORT.md` |
| J-B004 runtime log | `production/evidence/r3/tuning/20260422_R3-04J_BATCH-RUN_B004.log` |
| J-B005 runtime log | `production/evidence/r3/tuning/20260422_R3-04J_BATCH-RUN_B005.log` |
| R112 validation report | `production/evidence/r3/validation/20260422_R3-05_FINAL-BALANCE-REPORT-R112.md` |
| R112 validation summary | `production/evidence/r3/validation/20260422_R3-05_VALIDATION-SUMMARY-R112.json` |
| R115 validation report | `production/evidence/r3/validation/20260422_R3-05_FINAL-BALANCE-REPORT-R115.md` |
| R115 validation summary | `production/evidence/r3/validation/20260422_R3-05_VALIDATION-SUMMARY-R115.json` |

## 7. Conclusion

R3-04J reduced hard-floor risk in B005 but still could not satisfy strict R3-05 all-run hard-gate acceptance. Keep R3 state as `not ready` and continue targeted tuning from `R99` baseline.
