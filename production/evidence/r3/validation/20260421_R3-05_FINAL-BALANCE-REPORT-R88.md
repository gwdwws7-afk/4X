# R3-05 Final Balance Report

- Stage: R3 Balance and AI Tuning
- Task: R3-05 balance regression validation
- Date: 2026-04-21
- Validation base profile: `R88-r82-stability-pace-a`
- Verdict: `FAIL`

## 1. Run-Level KPI Score

| Run | Batch | KPI Pass | Win(E/S/H) | AvgEndTurn | Attrition | PathMonopoly |
|---|---|---:|---:|---:|---:|---:|
| V01 | R3-05-B002-R88-V01 | 9/13 | 0.6100/0.4975/0.2433 | 13.9560 | 0.1980 | 0.7055 |
| V02 | R3-05-B002-R88-V02 | 11/13 | 0.5567/0.4550/0.2800 | 14.5460 | 0.2390 | 0.6975 |
| V03 | R3-05-B002-R88-V03 | 11/13 | 0.5400/0.4150/0.2467 | 14.8550 | 0.2370 | 0.6965 |

## 2. KPI Stability Matrix

| KPI | Mean Actual | Pass Runs | Target |
|---|---:|---:|---:|
| WIN_RATE_EASY | 0.5689 | 0/3 | [0.6500, 0.8000] |
| WIN_RATE_STANDARD | 0.4558 | 2/3 | [0.4500, 0.5500] |
| WIN_RATE_HARD | 0.2567 | 3/3 | [0.2000, 0.3500] |
| WIN_RATE_OVERALL | 0.4300 | 3/3 | [0.4000, 0.6000] |
| AVG_END_TURN_OVERALL | 14.4523 | 2/3 | [14.0000, 20.0000] |
| AVG_END_TURN_STANDARD | 15.8167 | 3/3 | [15.0000, 19.0000] |
| ATTRITION_RATE_OVERALL | 0.2247 | 3/3 | [0.1000, 0.3500] |
| RESOURCE_CV_GOLDLEAF | 0.3672 | 3/3 | [0.1800, 0.4500] |
| RESOURCE_CV_FIREOIL | 0.1900 | 3/3 | [0.1500, 0.4000] |
| RESOURCE_CV_ARMS | 0.2617 | 3/3 | [0.2000, 0.5500] |
| VICTORY_SHARE_OVERALL | 0.4300 | 3/3 | [0.3500, 0.6500] |
| DEFEAT_SHARE_OVERALL | 0.3453 | 1/3 | [0.3500, 0.6500] |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.6998 | 2/3 | [0.0000, 0.7000] |

## 3. Gate Decision Rule

- Rule: all hard-gate KPIs must pass in all validation runs.
- Result: `FAIL`
- Failed KPI IDs: WIN_RATE_EASY, WIN_RATE_STANDARD, AVG_END_TURN_OVERALL, DEFEAT_SHARE_OVERALL, SINGLE_PATH_VICTORY_MONOPOLY

## 4. Acceptance Conclusion

- R3-05 validation does not meet KPI gates. Keep R3 in `not ready` state and continue targeted tuning.
