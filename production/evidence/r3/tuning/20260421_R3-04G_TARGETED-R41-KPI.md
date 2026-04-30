# R3-04 KPI Evaluation (R41)

- Batch: `R3-04G-B005-R41`
- Profile: `r36-close-hard-recover-b`
- Runs: `1000`
- Source CSV: `C:\test\4X\production\evidence\r3\tuning\20260421_R3-04G_TARGETED-R41.csv`
- KPI Pass: `8/13`

## Round Parameter Delta

- Bias (Easy/Standard/Hard): `0.23/0.14/0.02`
- Init Scale (Easy/Standard/Hard): `1.13/1.05/1.00`
- Social/Trade Init Scale: `0.22`
- Victory Threshold (Easy/Standard/Hard): `86/83/83`
- Pressure Offset: `0.23`; Conflict Base/TurnScale: `0.380/0.012`
- Delta Scale/Noise Scale: `0.96/2.00`
- Early Suppression: `turn<= 7` with `positive*0.46`
- Shock: `chance=0.40`, `magnitude=+/-32`

## KPI Snapshot

| KPI | Actual | Target | Status |
|---|---:|---:|---|
| WIN_RATE_EASY | 0.5867 | [0.6500, 0.8000] | FAIL |
| WIN_RATE_STANDARD | 0.4200 | [0.4500, 0.5500] | FAIL |
| WIN_RATE_HARD | 0.0900 | [0.2000, 0.3500] | FAIL |
| WIN_RATE_OVERALL | 0.3710 | [0.4000, 0.6000] | FAIL |
| AVG_END_TURN_OVERALL | 15.2170 | [14.0000, 20.0000] | PASS |
| AVG_END_TURN_STANDARD | 15.9150 | [15.0000, 19.0000] | PASS |
| ATTRITION_RATE_OVERALL | 0.2340 | [0.1000, 0.3500] | PASS |
| RESOURCE_CV_GOLDLEAF | 0.3763 | [0.1800, 0.4500] | PASS |
| RESOURCE_CV_FIREOIL | 0.2045 | [0.1500, 0.4000] | PASS |
| RESOURCE_CV_ARMS | 0.2740 | [0.2000, 0.5500] | PASS |
| VICTORY_SHARE_OVERALL | 0.3710 | [0.3500, 0.6500] | PASS |
| DEFEAT_SHARE_OVERALL | 0.3950 | [0.3500, 0.6500] | PASS |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.8571 | [0.0000, 0.7000] | FAIL |

## Endgame Distribution

| End Type | Count | Share |
|---|---:|---:|
| victory | 371 | 0.3710 |
| defeat | 395 | 0.3950 |
| timeout | 234 | 0.2340 |

- Top victory path: `energyliberation` (`318` wins, share `0.8571`)
