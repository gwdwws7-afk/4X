# R5-02 Profiling 基线报告模板

- 阶段：R5 性能与稳定性（2026-06-22 ~ 2026-06-28）
- 对应任务：R5-02 Profiling 热点分析与优化
- 执行入口：
  - `EventideAge/Run R5 Performance Baseline (R5-01)`
  - `R5StabilityRunner.RunPerformanceBaseline()`

## 1. 运行记录

| RunID | 日期 | 分支/提交 | 结果 | CSV | MD 报告 | 备注 |
|---|---|---|---|---|---|---|
| R5-02-RUN-01 | 2026-04-29 | workspace-head | PASS | `production/evidence/r5/baseline/20260429_155317_R5-01-PERF-BASELINE.csv` | `production/evidence/r5/baseline/20260429_155317_R5-01-PERF-BASELINE.md` | Unity batchmode executed via `TestMenu.RunR5PerformanceBaseline` |
| R5-02-RUN-02 |  |  |  |  |  |  |

## 2. KPI 对照（引用 R5-01 预算）

| KPI | 实测值 | 预算 | 状态(PASS/FAIL) | 结论 |
|---|---:|---:|---|---|
| TURN_AVG_MS | 0.042 | 16.67 | PASS | 明显低于预算 |
| TURN_P95_MS | 0.034 | 33.30 | PASS | 明显低于预算 |
| SAVELOAD_P95_MS | 23.375 | 900 | PASS | 明显低于预算 |
| MANAGED_PEAK_MB | 10.203 | 1200 | PASS | 峰值充足 |
| MANAGED_GROWTH_MB | 0.762 | 256 | PASS | 漂移可接受 |
| SAVELOAD_FAILURES | 0 | 0 | PASS | 未出现失败 |

## 3. 热点结论

| 热点模块 | 证据 | 影响等级 | 处置建议 | Owner | 状态 |
|---|---|---|---|---|---|
| 回合推进 | `TURN_AVG_MS / TURN_P95_MS` | 低 | 进入主场景实机再测 | Tech | Done |
| 存档读写 | `SAVELOAD_P95_MS` | 低 | 扩展到大存档样本做二次确认 | Tech | Done |
| UI刷新 | 当前脚本未覆盖渲染层 | 中 | 在 R5-03 引入实场景帧采样 | UI/QA | Open |

## 4. 修复与回归

| 缺陷/问题 | 修改文件 | 修复说明 | 回归结果 |
|---|---|---|---|
| LaunchFlow 编译阻塞（编码损坏） | `Assets/Scripts/UI/LaunchFlowUIController.cs` | 修复损坏字符串与插值语法 | PASS（R5 基线可执行） |
| L4 默认表字符串损坏 | `Assets/Scripts/Systems/L4/LocalizationTableConfig.cs` / `Assets/Scripts/Systems/L4/LocalizationSystem.cs` | 修复缺失引号与货币符号返回值 | PASS（Unity 编译通过） |

## 5. 签收

- [x] PASS：R5-02 达标
- [ ] CONDITIONAL PASS：需带约束进入 R5-03 / R5-04
- [ ] FAIL：阻塞，不可进入 R6
