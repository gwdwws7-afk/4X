# R3-05 Balance Regression Validation (R21 Base)

- Stage: R3 Balance and AI Tuning (2026-05-25 ~ 2026-06-07)
- Task: R3-05 balance regression validation (3 x 1000-run, base=R21)
- Execution Date: 2026-04-20
- Result: FAIL (not ready for R3-06 sign-off)

## 1. Validation Setup

1. Base profile: `R21-win-floor-guard`.
2. Validation runs: `V01/V02/V03`, each 1000 simulations.
3. Gate rule: all hard-gate KPIs pass in all validation runs.

## 2. Run Summary

| Run | Batch | KPI Pass | Win(E/S/H) | Avg End Turn | Attrition | Path Monopoly |
|---|---|---:|---:|---:|---:|---:|
| V01 | R3-05-B002-R21-V01 | 9/13 | 0.6233 / 0.4325 / 0.1400 | 14.6970 | 0.2200 | 0.9876 |
| V02 | R3-05-B002-R21-V02 | 10/13 | 0.6100 / 0.4500 / 0.1600 | 15.0360 | 0.2180 | 0.9659 |
| V03 | R3-05-B002-R21-V03 | 8/13 | 0.5967 / 0.4250 / 0.1667 | 14.8670 | 0.2310 | 0.9925 |

## 3. Stability and Gate Result

- Verdict: `FAIL`
- Mean KPI highlights:
  - `AVG_END_TURN_OVERALL`: `14.8667` (PASS 3/3)
  - `AVG_END_TURN_STANDARD`: `16.2217` (PASS 3/3)
  - `ATTRITION_RATE_OVERALL`: `0.2230` (PASS 3/3)
  - `VICTORY_SHARE_OVERALL`: `0.4040` (PASS 3/3)
  - `SINGLE_PATH_VICTORY_MONOPOLY`: `0.9820` (FAIL 0/3)

Failed KPI IDs:
- `WIN_RATE_EASY`
- `WIN_RATE_STANDARD`
- `WIN_RATE_HARD`
- `WIN_RATE_OVERALL`
- `SINGLE_PATH_VICTORY_MONOPOLY`

## 4. Regression Safety

- Post-validation `RunAllTests`: PASS (`400 passed, 0 failed`).
- Current blocker remains KPI conformance, not functional stability.

## 5. Artifacts

| Artifact | Path |
|---|---|
| Final report | `production/evidence/r3/validation/20260420_R3-05_FINAL-BALANCE-REPORT-R21.md` |
| Summary JSON | `production/evidence/r3/validation/20260420_R3-05_VALIDATION-SUMMARY-R21.json` |
| V01 raw results | `production/evidence/r3/validation/20260420_R3-05_VALIDATION-R21-V01.csv` |
| V01 KPI | `production/evidence/r3/validation/20260420_R3-05_VALIDATION-R21-V01-KPI.md` |
| V02 raw results | `production/evidence/r3/validation/20260420_R3-05_VALIDATION-R21-V02.csv` |
| V02 KPI | `production/evidence/r3/validation/20260420_R3-05_VALIDATION-R21-V02-KPI.md` |
| V03 raw results | `production/evidence/r3/validation/20260420_R3-05_VALIDATION-R21-V03.csv` |
| V03 KPI | `production/evidence/r3/validation/20260420_R3-05_VALIDATION-R21-V03-KPI.md` |
| Batch runtime log | `production/evidence/r3/validation/20260420_R3-05-R21_BATCH-RUN.log` |
| RunAll regression log | `production/evidence/r3/validation/20260420_R3-05-R21_RUNALL-REGRESSION.log` |
