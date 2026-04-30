# R3-05 Balance Regression Validation (R14 Base)

- Stage: R3 Balance and AI Tuning (2026-05-25 ~ 2026-06-07)
- Task: R3-05 balance regression validation (3 x 1000-run, base=R14)
- Execution Date: 2026-04-20
- Result: FAIL (not ready for R3-06 sign-off)

## 1. Validation Setup

1. Base profile: `R14-duration-volatility`.
2. Validation runs: `V01/V02/V03`, each 1000 simulations.
3. Gate rule: all hard-gate KPIs pass in all validation runs.

## 2. Run Summary

| Run | Batch | KPI Pass | Win(E/S/H) | Avg End Turn | Attrition | Path Monopoly |
|---|---|---:|---:|---:|---:|---:|
| V01 | R3-05-B002-R14-V01 | 7/13 | 0.3667 / 0.0625 / 0.0300 | 14.5170 | 0.2580 | 1.0000 |
| V02 | R3-05-B002-R14-V02 | 7/13 | 0.3067 / 0.0650 / 0.0300 | 15.1150 | 0.2760 | 1.0000 |
| V03 | R3-05-B002-R14-V03 | 7/13 | 0.3300 / 0.0525 / 0.0300 | 14.7100 | 0.2690 | 1.0000 |

## 3. Stability and Gate Result

- Verdict: `FAIL`
- Failed KPI IDs:
  - `WIN_RATE_EASY`
  - `WIN_RATE_STANDARD`
  - `WIN_RATE_HARD`
  - `WIN_RATE_OVERALL`
  - `VICTORY_SHARE_OVERALL`
  - `SINGLE_PATH_VICTORY_MONOPOLY`

## 4. Regression Safety

- `RunAllTests`: PASS (`400 passed, 0 failed`).
- Current blocker remains balance KPI conformance, not runtime stability.

## 5. Artifacts

| Artifact | Path |
|---|---|
| Final report | `production/evidence/r3/validation/20260420_R3-05_FINAL-BALANCE-REPORT-R14.md` |
| Summary JSON | `production/evidence/r3/validation/20260420_R3-05_VALIDATION-SUMMARY-R14.json` |
| V01 raw results | `production/evidence/r3/validation/20260420_R3-05_VALIDATION-R14-V01.csv` |
| V01 KPI | `production/evidence/r3/validation/20260420_R3-05_VALIDATION-R14-V01-KPI.md` |
| V02 raw results | `production/evidence/r3/validation/20260420_R3-05_VALIDATION-R14-V02.csv` |
| V02 KPI | `production/evidence/r3/validation/20260420_R3-05_VALIDATION-R14-V02-KPI.md` |
| V03 raw results | `production/evidence/r3/validation/20260420_R3-05_VALIDATION-R14-V03.csv` |
| V03 KPI | `production/evidence/r3/validation/20260420_R3-05_VALIDATION-R14-V03-KPI.md` |
| Batch runtime log | `production/evidence/r3/validation/20260420_R3-05-R14_BATCH-RUN.log` |
| RunAll regression log | `production/evidence/r3/validation/20260420_R3-05-R14_RUNALL-REGRESSION.log` |
