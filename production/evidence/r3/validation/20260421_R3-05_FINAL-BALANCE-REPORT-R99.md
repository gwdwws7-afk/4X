# R3-05 Final Balance Report

- Stage: R3 Balance and AI Tuning
- Task: R3-05 balance regression validation
- Date: 2026-04-21
- Validation base profile: `R99-r82-early-suppress-c`
- Verdict: `FAIL`

## 1. Run-Level KPI Score

| Run | Batch | KPI Pass | Win(E/S/H) | AvgEndTurn | Attrition | PathMonopoly |
|---|---|---:|---:|---:|---:|---:|
| V01 | R3-05-B002-R99-V01 | 11/13 | 0.6867/0.5100/0.2367 | 13.4530 | 0.1760 | 0.6590 |
| V02 | R3-05-B002-R99-V02 | 11/13 | 0.6533/0.4875/0.3000 | 13.9580 | 0.2020 | 0.6528 |
| V03 | R3-05-B002-R99-V03 | 11/13 | 0.7000/0.4550/0.2300 | 13.9250 | 0.1930 | 0.6659 |

## 2. KPI Stability Matrix

| KPI | Mean Actual | Pass Runs | Target |
|---|---:|---:|---:|
| WIN_RATE_EASY | 0.6800 | 3/3 | [0.6500, 0.8000] |
| WIN_RATE_STANDARD | 0.4842 | 3/3 | [0.4500, 0.5500] |
| WIN_RATE_HARD | 0.2556 | 3/3 | [0.2000, 0.3500] |
| WIN_RATE_OVERALL | 0.4743 | 3/3 | [0.4000, 0.6000] |
| AVG_END_TURN_OVERALL | 13.7787 | 0/3 | [14.0000, 20.0000] |
| AVG_END_TURN_STANDARD | 15.4325 | 3/3 | [15.0000, 19.0000] |
| ATTRITION_RATE_OVERALL | 0.1903 | 3/3 | [0.1000, 0.3500] |
| RESOURCE_CV_GOLDLEAF | 0.3680 | 3/3 | [0.1800, 0.4500] |
| RESOURCE_CV_FIREOIL | 0.1787 | 3/3 | [0.1500, 0.4000] |
| RESOURCE_CV_ARMS | 0.2492 | 3/3 | [0.2000, 0.5500] |
| VICTORY_SHARE_OVERALL | 0.4743 | 3/3 | [0.3500, 0.6500] |
| DEFEAT_SHARE_OVERALL | 0.3353 | 0/3 | [0.3500, 0.6500] |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.6593 | 3/3 | [0.0000, 0.7000] |

## 3. Gate Decision Rule

- Rule: all hard-gate KPIs must pass in all validation runs.
- Result: `FAIL`
- Failed KPI IDs: AVG_END_TURN_OVERALL, DEFEAT_SHARE_OVERALL

## 4. Acceptance Conclusion

- R3-05 validation does not meet KPI gates. Keep R3 in `not ready` state and continue targeted tuning.
