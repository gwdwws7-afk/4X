# R3-04 KPI Evaluation (R44)

- Batch: `R3-04G-B006-R44`
- Profile: `post-combined-rebalance-b`
- Runs: `1000`
- Source CSV: `C:\test\4X\production\evidence\r3\tuning\20260421_R3-04G_TARGETED-R44.csv`
- KPI Pass: `8/13`

## Round Parameter Delta

- Bias (Easy/Standard/Hard): `0.23/0.15/0.04`
- Init Scale (Easy/Standard/Hard): `1.14/1.06/1.04`
- Social/Trade Init Scale: `0.22`
- Victory Threshold (Easy/Standard/Hard): `86/83/82`
- Pressure Offset: `0.24`; Conflict Base/TurnScale: `0.400/0.013`
- Delta Scale/Noise Scale: `0.95/2.04`
- Early Suppression: `turn<= 8` with `positive*0.43`
- Shock: `chance=0.41`, `magnitude=+/-33`

## KPI Snapshot

| KPI | Actual | Target | Status |
|---|---:|---:|---|
| WIN_RATE_EASY | 0.5633 | [0.6500, 0.8000] | FAIL |
| WIN_RATE_STANDARD | 0.4375 | [0.4500, 0.5500] | FAIL |
| WIN_RATE_HARD | 0.1700 | [0.2000, 0.3500] | FAIL |
| WIN_RATE_OVERALL | 0.3950 | [0.4000, 0.6000] | FAIL |
| AVG_END_TURN_OVERALL | 15.1240 | [14.0000, 20.0000] | PASS |
| AVG_END_TURN_STANDARD | 15.5300 | [15.0000, 19.0000] | PASS |
| ATTRITION_RATE_OVERALL | 0.2320 | [0.1000, 0.3500] | PASS |
| RESOURCE_CV_GOLDLEAF | 0.3838 | [0.1800, 0.4500] | PASS |
| RESOURCE_CV_FIREOIL | 0.2108 | [0.1500, 0.4000] | PASS |
| RESOURCE_CV_ARMS | 0.2927 | [0.2000, 0.5500] | PASS |
| VICTORY_SHARE_OVERALL | 0.3950 | [0.3500, 0.6500] | PASS |
| DEFEAT_SHARE_OVERALL | 0.3730 | [0.3500, 0.6500] | PASS |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.8278 | [0.0000, 0.7000] | FAIL |

## Endgame Distribution

| End Type | Count | Share |
|---|---:|---:|
| victory | 395 | 0.3950 |
| defeat | 373 | 0.3730 |
| timeout | 232 | 0.2320 |

- Top victory path: `energyliberation` (`327` wins, share `0.8278`)
