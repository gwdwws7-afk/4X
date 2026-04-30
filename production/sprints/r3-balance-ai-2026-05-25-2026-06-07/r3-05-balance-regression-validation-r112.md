# R3-05 Balance Regression Validation (R112)

- Stage: R3 Balance and AI Tuning (2026-05-25 ~ 2026-06-07)
- Task: R3-05 validation probe on J-B004 candidate
- Execution Date: 2026-04-22
- Validation base profile: `R112-r106-hard-pace-standard-floor-a`
- Result: FAIL

## Gate Summary

- Run-level KPI pass: `10/13`, `11/13`, `12/13`
- Failing KPI IDs (union):
  - `WIN_RATE_EASY`
  - `WIN_RATE_HARD`
  - `AVG_END_TURN_OVERALL`
  - `DEFEAT_SHARE_OVERALL`
  - `SINGLE_PATH_VICTORY_MONOPOLY`

## Conclusion

`R112` is not promotable as R3-05 baseline.

## Artifacts

- Final report: `production/evidence/r3/validation/20260422_R3-05_FINAL-BALANCE-REPORT-R112.md`
- Summary JSON: `production/evidence/r3/validation/20260422_R3-05_VALIDATION-SUMMARY-R112.json`
- Runtime log: `production/evidence/r3/validation/20260422_R3-05-R112_VALIDATION.log`
