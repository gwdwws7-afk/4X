# R2-03 地图配置终版（v1 锁版）

- 阶段：R2 内容锁定（2026-05-11 ~ 2026-05-24）
- 任务：R2-03 地图配置终版（节点 / 连接 / 产出 / 控制点）
- 执行日期：2026-04-16
- 结论：PASS（v1 lock 已形成）

## 1. 产物清单

| 产物 | 路径 | 说明 |
|---|---|---|
| 地图锁版文件 | `production/sprints/r2-content-lock-2026-05-11-2026-05-24/map-config-v1-lock.json` | v1 锁版 SSOT（R2口径） |
| 地图锁版护栏测试 | `Assets/Scripts/Tests/StandaloneTest.cs` | `TestR2MapConfigV1LockGuardrail()` |
| 定向测试入口（Editor） | `Assets/Editor/TestMenu.cs` | `Run R2 Map Config V1 Lock Checks` |
| 定向测试入口（ContextMenu） | `Assets/Scripts/Tests/TestRunner.cs` | `Run R2 Map Config V1 Lock Checks (R2-03)` |

## 2. v1 锁版范围

- 区域：6
- 节点：12
- 资源产出节点：2（`Caspian`、`TradeHub`）
- 邻接边：13（含 6 条区内边 + 7 条跨区标准边）

## 3. 锁版校验规则（已自动化）

1. 区域数量与每区节点数必须匹配 v1 清单。
2. 每个节点的 `NodeType / DefenseBonus / InitialController / InitialControlPoints / MaxControlPoints` 必须匹配。
3. 资源产出节点集必须固定为 `Caspian`、`TradeHub`。
4. 拓扑边数必须为 13，且每条边双向可达。
5. 关键非边关系（`Hormuz` ↔ `IsraelCore`）必须为不可达。

## 4. 验证记录

| RunID | 范围 | 结果 | 证据 |
|---|---|---|---|
| R2-03-RUN-01 | `RunR2MapConfigV1LockChecks` 定向检查 | PASS | `production/evidence/r2/map-config/20260416_R2-03_MAP-LOCK-CHECKS.log` |
| R2-03-RUN-02 | `RunAllTests` 全量回归 | PASS（`364 passed, 0 failed`） | `production/evidence/r2/map-config/20260416_R2-03_RUNALL-REGRESSION.log` |

## 5. 说明（产出口径）

当前运行时 `NodeConfig` 未包含显式“产出资源字段”，v1 lock 采用以下口径：

1. 产出节点由 `NodeType=ResourceNode` 判定。
2. 锁版文件中以 `output` 字段标注设计口径（`Caspian -> FireOil`，`TradeHub -> TradeToken`）。
3. 后续若新增显式产出字段（配置化），以配置字段为准替换该代理口径。
