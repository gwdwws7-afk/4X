# R6 RC Go/No-Go Gate Checklist

- Stage: R6 RC and Release (2026-06-29 ~ 2026-07-06)
- Purpose: Unified release gate checklist before Go/No-Go meeting
- Owner: Producer / Tech Lead / QA Lead

## 1. Build & Regression Gate

| Gate | Criteria | Status | Evidence |
|---|---|---|---|
| Build Clean | No compile errors in target branch | TODO |  |
| Full Automation | All automated tests pass | TODO |  |
| P0 Defects | P0 = 0 | TODO |  |
| P1 Defects | P1 controlled and accepted | TODO |  |

## 2. System Acceptance Gate

| Gate | Criteria | Status | Evidence |
|---|---|---|---|
| R1 Chain Replay | Main chain replayable end-to-end | TODO |  |
| R2 Content Lock | 24-turn campaign no empty turn/no silent stage | TODO |  |
| R3 Balance | KPI in target range | TODO |  |
| R4 UI | Map/Diplomacy/Report/Event meet acceptance | PARTIAL | `production/sprints/r4-ui-productization-2026-06-08-2026-06-21/r4-02-prefab-feedback-implementation.md`; `production/sprints/r4-ui-productization-2026-06-08-2026-06-21/r4-04-copy-readability-localization-closeout.md`; `production/sprints/r4-ui-productization-2026-06-08-2026-06-21/r4-05-ui-regression-multi-resolution-input.md`; `production/sprints/r4-ui-productization-2026-06-08-2026-06-21/r4-06-ux-acceptance-signoff.md`; `production/evidence/r4/validation/20260430_151648_R4-06_UX_VISUAL_AUDIT_RUN01.csv`; `production/evidence/r1/replay-logs/20260430_151631_R1-03_RL-01.log`; `production/sprints/r4-ui-productization-2026-06-08-2026-06-21/r4-06-manual-review-workitems-run02.md`; `production/sprints/r4-ui-productization-2026-06-08-2026-06-21/r4-06-signoff-ready-summary-run02.md` |
| R5 Stability | Perf budget and soak gate pass | TODO |  |

## 3. Release Readiness Gate

| Gate | Criteria | Status | Evidence |
|---|---|---|---|
| Release Notes | Complete and reviewed | TODO |  |
| Known Issues | Public list reviewed and acceptable | TODO |  |
| Rollback Plan | Rollback rehearsal pass | TODO |  |
| Save Compatibility | Current RC can read supported save versions | TODO |  |
| Localization | L4 critical copy coverage accepted | PARTIAL | `production/sprints/r4-ui-productization-2026-06-08-2026-06-21/r4-04-copy-readability-localization-closeout.md`; `production/evidence/adhoc/20260430_R4UI_L10_LOCALIZATION_RECHECK.log` |

## 4. Final Decision

- Go/No-Go Meeting Date: `YYYY-MM-DD`
- Decision: `GO / CONDITIONAL GO / NO-GO`
- Decision Notes:
  - 

## 5. Signoff

- Producer: __________ Date: __________
- Tech Lead: __________ Date: __________
- QA Lead: __________ Date: __________
