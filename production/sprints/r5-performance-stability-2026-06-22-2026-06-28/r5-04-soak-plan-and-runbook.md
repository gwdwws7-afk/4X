# R5-04 Soak Test 计划与执行手册

- 阶段：R5 性能与稳定性（2026-06-22 ~ 2026-06-28）
- 目标：验证长时间运行稳定性、内存漂移、存档连续性
- 脚本入口：
  - `EventideAge/Run R5 Soak Baseline (R5-04)`
  - `R5StabilityRunner.RunSoakBaseline()`

## 1. 场景定义

| 场景ID | 说明 | 样本数 | 存档读写频率 | 输出目录 |
|---|---|---:|---:|---|
| R5-04-SOAK-8H-ACCEL | 8 小时映射的加速 Soak | 2400 | 每 50 样本 | `production/evidence/r5/soak/` |

## 2. 执行步骤

1. 打开 Unity 工程 `C:\test\4X`。
2. 确认 Console 无新编译错误。
3. 执行菜单：`EventideAge -> Run R5 Soak Baseline (R5-04)`。
4. 执行完成后归档两份证据：
   - `*_R5-04-SOAK-8H-ACCEL.csv`
   - `*_R5-04-SOAK-8H-ACCEL.md`
5. 将结果回填到本文件第 4 节。

## 3. 验收门禁

| 指标 | 门槛 |
|---|---|
| 阻断崩溃 | 0 |
| 存档读写失败 | 0 |
| 托管内存增长 | <= 256 MB |
| 回合推进 P95 | <= 33.30 ms |

## 4. 执行记录

| RunID | 日期 | 结果 | CSV | MD 报告 | 备注 |
|---|---|---|---|---|---|
| R5-04-RUN-01 | 2026-04-29 | PASS | `production/evidence/r5/soak/20260429_155347_R5-04-SOAK-8H-ACCEL.csv` | `production/evidence/r5/soak/20260429_155347_R5-04-SOAK-8H-ACCEL.md` | Unity batchmode executed via `TestMenu.RunR5SoakBaseline` |
| R5-04-RUN-02 |  |  |  |  |  |

## 5. 问题与回归

| 问题ID | 现象 | 影响级别 | 修复措施 | 复测结果 |
|---|---|---|---|---|
| R5-SOAK-OBS-01 | GC2 增量为 7（非 0） | 低 | 在 R5-03 增加真实场景内存剖析与分配热点跟踪 | 待复测 |
