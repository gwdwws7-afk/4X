# R4-04 Copy Readability & Localization Closeout

- Stage: R4 UI Productization (2026-06-08 ~ 2026-06-21)
- Task: R4-04 (copy readability / state explainability)
- Slice: L4 localization wiring for four high-frequency surfaces
- Execution Date: 2026-04-30
- Status: PASS

## 1. Scope Closed in This Slice

Closed the remaining `MISSING` localization keys used by the four highest-frequency surfaces:

1. `ui.map.hotspot`
2. `ui.diplomacy.action.locked`
3. `ui.report.summary`
4. `ui.event.option.preview`

## 2. File-Level Integration Map

| File | Integration |
|---|---|
| `Assets/Scripts/UI/MapPanelUI.cs` | Critical map latest line now prefixes localized `ui.map.hotspot`. |
| `Assets/Scripts/UI/DiplomacyPanelUI.cs` | Locked diplomacy latest line now prefixes localized `ui.diplomacy.action.locked`. |
| `Assets/Scripts/UI/ActionLogUI.cs` | Summary stage header now uses localized `ui.report.summary`. |
| `Assets/Scripts/UI/EventPanelUI.cs` | Story-event latest line now prefixes localized `ui.event.option.preview`. |
| `Assets/Scripts/Systems/L4/LocalizationTableConfig.cs` | Added table entries for the 4 keys (`zh-CN`, `zh-TW`, `en-US`). |
| `production/release/l4-localization-copy-inventory-v1.csv` | Updated key status from `MISSING` to `IN_USE`. |

## 3. Regression Guardrail Added

New automated guardrail:

- `TestUiLocalizationHighFrequencySurfaceLabelsGuardrail`

Coverage:

1. `zh-CN` labels are rendered on Map / Diplomacy / Report / Event.
2. Locale switch `zh-CN -> en-US` updates labels correctly.
3. Wired into both:
   - `StandaloneTest.RunAll()`
   - `StandaloneTest.RunR4UiProductizationChecks()`

## 4. Evidence

1. Validation log:
   - `production/evidence/adhoc/20260430_R4UI_L10_LOCALIZATION_RECHECK.log`
2. Key outcomes:
   - `PASS_LINES=77`
   - `FAIL_LINES=0`
   - Batch exit: `return code 0`

## 5. Acceptance Decision

- Gate A: 4 previously missing keys are connected in runtime UI -> PASS
- Gate B: key inventory reflects `IN_USE` -> PASS
- Gate C: localization behavior has regression guardrail -> PASS

Decision: **accept this R4-04 localization closeout slice**.

## 6. Residual Notes

- `fa-IR / ar-SA / ru-RU` fields for newly added keys are still fallback-to-English in this slice.
- This is non-blocking for current R4 gate because runtime fallback is active and tested.
