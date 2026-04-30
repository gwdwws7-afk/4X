# R3-05 Balance Regression Validation (R136)

- Stage: R3 Balance and AI Tuning (2026-05-25 ~ 2026-06-07)
- Task: R3-05 validation probe on L-B003 candidate
- Execution Date: 2026-04-22
- Validation base profile: `R136-r134-avgturn-recover-a`
- Result: FAIL

## Gate Summary

- Run-level KPI pass: `11/13`, `12/13`, `12/13`
- Failing KPI IDs (union):
  - `AVG_END_TURN_OVERALL`
  - `DEFEAT_SHARE_OVERALL`

## Conclusion

`R136` achieved `13/13` in single-run tuning, but validation stability still misses the same two residual gates. Candidate remains `probe-only`.

## Artifacts

- Final report: `production/evidence/r3/validation/20260422_R3-05_FINAL-BALANCE-REPORT-R136.md`
- Summary JSON: `production/evidence/r3/validation/20260422_R3-05_VALIDATION-SUMMARY-R136.json`
- Runtime log: `production/evidence/r3/validation/20260422_R3-05-R136_VALIDATION.log`
