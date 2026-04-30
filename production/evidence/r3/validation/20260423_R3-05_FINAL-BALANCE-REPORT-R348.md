# R3-05 Final Balance Report

- Stage: R3 Balance and AI Tuning
- Task: R3-05 balance regression validation
- Date: 2026-04-23
- Validation base profile: `R348-auto-grid-r311`
- Verdict: `FAIL`

## 1. Run-Level KPI Score

| Run | Batch | KPI Pass | Win(E/S/H) | AvgEndTurn | Attrition | PathMonopoly |
|---|---|---:|---:|---:|---:|---:|
| V01 | R3-05-B002-R348-V01 | 13/13 | 0.6767/0.4550/0.2167 | 14.3570 | 0.2000 | 0.6578 |
| V02 | R3-05-B002-R348-V02 | 11/13 | 0.6433/0.4950/0.2900 | 14.5150 | 0.1940 | 0.6402 |
| V03 | R3-05-B002-R348-V03 | 10/13 | 0.6433/0.4450/0.2300 | 14.6290 | 0.2130 | 0.6295 |

## 2. KPI Stability Matrix

| KPI | Mean Actual | Pass Runs | Target |
|---|---:|---:|---:|
| WIN_RATE_EASY | 0.6544 | 1/3 | [0.6500, 0.8000] |
| WIN_RATE_STANDARD | 0.4650 | 2/3 | [0.4500, 0.5500] |
| WIN_RATE_HARD | 0.2456 | 3/3 | [0.2000, 0.3500] |
| WIN_RATE_OVERALL | 0.4560 | 3/3 | [0.4000, 0.6000] |
| AVG_END_TURN_OVERALL | 14.5003 | 3/3 | [14.0000, 20.0000] |
| AVG_END_TURN_STANDARD | 15.3750 | 3/3 | [15.0000, 19.0000] |
| ATTRITION_RATE_OVERALL | 0.2023 | 3/3 | [0.1000, 0.3500] |
| RESOURCE_CV_GOLDLEAF | 0.4159 | 3/3 | [0.1800, 0.4500] |
| RESOURCE_CV_FIREOIL | 0.1843 | 3/3 | [0.1500, 0.4000] |
| RESOURCE_CV_ARMS | 0.2682 | 3/3 | [0.2000, 0.5500] |
| VICTORY_SHARE_OVERALL | 0.4560 | 3/3 | [0.3500, 0.6500] |
| DEFEAT_SHARE_OVERALL | 0.3417 | 1/3 | [0.3500, 0.6500] |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.6425 | 3/3 | [0.0000, 0.7000] |

## 3. Gate Decision Rule

- Rule: all hard-gate KPIs must pass in all validation runs.
- Result: `FAIL`
- Failed KPI IDs: WIN_RATE_EASY, WIN_RATE_STANDARD, DEFEAT_SHARE_OVERALL

## 4. Acceptance Conclusion

- R3-05 validation does not meet KPI gates. Keep R3 in `not ready` state and continue targeted tuning.
