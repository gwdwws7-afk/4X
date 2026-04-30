# R3-05 Final Balance Report

- Stage: R3 Balance and AI Tuning
- Task: R3-05 balance regression validation
- Date: 2026-04-23
- Validation base profile: `R313-r307-defeat-floor-lift-c`
- Verdict: `FAIL`

## 1. Run-Level KPI Score

| Run | Batch | KPI Pass | Win(E/S/H) | AvgEndTurn | Attrition | PathMonopoly |
|---|---|---:|---:|---:|---:|---:|
| V01 | R3-05-B002-R313-V01 | 12/13 | 0.6567/0.4675/0.2200 | 14.3810 | 0.2090 | 0.6467 |
| V02 | R3-05-B002-R313-V02 | 12/13 | 0.6633/0.4900/0.2833 | 14.4740 | 0.1890 | 0.6438 |
| V03 | R3-05-B002-R313-V03 | 13/13 | 0.6633/0.4650/0.2300 | 14.3380 | 0.1940 | 0.6278 |

## 2. KPI Stability Matrix

| KPI | Mean Actual | Pass Runs | Target |
|---|---:|---:|---:|
| WIN_RATE_EASY | 0.6611 | 3/3 | [0.6500, 0.8000] |
| WIN_RATE_STANDARD | 0.4742 | 3/3 | [0.4500, 0.5500] |
| WIN_RATE_HARD | 0.2444 | 3/3 | [0.2000, 0.3500] |
| WIN_RATE_OVERALL | 0.4613 | 3/3 | [0.4000, 0.6000] |
| AVG_END_TURN_OVERALL | 14.3977 | 3/3 | [14.0000, 20.0000] |
| AVG_END_TURN_STANDARD | 15.2700 | 3/3 | [15.0000, 19.0000] |
| ATTRITION_RATE_OVERALL | 0.1973 | 3/3 | [0.1000, 0.3500] |
| RESOURCE_CV_GOLDLEAF | 0.4109 | 3/3 | [0.1800, 0.4500] |
| RESOURCE_CV_FIREOIL | 0.1821 | 3/3 | [0.1500, 0.4000] |
| RESOURCE_CV_ARMS | 0.2689 | 3/3 | [0.2000, 0.5500] |
| VICTORY_SHARE_OVERALL | 0.4613 | 3/3 | [0.3500, 0.6500] |
| DEFEAT_SHARE_OVERALL | 0.3413 | 1/3 | [0.3500, 0.6500] |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.6394 | 3/3 | [0.0000, 0.7000] |

## 3. Gate Decision Rule

- Rule: all hard-gate KPIs must pass in all validation runs.
- Result: `FAIL`
- Failed KPI IDs: DEFEAT_SHARE_OVERALL

## 4. Acceptance Conclusion

- R3-05 validation does not meet KPI gates. Keep R3 in `not ready` state and continue targeted tuning.
