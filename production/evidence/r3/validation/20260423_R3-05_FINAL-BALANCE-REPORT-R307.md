# R3-05 Final Balance Report

- Stage: R3 Balance and AI Tuning
- Task: R3-05 balance regression validation
- Date: 2026-04-23
- Validation base profile: `R307-r306-standard-recover-hard-pressure-a`
- Verdict: `FAIL`

## 1. Run-Level KPI Score

| Run | Batch | KPI Pass | Win(E/S/H) | AvgEndTurn | Attrition | PathMonopoly |
|---|---|---:|---:|---:|---:|---:|
| V01 | R3-05-B002-R307-V01 | 12/13 | 0.6567/0.4625/0.2200 | 14.4060 | 0.2100 | 0.6451 |
| V02 | R3-05-B002-R307-V02 | 12/13 | 0.6600/0.4950/0.2900 | 14.4640 | 0.1890 | 0.6377 |
| V03 | R3-05-B002-R307-V03 | 13/13 | 0.6533/0.4625/0.2300 | 14.3690 | 0.1940 | 0.6289 |

## 2. KPI Stability Matrix

| KPI | Mean Actual | Pass Runs | Target |
|---|---:|---:|---:|
| WIN_RATE_EASY | 0.6567 | 3/3 | [0.6500, 0.8000] |
| WIN_RATE_STANDARD | 0.4733 | 3/3 | [0.4500, 0.5500] |
| WIN_RATE_HARD | 0.2467 | 3/3 | [0.2000, 0.3500] |
| WIN_RATE_OVERALL | 0.4603 | 3/3 | [0.4000, 0.6000] |
| AVG_END_TURN_OVERALL | 14.4130 | 3/3 | [14.0000, 20.0000] |
| AVG_END_TURN_STANDARD | 15.2825 | 3/3 | [15.0000, 19.0000] |
| ATTRITION_RATE_OVERALL | 0.1977 | 3/3 | [0.1000, 0.3500] |
| RESOURCE_CV_GOLDLEAF | 0.4126 | 3/3 | [0.1800, 0.4500] |
| RESOURCE_CV_FIREOIL | 0.1820 | 3/3 | [0.1500, 0.4000] |
| RESOURCE_CV_ARMS | 0.2692 | 3/3 | [0.2000, 0.5500] |
| VICTORY_SHARE_OVERALL | 0.4603 | 3/3 | [0.3500, 0.6500] |
| DEFEAT_SHARE_OVERALL | 0.3420 | 1/3 | [0.3500, 0.6500] |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.6372 | 3/3 | [0.0000, 0.7000] |

## 3. Gate Decision Rule

- Rule: all hard-gate KPIs must pass in all validation runs.
- Result: `FAIL`
- Failed KPI IDs: DEFEAT_SHARE_OVERALL

## 4. Acceptance Conclusion

- R3-05 validation does not meet KPI gates. Keep R3 in `not ready` state and continue targeted tuning.
