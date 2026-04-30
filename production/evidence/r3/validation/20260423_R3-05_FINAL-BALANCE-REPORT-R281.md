# R3-05 Final Balance Report

- Stage: R3 Balance and AI Tuning
- Task: R3-05 balance regression validation
- Date: 2026-04-23
- Validation base profile: `R281-r276-defeat-lift-d`
- Verdict: `FAIL`

## 1. Run-Level KPI Score

| Run | Batch | KPI Pass | Win(E/S/H) | AvgEndTurn | Attrition | PathMonopoly |
|---|---|---:|---:|---:|---:|---:|
| V01 | R3-05-B002-R281-V01 | 13/13 | 0.6500/0.4700/0.2267 | 14.2440 | 0.1930 | 0.6807 |
| V02 | R3-05-B002-R281-V02 | 11/13 | 0.6733/0.4900/0.3067 | 14.1260 | 0.1850 | 0.6429 |
| V03 | R3-05-B002-R281-V03 | 13/13 | 0.6700/0.4625/0.2000 | 14.2350 | 0.1810 | 0.6233 |

## 2. KPI Stability Matrix

| KPI | Mean Actual | Pass Runs | Target |
|---|---:|---:|---:|
| WIN_RATE_EASY | 0.6644 | 3/3 | [0.6500, 0.8000] |
| WIN_RATE_STANDARD | 0.4742 | 3/3 | [0.4500, 0.5500] |
| WIN_RATE_HARD | 0.2444 | 3/3 | [0.2000, 0.3500] |
| WIN_RATE_OVERALL | 0.4623 | 3/3 | [0.4000, 0.6000] |
| AVG_END_TURN_OVERALL | 14.2017 | 3/3 | [14.0000, 20.0000] |
| AVG_END_TURN_STANDARD | 15.1067 | 2/3 | [15.0000, 19.0000] |
| ATTRITION_RATE_OVERALL | 0.1863 | 3/3 | [0.1000, 0.3500] |
| RESOURCE_CV_GOLDLEAF | 0.4186 | 3/3 | [0.1800, 0.4500] |
| RESOURCE_CV_FIREOIL | 0.1836 | 3/3 | [0.1500, 0.4000] |
| RESOURCE_CV_ARMS | 0.2715 | 3/3 | [0.2000, 0.5500] |
| VICTORY_SHARE_OVERALL | 0.4623 | 3/3 | [0.3500, 0.6500] |
| DEFEAT_SHARE_OVERALL | 0.3513 | 2/3 | [0.3500, 0.6500] |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.6490 | 3/3 | [0.0000, 0.7000] |

## 3. Gate Decision Rule

- Rule: all hard-gate KPIs must pass in all validation runs.
- Result: `FAIL`
- Failed KPI IDs: AVG_END_TURN_STANDARD, DEFEAT_SHARE_OVERALL

## 4. Acceptance Conclusion

- R3-05 validation does not meet KPI gates. Keep R3 in `not ready` state and continue targeted tuning.
