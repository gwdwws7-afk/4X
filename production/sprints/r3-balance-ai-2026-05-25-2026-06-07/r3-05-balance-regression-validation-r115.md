# R3-05 Balance Regression Validation (R115)

- Stage: R3 Balance and AI Tuning (2026-05-25 ~ 2026-06-07)
- Task: R3-05 validation probe on J-B005 candidate
- Execution Date: 2026-04-22
- Validation base profile: `R115-r112-hard-recover-floor-a`
- Result: FAIL

## Gate Summary

- Run-level KPI pass: `10/13`, `11/13`, `11/13`
- Failing KPI IDs (union):
  - `WIN_RATE_EASY`
  - `AVG_END_TURN_OVERALL`
  - `DEFEAT_SHARE_OVERALL`

## Conclusion

`R115` closes hard-win instability versus `R112`, but still fails easy-floor + pacing floor + defeat-share floor gate. Not promotable.

## Artifacts

- Final report: `production/evidence/r3/validation/20260422_R3-05_FINAL-BALANCE-REPORT-R115.md`
- Summary JSON: `production/evidence/r3/validation/20260422_R3-05_VALIDATION-SUMMARY-R115.json`
- Runtime log: `production/evidence/r3/validation/20260422_R3-05-R115_VALIDATION.log`
