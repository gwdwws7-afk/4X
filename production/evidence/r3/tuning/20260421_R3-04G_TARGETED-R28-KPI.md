# R3-04 KPI Evaluation (R28)

- Batch: `R3-04G-B001-R28`
- Profile: `difficulty-rebalance-a`
- Runs: `1000`
- Source CSV: `C:\test\4X\production\evidence\r3\tuning\20260421_R3-04G_TARGETED-R28.csv`
- KPI Pass: `9/13`

## Round Parameter Delta

- Bias (Easy/Standard/Hard): `0.28/0.13/-0.02`
- Init Scale (Easy/Standard/Hard): `1.14/1.03/0.96`
- Social/Trade Init Scale: `0.23`
- Victory Threshold (Easy/Standard/Hard): `86/84/84`
- Pressure Offset: `0.23`; Conflict Base/TurnScale: `0.390/0.013`
- Delta Scale/Noise Scale: `0.97/2.02`
- Early Suppression: `turn<= 7` with `positive*0.47`
- Shock: `chance=0.42`, `magnitude=+/-33`

## KPI Snapshot

| KPI | Actual | Target | Status |
|---|---:|---:|---|
| WIN_RATE_EASY | 0.7100 | [0.6500, 0.8000] | PASS |
| WIN_RATE_STANDARD | 0.3625 | [0.4500, 0.5500] | FAIL |
| WIN_RATE_HARD | 0.0300 | [0.2000, 0.3500] | FAIL |
| WIN_RATE_OVERALL | 0.3670 | [0.4000, 0.6000] | FAIL |
| AVG_END_TURN_OVERALL | 14.0470 | [14.0000, 20.0000] | PASS |
| AVG_END_TURN_STANDARD | 16.3725 | [15.0000, 19.0000] | PASS |
| ATTRITION_RATE_OVERALL | 0.1890 | [0.1000, 0.3500] | PASS |
| RESOURCE_CV_GOLDLEAF | 0.4064 | [0.1800, 0.4500] | PASS |
| RESOURCE_CV_FIREOIL | 0.2049 | [0.1500, 0.4000] | PASS |
| RESOURCE_CV_ARMS | 0.2851 | [0.2000, 0.5500] | PASS |
| VICTORY_SHARE_OVERALL | 0.3670 | [0.3500, 0.6500] | PASS |
| DEFEAT_SHARE_OVERALL | 0.4440 | [0.3500, 0.6500] | PASS |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.9210 | [0.0000, 0.7000] | FAIL |

## Endgame Distribution

| End Type | Count | Share |
|---|---:|---:|
| victory | 367 | 0.3670 |
| defeat | 444 | 0.4440 |
| timeout | 189 | 0.1890 |

- Top victory path: `energyliberation` (`338` wins, share `0.9210`)
