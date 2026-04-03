# C4 联盟系统 (Alliance System)

> **状态**: Draft
> **作者**: GDC Framework / 游戏设计师
> **最后更新**: 2026-03-31
> **实现支柱**: P2（外交深度系统）

## Overview 概述

联盟系统是C2外交协议的延伸，定义多边联盟的形成、维持和瓦解机制。

## Player Fantasy 玩家幻想

玩家感受到"联盟是力量，也是枷锁"。

你牵头组建了一个抵抗轴心联盟——包括灰烬众和两个两河小国。联盟让你在军事行动时有更多协同加成，外交谈判时有更多筹码。但当你的盟友被攻击时，你必须出手相助，否则联盟瓦解，你的国际声望暴跌。

## Detailed Design 详细设计

### Core Rules 核心规则

#### 联盟类型

| 联盟类型 | 英文 | 成员要求 | 效果 |
|---------|------|---------|------|
| **双边联盟** | Bilateral Alliance | 2方 | C2已有定义（联合防御条款） |
| **抵抗轴心联盟** | Axis Alliance | 瓦希德+至少1个灰烬众成员 | 协同军事行动+10% |
| **区域联盟** | Regional Alliance | 3方以上 | 联合防御、经济合作 |

#### 联盟形成

```
联盟形成条件：
1. 任意2方以上签订C2联合防御条款
2. 联盟协调行动机制建立（C2抵抗轴心协调协议）
3. 联盟维持成本支付

联盟形成后：
- 所有成员获得联盟标签
- 联盟效果生效
- 联盟维护成本计算
```

#### 联盟维持

```
联盟维持条件：
1. 每回合支付联盟维护成本（朝贡序-3）
2. 成员关系不低于"友好"（+40）
3. 无成员主动退盟

联盟维护成本：
- 成员数量 × 2 朝贡序/回合
```

#### 联盟瓦解

```
联盟瓦解条件：
- 成员关系降至冷淡（-40）以下
- 成员主动退盟（消耗朝贡序-10）
- 核心成员被消灭（节点丢失）

瓦解效果：
- 联盟效果立即消失
- 成员关系-10
- 朝贡序-15
```

## Dependencies 依赖关系

### 上游依赖

| 系统 | 依赖性质 | 依赖内容 |
|------|---------|---------|
| C1 外交关系 | 硬依赖 | 关系门槛 |
| C2 外交协议 | 硬依赖 | 联合防御条款 |

## Data Interfaces 数据接口

```typescript
interface AllianceSystem {
    createAlliance(members: FactionId[]): Alliance
    getAlliance(allianceId: string): Alliance
    getAllianceEffects(allianceId: string): AllianceEffect[]
    dissolveAlliance(allianceId: string): void
}

interface Alliance {
    allianceId: string
    members: FactionId[]
    type: AllianceType
    stability: number
    effects: AllianceEffect[]
}
```

## Acceptance Criteria 验收标准

- [ ] 联盟正确形成
- [ ] 联盟效果正确应用
- [ ] 联盟维持成本正确扣除
- [ ] 联盟瓦解正确处理

## Open Questions 待解决问题

无。
