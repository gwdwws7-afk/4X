# J 胜负判定系统 (Victory/Defeat System)

> **状态**: LOCKED
> **最后更新**: 2026-04-08
> **锁版来源**: `design/gdd/ssot-core-rules.md` (`EVA-SSOT-RULES-2026-04-08.v2`)

## Overview 概述

J 负责每回合的胜负检测、风险预警与终局触发。

## 胜利路径（固定）

| 类型 | 英文ID |
|---|---|
| 能源解放 | EnergyLiberation |
| 军事均势 | MilitaryStalemate |
| 抵抗轴心胜利 | AxisVictory |
| 外交解决 | DiplomaticResolution |

## 胜利判定口径

- 每条路径进度范围：0~100
- 单路径胜利阈值：>=80
- 组合胜利阈值：>=2条路径同时>=80

## 失败判定口径

- 军事崩溃：`AshWill < 30`
- 经济崩溃：`FireOil <= 0`
- 内部分裂：任一派系满意度 `< 15`

## 超时终局

- 当 `CurrentTurn >= 24` 且尚未触发胜利/失败，触发 Attrition Timeout。

## 与时间规则关系

- 回合长度与回合上限由 SSOT 的时间规则定义。
- J 不得定义与 SSOT 冲突的“3年=24回合”等换算。

## 输出

- 当前四路径进度
- 失败风险状态
- 终局类型与触发原因

