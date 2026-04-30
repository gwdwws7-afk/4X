# R3-04L Targeted Retuning

- Stage: R3 Balance and AI Tuning (2026-05-25 ~ 2026-06-07)
- Task: R3-04L targeted retuning (close residual `AVG_END_TURN_OVERALL` + `DEFEAT_SHARE_OVERALL`)
- Execution Date: 2026-04-22
- Result: COMPLETE (B001~B003 executed, best single-run reached 13/13)

## 1. Delivered Scope

1. Added dedicated L execution entry (`RunR304LRetuning`) and menu/context hooks.
2. Executed three L batches (`B001/B002/B003`) with fixed-seed comparable window.
3. Achieved single-run full gate pass on `R136 (13/13)`.
4. Ran R3-05 validation probe on `R136` and captured stability result.
5. Restored default validation baseline priority to `R99` after probe.

## 2. Code Changes

| File | Change |
|---|---|
| `Assets/Scripts/Tests/R3TuningRunner.cs` | Added `RunR304LRetuning`, `BuildR304LProfiles`, and updated resolver order (`R99` first, L candidates retained as secondary probes) |
| `Assets/Editor/TestMenu.cs` | Added menu `EventideAge/Run R3 Targeted Retuning (R3-04L)` |
| `Assets/Scripts/Tests/TestRunner.cs` | Added context menu `Run R3 Targeted Retuning (R3-04L)` |

## 3. Batch Summary

| Batch | Profiles | Best | Best KPI Pass | Remaining Gaps |
|---|---|---|---:|---|
| B001 | R130/R131/R132 | R132 | 10/13 | WIN_RATE_STANDARD, WIN_RATE_HARD, SINGLE_PATH_VICTORY_MONOPOLY |
| B002 | R133/R134/R135 | R134 | 12/13 | AVG_END_TURN_OVERALL |
| B003 | R136/R137/R138 | R136 | 13/13 | (none, single-run) |

## 4. Final Batch (B003) KPI Snapshot

| Round | KPI Pass | Win(E/S/H) | Avg End Turn | Defeat Share | Path Monopoly |
|---|---:|---:|---:|---:|---:|
| R136 | 13/13 | 0.6767 / 0.4625 / 0.2333 | 14.1640 | 0.3510 | 0.6332 |
| R137 | 12/13 | 0.6367 / 0.4600 / 0.2200 | 14.1900 | 0.3590 | 0.6304 |
| R138 | 12/13 | 0.6300 / 0.4550 / 0.2367 | 14.3300 | 0.3540 | 0.6290 |

Selected best (single-run): `R136`.

## 5. Validation Probe (R136)

- R3-05 verdict: `FAIL`
- Run-level KPI pass: `11/13`, `12/13`, `12/13`
- Failed KPI IDs: `AVG_END_TURN_OVERALL`, `DEFEAT_SHARE_OVERALL`

Interpretation:
- `R136` has reached a strong single-run optimum, but cross-seed stability still misses the same two residual gates.

## 6. Promotion Decision

- `R136` is **not promoted** as default R3-05 baseline yet.
- Default baseline remains `R99`; L profiles are retained as secondary candidates.

## 7. Artifacts

| Artifact | Path |
|---|---|
| L impact report (latest B003) | `production/evidence/r3/tuning/20260422_R3-04L_TARGETED-IMPACT-REPORT.md` |
| B001 runtime log | `production/evidence/r3/tuning/20260422_R3-04L_BATCH-RUN_B001.log` |
| B002 runtime log | `production/evidence/r3/tuning/20260422_R3-04L_BATCH-RUN_B002.log` |
| B003 runtime log | `production/evidence/r3/tuning/20260422_R3-04L_BATCH-RUN_B003.log` |
| R136 KPI | `production/evidence/r3/tuning/20260422_R3-04L_TARGETED-R136-KPI.md` |
| R136 validation report | `production/evidence/r3/validation/20260422_R3-05_FINAL-BALANCE-REPORT-R136.md` |
| R136 validation summary | `production/evidence/r3/validation/20260422_R3-05_VALIDATION-SUMMARY-R136.json` |

## 8. Conclusion

R3-04L produced the first 13/13 single-run candidate in this lane (`R136`), but R3-05 stability acceptance is still blocked by the two longstanding residual gaps under multi-run validation.
