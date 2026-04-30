# R3-05 Final Balance Report

- Stage: R3 Balance and AI Tuning
- Task: R3-05 balance regression validation
- Date: 2026-04-22
- Validation base profile: `R180-r173-endturn-correct-c`
- Verdict: `FAIL`

## 1. Run-Level KPI Score

| Run | Batch | KPI Pass | Win(E/S/H) | AvgEndTurn | Attrition | PathMonopoly |
|---|---|---:|---:|---:|---:|---:|
| V01 | R3-05-B002-R180-V01 | 11/13 | 0.7000/0.4775/0.1967 | 13.6820 | 0.1840 | 0.6543 |
| V02 | R3-05-B002-R180-V02 | 12/13 | 0.6700/0.4875/0.2767 | 14.0220 | 0.1920 | 0.6263 |
| V03 | R3-05-B002-R180-V03 | 11/13 | 0.6733/0.4550/0.1867 | 13.9810 | 0.1960 | 0.6364 |

## 2. KPI Stability Matrix

| KPI | Mean Actual | Pass Runs | Target |
|---|---:|---:|---:|
| WIN_RATE_EASY | 0.6811 | 3/3 | [0.6500, 0.8000] |
| WIN_RATE_STANDARD | 0.4733 | 3/3 | [0.4500, 0.5500] |
| WIN_RATE_HARD | 0.2200 | 1/3 | [0.2000, 0.3500] |
| WIN_RATE_OVERALL | 0.4597 | 3/3 | [0.4000, 0.6000] |
| AVG_END_TURN_OVERALL | 13.8950 | 1/3 | [14.0000, 20.0000] |
| AVG_END_TURN_STANDARD | 15.4008 | 3/3 | [15.0000, 19.0000] |
| ATTRITION_RATE_OVERALL | 0.1907 | 3/3 | [0.1000, 0.3500] |
| RESOURCE_CV_GOLDLEAF | 0.4013 | 3/3 | [0.1800, 0.4500] |
| RESOURCE_CV_FIREOIL | 0.1787 | 3/3 | [0.1500, 0.4000] |
| RESOURCE_CV_ARMS | 0.2564 | 3/3 | [0.2000, 0.5500] |
| VICTORY_SHARE_OVERALL | 0.4597 | 3/3 | [0.3500, 0.6500] |
| DEFEAT_SHARE_OVERALL | 0.3497 | 2/3 | [0.3500, 0.6500] |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.6390 | 3/3 | [0.0000, 0.7000] |

## 3. Gate Decision Rule

- Rule: all hard-gate KPIs must pass in all validation runs.
- Result: `FAIL`
- Failed KPI IDs: WIN_RATE_HARD, AVG_END_TURN_OVERALL, DEFEAT_SHARE_OVERALL

## 4. Acceptance Conclusion

- R3-05 validation does not meet KPI gates. Keep R3 in `not ready` state and continue targeted tuning.
