# 抵抗轴心 (The Eventide Age) - Unity Project Setup Guide

## 项目阶段状态（Pre-Production）

当前工程处于“可运行原型基线已建立、待 Alpha 验证”阶段。

## 规则锁版（SSOT）

- 锁版号: `EVA-SSOT-RULES-2026-04-08.v2`
- 唯一规则源: `design/gdd/ssot-core-rules.md`
- 唯一企划案: `design/gdd/eventideage-unified-gdd.md`
- 一致性矩阵: `design/gdd/rules-consistency-matrix.md`
- 若本文与其他文档冲突，以 SSOT 为准。

---

## 快速设置（5分钟）

### 步骤 1: 在 Unity Editor 中打开项目

```
File → Open Project → 选择 C:\test\4X
```

### 步骤 2: 运行自动设置

在 Unity Editor 菜单栏:
```
EventideAge → Setup Complete Scene (one click)
EventideAge → Setup Project (alias, same pipeline)
```

这将自动创建:
- ScriptableObject 资产 (GameState, GameEvents, DefaultGameConfig)
- 配置 GameManager 并挂载所有 System 组件
- 设置基础 UI Canvas

### 步骤 3: 验证设置

1. 在 Hierarchy 中确认 GameManager 对象存在
2. 点击 GameManager，Inspector 中确认:
   - State → GameState (资产)
   - Events → GameEvents (资产)
   - Config → DefaultGameConfig (资产)
   - Systems → 7个系统组件

3. Play 运行，在 Console 查看:
   ```
   [GameManager] Initialized. Turn 1, Phase 0
   [PhaseEngine] Entered Phase 0: 外交
   ```

---

## 项目结构

```
Assets/
├── Scripts/
│   ├── Core/
│   │   ├── GameState.cs        # 游戏数据 ScriptableObject
│   │   ├── GameEvents.cs       # 事件通道
│   │   ├── GameConfig.cs       # 配置基类
│   │   ├── GameSystem.cs       # System 基类
│   │   └── GameManager.cs      # 游戏管理器 (场景单例)
│   ├── Systems/
│   │   ├── A1/TurnLoopSystem   # 6阶段/11行动点
│   │   ├── A2/PhaseEngine      # 阶段切换
│   │   ├── A3/ResourceSystem   # 7种资源
│   │   ├── A4/SaveSystem       # JSON存档
│   │   ├── A5/GameClock        # H1/H2 timeline (Turn1=2028H1, Turn2=2028H2)
│   │   └── H1/StrategicMap    # 6区域12节点
│   ├── UI/
│   │   ├── UIManager           # 主UI管理器
│   │   ├── ResourceItemUI      # 资源条项
│   │   ├── PhaseIndicatorUI    # 阶段指示器
│   │   └── ActionButtonUI      # 行动按钮
│   ├── Config/
│   │   └── DefaultGameConfig   # 默认游戏配置
│   └── Tests/
│       └── IntegrationTest     # 集成测试
├── Config/
│   └── DefaultGameConfig.cs    # 配置数据
├── Editor/
│   └── ProjectSetup.cs         # 自动设置工具
└── Scenes/
    └── MainScene.unity         # 主场景
```

---

## 核心架构

### GameState (ScriptableObject)
- 唯一数据源：势力、资源、地图
- 可序列化存档
- Editor 可视化调试

### GameEvents (ScriptableObject)
- 系统间松耦合通信
- 事件：OnTurnChanged, OnPhaseChanged, OnResourceChanged 等

### GameManager (场景单例)
- 协调所有 Systems
- 管理游戏生命周期
- 处理 AP 消耗

---

## 6阶段回合结构

| Phase | 名称 | AP |
|-------|------|-----|
| 0 | 外交 | 2 |
| 1 | 战略 | 2 |
| 2 | 作战 | 3 |
| 3 | 后勤 | 1 |
| 4 | 情报 | 1 |
| 5 | AI响应 | 0 |

+ 2 通用AP（可用于任意阶段）

总AP口径：`2 + 2 + 3 + 1 + 1 + 0 + 2 = 11`

时间口径：`1回合 = 6个月`，`最大24回合`

---

## 胜负规则（锁版摘要）

- 胜利路径固定4条：能源解放 / 军事均势 / 抵抗轴心胜利 / 外交解决
- 单路径进度 `>=80` 触发胜利
- 任意2条路径都 `>=80` 触发组合胜利
- 失败条件：`AshWill < 30` 或 `FireOil <= 0` 或 任一派系满意度 `< 15`
- `CurrentTurn >= 24` 且未分胜负时触发持久战终局

---

## 7种资源

| ID | 名称 | 类型 |
|----|------|------|
| Arms | 战械 | 消耗型 |
| FireOil | 火油 | 累积型 |
| GoldLeaf | 金叶 | 累积型 |
| TradeToken | 商盟券 | 累积型 |
| SocialValue | 社稷值 | 比率型 |
| AshWill | 灰烬志 | 比率型 |
| TributeOrder | 朝贡序 | 比率型 |

---

## 6个区域/12个节点

| 区域 | 节点 |
|------|------|
| 波斯湾 | 霍尔木兹, 布什尔 |
| 西线 | 伊拉克边境, 叙利亚区域 |
| 北境 | 里海油田, 高加索通道 |
| 阿拉伯半岛 | 红海通道, 海湾基地 |
| 黎凡特 | 地中海东岸, 以色列核心 |
| 中亚 | 阿富汗走廊, 贸易枢纽 |

---

## 测试

运行 IntegrationTest:
1. GameManager 上添加 IntegrationTest 组件
2. 引用 GameManager
3. Play Mode
4. Console 输出 `[PASS]` / `[FAIL]`

---

## 已知问题

1. 首次 Play 需要在 Editor 中设置 ScriptableObject 引用
2. 使用菜单 `EventideAge → Setup Complete Scene`（主入口）或 `EventideAge → Setup Project`（同一管线别名）自动完成

---

## 后续迭代重点（MVP→Alpha）

- B1-B5: 经济系统平衡与压测（能源金融/封锁/贸易/汇率/结算）
- C1-C5: 外交系统可读性与反馈强化（关系/协议/意识形态/联盟/国际组织）
- D1-D6: 军事系统闭环打磨（行动/联动/民事/核威慑/结算/科技）
- E/F/G: 派系、情报、AI决策树深度迭代
- I1/J: 事件触发密度与胜负节奏校准
- L1-L4: 多人/Steam/本地化（非当前MVP阻塞项）

