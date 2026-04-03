# Unity 瓦希德帝国项目 - 快速参考

## 立即执行命令

### 1. 编译验证
```
在 Unity Editor 中:
1. EventideAge → Setup Complete Scene
2. 检查 Console 错误
```

### 2. 运行测试
```
Menu: EventideAge → Run All Tests
```

---

## 系统实现清单

| ID | 系统名 | 类文件 | 状态 |
|----|--------|--------|------|
| A1 | 回合循环 | TurnLoopSystem.cs | ✅ |
| A2 | 阶段引擎 | PhaseEngine.cs | ✅ |
| A3 | 资源系统 | ResourceSystem.cs | ✅ |
| A4 | 存档系统 | SaveSystem.cs | ✅ |
| A5 | 游戏时钟 | GameClock.cs | ✅ |
| B1 | 金融系统 | FinanceSystem.cs | ✅ |
| B2 | 封锁系统 | BlockadeSystem.cs | ✅ |
| B3 | 贸易网络 | TradeNetworkSystem.cs | ✅ |
| B4 | 汇率系统 | ExchangeRateSystem.cs | ✅ |
| B5 | 经济结算 | EconomicSettlementSystem.cs | ✅ |
| C1 | 外交关系 | DiplomaticRelationsSystem.cs | ✅ |
| C2 | 外交协议 | DiplomaticProtocolsSystem.cs | ✅ |
| C3 | 意识形态 | IdeologySystem.cs | ✅ |
| C4 | 联盟系统 | AllianceSystem.cs | ✅ |
| C5 | 国际组织 | InternationalOrgsSystem.cs | ✅ |
| D1 | 军事行动 | MilitaryOperationsSystem.cs | ✅ |
| D2 | 军事政治联动 | MilitaryPoliticalLinkageSystem.cs | ✅ |
| D3 | 代理民政 | ProxyCivilAffairsSystem.cs | ✅ |
| D4 | 核威慑 | NuclearDeterrenceSystem.cs | ✅ |
| D5 | 战争结算 | WarResolutionSystem.cs | ✅ |
| D6 | 军事科技 | MilitaryTechSystem.cs | ✅ |
| E | 内部政治 | InternalPoliticsSystem.cs | ✅ |
| F1 | 情报系统 | IntelligenceSystem.cs | ✅ |
| G | 派系AI | FactionAISystem.cs | ✅ |
| H1 | 战略地图 | StrategicMapSystem.cs | ✅ |
| I1 | 事件系统 | EventSystem.cs | ✅ |
| J | 胜负判定 | VictoryDefeatSystem.cs | ✅ |

---

## UI组件

| UI组件 | 文件 | 功能 |
|--------|------|------|
| 主HUD | UIManager.cs | 资源显示、阶段指示 |
| 胜利进度 | VictoryProgressUI.cs | 4路径进度条 |
| 科技树 | MilitaryTechUI.cs | 11科技研发 |
| 核威慑 | NuclearDeterrenceUI.cs | 弹头/威慑等级 |
| 代理民政 | ProxyAffairsUI.cs | 稳定度/民心 |

---

## 测试命令

```csharp
// 在 Unity Console 中
// 右键 TestRunner → Run All Tests
```

测试文件: `Assets/Scripts/Tests/StandaloneTest.cs`

---

## 关键文件路径

```
C:\test\4X\
├── Assets\Scripts\Systems\D1\MilitaryOperationsSystem.cs   # 军事行动核心
├── Assets\Scripts\Systems\J\VictoryDefeatSystem.cs          # 胜负判定核心
├── Assets\Scripts\Systems\D6\MilitaryTechSystem.cs          # 科技树
├── Assets\Scripts\Core\GameManager.cs                       # 系统协调器
├── Assets\Scripts\Core\GameState.cs                         # 数据容器
├── Assets\Scripts\Core\GameEvents.cs                        # 事件通道
├── Assets\Scripts\Systems\A4\SaveSystem.cs                   # 存档系统
└── Assets\Scripts\Systems\A4\SaveData.cs                    # 存档数据结构
```

---

## Unity菜单

```
EventideAge/
├── Generate Default GameConfig
├── Generate GameState with Default Config
├── Create Boot Scene
├── Create Main Game Scene
├── Setup Complete Scene → [3个子选项]
└── Run All Tests
```

---

## 下一步

1. **打开Unity Editor** 加载 `C:\test\4X`
2. **执行 Setup Complete Scene**
3. **检查Console** 是否有编译错误
4. **运行测试** EventideAge → Run All Tests
5. **报告结果**
