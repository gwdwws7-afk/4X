# R3-04 KPI Evaluation (R228)

- Batch: `R3-04AG-B003-R228`
- Profile: `r212-stability-balance-c`
- Runs: `1000`
- Source CSV: `C:\test\4X\production\evidence\r3\tuning\20260423_R3-04AG_TARGETED-R228.csv`
- KPI Pass: `11/13`

## Round Parameter Delta

- Bias (Easy/Standard/Hard): `0.27/0.16/0.05`
- Init Scale (Easy/Standard/Hard): `1.15/1.11/1.09`
- Social/Trade Init Scale: `0.15`
- Victory Threshold (Easy/Standard/Hard): `85/83/81`
- Pressure Offset/TurnScale: `0.24/0.247`; Conflict Base/TurnScale: `0.397/0.015`
- Delta Scale/Noise Scale: `0.96/2.02`
- Early Suppression: `turn<= 14` with `positive*0.33`
- Shock: `chance=0.32`, `magnitude=+/-26`
- Late Escalation: `startTurn=14`, `pressure+=0.094`, `conflict+=0.066`, `loss+=0.112`, `capture-=0.036`

## KPI Snapshot

| KPI | Actual | Target | Status |
|---|---:|---:|---|
| WIN_RATE_EASY | 0.7433 | [0.6500, 0.8000] | PASS |
| WIN_RATE_STANDARD | 0.4650 | [0.4500, 0.5500] | PASS |
| WIN_RATE_HARD | 0.2200 | [0.2000, 0.3500] | PASS |
| WIN_RATE_OVERALL | 0.4750 | [0.4000, 0.6000] | PASS |
| AVG_END_TURN_OVERALL | 13.6930 | [14.0000, 20.0000] | FAIL |
| AVG_END_TURN_STANDARD | 15.5125 | [15.0000, 19.0000] | PASS |
| ATTRITION_RATE_OVERALL | 0.1820 | [0.1000, 0.3500] | PASS |
| RESOURCE_CV_GOLDLEAF | 0.3908 | [0.1800, 0.4500] | PASS |
| RESOURCE_CV_FIREOIL | 0.1619 | [0.1500, 0.4000] | PASS |
| RESOURCE_CV_ARMS | 0.2260 | [0.2000, 0.5500] | PASS |
| VICTORY_SHARE_OVERALL | 0.4750 | [0.3500, 0.6500] | PASS |
| DEFEAT_SHARE_OVERALL | 0.3430 | [0.3500, 0.6500] | FAIL |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.6211 | [0.0000, 0.7000] | PASS |

## Endgame Distribution

| End Type | Count | Share |
|---|---:|---:|
| victory | 475 | 0.4750 |
| defeat | 343 | 0.3430 |
| timeout | 182 | 0.1820 |

- Top victory path: `energyliberation` (`295` wins, share `0.6211`)
