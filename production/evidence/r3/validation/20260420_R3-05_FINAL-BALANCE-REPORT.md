# R3-05 Final Balance Report

- Stage: R3 Balance and AI Tuning
- Task: R3-05 balance regression validation
- Date: 2026-04-20
- Validation base profile: `R3-04 R03 (target-seek)`
- Verdict: `FAIL`

## 1. Run-Level KPI Score

| Run | Batch | KPI Pass | Win(E/S/H) | AvgEndTurn | Attrition | PathMonopoly |
|---|---|---:|---:|---:|---:|---:|
| V01 | R3-05-B001-V01 | 4/13 | 0.9267/0.2925/0.3133 | 9.8450 | 0.0700 | 0.8896 |
| V02 | R3-05-B001-V02 | 4/13 | 0.9333/0.3175/0.3367 | 9.6890 | 0.0790 | 0.8957 |
| V03 | R3-05-B001-V03 | 4/13 | 0.9200/0.3075/0.2800 | 9.9270 | 0.0870 | 0.9089 |

## 2. KPI Stability Matrix

| KPI | Mean Actual | Pass Runs | Target |
|---|---:|---:|---:|
| WIN_RATE_EASY | 0.9267 | 0/3 | [0.6500, 0.8000] |
| WIN_RATE_STANDARD | 0.3058 | 0/3 | [0.4500, 0.5500] |
| WIN_RATE_HARD | 0.3100 | 3/3 | [0.2000, 0.3500] |
| WIN_RATE_OVERALL | 0.4933 | 3/3 | [0.4000, 0.6000] |
| AVG_END_TURN_OVERALL | 9.8203 | 0/3 | [14.0000, 20.0000] |
| AVG_END_TURN_STANDARD | 13.4942 | 0/3 | [15.0000, 19.0000] |
| ATTRITION_RATE_OVERALL | 0.0787 | 0/3 | [0.1000, 0.3500] |
| RESOURCE_CV_GOLDLEAF | 0.1347 | 0/3 | [0.1800, 0.4500] |
| RESOURCE_CV_FIREOIL | 0.0823 | 0/3 | [0.1500, 0.4000] |
| RESOURCE_CV_ARMS | 0.1023 | 0/3 | [0.2000, 0.5500] |
| VICTORY_SHARE_OVERALL | 0.4933 | 3/3 | [0.3500, 0.6500] |
| DEFEAT_SHARE_OVERALL | 0.4280 | 3/3 | [0.3500, 0.6500] |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.8980 | 0/3 | [0.0000, 0.7000] |

## 3. Gate Decision Rule

- Rule: all hard-gate KPIs must pass in all validation runs.
- Result: `FAIL`
- Failed KPI IDs: WIN_RATE_EASY, WIN_RATE_STANDARD, AVG_END_TURN_OVERALL, AVG_END_TURN_STANDARD, ATTRITION_RATE_OVERALL, RESOURCE_CV_GOLDLEAF, RESOURCE_CV_FIREOIL, RESOURCE_CV_ARMS, SINGLE_PATH_VICTORY_MONOPOLY

## 4. Acceptance Conclusion

- R3-05 validation does not meet KPI gates. Keep R3 in `not ready` state and continue targeted tuning.
