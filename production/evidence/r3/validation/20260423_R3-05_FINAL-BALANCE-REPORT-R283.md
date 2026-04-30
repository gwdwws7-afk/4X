# R3-05 Final Balance Report

- Stage: R3 Balance and AI Tuning
- Task: R3-05 balance regression validation
- Date: 2026-04-23
- Validation base profile: `R283-r276-defeat-lift-f`
- Verdict: `FAIL`

## 1. Run-Level KPI Score

| Run | Batch | KPI Pass | Win(E/S/H) | AvgEndTurn | Attrition | PathMonopoly |
|---|---|---:|---:|---:|---:|---:|
| V01 | R3-05-B002-R283-V01 | 13/13 | 0.6500/0.4700/0.2267 | 14.2320 | 0.1940 | 0.6807 |
| V02 | R3-05-B002-R283-V02 | 11/13 | 0.6733/0.4900/0.3100 | 14.1410 | 0.1860 | 0.6415 |
| V03 | R3-05-B002-R283-V03 | 12/13 | 0.6700/0.4625/0.1967 | 14.2460 | 0.1820 | 0.6225 |

## 2. KPI Stability Matrix

| KPI | Mean Actual | Pass Runs | Target |
|---|---:|---:|---:|
| WIN_RATE_EASY | 0.6644 | 3/3 | [0.6500, 0.8000] |
| WIN_RATE_STANDARD | 0.4742 | 3/3 | [0.4500, 0.5500] |
| WIN_RATE_HARD | 0.2444 | 2/3 | [0.2000, 0.3500] |
| WIN_RATE_OVERALL | 0.4623 | 3/3 | [0.4000, 0.6000] |
| AVG_END_TURN_OVERALL | 14.2063 | 3/3 | [14.0000, 20.0000] |
| AVG_END_TURN_STANDARD | 15.1067 | 2/3 | [15.0000, 19.0000] |
| ATTRITION_RATE_OVERALL | 0.1873 | 3/3 | [0.1000, 0.3500] |
| RESOURCE_CV_GOLDLEAF | 0.4178 | 3/3 | [0.1800, 0.4500] |
| RESOURCE_CV_FIREOIL | 0.1834 | 3/3 | [0.1500, 0.4000] |
| RESOURCE_CV_ARMS | 0.2715 | 3/3 | [0.2000, 0.5500] |
| VICTORY_SHARE_OVERALL | 0.4623 | 3/3 | [0.3500, 0.6500] |
| DEFEAT_SHARE_OVERALL | 0.3503 | 2/3 | [0.3500, 0.6500] |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.6482 | 3/3 | [0.0000, 0.7000] |

## 3. Gate Decision Rule

- Rule: all hard-gate KPIs must pass in all validation runs.
- Result: `FAIL`
- Failed KPI IDs: WIN_RATE_HARD, AVG_END_TURN_STANDARD, DEFEAT_SHARE_OVERALL

## 4. Acceptance Conclusion

- R3-05 validation does not meet KPI gates. Keep R3 in `not ready` state and continue targeted tuning.
