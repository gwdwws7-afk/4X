# R3-04 KPI Evaluation (R83)

- Batch: `R3-04H-B004-R83`
- Profile: `r79-pace-backpull-b`
- Runs: `1000`
- Source CSV: `C:\test\4X\production\evidence\r3\tuning\20260421_R3-04H_TARGETED-R83.csv`
- KPI Pass: `11/13`

## Round Parameter Delta

- Bias (Easy/Standard/Hard): `0.26/0.14/0.05`
- Init Scale (Easy/Standard/Hard): `1.12/1.05/1.04`
- Social/Trade Init Scale: `0.16`
- Victory Threshold (Easy/Standard/Hard): `85/83/81`
- Pressure Offset: `0.23`; Conflict Base/TurnScale: `0.390/0.013`
- Delta Scale/Noise Scale: `0.96/2.05`
- Early Suppression: `turn<= 8` with `positive*0.46`
- Shock: `chance=0.41`, `magnitude=+/-33`

## KPI Snapshot

| KPI | Actual | Target | Status |
|---|---:|---:|---|
| WIN_RATE_EASY | 0.6667 | [0.6500, 0.8000] | PASS |
| WIN_RATE_STANDARD | 0.5250 | [0.4500, 0.5500] | PASS |
| WIN_RATE_HARD | 0.2267 | [0.2000, 0.3500] | PASS |
| WIN_RATE_OVERALL | 0.4780 | [0.4000, 0.6000] | PASS |
| AVG_END_TURN_OVERALL | 13.8610 | [14.0000, 20.0000] | FAIL |
| AVG_END_TURN_STANDARD | 15.0525 | [15.0000, 19.0000] | PASS |
| ATTRITION_RATE_OVERALL | 0.1820 | [0.1000, 0.3500] | PASS |
| RESOURCE_CV_GOLDLEAF | 0.3431 | [0.1800, 0.4500] | PASS |
| RESOURCE_CV_FIREOIL | 0.1830 | [0.1500, 0.4000] | PASS |
| RESOURCE_CV_ARMS | 0.2780 | [0.2000, 0.5500] | PASS |
| VICTORY_SHARE_OVERALL | 0.4780 | [0.3500, 0.6500] | PASS |
| DEFEAT_SHARE_OVERALL | 0.3400 | [0.3500, 0.6500] | FAIL |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.6862 | [0.0000, 0.7000] | PASS |

## Endgame Distribution

| End Type | Count | Share |
|---|---:|---:|
| victory | 478 | 0.4780 |
| defeat | 340 | 0.3400 |
| timeout | 182 | 0.1820 |

- Top victory path: `energyliberation` (`328` wins, share `0.6862`)
