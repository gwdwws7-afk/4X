# A2 回合阶段引擎 (Turn Phase Engine)

> **状态**: LOCKED
> **最后更新**: 2026-04-08
> **锁版来源**: `design/gdd/ssot-core-rules.md` (`EVA-SSOT-RULES-2026-04-08.v2`)

## Overview 概述

A2 是 A1 的执行器：负责阶段切换、阶段AP与通用AP的消费执行，不重新定义回合规则。

## 固定执行口径

### 阶段顺序

`0外交 -> 1战略 -> 2作战 -> 3后勤 -> 4情报 -> 5AI响应`

### AP执行

- 阶段基础 AP：`2/2/3/1/1/0`
- 通用 AP：每回合 `2`
- 总 AP：每回合 `11`

### AP消费约束

- 通用AP池在同一回合内可跨阶段复用。
- 阶段切换不会重置通用AP池。
- 回合结束后通用AP池归零。

### 切换规则

- 玩家主动结束阶段，或阶段行动不可继续时进入下一阶段。
- A2 不引入与 SSOT 冲突的自动超时强制结束规则。

## 数据接口（摘要）

- `GetCurrentPhase()`
- `GetPhaseBaseAP(phaseIndex)`
- `GetUniversalPointsRemaining()`
- `AdvanceToNextPhase()`

## 变更约束

- A2 不得单独更改阶段顺序、AP总量、时间模型。
- 如需变更，必须先更新 SSOT 与一致性矩阵。

