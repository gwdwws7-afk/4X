# R3-04 KPI Evaluation (R40)

- Batch: `R3-04G-B005-R40`
- Profile: `r36-close-hard-recover-a`
- Runs: `1000`
- Source CSV: `C:\test\4X\production\evidence\r3\tuning\20260421_R3-04G_TARGETED-R40.csv`
- KPI Pass: `9/13`

## Round Parameter Delta

- Bias (Easy/Standard/Hard): `0.23/0.14/0.03`
- Init Scale (Easy/Standard/Hard): `1.13/1.05/1.02`
- Social/Trade Init Scale: `0.22`
- Victory Threshold (Easy/Standard/Hard): `86/83/82`
- Pressure Offset: `0.23`; Conflict Base/TurnScale: `0.390/0.012`
- Delta Scale/Noise Scale: `0.97/2.00`
- Early Suppression: `turn<= 7` with `positive*0.46`
- Shock: `chance=0.40`, `magnitude=+/-32`

## KPI Snapshot

| KPI | Actual | Target | Status |
|---|---:|---:|---|
| WIN_RATE_EASY | 0.6200 | [0.6500, 0.8000] | FAIL |
| WIN_RATE_STANDARD | 0.4400 | [0.4500, 0.5500] | FAIL |
| WIN_RATE_HARD | 0.1300 | [0.2000, 0.3500] | FAIL |
| WIN_RATE_OVERALL | 0.4010 | [0.4000, 0.6000] | PASS |
| AVG_END_TURN_OVERALL | 14.9740 | [14.0000, 20.0000] | PASS |
| AVG_END_TURN_STANDARD | 16.0200 | [15.0000, 19.0000] | PASS |
| ATTRITION_RATE_OVERALL | 0.2270 | [0.1000, 0.3500] | PASS |
| RESOURCE_CV_GOLDLEAF | 0.3729 | [0.1800, 0.4500] | PASS |
| RESOURCE_CV_FIREOIL | 0.1998 | [0.1500, 0.4000] | PASS |
| RESOURCE_CV_ARMS | 0.2757 | [0.2000, 0.5500] | PASS |
| VICTORY_SHARE_OVERALL | 0.4010 | [0.3500, 0.6500] | PASS |
| DEFEAT_SHARE_OVERALL | 0.3720 | [0.3500, 0.6500] | PASS |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.8653 | [0.0000, 0.7000] | FAIL |

## Endgame Distribution

| End Type | Count | Share |
|---|---:|---:|
| victory | 401 | 0.4010 |
| defeat | 372 | 0.3720 |
| timeout | 227 | 0.2270 |

- Top victory path: `energyliberation` (`347` wins, share `0.8653`)
