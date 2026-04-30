# R3-05 Final Balance Report

- Stage: R3 Balance and AI Tuning
- Task: R3-05 balance regression validation
- Date: 2026-04-23
- Validation base profile: `R204-r201-stability-close-c`
- Verdict: `FAIL`

## 1. Run-Level KPI Score

| Run | Batch | KPI Pass | Win(E/S/H) | AvgEndTurn | Attrition | PathMonopoly |
|---|---|---:|---:|---:|---:|---:|
| V01 | R3-05-B002-R204-V01 | 11/13 | 0.6633/0.5125/0.2233 | 14.0530 | 0.2010 | 0.6773 |
| V02 | R3-05-B002-R204-V02 | 12/13 | 0.6767/0.4750/0.3100 | 14.2650 | 0.1920 | 0.6667 |
| V03 | R3-05-B002-R204-V03 | 11/13 | 0.6867/0.4400/0.1900 | 14.7100 | 0.2080 | 0.6697 |

## 2. KPI Stability Matrix

| KPI | Mean Actual | Pass Runs | Target |
|---|---:|---:|---:|
| WIN_RATE_EASY | 0.6756 | 3/3 | [0.6500, 0.8000] |
| WIN_RATE_STANDARD | 0.4758 | 2/3 | [0.4500, 0.5500] |
| WIN_RATE_HARD | 0.2411 | 2/3 | [0.2000, 0.3500] |
| WIN_RATE_OVERALL | 0.4653 | 3/3 | [0.4000, 0.6000] |
| AVG_END_TURN_OVERALL | 14.3427 | 3/3 | [14.0000, 20.0000] |
| AVG_END_TURN_STANDARD | 15.3317 | 2/3 | [15.0000, 19.0000] |
| ATTRITION_RATE_OVERALL | 0.2003 | 3/3 | [0.1000, 0.3500] |
| RESOURCE_CV_GOLDLEAF | 0.4130 | 3/3 | [0.1800, 0.4500] |
| RESOURCE_CV_FIREOIL | 0.1723 | 3/3 | [0.1500, 0.4000] |
| RESOURCE_CV_ARMS | 0.2510 | 3/3 | [0.2000, 0.5500] |
| VICTORY_SHARE_OVERALL | 0.4653 | 3/3 | [0.3500, 0.6500] |
| DEFEAT_SHARE_OVERALL | 0.3343 | 1/3 | [0.3500, 0.6500] |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.6712 | 3/3 | [0.0000, 0.7000] |

## 3. Gate Decision Rule

- Rule: all hard-gate KPIs must pass in all validation runs.
- Result: `FAIL`
- Failed KPI IDs: WIN_RATE_STANDARD, WIN_RATE_HARD, AVG_END_TURN_STANDARD, DEFEAT_SHARE_OVERALL

## 4. Acceptance Conclusion

- R3-05 validation does not meet KPI gates. Keep R3 in `not ready` state and continue targeted tuning.
