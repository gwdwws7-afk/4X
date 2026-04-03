# EventideAge Unity Setup Guide

## 快速开始

### 1. 在Unity中打开项目
1. 打开Unity Editor
2. 选择 `C:\test\4X` 作为项目目录
3. 等待项目导入完成

### 2. 生成默认配置（MenuItem）
```
EventideAge → Generate Default GameConfig
EventideAge → Generate GameState with Default Config
```

### 3. 创建游戏场景（MenuItem）
```
EventideAge → Create Boot Scene
EventideAge → Create Main Game Scene
```

### 4. 创建GameManager（MenuItem）
```
EventideAge → Setup Complete Scene → "在当前场景中创建 GameManager"
```

这将自动创建：
- GameManager 游戏对象
- Systems 子对象（包含所有30个系统组件）
- UI 子对象（包含UIManager）
- 所有必要的ScriptableObject引用

## 系统列表

### 核心系统 (A)
| 系统 | 类名 |
|------|------|
| A1 | TurnLoopSystem |
| A2 | PhaseEngine |
| A3 | ResourceSystem |
| A4 | SaveSystem |
| A5 | GameClock |

### 经济系统 (B)
| 系统 | 类名 |
|------|------|
| B1 | FinanceSystem |
| B2 | BlockadeSystem |
| B3 | TradeNetworkSystem |
| B4 | ExchangeRateSystem |
| B5 | EconomicSettlementSystem |

### 外交系统 (C)
| 系统 | 类名 |
|------|------|
| C1 | DiplomaticRelationsSystem |
| C2 | DiplomaticProtocolsSystem |
| C3 | IdeologySystem |
| C4 | AllianceSystem |
| C5 | InternationalOrgsSystem |

### 军事系统 (D)
| 系统 | 类名 |
|------|------|
| D1 | MilitaryOperationsSystem |
| D2 | MilitaryPoliticalLinkageSystem |
| D3 | ProxyCivilAffairsSystem |
| D4 | NuclearDeterrenceSystem |
| D5 | WarResolutionSystem |
| D6 | MilitaryTechSystem |

### 其他系统
| 系统 | 类名 |
|------|------|
| E | InternalPoliticsSystem |
| F1 | IntelligenceSystem |
| G | FactionAISystem |
| H1 | StrategicMapSystem |
| I1 | EventSystem |
| J | VictoryDefeatSystem |

## 预制体结构

```
GameManager (预制体)
├── Config (GameConfig)
├── State (GameState)  
├── Events (GameEvents)
└── Systems/
    ├── TurnLoopSystem
    ├── PhaseEngine
    ├── ResourceSystem
    ├── SaveSystem
    ├── GameClock
    ├── FinanceSystem
    ├── BlockadeSystem
    ├── TradeNetworkSystem
    ├── ExchangeRateSystem
    ├── EconomicSettlementSystem
    ├── DiplomaticRelationsSystem
    ├── DiplomaticProtocolsSystem
    ├── IdeologySystem
    ├── AllianceSystem
    ├── InternationalOrgsSystem
    ├── MilitaryOperationsSystem
    ├── MilitaryPoliticalLinkageSystem
    ├── ProxyCivilAffairsSystem
    ├── NuclearDeterrenceSystem
    ├── WarResolutionSystem
    ├── MilitaryTechSystem
    ├── InternalPoliticsSystem
    ├── IntelligenceSystem
    ├── FactionAISystem
    ├── StrategicMapSystem
    ├── EventSystem
    └── VictoryDefeatSystem
```

## UI面板

| 面板 | 类名 |
|------|------|
| 主HUD | UIManager |
| 胜利进度 | VictoryProgressUI |
| 科技树 | MilitaryTechUI |
| 核威慑 | NuclearDeterrenceUI |
| 代理民政 | ProxyAffairsUI |

## 运行测试

在Unity中：
1. 在Hierarchy中找到 `TestRunner` 组件
2. 右键 → Run All Tests

或使用菜单：
```
EventideAge → Run All Tests
```

## 常见问题

### 编译错误
如果遇到编译错误，检查：
1. Assembly Definition References 是否正确配置
2. 所有 using 语句是否正确

### 引用缺失
如果系统引用显示 None：
1. 确保运行了 SceneSetup 中的 "在当前场景中创建 GameManager"
2. 检查 GameManager → Systems 列表是否包含所有系统

### 存档不工作
SaveSystem 需要正确初始化。确保：
1. SaveSystem 在 Systems 列表中
2. SaveData.cs 被正确编译
