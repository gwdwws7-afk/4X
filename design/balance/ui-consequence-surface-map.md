# UI Consequence Surface Map (Lock v1)

> 日期：2026-04-08  
> 作用：把 `consequence-ledger-template.csv` 的 `PlayerVisibleSurface` 映射到唯一 canonical UI surface，并标出运行时 `Bound/Gap`。

## 1. 判定口径

- `runtime_binding_status = Bound`：`primary_surface` 在当前代码里有明确运行时绑定（可定位到脚本字段/方法）。
- `runtime_binding_status = Gap`：`primary_surface` 仅概念存在或无代码绑定。
- `secondary_surface` 用于补充追踪链路，不影响 `Bound/Gap` 判定，但会暴露后续补齐优先级。

## 2. Canonical Surface Dictionary

| 历史命名/别名 | Canonical Surface | 当前状态 | 代码证据 |
|---|---|---|---|
| MilitaryPanel | UI.ActionPanel | Bound | `ActionPanelUI` 订阅 `OnActionLogAdded` / `OnConsequenceAdded`，提供行动结算流 |
| EconomyPanel | HUD.ResourceBar | Bound | `UIManager.InitializeResourceBar` + `ResourceItemUI.UpdateDisplay` |
| DiplomacyPanel | UI.DiplomacyPanel | Bound | `DiplomacyPanelUI` 订阅 `C1/C2` 相关日志、后果、通知与关系变更 |
| MapPanel | UI.MapPanel | Bound | `MapPanelUI` 订阅 `OnNodeControlChanged` / 地图相关后果 / 情报更新 |
| GlobalAlert | UI.GlobalAlertPanel | Bound | `GlobalAlertUI` 订阅 `OnGlobalAlertRaised` 并显示告警 |
| ActionLog | UI.ActionLogPanel | Bound | `ActionLogUI` 订阅 `OnActionLogAdded` 并维护历史日志 |
| ConsequencePanel | UI.ConsequencePanel | Bound | `ConsequencePanelUI` 订阅 `OnConsequenceAdded` 并跟踪持续回合 |
| NotificationPanel | UI.NotificationPanel | Bound | `NotificationPanelUI` 订阅 `OnNotificationAdded` |
| AlertPanel | UI.AlertPanel | Bound | `AlertPanelUI` 订阅 `OnAlertAdded` |
| EventPanel | UI.EventPanel | Bound | `EventPanelUI` 订阅 `OnNarrativeEventAdded` |
| IntelPanel | UI.IntelPanel | Bound | `IntelPanelUI` 订阅 `OnIntelReportAdded` 与地图控制变更 |

## 3. Runtime Binding Evidence

- HUD AP/阶段/回合：`Assets/Scripts/UI/UIManager.cs`
- HUD 资源条：`Assets/Scripts/UI/UIManager.cs` + `Assets/Scripts/UI/ResourceItemUI.cs`
- 核威慑面板：`Assets/Scripts/UI/NuclearDeterrenceUI.cs`
- 行动主面板：`Assets/Scripts/UI/ActionPanelUI.cs`
- 行动日志面板：`Assets/Scripts/UI/ActionLogUI.cs`
- 后果追踪面板：`Assets/Scripts/UI/ConsequencePanelUI.cs`
- 外交主面板：`Assets/Scripts/UI/DiplomacyPanelUI.cs`
- 地图主面板：`Assets/Scripts/UI/MapPanelUI.cs`
- 全局告警面板：`Assets/Scripts/UI/GlobalAlertUI.cs`
- 通知面板：`Assets/Scripts/UI/NotificationPanelUI.cs`
- 策略告警面板：`Assets/Scripts/UI/AlertPanelUI.cs`
- 叙事事件面板：`Assets/Scripts/UI/EventPanelUI.cs`
- 情报面板：`Assets/Scripts/UI/IntelPanelUI.cs`
- 胜负进度与失败预警：`Assets/Scripts/UI/VictoryProgressUI.cs`

## 4. Action Mapping Matrix

