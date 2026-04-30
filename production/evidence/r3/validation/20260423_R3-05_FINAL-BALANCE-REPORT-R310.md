# R3-05 Final Balance Report

- Stage: R3 Balance and AI Tuning
- Task: R3-05 balance regression validation
- Date: 2026-04-23
- Validation base profile: `R310-r306-standard-recover-hard-pressure-d`
- Verdict: `FAIL`

## 1. Run-Level KPI Score

| Run | Batch | KPI Pass | Win(E/S/H) | AvgEndTurn | Attrition | PathMonopoly |
|---|---|---:|---:|---:|---:|---:|
| V01 | R3-05-B002-R310-V01 | 12/13 | 0.6567/0.4700/0.2267 | 14.3330 | 0.2080 | 0.6512 |
| V02 | R3-05-B002-R310-V02 | 12/13 | 0.6600/0.5050/0.2967 | 14.3610 | 0.1860 | 0.6380 |
| V03 | R3-05-B002-R310-V03 | 13/13 | 0.6567/0.4650/0.2300 | 14.3210 | 0.1920 | 0.6283 |

## 2. KPI Stability Matrix

| KPI | Mean Actual | Pass Runs | Target |
|---|---:|---:|---:|
| WIN_RATE_EASY | 0.6578 | 3/3 | [0.6500, 0.8000] |
| WIN_RATE_STANDARD | 0.4800 | 3/3 | [0.4500, 0.5500] |
| WIN_RATE_HARD | 0.2511 | 3/3 | [0.2000, 0.3500] |
| WIN_RATE_OVERALL | 0.4647 | 3/3 | [0.4000, 0.6000] |
| AVG_END_TURN_OVERALL | 14.3383 | 3/3 | [14.0000, 20.0000] |
| AVG_END_TURN_STANDARD | 15.1867 | 3/3 | [15.0000, 19.0000] |
| ATTRITION_RATE_OVERALL | 0.1953 | 3/3 | [0.1000, 0.3500] |
| RESOURCE_CV_GOLDLEAF | 0.4139 | 3/3 | [0.1800, 0.4500] |
| RESOURCE_CV_FIREOIL | 0.1824 | 3/3 | [0.1500, 0.4000] |
| RESOURCE_CV_ARMS | 0.2699 | 3/3 | [0.2000, 0.5500] |
| VICTORY_SHARE_OVERALL | 0.4647 | 3/3 | [0.3500, 0.6500] |
| DEFEAT_SHARE_OVERALL | 0.3400 | 1/3 | [0.3500, 0.6500] |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.6392 | 3/3 | [0.0000, 0.7000] |

## 3. Gate Decision Rule

- Rule: all hard-gate KPIs must pass in all validation runs.
- Result: `FAIL`
- Failed KPI IDs: DEFEAT_SHARE_OVERALL

## 4. Acceptance Conclusion

- R3-05 validation does not meet KPI gates. Keep R3 in `not ready` state and continue targeted tuning.
