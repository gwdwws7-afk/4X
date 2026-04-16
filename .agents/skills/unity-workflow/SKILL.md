---
name: unity-workflow
description: "Unity project workflow - compile, test, and iterate"
argument-hint: "[action: compile|test|balance|ui]"
user-invocable: true
allowed-tools: Read, Glob, Grep, Write, Edit, Bash
---

# Unity项目工作流

## 编译验证 (compile)

### 在Unity Editor中执行
1. 打开 C:\test\4X 项目
2. 执行 `EventideAge → Setup Complete Scene`
3. 检查 Console 是否有错误

### 常见编译错误修复
```
错误: "The type or namespace name 'X' could not be found"
修复: 添加 using EventideAge.[命名空间];

错误: "NullReferenceException"
修复: 确保ScriptableObject资产已创建 (执行 Generate Default GameConfig)

错误: "Assembly has compilation errors"
修复: 检查所有 using 语句，缺少哪个命名空间
```

## 测试运行 (test)

### 执行测试
```
Menu: EventideAge → Run All Tests
```

### 测试结果解读
- `[PASS]` = 测试通过
- `[FAIL]` = 测试失败，需要修复
- 报告格式: "X passed, Y failed"

## 游戏平衡 (balance)

### 调整数值位置
| 系统 | 文件 | 调整项 |
|------|------|---------|
| D1军事 | MilitaryOperationsSystem.cs | 成功率、成本 |
| D4核威慑 | NuclearDeterrenceSystem.cs | 弹头阈值、威慑代价 |
| D6科技 | MilitaryTechSystem.cs | 研发成本、周期 |
| J胜负 | VictoryDefeatSystem.cs | 胜利阈值 |

### 关键数值
- 胜利阈值: VictoryThreshold = 80%
- 军事崩溃: MilitaryCollapseAshWillThreshold = 30
- 核弹头绝对阈值: AbsoluteThreshold = 61

## UI完善 (ui)

### UI组件位置
- VictoryProgressUI.cs: 胜利进度条
- MilitaryTechUI.cs: 科技树
- NuclearDeterrenceUI.cs: 核威慑状态
- ProxyAffairsUI.cs: 代理民政

### 创建UI Prefab步骤
1. 在Unity中创建 Panel (UI → Panel)
2. 添加对应UI组件脚本
3. 配置引用 (拖拽赋值)
4. 保存为Prefab

### UIManager面板列表
```csharp
VictoryProgressPanel    // 胜利进度
TechTreePanel           // 科技树
NuclearStatusPanel      // 核威慑
ProxyAffairsPanel       // 代理民政
```
