# 瓦希德帝国 (EventideAge) - Unity Project

## 项目概述

**游戏类型**: 回合制4X策略游戏
**引擎**: Unity 6 (6000.x)
**语言**: C#
**架构**: ScriptableObject + Event-Driven

**玩家扮演**: 伊朗（瓦希德帝国）对抗西方联盟（黄金领）
**核心玩法**: 军事行动、外交斡旋、能源经济、抵抗轴心联盟

## 技术架构

### 核心组件
- **GameState**: ScriptableObject 数据容器
- **GameEvents**: 事件通道（OnTurnChanged, OnPhaseChanged, OnResourceChanged等）
- **GameManager**: MonoBehaviour单例，管理回合循环和系统协调
- **GameSystem**: 所有系统的基类

### 30系统实现状态

| 类别 | 系统 | 状态 |
|------|------|------|
| A (核心) | A1-A5 | ✅ 完成 |
| B (经济) | B1-B5 | ✅ 完成 |
| C (外交) | C1-C5 | ✅ 完成 |
| D (军事) | D1-D6 | ✅ 完成 |
| E | InternalPolitics | ✅ 完成 |
| F | Intelligence | ✅ 完成 |
| G | FactionAI | ✅ 完成 |
| H | StrategicMap | ✅ 完成 |
| I | Events | ✅ 完成 |
| J | VictoryDefeat | ✅ 完成 |

## Unity Editor 操作指南

### 首次设置（按顺序执行）
```
1. 打开 Unity Editor，加载 C:\test\4X 项目

2. 生成配置资源:
   Menu: EventideAge → Generate Default GameConfig
   Menu: EventideAge → Generate GameState with Default Config

3. 创建场景:
   Menu: EventideAge → Create Boot Scene
   Menu: EventideAge → Create Main Game Scene

4. 创建游戏管理:
   Menu: EventideAge → Setup Complete Scene → "在当前场景中创建 GameManager"
```

### 运行测试
```
Menu: EventideAge → Run All Tests
```

### 系统引用自动绑定
GameManager 在 Start() 时自动通过反射绑定跨系统引用（D1↔D6, D1↔D2, D2↔J等）

## 协作协议

**用户驱动，非自主执行**
每个任务遵循: **问题 → 选项 → 决策 → 草稿 → 审批**

- 使用 Write/Edit 工具前必须询问 "May I write this to [filepath]?"
- 变更前必须展示草稿或摘要请求批准
- 多文件变更需要明确的完整变更集审批
- 未经用户指示不得提交

## 代码标准

### 命名规范
- 类名: PascalCase
- 方法名: PascalCase  
- 私有字段: _camelCase
- 常量: kPascalCase
- 枚举值: PascalCase

### 事件模式
```csharp
// 正确：事件通过 GameEvents 触发
Events.ResourceChanged("GoldLeaf", oldAmount, newAmount);

// 错误：不要直接修改其他系统
```

### 系统隔离
- Core assembly 不得引用 Systems assembly
- Systems 之间通过属性引用（由 GameManager 绑定）
- 使用 `Events.OnTurnEnded` 订阅回合事件

## 项目约定

1. **ScriptableObject**: 所有数据资产使用 CreateAssetMenu
2. **无 TextMeshPro**: 使用 UnityEngine.UI.Text
3. **回合编号**: 从1开始
4. **存档格式**: JSON (Application.persistentDataPath)

## 后续计划

### 待完成
1. **编译验证**: 在Unity Editor中验证所有脚本编译通过
2. **UI完善**: 为所有面板创建具体Unity UI Prefab
3. **游戏平衡**: 根据Alpha测试调整数值
4. **AI完善**: G系统AI决策树
5. **地图编辑器**: H系统的可视化编辑工具

### 立即执行步骤
```
1. 在 Unity Editor 中打开 C:\test\4X
2. 执行 Menu: EventideAge → Setup Complete Scene
3. 检查 Console 是否有编译错误
4. 如有错误，根据错误信息修复
5. 执行 Menu: EventideAge → Run All Tests
6. 报告测试结果
```

## 关键文件路径

```
Assets/Scripts/
├── Core/           # GameState, GameEvents, GameManager, GameSystem, GameConfig
├── Systems/        # A1-J 所有30个系统
├── UI/             # UIManager 和各系统UI组件
└── Tests/          # StandaloneTest, IntegrationTest

Assets/Editor/
├── SceneSetup.cs          # GameManager预制体创建
├── GameConfigGenerator.cs  # 配置生成器
└── SceneCreator.cs        # 场景创建工具

Assets/ScriptableObjects/   # 运行时生成 (需先执行菜单)
```

## 文档

- `@SETUP_GUIDE.md` - 详细Unity设置指南
- `@QUICKREF.md` - 快速参考
- `@design/gdd/` - 30个GDD设计文档
- `@production/` - 冲刺计划和阶段记录
