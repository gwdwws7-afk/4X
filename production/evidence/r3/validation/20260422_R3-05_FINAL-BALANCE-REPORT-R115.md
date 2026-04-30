# R3-05 Final Balance Report

- Stage: R3 Balance and AI Tuning
- Task: R3-05 balance regression validation
- Date: 2026-04-22
- Validation base profile: `R115-r112-hard-recover-floor-a`
- Verdict: `FAIL`

## 1. Run-Level KPI Score

| Run | Batch | KPI Pass | Win(E/S/H) | AvgEndTurn | Attrition | PathMonopoly |
|---|---|---:|---:|---:|---:|---:|
| V01 | R3-05-B002-R115-V01 | 10/13 | 0.6433/0.5150/0.2600 | 13.9060 | 0.1940 | 0.6730 |
| V02 | R3-05-B002-R115-V02 | 11/13 | 0.5533/0.4900/0.3200 | 14.4540 | 0.2280 | 0.6528 |
| V03 | R3-05-B002-R115-V03 | 11/13 | 0.6233/0.4650/0.2433 | 14.3810 | 0.2070 | 0.6614 |

## 2. KPI Stability Matrix

| KPI | Mean Actual | Pass Runs | Target |
|---|---:|---:|---:|
| WIN_RATE_EASY | 0.6067 | 0/3 | [0.6500, 0.8000] |
| WIN_RATE_STANDARD | 0.4900 | 3/3 | [0.4500, 0.5500] |
| WIN_RATE_HARD | 0.2744 | 3/3 | [0.2000, 0.3500] |
| WIN_RATE_OVERALL | 0.4603 | 3/3 | [0.4000, 0.6000] |
| AVG_END_TURN_OVERALL | 14.2470 | 2/3 | [14.0000, 20.0000] |
| AVG_END_TURN_STANDARD | 15.3292 | 3/3 | [15.0000, 19.0000] |
| ATTRITION_RATE_OVERALL | 0.2097 | 3/3 | [0.1000, 0.3500] |
| RESOURCE_CV_GOLDLEAF | 0.3952 | 3/3 | [0.1800, 0.4500] |
| RESOURCE_CV_FIREOIL | 0.1836 | 3/3 | [0.1500, 0.4000] |
| RESOURCE_CV_ARMS | 0.2624 | 3/3 | [0.2000, 0.5500] |
| VICTORY_SHARE_OVERALL | 0.4603 | 3/3 | [0.3500, 0.6500] |
| DEFEAT_SHARE_OVERALL | 0.3300 | 0/3 | [0.3500, 0.6500] |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.6624 | 3/3 | [0.0000, 0.7000] |

## 3. Gate Decision Rule

- Rule: all hard-gate KPIs must pass in all validation runs.
- Result: `FAIL`
- Failed KPI IDs: WIN_RATE_EASY, AVG_END_TURN_OVERALL, DEFEAT_SHARE_OVERALL

## 4. Acceptance Conclusion

- R3-05 validation does not meet KPI gates. Keep R3 in `not ready` state and continue targeted tuning.
