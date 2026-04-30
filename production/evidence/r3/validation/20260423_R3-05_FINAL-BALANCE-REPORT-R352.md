# R3-05 Final Balance Report

- Stage: R3 Balance and AI Tuning
- Task: R3-05 balance regression validation
- Date: 2026-04-23
- Validation base profile: `R352-auto-grid-r311`
- Verdict: `FAIL`

## 1. Run-Level KPI Score

| Run | Batch | KPI Pass | Win(E/S/H) | AvgEndTurn | Attrition | PathMonopoly |
|---|---|---:|---:|---:|---:|---:|
| V01 | R3-05-B002-R352-V01 | 13/13 | 0.6767/0.4550/0.2067 | 14.3900 | 0.2000 | 0.6644 |
| V02 | R3-05-B002-R352-V02 | 11/13 | 0.6433/0.4925/0.2800 | 14.5560 | 0.1940 | 0.6456 |
| V03 | R3-05-B002-R352-V03 | 10/13 | 0.6433/0.4450/0.2267 | 14.6360 | 0.2130 | 0.6310 |

## 2. KPI Stability Matrix

| KPI | Mean Actual | Pass Runs | Target |
|---|---:|---:|---:|
| WIN_RATE_EASY | 0.6544 | 1/3 | [0.6500, 0.8000] |
| WIN_RATE_STANDARD | 0.4642 | 2/3 | [0.4500, 0.5500] |
| WIN_RATE_HARD | 0.2378 | 3/3 | [0.2000, 0.3500] |
| WIN_RATE_OVERALL | 0.4533 | 3/3 | [0.4000, 0.6000] |
| AVG_END_TURN_OVERALL | 14.5273 | 3/3 | [14.0000, 20.0000] |
| AVG_END_TURN_STANDARD | 15.3892 | 3/3 | [15.0000, 19.0000] |
| ATTRITION_RATE_OVERALL | 0.2023 | 3/3 | [0.1000, 0.3500] |
| RESOURCE_CV_GOLDLEAF | 0.4171 | 3/3 | [0.1800, 0.4500] |
| RESOURCE_CV_FIREOIL | 0.1846 | 3/3 | [0.1500, 0.4000] |
| RESOURCE_CV_ARMS | 0.2686 | 3/3 | [0.2000, 0.5500] |
| VICTORY_SHARE_OVERALL | 0.4533 | 3/3 | [0.3500, 0.6500] |
| DEFEAT_SHARE_OVERALL | 0.3443 | 1/3 | [0.3500, 0.6500] |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.6470 | 3/3 | [0.0000, 0.7000] |

## 3. Gate Decision Rule

- Rule: all hard-gate KPIs must pass in all validation runs.
- Result: `FAIL`
- Failed KPI IDs: WIN_RATE_EASY, WIN_RATE_STANDARD, DEFEAT_SHARE_OVERALL

## 4. Acceptance Conclusion

- R3-05 validation does not meet KPI gates. Keep R3 in `not ready` state and continue targeted tuning.
