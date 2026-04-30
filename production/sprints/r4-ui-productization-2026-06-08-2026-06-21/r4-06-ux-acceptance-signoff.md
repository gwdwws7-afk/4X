# R4-06 体验验收签收（四大高频界面）

- 阶段：R4 UI 产品化（2026-06-08 ~ 2026-06-21）
- 任务：R4-06 体验验收签收
- 版本：R4-06-UX-ACCEPTANCE-2026-04-30.v3
- 状态：IN_PROGRESS（自动预验收与主链路回放通过；剩余12项人工签收）

## 1. 前置条件（已满足）

1. R4-02 语义对齐通过：`r4-02-prefab-feedback-implementation.md`
2. R4-04 文案与本地化闭环通过：`r4-04-copy-readability-localization-closeout.md`
3. R4-05 自动化多分辨率回归通过：`r4-05-ui-regression-multi-resolution-input.md`

## 2. 本次验收目标

对以下四大高频界面完成“可读、可理解、可操作”的产品化体验签收：

1. 地图（Map）
2. 外交（Diplomacy）
3. 战报（Report）
4. 事件（Event）

## 2.1 自动预验收结果（已完成）

执行入口：

1. `EventideAge/Run R4 UX Visual Audit (R4-06)`
2. 批处理方法：`EventideAge.Editor.TestMenu.RunR4UxVisualAudit`

结果概览（2026-04-30）：

1. 三档分辨率（720p/1080p/1440p）全部 PASS。
2. 四大高频界面全部找到（`4/4`）。
3. 越界面板数 `0`，重叠对 `NONE`。

证据文件：

1. `production/evidence/r4/validation/20260430_141636_R4-06_UX_VISUAL_AUDIT_RUN01.csv`
2. `production/evidence/r4/validation/20260430_141636_R4-06_UX_VISUAL_AUDIT_RUN01.log`
3. `production/evidence/r4/validation/20260430_141636_R4-06_UX_VISUAL_AUDIT_RUN01.md`
4. 执行日志：`production/evidence/adhoc/20260430_R4_06_VISUAL_AUDIT_RUN.log`
5. 刷新证据（Run02）：`production/evidence/r4/validation/20260430_151648_R4-06_UX_VISUAL_AUDIT_RUN01.csv`
6. 刷新证据（Run02）：`production/evidence/r4/validation/20260430_151648_R4-06_UX_VISUAL_AUDIT_RUN01.log`
7. 刷新证据（Run02）：`production/evidence/r4/validation/20260430_151648_R4-06_UX_VISUAL_AUDIT_RUN01.md`
8. 刷新执行日志：`production/evidence/adhoc/20260430_R4_06_VISUAL_AUDIT_REFRESH.log`

人工走查记录（Run01）：

1. `production/sprints/r4-ui-productization-2026-06-08-2026-06-21/r4-06-manual-visual-walkthrough-run01.md`
2. `production/sprints/r4-ui-productization-2026-06-08-2026-06-21/r4-06-manual-visual-walkthrough-run01.json`
3. 待签字汇总：`production/sprints/r4-ui-productization-2026-06-08-2026-06-21/r4-06-signoff-ready-summary-run01.md`
4. 待签字汇总（机器读）：`production/sprints/r4-ui-productization-2026-06-08-2026-06-21/r4-06-signoff-ready-summary-run01.json`
5. 工单版（16条）：`production/sprints/r4-ui-productization-2026-06-08-2026-06-21/r4-06-manual-review-workitems-run01.md`
6. 工单版（机器读）：`production/sprints/r4-ui-productization-2026-06-08-2026-06-21/r4-06-manual-review-workitems-run01.json`
7. 工单版（16条，Run02 预填）：`production/sprints/r4-ui-productization-2026-06-08-2026-06-21/r4-06-manual-review-workitems-run02.md`
8. 工单版（机器读，Run02）：`production/sprints/r4-ui-productization-2026-06-08-2026-06-21/r4-06-manual-review-workitems-run02.json`
9. 待签字汇总（Run02）：`production/sprints/r4-ui-productization-2026-06-08-2026-06-21/r4-06-signoff-ready-summary-run02.md`
10. 待签字汇总（机器读，Run02）：`production/sprints/r4-ui-productization-2026-06-08-2026-06-21/r4-06-signoff-ready-summary-run02.json`

主链路回放复核（自动）：

1. 批处理方法：`EventideAge.Editor.TestMenu.RunR1ReplayScenarios`
2. 本次复核结果：RL-01 地图 -> 外交 -> 战报 -> 事件四步全部 PASS。
3. 证据：`production/evidence/r1/replay-logs/20260430_151631_R1-03_RL-01.log`
4. 执行日志：`production/evidence/adhoc/20260430_R4_06_R1_REPLAY_REFRESH.log`

## 3. 人工验收矩阵

| 维度 | 口径 | 720p | 1080p | 1440p |
|---|---|---|---|---|
| 文本可读性 | 标题/正文/状态标签无糊字、无压行 | TODO（人工） | TODO（人工） | TODO（人工） |
| 布局安全区 | 无裁切、无重叠、无越界遮挡 | PASS（自动预验收） | PASS（自动预验收） | PASS（自动预验收） |
| 状态可解释性 | `status/reason/hint` 可被玩家理解 | TODO | TODO | TODO |
| 操作反馈 | 点击后有明确反馈，不出现“无响应” | TODO | TODO | TODO |
| 跨面板链路 | 地图 -> 外交 -> 战报 -> 事件 路径顺畅 | PASS（自动回放） | PASS（自动回放） | PASS（自动回放） |

## 4. 输入路径验收清单（鼠标键盘）

1. 地图节点点击：
   - 切换节点后，地图最新栏更新，热点标签展示正确。
2. 外交动作触发：
   - 可执行动作有反馈；锁定动作展示 `reason` 与锁定文案。
3. 战报阅读与定位：
   - 可从战报快速识别 `执行/结算/后果` 分组与回合总结。
4. 事件选项预览：
   - 故事事件显示“选项预览”并提供后续决策语义。

## 5. 缺陷分级口径

1. P0：阻断主流程（无法继续游戏、界面不可操作）
2. P1：高频体验严重受损（关键信息不可读、误导操作）
3. P2：存在明显瑕疵但不阻断流程（对齐/间距/文案细节）

## 6. 签收判定

通过条件：

1. 3 档分辨率在四大界面均无 P0/P1。
2. 四大界面主链路可完整走通且无“空反馈”步骤。
3. 遗留问题均记录并定级，且有责任人和修复计划。

## 7. 签收记录

| 角色 | 姓名 | 日期 | 结论 | 备注 |
|---|---|---|---|---|
| 产品 |  |  | TODO |  |
| 设计 |  |  | TODO |  |
| QA |  |  | TODO |  |
| 技术 |  | 2026-04-30 | PRECHECK PASS | 自动视觉审计与主链路回放通过（见 RUN02 证据） |

最终结论：`GO / CONDITIONAL GO / NO-GO`
