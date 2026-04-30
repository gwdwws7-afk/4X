# R3-04 KPI Evaluation (R15)

- Batch: `R3-04C-B001-R15`
- Profile: `overall-balance-candidate`
- Runs: `1000`
- Source CSV: `C:\test\4X\production\evidence\r3\tuning\20260420_R3-04C_TARGETED-R15.csv`
- KPI Pass: `5/13`

## Round Parameter Delta

- Bias (Easy/Standard/Hard): `0.18/0.09/-0.05`
- Init Scale (Easy/Standard/Hard): `1.04/1.00/0.94`
- Social/Trade Init Scale: `0.46`
- Victory Threshold (Easy/Standard/Hard): `90/89/84`
- Pressure Offset: `0.18`; Conflict Base/TurnScale: `0.360/0.012`
- Delta Scale/Noise Scale: `0.95/1.90`
- Early Suppression: `turn<= 7` with `positive*0.50`
- Shock: `chance=0.32`, `magnitude=+/-26`

## KPI Snapshot

| KPI | Actual | Target | Status |
|---|---:|---:|---|
| WIN_RATE_EASY | 0.6333 | [0.6500, 0.8000] | FAIL |
| WIN_RATE_STANDARD | 0.2425 | [0.4500, 0.5500] | FAIL |
| WIN_RATE_HARD | 0.0333 | [0.2000, 0.3500] | FAIL |
| WIN_RATE_OVERALL | 0.2970 | [0.4000, 0.6000] | FAIL |
| AVG_END_TURN_OVERALL | 13.7840 | [14.0000, 20.0000] | FAIL |
| AVG_END_TURN_STANDARD | 17.6800 | [15.0000, 19.0000] | PASS |
| ATTRITION_RATE_OVERALL | 0.2640 | [0.1000, 0.3500] | PASS |
| RESOURCE_CV_GOLDLEAF | 0.3347 | [0.1800, 0.4500] | PASS |
| RESOURCE_CV_FIREOIL | 0.1596 | [0.1500, 0.4000] | PASS |
| RESOURCE_CV_ARMS | 0.1988 | [0.2000, 0.5500] | FAIL |
| VICTORY_SHARE_OVERALL | 0.2970 | [0.3500, 0.6500] | FAIL |
| DEFEAT_SHARE_OVERALL | 0.4390 | [0.3500, 0.6500] | PASS |
| SINGLE_PATH_VICTORY_MONOPOLY | 1.0000 | [0.0000, 0.7000] | FAIL |

## Endgame Distribution

| End Type | Count | Share |
|---|---:|---:|
| victory | 297 | 0.2970 |
| defeat | 439 | 0.4390 |
| timeout | 264 | 0.2640 |

- Top victory path: `energyliberation` (`297` wins, share `1.0000`)
