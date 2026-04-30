# R3-04 KPI Evaluation (R42)

- Batch: `R3-04G-B005-R42`
- Profile: `r36-close-pace-diversity`
- Runs: `1000`
- Source CSV: `C:\test\4X\production\evidence\r3\tuning\20260421_R3-04G_TARGETED-R42.csv`
- KPI Pass: `7/13`

## Round Parameter Delta

- Bias (Easy/Standard/Hard): `0.22/0.14/0.03`
- Init Scale (Easy/Standard/Hard): `1.12/1.04/1.01`
- Social/Trade Init Scale: `0.21`
- Victory Threshold (Easy/Standard/Hard): `86/84/82`
- Pressure Offset: `0.24`; Conflict Base/TurnScale: `0.390/0.013`
- Delta Scale/Noise Scale: `0.96/2.02`
- Early Suppression: `turn<= 8` with `positive*0.45`
- Shock: `chance=0.41`, `magnitude=+/-33`

## KPI Snapshot

| KPI | Actual | Target | Status |
|---|---:|---:|---|
| WIN_RATE_EASY | 0.5033 | [0.6500, 0.8000] | FAIL |
| WIN_RATE_STANDARD | 0.3750 | [0.4500, 0.5500] | FAIL |
| WIN_RATE_HARD | 0.0867 | [0.2000, 0.3500] | FAIL |
| WIN_RATE_OVERALL | 0.3270 | [0.4000, 0.6000] | FAIL |
| AVG_END_TURN_OVERALL | 15.3350 | [14.0000, 20.0000] | PASS |
| AVG_END_TURN_STANDARD | 16.4200 | [15.0000, 19.0000] | PASS |
| ATTRITION_RATE_OVERALL | 0.2440 | [0.1000, 0.3500] | PASS |
| RESOURCE_CV_GOLDLEAF | 0.4170 | [0.1800, 0.4500] | PASS |
| RESOURCE_CV_FIREOIL | 0.2187 | [0.1500, 0.4000] | PASS |
| RESOURCE_CV_ARMS | 0.2905 | [0.2000, 0.5500] | PASS |
| VICTORY_SHARE_OVERALL | 0.3270 | [0.3500, 0.6500] | FAIL |
| DEFEAT_SHARE_OVERALL | 0.4290 | [0.3500, 0.6500] | PASS |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.8777 | [0.0000, 0.7000] | FAIL |

## Endgame Distribution

| End Type | Count | Share |
|---|---:|---:|
| victory | 327 | 0.3270 |
| defeat | 429 | 0.4290 |
| timeout | 244 | 0.2440 |

- Top victory path: `energyliberation` (`287` wins, share `0.8777`)
