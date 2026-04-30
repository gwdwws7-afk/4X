# R3-04 KPI Evaluation (R31)

- Batch: `R3-04G-B002-R31`
- Profile: `r23-guided-rebalance-a`
- Runs: `1000`
- Source CSV: `C:\test\4X\production\evidence\r3\tuning\20260421_R3-04G_TARGETED-R31.csv`
- KPI Pass: `9/13`

## Round Parameter Delta

- Bias (Easy/Standard/Hard): `0.26/0.13/0.00`
- Init Scale (Easy/Standard/Hard): `1.12/1.03/0.98`
- Social/Trade Init Scale: `0.23`
- Victory Threshold (Easy/Standard/Hard): `86/84/83`
- Pressure Offset: `0.23`; Conflict Base/TurnScale: `0.390/0.012`
- Delta Scale/Noise Scale: `0.97/2.00`
- Early Suppression: `turn<= 7` with `positive*0.48`
- Shock: `chance=0.41`, `magnitude=+/-33`

## KPI Snapshot

| KPI | Actual | Target | Status |
|---|---:|---:|---|
| WIN_RATE_EASY | 0.6733 | [0.6500, 0.8000] | PASS |
| WIN_RATE_STANDARD | 0.3375 | [0.4500, 0.5500] | FAIL |
| WIN_RATE_HARD | 0.0667 | [0.2000, 0.3500] | FAIL |
| WIN_RATE_OVERALL | 0.3570 | [0.4000, 0.6000] | FAIL |
| AVG_END_TURN_OVERALL | 14.5380 | [14.0000, 20.0000] | PASS |
| AVG_END_TURN_STANDARD | 16.6400 | [15.0000, 19.0000] | PASS |
| ATTRITION_RATE_OVERALL | 0.2060 | [0.1000, 0.3500] | PASS |
| RESOURCE_CV_GOLDLEAF | 0.3947 | [0.1800, 0.4500] | PASS |
| RESOURCE_CV_FIREOIL | 0.2096 | [0.1500, 0.4000] | PASS |
| RESOURCE_CV_ARMS | 0.2900 | [0.2000, 0.5500] | PASS |
| VICTORY_SHARE_OVERALL | 0.3570 | [0.3500, 0.6500] | PASS |
| DEFEAT_SHARE_OVERALL | 0.4370 | [0.3500, 0.6500] | PASS |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.9468 | [0.0000, 0.7000] | FAIL |

## Endgame Distribution

| End Type | Count | Share |
|---|---:|---:|
| victory | 357 | 0.3570 |
| defeat | 437 | 0.4370 |
| timeout | 206 | 0.2060 |

- Top victory path: `energyliberation` (`338` wins, share `0.9468`)
