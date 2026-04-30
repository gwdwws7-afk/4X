# R3-05 Final Balance Report

- Stage: R3 Balance and AI Tuning
- Task: R3-05 balance regression validation
- Date: 2026-04-23
- Validation base profile: `R205-r204-variance-close-a`
- Verdict: `FAIL`

## 1. Run-Level KPI Score

| Run | Batch | KPI Pass | Win(E/S/H) | AvgEndTurn | Attrition | PathMonopoly |
|---|---|---:|---:|---:|---:|---:|
| V01 | R3-05-B002-R205-V01 | 11/13 | 0.5867/0.4700/0.2300 | 14.7600 | 0.2480 | 0.6674 |
| V02 | R3-05-B002-R205-V02 | 11/13 | 0.6033/0.4725/0.3033 | 14.7600 | 0.2280 | 0.6095 |
| V03 | R3-05-B002-R205-V03 | 9/13 | 0.5967/0.4325/0.1900 | 15.3730 | 0.2550 | 0.6161 |

## 2. KPI Stability Matrix

| KPI | Mean Actual | Pass Runs | Target |
|---|---:|---:|---:|
| WIN_RATE_EASY | 0.5956 | 0/3 | [0.6500, 0.8000] |
| WIN_RATE_STANDARD | 0.4583 | 2/3 | [0.4500, 0.5500] |
| WIN_RATE_HARD | 0.2411 | 2/3 | [0.2000, 0.3500] |
| WIN_RATE_OVERALL | 0.4343 | 3/3 | [0.4000, 0.6000] |
| AVG_END_TURN_OVERALL | 14.9643 | 3/3 | [14.0000, 20.0000] |
| AVG_END_TURN_STANDARD | 15.9217 | 3/3 | [15.0000, 19.0000] |
| ATTRITION_RATE_OVERALL | 0.2437 | 3/3 | [0.1000, 0.3500] |
| RESOURCE_CV_GOLDLEAF | 0.4006 | 3/3 | [0.1800, 0.4500] |
| RESOURCE_CV_FIREOIL | 0.1531 | 3/3 | [0.1500, 0.4000] |
| RESOURCE_CV_ARMS | 0.2165 | 3/3 | [0.2000, 0.5500] |
| VICTORY_SHARE_OVERALL | 0.4343 | 3/3 | [0.3500, 0.6500] |
| DEFEAT_SHARE_OVERALL | 0.3220 | 0/3 | [0.3500, 0.6500] |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.6310 | 3/3 | [0.0000, 0.7000] |

## 3. Gate Decision Rule

- Rule: all hard-gate KPIs must pass in all validation runs.
- Result: `FAIL`
- Failed KPI IDs: WIN_RATE_EASY, WIN_RATE_STANDARD, WIN_RATE_HARD, DEFEAT_SHARE_OVERALL

## 4. Acceptance Conclusion

- R3-05 validation does not meet KPI gates. Keep R3 in `not ready` state and continue targeted tuning.
