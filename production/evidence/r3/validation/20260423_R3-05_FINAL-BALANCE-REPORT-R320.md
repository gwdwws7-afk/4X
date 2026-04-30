# R3-05 Final Balance Report

- Stage: R3 Balance and AI Tuning
- Task: R3-05 balance regression validation
- Date: 2026-04-23
- Validation base profile: `R320-r318-late-convert-timeout-b`
- Verdict: `FAIL`

## 1. Run-Level KPI Score

| Run | Batch | KPI Pass | Win(E/S/H) | AvgEndTurn | Attrition | PathMonopoly |
|---|---|---:|---:|---:|---:|---:|
| V01 | R3-05-B002-R320-V01 | 13/13 | 0.6500/0.4500/0.2000 | 14.4710 | 0.2090 | 0.6552 |
| V02 | R3-05-B002-R320-V02 | 11/13 | 0.6433/0.4875/0.2767 | 14.5250 | 0.2000 | 0.6391 |
| V03 | R3-05-B002-R320-V03 | 11/13 | 0.6400/0.4425/0.2233 | 14.5380 | 0.2140 | 0.6216 |

## 2. KPI Stability Matrix

| KPI | Mean Actual | Pass Runs | Target |
|---|---:|---:|---:|
| WIN_RATE_EASY | 0.6444 | 1/3 | [0.6500, 0.8000] |
| WIN_RATE_STANDARD | 0.4600 | 2/3 | [0.4500, 0.5500] |
| WIN_RATE_HARD | 0.2333 | 3/3 | [0.2000, 0.3500] |
| WIN_RATE_OVERALL | 0.4473 | 3/3 | [0.4000, 0.6000] |
| AVG_END_TURN_OVERALL | 14.5113 | 3/3 | [14.0000, 20.0000] |
| AVG_END_TURN_STANDARD | 15.3975 | 3/3 | [15.0000, 19.0000] |
| ATTRITION_RATE_OVERALL | 0.2077 | 3/3 | [0.1000, 0.3500] |
| RESOURCE_CV_GOLDLEAF | 0.4142 | 3/3 | [0.1800, 0.4500] |
| RESOURCE_CV_FIREOIL | 0.1833 | 3/3 | [0.1500, 0.4000] |
| RESOURCE_CV_ARMS | 0.2694 | 3/3 | [0.2000, 0.5500] |
| VICTORY_SHARE_OVERALL | 0.4473 | 3/3 | [0.3500, 0.6500] |
| DEFEAT_SHARE_OVERALL | 0.3450 | 2/3 | [0.3500, 0.6500] |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.6386 | 3/3 | [0.0000, 0.7000] |

## 3. Gate Decision Rule

- Rule: all hard-gate KPIs must pass in all validation runs.
- Result: `FAIL`
- Failed KPI IDs: WIN_RATE_EASY, WIN_RATE_STANDARD, DEFEAT_SHARE_OVERALL

## 4. Acceptance Conclusion

- R3-05 validation does not meet KPI gates. Keep R3 in `not ready` state and continue targeted tuning.
