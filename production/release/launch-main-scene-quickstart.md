# MainGameScene 上线场景快速说明

## 进入方式
1. 打开 `Assets/Scenes/MainGameScene.unity`。
2. 点击 Play。
3. 在右下角找到 `上线流程控制台`。

## 主流程按钮
1. `推进整回合`：执行完整阶段循环（外交 -> 战略 -> 作战 -> 后勤 -> 情报 -> AI）。
2. `推进至24回合`：持续推进直到终局或达到回合上限。
3. `下一阶段`：手动推进一个阶段。

## 分阶段动作
1. `外交`：发起并签署贸易协议。
2. `战略`：执行意识形态宣传。
3. `作战`：对当前目标节点执行军事行动。
4. `后勤`：执行经济结算并输出健康度。
5. `情报`：执行目标侦察并写入情报面板。

## 辅助按钮
1. `切换节点`：切换作战/情报目标节点。
2. `保存`：写入 `release_quickslot`。
3. `读取`：从 `release_quickslot` 恢复。
4. `显示引导/隐藏引导`：展开或折叠操作提示。

## 验证门
1. 菜单执行：`EventideAge/Release/Build Launch Main Scene`。
2. 菜单执行：`EventideAge/Run All Tests`。
3. 日志确认：`=== Results: 446 passed, 0 failed ===`。
