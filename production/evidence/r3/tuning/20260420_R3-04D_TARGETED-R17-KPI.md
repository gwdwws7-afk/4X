# R3-04 KPI Evaluation (R17)

- Batch: `R3-04D-B001-R17`
- Profile: `std-hard-balance`
- Runs: `1000`
- Source CSV: `C:\test\4X\production\evidence\r3\tuning\20260420_R3-04D_TARGETED-R17.csv`
- KPI Pass: `7/13`

## Round Parameter Delta

- Bias (Easy/Standard/Hard): `0.14/0.10/0.04`
- Init Scale (Easy/Standard/Hard): `1.00/1.00/1.00`
- Social/Trade Init Scale: `0.40`
- Victory Threshold (Easy/Standard/Hard): `88/84/82`
- Pressure Offset: `0.18`; Conflict Base/TurnScale: `0.360/0.013`
- Delta Scale/Noise Scale: `0.94/1.90`
- Early Suppression: `turn<= 7` with `positive*0.52`
- Shock: `chance=0.36`, `magnitude=+/-30`

## KPI Snapshot

| KPI | Actual | Target | Status |
|---|---:|---:|---|
| WIN_RATE_EASY | 0.5200 | [0.6500, 0.8000] | FAIL |
| WIN_RATE_STANDARD | 0.4275 | [0.4500, 0.5500] | FAIL |
| WIN_RATE_HARD | 0.1633 | [0.2000, 0.3500] | FAIL |
| WIN_RATE_OVERALL | 0.3760 | [0.4000, 0.6000] | FAIL |
| AVG_END_TURN_OVERALL | 14.2980 | [14.0000, 20.0000] | PASS |
| AVG_END_TURN_STANDARD | 14.8150 | [15.0000, 19.0000] | FAIL |
| ATTRITION_RATE_OVERALL | 0.2090 | [0.1000, 0.3500] | PASS |
| RESOURCE_CV_GOLDLEAF | 0.3323 | [0.1800, 0.4500] | PASS |
| RESOURCE_CV_FIREOIL | 0.1834 | [0.1500, 0.4000] | PASS |
| RESOURCE_CV_ARMS | 0.2479 | [0.2000, 0.5500] | PASS |
| VICTORY_SHARE_OVERALL | 0.3760 | [0.3500, 0.6500] | PASS |
| DEFEAT_SHARE_OVERALL | 0.4150 | [0.3500, 0.6500] | PASS |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.9787 | [0.0000, 0.7000] | FAIL |

## Endgame Distribution

| End Type | Count | Share |
|---|---:|---:|
| victory | 376 | 0.3760 |
| defeat | 415 | 0.4150 |
| timeout | 209 | 0.2090 |

- Top victory path: `energyliberation` (`368` wins, share `0.9787`)
