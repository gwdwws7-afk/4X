# R3-05 Final Balance Report

- Stage: R3 Balance and AI Tuning
- Task: R3-05 balance regression validation
- Date: 2026-04-23
- Validation base profile: `R222-r212-hard-defeat-balance-c`
- Verdict: `FAIL`

## 1. Run-Level KPI Score

| Run | Batch | KPI Pass | Win(E/S/H) | AvgEndTurn | Attrition | PathMonopoly |
|---|---|---:|---:|---:|---:|---:|
| V01 | R3-05-B002-R222-V01 | 8/13 | 0.6433/0.4725/0.2267 | 13.9950 | 0.2260 | 0.6800 |
| V02 | R3-05-B002-R222-V02 | 10/13 | 0.6667/0.4850/0.2933 | 13.8740 | 0.2020 | 0.6183 |
| V03 | R3-05-B002-R222-V03 | 9/13 | 0.6833/0.4300/0.1800 | 14.4770 | 0.2250 | 0.6148 |

## 2. KPI Stability Matrix

| KPI | Mean Actual | Pass Runs | Target |
|---|---:|---:|---:|
| WIN_RATE_EASY | 0.6644 | 2/3 | [0.6500, 0.8000] |
| WIN_RATE_STANDARD | 0.4625 | 2/3 | [0.4500, 0.5500] |
| WIN_RATE_HARD | 0.2333 | 2/3 | [0.2000, 0.3500] |
| WIN_RATE_OVERALL | 0.4543 | 3/3 | [0.4000, 0.6000] |
| AVG_END_TURN_OVERALL | 14.1153 | 1/3 | [14.0000, 20.0000] |
| AVG_END_TURN_STANDARD | 15.6867 | 3/3 | [15.0000, 19.0000] |
| ATTRITION_RATE_OVERALL | 0.2177 | 3/3 | [0.1000, 0.3500] |
| RESOURCE_CV_GOLDLEAF | 0.3740 | 3/3 | [0.1800, 0.4500] |
| RESOURCE_CV_FIREOIL | 0.1445 | 0/3 | [0.1500, 0.4000] |
| RESOURCE_CV_ARMS | 0.2053 | 2/3 | [0.2000, 0.5500] |
| VICTORY_SHARE_OVERALL | 0.4543 | 3/3 | [0.3500, 0.6500] |
| DEFEAT_SHARE_OVERALL | 0.3280 | 0/3 | [0.3500, 0.6500] |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.6377 | 3/3 | [0.0000, 0.7000] |

## 3. Gate Decision Rule

- Rule: all hard-gate KPIs must pass in all validation runs.
- Result: `FAIL`
- Failed KPI IDs: WIN_RATE_EASY, WIN_RATE_STANDARD, WIN_RATE_HARD, AVG_END_TURN_OVERALL, RESOURCE_CV_FIREOIL, RESOURCE_CV_ARMS, DEFEAT_SHARE_OVERALL

## 4. Acceptance Conclusion

- R3-05 validation does not meet KPI gates. Keep R3 in `not ready` state and continue targeted tuning.
