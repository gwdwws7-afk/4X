# R3-04 KPI Evaluation (R35)

- Batch: `R3-04G-B003-R35`
- Profile: `defeat-share-tune`
- Runs: `1000`
- Source CSV: `C:\test\4X\production\evidence\r3\tuning\20260421_R3-04G_TARGETED-R35.csv`
- KPI Pass: `7/13`

## Round Parameter Delta

- Bias (Easy/Standard/Hard): `0.22/0.13/-0.04`
- Init Scale (Easy/Standard/Hard): `1.15/1.04/0.93`
- Social/Trade Init Scale: `0.24`
- Victory Threshold (Easy/Standard/Hard): `86/84/85`
- Pressure Offset: `0.24`; Conflict Base/TurnScale: `0.390/0.013`
- Delta Scale/Noise Scale: `0.97/2.02`
- Early Suppression: `turn<= 7` with `positive*0.48`
- Shock: `chance=0.41`, `magnitude=+/-33`

## KPI Snapshot

| KPI | Actual | Target | Status |
|---|---:|---:|---|
| WIN_RATE_EASY | 0.5000 | [0.6500, 0.8000] | FAIL |
| WIN_RATE_STANDARD | 0.3200 | [0.4500, 0.5500] | FAIL |
| WIN_RATE_HARD | 0.0167 | [0.2000, 0.3500] | FAIL |
| WIN_RATE_OVERALL | 0.2830 | [0.4000, 0.6000] | FAIL |
| AVG_END_TURN_OVERALL | 14.6590 | [14.0000, 20.0000] | PASS |
| AVG_END_TURN_STANDARD | 16.8325 | [15.0000, 19.0000] | PASS |
| ATTRITION_RATE_OVERALL | 0.2560 | [0.1000, 0.3500] | PASS |
| RESOURCE_CV_GOLDLEAF | 0.4064 | [0.1800, 0.4500] | PASS |
| RESOURCE_CV_FIREOIL | 0.2121 | [0.1500, 0.4000] | PASS |
| RESOURCE_CV_ARMS | 0.2858 | [0.2000, 0.5500] | PASS |
| VICTORY_SHARE_OVERALL | 0.2830 | [0.3500, 0.6500] | FAIL |
| DEFEAT_SHARE_OVERALL | 0.4610 | [0.3500, 0.6500] | PASS |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.9364 | [0.0000, 0.7000] | FAIL |

## Endgame Distribution

| End Type | Count | Share |
|---|---:|---:|
| victory | 283 | 0.2830 |
| defeat | 461 | 0.4610 |
| timeout | 256 | 0.2560 |

- Top victory path: `energyliberation` (`265` wins, share `0.9364`)
