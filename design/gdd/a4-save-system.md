# A4 存档系统 (Save System)

> **状态**: Draft
> **作者**: GDC Framework / 游戏设计师
> **最后更新**: 2026-03-31
> **实现支柱**: P3（游戏可维护性）

## Overview 概述

存档系统负责游戏的保存和读取，包括完整存档、快速存档、自动存档。

## Detailed Design 详细设计

### Core Rules 核心规则

#### 存档类型

| 存档类型 | 触发方式 | 保存内容 | 位置 |
|---------|---------|---------|------|
| **完整存档** | 玩家手动 | 完整游戏状态 | 本地/云端 |
| **快速存档** | 快捷键S | 完整游戏状态（覆盖） | 本地 |
| **自动存档** | 每回合开始 | 完整游戏状态（不覆盖） | 本地 |

#### 存档内容

```
存档数据 = {
    游戏版本: string,
    回合数: number,
    游戏时间: string,
    当前阶段: Phase,
    
    // A3资源系统
    资源状态: ResourceState,
    
    // H2战略节点
    节点状态: NodeState[],
    
    // C1外交关系
    外交关系: RelationState,
    
    // C2外交协议
    有效协议: Protocol[],
    
    // B2封锁状态
    封锁等级: BlockadeLevel,
    
    // D1军事状态
    争夺点数: ContestPoints,
    
    // G AI记忆
    AI记忆: AIMemory,
    
    // J胜负
    胜利进度: VictoryProgress,
    
    // UI设置
    设置: GameSettings
}
```

#### 存档位置

```
本地存档位置：
- Windows: %APPDATA%/EventideAge/saves/
- macOS: ~/Library/Application Support/EventideAge/saves/

云端存档（可选）：
- Steam Cloud（若集成L3）
- 自动同步本地和云端
```

### 自动存档规则

```
自动存档触发时机：
- 每回合开始时（回合结算完成后）
- 自动存档保留最近3个
- 超出3个时自动删除最早的

手动存档：
- 无数量限制
- 玩家可自定义名称
- 存档列表按时间排序
```

## Dependencies 依赖关系

### 上游依赖

| 系统 | 依赖性质 | 依赖内容 |
|------|---------|---------|
| A1 回合主循环 | 硬依赖 | 回合触发存档 |
| K UI框架 | 硬依赖 | 存档菜单UI |

### 下游依赖

| 系统 | 依赖性质 | 依赖内容 |
|------|---------|---------|
| L3 Steam集成 | 软依赖 | Steam Cloud存档 |

## Data Interfaces 数据接口

```typescript
interface SaveSystem {
    saveGame(slot: SaveSlot): SaveResult
    loadGame(slot: SaveSlot): LoadResult
    deleteSave(slot: SaveSlot): boolean
    getSaveList(): SaveMetadata[]
    autoSave(): void
}

interface SaveSlot {
    id: string
    name: string
    type: 'manual' | 'quick' | 'auto'
    turn: number
    gameTime: string
    createdAt: Date
}
```

## Acceptance Criteria 验收标准

- [ ] 手动存档正确保存
- [ ] 快速存档正确保存/读取
- [ ] 自动存档每回合触发
- [ ] 存档列表正确显示
- [ ] 存档损坏时正确处理

## Open Questions 待解决问题

无。
