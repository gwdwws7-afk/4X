# Systems Index: 抵抗轴心 (The Eventide Age)

> **Status**: Draft
> **Created**: 2026-03-31
> **Last Updated**: 2026-03-31
> **Source Concept**: design/gdd/4x-game-concept.md

---

## Overview

抵抗轴心是一款回合制4X大战略游戏，核心是**不对称博弈**——玩家以瓦希德帝国的视角对抗黄金领的全体系围堵。游戏围绕军事（代理人战争、不对称防御）、经济（封锁与反封锁、能源金融）、外交（联盟构建、协议签订）、意识形态（抵抗叙事输出）四条线展开。

所有系统围绕一个核心循环运转：**回合阶段 → 行动选择 → 代价结算 → 敌对反应**。玩家的每一个决策都在内部（派系满意度、社稷值）和外部（外交关系、军事态势）同时埋下代价，直到帝国自己先崩溃或玩家在任一维度取得突破。

---

## Systems Enumeration

| # | System Name | Category | Priority | Status | Design Doc | Depends On |
|---|-------------|----------|----------|--------|------------|------------|
| A1 | 回合主循环 | Core | MVP | Designed | design/gdd/turn-loop.md | — |
| A2 | 回合阶段引擎 | Core | MVP | Designed | design/gdd/a2-phase-engine.md | A1 |
| A3 | 资源系统 | Core | MVP | Designed | design/gdd/a3-resource-system.md | — |
| A4 | 存档系统 | Persistence | MVP | Designed | design/gdd/a4-save-system.md | A1 |
| A5 | 游戏时钟 | Core | MVP | Designed | design/gdd/a5-game-clock.md | A1 |
| B1 | 星币-能源金融系统 | Economy | MVP | Designed | design/gdd/b1-star-currency-energy-system.md | A3, H2 |
| B2 | 封锁与反封锁系统 | Economy | MVP | Designed | design/gdd/b2-blockade-system.md | B1, A3 |
| B3 | 能源贸易网络 | Economy | MVP | Designed | design/gdd/b3-energy-trade-network.md | B1, H2 |
| B4 | 汇率系统 | Economy | Alpha | Designed | design/gdd/b4-exchange-rate.md | B1 |
| B5 | 经济结算系统 | Economy | MVP | Designed | design/gdd/b5-economic-settlement.md | B1, B2, B3, A3 |
| C1 | 外交关系系统 | Gameplay | MVP | Designed | design/gdd/c1-diplomatic-relations.md | A3, H1 |
| C2 | 外交协议系统 | Gameplay | MVP | Designed | design/gdd/c2-diplomatic-protocols.md | C1, A3 |
| C3 | 意识形态输出系统 | Gameplay | Alpha | Designed | design/gdd/c3-ideology-system.md | C1, A3 |
| C4 | 联盟系统 | Gameplay | Alpha | Designed | design/gdd/c4-alliance-system.md | C1, C2 |
| C5 | 国际组织系统 | Gameplay | Alpha | Designed | design/gdd/c5-international-orgs.md | C1, C3 |
| D1 | 军事行动系统 | Gameplay | MVP | Designed | design/gdd/d1-military-operations.md | B1, H1, H2 |
| D2 | 军事-政治联动 | Gameplay | MVP | Designed | design/gdd/d2-military-political-linkage.md | D1, E, C1 |
| D3 | 代理人民事系统 | Gameplay | Alpha | Designed | design/gdd/d3-proxy-civil-affairs.md | D1, C4 |
| D4 | 核威慑系统 | Gameplay | Alpha | Designed | design/gdd/d4-nuclear-deterrence.md | D1, F1 |
| D5 | 战争结算系统 | Gameplay | MVP | Designed | design/gdd/d5-war-resolution.md | D1, H1, H2 |
| D6 | 军事科技系统 | Progression | Alpha | Designed | design/gdd/d6-military-tech.md | B1, D1 |
| E | 内部政治系统 | Gameplay | MVP | Designed | design/gdd/e-internal-politics.md | A3, D2, C3, B5 |
| F1 | 情报系统 | Gameplay | Alpha | Designed | design/gdd/f1-intelligence-system.md | A1 |
| G | 势力AI框架 | AI | MVP | Designed | design/gdd/g-faction-ai-framework.md | B1, C1, D1 |
| H1 | 战略地图系统 | Presentation | MVP | Designed | design/gdd/h1-strategic-map.md | — |
| H2 | 战略节点系统 | Presentation | MVP | Designed | design/gdd/h2-strategic-node.md | H1 |
| H3 | 地形与视野系统 | Gameplay | Alpha | Designed | design/gdd/h3-terrain-vision.md | H1, H2, F1 |
| I1 | 事件系统 | Narrative | Alpha | Designed | design/gdd/i1-event-system.md | C1, D1, E |
| J | 胜负判定系统 | Gameplay | MVP | Designed | design/gdd/j-victory-defeat.md | C1, D1, B1, B5, D5 |
| K | UI框架 | UI | MVP | Designed | design/gdd/k-ui-framework.md | H1, A3 |
| L1 | 异步多人系统 | Meta | Full Vision | Designed | design/gdd/l1-async-multiplayer.md | A1, G |
| L2 | 新手教程系统 | Meta | Alpha | Designed | design/gdd/l2-tutorial-system.md | K |
| L3 | Steam集成 | Meta | Full Vision | Designed | design/gdd/l3-steam-integration.md | A4 |
| L4 | 本地化系统 | Meta | Full Vision | Designed | design/gdd/l4-localization.md | K |

