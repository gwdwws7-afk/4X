# R3-05 Final Balance Report

- Stage: R3 Balance and AI Tuning
- Task: R3-05 balance regression validation
- Date: 2026-04-20
- Validation base profile: `R12-winrate-recover-diversify`
- Verdict: `FAIL`

## 1. Run-Level KPI Score

| Run | Batch | KPI Pass | Win(E/S/H) | AvgEndTurn | Attrition | PathMonopoly |
|---|---|---:|---:|---:|---:|---:|
| V01 | R3-05-B002-R12-V01 | 6/13 | 0.1533/0.5250/0.3933 | 14.3600 | 0.2500 | 0.8235 |
| V02 | R3-05-B002-R12-V02 | 6/13 | 0.1433/0.5350/0.3533 | 14.4960 | 0.2530 | 0.8430 |
| V03 | R3-05-B002-R12-V03 | 7/13 | 0.1700/0.5025/0.3367 | 14.7830 | 0.2560 | 0.8555 |

## 2. KPI Stability Matrix

| KPI | Mean Actual | Pass Runs | Target |
|---|---:|---:|---:|
| WIN_RATE_EASY | 0.1556 | 0/3 | [0.6500, 0.8000] |
| WIN_RATE_STANDARD | 0.5208 | 3/3 | [0.4500, 0.5500] |
| WIN_RATE_HARD | 0.3611 | 1/3 | [0.2000, 0.3500] |
| WIN_RATE_OVERALL | 0.3633 | 0/3 | [0.4000, 0.6000] |
| AVG_END_TURN_OVERALL | 14.5463 | 3/3 | [14.0000, 20.0000] |
| AVG_END_TURN_STANDARD | 13.8975 | 0/3 | [15.0000, 19.0000] |
| ATTRITION_RATE_OVERALL | 0.2530 | 3/3 | [0.1000, 0.3500] |
| RESOURCE_CV_GOLDLEAF | 0.2642 | 3/3 | [0.1800, 0.4500] |
| RESOURCE_CV_FIREOIL | 0.1272 | 0/3 | [0.1500, 0.4000] |
| RESOURCE_CV_ARMS | 0.1525 | 0/3 | [0.2000, 0.5500] |
| VICTORY_SHARE_OVERALL | 0.3633 | 3/3 | [0.3500, 0.6500] |
| DEFEAT_SHARE_OVERALL | 0.3837 | 3/3 | [0.3500, 0.6500] |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.8407 | 0/3 | [0.0000, 0.7000] |

## 3. Gate Decision Rule

- Rule: all hard-gate KPIs must pass in all validation runs.
- Result: `FAIL`
- Failed KPI IDs: WIN_RATE_EASY, WIN_RATE_HARD, WIN_RATE_OVERALL, AVG_END_TURN_STANDARD, RESOURCE_CV_FIREOIL, RESOURCE_CV_ARMS, SINGLE_PATH_VICTORY_MONOPOLY

## 4. Acceptance Conclusion

- R3-05 validation does not meet KPI gates. Keep R3 in `not ready` state and continue targeted tuning.
