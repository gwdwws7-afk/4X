# R3-05 Final Balance Report

- Stage: R3 Balance and AI Tuning
- Task: R3-05 balance regression validation
- Date: 2026-04-22
- Validation base profile: `R151-r149-defeat-floor-recover-a`
- Verdict: `FAIL`

## 1. Run-Level KPI Score

| Run | Batch | KPI Pass | Win(E/S/H) | AvgEndTurn | Attrition | PathMonopoly |
|---|---|---:|---:|---:|---:|---:|
| V01 | R3-05-B002-R151-V01 | 10/13 | 0.7233/0.5325/0.2467 | 13.6290 | 0.1760 | 0.6429 |
| V02 | R3-05-B002-R151-V02 | 12/13 | 0.6900/0.5175/0.3200 | 14.1420 | 0.1870 | 0.6294 |
| V03 | R3-05-B002-R151-V03 | 12/13 | 0.7033/0.4800/0.2300 | 14.0020 | 0.1930 | 0.6377 |

## 2. KPI Stability Matrix

| KPI | Mean Actual | Pass Runs | Target |
|---|---:|---:|---:|
| WIN_RATE_EASY | 0.7056 | 3/3 | [0.6500, 0.8000] |
| WIN_RATE_STANDARD | 0.5100 | 3/3 | [0.4500, 0.5500] |
| WIN_RATE_HARD | 0.2656 | 3/3 | [0.2000, 0.3500] |
| WIN_RATE_OVERALL | 0.4953 | 3/3 | [0.4000, 0.6000] |
| AVG_END_TURN_OVERALL | 13.9243 | 2/3 | [14.0000, 20.0000] |
| AVG_END_TURN_STANDARD | 15.3542 | 2/3 | [15.0000, 19.0000] |
| ATTRITION_RATE_OVERALL | 0.1853 | 3/3 | [0.1000, 0.3500] |
| RESOURCE_CV_GOLDLEAF | 0.3945 | 3/3 | [0.1800, 0.4500] |
| RESOURCE_CV_FIREOIL | 0.1794 | 3/3 | [0.1500, 0.4000] |
| RESOURCE_CV_ARMS | 0.2583 | 3/3 | [0.2000, 0.5500] |
| VICTORY_SHARE_OVERALL | 0.4953 | 3/3 | [0.3500, 0.6500] |
| DEFEAT_SHARE_OVERALL | 0.3193 | 0/3 | [0.3500, 0.6500] |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.6367 | 3/3 | [0.0000, 0.7000] |

## 3. Gate Decision Rule

- Rule: all hard-gate KPIs must pass in all validation runs.
- Result: `FAIL`
- Failed KPI IDs: AVG_END_TURN_OVERALL, AVG_END_TURN_STANDARD, DEFEAT_SHARE_OVERALL

## 4. Acceptance Conclusion

- R3-05 validation does not meet KPI gates. Keep R3 in `not ready` state and continue targeted tuning.
