# R1-04 跨回合一致性报告（AP / 资源 / 节点控制 / 胜负状态）

- 阶段：R1 集成验收（2026-04-27 ~ 2026-05-10）
- 对应任务：R1-04 验证跨回合一致性
- 回放脚本入口：`StandaloneTest.RunR1CrossTurnConsistencyChecks()`
- 关联日志目录：`production/evidence/r1/replay-logs/`（结构化步骤日志）

## 1. 用例范围
- AP 一致性：跨阶段消耗、跨回合重置（11/2/2）
- 资源一致性：跨回合资源值不漂移（GoldLeaf / FireOil）
- 节点控制一致性：节点控制点与控制方跨回合保持一致
- 胜负状态一致性：终局状态跨回合稳定，不重复派发

## 2. 脚本映射
| 验证项 | 脚本方法 | 日志链路ID |
|---|---|---|
| R1-04 一致性主检查 | `TestR1CrossTurnConsistencyApResourceNodeVictory()` | `CONSISTENCY` |

## 3. 执行记录
| RunID | 执行时间 | 执行人 | 结果 | 证据路径 | 备注 |
|---|---|---|---|---|---|
| R1-04-RUN-01 | 2026-04-16 | Codex | PASS | `production/evidence/r1/replay-logs/20260416_150419_R1-04_CONSISTENCY.log` | 5/5 步骤 PASS |
| R1-04-RUN-02 |  |  |  |  |  |

## 4. 断言结果明细
| 步骤 | 断言 | 结果(PASS/FAIL) | 实际观测 | 缺陷ID |
|---|---|---|---|---|
| S01 | 初始 AP 包络为 11/2/2 | PASS | Turn=1, Phase=0, AP=11, PhaseAP=2, UniversalAP=2 |  |
| S02 | 阶段 AP 与通用 AP 消耗正确 | PASS | Spend=True, AP=8, PhaseAP=0, UniversalAP=1 |  |
| S03 | 资源与节点快照可追踪 | PASS | GoldLeaf=183, FireOil=189, NodeControl=58, NodeController=Aurean |  |
| S04 | 跨回合 AP 重置、资源/节点不漂移 | PASS | Turn=2, AP=11, PhaseAP=2, UniversalAP=2; 资源与节点快照一致 |  |
| S05 | 终局状态跨回合稳定且无重复派发 | PASS | GameEnded=True, EndReason=r1_04_manual_lock, EndEvents=0 |  |

## 5. 结论
- [x] PASS：R1-04 达标，可进入 R1-05
- [ ] CONDITIONAL PASS：存在可控问题，需带约束进入 R1-05
- [ ] FAIL：阻塞问题未清除，不可进入 R1-05

## 6. 签收
- QA Lead：____________  日期：____________
- Tech Lead：___________  日期：____________
- Producer：____________  日期：____________
