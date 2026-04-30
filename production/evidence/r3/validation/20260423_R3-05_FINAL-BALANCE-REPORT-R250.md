# R3-05 Final Balance Report

- Stage: R3 Balance and AI Tuning
- Task: R3-05 balance regression validation
- Date: 2026-04-23
- Validation base profile: `R250-r230-easy-final-fine-a`
- Verdict: `FAIL`

## 1. Run-Level KPI Score

| Run | Batch | KPI Pass | Win(E/S/H) | AvgEndTurn | Attrition | PathMonopoly |
|---|---|---:|---:|---:|---:|---:|
| V01 | R3-05-B002-R250-V01 | 9/13 | 0.5833/0.5150/0.2400 | 13.9940 | 0.2270 | 0.6865 |
| V02 | R3-05-B002-R250-V02 | 9/13 | 0.6333/0.4900/0.2933 | 13.9180 | 0.2080 | 0.6371 |
| V03 | R3-05-B002-R250-V03 | 11/13 | 0.6033/0.4550/0.2000 | 14.3990 | 0.2370 | 0.6028 |

## 2. KPI Stability Matrix

| KPI | Mean Actual | Pass Runs | Target |
|---|---:|---:|---:|
| WIN_RATE_EASY | 0.6067 | 0/3 | [0.6500, 0.8000] |
| WIN_RATE_STANDARD | 0.4867 | 3/3 | [0.4500, 0.5500] |
| WIN_RATE_HARD | 0.2444 | 3/3 | [0.2000, 0.3500] |
| WIN_RATE_OVERALL | 0.4500 | 3/3 | [0.4000, 0.6000] |
| AVG_END_TURN_OVERALL | 14.1037 | 1/3 | [14.0000, 20.0000] |
| AVG_END_TURN_STANDARD | 15.1400 | 1/3 | [15.0000, 19.0000] |
| ATTRITION_RATE_OVERALL | 0.2240 | 3/3 | [0.1000, 0.3500] |
| RESOURCE_CV_GOLDLEAF | 0.3862 | 3/3 | [0.1800, 0.4500] |
| RESOURCE_CV_FIREOIL | 0.1547 | 3/3 | [0.1500, 0.4000] |
| RESOURCE_CV_ARMS | 0.2225 | 3/3 | [0.2000, 0.5500] |
| VICTORY_SHARE_OVERALL | 0.4500 | 3/3 | [0.3500, 0.6500] |
| DEFEAT_SHARE_OVERALL | 0.3260 | 0/3 | [0.3500, 0.6500] |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.6422 | 3/3 | [0.0000, 0.7000] |

## 3. Gate Decision Rule

- Rule: all hard-gate KPIs must pass in all validation runs.
- Result: `FAIL`
- Failed KPI IDs: WIN_RATE_EASY, AVG_END_TURN_OVERALL, AVG_END_TURN_STANDARD, DEFEAT_SHARE_OVERALL

## 4. Acceptance Conclusion

- R3-05 validation does not meet KPI gates. Keep R3 in `not ready` state and continue targeted tuning.
