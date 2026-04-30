# R3-05 Final Balance Report

- Stage: R3 Balance and AI Tuning
- Task: R3-05 balance regression validation
- Date: 2026-04-20
- Validation base profile: `R14-duration-volatility`
- Verdict: `FAIL`

## 1. Run-Level KPI Score

| Run | Batch | KPI Pass | Win(E/S/H) | AvgEndTurn | Attrition | PathMonopoly |
|---|---|---:|---:|---:|---:|---:|
| V01 | R3-05-B002-R14-V01 | 7/13 | 0.3667/0.0625/0.0300 | 14.5170 | 0.2580 | 1.0000 |
| V02 | R3-05-B002-R14-V02 | 7/13 | 0.3067/0.0650/0.0300 | 15.1150 | 0.2760 | 1.0000 |
| V03 | R3-05-B002-R14-V03 | 7/13 | 0.3300/0.0525/0.0300 | 14.7100 | 0.2690 | 1.0000 |

## 2. KPI Stability Matrix

| KPI | Mean Actual | Pass Runs | Target |
|---|---:|---:|---:|
| WIN_RATE_EASY | 0.3344 | 0/3 | [0.6500, 0.8000] |
| WIN_RATE_STANDARD | 0.0600 | 0/3 | [0.4500, 0.5500] |
| WIN_RATE_HARD | 0.0300 | 0/3 | [0.2000, 0.3500] |
| WIN_RATE_OVERALL | 0.1333 | 0/3 | [0.4000, 0.6000] |
| AVG_END_TURN_OVERALL | 14.7807 | 3/3 | [14.0000, 20.0000] |
| AVG_END_TURN_STANDARD | 17.7558 | 3/3 | [15.0000, 19.0000] |
| ATTRITION_RATE_OVERALL | 0.2677 | 3/3 | [0.1000, 0.3500] |
| RESOURCE_CV_GOLDLEAF | 0.3991 | 3/3 | [0.1800, 0.4500] |
| RESOURCE_CV_FIREOIL | 0.1782 | 3/3 | [0.1500, 0.4000] |
| RESOURCE_CV_ARMS | 0.2386 | 3/3 | [0.2000, 0.5500] |
| VICTORY_SHARE_OVERALL | 0.1333 | 0/3 | [0.3500, 0.6500] |
| DEFEAT_SHARE_OVERALL | 0.5990 | 3/3 | [0.3500, 0.6500] |
| SINGLE_PATH_VICTORY_MONOPOLY | 1.0000 | 0/3 | [0.0000, 0.7000] |

## 3. Gate Decision Rule

- Rule: all hard-gate KPIs must pass in all validation runs.
- Result: `FAIL`
- Failed KPI IDs: WIN_RATE_EASY, WIN_RATE_STANDARD, WIN_RATE_HARD, WIN_RATE_OVERALL, VICTORY_SHARE_OVERALL, SINGLE_PATH_VICTORY_MONOPOLY

## 4. Acceptance Conclusion

- R3-05 validation does not meet KPI gates. Keep R3 in `not ready` state and continue targeted tuning.
