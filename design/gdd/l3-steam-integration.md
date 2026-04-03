# L3 Steam集成 (Steam Integration)

> **状态**: Draft
> **作者**: GDC Framework / 游戏设计师
> **最后更新**: 2026-03-31
> **实现支柱**: P4（平台集成）

## Overview 概述

Steam集成系统将游戏与Steam平台深度整合，包括成就、云存档、社区功能等。

## Detailed Design 详细设计

### Core Rules 核心规则

#### Steam功能集成

| 功能 | 说明 | 实现方式 |
|------|------|---------|
| **云存档** | 自动同步存档到Steam Cloud | Steam Cloud API |
| **成就系统** | 解锁游戏成就 | Steam Achievement API |
| **Steam Overlay** | 游戏内访问Steam社区 | Steam Overlay API |
| **统计** | 游戏内统计追踪 | Steam Statistics API |
| **创意工坊** | MOD支持（未来） | Steam Workshop API |

#### 成就列表

| 成就ID | 名称 | 描述 | 难度 |
|--------|------|------|------|
| FIRST_STEP | 初出茅庐 | 完成第一回合 | 简单 |
| ENEMY_OF_MY_ENEMY | 敌人的敌人 | 与灰烬众结盟 | 简单 |
| DEAL_MAKER | 交易大师 | 签订3份贸易协议 | 中等 |
| PERSIAN_GULF_MASTER | 波斯湾之主 | 控制所有火海节点 | 困难 |
| ENERGY_LIBERATION | 能源解放 | 达成能源解放胜利 | 困难 |
| DIPLOMATIC_VICTORY | 外交胜利 | 达成外交解决胜利 | 中等 |
| AXIS_VICTORY | 抵抗轴心胜利 | 达成抵抗轴心胜利 | 困难 |
| STALEMATE_MASTER | 势均力敌 | 达成军事均势胜利 | 极困难 |
| IRON_WALLET | 铁钱包 | 经济崩溃后恢复 | 中等 |
| SURVIVOR | 幸存者 | 存活超过回合20 | 中等 |

#### 云存档

```
云存档触发：
- 每回合结束时自动上传
- 玩家手动存档时上传
- 读取时优先读取云端

存档冲突处理：
- 检测到冲突时显示选项
- 玩家可选择本地或云端
```

## Dependencies 依赖关系

### 上游依赖

| 系统 | 依赖性质 | 依赖内容 |
|------|---------|---------|
| A4 存档系统 | 硬依赖 | 存档数据格式 |

## Acceptance Criteria 验收标准

- [ ] Steam登录正确
- [ ] 成就正确解锁
- [ ] 云存档正确同步
- [ ] Steam Overlay正确工作

## Open Questions 待解决问题

无。
