# Sprint B Batch-2 Closeout (2026-04-13)

## Scope
- GAP-04 resource-canonical convergence in runtime systems (`B1`/`B5`)
- Guardrail backfill for canonical resource-path regression coverage
- Gap status/evidence sync in design consistency docs

## Changes Delivered

1. B1 purchase entry converges to canonical resource ids
- File: `Assets/Scripts/Systems/B1/FinanceSystem.cs`
- Changes:
  - `TryPurchaseWithCurrency` now resolves input through `GameIds.ResolveResourceId(...)`
  - Settlement spend path unified into `TrySpendResource(...)`
  - Legacy channel ids (`GoldLeaves`/`TradeNotes`/`NorthCoins`) and canonical ids (`GoldLeaf`/`TradeToken`) now share one canonical execution path
  - `EconomicState` canonical fields introduced (`GoldLeaf`/`TradeToken`), with legacy accessors kept for migration compatibility

2. B5 canonical naming alignment follow-up
- File: `Assets/Scripts/Systems/B5/EconomicSettlementSystem.cs`
- Changes:
  - Internal trend baseline field renamed to canonical form (`_previousGoldLeafIncome`)
  - No runtime behavior change intended; aligns terminology with canonical resource naming

3. Regression coverage added for GAP-04 closure
- File: `Assets/Scripts/Tests/StandaloneTest.cs`
- Added tests:
  - `TestB1CurrencyCanonicalGuardrail`
  - `TestB5SettlementCanonicalResourceIdGuardrail`
- `RunAll()` updated to execute both tests

4. Gap/evidence docs synchronized
- Files:
  - `design/gdd/eventideage-unified-gdd.md`
  - `design/gdd/rules-consistency-matrix.md`
- Changes:
  - `GAP-04` marked `Closed (2026-04-13)`
  - `G2-ReleaseLock` blocker list updated to `GAP-03` only
  - Matrix row added for `GAP-04` closure evidence

## Verification
- Command entry: `EventideAge.Editor.TestMenu.RunAllTests` (Unity batch mode)
- Log: `Temp/sprintB_batch2.log`
- Result: `266 passed, 0 failed`

## Key Evidence Anchors
- `Temp/sprintB_batch2.log:2847` (`Testing B1 Currency Canonical Guardrail`)
- `Temp/sprintB_batch2.log:2966` (`B1 purchase path emits canonical resource ids only`)
- `Temp/sprintB_batch2.log:2979` (`Testing B5 Settlement Canonical ResourceId Guardrail`)
- `Temp/sprintB_batch2.log:3050` (`B5 settlement resource report uses canonical resource ids only`)
- `Temp/sprintB_batch2.log:5348` (`=== Results: 266 passed, 0 failed ===`)

## Sprint B Batch-2 Status
- Status: Completed
- GAP-04: Closed
- Remaining G2 blocker: GAP-03