| action_id | primary_surface | secondary_surface | trigger_timing | minimum_copy_template | runtime_binding_status |
|---|---|---|---|---|---|
| D1.ProxyAction.Standard | UI.ActionPanel | UI.ActionLogPanel | On action resolve | [D1.ProxyAction.Standard] 立刻:AP-1;Arms-2;GoldLeaf-20 -> 后续:BlockadePressure+15;AshWill-5 on fail -> 触发:On action resolve -> 持续:2T | Bound |
| D1.ProxyAction.Heavy | UI.ActionPanel | UI.ActionLogPanel | On action resolve | [D1.ProxyAction.Heavy] 立刻:AP-2;Arms-4;GoldLeaf-30 -> 后续:BlockadePressure+20;AshWill-5 on fail -> 触发:On action resolve -> 持续:3T | Bound |
| D1.SpecialForces.Strike | UI.ActionPanel | UI.MapPanel | On action resolve | [D1.SpecialForces.Strike] 立刻:AP-2;Arms-3;GoldLeaf-30 -> 后续:SocialValue-3 on fail;BlockadePressure+20 -> 触发:On action resolve -> 持续:1T | Bound |
| D1.SpecialForces.Intel | UI.ActionPanel | UI.IntelPanel | If intel operation fails | [D1.SpecialForces.Intel] 立刻:AP-2;Arms-1;GoldLeaf-15 -> 后续:Low probability exposure risk -> 触发:If intel operation fails -> 持续:1T | Bound |
| D1.ChokepointThreat.Start | UI.ActionPanel | UI.MapPanel | On threat activation | [D1.ChokepointThreat.Start] 立刻:AP-2;Arms-1 -> 后续:RouteEfficiency-30%;BlockadePressure+40 -> 触发:On threat activation -> 持续:2T | Bound |
| D1.ChokepointThreat.Escalate | UI.ActionPanel | UI.MapPanel | On escalation confirm | [D1.ChokepointThreat.Escalate] 立刻:AP-1;Arms-1 -> 后续:Additional BlockadePressure+20 -> 触发:On escalation confirm -> 持续:2T | Bound |
| D1.ChokepointThreat.Withdraw | UI.ActionPanel | UI.MapPanel | On withdraw confirm | [D1.ChokepointThreat.Withdraw] 立刻:AP-0 -> 后续:BlockadePressure-15;RouteRisk decreases -> 触发:On withdraw confirm -> 持续:0T | Bound |
| D1.AsymmetricDefense.Fortify | UI.ActionPanel | UI.MapPanel | On defense resolve | [D1.AsymmetricDefense.Fortify] 立刻:AP-3;Arms-4 -> 后续:NodeStability+10;ControlPoints+30;SocialValue-5 on fail -> 触发:On defense resolve -> 持续:2T | Bound |
| D1.NuclearDeterrence.Display | UI.NuclearStatusPanel | UI.GlobalAlertPanel | On display resolve | [D1.NuclearDeterrence.Display] 立刻:AP-4;AshWill-10;TributeOrder-15 -> 后续:Enemy TotalWar disabled for 1 turn;BlockadePressure-20 -> 触发:On display resolve -> 持续:1T | Bound |
| D1.TotalWar | UI.ActionPanel | UI.ConsequencePanel | On declaration | [D1.TotalWar] 立刻:AP-5;Arms-8;GoldLeaf-60 -> 后续:SocialValue-20;AshWill-15;FullBlockadeRisk -> 触发:On declaration -> 持续:4T | Bound |
| C2.CurrencySettlement.Sign | UI.DiplomacyPanel | UI.ConsequencePanel | On agreement signed | [C2.CurrencySettlement.Sign] 立刻:AP-1;TributeOrder-10 -> 后续:Relationship+10~15;Trade channel unlock -> 触发:On agreement signed -> 持续:Persistent | Bound |
| C2.MilitaryCooperation.Sign | UI.DiplomacyPanel | UI.ConsequencePanel | On agreement signed | [C2.MilitaryCooperation.Sign] 立刻:AP-1;TributeOrder-15 -> 后续:Arms +1~3 per turn;Weapon price discount -> 触发:On agreement signed -> 持续:Persistent | Bound |
| C2.AxisCoordination.Sign | UI.DiplomacyPanel | UI.ConsequencePanel | On agreement signed | [C2.AxisCoordination.Sign] 立刻:AP-1;TributeOrder-8 -> 后续:Proxy coordination +20% -> 触发:On agreement signed -> 持续:Persistent | Bound |
| C2.EnergyTransit.Sign | UI.DiplomacyPanel | UI.MapPanel | On agreement signed | [C2.EnergyTransit.Sign] 立刻:AP-1;TributeOrder-10 -> 后续:Alternative route unlock;RouteEfficiency+15% -> 触发:On agreement signed -> 持续:5T | Bound |
| C2.NeutralityGuarantee.Sign | UI.DiplomacyPanel | UI.AlertPanel | On agreement signed | [C2.NeutralityGuarantee.Sign] 立刻:AP-2;TributeOrder-20 -> 后续:Target attack efficiency -20% when intervention triggers -> 触发:On agreement signed -> 持续:10T | Bound |
| C2.DefensePact.Sign | UI.DiplomacyPanel | UI.ConsequencePanel | On agreement signed | [C2.DefensePact.Sign] 立刻:AP-2;TributeOrder-25 -> 后续:Allied aid +2~3 Arms per turn on attack -> 触发:On agreement signed -> 持续:Persistent | Bound |
| C2.Protocol.Renew | UI.DiplomacyPanel | UI.NotificationPanel | 2 turns before expiration | [C2.Protocol.Renew] 立刻:AP-1;TributeOrder variable -> 后续:Failure to renew enters unstable state -> 触发:2 turns before expiration -> 持续:5T | Bound |
| C2.Protocol.Breach | UI.DiplomacyPanel | UI.EventPanel | On breach detection | [C2.Protocol.Breach] 立刻:AP-0 -> 后续:Relationship-20~-40;Re-sign cost x2;Narrative penalty -> 触发:On breach detection -> 持续:3T | Bound |
| B2.EnergyEmbargo.Upgrade | HUD.ResourceBar | UI.GlobalAlertPanel | When blockade review approves | [B2.EnergyEmbargo.Upgrade] 立刻:AP-0 -> 后续:ExportIncome coefficient down to embargo level -> 触发:When blockade review approves -> 持续:Persistent | Bound |
| B2.FinancialBlockade.Upgrade | HUD.ResourceBar | UI.GlobalAlertPanel | When blockade review approves | [B2.FinancialBlockade.Upgrade] 立刻:AP-0 -> 后续:StarCurrency acquisition drops to blockade level -> 触发:When blockade review approves -> 持续:Persistent | Bound |
| B2.SecondaryBlockade.Upgrade | HUD.ResourceBar | UI.DiplomacyPanel | When blockade review approves | [B2.SecondaryBlockade.Upgrade] 立刻:AP-0 -> 后续:Third-party routes lose stability -> 触发:When blockade review approves -> 持续:4T | Bound |
| B2.MilitaryEmbargo.Upgrade | UI.ActionPanel | UI.GlobalAlertPanel | When blockade review approves | [B2.MilitaryEmbargo.Upgrade] 立刻:AP-0 -> 后续:Advanced weapon channels restricted -> 触发:When blockade review approves -> 持续:Persistent | Bound |
| B2.NavalBlockade.Upgrade | UI.MapPanel | HUD.ResourceBar | When choke threat crosses threshold | [B2.NavalBlockade.Upgrade] 立刻:AP-0 -> 后续:Main sea route throughput to near zero -> 触发:When choke threat crosses threshold -> 持续:3T | Bound |
| B2.Countermeasure.LandRoute | HUD.ResourceBar | UI.MapPanel | When sea route disrupted | [B2.Countermeasure.LandRoute] 立刻:AP-1;TradeToken-5 -> 后续:Partial export recovery via land routes -> 触发:When sea route disrupted -> 持续:3T | Bound |
| B2.Countermeasure.GreyMarket | HUD.ResourceBar | UI.NotificationPanel | On each grey-market transaction | [B2.Countermeasure.GreyMarket] 立刻:AP-1;GoldLeaf surcharge +40% -> 后续:Exposure risk;future channel stability down -> 触发:On each grey-market transaction -> 持续:2T | Bound |
| B2.Countermeasure.CurrencyBypass | HUD.ResourceBar | UI.DiplomacyPanel | If bypass route accepted | [B2.Countermeasure.CurrencyBypass] 立刻:AP-1;TributeOrder-5 -> 后续:Temporary payment continuity;future sanctions pressure+ -> 触发:If bypass route accepted -> 持续:2T | Bound |
## 5. 使用约束（锁版）

- 新增行动时，必须先补 `consequence-ledger-template.csv` 与本映射文档，再进入实现。
- 若行动标记为 `Bound` 但 `secondary_surface` 仍为 `UI.Gap.*`，该行动只能算“部分可感知”，不得声明后果追踪闭环完成。
- 从本版起，评审中禁止再使用 `MilitaryPanel/EconomyPanel/...` 等历史别名，统一使用 canonical surface。