---

## Categories

| Category | Description | Systems |
|----------|-------------|---------|
| **Core** | 驱动游戏运转的基础系统，无此则游戏无法启动 | A1, A2, A3, A4, A5 |
| **Economy** | 资源产生、消耗、交易和封锁的经济循环 | B1, B2, B3, B4, B5 |
| **Gameplay** | 定义游戏核心玩法和策略维度的系统 | C1-C5, D1-D6, E, F1, H3, I1, J |
| **AI** | 所有AI势力的决策和行为框架 | G |
| **Presentation** | 地图和节点的视觉呈现层 | H1, H2 |
| **UI** | 面向玩家的所有界面和信息显示框架 | K |
| **Narrative** | 事件、大事记和叙事生成系统 | I1 |
| **Progression** | 玩家和势力的成长与解锁系统 | D6 |
| **Persistence** | 存档、回放和设置系统 | A4 |
| **Meta** | 游戏外的系统如教程（仅L2） | L2 |

---

## Priority Tiers

| Tier | Definition | Target Milestone | Count |
|------|------------|------------------|-------|
| **MVP** | 核心循环可运行的最小系统集。没有这些无法测试"游戏是否有乐趣"。 | 第一阶段（4个月） | 16 |
| **Alpha** | 完整游戏系统rough form。所有核心机制都已存在，内容可填充。 | 第二/三阶段 | 13 |
| **Full Vision** | 抛光、边缘情况、可选功能和完整内容。 | 第四阶段/发布 | 4 |

---

## Dependency Map

### Foundation Layer (no dependencies)

1. **A1 回合主循环** — 最高层驱动，整个游戏的心脏
2. **A3 资源系统** — 7种资源的定义和基础计算
3. **H1 战略地图系统** — 地图渲染和交互基础设施

### Core Layer (depends on Foundation)

1. **H2 战略节点系统** — 地图上的争夺对象，依赖H1
2. **B1 星币-能源金融系统** — 依赖A3（资源定义）
3. **F1 情报系统** — 依赖A1（回合触发）
4. **A4 存档系统** — 依赖A1

### Core-Mechanics Layer (depends on Core)

1. **B2 封锁与反封锁系统** — 依赖B1
2. **B5 经济结算系统** — 依赖B1, B2, B3, A3
3. **C1 外交关系系统** — 依赖A3, H1
4. **D1 军事行动系统** — 依赖B1, H1, H2
5. **E 内部政治系统** — 依赖A3, D2, C3, B5
6. **K UI框架** — 依赖H1, A3

### Feature Layer (depends on Core-Mechanics)

1. **A2 回合阶段引擎** — 依赖A1
2. **B3 能源贸易网络** — 依赖B1, H2
3. **B4 汇率系统** — 依赖B1
4. **C2 外交协议系统** — 依赖C1, A3
5. **C3 意识形态输出系统** — 依赖C1, A3
6. **D2 军事-政治联动** — 依赖D1, E, C1
7. **D5 战争结算系统** — 依赖D1, H1, H2
8. **G 势力AI框架** — 依赖B1, C1, E, D1
9. **J 胜负判定系统** — 依赖C1, D1, C3, B1, E, B5, D5

### Advanced Feature Layer (depends on Feature)

1. **C4 联盟系统** — 依赖C1, C2
2. **C5 国际组织系统** — 依赖C1, C3
3. **D3 代理人民事系统** — 依赖D1, C4
4. **D4 核威慑系统** — 依赖D1, F1
5. **D6 军事科技系统** — 依赖B1, D1
6. **H3 地形与视野系统** — 依赖H1, H2, F1
7. **I1 事件系统** — 依赖C1, D1, E

