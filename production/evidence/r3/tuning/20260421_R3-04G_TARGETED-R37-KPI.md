# R3-04 KPI Evaluation (R37)

- Batch: `R3-04G-B004-R37`
- Profile: `anti-energy-rebalance-a`
- Runs: `1000`
- Source CSV: `C:\test\4X\production\evidence\r3\tuning\20260421_R3-04G_TARGETED-R37.csv`
- KPI Pass: `7/13`

## Round Parameter Delta

- Bias (Easy/Standard/Hard): `0.22/0.14/0.04`
- Init Scale (Easy/Standard/Hard): `1.12/1.04/1.01`
- Social/Trade Init Scale: `0.19`
- Victory Threshold (Easy/Standard/Hard): `86/84/82`
- Pressure Offset: `0.24`; Conflict Base/TurnScale: `0.390/0.012`
- Delta Scale/Noise Scale: `0.97/2.00`
- Early Suppression: `turn<= 7` with `positive*0.47`
- Shock: `chance=0.40`, `magnitude=+/-32`

## KPI Snapshot

| KPI | Actual | Target | Status |
|---|---:|---:|---|
| WIN_RATE_EASY | 0.5367 | [0.6500, 0.8000] | FAIL |
| WIN_RATE_STANDARD | 0.3525 | [0.4500, 0.5500] | FAIL |
| WIN_RATE_HARD | 0.1167 | [0.2000, 0.3500] | FAIL |
| WIN_RATE_OVERALL | 0.3370 | [0.4000, 0.6000] | FAIL |
| AVG_END_TURN_OVERALL | 15.5410 | [14.0000, 20.0000] | PASS |
| AVG_END_TURN_STANDARD | 17.0950 | [15.0000, 19.0000] | PASS |
| ATTRITION_RATE_OVERALL | 0.2540 | [0.1000, 0.3500] | PASS |
| RESOURCE_CV_GOLDLEAF | 0.3927 | [0.1800, 0.4500] | PASS |
| RESOURCE_CV_FIREOIL | 0.2058 | [0.1500, 0.4000] | PASS |
| RESOURCE_CV_ARMS | 0.2783 | [0.2000, 0.5500] | PASS |
| VICTORY_SHARE_OVERALL | 0.3370 | [0.3500, 0.6500] | FAIL |
| DEFEAT_SHARE_OVERALL | 0.4090 | [0.3500, 0.6500] | PASS |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.8961 | [0.0000, 0.7000] | FAIL |

## Endgame Distribution

| End Type | Count | Share |
|---|---:|---:|
| victory | 337 | 0.3370 |
| defeat | 409 | 0.4090 |
| timeout | 254 | 0.2540 |

- Top victory path: `energyliberation` (`302` wins, share `0.8961`)
