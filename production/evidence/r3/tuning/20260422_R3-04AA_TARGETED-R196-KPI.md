# R3-04 KPI Evaluation (R196)

- Batch: `R3-04AA-B001-R196`
- Profile: `r194-pace-floor-a`
- Runs: `1000`
- Source CSV: `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04AA_TARGETED-R196.csv`
- KPI Pass: `11/13`

## Round Parameter Delta

- Bias (Easy/Standard/Hard): `0.27/0.16/0.05`
- Init Scale (Easy/Standard/Hard): `1.15/1.11/1.08`
- Social/Trade Init Scale: `0.15`
- Victory Threshold (Easy/Standard/Hard): `85/83/81`
- Pressure Offset/TurnScale: `0.24/0.247`; Conflict Base/TurnScale: `0.397/0.015`
- Delta Scale/Noise Scale: `0.96/2.02`
- Early Suppression: `turn<= 14` with `positive*0.33`
- Shock: `chance=0.38`, `magnitude=+/-30`
- Late Escalation: `startTurn=15`, `pressure+=0.093`, `conflict+=0.066`, `loss+=0.106`, `capture-=0.036`

## KPI Snapshot

| KPI | Actual | Target | Status |
|---|---:|---:|---|
| WIN_RATE_EASY | 0.7267 | [0.6500, 0.8000] | PASS |
| WIN_RATE_STANDARD | 0.4475 | [0.4500, 0.5500] | FAIL |
| WIN_RATE_HARD | 0.2100 | [0.2000, 0.3500] | PASS |
| WIN_RATE_OVERALL | 0.4600 | [0.4000, 0.6000] | PASS |
| AVG_END_TURN_OVERALL | 13.7490 | [14.0000, 20.0000] | FAIL |
| AVG_END_TURN_STANDARD | 15.3500 | [15.0000, 19.0000] | PASS |
| ATTRITION_RATE_OVERALL | 0.1730 | [0.1000, 0.3500] | PASS |
| RESOURCE_CV_GOLDLEAF | 0.4202 | [0.1800, 0.4500] | PASS |
| RESOURCE_CV_FIREOIL | 0.1930 | [0.1500, 0.4000] | PASS |
| RESOURCE_CV_ARMS | 0.2697 | [0.2000, 0.5500] | PASS |
| VICTORY_SHARE_OVERALL | 0.4600 | [0.3500, 0.6500] | PASS |
| DEFEAT_SHARE_OVERALL | 0.3670 | [0.3500, 0.6500] | PASS |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.6196 | [0.0000, 0.7000] | PASS |

## Endgame Distribution

| End Type | Count | Share |
|---|---:|---:|
| victory | 460 | 0.4600 |
| defeat | 367 | 0.3670 |
| timeout | 173 | 0.1730 |

- Top victory path: `energyliberation` (`285` wins, share `0.6196`)
