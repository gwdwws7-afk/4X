# 瓦希德帝国（EventideAge）唯一权威企划案

> **文档ID**: `EVA-UNIFIED-GDD-2026-04-08.v4`
> **状态**: LOCKED (Single Canonical GDD)
> **生效日期**: 2026-04-08
> **唯一规则基线**: `design/gdd/ssot-core-rules.md` (`EVA-SSOT-RULES-2026-04-08.v2`)
> **项目阶段**: Pre-Production（含可运行原型代码基线）
> **引擎**: Unity 2022.3 LTS（当前工程：2022.3.62f1）
> **目标平台**: PC (Steam)
> **锁版修订**:
> - `v4`（2026-04-08）：补充“设计完成定义（DoD）”“设计/实现差距账本”“G2阻塞项闭环条件”，明确企划完成与实现完成的边界，避免继续误判完成度。
> - `v3`（2026-04-08）：统一系统总量口径为34，重定义锁版门禁，补齐 NodeControlChanged 事件语义一致性与扩展一致性矩阵覆盖。
> - `v2`（2026-04-08）：同步文档状态口径、清理旧时间格式、补齐关键 UI 后果可见性缺口并完成一致性回归。

---

## 0. 权威声明

1. 本文档是项目唯一企划案（Single Canonical GDD）。
2. 概念、规则、范围、阶段目标、验收口径均以本文为准。
3. 其他文档（如 `4x-game-concept.md`、`systems-index.md`、各系统子GDD）为补充说明，不得覆盖本文。
4. 若补充文档与本文冲突，以本文为准。

---

## 1. 游戏定义

### 1.1 一句话定义

在文明黄昏的近未来，玩家作为瓦希德帝国最高决策者，在军事、经济、外交、意识形态四条线上进行不对称博弈，以有限资源突破围堵并争取多路径胜利。

### 1.2 核心体验

- 以弱胜强，不靠数值对等。
- 每个行动都有可追踪代价。
- 敌对阵营是动态系统，不是静态Boss。
- 战争不是唯一有效路径。

### 1.3 设计支柱（Pillars）

- P0 不对称博弈。
- P1 行动有代价。
- P2 帝国是活的（会反扑、会演化）。
- P3 意识形态可传播且可反噬。

### 1.4 反支柱（Anti-Pillars）

- 不做势力变量等同。
- 不做无代价胜利。
- 不把帝国写成单一扁平反派。
- 不把抵抗写成无阴影纯正义。
- 不把战争设成唯一解法。

---

## 2. 世界观与势力

### 2.1 时间背景

- 游戏起点：2028 H1。
- 叙事背景：中东权力格局进入临界对抗期。

### 2.2 主要势力

- 瓦希德帝国（玩家）：抵抗轴心核心。
- 黄金领：主导封锁秩序的帝国联盟核心。
- 圣火序：高风险军事冒险者。
- 金帐合众：机会主义中间势力。
- 灰烬众：代理人网络关键节点。

---

## 3. 核心循环与规则（锁版）

> 本章与 `ssot-core-rules.md` 完全一致。

### 3.1 阶段顺序与基础AP

| 索引 | 阶段 | 基础AP |
|---|---|---|
| 0 | 外交 | 2 |
| 1 | 战略 | 2 |
| 2 | 作战 | 3 |
| 3 | 后勤 | 1 |
| 4 | 情报 | 1 |
| 5 | AI响应 | 0 |

### 3.2 AP总量与使用约束

- 每回合阶段基础AP合计：9。
- 每回合通用AP：2。
- 每回合总AP：11。
- 阶段专用AP不跨阶段。
- 通用AP可跨玩家阶段，不跨回合。
- 回合结束后剩余AP清零。

### 3.3 时间规则

- `1回合 = 6个月`。
- 起始：`2028 H1`。
- 最大回合数：`24`（覆盖 2028H1 至 2039H2）。
- 时间显示：奇数回合为 H1，偶数回合为 H2。

### 3.4 胜负规则

- 胜利路径固定4条：能源解放 / 军事均势 / 抵抗轴心胜利 / 外交解决。
- 单路径胜利：路径进度 `>= 80`。
- 组合胜利：任意2条路径都 `>= 80`。
- 失败条件：`AshWill < 30` 或 `FireOil <= 0` 或任一派系满意度 `< 15`。
- 超时终局：`CurrentTurn >= 24` 且无胜负则触发持久战终局。

---

## 4. 游戏对象与资源基线

### 4.1 资源（7种）

- Arms（战械，Consumable）
- FireOil（火油，Accumulative）
- GoldLeaf（金叶，Accumulative）
- TradeToken（商盟券，Accumulative）
- SocialValue（社稷值，Ratio）
- AshWill（灰烬志，Ratio）
- TributeOrder（朝贡序，Ratio）

### 4.2 地图（MVP基线）

- 6 区域、12 节点。
- 节点类型包含：咽喉、港口、城市、资源点。

