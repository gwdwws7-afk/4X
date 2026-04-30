# R3-04 KPI Evaluation (R55)

- Batch: `R3-04G-B010-R55`
- Profile: `axis-diversify-hard-clamp-a`
- Runs: `1000`
- Source CSV: `C:\test\4X\production\evidence\r3\tuning\20260421_R3-04G_TARGETED-R55.csv`
- KPI Pass: `10/13`

## Round Parameter Delta

- Bias (Easy/Standard/Hard): `0.24/0.14/0.04`
- Init Scale (Easy/Standard/Hard): `1.11/1.05/1.02`
- Social/Trade Init Scale: `0.24`
- Victory Threshold (Easy/Standard/Hard): `86/83/80`
- Pressure Offset: `0.22`; Conflict Base/TurnScale: `0.380/0.012`
- Delta Scale/Noise Scale: `0.98/2.00`
- Early Suppression: `turn<= 7` with `positive*0.48`
- Shock: `chance=0.40`, `magnitude=+/-32`

## KPI Snapshot

| KPI | Actual | Target | Status |
|---|---:|---:|---|
| WIN_RATE_EASY | 0.6667 | [0.6500, 0.8000] | PASS |
| WIN_RATE_STANDARD | 0.4625 | [0.4500, 0.5500] | PASS |
| WIN_RATE_HARD | 0.5767 | [0.2000, 0.3500] | FAIL |
| WIN_RATE_OVERALL | 0.5580 | [0.4000, 0.6000] | PASS |
| AVG_END_TURN_OVERALL | 13.2150 | [14.0000, 20.0000] | FAIL |
| AVG_END_TURN_STANDARD | 15.9200 | [15.0000, 19.0000] | PASS |
| ATTRITION_RATE_OVERALL | 0.1870 | [0.1000, 0.3500] | PASS |
| RESOURCE_CV_GOLDLEAF | 0.3192 | [0.1800, 0.4500] | PASS |
| RESOURCE_CV_FIREOIL | 0.1793 | [0.1500, 0.4000] | PASS |
| RESOURCE_CV_ARMS | 0.2482 | [0.2000, 0.5500] | PASS |
| VICTORY_SHARE_OVERALL | 0.5580 | [0.3500, 0.6500] | PASS |
| DEFEAT_SHARE_OVERALL | 0.2550 | [0.3500, 0.6500] | FAIL |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.6344 | [0.0000, 0.7000] | PASS |

## Endgame Distribution

| End Type | Count | Share |
|---|---:|---:|
| victory | 558 | 0.5580 |
| defeat | 255 | 0.2550 |
| timeout | 187 | 0.1870 |

- Top victory path: `energyliberation` (`354` wins, share `0.6344`)
