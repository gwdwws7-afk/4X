# 项目目录结构

## C:\test\4X

```
C:\test\4X\
├── Assets\                          # Unity资源
│   ├── Scripts\                     # 所有C#脚本
│   ├── Editor\                      # Unity Editor工具
│   ├── Prefabs\                     # 预制体
│   ├── Scenes\                     # 场景
│   ├── ScriptableObjects\           # ScriptableObject资产
│   └── Resources\                   # 运行时资源
├── design\                          # 游戏设计文档
│   └── gdd\                         # 30个GDD文件
│       ├── a2-phase-engine.md
│       ├── d1-military-operations.md
│       ├── j-victory-defeat.md
│       └── ... (共30+ GDD)
├── production\                      # 生产文档
│   ├── sprints\                     # 冲刺计划
│   ├── stage.txt                   # 当前阶段
│   └── session-state\              # 会话状态
├── prototypes\                      # 原型
│   └── core-loop-prototype\        # 核心循环原型
├── .claude\                         # Claude Code配置
│   ├── skills\                     # 技能文件
│   └── docs\                       # 文档
├── CLAUDE.md                        # 项目主说明
├── CONTINUATION.md                  # 后续执行指南
├── SETUP_GUIDE.md                  # Unity设置指南
└── QUICKREF.md                     # 快速参考
```
```

## 关键文件说明

| 文件 | 用途 |
|------|------|
| `GameManager.cs` | 单例，协调所有系统 |
| `GameState.cs` | ScriptableObject数据容器 |
| `GameEvents.cs` | 事件发布/订阅通道 |
| `SaveSystem.cs` | 存档读写 |
| `SaveData.cs` | 存档数据结构 |

## 系统命名空间

| 前缀 | 命名空间 |
|------|----------|
| Core | `EventideAge.Core` |
| Systems | `EventideAge.Systems.[ID]` |
| UI | `EventideAge.UI` |
| Tests | `EventideAge.Tests` |
| Editor | `EventideAge.Editor` |
