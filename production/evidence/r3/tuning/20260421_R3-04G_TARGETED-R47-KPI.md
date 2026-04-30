# R3-04 KPI Evaluation (R47)

- Batch: `R3-04G-B007-R47`
- Profile: `r23-balanced-mid`
- Runs: `1000`
- Source CSV: `C:\test\4X\production\evidence\r3\tuning\20260421_R3-04G_TARGETED-R47.csv`
- KPI Pass: `9/13`

## Round Parameter Delta

- Bias (Easy/Standard/Hard): `0.25/0.14/0.00`
- Init Scale (Easy/Standard/Hard): `1.12/1.05/0.98`
- Social/Trade Init Scale: `0.24`
- Victory Threshold (Easy/Standard/Hard): `86/83/84`
- Pressure Offset: `0.22`; Conflict Base/TurnScale: `0.390/0.013`
- Delta Scale/Noise Scale: `0.96/2.00`
- Early Suppression: `turn<= 7` with `positive*0.47`
- Shock: `chance=0.41`, `magnitude=+/-33`

## KPI Snapshot

| KPI | Actual | Target | Status |
|---|---:|---:|---|
| WIN_RATE_EASY | 0.5933 | [0.6500, 0.8000] | FAIL |
| WIN_RATE_STANDARD | 0.4525 | [0.4500, 0.5500] | PASS |
| WIN_RATE_HARD | 0.0500 | [0.2000, 0.3500] | FAIL |
| WIN_RATE_OVERALL | 0.3740 | [0.4000, 0.6000] | FAIL |
| AVG_END_TURN_OVERALL | 14.6640 | [14.0000, 20.0000] | PASS |
| AVG_END_TURN_STANDARD | 15.6900 | [15.0000, 19.0000] | PASS |
| ATTRITION_RATE_OVERALL | 0.2170 | [0.1000, 0.3500] | PASS |
| RESOURCE_CV_GOLDLEAF | 0.3718 | [0.1800, 0.4500] | PASS |
| RESOURCE_CV_FIREOIL | 0.2137 | [0.1500, 0.4000] | PASS |
| RESOURCE_CV_ARMS | 0.2723 | [0.2000, 0.5500] | PASS |
| VICTORY_SHARE_OVERALL | 0.3740 | [0.3500, 0.6500] | PASS |
| DEFEAT_SHARE_OVERALL | 0.4090 | [0.3500, 0.6500] | PASS |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.8770 | [0.0000, 0.7000] | FAIL |

## Endgame Distribution

| End Type | Count | Share |
|---|---:|---:|
| victory | 374 | 0.3740 |
| defeat | 409 | 0.4090 |
| timeout | 217 | 0.2170 |

- Top victory path: `energyliberation` (`328` wins, share `0.8770`)
