# A3 资源系统 (Resource System)

> **状态**: LOCKED (Boundary)
> **最后更新**: 2026-04-08
> **锁版来源**: `design/gdd/ssot-core-rules.md` (`EVA-SSOT-RULES-2026-04-08.v2`)

## Boundary 边界声明

A3 不拥有以下规则定义权：

- 回合结构
- 阶段/AP规则
- 时间推进规则
- 胜负判定规则

上述规则由 SSOT（A1/A2/J）统一定义，A3 仅消费这些规则产生的结果。

## Core Scope 核心职责

- 维护资源数据模型（消耗型/累积型/比率型）。
- 提供资源读写接口：查询、消耗、增加、容量检查。
- 在回合结算触发点与经济/外交/军事系统联动。

## 资源清单（当前口径）

| 资源ID | 名称 | 类型 |
|---|---|---|
| Arms | 战械 | Consumable |
| FireOil | 火油 | Accumulative |
| GoldLeaf | 金叶 | Accumulative |
| TradeToken | 商盟券 | Accumulative |
| SocialValue | 社稷值 | Ratio |
| AshWill | 灰烬志 | Ratio |
| TributeOrder | 朝贡序 | Ratio |

## 说明

- 资源产生与消耗由行动和结算共同驱动。
- 与回合/AP/胜负相关的文字描述，若与 SSOT 冲突，一律作废。

