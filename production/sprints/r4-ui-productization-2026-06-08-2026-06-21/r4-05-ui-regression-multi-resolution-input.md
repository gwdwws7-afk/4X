# R4-05 UI 回归测试（多分辨率 / 输入路径）

- 阶段：R4 UI 产品化（2026-06-08 ~ 2026-06-21）
- 任务：R4-05 UI 回归测试（多分辨率、输入路径）
- 执行日期：2026-04-30
- 状态：PASS（自动化回归维度）

## 1. 验收范围

1. 分辨率：1280x720 / 1920x1080 / 2560x1440
2. 输入路径：鼠标键盘主路径（以事件驱动 UI 入口代理验证）
3. 高频界面：地图 / 外交 / 战报 / 事件
4. 回归入口：
   - `EventideAge.Editor.TestMenu.RunR4UiProductizationChecks`（R4-02）
   - `EventideAge.Editor.TestMenu.RunR4UiInteractionHintChecks`（R4-03）

## 2. 结果摘要

| CaseID | 分辨率 | R4-02 | R4-03 | 结果 |
|---|---|---:|---:|---|
| R4-05-WIN-720P | 1280 x 720 | PASS（77 / 0） | PASS（13 / 0） | PASS |
| R4-05-WIN-1080P | 1920 x 1080 | PASS（77 / 0） | PASS（13 / 0） | PASS |
| R4-05-WIN-1440P | 2560 x 1440 | PASS（77 / 0） | PASS（13 / 0） | PASS |

说明：括号内为 `PASS_LINES / FAIL_LINES`。

## 3. 证据文件

1. 汇总 CSV：`production/evidence/r4/validation/20260430_092522_R4-05_UI_REGRESSION_RUN02.csv`
2. 汇总 MD：`production/evidence/r4/validation/20260430_092522_R4-05_UI_REGRESSION_RUN02.md`
3. 分项日志：
   - `production/evidence/r4/validation/20260430_092522_R4-05-WIN-720P_R4-02.log`
   - `production/evidence/r4/validation/20260430_092522_R4-05-WIN-720P_R4-03.log`
   - `production/evidence/r4/validation/20260430_092522_R4-05-WIN-1080P_R4-02.log`
   - `production/evidence/r4/validation/20260430_092522_R4-05-WIN-1080P_R4-03.log`
   - `production/evidence/r4/validation/20260430_092522_R4-05-WIN-1440P_R4-02.log`
   - `production/evidence/r4/validation/20260430_092522_R4-05-WIN-1440P_R4-03.log`

## 4. 结论

1. 在自动化回归口径下，三档分辨率与主输入路径代理检查全部通过。
2. 高频界面语义、锁定提示、状态标签、跳转提示无回归。
3. R4-05 自动化验收通过，可进入视觉层终验（像素可读性/遮挡/字体缩放人工检查）。

## 5. 限制与后续

1. 本轮为 batchmode 自动化验证，未覆盖真实交互中的像素级布局问题。
2. 建议下一步执行 R4-06 体验验收前，补一轮可视化截图/人工走查记录。
