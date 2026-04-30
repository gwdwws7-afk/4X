# R3-05 Final Balance Report

- Stage: R3 Balance and AI Tuning
- Task: R3-05 balance regression validation
- Date: 2026-04-23
- Validation base profile: `R319-r318-late-convert-timeout-a`
- Verdict: `FAIL`

## 1. Run-Level KPI Score

| Run | Batch | KPI Pass | Win(E/S/H) | AvgEndTurn | Attrition | PathMonopoly |
|---|---|---:|---:|---:|---:|---:|
| V01 | R3-05-B002-R319-V01 | 13/13 | 0.6633/0.4500/0.2000 | 14.4400 | 0.2060 | 0.6606 |
| V02 | R3-05-B002-R319-V02 | 12/13 | 0.6533/0.4925/0.2800 | 14.5220 | 0.1950 | 0.6436 |
| V03 | R3-05-B002-R319-V03 | 11/13 | 0.6433/0.4425/0.2267 | 14.5180 | 0.2090 | 0.6279 |

## 2. KPI Stability Matrix

| KPI | Mean Actual | Pass Runs | Target |
|---|---:|---:|---:|
| WIN_RATE_EASY | 0.6533 | 2/3 | [0.6500, 0.8000] |
| WIN_RATE_STANDARD | 0.4617 | 2/3 | [0.4500, 0.5500] |
| WIN_RATE_HARD | 0.2356 | 3/3 | [0.2000, 0.3500] |
| WIN_RATE_OVERALL | 0.4513 | 3/3 | [0.4000, 0.6000] |
| AVG_END_TURN_OVERALL | 14.4933 | 3/3 | [14.0000, 20.0000] |
| AVG_END_TURN_STANDARD | 15.4025 | 3/3 | [15.0000, 19.0000] |
| ATTRITION_RATE_OVERALL | 0.2033 | 3/3 | [0.1000, 0.3500] |
| RESOURCE_CV_GOLDLEAF | 0.4139 | 3/3 | [0.1800, 0.4500] |
| RESOURCE_CV_FIREOIL | 0.1831 | 3/3 | [0.1500, 0.4000] |
| RESOURCE_CV_ARMS | 0.2689 | 3/3 | [0.2000, 0.5500] |
| VICTORY_SHARE_OVERALL | 0.4513 | 3/3 | [0.3500, 0.6500] |
| DEFEAT_SHARE_OVERALL | 0.3453 | 2/3 | [0.3500, 0.6500] |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.6440 | 3/3 | [0.0000, 0.7000] |

## 3. Gate Decision Rule

- Rule: all hard-gate KPIs must pass in all validation runs.
- Result: `FAIL`
- Failed KPI IDs: WIN_RATE_EASY, WIN_RATE_STANDARD, DEFEAT_SHARE_OVERALL

## 4. Acceptance Conclusion

- R3-05 validation does not meet KPI gates. Keep R3 in `not ready` state and continue targeted tuning.
