# R3-05 Balance Regression Validation (R99 Base)

- Stage: R3 Balance and AI Tuning (2026-05-25 ~ 2026-06-07)
- Task: R3-05 balance regression validation (3 x 1000-run, base=R99)
- Execution Date: 2026-04-21
- Result: FAIL (not ready for R3-06 sign-off)

## 1. Validation Setup

1. Base profile: `R99-r82-early-suppress-c`.
2. Validation runs: `V01/V02/V03`, each 1000 simulations.
3. Gate rule: all hard-gate KPIs pass in all validation runs.

## 2. Run Summary

| Run | Batch | KPI Pass | Win(E/S/H) | Avg End Turn | Attrition | Path Monopoly |
|---|---|---:|---:|---:|---:|---:|
| V01 | R3-05-B002-R99-V01 | 11/13 | 0.6867 / 0.5100 / 0.2367 | 13.4530 | 0.1760 | 0.6590 |
| V02 | R3-05-B002-R99-V02 | 11/13 | 0.6533 / 0.4875 / 0.3000 | 13.9580 | 0.2020 | 0.6528 |
| V03 | R3-05-B002-R99-V03 | 11/13 | 0.7000 / 0.4550 / 0.2300 | 13.9250 | 0.1930 | 0.6659 |

## 3. Stability and Gate Result

- Verdict: `FAIL`
- Mean KPI highlights:
  - `WIN_RATE_EASY`: `0.6800` (PASS 3/3)
  - `WIN_RATE_STANDARD`: `0.4842` (PASS 3/3)
  - `WIN_RATE_HARD`: `0.2556` (PASS 3/3)
  - `AVG_END_TURN_OVERALL`: `13.7787` (PASS 0/3)
  - `AVG_END_TURN_STANDARD`: `15.4325` (PASS 3/3)
  - `DEFEAT_SHARE_OVERALL`: `0.3353` (PASS 0/3)
  - `SINGLE_PATH_VICTORY_MONOPOLY`: `0.6593` (PASS 3/3)

Failed KPI IDs:
- `AVG_END_TURN_OVERALL`
- `DEFEAT_SHARE_OVERALL`

## 4. Regression Safety

- Post-validation `RunAllTests`: PASS (`400 passed, 0 failed`).
- Remaining blocker is KPI floor convergence (pacing + defeat share), not regression stability.

## 5. Artifacts

| Artifact | Path |
|---|---|
| Final report | `production/evidence/r3/validation/20260421_R3-05_FINAL-BALANCE-REPORT-R99.md` |
| Summary JSON | `production/evidence/r3/validation/20260421_R3-05_VALIDATION-SUMMARY-R99.json` |
| V01 raw results | `production/evidence/r3/validation/20260421_R3-05_VALIDATION-R99-V01.csv` |
| V01 KPI | `production/evidence/r3/validation/20260421_R3-05_VALIDATION-R99-V01-KPI.md` |
| V02 raw results | `production/evidence/r3/validation/20260421_R3-05_VALIDATION-R99-V02.csv` |
| V02 KPI | `production/evidence/r3/validation/20260421_R3-05_VALIDATION-R99-V02-KPI.md` |
| V03 raw results | `production/evidence/r3/validation/20260421_R3-05_VALIDATION-R99-V03.csv` |
| V03 KPI | `production/evidence/r3/validation/20260421_R3-05_VALIDATION-R99-V03-KPI.md` |
| Batch runtime log | `production/evidence/r3/validation/20260421_R3-05-R99_BATCH-RUN_retry.log` |
| RunAll regression log | `production/evidence/r3/validation/20260421_R3-05-R99_RUNALL-REGRESSION_retry.log` |
