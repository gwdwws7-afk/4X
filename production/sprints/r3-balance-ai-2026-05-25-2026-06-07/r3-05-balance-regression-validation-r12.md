# R3-05 Balance Regression Validation (R12 Base)

- Stage: R3 Balance and AI Tuning (2026-05-25 ~ 2026-06-07)
- Task: R3-05 balance regression validation (3 x 1000-run, base=R12)
- Execution Date: 2026-04-20
- Result: FAIL (not ready for R3-06)

## 1. Validation Setup

1. Base profile: `R12-winrate-recover-diversify`
2. Validation runs: `V01/V02/V03`, each 1000 simulations.
3. Gate rule: all KPI hard-gates must pass in all runs.

## 2. Run Summary

| Run | Batch | KPI Pass | Win(E/S/H) | Avg End Turn | Attrition | Path Monopoly |
|---|---|---:|---:|---:|---:|---:|
| V01 | R3-05-B002-R12-V01 | 6/13 | 0.1533 / 0.5250 / 0.3933 | 14.3600 | 0.2500 | 0.8235 |
| V02 | R3-05-B002-R12-V02 | 6/13 | 0.1433 / 0.5350 / 0.3533 | 14.4960 | 0.2530 | 0.8430 |
| V03 | R3-05-B002-R12-V03 | 7/13 | 0.1700 / 0.5025 / 0.3367 | 14.7830 | 0.2560 | 0.8555 |

## 3. Stability and Gate Result

- Verdict: `FAIL`
- Mean KPI highlights:
  - `WIN_RATE_STANDARD`: `0.5208` (PASS 3/3)
  - `AVG_END_TURN_OVERALL`: `14.5463` (PASS 3/3)
  - `ATTRITION_RATE_OVERALL`: `0.2530` (PASS 3/3)
  - `SINGLE_PATH_VICTORY_MONOPOLY`: `0.8407` (FAIL 0/3)

Failed KPI IDs:
- `WIN_RATE_EASY`
- `WIN_RATE_HARD`
- `WIN_RATE_OVERALL`
- `AVG_END_TURN_STANDARD`
- `RESOURCE_CV_FIREOIL`
- `RESOURCE_CV_ARMS`
- `SINGLE_PATH_VICTORY_MONOPOLY`

## 4. Regression Safety

- Post-validation `RunAllTests`: PASS (`400 passed, 0 failed`).
- Current blocker remains KPI conformance, not functional stability.

## 5. Artifacts

| Artifact | Path |
|---|---|
| Final report | `production/evidence/r3/validation/20260420_R3-05_FINAL-BALANCE-REPORT-R12.md` |
| Summary JSON | `production/evidence/r3/validation/20260420_R3-05_VALIDATION-SUMMARY-R12.json` |
| V01 raw results | `production/evidence/r3/validation/20260420_R3-05_VALIDATION-R12-V01.csv` |
| V01 KPI | `production/evidence/r3/validation/20260420_R3-05_VALIDATION-R12-V01-KPI.md` |
| V02 raw results | `production/evidence/r3/validation/20260420_R3-05_VALIDATION-R12-V02.csv` |
| V02 KPI | `production/evidence/r3/validation/20260420_R3-05_VALIDATION-R12-V02-KPI.md` |
| V03 raw results | `production/evidence/r3/validation/20260420_R3-05_VALIDATION-R12-V03.csv` |
| V03 KPI | `production/evidence/r3/validation/20260420_R3-05_VALIDATION-R12-V03-KPI.md` |
| Batch runtime log | `production/evidence/r3/validation/20260420_R3-05-R12_BATCH-RUN.log` |
| RunAll regression log | `production/evidence/r3/validation/20260420_R3-05-R12_RUNALL-REGRESSION.log` |
