# R4-06 人工视觉审查工单（RUN01）

- 阶段：R4 UI 产品化
- 任务：R4-06
- RunID：R4-06-MANUAL-RUN01
- 工单版本：R4-06-MANUAL-WORKITEMS-RUN01.v1
- 状态：READY_FOR_REVIEW

## 1. 使用说明

1. 每条工单完成后将 `Status` 从 `TODO` 改为 `PASS/FAIL/BLOCKED`。
2. 若 `FAIL` 或 `BLOCKED`，必须填写 `Blocker/Issue` 并同步缺陷台账。
3. 全部 16 条工单完成后，再更新 `r4-06-ux-acceptance-signoff.md` 最终结论。

## 2. 工单清单（16条）

| TaskID | 类型 | 分辨率 | 界面/步骤 | 检查内容 | Owner | Due | Status | Evidence | Blocker/Issue |
|---|---|---|---|---|---|---|---|---|---|
| R4-06-M01 | Surface | 1280x720 | Map | 文本可读性 + 状态可解释性 + 操作反馈 + 行结论 | QA | TBD | TODO |  |  |
| R4-06-M02 | Surface | 1280x720 | Diplomacy | 文本可读性 + 状态可解释性 + 操作反馈 + 行结论 | QA | TBD | TODO |  |  |
| R4-06-M03 | Surface | 1280x720 | Battle Report | 文本可读性 + 状态可解释性 + 操作反馈 + 行结论 | QA | TBD | TODO |  |  |
| R4-06-M04 | Surface | 1280x720 | Event | 文本可读性 + 状态可解释性 + 操作反馈 + 行结论 | QA | TBD | TODO |  |  |
| R4-06-M05 | Surface | 1920x1080 | Map | 文本可读性 + 状态可解释性 + 操作反馈 + 行结论 | QA | TBD | TODO |  |  |
| R4-06-M06 | Surface | 1920x1080 | Diplomacy | 文本可读性 + 状态可解释性 + 操作反馈 + 行结论 | QA | TBD | TODO |  |  |
| R4-06-M07 | Surface | 1920x1080 | Battle Report | 文本可读性 + 状态可解释性 + 操作反馈 + 行结论 | QA | TBD | TODO |  |  |
| R4-06-M08 | Surface | 1920x1080 | Event | 文本可读性 + 状态可解释性 + 操作反馈 + 行结论 | QA | TBD | TODO |  |  |
| R4-06-M09 | Surface | 2560x1440 | Map | 文本可读性 + 状态可解释性 + 操作反馈 + 行结论 | QA | TBD | TODO |  |  |
| R4-06-M10 | Surface | 2560x1440 | Diplomacy | 文本可读性 + 状态可解释性 + 操作反馈 + 行结论 | QA | TBD | TODO |  |  |
| R4-06-M11 | Surface | 2560x1440 | Battle Report | 文本可读性 + 状态可解释性 + 操作反馈 + 行结论 | QA | TBD | TODO |  |  |
| R4-06-M12 | Surface | 2560x1440 | Event | 文本可读性 + 状态可解释性 + 操作反馈 + 行结论 | QA | TBD | TODO |  |  |
| R4-06-C01 | Chain | All | Map Node Switch | 地图节点切换后最新栏更新且热点标签正确 | QA | TBD | TODO |  |  |
| R4-06-C02 | Chain | All | Diplomacy Action | 可执行动作有反馈；锁定动作有 reason 与锁定文案 | QA | TBD | TODO |  |  |
| R4-06-C03 | Chain | All | Report Review | 战报可识别执行/结算/后果分组与回合总结 | QA | TBD | TODO |  |  |
| R4-06-C04 | Chain | All | Event Preview | 选项预览可见且后续语义明确 | QA | TBD | TODO |  |  |

## 3. 预填证据（自动预验收）

1. `production/evidence/r4/validation/20260430_141636_R4-06_UX_VISUAL_AUDIT_RUN01.csv`
2. `production/evidence/r4/validation/20260430_141636_R4-06_UX_VISUAL_AUDIT_RUN01.log`
3. `production/evidence/r4/validation/20260430_141636_R4-06_UX_VISUAL_AUDIT_RUN01.md`

## 4. 完成判定

1. 16 条工单全部为 `PASS` 或有可接受豁免。
2. 不存在 P0/P1 未闭环缺陷。
3. QA/设计/产品签字完成。
