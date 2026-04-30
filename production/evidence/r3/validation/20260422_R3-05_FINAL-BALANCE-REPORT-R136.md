# R3-05 Final Balance Report

- Stage: R3 Balance and AI Tuning
- Task: R3-05 balance regression validation
- Date: 2026-04-22
- Validation base profile: `R136-r134-avgturn-recover-a`
- Verdict: `FAIL`

## 1. Run-Level KPI Score

| Run | Batch | KPI Pass | Win(E/S/H) | AvgEndTurn | Attrition | PathMonopoly |
|---|---|---:|---:|---:|---:|---:|
| V01 | R3-05-B002-R136-V01 | 11/13 | 0.7000/0.5325/0.2400 | 13.8160 | 0.1800 | 0.6545 |
| V02 | R3-05-B002-R136-V02 | 12/13 | 0.6767/0.5175/0.3133 | 14.2760 | 0.1900 | 0.6329 |
| V03 | R3-05-B002-R136-V03 | 12/13 | 0.7000/0.4850/0.2200 | 14.1290 | 0.1910 | 0.6532 |

## 2. KPI Stability Matrix

| KPI | Mean Actual | Pass Runs | Target |
|---|---:|---:|---:|
| WIN_RATE_EASY | 0.6922 | 3/3 | [0.6500, 0.8000] |
| WIN_RATE_STANDARD | 0.5117 | 3/3 | [0.4500, 0.5500] |
| WIN_RATE_HARD | 0.2578 | 3/3 | [0.2000, 0.3500] |
| WIN_RATE_OVERALL | 0.4897 | 3/3 | [0.4000, 0.6000] |
| AVG_END_TURN_OVERALL | 14.0737 | 2/3 | [14.0000, 20.0000] |
| AVG_END_TURN_STANDARD | 15.3125 | 3/3 | [15.0000, 19.0000] |
| ATTRITION_RATE_OVERALL | 0.1870 | 3/3 | [0.1000, 0.3500] |
| RESOURCE_CV_GOLDLEAF | 0.4000 | 3/3 | [0.1800, 0.4500] |
| RESOURCE_CV_FIREOIL | 0.1816 | 3/3 | [0.1500, 0.4000] |
| RESOURCE_CV_ARMS | 0.2609 | 3/3 | [0.2000, 0.5500] |
| VICTORY_SHARE_OVERALL | 0.4897 | 3/3 | [0.3500, 0.6500] |
| DEFEAT_SHARE_OVERALL | 0.3233 | 0/3 | [0.3500, 0.6500] |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.6469 | 3/3 | [0.0000, 0.7000] |

## 3. Gate Decision Rule

- Rule: all hard-gate KPIs must pass in all validation runs.
- Result: `FAIL`
- Failed KPI IDs: AVG_END_TURN_OVERALL, DEFEAT_SHARE_OVERALL

## 4. Acceptance Conclusion

- R3-05 validation does not meet KPI gates. Keep R3 in `not ready` state and continue targeted tuning.
