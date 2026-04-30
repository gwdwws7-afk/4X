# R3-05 Final Balance Report

- Stage: R3 Balance and AI Tuning
- Task: R3-05 balance regression validation
- Date: 2026-04-22
- Validation base profile: `R157-r154-r305-gap-close-a2`
- Verdict: `FAIL`

## 1. Run-Level KPI Score

| Run | Batch | KPI Pass | Win(E/S/H) | AvgEndTurn | Attrition | PathMonopoly |
|---|---|---:|---:|---:|---:|---:|
| V01 | R3-05-B002-R157-V01 | 11/13 | 0.6900/0.5100/0.2367 | 13.8450 | 0.1890 | 0.6598 |
| V02 | R3-05-B002-R157-V02 | 12/13 | 0.6667/0.4900/0.2967 | 14.3470 | 0.1990 | 0.6412 |
| V03 | R3-05-B002-R157-V03 | 12/13 | 0.6867/0.4600/0.2333 | 14.1730 | 0.1960 | 0.6500 |

## 2. KPI Stability Matrix

| KPI | Mean Actual | Pass Runs | Target |
|---|---:|---:|---:|
| WIN_RATE_EASY | 0.6811 | 3/3 | [0.6500, 0.8000] |
| WIN_RATE_STANDARD | 0.4867 | 3/3 | [0.4500, 0.5500] |
| WIN_RATE_HARD | 0.2556 | 3/3 | [0.2000, 0.3500] |
| WIN_RATE_OVERALL | 0.4757 | 3/3 | [0.4000, 0.6000] |
| AVG_END_TURN_OVERALL | 14.1217 | 2/3 | [14.0000, 20.0000] |
| AVG_END_TURN_STANDARD | 15.4975 | 3/3 | [15.0000, 19.0000] |
| ATTRITION_RATE_OVERALL | 0.1947 | 3/3 | [0.1000, 0.3500] |
| RESOURCE_CV_GOLDLEAF | 0.4021 | 3/3 | [0.1800, 0.4500] |
| RESOURCE_CV_FIREOIL | 0.1810 | 3/3 | [0.1500, 0.4000] |
| RESOURCE_CV_ARMS | 0.2616 | 3/3 | [0.2000, 0.5500] |
| VICTORY_SHARE_OVERALL | 0.4757 | 3/3 | [0.3500, 0.6500] |
| DEFEAT_SHARE_OVERALL | 0.3297 | 0/3 | [0.3500, 0.6500] |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.6503 | 3/3 | [0.0000, 0.7000] |

## 3. Gate Decision Rule

- Rule: all hard-gate KPIs must pass in all validation runs.
- Result: `FAIL`
- Failed KPI IDs: AVG_END_TURN_OVERALL, DEFEAT_SHARE_OVERALL

## 4. Acceptance Conclusion

- R3-05 validation does not meet KPI gates. Keep R3 in `not ready` state and continue targeted tuning.
