# Sprint 1 -- 2026-04-07 to 2026-04-18

## Sprint Goal

建立 Unity 项目核心架构，实现回合主循环（A1）和阶段引擎（A2），完成可运行的最小可行游戏循环。

## Capacity

- 总天数: 10 天（2周）
- Buffer (20%): 2 天
- 可用: 8 天

## Tasks

### Must Have (Critical Path)

| ID | Task | Agent/Owner | Est. Days | Dependencies | Acceptance Criteria |
|----|------|-------------|-----------|-------------|-------------------|
| S1-01 | 创建 Unity 项目基础结构 | Lead Dev | 1 | None | 目录结构符合 ADR-001，GameState/GameEvents/GameSystem 基类可编译 |
| S1-02 | 实现 A1 回合主循环 | Gameplay Programmer | 2 | S1-01 | 6阶段结构，11行动点，AI仅Phase6响应，回合正确推进 |
| S1-03 | 实现 A2 阶段引擎 | Gameplay Programmer | 1 | S1-02 | 阶段切换触发事件，行动点正确分配/消耗 |
| S1-04 | 实现 A3 资源系统基础 | Gameplay Programmer | 1 | S1-01 | 7种资源（Arms/StarCoins/FireOil等），3种类型区分，界面显示 |
| S1-05 | 实现 H1 战略地图基础 | Technical Artist | 2 | S1-01 | 节点地图渲染，6区域显示，鼠标悬停显示信息 |
| S1-06 | 集成测试：回合循环可运行 | QA | 1 | S1-02, S1-03 | 可开始新回合，各阶段切换，行动点消耗/重置正确 |

### Should Have

| ID | Task | Agent/Owner | Est. Days | Dependencies | Acceptance Criteria |
|----|------|-------------|-----------|-------------|-------------------|
| S1-07 | 实现 A4 存档系统基础 | Lead Dev | 1 | S1-01 | 支持完整存档保存/加载，GameState序列化正常 |
| S1-08 | 实现 A5 游戏时钟 | Gameplay Programmer | 0.5 | S1-02 | 时间格式显示（2028 H1/H2），24回合上限提示 |
| S1-09 | 创建 UI 基础框架 (K) | UI Programmer | 1 | S1-01 | 主面板布局，资源条，显示行动点剩余 |

### Nice to Have

| ID | Task | Agent/Owner | Est. Days | Dependencies | Acceptance Criteria |
|----|------|-------------|-----------|-------------|-------------------|
| S1-10 | 入门教程 L2 部分 | Narrative Designer | 1 | S1-05, S1-09 | 第一个教程提示正确触发 |
| S1-11 | 基础音效骨架 | Audio Designer | 0.5 | S1-02 | UI交互音效骨骼 |

## Carryover from Previous Sprint

无（首个Sprint）

## Risks

| Risk | Probability | Impact | Mitigation |
|------|------------|--------|------------|
| Unity 项目结构与 ADR-001 假设不符 | 中 | 高 | Sprint开始第1天完成架构Code Review，必要时调整ADR |
| 战略地图 H1 实现复杂度超预期 | 中 | 中 | H1 仅实现基础节点显示，详细地形后续Sprint |
| 多系统集成时事件通信出问题 | 低 | 高 | S1-06集成测试充分，覆盖正常流程和异常流程 |

## Dependencies on External Factors

- Unity 2022.3 LTS 许可/安装（当前工程版本：2022.3.62f1）
- 所有 GDD 文档已完成（30/30）

## Definition of Done for this Sprint

- [ ] 所有 Must Have 任务完成
- [ ] 代码可编译，无编译错误
- [ ] TurnLoop 通过集成测试（6阶段切换、11AP消耗/重置）
- [ ] 战略地图显示 6 区域和 12 节点
- [ ] 资源系统显示 7 种资源
- [ ] UI 显示回合数/阶段/行动点
- [ ] 存档可保存/加载当前状态
- [ ] 代码符合 CLAUDE.md 中的编码标准
