# R2-02 事件池补齐并去重

- 阶段：R2 内容锁定（2026-05-11 ~ 2026-05-24）
- 任务：R2-02 事件池补齐并去重
- 执行日期：2026-04-16
- 结论：PASS

## 1. 本次交付

1. 新增可配置事件池资产结构（模板化）
2. I1 接入事件池加载流程
3. I1 增加去重机制（入队去重 + 已触发历史去重）
4. 补齐默认事件池最小可玩批次（9 条模板）
5. 补齐专项自动化护栏与回归证据

## 2. 代码落地点

| 文件 | 变更 |
|---|---|
| `Assets/Scripts/Systems/I1/EventPoolConfig.cs` | 新增 `EventTemplate` 与 `EventPoolConfig`，支持模板转运行时事件、默认模板集 |
| `Assets/Scripts/Systems/I1/EventSystem.cs` | 新增事件池加载、去重键、重复拦截、加载统计、默认自动加载开关 |
| `Assets/Scripts/Tests/StandaloneTest.cs` | 新增 `TestI1EventPoolBackfillAndDedupGuardrail()` 与 `RunR2EventPoolChecks()` |
| `Assets/Scripts/Tests/TestRunner.cs` | 新增 ContextMenu：`Run R2 Event Pool Checks (R2-02)` |
| `Assets/Editor/TestMenu.cs` | 新增菜单：`EventideAge/Run R2 Event Pool Checks` |
| `Assets/Editor/GameConfigGenerator.cs` | 新增菜单：`EventideAge/Generate Default EventPoolConfig` |

## 3. 去重口径（实现）

去重优先级：

1. 若存在 `TemplateId`：使用 `template:<TemplateId>` 作为唯一键
2. 否则使用语义组合键（`Name/Type/Trigger/Turn/Condition/Effects`）

判定规则：

1. 已在队列中的同键事件：拒绝入队
2. 已触发历史中的同键事件：默认拒绝再次入队
3. `AllowRepeat=true` 事件：允许跨历史重复，但仍禁止“同一时刻重复排队”

## 4. 默认事件池（最小批次）

- 默认模板数：9
- 覆盖类型：Narrative / PlayerResponse / Random / Endgame
- 覆盖触发：TurnBased / ConditionBased / Random

## 5. 自动化验证

| RunID | 范围 | 结果 | 证据 |
|---|---|---|---|
| R2-02-RUN-01 | `RunR2EventPoolChecks` 定向验证 | PASS（8/8） | `production/evidence/r2/event-pool/20260416_R2-02_EVENT-POOL-CHECKS.log` |
| R2-02-RUN-02 | `RunAllTests` 全量回归 | PASS（327 passed, 0 failed） | `production/evidence/r2/event-pool/20260416_R2-02_RUNALL-REGRESSION.log` |

## 6. 对 R2 后续任务的影响

1. R2-03（地图配置终版）：无阻塞，继续按地图锁版推进
2. R2-04（AI 策略集）：可直接绑定 I1 模板触发反馈
3. R2-05（教学流程）：可复用 I1 事件模板作为引导提示载体