### 4.3 默认战役配置基线

- Factions：Vashid / Aurean / SacredFire / GoldenHord / Ash Confederacy。
- Phase/AP 与本企划第3章保持一致。
- 配置来源：`Assets/Config/DefaultGameConfig.cs`。
- ID字典来源：`Assets/Scripts/Core/GameIds.cs`（主ID）+ 兼容别名映射（迁移用途）。

---

## 5. 系统架构与职责边界

### 5.1 核心架构

- GameState：ScriptableObject 数据源。
- GameEvents：事件总线。
- GameManager：生命周期与系统协调。
- GameSystem：系统基类。

### 5.2 34系统分层（统一口径）

- Core：A1-A5。
- Economy：B1-B5。
- Diplomacy/Ideology：C1-C5。
- Military：D1-D6。
- Internal/Intel/AI：E/F1/G。
- Map：H1-H3。
- Narrative/WinLose：I1/J。
- UI/Meta：K/L1-L4。
- 合计：34个系统（A1-L4）。

### 5.3 边界裁决

- A1 拥有回合推进、阶段顺序、总AP规则。
- A2 拥有阶段切换执行与AP消费执行。
- A3 不拥有回合/AP/胜负定义权，仅消费规则结果。
- J 拥有胜负检测与终局触发。

### 5.4 后果追踪账本（Consequence Ledger，强制）

为落实 P1（行动有代价）与 FADT 的可感知后果要求，所有可执行行动必须在设计层维护“后果追踪账本”记录。

#### 账本字段（最小集）

| 字段 | 必填 | 说明 |
|---|---|---|
| ActionId | 是 | 行动唯一标识（与系统内 ActionId 对齐） |
| ImmediateCost | 是 | 即时代价（资源/AP/关系/稳定度等） |
| DelayedCost | 是 | 延迟代价（若无填 `None`） |
| TriggerCondition | 是 | 延迟代价触发条件（若无填 `N/A`） |
| DurationTurns | 是 | 影响持续回合数（若即时一次性填 `0`） |
| PlayerVisibleSurface | 是 | 玩家可见位置（HUD/日志/详情面板） |
| Severity | 是 | 影响强度：Low / Medium / High / Critical |
| Reversible | 是 | 是否可逆：Yes / No |
| LinkedSystems | 是 | 关联系统（例如 D1->B2->J） |
| RuleSource | 是 | 规则来源（SSOT或对应GDD章节） |

#### 执行规则

1. 未登记账本字段的行动，不得进入“可发布设计”状态。
2. 所有 `Severity >= Medium` 的后果，必须在当回合可见于 UI 或行动结算日志。
3. 所有 `DelayedCost != None` 的行动，必须在玩家视图提供“来源可追溯”入口。
4. 每次版本评审必须抽查至少 10 条账本记录，验证“设计描述-运行行为-UI呈现”一致。

#### 与UI的对齐要求

- MVP UI 至少提供两类展示：
  - 行动结算流（本回合发生了什么）。
  - 后果追踪流（这些结果来自哪次行动）。
- 账本中的 `PlayerVisibleSurface` 必须能映射到 K 系统具体界面元素。

---

## 6. MVP定义（本项目执行口径）

### 6.1 MVP目标

- 可完整跑通 24 回合战役流程。
- 阶段/AP/时间/胜负规则全程一致。
- 4条胜利路径均可推进并显示进度。
- 至少 1 条胜利路径和 1 条失败路径可被稳定触发。

### 6.2 MVP包含

- A1/A2/A3/A5/J 与 K 的闭环。
- B1/B2/B5 的基础经济封锁闭环。
- C1/C2 与 D1/D5 的最小联动闭环。
- H1/H2 的节点争夺与可视化反馈。

### 6.3 MVP不包含

- L1 异步多人。
- 完整 Steam 接入（L3）。
- 全量事件库（I1 先做模板化最小集）。

---

## 7. 里程碑与交付

### 7.1 当前阶段

- 阶段：Pre-Production。
- 策略：先完成规则与系统闭环一致性，再做内容扩展和平衡打磨。

### 7.2 下一阶段目标（进入 Alpha 前）

- 完成 MVP 可玩闭环。
- 完成核心UI信息闭环（阶段/AP/资源/胜负进度/风险提示）。
- 完成最小可验证平衡批次（至少 20 局自动化或半自动样本）。
- 建立并执行固定平衡回归资产：
  - `design/balance/b1-b3-parameter-baseline.csv`
  - `design/balance/regression-scenarios.csv`
  - `design/balance/regression-results-template.csv`

---

## 8. 风险与缓解

- 风险1：规则分叉导致反复返工。
  - 缓解：本文 + SSOT + 一致性矩阵联合锁版。
- 风险2：AI行为单调影响中后期体验。
  - 缓解：先确保可读性与可解释性，再引入策略多样性。
- 风险3：事件系统体量过大压垮节奏。
  - 缓解：先模板化 10-15 个关键事件。
