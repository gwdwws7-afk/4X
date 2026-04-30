# R3-05 Final Balance Report

- Stage: R3 Balance and AI Tuning
- Task: R3-05 balance regression validation
- Date: 2026-04-23
- Validation base profile: `R296-r293-late-defeat-balance-a`
- Verdict: `FAIL`

## 1. Run-Level KPI Score

| Run | Batch | KPI Pass | Win(E/S/H) | AvgEndTurn | Attrition | PathMonopoly |
|---|---|---:|---:|---:|---:|---:|
| V01 | R3-05-B002-R296-V01 | 13/13 | 0.6500/0.4775/0.2300 | 14.3360 | 0.1950 | 0.6813 |
| V02 | R3-05-B002-R296-V02 | 12/13 | 0.6767/0.4875/0.3200 | 14.2730 | 0.1920 | 0.6498 |
| V03 | R3-05-B002-R296-V03 | 12/13 | 0.6700/0.4425/0.2000 | 14.4360 | 0.1880 | 0.6347 |

## 2. KPI Stability Matrix

| KPI | Mean Actual | Pass Runs | Target |
|---|---:|---:|---:|
| WIN_RATE_EASY | 0.6656 | 3/3 | [0.6500, 0.8000] |
| WIN_RATE_STANDARD | 0.4692 | 2/3 | [0.4500, 0.5500] |
| WIN_RATE_HARD | 0.2500 | 3/3 | [0.2000, 0.3500] |
| WIN_RATE_OVERALL | 0.4623 | 3/3 | [0.4000, 0.6000] |
| AVG_END_TURN_OVERALL | 14.3483 | 3/3 | [14.0000, 20.0000] |
| AVG_END_TURN_STANDARD | 15.3250 | 3/3 | [15.0000, 19.0000] |
| ATTRITION_RATE_OVERALL | 0.1917 | 3/3 | [0.1000, 0.3500] |
| RESOURCE_CV_GOLDLEAF | 0.4218 | 3/3 | [0.1800, 0.4500] |
| RESOURCE_CV_FIREOIL | 0.1839 | 3/3 | [0.1500, 0.4000] |
| RESOURCE_CV_ARMS | 0.2729 | 3/3 | [0.2000, 0.5500] |
| VICTORY_SHARE_OVERALL | 0.4623 | 3/3 | [0.3500, 0.6500] |
| DEFEAT_SHARE_OVERALL | 0.3460 | 2/3 | [0.3500, 0.6500] |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.6553 | 3/3 | [0.0000, 0.7000] |

## 3. Gate Decision Rule

- Rule: all hard-gate KPIs must pass in all validation runs.
- Result: `FAIL`
- Failed KPI IDs: WIN_RATE_STANDARD, DEFEAT_SHARE_OVERALL

## 4. Acceptance Conclusion

- R3-05 validation does not meet KPI gates. Keep R3 in `not ready` state and continue targeted tuning.
