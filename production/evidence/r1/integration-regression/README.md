# R1 Integration Regression Evidence

- Stage: R1 Integration Acceptance
- Task: R1-06 Integration Issue Fixes + Regression
- Date: 2026-04-16

## Evidence Index

| Type | Description | Path |
|---|---|---|
| Baseline failure evidence | RL-03 failed at S02 before fix (`result=FAIL`) | `production/evidence/r1/replay-logs/20260416_164950_R1-03_RL-03.log` |
| Post-fix targeted replay evidence | RL-03 S01/S02/S03 all PASS after fix | `production/evidence/r1/replay-logs/20260416_165344_R1-03_RL-03.log` |
| Post-fix full regression evidence | `RunAllTests` full run summary `319 passed, 0 failed` | `production/evidence/r1/integration-regression/20260416_R1-06_RUN-FULL-REGRESSION_PASS.log` |
