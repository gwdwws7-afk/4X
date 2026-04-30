# R3-05 Final Balance Report

- Stage: R3 Balance and AI Tuning
- Task: R3-05 balance regression validation
- Date: 2026-04-23
- Validation base profile: `R306-r296-variance-compress-d`
- Verdict: `FAIL`

## 1. Run-Level KPI Score

| Run | Batch | KPI Pass | Win(E/S/H) | AvgEndTurn | Attrition | PathMonopoly |
|---|---|---:|---:|---:|---:|---:|
| V01 | R3-05-B002-R306-V01 | 12/13 | 0.6667/0.4625/0.2100 | 14.4780 | 0.2070 | 0.6473 |
| V02 | R3-05-B002-R306-V02 | 12/13 | 0.6600/0.4925/0.2867 | 14.5440 | 0.1910 | 0.6445 |
| V03 | R3-05-B002-R306-V03 | 12/13 | 0.6533/0.4425/0.2200 | 14.5560 | 0.2010 | 0.6469 |

## 2. KPI Stability Matrix

| KPI | Mean Actual | Pass Runs | Target |
|---|---:|---:|---:|
| WIN_RATE_EASY | 0.6600 | 3/3 | [0.6500, 0.8000] |
| WIN_RATE_STANDARD | 0.4658 | 2/3 | [0.4500, 0.5500] |
| WIN_RATE_HARD | 0.2389 | 3/3 | [0.2000, 0.3500] |
| WIN_RATE_OVERALL | 0.4560 | 3/3 | [0.4000, 0.6000] |
| AVG_END_TURN_OVERALL | 14.5260 | 3/3 | [14.0000, 20.0000] |
| AVG_END_TURN_STANDARD | 15.3800 | 3/3 | [15.0000, 19.0000] |
| ATTRITION_RATE_OVERALL | 0.1997 | 3/3 | [0.1000, 0.3500] |
| RESOURCE_CV_GOLDLEAF | 0.4167 | 3/3 | [0.1800, 0.4500] |
| RESOURCE_CV_FIREOIL | 0.1830 | 3/3 | [0.1500, 0.4000] |
| RESOURCE_CV_ARMS | 0.2706 | 3/3 | [0.2000, 0.5500] |
| VICTORY_SHARE_OVERALL | 0.4560 | 3/3 | [0.3500, 0.6500] |
| DEFEAT_SHARE_OVERALL | 0.3443 | 1/3 | [0.3500, 0.6500] |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.6462 | 3/3 | [0.0000, 0.7000] |

## 3. Gate Decision Rule

- Rule: all hard-gate KPIs must pass in all validation runs.
- Result: `FAIL`
- Failed KPI IDs: WIN_RATE_STANDARD, DEFEAT_SHARE_OVERALL

## 4. Acceptance Conclusion

- R3-05 validation does not meet KPI gates. Keep R3 in `not ready` state and continue targeted tuning.
