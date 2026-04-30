# R3-05 Balance Regression Validation (R82 Base)

- Stage: R3 Balance and AI Tuning (2026-05-25 ~ 2026-06-07)
- Task: R3-05 balance regression validation (3 x 1000-run, base=R82)
- Execution Date: 2026-04-21
- Result: FAIL (not ready for R3-06 sign-off)

## 1. Validation Setup

1. Base profile: `R82-r79-pace-backpull-a`.
2. Validation runs: `V01/V02/V03`, each 1000 simulations.
3. Gate rule: all hard-gate KPIs pass in all validation runs.

## 2. Run Summary

| Run | Batch | KPI Pass | Win(E/S/H) | Avg End Turn | Attrition | Path Monopoly |
|---|---|---:|---:|---:|---:|---:|
| V01 | R3-05-B002-R82-V01 | 10/13 | 0.7200 / 0.5050 / 0.2567 | 13.2570 | 0.1590 | 0.6788 |
| V02 | R3-05-B002-R82-V02 | 9/13 | 0.7133 / 0.4475 / 0.3100 | 13.7200 | 0.1770 | 0.7037 |
| V03 | R3-05-B002-R82-V03 | 12/13 | 0.6967 / 0.4100 / 0.2667 | 14.2370 | 0.1960 | 0.6711 |

## 3. Stability and Gate Result

- Verdict: `FAIL`
- Mean KPI highlights:
  - `WIN_RATE_EASY`: `0.7100` (PASS 3/3)
  - `WIN_RATE_STANDARD`: `0.4542` (PASS 1/3)
  - `AVG_END_TURN_OVERALL`: `13.7380` (PASS 1/3)
  - `DEFEAT_SHARE_OVERALL`: `0.3447` (PASS 1/3)
  - `SINGLE_PATH_VICTORY_MONOPOLY`: `0.6845` (PASS 2/3)

Failed KPI IDs:
- `WIN_RATE_STANDARD`
- `AVG_END_TURN_OVERALL`
- `AVG_END_TURN_STANDARD`
- `DEFEAT_SHARE_OVERALL`
- `SINGLE_PATH_VICTORY_MONOPOLY`

## 4. Regression Safety

- Post-validation `RunAllTests`: PASS (`400 passed, 0 failed`).
- Current blocker remains KPI stability under 3-run validation, not functional regression.

## 5. Artifacts

| Artifact | Path |
|---|---|
| Final report | `production/evidence/r3/validation/20260421_R3-05_FINAL-BALANCE-REPORT-R82.md` |
| Summary JSON | `production/evidence/r3/validation/20260421_R3-05_VALIDATION-SUMMARY-R82.json` |
| V01 raw results | `production/evidence/r3/validation/20260421_R3-05_VALIDATION-R82-V01.csv` |
| V01 KPI | `production/evidence/r3/validation/20260421_R3-05_VALIDATION-R82-V01-KPI.md` |
| V02 raw results | `production/evidence/r3/validation/20260421_R3-05_VALIDATION-R82-V02.csv` |
| V02 KPI | `production/evidence/r3/validation/20260421_R3-05_VALIDATION-R82-V02-KPI.md` |
| V03 raw results | `production/evidence/r3/validation/20260421_R3-05_VALIDATION-R82-V03.csv` |
| V03 KPI | `production/evidence/r3/validation/20260421_R3-05_VALIDATION-R82-V03-KPI.md` |
| Batch runtime log | `production/evidence/r3/validation/20260421_R3-05-R82_BATCH-RUN.log` |
| RunAll regression log | `production/evidence/r3/validation/20260421_R3-05-R82_RUNALL-REGRESSION.log` |
