# R3-05 Final Balance Report

- Stage: R3 Balance and AI Tuning
- Task: R3-05 balance regression validation
- Date: 2026-04-21
- Validation base profile: `R85-r82-gate-stability-a`
- Verdict: `FAIL`

## 1. Run-Level KPI Score

| Run | Batch | KPI Pass | Win(E/S/H) | AvgEndTurn | Attrition | PathMonopoly |
|---|---|---:|---:|---:|---:|---:|
| V01 | R3-05-B002-R85-V01 | 11/13 | 0.6600/0.4875/0.2233 | 13.8990 | 0.1930 | 0.6696 |
| V02 | R3-05-B002-R85-V02 | 10/13 | 0.6367/0.4275/0.2800 | 14.3600 | 0.2110 | 0.6659 |
| V03 | R3-05-B002-R85-V03 | 11/13 | 0.6367/0.3775/0.2333 | 14.6490 | 0.2180 | 0.6383 |

## 2. KPI Stability Matrix

| KPI | Mean Actual | Pass Runs | Target |
|---|---:|---:|---:|
| WIN_RATE_EASY | 0.6444 | 1/3 | [0.6500, 0.8000] |
| WIN_RATE_STANDARD | 0.4308 | 1/3 | [0.4500, 0.5500] |
| WIN_RATE_HARD | 0.2456 | 3/3 | [0.2000, 0.3500] |
| WIN_RATE_OVERALL | 0.4393 | 3/3 | [0.4000, 0.6000] |
| AVG_END_TURN_OVERALL | 14.3027 | 2/3 | [14.0000, 20.0000] |
| AVG_END_TURN_STANDARD | 15.9442 | 3/3 | [15.0000, 19.0000] |
| ATTRITION_RATE_OVERALL | 0.2073 | 3/3 | [0.1000, 0.3500] |
| RESOURCE_CV_GOLDLEAF | 0.3598 | 3/3 | [0.1800, 0.4500] |
| RESOURCE_CV_FIREOIL | 0.1858 | 3/3 | [0.1500, 0.4000] |
| RESOURCE_CV_ARMS | 0.2578 | 3/3 | [0.2000, 0.5500] |
| VICTORY_SHARE_OVERALL | 0.4393 | 3/3 | [0.3500, 0.6500] |
| DEFEAT_SHARE_OVERALL | 0.3533 | 1/3 | [0.3500, 0.6500] |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.6579 | 3/3 | [0.0000, 0.7000] |

## 3. Gate Decision Rule

- Rule: all hard-gate KPIs must pass in all validation runs.
- Result: `FAIL`
- Failed KPI IDs: WIN_RATE_EASY, WIN_RATE_STANDARD, AVG_END_TURN_OVERALL, DEFEAT_SHARE_OVERALL

## 4. Acceptance Conclusion

- R3-05 validation does not meet KPI gates. Keep R3 in `not ready` state and continue targeted tuning.
