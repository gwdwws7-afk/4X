# R3-05 Final Balance Report

- Stage: R3 Balance and AI Tuning
- Task: R3-05 balance regression validation
- Date: 2026-04-23
- Validation base profile: `R318-r311-node-loss-convert-d`
- Verdict: `FAIL`

## 1. Run-Level KPI Score

| Run | Batch | KPI Pass | Win(E/S/H) | AvgEndTurn | Attrition | PathMonopoly |
|---|---|---:|---:|---:|---:|---:|
| V01 | R3-05-B002-R318-V01 | 13/13 | 0.6700/0.4550/0.2033 | 14.4030 | 0.2040 | 0.6509 |
| V02 | R3-05-B002-R318-V02 | 12/13 | 0.6533/0.4875/0.2800 | 14.5440 | 0.1980 | 0.6400 |
| V03 | R3-05-B002-R318-V03 | 12/13 | 0.6533/0.4425/0.2333 | 14.4580 | 0.2030 | 0.6275 |

## 2. KPI Stability Matrix

| KPI | Mean Actual | Pass Runs | Target |
|---|---:|---:|---:|
| WIN_RATE_EASY | 0.6589 | 3/3 | [0.6500, 0.8000] |
| WIN_RATE_STANDARD | 0.4617 | 2/3 | [0.4500, 0.5500] |
| WIN_RATE_HARD | 0.2389 | 3/3 | [0.2000, 0.3500] |
| WIN_RATE_OVERALL | 0.4540 | 3/3 | [0.4000, 0.6000] |
| AVG_END_TURN_OVERALL | 14.4683 | 3/3 | [14.0000, 20.0000] |
| AVG_END_TURN_STANDARD | 15.3892 | 3/3 | [15.0000, 19.0000] |
| ATTRITION_RATE_OVERALL | 0.2017 | 3/3 | [0.1000, 0.3500] |
| RESOURCE_CV_GOLDLEAF | 0.4130 | 3/3 | [0.1800, 0.4500] |
| RESOURCE_CV_FIREOIL | 0.1829 | 3/3 | [0.1500, 0.4000] |
| RESOURCE_CV_ARMS | 0.2687 | 3/3 | [0.2000, 0.5500] |
| VICTORY_SHARE_OVERALL | 0.4540 | 3/3 | [0.3500, 0.6500] |
| DEFEAT_SHARE_OVERALL | 0.3443 | 2/3 | [0.3500, 0.6500] |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.6395 | 3/3 | [0.0000, 0.7000] |

## 3. Gate Decision Rule

- Rule: all hard-gate KPIs must pass in all validation runs.
- Result: `FAIL`
- Failed KPI IDs: WIN_RATE_STANDARD, DEFEAT_SHARE_OVERALL

## 4. Acceptance Conclusion

- R3-05 validation does not meet KPI gates. Keep R3 in `not ready` state and continue targeted tuning.
