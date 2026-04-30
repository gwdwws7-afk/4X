# R2-01 内容资产盘点（事件池 / 地图配置 / AI策略 / 教程流程）

- 阶段：R2 内容锁定（2026-05-11 ~ 2026-05-24）
- 任务：R2-01 内容资产盘点
- 盘点日期：2026-04-16
- 盘点执行：Codex

## 1. 盘点口径

按 R2 目标口径，内容资产分为 4 类：

1. 事件池（可用于 24 回合的可触发事件内容）
2. 地图配置（区域/节点/连接/控制点等可发布配置）
3. AI 策略集（可调参的阶段行动偏好/人格策略）
4. 教学流程（新手前 3~5 回合引导内容与触发规则）

## 2. 总览结论

| 类别 | 当前形态 | 数量/规模 | 运行证据 | R2 就绪度 |
|---|---|---|---|---|
| 事件池 | 代码内事件工厂 + 队列系统，暂无独立内容库文件 | 工厂API 3个；独立事件内容资产 0 | I1 触发护栏与 RL-01 回放通过 | 低 |
| 地图配置 | `GameConfig` 内置默认地图 + 编辑器窗口 | 6 区域 / 12 节点 / 7 跨区默认连线 | H1/H2/H3 护栏与 RL-01 回放通过 | 中 |
| AI 策略集 | `FactionAISystem` 硬编码人格与策略 | 4 个 AI 势力画像（非玩家） | G 护栏通过 | 中-低 |
| 教学流程 | 仅设计文档，未见运行时代码 | 运行时资产 0 | 无自动化用例 | 未开始 |

## 3. 资产明细

### 3.1 事件池

| 资产 | 路径 | 现状 |
|---|---|---|
| 事件运行时系统 | `Assets/Scripts/Systems/I1/EventSystem.cs` | 具备 Queue/Trigger/Condition 机制 |
| 事件工厂 API | `EventSystem.CreateRandomEvent/CreateTurnBasedEvent/CreateResponseEvent` | 3 个工厂接口可用 |
| 事件内容资源 | （未发现独立 ScriptableObject/JSON 事件池） | **缺失** |
| 自动化证据 | `Assets/Scripts/Tests/StandaloneTest.cs` 的 `TestI1EventTriggerGuardrail`、`TestR1ReplayRL01MapDiplomacyBattleEvent` | 已覆盖基础触发链路 |

盘点结论：I1 机制可运行，但“可锁版事件池（内容层）”尚未建立。

### 3.2 地图配置

| 资产 | 路径 | 现状 |
|---|---|---|
| 默认地图配置代码 | `Assets/Config/DefaultGameConfig.cs` | 6 区域、12 节点、初始控制信息齐全 |
| 配置资产 | `Assets/ScriptableObjects/Config/DefaultGameConfig.asset` | 已有默认资产 |
| 网络拓扑系统 | `Assets/Scripts/Systems/H2/NodeNetworkSystem.cs` | 含 7 条跨区默认连线 |
| 地图系统 | `Assets/Scripts/Systems/H1/StrategicMapSystem.cs` | 地图构建/刷新可用 |
| 地图编辑器 | `Assets/Editor/MapEditorWindow.cs` | 可编辑并导出地图数据 |
| 自动化证据 | `StandaloneTest` 的 H1/H2/H3 护栏 + RL-01 | 已覆盖核心链路 |

盘点结论：地图“可运行”，但缺少 R2 版本化锁版文件与验收口径绑定（v1 lock）。

### 3.3 AI 策略集

| 资产 | 路径 | 现状 |
|---|---|---|
| AI 主系统 | `Assets/Scripts/Systems/G/FactionAISystem.cs` | 决策生成与执行链路可运行 |
| 势力画像 | `InitializeFactionAIs()` | 4 个 AI 势力（Aurean/SacredFire/GoldenHord/AshConfederacy） |
| 策略参数 | 同文件内阈值/权重字段 | 主要为代码硬编码参数 |
| 策略配置文件 | （未发现独立 AI 配置资产） | **缺失** |
| 自动化证据 | `TestGAdjacencyGuardrail`、`TestGAIDecisionExecutionGuardrail` | 已有护栏 |

盘点结论：AI 主逻辑可用，但策略集尚未“数据化可锁版”，难以进行 R3 批量调参。

### 3.4 教学流程

| 资产 | 路径 | 现状 |
|---|---|---|
| 设计文档 | `design/gdd/l2-tutorial-system.md` | 有阶段设计与验收条目 |
| 运行时代码 | `Assets/Scripts` 下未发现 Tutorial/L2 相关系统 | **缺失** |
| 教程 UI/触发配置 | 未发现独立资产 | **缺失** |
| 自动化证据 | 无 | **缺失** |

盘点结论：L2 仍处设计态，当前无法支撑 R2 “教学流程打通”目标。

## 4. 缺口矩阵（R2 阻塞视角）

| Gap ID | 类别 | 缺口描述 | 阻塞级别 | 对应后续任务 |
|---|---|---|---|---|
| GAP-R2-01-01 | 事件池 | 缺少可维护的事件内容库（仅有运行时机制） | 高 | R2-02 事件池补齐并去重 |
| GAP-R2-01-02 | 地图配置 | 缺少地图配置 v1 锁版与验收基线标记 | 中 | R2-03 地图配置终版 |
| GAP-R2-01-03 | AI 策略集 | 缺少可配置化策略集（难度/阶段偏好尚未资产化） | 高 | R2-04 AI 策略集 v1 |
| GAP-R2-01-04 | 教学流程 | 无运行时代码与脚本化触发 | 高 | R2-05 教学流程打通 |

## 5. R2 衔接建议（可直接开工）

1. R2-02 先落“事件池清单文件”并接入 I1 加载路径（先支持最小可玩批次）。
2. R2-03 从 `DefaultGameConfig` 提取地图锁版清单（区域、节点、连线、控制点）并固化为 v1。
3. R2-04 把 `FactionAISystem` 中人格/阈值迁移为可配置策略集（便于 R3 仿真调参）。
4. R2-05 新建 L2 运行时骨架（触发器、步骤状态、可跳过）并接 K 面板提示。

## 6. R2-01 验收勾选

- [x] 事件池资产已盘点并标出缺口
- [x] 地图配置资产已盘点并标出缺口
- [x] AI 策略资产已盘点并标出缺口
- [x] 教学流程资产已盘点并标出缺口
- [x] 已形成可执行缺口矩阵并映射后续任务
