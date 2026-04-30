# R0-02 API Freeze Checklist (A1-L4)

## Freeze Rules
- Freeze target: A1-K all public runtime API surfaces.
- L1-L4 currently have no runtime code; freeze status is "empty surface".
- Any public signature change after R0 requires: RFC + compatibility note + full regression rerun.

## Runtime Public API Freeze List
| System | Entry Class | Entry File | Freeze Status |
|---|---|---|---|
| A1 | `TurnLoopSystem` | `Assets/Scripts/Systems/A1/TurnLoopSystem.cs` | Frozen@R0 |
| A2 | `PhaseEngine` | `Assets/Scripts/Systems/A2/PhaseEngine.cs` | Frozen@R0 |
| A3 | `ResourceSystem` | `Assets/Scripts/Systems/A3/ResourceSystem.cs` | Frozen@R0 |
| A4 | `SaveSystem` | `Assets/Scripts/Systems/A4/SaveSystem.cs` | Frozen@R0 |
| A5 | `GameClock` | `Assets/Scripts/Systems/A5/GameClock.cs` | Frozen@R0 |
| B1 | `FinanceSystem` | `Assets/Scripts/Systems/B1/FinanceSystem.cs` | Frozen@R0 |
| B2 | `BlockadeSystem` | `Assets/Scripts/Systems/B2/BlockadeSystem.cs` | Frozen@R0 |
| B3 | `TradeNetworkSystem` | `Assets/Scripts/Systems/B3/TradeNetworkSystem.cs` | Frozen@R0 |
| B4 | `ExchangeRateSystem` | `Assets/Scripts/Systems/B4/ExchangeRateSystem.cs` | Frozen@R0 |
| B5 | `EconomicSettlementSystem` | `Assets/Scripts/Systems/B5/EconomicSettlementSystem.cs` | Frozen@R0 |
| C1 | `DiplomaticRelationsSystem` | `Assets/Scripts/Systems/C1/DiplomaticRelationsSystem.cs` | Frozen@R0 |
| C2 | `DiplomaticProtocolsSystem` | `Assets/Scripts/Systems/C2/DiplomaticProtocolsSystem.cs` | Frozen@R0 |
| C3 | `IdeologySystem` | `Assets/Scripts/Systems/C3/IdeologySystem.cs` | Frozen@R0 |
| C4 | `AllianceSystem` | `Assets/Scripts/Systems/C4/AllianceSystem.cs` | Frozen@R0 |
| C5 | `InternationalOrgsSystem` | `Assets/Scripts/Systems/C5/InternationalOrgsSystem.cs` | Frozen@R0 |
| D1 | `MilitaryOperationsSystem` | `Assets/Scripts/Systems/D1/MilitaryOperationsSystem.cs` | Frozen@R0 |
| D2 | `MilitaryPoliticalLinkageSystem` | `Assets/Scripts/Systems/D2/MilitaryPoliticalLinkageSystem.cs` | Frozen@R0 |
| D3 | `ProxyCivilAffairsSystem` | `Assets/Scripts/Systems/D3/ProxyCivilAffairsSystem.cs` | Frozen@R0 |
| D4 | `NuclearDeterrenceSystem` | `Assets/Scripts/Systems/D4/NuclearDeterrenceSystem.cs` | Frozen@R0 |
| D5 | `WarResolutionSystem` | `Assets/Scripts/Systems/D5/WarResolutionSystem.cs` | Frozen@R0 |
| D6 | `MilitaryTechSystem` | `Assets/Scripts/Systems/D6/MilitaryTechSystem.cs` | Frozen@R0 |
| E | `InternalPoliticsSystem` | `Assets/Scripts/Systems/E/InternalPoliticsSystem.cs` | Frozen@R0 |
| F1 | `IntelligenceSystem` | `Assets/Scripts/Systems/F1/IntelligenceSystem.cs` | Frozen@R0 |
| G | `FactionAISystem` | `Assets/Scripts/Systems/G/FactionAISystem.cs` | Frozen@R0 |
| H1 | `StrategicMapSystem` | `Assets/Scripts/Systems/H1/StrategicMapSystem.cs` | Frozen@R0 |
| H2 | `NodeNetworkSystem` | `Assets/Scripts/Systems/H2/NodeNetworkSystem.cs` | Frozen@R0 |
| H3 | `TerrainVisionSystem` | `Assets/Scripts/Systems/H3/TerrainVisionSystem.cs` | Frozen@R0 |
| I1 | `EventSystem` | `Assets/Scripts/Systems/I1/EventSystem.cs` | Frozen@R0 |
| J | `VictoryDefeatSystem` | `Assets/Scripts/Systems/J/VictoryDefeatSystem.cs` | Frozen@R0 |
| K | `UIManager` + K panel systems | `Assets/Scripts/UI/*.cs` (GameSystem-derived) | Frozen@R0 |
| L1 | N/A | _No runtime code_ | Frozen as empty surface |
| L2 | N/A | _No runtime code_ | Frozen as empty surface |
| L3 | N/A | _No runtime code_ | Frozen as empty surface |
| L4 | N/A | _No runtime code_ | Frozen as empty surface |

## K Scope (Current Runtime Classes)
- `Assets/Scripts/UI/UIManager.cs`
- `Assets/Scripts/UI/ActionLogUI.cs`
- `Assets/Scripts/UI/ActionPanelUI.cs`
- `Assets/Scripts/UI/AlertPanelUI.cs`
- `Assets/Scripts/UI/ConsequencePanelUI.cs`
- `Assets/Scripts/UI/DiplomacyPanelUI.cs`
- `Assets/Scripts/UI/EventPanelUI.cs`
- `Assets/Scripts/UI/GlobalAlertUI.cs`
- `Assets/Scripts/UI/IntelPanelUI.cs`
- `Assets/Scripts/UI/MapPanelUI.cs`
- `Assets/Scripts/UI/MilitaryTechUI.cs`
- `Assets/Scripts/UI/NotificationPanelUI.cs`
- `Assets/Scripts/UI/NuclearDeterrenceUI.cs`
- `Assets/Scripts/UI/ProxyAffairsUI.cs`
- `Assets/Scripts/UI/VictoryProgressUI.cs`

