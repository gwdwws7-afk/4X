# SSOT：回合/AP/时间/胜负统一规则

> **状态**: LOCKED
> **锁版号**: `EVA-SSOT-RULES-2026-04-08.v2`
> **生效日期**: 2026-04-08
> **适用范围**: 企划主文档 / A1 / A2 / A3（边界引用）/ J / README / AGENTS
> **唯一企划案**: `design/gdd/eventideage-unified-gdd.md` (`EVA-UNIFIED-GDD-2026-04-08.v4`)

## 1. 规则优先级

1. 本文档为唯一规则真相源（Single Source of Truth）。
2. 企划层面的唯一文档为 `eventideage-unified-gdd.md`，规则层面由本文提供裁决。
3. 任何文档与本文冲突时，以本文为准。
4. 变更本文必须同步更新：
   - `design/gdd/rules-consistency-matrix.md`
   - 受影响文档的锁版标记与更新日期。

## 2. 回合与阶段规则（Turn + Phase）

### 2.1 阶段顺序（固定）

| 索引 | 阶段名称 | 基础AP |
|------|----------|--------|
| 0 | 外交 | 2 |
| 1 | 战略 | 2 |
| 2 | 作战 | 3 |
| 3 | 后勤 | 1 |
| 4 | 情报 | 1 |
| 5 | AI响应 | 0 |

### 2.2 AP模型（固定）

- 每回合基础阶段 AP 合计：`9`
- 每回合通用 AP：`2`
- 每回合总 AP：`11`
- 常量口径：
  - `GameConfig.kTotalActionPoints = 11`
  - `GameConfig.kUniversalActionPoints = 2`

### 2.3 AP使用约束

- 阶段专用 AP 只能在对应阶段使用。
- 通用 AP 可在任意玩家阶段使用（不含 AI响应）。
- 阶段结束时，未用完的阶段专用 AP 作废。
- 回合结束时，未用完的通用 AP 作废。
- 行动点不可跨回合累积。

## 3. 时间规则（Time）

- `1回合 = 6个月`
- 起始时间：`2028 H1`
- 最大回合数：`24`
- 24回合战役覆盖 12 年窗口（2028H1 至 2039H2）

### 3.1 显示规则

- 若回合号为奇数：显示 `H1`
- 若回合号为偶数：显示 `H2`
- 年份公式：`Year = 2028 + floor((Turn - 1) / 2)`

## 4. 胜负规则（Victory / Defeat）

### 4.1 胜利路径（固定4条）

- 能源解放（Energy Liberation）
- 军事均势（Military Stalemate）
- 抵抗轴心胜利（Axis Victory）
- 外交解决（Diplomatic Resolution）

### 4.2 胜利判定阈值

- 每条路径进度范围：`0~100`
- 单路径胜利阈值：`>= 80`
- 组合胜利：`>=2` 条路径都达到 `>= 80`

### 4.3 失败判定

- 军事崩溃：`AshWill < 30`
- 经济崩溃：`FireOil <= 0`
- 内部分裂：任一派系满意度 `< 15`

### 4.4 超时判定

- 当 `CurrentTurn >= 24` 且尚未触发胜利/失败，触发持久战终局（Attrition Timeout）。

## 5. 所有权边界

- A1（回合主循环）拥有：回合推进、阶段顺序、总AP规则。
- A2（阶段引擎）拥有：阶段切换执行与阶段AP/通用AP消耗执行。
- A3（资源系统）不拥有回合/AP/胜负规则，仅消费其结果。
- J（胜负系统）拥有：胜负检测与终局触发。
- README/AGENTS 为对外说明层，不可定义与 SSOT 冲突的新规则。

## 6. 变更控制

- 未同步更新一致性矩阵，不得提升锁版号。
- 推荐版本格式：`EVA-SSOT-RULES-YYYY-MM-DD.vN`

## 7. 锁版门禁（重定义）

- `G0-RuleLock`：规则锁，要求 SSOT 与 A1/A2/A3/A5/J/README/AGENTS/一致性矩阵完全对齐。
- `G1-ScopeLock`：范围锁，要求系统总量口径一致（当前统一为 `34`）。
- `G2-ReleaseLock`：发布锁，要求关键事件语义一致且有自动化回归证据。

门禁状态记录来源：`design/gdd/rules-consistency-matrix.md`。

## 8. 标准ID字典（SSOT）

唯一运行时ID字典：`Assets/Scripts/Core/GameIds.cs`

- Faction（主ID）：
  - `Vashid` / `Aurean` / `SacredFire` / `GoldenHord` / `Ash Confederacy`
- Resource（主ID）：
  - `Arms` / `FireOil` / `GoldLeaf` / `TradeToken` / `SocialValue` / `AshWill` / `TributeOrder`
- Node（主ID）：
  - `Hormuz` / `Bushehr` / `IraqBorder` / `SyriaZone` / `Caspian` / `Caucasus` / `RedSea` / `GulfBase` / `Mediterranean` / `IsraelCore` / `Afghanistan` / `TradeHub`

兼容别名（仅用于迁移，不得新增业务依赖）：`GoldLeader`、`HolyFire`、`AshCloud`、`Vahid`、`Tigris`、`Damascus`、`Beirut`、`Energy` 等。
