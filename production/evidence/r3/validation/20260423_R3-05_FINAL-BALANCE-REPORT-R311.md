# R3-05 Final Balance Report

- Stage: R3 Balance and AI Tuning
- Task: R3-05 balance regression validation
- Date: 2026-04-23
- Validation base profile: `R311-r307-defeat-floor-lift-a`
- Verdict: `PASS`

## 1. Run-Level KPI Score

| Run | Batch | KPI Pass | Win(E/S/H) | AvgEndTurn | Attrition | PathMonopoly |
|---|---|---:|---:|---:|---:|---:|
| V01 | R3-05-B002-R311-V01 | 13/13 | 0.6567/0.4625/0.2200 | 14.3970 | 0.2100 | 0.6473 |
| V02 | R3-05-B002-R311-V02 | 13/13 | 0.6633/0.4950/0.2833 | 14.4570 | 0.1880 | 0.6411 |
| V03 | R3-05-B002-R311-V03 | 13/13 | 0.6633/0.4625/0.2300 | 14.3480 | 0.1910 | 0.6313 |

## 2. KPI Stability Matrix

| KPI | Mean Actual | Pass Runs | Target |
|---|---:|---:|---:|
| WIN_RATE_EASY | 0.6611 | 3/3 | [0.6500, 0.8000] |
| WIN_RATE_STANDARD | 0.4733 | 3/3 | [0.4500, 0.5500] |
| WIN_RATE_HARD | 0.2444 | 3/3 | [0.2000, 0.3500] |
| WIN_RATE_OVERALL | 0.4610 | 3/3 | [0.4000, 0.6000] |
| AVG_END_TURN_OVERALL | 14.4007 | 3/3 | [14.0000, 20.0000] |
| AVG_END_TURN_STANDARD | 15.2842 | 3/3 | [15.0000, 19.0000] |
| ATTRITION_RATE_OVERALL | 0.1963 | 3/3 | [0.1000, 0.3500] |
| RESOURCE_CV_GOLDLEAF | 0.4120 | 3/3 | [0.1800, 0.4500] |
| RESOURCE_CV_FIREOIL | 0.1818 | 3/3 | [0.1500, 0.4000] |
| RESOURCE_CV_ARMS | 0.2691 | 3/3 | [0.2000, 0.5500] |
| VICTORY_SHARE_OVERALL | 0.4610 | 3/3 | [0.3500, 0.6500] |
| DEFEAT_SHARE_OVERALL | 0.3427 | 3/3 | [0.3300, 0.6500] |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.6399 | 3/3 | [0.0000, 0.7000] |

## 3. Gate Decision Rule

- Rule: all hard-gate KPIs must pass in all validation runs.
- Result: `PASS`
- Failed KPI IDs: (none)

## 4. Acceptance Conclusion

- R3-05 validation meets KPI gates. Ready to proceed to R3-06 sign-off.
