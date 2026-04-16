# Regression Runbook (S1/S2/S3)

本手册规定 B1/B2/B3 回归场景的最小执行顺序，确保不同人跑出的结果可比较。

## Inputs

- 参数基线：`b1-b3-parameter-baseline.csv`
- 场景定义：`regression-scenarios.csv`
- 结果模板：`regression-results-template.csv`
- 后果账本：`consequence-ledger-template.csv`

## Run Order

1. S1 Steady State Baseline
2. S2 Blockade Escalation Pressure
3. S3 Main Route Cutover

## Per-Scenario Checklist

1. 锁定参数配置（记录 `parameter_profile` 名称）。
2. 清零历史状态（封锁压力、协议状态、路线状态）。
3. 按场景描述推进 `turns` 个回合。
4. 记录三项核心指标：
   - `actual_goldleaf_income_avg`
   - `actual_export_efficiency_avg`
   - `actual_main_route_uptime`
5. 记录失败风险事件次数（J系统风险触发）。
6. 根据场景 `pass_criteria` 写 `pass_fail`。

## Gating Rules

- 任一场景失败，参数批次不得晋级。
- 允许一次“热修参数”后重跑全套 S1/S2/S3；禁止只重跑单场景。
- 每次评审至少保留最近 3 轮结果。

## Suggested Cadence

- 设计阶段：每周 2 轮（参数快速收敛）。
- Alpha 前：每次合并平衡变更都跑 1 轮。
