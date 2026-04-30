# R3-05 Final Balance Report

- Stage: R3 Balance and AI Tuning
- Task: R3-05 balance regression validation
- Date: 2026-04-23
- Validation base profile: `R305-r296-variance-compress-c`
- Verdict: `FAIL`

## 1. Run-Level KPI Score

| Run | Batch | KPI Pass | Win(E/S/H) | AvgEndTurn | Attrition | PathMonopoly |
|---|---|---:|---:|---:|---:|---:|
| V01 | R3-05-B002-R305-V01 | 11/13 | 0.6300/0.4400/0.2133 | 14.7800 | 0.2160 | 0.6713 |
| V02 | R3-05-B002-R305-V02 | 12/13 | 0.6567/0.4675/0.2800 | 14.6590 | 0.2020 | 0.6389 |
| V03 | R3-05-B002-R305-V03 | 11/13 | 0.6600/0.4225/0.1867 | 14.7470 | 0.2030 | 0.6336 |

## 2. KPI Stability Matrix

| KPI | Mean Actual | Pass Runs | Target |
|---|---:|---:|---:|
| WIN_RATE_EASY | 0.6489 | 2/3 | [0.6500, 0.8000] |
| WIN_RATE_STANDARD | 0.4433 | 1/3 | [0.4500, 0.5500] |
| WIN_RATE_HARD | 0.2267 | 2/3 | [0.2000, 0.3500] |
| WIN_RATE_OVERALL | 0.4400 | 3/3 | [0.4000, 0.6000] |
| AVG_END_TURN_OVERALL | 14.7287 | 3/3 | [14.0000, 20.0000] |
| AVG_END_TURN_STANDARD | 15.7692 | 3/3 | [15.0000, 19.0000] |
| ATTRITION_RATE_OVERALL | 0.2070 | 3/3 | [0.1000, 0.3500] |
| RESOURCE_CV_GOLDLEAF | 0.4183 | 3/3 | [0.1800, 0.4500] |
| RESOURCE_CV_FIREOIL | 0.1833 | 3/3 | [0.1500, 0.4000] |
| RESOURCE_CV_ARMS | 0.2708 | 3/3 | [0.2000, 0.5500] |
| VICTORY_SHARE_OVERALL | 0.4400 | 3/3 | [0.3500, 0.6500] |
| DEFEAT_SHARE_OVERALL | 0.3530 | 2/3 | [0.3500, 0.6500] |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.6479 | 3/3 | [0.0000, 0.7000] |

## 3. Gate Decision Rule

- Rule: all hard-gate KPIs must pass in all validation runs.
- Result: `FAIL`
- Failed KPI IDs: WIN_RATE_EASY, WIN_RATE_STANDARD, WIN_RATE_HARD, DEFEAT_SHARE_OVERALL

## 4. Acceptance Conclusion

- R3-05 validation does not meet KPI gates. Keep R3 in `not ready` state and continue targeted tuning.
