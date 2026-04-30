# 瓦希德帝国 - 后续执行指南

## 项目状态

这是一个基于 Unity 2022.3（2022.3.62f1）的回合制4X策略游戏项目，位于 `C:\test\4X`。

### 已完成
- ✅ 34个系统口径已统一 (A1-L4，待 Alpha 联调验证)
- ✅ 4个UI组件框架 (VictoryProgressUI, MilitaryTechUI, NuclearDeterrenceUI, ProxyAffairsUI)
- ✅ 测试框架 (StandaloneTest, IntegrationTest)
- ✅ 配置数据生成器 (DefaultGameConfig)
- ✅ Unity Editor工具 (SceneSetup, GameConfigGenerator)
- ✅ 存档系统扩展 (SaveData, SaveSystem扩展)

### 待完成
- ⬜ Unity编译验证
- ⬜ UI Prefab具体创建
- ⬜ 游戏平衡数值调整
- ⬜ AI决策树完善
- ⬜ 地图编辑器工具

---

## 立即执行步骤

### 步骤1: 打开Unity项目
在Unity Editor中打开 `C:\test\4X` 项目

### 步骤2: 生成配置资源
```
Menu: EventideAge → Generate Default GameConfig
Menu: EventideAge → Generate GameState with Default Config
```

### 步骤3: 创建GameManager
```
Menu: EventideAge → Setup Complete Scene (one click)
Menu: EventideAge → Setup Project (alias, same pipeline)
```

### 步骤4: 检查编译
- 查看Console是否有红色错误
- 如有错误，根据错误信息修复
- 常见问题：缺少using语句、空引用

### 步骤5: 运行测试
```
Menu: EventideAge → Run All Tests
```

### 步骤6: 报告结果
报告：
- 编译状态 (通过/有错误)
- 测试结果 (X passed, Y failed)
- 需要修复的问题列表

---

## 项目结构

```
C:\test\4X\
├── Assets\
│   ├── Scripts\
│   │   ├── Core\           # GameState, GameEvents, GameManager, GameSystem, GameConfig
│   │   ├── Systems\        # A1-L4 共34个系统（含K/L）
│   │   │   ├── A1\TurnLoopSystem.cs
│   │   │   ├── B1\FinanceSystem.cs
│   │   │   ├── D1\MilitaryOperationsSystem.cs
│   │   │   ├── J\VictoryDefeatSystem.cs
│   │   │   └── ... (共29个系统脚本)
│   │   ├── UI\             # UIManager + 4个UI组件
│   │   └── Tests\          # StandaloneTest, IntegrationTest
│   ├── Editor\             # SceneSetup, GameConfigGenerator等工具
│   └── ScriptableObjects\   # 运行时生成
├── CLAUDE.md               # 本文件，项目主说明
├── SETUP_GUIDE.md          # Unity设置详细指南
└── README.md               # 基础项目说明
```

---

## 系统快速参考

| 系统 | 关键方法 | 说明 |
|------|----------|------|
| D1 | ExecuteAction() | 执行军事行动 |
| D2 | ProcessNodeDigestion() | 节点政治消化 |
| D3 | ProcessTurnDecay() | 民政稳定度衰减 |
| D4 | ExecuteDeterrenceDisplay() | 核威慑展示 |
| D6 | StartResearch() | 开始科技研究 |
| J | CheckVictoryDefeat() | 胜负检测 |

---

## 关键数值 (待平衡)

| 参数 | 当前值 | 文件位置 |
|------|--------|----------|
| 胜利阈值 | 80% | J.VictoryDefeatSystem |
| 军事崩溃社稷值 | <30 | J.VictoryDefeatSystem |
| 核弹头绝对门槛 | 61+ | D4.NuclearDeterrenceSystem |
| 科技研发周期 | 3-8回合 | D6.MilitaryTechSystem |

---

## 对后续AI的指令

**开始前必须:**
1. 阅读 `CLAUDE.md` 了解项目架构
2. 阅读 `SETUP_GUIDE.md` 了解Unity操作流程
3. 在Unity Editor中打开项目验证当前状态

**如果遇到问题:**
1. 先尝试在Unity Console中找到具体错误
2. 根据错误信息定位到具体文件和行号
3. 修复后重新运行测试验证

**用户驱动原则:**
- 每次变更前必须询问用户
- 展示修改内容后再执行
- 不要未经批准直接修改大量代码
