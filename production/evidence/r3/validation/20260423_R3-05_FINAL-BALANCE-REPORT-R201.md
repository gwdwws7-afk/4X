# R3-05 Final Balance Report

- Stage: R3 Balance and AI Tuning
- Task: R3-05 balance regression validation
- Date: 2026-04-23
- Validation base profile: `R201-r198-hard-survival-c`
- Verdict: `FAIL`

## 1. Run-Level KPI Score

| Run | Batch | KPI Pass | Win(E/S/H) | AvgEndTurn | Attrition | PathMonopoly |
|---|---|---:|---:|---:|---:|---:|
| V01 | R3-05-B002-R201-V01 | 13/13 | 0.6533/0.4775/0.2067 | 14.2960 | 0.1920 | 0.6815 |
| V02 | R3-05-B002-R201-V02 | 12/13 | 0.6800/0.4975/0.2967 | 14.1780 | 0.1880 | 0.6362 |
| V03 | R3-05-B002-R201-V03 | 12/13 | 0.6800/0.4625/0.1867 | 14.2490 | 0.1820 | 0.6292 |

## 2. KPI Stability Matrix

| KPI | Mean Actual | Pass Runs | Target |
|---|---:|---:|---:|
| WIN_RATE_EASY | 0.6711 | 3/3 | [0.6500, 0.8000] |
| WIN_RATE_STANDARD | 0.4792 | 3/3 | [0.4500, 0.5500] |
| WIN_RATE_HARD | 0.2300 | 2/3 | [0.2000, 0.3500] |
| WIN_RATE_OVERALL | 0.4620 | 3/3 | [0.4000, 0.6000] |
| AVG_END_TURN_OVERALL | 14.2410 | 3/3 | [14.0000, 20.0000] |
| AVG_END_TURN_STANDARD | 15.3575 | 3/3 | [15.0000, 19.0000] |
| ATTRITION_RATE_OVERALL | 0.1873 | 3/3 | [0.1000, 0.3500] |
| RESOURCE_CV_GOLDLEAF | 0.4213 | 3/3 | [0.1800, 0.4500] |
| RESOURCE_CV_FIREOIL | 0.1838 | 3/3 | [0.1500, 0.4000] |
| RESOURCE_CV_ARMS | 0.2719 | 3/3 | [0.2000, 0.5500] |
| VICTORY_SHARE_OVERALL | 0.4620 | 3/3 | [0.3500, 0.6500] |
| DEFEAT_SHARE_OVERALL | 0.3507 | 2/3 | [0.3500, 0.6500] |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.6490 | 3/3 | [0.0000, 0.7000] |

## 3. Gate Decision Rule

- Rule: all hard-gate KPIs must pass in all validation runs.
- Result: `FAIL`
- Failed KPI IDs: WIN_RATE_HARD, DEFEAT_SHARE_OVERALL

## 4. Acceptance Conclusion

- R3-05 validation does not meet KPI gates. Keep R3 in `not ready` state and continue targeted tuning.