### Polish Layer

1. **L1 异步多人系统** — 依赖A1, G
2. **L2 新手教程系统** — 依赖K
3. **L3 Steam集成** — 依赖A4
4. **L4 本地化系统** — 依赖K

---

## Recommended Design Order

| Order | System | Priority | Layer | Est. Effort |
|-------|--------|----------|-------|-------------|
| 1 | A1 回合主循环 | MVP | Foundation | M |
| 2 | A3 资源系统 | MVP | Foundation | M |
| 3 | A5 游戏时钟 | MVP | Foundation | S |
| 4 | H1 战略地图系统 | MVP | Foundation | M |
| 5 | H2 战略节点系统 | MVP | Core | M |
| 6 | B1 星币-能源金融系统 | MVP | Core | M |
| 7 | B2 封锁与反封锁系统 | MVP | Core | M |
| 8 | B5 经济结算系统 | MVP | Core | M |
| 9 | D1 军事行动系统 | MVP | Core | M |
| 10 | D5 战争结算系统 | MVP | Feature | M |
| 11 | C1 外交关系系统 | MVP | Core | M |
| 12 | C2 外交协议系统 | MVP | Feature | M |
| 13 | E 内部政治系统 | MVP | Feature | M |
| 14 | G 势力AI框架 | MVP | Feature | L |
| 15 | J 胜负判定系统 | MVP | Feature | S |
| 16 | K UI框架 | MVP | Presentation | L |
| 17 | B3 能源贸易网络 | MVP | Core | M |
| 18 | D2 军事-政治联动 | MVP | Feature | M |
| 19 | C3 意识形态输出系统 | Alpha | Feature | M |
| 20 | F1 情报系统 | Alpha | Feature | M |
| 21 | A2 回合阶段引擎 | Alpha | Feature | S |
| 22 | B4 汇率系统 | Alpha | Feature | S |
| 23 | C4 联盟系统 | Alpha | Feature | M |
| 24 | C5 国际组织系统 | Alpha | Feature | S |
| 25 | D3 代理人民事系统 | Alpha | Feature | M |
| 26 | D4 核威慑系统 | Alpha | Feature | S |
| 27 | D6 军事科技系统 | Alpha | Feature | M |
| 28 | H3 地形与视野系统 | Alpha | Feature | S |
| 29 | I1 事件系统 | Alpha | Narrative | L |
| 30 | L2 新手教程系统 | Alpha | Meta | M |

**Effort estimates**: S = 1 session, M = 2-3 sessions, L = 4+ sessions

---

## Circular Dependencies

- **None found** ✅

所有系统依赖链均为单向，无循环。

**注**: E（内部政治系统）与D2（军事-政治联动）存在双向影响（军事行动影响派系，派系影响行动可用性），但通过A3（资源）作为中介解耦，不构成真正的循环依赖。

---

## High-Risk Systems

| System | Risk Type | Risk Description | Mitigation |
|--------|-----------|-----------------|------------|
| **A1 回合主循环** | Design | 一旦设计有问题，所有系统全部重来；阶段顺序和行动点消耗影响全局平衡 | 优先原型验证；用最简单的数值跑通全流程 |
| **B1 星币-能源金融** | Design + Balance | 经济系统设计错误导致游戏无法平衡；封锁/反封锁机制是核心乐趣来源 | 参考Machinations工具模拟；在prototype阶段重点测试经济闭环 |
| **G 势力AI框架** | Design | AI行为如果不够丰富，游戏后期无聊；多势力共用同一框架但人格差异大 | 第二阶段优先做；参考Paradox的AI人格系统设计 |
| **C3 意识形态输出** | Design | 软实力数值化可能变成数值游戏而非叙事工具 | MVP阶段先做简化版（单向效果，暂不引入感染率） |
| **I1 事件系统** | Scope | 事件量大（150+），质量难以保证 | 第一阶段只做10-15个核心事件模板；后续用模版化事件生成 |

---

## Progress Tracker

| Metric | Count |
|--------|-------|
| Total systems identified | 30 |
| Design docs started | 30 |
| Design docs reviewed | 30 |
| Design docs approved | 0 |
| MVP systems designed | 16/16 |
| Alpha systems designed | 10/10 |
| Full Vision systems designed | 4/4 |

---

## Next Steps

- [x] Review and approve this systems enumeration (updated to 30 systems)
- [ ] Design MVP-tier systems first (use `/design-system [system-name]`)
- [ ] Run `/design-review` on each completed GDD
- [ ] Run `/gate-check pre-production` when MVP systems are designed
- [ ] Prototype the highest-risk system early (`/prototype [system]`)
