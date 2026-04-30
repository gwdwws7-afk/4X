# R3-05 Final Balance Report

- Stage: R3 Balance and AI Tuning
- Task: R3-05 balance regression validation
- Date: 2026-04-23
- Validation base profile: `R343-auto-grid-r311`
- Verdict: `FAIL`

## 1. Run-Level KPI Score

| Run | Batch | KPI Pass | Win(E/S/H) | AvgEndTurn | Attrition | PathMonopoly |
|---|---|---:|---:|---:|---:|---:|
| V01 | R3-05-B002-R343-V01 | 13/13 | 0.6733/0.4550/0.2033 | 14.4070 | 0.2030 | 0.6517 |
| V02 | R3-05-B002-R343-V02 | 12/13 | 0.6500/0.4900/0.2800 | 14.5580 | 0.1980 | 0.6379 |
| V03 | R3-05-B002-R343-V03 | 11/13 | 0.6467/0.4450/0.2333 | 14.4740 | 0.2060 | 0.6267 |

## 2. KPI Stability Matrix

| KPI | Mean Actual | Pass Runs | Target |
|---|---:|---:|---:|
| WIN_RATE_EASY | 0.6567 | 2/3 | [0.6500, 0.8000] |
| WIN_RATE_STANDARD | 0.4633 | 2/3 | [0.4500, 0.5500] |
| WIN_RATE_HARD | 0.2389 | 3/3 | [0.2000, 0.3500] |
| WIN_RATE_OVERALL | 0.4540 | 3/3 | [0.4000, 0.6000] |
| AVG_END_TURN_OVERALL | 14.4797 | 3/3 | [14.0000, 20.0000] |
| AVG_END_TURN_STANDARD | 15.3842 | 3/3 | [15.0000, 19.0000] |
| ATTRITION_RATE_OVERALL | 0.2023 | 3/3 | [0.1000, 0.3500] |
| RESOURCE_CV_GOLDLEAF | 0.4135 | 3/3 | [0.1800, 0.4500] |
| RESOURCE_CV_FIREOIL | 0.1830 | 3/3 | [0.1500, 0.4000] |
| RESOURCE_CV_ARMS | 0.2685 | 3/3 | [0.2000, 0.5500] |
| VICTORY_SHARE_OVERALL | 0.4540 | 3/3 | [0.3500, 0.6500] |
| DEFEAT_SHARE_OVERALL | 0.3437 | 2/3 | [0.3500, 0.6500] |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.6388 | 3/3 | [0.0000, 0.7000] |

## 3. Gate Decision Rule

- Rule: all hard-gate KPIs must pass in all validation runs.
- Result: `FAIL`
- Failed KPI IDs: WIN_RATE_EASY, WIN_RATE_STANDARD, DEFEAT_SHARE_OVERALL

## 4. Acceptance Conclusion

- R3-05 validation does not meet KPI gates. Keep R3 in `not ready` state and continue targeted tuning.
