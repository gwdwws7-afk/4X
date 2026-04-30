# R3-05 Final Balance Report

- Stage: R3 Balance and AI Tuning
- Task: R3-05 balance regression validation
- Date: 2026-04-22
- Validation base profile: `R112-r106-hard-pace-standard-floor-a`
- Verdict: `FAIL`

## 1. Run-Level KPI Score

| Run | Batch | KPI Pass | Win(E/S/H) | AvgEndTurn | Attrition | PathMonopoly |
|---|---|---:|---:|---:|---:|---:|
| V01 | R3-05-B002-R112-V01 | 10/13 | 0.7000/0.5125/0.1600 | 13.8120 | 0.1850 | 0.7041 |
| V02 | R3-05-B002-R112-V02 | 11/13 | 0.6400/0.4825/0.2000 | 14.3230 | 0.2080 | 0.6787 |
| V03 | R3-05-B002-R112-V03 | 12/13 | 0.7100/0.4650/0.1500 | 14.2510 | 0.1920 | 0.6982 |

## 2. KPI Stability Matrix

| KPI | Mean Actual | Pass Runs | Target |
|---|---:|---:|---:|
| WIN_RATE_EASY | 0.6833 | 2/3 | [0.6500, 0.8000] |
| WIN_RATE_STANDARD | 0.4867 | 3/3 | [0.4500, 0.5500] |
| WIN_RATE_HARD | 0.1700 | 1/3 | [0.2000, 0.3500] |
| WIN_RATE_OVERALL | 0.4507 | 3/3 | [0.4000, 0.6000] |
| AVG_END_TURN_OVERALL | 14.1287 | 2/3 | [14.0000, 20.0000] |
| AVG_END_TURN_STANDARD | 15.4417 | 3/3 | [15.0000, 19.0000] |
| ATTRITION_RATE_OVERALL | 0.1950 | 3/3 | [0.1000, 0.3500] |
| RESOURCE_CV_GOLDLEAF | 0.3942 | 3/3 | [0.1800, 0.4500] |
| RESOURCE_CV_FIREOIL | 0.1854 | 3/3 | [0.1500, 0.4000] |
| RESOURCE_CV_ARMS | 0.2609 | 3/3 | [0.2000, 0.5500] |
| VICTORY_SHARE_OVERALL | 0.4507 | 3/3 | [0.3500, 0.6500] |
| DEFEAT_SHARE_OVERALL | 0.3543 | 2/3 | [0.3500, 0.6500] |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.6937 | 2/3 | [0.0000, 0.7000] |

## 3. Gate Decision Rule

- Rule: all hard-gate KPIs must pass in all validation runs.
- Result: `FAIL`
- Failed KPI IDs: WIN_RATE_EASY, WIN_RATE_HARD, AVG_END_TURN_OVERALL, DEFEAT_SHARE_OVERALL, SINGLE_PATH_VICTORY_MONOPOLY

## 4. Acceptance Conclusion

- R3-05 validation does not meet KPI gates. Keep R3 in `not ready` state and continue targeted tuning.
