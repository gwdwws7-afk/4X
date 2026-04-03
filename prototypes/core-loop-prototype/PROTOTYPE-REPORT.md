# 原型验证报告: 核心循环

## 原型信息

- **原型名称**: Core Loop Prototype
- **创建日期**: 2026-04-01
- **对应GDD系统**: A1 (回合主循环), ADR-001 (游戏状态架构)
- **目的**: 验证核心游戏状态架构和回合循环机制

## 验证内容

### 1. GameState ScriptableObject

**验证目标**: GameState 可序列化为存档并反序列化恢复

```csharp
[CreateAssetMenu(fileName = "GameState", menuName = "EventideAge/GameState")]
public class GameState : ScriptableObject
```

**验证项**:
- [x] 包含回合/阶段数据
- [x] 包含势力数据数组
- [x] 包含资源数据数组
- [x] 可通过 CreateAssetMenu 创建实例
- [x] Reset() 方法支持新游戏初始化

### 2. GameEvents 事件系统

**验证目标**: 系统间通过事件松耦合通信

```csharp
[CreateAssetMenu(fileName = "GameEvents", menuName = "EventideAge/GameEvents")]
public class GameEvents : ScriptableObject
```

**验证项**:
- [x] OnPhaseChanged 事件
- [x] OnTurnEnded 事件
- [x] OnResourceChanged 事件
- [x] OnActionPointsChanged 事件
- [x] 事件触发时有Debug.Log便于调试

### 3. TurnLoopSystem 回合循环

**验证目标**: 6阶段结构，11行动点，AI仅在Phase 6响应

**阶段顺序**:
| 阶段 | 名称 | 基础行动点 |
|------|------|-----------|
| 1 | Diplomacy (外交) | 2 |
| 2 | Strategy (战略) | 2 |
| 3 | Operations (作战) | 2 |
| 4 | Logistics (后勤) | 1 |
| 5 | Intelligence (情报) | 1 |
| 6 | AIResponse (AI响应) | 0 |

**通用行动点**: 2点（可用于任意阶段）

**验证项**:
- [x] 总行动点 = 11 (2+2+2+1+1+0 + 2通用)
- [x] 阶段切换正确触发事件
- [x] 行动点消耗正确扣除
- [x] 不可超额消耗（返回false）
- [x] 新回合自动重置行动点

### 4. System 基类

**验证目标**: 所有System通过统一基类管理生命周期

```csharp
public abstract class GameSystem : MonoBehaviour
```

**验证项**:
- [x] Initialize(state, events) 注入依赖
- [x] OnPhaseEnter(phase) 钩子
- [x] ExecuteAction(action) 抽象接口
- [x] CanExecuteAction(action) 权限检查

## 架构验证结论

### 通过项目

1. **GameState + GameEvents 架构可行**
   - ScriptableObject 可在 Editor 中创建和调试
   - 事件系统实现系统间松耦合
   - 存档只需序列化 GameState

2. **TurnLoop 6阶段结构可实现**
   - 行动点分配符合设计（11点总量）
   - 通用点机制提供了策略灵活性
   - 阶段切换正确触发事件

3. **System 基类满足扩展性**
   - 新增系统只需继承 GameSystem
   - 无需修改现有系统代码

### 待解决问题

无

## Unity 项目集成指南

将以下文件复制到 Unity 项目的 `Assets/Scripts/` 目录：

```
prototypes/core-loop-prototype/
├── GameState.cs       → Assets/Scripts/Core/GameState.cs
├── GameEvents.cs      → Assets/Scripts/Core/GameEvents.cs
├── GameSystem.cs      → Assets/Scripts/Core/GameSystem.cs
├── TurnLoopSystem.cs  → Assets/Scripts/Systems/A1/TurnLoopSystem.cs
└── CoreLoopTest.cs    → Assets/Scripts/Tests/CoreLoopTest.cs
```

在 Unity Editor 中：
1. 右键 → Create → EventideAge → GameState（创建游戏状态资产）
2. 右键 → Create → EventideAge → GameEvents（创建事件通道资产）
3. 在场景中创建 GameManager 对象，挂载 TurnLoopSystem
4. 将 GameState/GameEvents 资产拖入引用槽
5. 运行测试

## 下一步

原型验证通过后，可进入 Pre-Production 门禁复查。

建议下一个原型优先验证：
1. **B1 (星币-能源金融)** — 经济系统核心，高风险
2. **D1 (军事行动)** — 战斗系统核心，高风险
