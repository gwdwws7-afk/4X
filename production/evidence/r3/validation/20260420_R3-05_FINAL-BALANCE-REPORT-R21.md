# R3-05 Final Balance Report

- Stage: R3 Balance and AI Tuning
- Task: R3-05 balance regression validation
- Date: 2026-04-20
- Validation base profile: `R21-win-floor-guard`
- Verdict: `FAIL`

## 1. Run-Level KPI Score

| Run | Batch | KPI Pass | Win(E/S/H) | AvgEndTurn | Attrition | PathMonopoly |
|---|---|---:|---:|---:|---:|---:|
| V01 | R3-05-B002-R21-V01 | 9/13 | 0.6233/0.4325/0.1400 | 14.6970 | 0.2200 | 0.9876 |
| V02 | R3-05-B002-R21-V02 | 10/13 | 0.6100/0.4500/0.1600 | 15.0360 | 0.2180 | 0.9659 |
| V03 | R3-05-B002-R21-V03 | 8/13 | 0.5967/0.4250/0.1667 | 14.8670 | 0.2310 | 0.9925 |

## 2. KPI Stability Matrix

| KPI | Mean Actual | Pass Runs | Target |
|---|---:|---:|---:|
| WIN_RATE_EASY | 0.6100 | 0/3 | [0.6500, 0.8000] |
| WIN_RATE_STANDARD | 0.4358 | 1/3 | [0.4500, 0.5500] |
| WIN_RATE_HARD | 0.1556 | 0/3 | [0.2000, 0.3500] |
| WIN_RATE_OVERALL | 0.4040 | 2/3 | [0.4000, 0.6000] |
| AVG_END_TURN_OVERALL | 14.8667 | 3/3 | [14.0000, 20.0000] |
| AVG_END_TURN_STANDARD | 16.2217 | 3/3 | [15.0000, 19.0000] |
| ATTRITION_RATE_OVERALL | 0.2230 | 3/3 | [0.1000, 0.3500] |
| RESOURCE_CV_GOLDLEAF | 0.3283 | 3/3 | [0.1800, 0.4500] |
| RESOURCE_CV_FIREOIL | 0.1871 | 3/3 | [0.1500, 0.4000] |
| RESOURCE_CV_ARMS | 0.2414 | 3/3 | [0.2000, 0.5500] |
| VICTORY_SHARE_OVERALL | 0.4040 | 3/3 | [0.3500, 0.6500] |
| DEFEAT_SHARE_OVERALL | 0.3730 | 3/3 | [0.3500, 0.6500] |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.9820 | 0/3 | [0.0000, 0.7000] |

## 3. Gate Decision Rule

- Rule: all hard-gate KPIs must pass in all validation runs.
- Result: `FAIL`
- Failed KPI IDs: WIN_RATE_EASY, WIN_RATE_STANDARD, WIN_RATE_HARD, WIN_RATE_OVERALL, SINGLE_PATH_VICTORY_MONOPOLY

## 4. Acceptance Conclusion

- R3-05 validation does not meet KPI gates. Keep R3 in `not ready` state and continue targeted tuning.