- 风险4：经济闭环失衡导致单一路线碾压。
  - 缓解：优先压测 B1/B2/B5 与 J 的联动阈值。

---

## 9. 验收标准（企划层）

- 任一评审者仅阅读本文即可理解并执行项目。
- 回合/AP/时间/胜负在文档与实现之间无冲突。
- 术语唯一、阈值唯一、阶段顺序唯一。
- 任何补充文档不得定义与本文冲突的新基线。

---

## 10. 锁版门禁（重定义）

为避免“规则已锁但范围未收敛”导致返工，锁版分为三级门禁：

1. `G0-RuleLock`（规则锁）
   - 口径：回合/AP/时间/胜负由 SSOT 唯一裁决。
   - 验证：A1/A2/A3/A5/J + README + AGENTS + 一致性矩阵全部对齐。
2. `G1-ScopeLock`（范围锁）
   - 口径：系统总量、系统命名、设计状态统计必须统一。
   - 验证：统一GDD / systems-index / AGENTS / README 的系统总量一致（当前为34）。
3. `G2-ReleaseLock`（发布锁）
   - 口径：关键行为语义一致且具备可回归证据。
   - 验证：关键事件链（如 `NodeControlChanged`）语义一致；自动化与手动回归资产齐备。

当前门禁快照：`G0=PASS`，`G1=PASS`，`G2=PASS`（GAP-03 已于 2026-04-14 闭环）。

---

## 11. 变更管理

- 修改本文必须同步更新：
  - `design/gdd/ssot-core-rules.md`
  - `design/gdd/rules-consistency-matrix.md`
- 未同步更新，不得变更本文版本号。
- 版本规则：`EVA-UNIFIED-GDD-YYYY-MM-DD.vN`。

---

## 12. 设计/实现差距账本（收敛执行）

> 本节用于消除“企划已完整”与“实现已完整”混淆。  
> 口径：企划完整 != 全部系统实现完成；但企划必须给出清晰差距与闭环条件。

| GapId | 主题 | 当前状态 | 影响范围 | 阻塞级别 | 闭环标准 |
|---|---|---|---|---|---|
| GAP-01 | H2战略节点系统（运行时） | Closed (2026-04-13) | H1/H2、D1、B1/B3、K | 已闭环 | 运行时代码落地 + 节点争夺/控制链路回归通过 |
| GAP-02 | B5经济结算支出阶段 | Closed (2026-04-13) | B5、B1/B2/B3、J | 已闭环 | `ExpensePhase` 完整实现 + 结算日志可追溯 + 回归样本通过 |
| GAP-03 | 运行时ID canonical化尾项 | Closed (2026-04-14) | B/C/D/G/H/J | 已闭环 | B/C/D/G/H 运行时入口与索引键 canonical 化 + alias 仅迁移入口 + 自动化回归通过 |
| GAP-04 | 资源口径尾项（NorthCoins等历史残留） | Closed (2026-04-13) | B1/B5、A3、SSOT | 已闭环 | B1购买入口 canonical 化 + B5结算资源ID canonical 守卫 + 自动化回归通过 |
| GAP-05 | UI时间显示与A5联动 | In Progress | K、A5、UIManager | 非阻塞 | HUD稳定显示 `2028 H1/H2`，与A5一致 |
| GAP-06 | Setup Project 与 README 实际行为一致性 | In Progress | Editor、README、AGENTS | 非阻塞 | 一键入口行为与文档描述一致，避免误配置 |

执行约束：

1. `G2-ReleaseLock` 未闭环前，不得宣称“发布就绪”。
2. `GAP-01~GAP-04` 任一未闭环，不得宣称“实现完整”。
3. 每次关闭一个 Gap，必须同步更新一致性矩阵中的 Runtime/Tests 行证据。

---

## 13. 企划完成定义（Definition of Complete）

当且仅当同时满足以下条件，可宣称“企划案完整且无冲突（Design Complete）”：

1. 规则唯一：回合/AP/时间/胜负只由 SSOT 定义，且无冲突条目。
2. 文档唯一：本文为唯一企划案，补充文档仅解释，不重定义基线。
3. 口径一致：系统总量、术语、ID 字典、门禁定义在 UNIFIED/SSOT/INDEX/README/AGENTS 一致。
4. 差距透明：本文件第12章差距账本存在且可追踪，状态与证据可回溯。

说明：

- `Design Complete` 解决“怎么做、按什么做、如何判定一致”的问题。
- `Implementation Complete` 解决“是否全部实现并可回归”的问题，两者不可混用。

---

## 14. 当前锁版快照（v4）

- `G0-RuleLock`：PASS  
- `G1-ScopeLock`：PASS  
- `G2-ReleaseLock`：PASS（2026-04-14）

总评：  
`Design Baseline = LOCKED`，`Implementation Baseline = READY（阻塞项已清零，非阻塞项持续推进）`。
