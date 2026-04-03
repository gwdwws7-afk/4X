# B4 汇率系统 (Exchange Rate System)

> **状态**: Draft
> **作者**: GDC Framework / 游戏设计师
> **最后更新**: 2026-03-31
> **实现支柱**: P2（经济深度系统）

## Overview 概述

汇率系统定义各货币之间的兑换比率。B1已经处理了基础结算，汇率系统处理的是"汇率波动"——即不同货币之间的兑换比例随市场变化而波动。

## Player Fantasy 玩家幻想

玩家感受到"汇率是机会，也是风险"。

今天商盟券对星币汇率是1:1，但明天可能变成0.9:1。你手上的商盟券换的金叶变少了——但如果你早知道会跌，你就不会签那份协议。汇率波动让经济多了一层不确定性。

## Detailed Design 详细设计

### Core Rules 核心规则

#### 汇率定义

```
汇率表（基准）：
1 金叶 = 1 星币（基准）
1 金叶 = 0.875 商盟券（瓦希德与东土商盟）
1 金叶 = 0.75 北境银（北境本币）

汇率波动范围：
- 金叶 vs 星币：±5%（锚定美元）
- 金叶 vs 商盟券：±15%
- 金叶 vs 北境银：±20%
```

#### 汇率波动机制

```
每5回合评估一次汇率：
波动 = random(-0.1, +0.1) × 基础汇率

影响因素：
- 封锁程度（影响商盟券价值）
- 北境政治关系（影响北境银价值）
- 全球经济事件
```

## Dependencies 依赖关系

### 上游依赖

| 系统 | 依赖性质 | 依赖内容 |
|------|---------|---------|
| B1 星币-能源金融 | 硬依赖 | 基础汇率定义 |

## Data Interfaces 数据接口

```typescript
interface ExchangeRateSystem {
    getExchangeRate(from: Currency, to: Currency): number
    getAllRates(): ExchangeRateMap
}
```

## Acceptance Criteria 验收标准

- [ ] 汇率正确显示
- [ ] 波动正确计算
- [ ] B1正确使用汇率

## Open Questions 待解决问题

无。
