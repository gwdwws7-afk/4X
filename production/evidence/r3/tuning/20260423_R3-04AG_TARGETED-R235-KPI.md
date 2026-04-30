# R3-04 KPI Evaluation (R235)

- Batch: `R3-04AG-B004-R235`
- Profile: `r99-gate-close-d`
- Runs: `1000`
- Source CSV: `C:\test\4X\production\evidence\r3\tuning\20260423_R3-04AG_TARGETED-R235.csv`
- KPI Pass: `10/13`

## Round Parameter Delta

- Bias (Easy/Standard/Hard): `0.26/0.15/0.05`
- Init Scale (Easy/Standard/Hard): `1.13/1.07/1.03`
- Social/Trade Init Scale: `0.16`
- Victory Threshold (Easy/Standard/Hard): `86/84/82`
- Pressure Offset/TurnScale: `0.24/0.216`; Conflict Base/TurnScale: `0.402/0.012`
- Delta Scale/Noise Scale: `0.96/2.02`
- Early Suppression: `turn<= 10` with `positive*0.39`
- Shock: `chance=0.38`, `magnitude=+/-30`
- Late Escalation: `startTurn=0`, `pressure+=0.000`, `conflict+=0.000`, `loss+=0.000`, `capture-=0.000`

## KPI Snapshot

| KPI | Actual | Target | Status |
|---|---:|---:|---|
| WIN_RATE_EASY | 0.6567 | [0.6500, 0.8000] | PASS |
| WIN_RATE_STANDARD | 0.4000 | [0.4500, 0.5500] | FAIL |
| WIN_RATE_HARD | 0.1500 | [0.2000, 0.3500] | FAIL |
| WIN_RATE_OVERALL | 0.4020 | [0.4000, 0.6000] | PASS |
| AVG_END_TURN_OVERALL | 14.6340 | [14.0000, 20.0000] | PASS |
| AVG_END_TURN_STANDARD | 16.3800 | [15.0000, 19.0000] | PASS |
| ATTRITION_RATE_OVERALL | 0.2140 | [0.1000, 0.3500] | PASS |
| RESOURCE_CV_GOLDLEAF | 0.4006 | [0.1800, 0.4500] | PASS |
| RESOURCE_CV_FIREOIL | 0.1931 | [0.1500, 0.4000] | PASS |
| RESOURCE_CV_ARMS | 0.2591 | [0.2000, 0.5500] | PASS |
| VICTORY_SHARE_OVERALL | 0.4020 | [0.3500, 0.6500] | PASS |
| DEFEAT_SHARE_OVERALL | 0.3840 | [0.3500, 0.6500] | PASS |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.7960 | [0.0000, 0.7000] | FAIL |

## Endgame Distribution

| End Type | Count | Share |
|---|---:|---:|
| victory | 402 | 0.4020 |
| defeat | 384 | 0.3840 |
| timeout | 214 | 0.2140 |

- Top victory path: `energyliberation` (`320` wins, share `0.7960`)
