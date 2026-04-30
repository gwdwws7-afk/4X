# R4-06 人工视觉审查工单（RUN02）

- 阶段：R4 UI 产品化
- 任务：R4-06
- RunID：R4-06-MANUAL-RUN02
- 工单版本：R4-06-MANUAL-WORKITEMS-RUN02.v1
- 状态：AUTO_PREFILLED_PENDING_HUMAN_SIGNOFF

## 1. 使用说明

1. 本文件已按最新自动证据预填：主链路 4 项已自动闭环，界面 12 项仍需人工可读性签字。
2. `AUTO_LAYOUT_PASS_PENDING_MANUAL` 只代表布局安全与面板存在通过，不代表人工体验签收已通过。
3. 人工完成后将对应项状态改为 `PASS/FAIL/BLOCKED`，并补全 `Evidence` 与 `Blocker/Issue`。

## 2. 工单清单（16条）

| TaskID | 类型 | 分辨率 | 界面/步骤 | 检查内容 | Owner | Due | Status | Evidence | Blocker/Issue |
|---|---|---|---|---|---|---|---|---|---|
| R4-06-M01 | Surface | 1280x720 | Map | 文本可读性 + 状态可解释性 + 操作反馈 + 行结论 | QA | 2026-05-01 | AUTO_LAYOUT_PASS_PENDING_MANUAL | `production/evidence/r4/validation/20260430_151648_R4-06_UX_VISUAL_AUDIT_RUN01.csv` | 待人工签收可读性与操作反馈 |
| R4-06-M02 | Surface | 1280x720 | Diplomacy | 文本可读性 + 状态可解释性 + 操作反馈 + 行结论 | QA | 2026-05-01 | AUTO_LAYOUT_PASS_PENDING_MANUAL | `production/evidence/r4/validation/20260430_151648_R4-06_UX_VISUAL_AUDIT_RUN01.csv` | 待人工签收可读性与操作反馈 |
| R4-06-M03 | Surface | 1280x720 | Battle Report | 文本可读性 + 状态可解释性 + 操作反馈 + 行结论 | QA | 2026-05-01 | AUTO_LAYOUT_PASS_PENDING_MANUAL | `production/evidence/r4/validation/20260430_151648_R4-06_UX_VISUAL_AUDIT_RUN01.csv` | 待人工签收可读性与操作反馈 |
| R4-06-M04 | Surface | 1280x720 | Event | 文本可读性 + 状态可解释性 + 操作反馈 + 行结论 | QA | 2026-05-01 | AUTO_LAYOUT_PASS_PENDING_MANUAL | `production/evidence/r4/validation/20260430_151648_R4-06_UX_VISUAL_AUDIT_RUN01.csv` | 待人工签收可读性与操作反馈 |
| R4-06-M05 | Surface | 1920x1080 | Map | 文本可读性 + 状态可解释性 + 操作反馈 + 行结论 | QA | 2026-05-01 | AUTO_LAYOUT_PASS_PENDING_MANUAL | `production/evidence/r4/validation/20260430_151648_R4-06_UX_VISUAL_AUDIT_RUN01.csv` | 待人工签收可读性与操作反馈 |
| R4-06-M06 | Surface | 1920x1080 | Diplomacy | 文本可读性 + 状态可解释性 + 操作反馈 + 行结论 | QA | 2026-05-01 | AUTO_LAYOUT_PASS_PENDING_MANUAL | `production/evidence/r4/validation/20260430_151648_R4-06_UX_VISUAL_AUDIT_RUN01.csv` | 待人工签收可读性与操作反馈 |
| R4-06-M07 | Surface | 1920x1080 | Battle Report | 文本可读性 + 状态可解释性 + 操作反馈 + 行结论 | QA | 2026-05-01 | AUTO_LAYOUT_PASS_PENDING_MANUAL | `production/evidence/r4/validation/20260430_151648_R4-06_UX_VISUAL_AUDIT_RUN01.csv` | 待人工签收可读性与操作反馈 |
| R4-06-M08 | Surface | 1920x1080 | Event | 文本可读性 + 状态可解释性 + 操作反馈 + 行结论 | QA | 2026-05-01 | AUTO_LAYOUT_PASS_PENDING_MANUAL | `production/evidence/r4/validation/20260430_151648_R4-06_UX_VISUAL_AUDIT_RUN01.csv` | 待人工签收可读性与操作反馈 |
| R4-06-M09 | Surface | 2560x1440 | Map | 文本可读性 + 状态可解释性 + 操作反馈 + 行结论 | QA | 2026-05-01 | AUTO_LAYOUT_PASS_PENDING_MANUAL | `production/evidence/r4/validation/20260430_151648_R4-06_UX_VISUAL_AUDIT_RUN01.csv` | 待人工签收可读性与操作反馈 |
| R4-06-M10 | Surface | 2560x1440 | Diplomacy | 文本可读性 + 状态可解释性 + 操作反馈 + 行结论 | QA | 2026-05-01 | AUTO_LAYOUT_PASS_PENDING_MANUAL | `production/evidence/r4/validation/20260430_151648_R4-06_UX_VISUAL_AUDIT_RUN01.csv` | 待人工签收可读性与操作反馈 |
| R4-06-M11 | Surface | 2560x1440 | Battle Report | 文本可读性 + 状态可解释性 + 操作反馈 + 行结论 | QA | 2026-05-01 | AUTO_LAYOUT_PASS_PENDING_MANUAL | `production/evidence/r4/validation/20260430_151648_R4-06_UX_VISUAL_AUDIT_RUN01.csv` | 待人工签收可读性与操作反馈 |
| R4-06-M12 | Surface | 2560x1440 | Event | 文本可读性 + 状态可解释性 + 操作反馈 + 行结论 | QA | 2026-05-01 | AUTO_LAYOUT_PASS_PENDING_MANUAL | `production/evidence/r4/validation/20260430_151648_R4-06_UX_VISUAL_AUDIT_RUN01.csv` | 待人工签收可读性与操作反馈 |
| R4-06-C01 | Chain | All | Map Node Switch | 地图节点切换后最新栏更新且热点标签正确 | QA | 2026-05-01 | PASS | `production/evidence/r1/replay-logs/20260430_151631_R1-03_RL-01.log` (S01) |  |
| R4-06-C02 | Chain | All | Diplomacy Action | 可执行动作有反馈；锁定动作有 reason 与锁定文案 | QA | 2026-05-01 | PASS | `production/evidence/r1/replay-logs/20260430_151631_R1-03_RL-01.log` (S02) |  |
| R4-06-C03 | Chain | All | Report Review | 战报可识别执行/结算/后果分组与回合总结 | QA | 2026-05-01 | PASS | `production/evidence/r1/replay-logs/20260430_151631_R1-03_RL-01.log` (S03) |  |
| R4-06-C04 | Chain | All | Event Preview | 选项预览可见且后续语义明确 | QA | 2026-05-01 | PASS | `production/evidence/r1/replay-logs/20260430_151631_R1-03_RL-01.log` (S04) |  |

## 3. 自动证据（RUN02）

1. `production/evidence/adhoc/20260430_R4_06_R1_REPLAY_REFRESH.log`
2. `production/evidence/r1/replay-logs/20260430_151631_R1-03_RL-01.log`
3. `production/evidence/r4/validation/20260430_151648_R4-06_UX_VISUAL_AUDIT_RUN01.csv`
4. `production/evidence/r4/validation/20260430_151648_R4-06_UX_VISUAL_AUDIT_RUN01.log`
5. `production/evidence/r4/validation/20260430_151648_R4-06_UX_VISUAL_AUDIT_RUN01.md`

## 4. 当前完成度（RUN02）

1. 已自动闭环：`4/16`（主链路 C01-C04）。
2. 待人工签收：`12/16`（三档分辨率 × 四高频界面可读性/可解释性/反馈）。
3. 当前门禁结论：`PARTIAL`（待 QA/设计/产品签字）。
