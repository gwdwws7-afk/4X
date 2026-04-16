# Balance Regression Assets

本目录用于把 B1/B2/B3 的平衡迭代从“人工感觉调参”升级为“可回归验证”。

## 文件清单

- `b1-b3-parameter-baseline.csv`
  - 参数基线表（当前锁定参考值、可调范围、归属系统）。
- `regression-scenarios.csv`
  - 固定回归场景定义（至少覆盖平稳/封锁升级/主路线中断绕行）。
- `regression-results-template.csv`
  - 每次测试批次记录模板（输入参数、输出指标、是否通过）。
- `consequence-ledger-template.csv`
  - 行动后果追踪账本模板（对应 Unified GDD 5.4）。
- `regression-runbook.md`
  - 固定场景执行顺序与门禁规则（S1/S2/S3）。
- `ui-consequence-surface-map.md`
  - 行动后果在 UI 的 canonical 映射与 Bound/Gap 状态。

## 执行流程

1. 基于 `b1-b3-parameter-baseline.csv` 建立本轮参数分支。
2. 按 `regression-runbook.md` 规定顺序执行 `S1 -> S2 -> S3`。
3. 将结果填入 `regression-results-template.csv`。
4. 核对 `consequence-ledger-template.csv` 与 `ui-consequence-surface-map.md` 的口径一致性。
5. 任一场景不满足期望区间，本轮参数不得标记为可发布。

## 门禁规则

- 不允许只改参数、不留回归结果。
- 不允许只跑单一“顺风局”样本。
- 版本评审时必须能追溯：参数改动 -> 场景结果 -> 结论。
- 行动后果若在映射中为 `Gap`，不得宣称“已完成可感知后果闭环”。
