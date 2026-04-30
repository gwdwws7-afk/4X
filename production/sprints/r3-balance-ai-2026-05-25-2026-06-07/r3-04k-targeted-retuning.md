# R3-04K Targeted Retuning

- Stage: R3 Balance and AI Tuning (2026-05-25 ~ 2026-06-07)
- Task: R3-04K targeted retuning (close residual `AVG_END_TURN_OVERALL` + `DEFEAT_SHARE_OVERALL`)
- Execution Date: 2026-04-22
- Result: COMPLETE (B001~B004 executed, no promotable candidate)

## 1. Delivered Scope

1. Added dedicated K execution entry (`RunR304KRetuning`) and menu/context hooks.
2. Executed four K batches (`B001/B002/B003/B004`) with fixed-seed comparable window.
3. Re-targeted parameters around `R99` baseline focusing on late-pressure and defeat-share uplift.
4. Preserved R3-05 default resolver baseline to `R99` (K candidates not auto-promoted).

## 2. Code Changes

| File | Change |
|---|---|
| `Assets/Scripts/Tests/R3TuningRunner.cs` | Added `RunR304KRetuning`, `BuildR304KProfiles`, updated base-profile resolver candidate pool/order |
| `Assets/Editor/TestMenu.cs` | Added menu `EventideAge/Run R3 Targeted Retuning (R3-04K)` |
| `Assets/Scripts/Tests/TestRunner.cs` | Added context menu `Run R3 Targeted Retuning (R3-04K)` |

## 3. Batch Summary

| Batch | Profiles | Best | Best KPI Pass | Remaining Gaps |
|---|---|---|---:|---|
| B001 | R118/R119/R120 | R119 | 10/13 | WIN_RATE_HARD, AVG_END_TURN_OVERALL, SINGLE_PATH_VICTORY_MONOPOLY |
| B002 | R121/R122/R123 | R121 | 11/13 | AVG_END_TURN_OVERALL, DEFEAT_SHARE_OVERALL |
| B003 | R124/R125/R126 | R124 | 11/13 | AVG_END_TURN_OVERALL, DEFEAT_SHARE_OVERALL |
| B004 | R127/R128/R129 | R127 | 11/13 | AVG_END_TURN_OVERALL, DEFEAT_SHARE_OVERALL |

## 4. Final Batch (B004) KPI Snapshot

| Round | KPI Pass | Win(E/S/H) | Avg End Turn | Defeat Share | Path Monopoly |
|---|---:|---:|---:|---:|---:|
| R127 | 11/13 | 0.6833 / 0.5075 / 0.2533 | 13.4850 | 0.3350 | 0.6653 |
| R128 | 11/13 | 0.6800 / 0.4175 / 0.2600 | 14.0570 | 0.3520 | 0.7216 |
| R129 | 10/13 | 0.6733 / 0.4350 / 0.2667 | 13.8680 | 0.3490 | 0.6996 |

Selected best (tie-break): `R127`.

## 5. Promotion Decision

- K candidates are **not promoted** to R3-05 base profile.
- Current default validation baseline remains: `R99`.

## 6. Artifacts

| Artifact | Path |
|---|---|
| K impact report (latest B004) | `production/evidence/r3/tuning/20260422_R3-04K_TARGETED-IMPACT-REPORT.md` |
| B001 runtime log | `production/evidence/r3/tuning/20260422_R3-04K_BATCH-RUN_B001.log` |
| B002 runtime log | `production/evidence/r3/tuning/20260422_R3-04K_BATCH-RUN_B002.log` |
| B003 runtime log | `production/evidence/r3/tuning/20260422_R3-04K_BATCH-RUN_B003.log` |
| B004 runtime log | `production/evidence/r3/tuning/20260422_R3-04K_BATCH-RUN_B004.log` |
| B004 R127 KPI | `production/evidence/r3/tuning/20260422_R3-04K_TARGETED-R127-KPI.md` |
| B004 R128 KPI | `production/evidence/r3/tuning/20260422_R3-04K_TARGETED-R128-KPI.md` |
| B004 R129 KPI | `production/evidence/r3/tuning/20260422_R3-04K_TARGETED-R129-KPI.md` |

## 7. Conclusion

R3-04K significantly narrowed search to a stable 11/13 band but did not close the final two residual gates simultaneously. Keep R3 as `not ready`, retain `R99` as validation baseline, and continue targeted retuning.
