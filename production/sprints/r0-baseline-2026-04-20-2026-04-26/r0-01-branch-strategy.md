# R0-01 Branch Strategy (2026-04-16)

## Baseline Branch
- Baseline branch: `release/baseline-2026Q2`
- Source branch: `master`
- Purpose: freeze release baseline inputs and reduce integration drift for R0-R1.

## Proposed Protection Rules
1. Require PR review for merge into `release/baseline-2026Q2`.
2. Require Unity batch gate pass: `EventideAge.Editor.TestMenu.RunAllTests`.
3. Disallow force-push.
4. Require at least 1 reviewer (Tech Lead or QA Lead).
5. Require scope label on every PR (`core/economy/diplomacy/military/ui/meta`).
6. Hotfix branches use `hotfix/r0-*` and must back-merge to `master`.

## Merge Policy During R0
- Allowed changes: blocker bug fix, test stabilization, build-fix, release doc sync.
- Deferred changes: feature expansion, non-blocking refactor, broad UI rework.
- Daily baseline cut: 18:00 (Asia/Shanghai).

