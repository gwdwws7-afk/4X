# R3-05 Final Balance Report

- Stage: R3 Balance and AI Tuning
- Task: R3-05 balance regression validation
- Date: 2026-04-21
- Validation base profile: `R82-r79-pace-backpull-a`
- Verdict: `FAIL`

## 1. Run-Level KPI Score

| Run | Batch | KPI Pass | Win(E/S/H) | AvgEndTurn | Attrition | PathMonopoly |
|---|---|---:|---:|---:|---:|---:|
| V01 | R3-05-B002-R82-V01 | 10/13 | 0.7200/0.5050/0.2567 | 13.2570 | 0.1590 | 0.6788 |
| V02 | R3-05-B002-R82-V02 | 9/13 | 0.7133/0.4475/0.3100 | 13.7200 | 0.1770 | 0.7037 |
| V03 | R3-05-B002-R82-V03 | 12/13 | 0.6967/0.4100/0.2667 | 14.2370 | 0.1960 | 0.6711 |

## 2. KPI Stability Matrix

| KPI | Mean Actual | Pass Runs | Target |
|---|---:|---:|---:|
| WIN_RATE_EASY | 0.7100 | 3/3 | [0.6500, 0.8000] |
| WIN_RATE_STANDARD | 0.4542 | 1/3 | [0.4500, 0.5500] |
| WIN_RATE_HARD | 0.2778 | 3/3 | [0.2000, 0.3500] |
| WIN_RATE_OVERALL | 0.4780 | 3/3 | [0.4000, 0.6000] |
| AVG_END_TURN_OVERALL | 13.7380 | 1/3 | [14.0000, 20.0000] |
| AVG_END_TURN_STANDARD | 15.6392 | 2/3 | [15.0000, 19.0000] |
| ATTRITION_RATE_OVERALL | 0.1773 | 3/3 | [0.1000, 0.3500] |
| RESOURCE_CV_GOLDLEAF | 0.3500 | 3/3 | [0.1800, 0.4500] |
| RESOURCE_CV_FIREOIL | 0.1898 | 3/3 | [0.1500, 0.4000] |
| RESOURCE_CV_ARMS | 0.2616 | 3/3 | [0.2000, 0.5500] |
| VICTORY_SHARE_OVERALL | 0.4780 | 3/3 | [0.3500, 0.6500] |
| DEFEAT_SHARE_OVERALL | 0.3447 | 1/3 | [0.3500, 0.6500] |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.6845 | 2/3 | [0.0000, 0.7000] |

## 3. Gate Decision Rule

- Rule: all hard-gate KPIs must pass in all validation runs.
- Result: `FAIL`
- Failed KPI IDs: WIN_RATE_STANDARD, AVG_END_TURN_OVERALL, AVG_END_TURN_STANDARD, DEFEAT_SHARE_OVERALL, SINGLE_PATH_VICTORY_MONOPOLY

## 4. Acceptance Conclusion

- R3-05 validation does not meet KPI gates. Keep R3 in `not ready` state and continue targeted tuning.
