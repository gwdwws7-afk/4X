# R3-04 KPI Evaluation (R45)

- Batch: `R3-04G-B006-R45`
- Profile: `post-combined-rebalance-c`
- Runs: `1000`
- Source CSV: `C:\test\4X\production\evidence\r3\tuning\20260421_R3-04G_TARGETED-R45.csv`
- KPI Pass: `8/13`

## Round Parameter Delta

- Bias (Easy/Standard/Hard): `0.23/0.14/0.03`
- Init Scale (Easy/Standard/Hard): `1.14/1.05/1.02`
- Social/Trade Init Scale: `0.21`
- Victory Threshold (Easy/Standard/Hard): `86/83/83`
- Pressure Offset: `0.23`; Conflict Base/TurnScale: `0.390/0.012`
- Delta Scale/Noise Scale: `0.96/2.00`
- Early Suppression: `turn<= 8` with `positive*0.44`
- Shock: `chance=0.40`, `magnitude=+/-32`

## KPI Snapshot

| KPI | Actual | Target | Status |
|---|---:|---:|---|
| WIN_RATE_EASY | 0.5767 | [0.6500, 0.8000] | FAIL |
| WIN_RATE_STANDARD | 0.4400 | [0.4500, 0.5500] | FAIL |
| WIN_RATE_HARD | 0.0933 | [0.2000, 0.3500] | FAIL |
| WIN_RATE_OVERALL | 0.3770 | [0.4000, 0.6000] | FAIL |
| AVG_END_TURN_OVERALL | 15.1600 | [14.0000, 20.0000] | PASS |
| AVG_END_TURN_STANDARD | 15.6825 | [15.0000, 19.0000] | PASS |
| ATTRITION_RATE_OVERALL | 0.2330 | [0.1000, 0.3500] | PASS |
| RESOURCE_CV_GOLDLEAF | 0.3874 | [0.1800, 0.4500] | PASS |
| RESOURCE_CV_FIREOIL | 0.2097 | [0.1500, 0.4000] | PASS |
| RESOURCE_CV_ARMS | 0.2787 | [0.2000, 0.5500] | PASS |
| VICTORY_SHARE_OVERALL | 0.3770 | [0.3500, 0.6500] | PASS |
| DEFEAT_SHARE_OVERALL | 0.3900 | [0.3500, 0.6500] | PASS |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.8568 | [0.0000, 0.7000] | FAIL |

## Endgame Distribution

| End Type | Count | Share |
|---|---:|---:|
| victory | 377 | 0.3770 |
| defeat | 390 | 0.3900 |
| timeout | 233 | 0.2330 |

- Top victory path: `energyliberation` (`323` wins, share `0.8568`)
