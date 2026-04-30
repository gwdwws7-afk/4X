# R3-05 Final Balance Report

- Stage: R3 Balance and AI Tuning
- Task: R3-05 balance regression validation
- Date: 2026-04-23
- Validation base profile: `R298-r293-late-defeat-balance-c`
- Verdict: `FAIL`

## 1. Run-Level KPI Score

| Run | Batch | KPI Pass | Win(E/S/H) | AvgEndTurn | Attrition | PathMonopoly |
|---|---|---:|---:|---:|---:|---:|
| V01 | R3-05-B002-R298-V01 | 12/13 | 0.6467/0.4775/0.2300 | 14.3160 | 0.1960 | 0.6784 |
| V02 | R3-05-B002-R298-V02 | 11/13 | 0.6767/0.4925/0.3200 | 14.2380 | 0.1920 | 0.6411 |
| V03 | R3-05-B002-R298-V03 | 13/13 | 0.6667/0.4650/0.2000 | 14.3340 | 0.1830 | 0.6211 |

## 2. KPI Stability Matrix

| KPI | Mean Actual | Pass Runs | Target |
|---|---:|---:|---:|
| WIN_RATE_EASY | 0.6633 | 2/3 | [0.6500, 0.8000] |
| WIN_RATE_STANDARD | 0.4783 | 3/3 | [0.4500, 0.5500] |
| WIN_RATE_HARD | 0.2500 | 3/3 | [0.2000, 0.3500] |
| WIN_RATE_OVERALL | 0.4653 | 3/3 | [0.4000, 0.6000] |
| AVG_END_TURN_OVERALL | 14.2960 | 3/3 | [14.0000, 20.0000] |
| AVG_END_TURN_STANDARD | 15.2033 | 2/3 | [15.0000, 19.0000] |
| ATTRITION_RATE_OVERALL | 0.1903 | 3/3 | [0.1000, 0.3500] |
| RESOURCE_CV_GOLDLEAF | 0.4201 | 3/3 | [0.1800, 0.4500] |
| RESOURCE_CV_FIREOIL | 0.1836 | 3/3 | [0.1500, 0.4000] |
| RESOURCE_CV_ARMS | 0.2723 | 3/3 | [0.2000, 0.5500] |
| VICTORY_SHARE_OVERALL | 0.4653 | 3/3 | [0.3500, 0.6500] |
| DEFEAT_SHARE_OVERALL | 0.3443 | 2/3 | [0.3500, 0.6500] |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.6469 | 3/3 | [0.0000, 0.7000] |

## 3. Gate Decision Rule

- Rule: all hard-gate KPIs must pass in all validation runs.
- Result: `FAIL`
- Failed KPI IDs: WIN_RATE_EASY, AVG_END_TURN_STANDARD, DEFEAT_SHARE_OVERALL

## 4. Acceptance Conclusion

- R3-05 validation does not meet KPI gates. Keep R3 in `not ready` state and continue targeted tuning.
