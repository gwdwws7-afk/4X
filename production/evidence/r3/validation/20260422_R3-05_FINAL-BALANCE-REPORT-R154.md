# R3-05 Final Balance Report

- Stage: R3 Balance and AI Tuning
- Task: R3-05 balance regression validation
- Date: 2026-04-22
- Validation base profile: `R154-r151-r305-gap-close-a`
- Verdict: `FAIL`

## 1. Run-Level KPI Score

| Run | Batch | KPI Pass | Win(E/S/H) | AvgEndTurn | Attrition | PathMonopoly |
|---|---|---:|---:|---:|---:|---:|
| V01 | R3-05-B002-R154-V01 | 10/13 | 0.6733/0.5225/0.1900 | 13.9450 | 0.1850 | 0.6731 |
| V02 | R3-05-B002-R154-V02 | 11/13 | 0.6067/0.4925/0.2467 | 14.4530 | 0.2140 | 0.6424 |
| V03 | R3-05-B002-R154-V03 | 12/13 | 0.6300/0.4650/0.2000 | 14.3300 | 0.2090 | 0.6529 |

## 2. KPI Stability Matrix

| KPI | Mean Actual | Pass Runs | Target |
|---|---:|---:|---:|
| WIN_RATE_EASY | 0.6367 | 1/3 | [0.6500, 0.8000] |
| WIN_RATE_STANDARD | 0.4933 | 3/3 | [0.4500, 0.5500] |
| WIN_RATE_HARD | 0.2122 | 2/3 | [0.2000, 0.3500] |
| WIN_RATE_OVERALL | 0.4520 | 3/3 | [0.4000, 0.6000] |
| AVG_END_TURN_OVERALL | 14.2427 | 2/3 | [14.0000, 20.0000] |
| AVG_END_TURN_STANDARD | 15.4583 | 3/3 | [15.0000, 19.0000] |
| ATTRITION_RATE_OVERALL | 0.2027 | 3/3 | [0.1000, 0.3500] |
| RESOURCE_CV_GOLDLEAF | 0.4142 | 3/3 | [0.1800, 0.4500] |
| RESOURCE_CV_FIREOIL | 0.1852 | 3/3 | [0.1500, 0.4000] |
| RESOURCE_CV_ARMS | 0.2688 | 3/3 | [0.2000, 0.5500] |
| VICTORY_SHARE_OVERALL | 0.4520 | 3/3 | [0.3500, 0.6500] |
| DEFEAT_SHARE_OVERALL | 0.3453 | 1/3 | [0.3500, 0.6500] |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.6561 | 3/3 | [0.0000, 0.7000] |

## 3. Gate Decision Rule

- Rule: all hard-gate KPIs must pass in all validation runs.
- Result: `FAIL`
- Failed KPI IDs: WIN_RATE_EASY, WIN_RATE_HARD, AVG_END_TURN_OVERALL, DEFEAT_SHARE_OVERALL

## 4. Acceptance Conclusion

- R3-05 validation does not meet KPI gates. Keep R3 in `not ready` state and continue targeted tuning.
