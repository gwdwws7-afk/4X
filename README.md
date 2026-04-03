# 抵抗轴心 (The Eventide Age) - Unity Project Setup Guide

## Sprint 1 完成状态 ✅

所有代码已创建，现在需要在 Unity Editor 中完成配置。

---

## 快速设置（5分钟）

### 步骤 1: 在 Unity Editor 中打开项目

```
File → Open Project → 选择 C:\test\4X
```

### 步骤 2: 运行自动设置

在 Unity Editor 菜单栏:
```
EventideAge → Setup Project
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
│   │   ├── A5/GameClock        # 2028.0格式时间
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
| 2 | 作战 | 2 |
| 3 | 后勤 | 1 |
| 4 | 情报 | 1 |
| 5 | AI响应 | 0 |

+ 2 通用AP（可用于任意阶段）

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
2. 使用菜单 `EventideAge → Setup Project` 自动完成

---

## 后续 Sprint

- B1-B5: 经济系统 (能源金融/封锁/贸易/汇率/结算)
- C1-C5: 外交系统 (关系/协议/意识形态/联盟/国际组织)
- D1-D6: 军事系统 (行动/联动/民事/核威慑/结算/科技)
- E: 内部政治 (派系)
- F1: 情报系统
- G: AI 框架
- I1: 事件系统
- J: 胜负判定
- L1-L4: 多人/Steam/本地化
