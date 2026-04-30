# R3-05 Final Balance Report

- Stage: R3 Balance and AI Tuning
- Task: R3-05 balance regression validation
- Date: 2026-04-23
- Validation base profile: `R276-r262-fine-closure-e`
- Verdict: `FAIL`

## 1. Run-Level KPI Score

| Run | Batch | KPI Pass | Win(E/S/H) | AvgEndTurn | Attrition | PathMonopoly |
|---|---|---:|---:|---:|---:|---:|
| V01 | R3-05-B002-R276-V01 | 13/13 | 0.6500/0.4725/0.2267 | 14.2950 | 0.1910 | 0.6836 |
| V02 | R3-05-B002-R276-V02 | 12/13 | 0.6767/0.4900/0.3067 | 14.1910 | 0.1830 | 0.6395 |
| V03 | R3-05-B002-R276-V03 | 13/13 | 0.6700/0.4600/0.2000 | 14.2610 | 0.1800 | 0.6247 |

## 2. KPI Stability Matrix

| KPI | Mean Actual | Pass Runs | Target |
|---|---:|---:|---:|
| WIN_RATE_EASY | 0.6656 | 3/3 | [0.6500, 0.8000] |
| WIN_RATE_STANDARD | 0.4742 | 3/3 | [0.4500, 0.5500] |
| WIN_RATE_HARD | 0.2444 | 3/3 | [0.2000, 0.3500] |
| WIN_RATE_OVERALL | 0.4627 | 3/3 | [0.4000, 0.6000] |
| AVG_END_TURN_OVERALL | 14.2490 | 3/3 | [14.0000, 20.0000] |
| AVG_END_TURN_STANDARD | 15.2158 | 3/3 | [15.0000, 19.0000] |
| ATTRITION_RATE_OVERALL | 0.1847 | 3/3 | [0.1000, 0.3500] |
| RESOURCE_CV_GOLDLEAF | 0.4211 | 3/3 | [0.1800, 0.4500] |
| RESOURCE_CV_FIREOIL | 0.1840 | 3/3 | [0.1500, 0.4000] |
| RESOURCE_CV_ARMS | 0.2726 | 3/3 | [0.2000, 0.5500] |
| VICTORY_SHARE_OVERALL | 0.4627 | 3/3 | [0.3500, 0.6500] |
| DEFEAT_SHARE_OVERALL | 0.3527 | 2/3 | [0.3500, 0.6500] |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.6493 | 3/3 | [0.0000, 0.7000] |

## 3. Gate Decision Rule

- Rule: all hard-gate KPIs must pass in all validation runs.
- Result: `FAIL`
- Failed KPI IDs: DEFEAT_SHARE_OVERALL

## 4. Acceptance Conclusion

- R3-05 validation does not meet KPI gates. Keep R3 in `not ready` state and continue targeted tuning.
