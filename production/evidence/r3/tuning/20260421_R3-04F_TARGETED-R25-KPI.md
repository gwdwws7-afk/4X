# R3-04 KPI Evaluation (R25)

- Batch: `R3-04F-B001-R25`
- Profile: `hard-clamp-easy-lift`
- Runs: `1000`
- Source CSV: `C:\test\4X\production\evidence\r3\tuning\20260421_R3-04F_TARGETED-R25.csv`
- KPI Pass: `7/13`

## Round Parameter Delta

- Bias (Easy/Standard/Hard): `0.28/0.11/-0.10`
- Init Scale (Easy/Standard/Hard): `1.16/1.00/0.88`
- Social/Trade Init Scale: `0.23`
- Victory Threshold (Easy/Standard/Hard): `86/84/85`
- Pressure Offset: `0.24`; Conflict Base/TurnScale: `0.410/0.014`
- Delta Scale/Noise Scale: `0.95/2.12`
- Early Suppression: `turn<= 7` with `positive*0.45`
- Shock: `chance=0.45`, `magnitude=+/-36`

## KPI Snapshot

| KPI | Actual | Target | Status |
|---|---:|---:|---|
| WIN_RATE_EASY | 0.6833 | [0.6500, 0.8000] | PASS |
| WIN_RATE_STANDARD | 0.2900 | [0.4500, 0.5500] | FAIL |
| WIN_RATE_HARD | 0.0033 | [0.2000, 0.3500] | FAIL |
| WIN_RATE_OVERALL | 0.3220 | [0.4000, 0.6000] | FAIL |
| AVG_END_TURN_OVERALL | 12.1380 | [14.0000, 20.0000] | FAIL |
| AVG_END_TURN_STANDARD | 15.4675 | [15.0000, 19.0000] | PASS |
| ATTRITION_RATE_OVERALL | 0.1700 | [0.1000, 0.3500] | PASS |
| RESOURCE_CV_GOLDLEAF | 0.3692 | [0.1800, 0.4500] | PASS |
| RESOURCE_CV_FIREOIL | 0.2133 | [0.1500, 0.4000] | PASS |
| RESOURCE_CV_ARMS | 0.2891 | [0.2000, 0.5500] | PASS |
| VICTORY_SHARE_OVERALL | 0.3220 | [0.3500, 0.6500] | FAIL |
| DEFEAT_SHARE_OVERALL | 0.5080 | [0.3500, 0.6500] | PASS |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.9565 | [0.0000, 0.7000] | FAIL |

## Endgame Distribution

| End Type | Count | Share |
|---|---:|---:|
| victory | 322 | 0.3220 |
| defeat | 508 | 0.5080 |
| timeout | 170 | 0.1700 |

- Top victory path: `energyliberation` (`308` wins, share `0.9565`)
