# R1-03 关键链路回放脚本与日志规范

- 阶段：R1 集成验收（2026-04-27 ~ 2026-05-10）
- 范围：RL-01 / RL-02 / RL-03 / RL-10
- 目标：关键链路可重复回放、日志可对齐验收表、结果可复验

## 1. 回放脚本入口
- 统一测试入口：`EventideAge.Tests.StandaloneTest.RunR1ReplayScenarios()`
- Unity 右键菜单入口：`TestRunner -> Run R1 Replay Scenarios (RL-01/02/03/10)`
- 全量入口：`StandaloneTest.RunAll()`（已包含 R1 回放场景）

## 2. 链路与脚本映射
| 链路ID | 目标链路 | 脚本方法 |
|---|---|---|
| RL-01 | 地图 -> 外交 -> 战报 -> 事件 | `TestR1ReplayRL01MapDiplomacyBattleEvent()` |
| RL-02 | 回合/阶段/AP 生命周期 | `TestR1ReplayRL02PhaseLifecycleReplay()` |
| RL-03 | 存档读写与跨回合一致 | `TestR1ReplayRL03SaveLoadCrossTurnReplay()` |
| RL-10 | A5/J 超时终局语义 | `TestR1ReplayRL10TimeoutEndgameReplay()` |

## 3. 日志输出目录
- 默认目录：`production/evidence/r1/replay-logs/`
- 文件命名：`yyyyMMdd_HHmmss_<runId>_<chainId>.log`
- 当前 runId：`R1-03`

## 4. 单行日志格式（机器可解析）
每一步输出一行 `|` 分隔字段：

```text
ts=<UTC-ISO8601>|run=<runId>|chain=<RL-ID>|step=<Sxx>|result=<PASS/FAIL>|turn=<int>|phase=<int>|ap=<int>|phaseAp=<int>|uap=<int>|action=<text>|expected=<text>|actual=<text>
```

- `ts`：UTC 时间戳（ISO-8601）
- `run`：当前回放批次号（固定 `R1-03`）
- `chain`：链路编号（RL-01/02/03/10）
- `step`：链路内步骤序号（S01...）
- `result`：步骤判定（PASS/FAIL）
- `turn/phase/ap/phaseAp/uap`：执行时状态快照
- `action/expected/actual`：动作、预期、实际

此外会输出两条元数据行：
- `meta=START`：链路回放开始
- `meta=END`：链路回放结束

## 5. 与验收表对齐规则
- `r1-01-acceptance-template.md`：`执行结果` 填链路最终 PASS/FAIL/BLOCKED。
- `r1-02-critical-replay-checklist.md`：`证据路径` 填对应日志文件路径。
- 失败步骤必须登记缺陷ID，并补一次复验日志（新文件，不覆盖旧文件）。

## 6. 验收门槛（R1-03）
- RL-01/02/03/10 四条脚本均可独立重复执行。
- 每条链路日志至少包含 `START + >=1步骤 + END`。
- 任一步骤 FAIL 时，链路结果判定为 FAIL。
