# R3-04 KPI Evaluation (R46)

- Batch: `R3-04G-B007-R46`
- Profile: `r23-split-hard-pressure`
- Runs: `1000`
- Source CSV: `C:\test\4X\production\evidence\r3\tuning\20260421_R3-04G_TARGETED-R46.csv`
- KPI Pass: `8/13`

## Round Parameter Delta

- Bias (Easy/Standard/Hard): `0.27/0.14/-0.02`
- Init Scale (Easy/Standard/Hard): `1.14/1.05/0.94`
- Social/Trade Init Scale: `0.24`
- Victory Threshold (Easy/Standard/Hard): `86/83/85`
- Pressure Offset: `0.23`; Conflict Base/TurnScale: `0.390/0.013`
- Delta Scale/Noise Scale: `0.96/2.02`
- Early Suppression: `turn<= 7` with `positive*0.46`
- Shock: `chance=0.42`, `magnitude=+/-34`

## KPI Snapshot

| KPI | Actual | Target | Status |
|---|---:|---:|---|
| WIN_RATE_EASY | 0.6633 | [0.6500, 0.8000] | PASS |
| WIN_RATE_STANDARD | 0.4125 | [0.4500, 0.5500] | FAIL |
| WIN_RATE_HARD | 0.0200 | [0.2000, 0.3500] | FAIL |
| WIN_RATE_OVERALL | 0.3700 | [0.4000, 0.6000] | FAIL |
| AVG_END_TURN_OVERALL | 13.6130 | [14.0000, 20.0000] | FAIL |
| AVG_END_TURN_STANDARD | 15.6400 | [15.0000, 19.0000] | PASS |
| ATTRITION_RATE_OVERALL | 0.1860 | [0.1000, 0.3500] | PASS |
| RESOURCE_CV_GOLDLEAF | 0.3861 | [0.1800, 0.4500] | PASS |
| RESOURCE_CV_FIREOIL | 0.2044 | [0.1500, 0.4000] | PASS |
| RESOURCE_CV_ARMS | 0.2844 | [0.2000, 0.5500] | PASS |
| VICTORY_SHARE_OVERALL | 0.3700 | [0.3500, 0.6500] | PASS |
| DEFEAT_SHARE_OVERALL | 0.4440 | [0.3500, 0.6500] | PASS |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.8973 | [0.0000, 0.7000] | FAIL |

## Endgame Distribution

| End Type | Count | Share |
|---|---:|---:|
| victory | 370 | 0.3700 |
| defeat | 444 | 0.4440 |
| timeout | 186 | 0.1860 |

- Top victory path: `energyliberation` (`332` wins, share `0.8973`)
