# R3-04 KPI Evaluation (R38)

- Batch: `R3-04G-B004-R38`
- Profile: `anti-energy-rebalance-b`
- Runs: `1000`
- Source CSV: `C:\test\4X\production\evidence\r3\tuning\20260421_R3-04G_TARGETED-R38.csv`
- KPI Pass: `7/13`

## Round Parameter Delta

- Bias (Easy/Standard/Hard): `0.23/0.14/0.03`
- Init Scale (Easy/Standard/Hard): `1.13/1.05/1.00`
- Social/Trade Init Scale: `0.20`
- Victory Threshold (Easy/Standard/Hard): `86/84/82`
- Pressure Offset: `0.24`; Conflict Base/TurnScale: `0.400/0.013`
- Delta Scale/Noise Scale: `0.96/2.04`
- Early Suppression: `turn<= 7` with `positive*0.46`
- Shock: `chance=0.41`, `magnitude=+/-33`

## KPI Snapshot

| KPI | Actual | Target | Status |
|---|---:|---:|---|
| WIN_RATE_EASY | 0.5500 | [0.6500, 0.8000] | FAIL |
| WIN_RATE_STANDARD | 0.3175 | [0.4500, 0.5500] | FAIL |
| WIN_RATE_HARD | 0.1400 | [0.2000, 0.3500] | FAIL |
| WIN_RATE_OVERALL | 0.3340 | [0.4000, 0.6000] | FAIL |
| AVG_END_TURN_OVERALL | 15.6500 | [14.0000, 20.0000] | PASS |
| AVG_END_TURN_STANDARD | 16.9700 | [15.0000, 19.0000] | PASS |
| ATTRITION_RATE_OVERALL | 0.2560 | [0.1000, 0.3500] | PASS |
| RESOURCE_CV_GOLDLEAF | 0.3944 | [0.1800, 0.4500] | PASS |
| RESOURCE_CV_FIREOIL | 0.2175 | [0.1500, 0.4000] | PASS |
| RESOURCE_CV_ARMS | 0.2906 | [0.2000, 0.5500] | PASS |
| VICTORY_SHARE_OVERALL | 0.3340 | [0.3500, 0.6500] | FAIL |
| DEFEAT_SHARE_OVERALL | 0.4100 | [0.3500, 0.6500] | PASS |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.8772 | [0.0000, 0.7000] | FAIL |

## Endgame Distribution

| End Type | Count | Share |
|---|---:|---:|
| victory | 334 | 0.3340 |
| defeat | 410 | 0.4100 |
| timeout | 256 | 0.2560 |

- Top victory path: `energyliberation` (`293` wins, share `0.8772`)
