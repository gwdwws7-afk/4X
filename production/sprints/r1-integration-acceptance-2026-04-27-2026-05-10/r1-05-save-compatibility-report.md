# R1-05 存档兼容与一致性报告（读写 / 多回合 / 异常恢复）

- 阶段：R1 集成验收（2026-04-27 ~ 2026-05-10）
- 对应任务：R1-05 存档兼容与一致性测试
- 脚本入口：`StandaloneTest.RunR1SaveCompatibilityChecks()`
- 关联日志目录：`production/evidence/r1/replay-logs/`

## 1. 用例范围
- 读写一致性：wrapped save（`GameSaveData`）写入与恢复
- 多回合一致性：多存档槽（turn4/turn5）写入、读取、枚举一致
- 异常恢复：缺失存档、损坏 wrapped payload、legacy save 兼容

## 2. 链路与脚本映射
| 链路ID | 验证目标 | 脚本方法 |
|---|---|---|
| SAVE-RW-RECOVERY | 读写一致 + 异常恢复 + 旧档兼容 | `TestR1SaveCompatibilityReadWriteAndRecovery()` |
| SAVE-MULTI-TURN | 多回合多槽位一致性 | `TestR1SaveCompatibilityMultiTurnConsistency()` |

## 3. 执行记录
| RunID | 执行时间 | 执行人 | 结果 | 证据路径 | 备注 |
|---|---|---|---|---|---|
| R1-05-RUN-01 | 2026-04-16 | Codex | PASS | `production/evidence/r1/replay-logs/20260416_151432_R1-05_SAVE-RW-RECOVERY.log` / `production/evidence/r1/replay-logs/20260416_151432_R1-05_SAVE-MULTI-TURN.log` | 10/10 步骤 PASS |
| R1-05-RUN-02 |  |  |  |  |  |

## 4. 断言明细（SAVE-RW-RECOVERY）
| 步骤 | 断言 | 结果(PASS/FAIL) | 实际观测 | 缺陷ID |
|---|---|---|---|---|
| S01 | wrapped save 可写入且存在 | PASS | Save=True, Exists=True |  |
| S02 | wrapped save 读取后关键状态恢复 | PASS | Turn=6, Phase=2, AP=6, PhaseAP=2, UAP=0, GoldLeaf=451, FireOil=177, Warheads=12, EndReason=r1_05_readwrite_snapshot |  |
| S03 | 缺失存档读取返回 false | PASS | Load=False（missing slot） |  |
| S04 | 损坏 wrapped payload 被拒绝且状态稳定 | PASS | Load=False, TurnBefore=6, TurnAfter=6 |  |
| S05 | legacy GameState-only 存档可读取 | PASS | Load=True, Turn=8, Phase=1, AP=9, PhaseAP=1, UAP=1, GoldLeaf=500 |  |

## 5. 断言明细（SAVE-MULTI-TURN）
| 步骤 | 断言 | 结果(PASS/FAIL) | 实际观测 | 缺陷ID |
|---|---|---|---|---|
| S01 | turn4 快照可写入 | PASS | Save=True, Turn=4, GoldLeaf=320, FireOil=210 |  |
| S02 | turn5 快照可写入 | PASS | Save=True, Turn=5, GoldLeaf=287, FireOil=198 |  |
| S03 | turn4 读档字段一致 | PASS | Turn=4, Phase=1, AP=8, PhaseAP=2, UAP=0, NodeControl=63, NodeController=Aurean |  |
| S04 | turn5 读档字段一致 | PASS | Turn=5, Phase=3, AP=5, PhaseAP=1, UAP=1, NodeControl=54, NodeController=Ash Confederacy |  |
| S05 | 存档列表可发现 turn4/turn5 槽位 | PASS | Turn4Found=True, Turn5Found=True, SaveCount=2 |  |

## 6. 结论
- [x] PASS：R1-05 达标，可进入 R1-06
- [ ] CONDITIONAL PASS：存在可控问题，需带约束进入 R1-06
- [ ] FAIL：阻塞问题未清除，不可进入 R1-06

## 7. 签收
- QA Lead：____________  日期：____________
- Tech Lead：___________  日期：____________
- Producer：____________  日期：____________
