# R3-05 Final Balance Report

- Stage: R3 Balance and AI Tuning
- Task: R3-05 balance regression validation
- Date: 2026-04-22
- Validation base profile: `R160-r157-defeat-share-recover-a`
- Verdict: `FAIL`

## 1. Run-Level KPI Score

| Run | Batch | KPI Pass | Win(E/S/H) | AvgEndTurn | Attrition | PathMonopoly |
|---|---|---:|---:|---:|---:|---:|
| V01 | R3-05-B002-R160-V01 | 11/13 | 0.7000/0.5050/0.2400 | 13.7830 | 0.1830 | 0.6632 |
| V02 | R3-05-B002-R160-V02 | 12/13 | 0.6633/0.4800/0.2900 | 14.4030 | 0.2040 | 0.6444 |
| V03 | R3-05-B002-R160-V03 | 12/13 | 0.6800/0.4550/0.2333 | 14.2420 | 0.1980 | 0.6491 |

## 2. KPI Stability Matrix

| KPI | Mean Actual | Pass Runs | Target |
|---|---:|---:|---:|
| WIN_RATE_EASY | 0.6811 | 3/3 | [0.6500, 0.8000] |
| WIN_RATE_STANDARD | 0.4800 | 3/3 | [0.4500, 0.5500] |
| WIN_RATE_HARD | 0.2544 | 3/3 | [0.2000, 0.3500] |
| WIN_RATE_OVERALL | 0.4727 | 3/3 | [0.4000, 0.6000] |
| AVG_END_TURN_OVERALL | 14.1427 | 2/3 | [14.0000, 20.0000] |
| AVG_END_TURN_STANDARD | 15.5633 | 3/3 | [15.0000, 19.0000] |
| ATTRITION_RATE_OVERALL | 0.1950 | 3/3 | [0.1000, 0.3500] |
| RESOURCE_CV_GOLDLEAF | 0.4028 | 3/3 | [0.1800, 0.4500] |
| RESOURCE_CV_FIREOIL | 0.1811 | 3/3 | [0.1500, 0.4000] |
| RESOURCE_CV_ARMS | 0.2618 | 3/3 | [0.2000, 0.5500] |
| VICTORY_SHARE_OVERALL | 0.4727 | 3/3 | [0.3500, 0.6500] |
| DEFEAT_SHARE_OVERALL | 0.3323 | 0/3 | [0.3500, 0.6500] |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.6522 | 3/3 | [0.0000, 0.7000] |

## 3. Gate Decision Rule

- Rule: all hard-gate KPIs must pass in all validation runs.
- Result: `FAIL`
- Failed KPI IDs: AVG_END_TURN_OVERALL, DEFEAT_SHARE_OVERALL

## 4. Acceptance Conclusion

- R3-05 validation does not meet KPI gates. Keep R3 in `not ready` state and continue targeted tuning.
