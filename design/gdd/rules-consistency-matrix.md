# 规则一致性矩阵（A1/A2/A3/A5/J/K/L4/4X/README/AGENTS/UNIFIED/INDEX）

> **状态**: LOCKED
> **对应锁版**: `EVA-SSOT-RULES-2026-04-08.v2`
> **最后更新**: 2026-04-14

## 对齐说明

- `Aligned`：该文件明确采用 SSOT 口径或逐字复述同口径。
- `Boundary`：该文件仅声明边界，不在本地定义阈值。
- `N/A`：该文件不负责该规则。
- `Snapshot`：该文件承载统计快照（不定义规则本身）。

## 企划唯一性

- 企划唯一文档：`design/gdd/eventideage-unified-gdd.md`（`EVA-UNIFIED-GDD-2026-04-08.v4`）
- 规则唯一真相源：`design/gdd/ssot-core-rules.md`（`EVA-SSOT-RULES-2026-04-08.v2`）
- 系统枚举快照：`design/gdd/systems-index.md`
- 运行时主 ID 字典：`Assets/Scripts/Core/GameIds.cs`

## 核心规则矩阵

| 规则项 | A1 (`turn-loop.md`) | A2 (`a2-phase-engine.md`) | A3 (`a3-resource-system.md`) | A5 (`a5-game-clock.md`) | J (`j-victory-defeat.md`) | K (`k-ui-framework.md`) | L4 (`l4-localization.md`) | 4X概念 (`4x-game-concept.md`) | README | AGENTS | UNIFIED GDD | INDEX |
|---|---|---|---|---|---|---|---|---|---|---|---|---|
| 阶段顺序固定为 外交→战略→作战→后勤→情报→AI响应 | Aligned | Aligned | Boundary | Boundary | Boundary | Boundary | N/A | Boundary | Aligned | Aligned | Aligned | Boundary |
| 阶段 AP 为 2/2/3/1/1/0 | Aligned | Aligned | Boundary | Boundary | Boundary | Boundary | N/A | Boundary | Aligned | Aligned | Aligned | Boundary |
| 通用 AP = 2（跨阶段、不可跨回合） | Aligned | Aligned | Boundary | Boundary | Boundary | Boundary | N/A | Boundary | Aligned | Aligned | Aligned | Boundary |
| 每回合总 AP = 11 | Aligned | Aligned | Boundary | Boundary | Boundary | Boundary | N/A | Boundary | Aligned | Aligned | Aligned | Boundary |
| 1 回合 = 6 个月 | Aligned | Aligned | Boundary | Aligned | Aligned | Aligned | Aligned | Aligned | Aligned | Aligned | Aligned | Boundary |
| 时间显示使用 `H1/H2`（不得使用 `2028.0/2028.5`） | Boundary | Boundary | N/A | Aligned | Boundary | Aligned | Aligned | Aligned | Boundary | Boundary | Aligned | Boundary |
| 最大回合数 = 24（时间窗 2028H1~2039H2） | Aligned | Aligned | Boundary | Aligned | Aligned | Aligned | Aligned | Aligned | Aligned | Aligned | Aligned | Boundary |
| 胜利路径固定 4 条 | Boundary | Boundary | N/A | N/A | Aligned | Aligned | N/A | Aligned | Aligned | Aligned | Aligned | Boundary |
| 单路径胜利阈值 >= 80 | Boundary | Boundary | N/A | N/A | Aligned | Aligned | N/A | Aligned | Aligned | Aligned | Aligned | Boundary |
| 组合胜利（>=2 路径达到 >=80） | Boundary | Boundary | N/A | N/A | Aligned | Aligned | N/A | Aligned | Aligned | Aligned | Aligned | Boundary |
| 失败条件：AshWill<30 / FireOil<=0 / 任一派系满意度<15 | Boundary | Boundary | N/A | N/A | Aligned | Aligned | N/A | Aligned | Aligned | Aligned | Aligned | Boundary |
| 超时终局：CurrentTurn>=24（终局触发归 J；A5 仅时间展示） | Boundary | Boundary | N/A | Boundary | Aligned | Aligned | N/A | Aligned | Aligned | Aligned | Aligned | Boundary |
| 标准 ID 字典以 `GameIds.cs` 为准（别名仅用于迁移） | Boundary | Boundary | Boundary | Boundary | Aligned | Boundary | Boundary | Boundary | Boundary | Boundary | Aligned | Boundary |
| 冲突时以 `ssot-core-rules.md` 为准 | Aligned | Aligned | Aligned | Aligned | Aligned | Aligned | Aligned | Aligned | Aligned | Aligned | Aligned | Aligned |

## 扩展覆盖矩阵（本次扩测）

| 检查项 | UNIFIED GDD | INDEX | README | AGENTS | Runtime / Tests | 结论 |
|---|---|---|---|---|---|---|
| 系统总量口径固定为 `34` | Aligned | Snapshot | Aligned | Aligned | N/A | PASS |
| 锁版门禁采用 `G0/G1/G2` 三级定义 | Aligned | Boundary | Boundary | Boundary | N/A | PASS |
| `NodeControlChanged(nodeId, oldController, newController, controlPoints)` 事件签名统一 | Boundary | Boundary | Boundary | Boundary | Aligned | PASS |
| 控制点更新允许 `oldController == newController`（CP-only） | Boundary | Boundary | Boundary | Boundary | Aligned | PASS |
| 地图消费逻辑区分“控制权变化”与“仅控制点变化” | Boundary | Boundary | Boundary | Boundary | Aligned | PASS |
| AP 双轨执行（阶段 AP + 通用 AP）与 UI 同步显示 | Aligned | Boundary | Aligned | Aligned | Aligned | PASS |
| 自动化覆盖快照（含 B5/H2/H3/C1/C2/C3/C4/C5/D2/D3/D5/G/H1/K/A5-J 护栏） | Boundary | Snapshot | Boundary | Boundary | Snapshot | PASS |
| GAP-01（H2运行时）闭环状态与证据同步 | Aligned | Snapshot | Boundary | Boundary | Snapshot | PASS |
| GAP-02（B5支出阶段）闭环状态与证据同步 | Aligned | Snapshot | Boundary | Boundary | Snapshot | PASS |
| GAP-03（运行时ID canonical化尾项）闭环状态与证据同步 | Aligned | Snapshot | Boundary | Boundary | Snapshot | PASS |
| GAP-04（资源口径尾项）闭环状态与证据同步 | Aligned | Snapshot | Boundary | Boundary | Snapshot | PASS |

## 锁版门禁结论

- `G0-RuleLock`：`PASS`
- `G1-ScopeLock`：`PASS`
- `G2-ReleaseLock`：`PASS`（2026-04-14）

总评：`PASS (LOCKED / G2 cleared)`

