# A5 游戏时钟 (Game Clock)

> **状态**: LOCKED
> **最后更新**: 2026-04-08
> **锁版来源**: `design/gdd/ssot-core-rules.md` (`EVA-SSOT-RULES-2026-04-08.v2`)

## Overview 概述

A5 负责回合时间显示与时间相关事件触发，是 A1 回合推进的下游执行器。

## 固定执行口径

- `1回合 = 6个月`
- 起始时间：`2028 H1`
- 最大回合：`24`
- 战役时间窗：`2028H1 ~ 2039H2`

## 时间显示规则

- 奇数回合显示 `H1`
- 偶数回合显示 `H2`
- 年份公式：`Year = 2028 + floor((Turn - 1) / 2)`
- 显示示例：`2028 H1`, `2028 H2`, `2029 H1`

## 终局触发边界

- 当 `CurrentTurn >= 24` 且尚未分胜负时，进入持久战终局判定流程（由 J 负责终局裁决）。
- A5 不得定义与 SSOT 冲突的“2028.0/2028.5/2040.0”时间模型。

## 数据接口（摘要）

- `GetCurrentTimeDisplay()`
- `GetGameProgress()`
- `IsGameNearEnd()`
- `TurnsRemaining()`

## 变更约束

- A5 不得单独修改回合长度、起始时间、回合上限与显示格式。
- 如需变更，必须先更新：
  - `design/gdd/ssot-core-rules.md`
  - `design/gdd/rules-consistency-matrix.md`

