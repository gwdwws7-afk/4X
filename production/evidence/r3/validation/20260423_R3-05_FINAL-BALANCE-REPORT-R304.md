# R3-05 Final Balance Report

- Stage: R3 Balance and AI Tuning
- Task: R3-05 balance regression validation
- Date: 2026-04-23
- Validation base profile: `R304-r296-variance-compress-b`
- Verdict: `FAIL`

## 1. Run-Level KPI Score

| Run | Batch | KPI Pass | Win(E/S/H) | AvgEndTurn | Attrition | PathMonopoly |
|---|---|---:|---:|---:|---:|---:|
| V01 | R3-05-B002-R304-V01 | 11/13 | 0.6233/0.4250/0.2067 | 14.9350 | 0.2220 | 0.6683 |
| V02 | R3-05-B002-R304-V02 | 11/13 | 0.6400/0.4575/0.2733 | 14.8310 | 0.2100 | 0.6368 |
| V03 | R3-05-B002-R304-V03 | 11/13 | 0.6533/0.3975/0.1733 | 14.9600 | 0.2150 | 0.6265 |

## 2. KPI Stability Matrix

| KPI | Mean Actual | Pass Runs | Target |
|---|---:|---:|---:|
| WIN_RATE_EASY | 0.6389 | 1/3 | [0.6500, 0.8000] |
| WIN_RATE_STANDARD | 0.4267 | 1/3 | [0.4500, 0.5500] |
| WIN_RATE_HARD | 0.2178 | 2/3 | [0.2000, 0.3500] |
| WIN_RATE_OVERALL | 0.4277 | 3/3 | [0.4000, 0.6000] |
| AVG_END_TURN_OVERALL | 14.9087 | 3/3 | [14.0000, 20.0000] |
| AVG_END_TURN_STANDARD | 16.0125 | 3/3 | [15.0000, 19.0000] |
| ATTRITION_RATE_OVERALL | 0.2157 | 3/3 | [0.1000, 0.3500] |
| RESOURCE_CV_GOLDLEAF | 0.4157 | 3/3 | [0.1800, 0.4500] |
| RESOURCE_CV_FIREOIL | 0.1820 | 3/3 | [0.1500, 0.4000] |
| RESOURCE_CV_ARMS | 0.2711 | 3/3 | [0.2000, 0.5500] |
| VICTORY_SHARE_OVERALL | 0.4277 | 3/3 | [0.3500, 0.6500] |
| DEFEAT_SHARE_OVERALL | 0.3567 | 2/3 | [0.3500, 0.6500] |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.6439 | 3/3 | [0.0000, 0.7000] |

## 3. Gate Decision Rule

- Rule: all hard-gate KPIs must pass in all validation runs.
- Result: `FAIL`
- Failed KPI IDs: WIN_RATE_EASY, WIN_RATE_STANDARD, WIN_RATE_HARD, DEFEAT_SHARE_OVERALL

## 4. Acceptance Conclusion

- R3-05 validation does not meet KPI gates. Keep R3 in `not ready` state and continue targeted tuning.
