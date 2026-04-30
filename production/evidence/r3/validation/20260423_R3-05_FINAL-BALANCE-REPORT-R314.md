# R3-05 Final Balance Report

- Stage: R3 Balance and AI Tuning
- Task: R3-05 balance regression validation
- Date: 2026-04-23
- Validation base profile: `R314-r307-defeat-floor-lift-d`
- Verdict: `FAIL`

## 1. Run-Level KPI Score

| Run | Batch | KPI Pass | Win(E/S/H) | AvgEndTurn | Attrition | PathMonopoly |
|---|---|---:|---:|---:|---:|---:|
| V01 | R3-05-B002-R314-V01 | 12/13 | 0.6600/0.4600/0.2167 | 14.3940 | 0.2100 | 0.6510 |
| V02 | R3-05-B002-R314-V02 | 12/13 | 0.6733/0.4950/0.2833 | 14.4170 | 0.1860 | 0.6433 |
| V03 | R3-05-B002-R314-V03 | 13/13 | 0.6667/0.4600/0.2300 | 14.3540 | 0.1920 | 0.6291 |

## 2. KPI Stability Matrix

| KPI | Mean Actual | Pass Runs | Target |
|---|---:|---:|---:|
| WIN_RATE_EASY | 0.6667 | 3/3 | [0.6500, 0.8000] |
| WIN_RATE_STANDARD | 0.4717 | 3/3 | [0.4500, 0.5500] |
| WIN_RATE_HARD | 0.2433 | 3/3 | [0.2000, 0.3500] |
| WIN_RATE_OVERALL | 0.4617 | 3/3 | [0.4000, 0.6000] |
| AVG_END_TURN_OVERALL | 14.3883 | 3/3 | [14.0000, 20.0000] |
| AVG_END_TURN_STANDARD | 15.2975 | 3/3 | [15.0000, 19.0000] |
| ATTRITION_RATE_OVERALL | 0.1960 | 3/3 | [0.1000, 0.3500] |
| RESOURCE_CV_GOLDLEAF | 0.4114 | 3/3 | [0.1800, 0.4500] |
| RESOURCE_CV_FIREOIL | 0.1812 | 3/3 | [0.1500, 0.4000] |
| RESOURCE_CV_ARMS | 0.2686 | 3/3 | [0.2000, 0.5500] |
| VICTORY_SHARE_OVERALL | 0.4617 | 3/3 | [0.3500, 0.6500] |
| DEFEAT_SHARE_OVERALL | 0.3423 | 1/3 | [0.3500, 0.6500] |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.6411 | 3/3 | [0.0000, 0.7000] |

## 3. Gate Decision Rule

- Rule: all hard-gate KPIs must pass in all validation runs.
- Result: `FAIL`
- Failed KPI IDs: DEFEAT_SHARE_OVERALL

## 4. Acceptance Conclusion

- R3-05 validation does not meet KPI gates. Keep R3 in `not ready` state and continue targeted tuning.
