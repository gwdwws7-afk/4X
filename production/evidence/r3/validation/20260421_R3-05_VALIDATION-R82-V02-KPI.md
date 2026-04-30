# R3-04 KPI Evaluation (V02)

- Batch: `R3-05-B002-R82-V02`
- Profile: `r79-pace-backpull-a-validation`
- Runs: `1000`
- Source CSV: `C:\test\4X\production\evidence\r3\validation\20260421_R3-05_VALIDATION-R82-V02.csv`
- KPI Pass: `9/13`

## Round Parameter Delta

- Bias (Easy/Standard/Hard): `0.26/0.14/0.05`
- Init Scale (Easy/Standard/Hard): `1.13/1.05/1.03`
- Social/Trade Init Scale: `0.16`
- Victory Threshold (Easy/Standard/Hard): `85/83/81`
- Pressure Offset: `0.24`; Conflict Base/TurnScale: `0.400/0.013`
- Delta Scale/Noise Scale: `0.96/2.05`
- Early Suppression: `turn<= 7` with `positive*0.47`
- Shock: `chance=0.41`, `magnitude=+/-33`

## KPI Snapshot

| KPI | Actual | Target | Status |
|---|---:|---:|---|
| WIN_RATE_EASY | 0.7133 | [0.6500, 0.8000] | PASS |
| WIN_RATE_STANDARD | 0.4475 | [0.4500, 0.5500] | FAIL |
| WIN_RATE_HARD | 0.3100 | [0.2000, 0.3500] | PASS |
| WIN_RATE_OVERALL | 0.4860 | [0.4000, 0.6000] | PASS |
| AVG_END_TURN_OVERALL | 13.7200 | [14.0000, 20.0000] | FAIL |
| AVG_END_TURN_STANDARD | 15.7075 | [15.0000, 19.0000] | PASS |
| ATTRITION_RATE_OVERALL | 0.1770 | [0.1000, 0.3500] | PASS |
| RESOURCE_CV_GOLDLEAF | 0.3551 | [0.1800, 0.4500] | PASS |
| RESOURCE_CV_FIREOIL | 0.1910 | [0.1500, 0.4000] | PASS |
| RESOURCE_CV_ARMS | 0.2573 | [0.2000, 0.5500] | PASS |
| VICTORY_SHARE_OVERALL | 0.4860 | [0.3500, 0.6500] | PASS |
| DEFEAT_SHARE_OVERALL | 0.3370 | [0.3500, 0.6500] | FAIL |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.7037 | [0.0000, 0.7000] | FAIL |

## Endgame Distribution

| End Type | Count | Share |
|---|---:|---:|
| victory | 486 | 0.4860 |
| defeat | 337 | 0.3370 |
| timeout | 177 | 0.1770 |

- Top victory path: `energyliberation` (`342` wins, share `0.7037`)
