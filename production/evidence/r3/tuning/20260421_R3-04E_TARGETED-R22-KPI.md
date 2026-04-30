# R3-04 KPI Evaluation (R22)

- Batch: `R3-04E-B001-R22`
- Profile: `path-diversify-axis-push`
- Runs: `1000`
- Source CSV: `C:\test\4X\production\evidence\r3\tuning\20260421_R3-04E_TARGETED-R22.csv`
- KPI Pass: `9/13`

## Round Parameter Delta

- Bias (Easy/Standard/Hard): `0.19/0.12/0.05`
- Init Scale (Easy/Standard/Hard): `1.05/1.03/1.04`
- Social/Trade Init Scale: `0.22`
- Victory Threshold (Easy/Standard/Hard): `88/84/80`
- Pressure Offset: `0.23`; Conflict Base/TurnScale: `0.390/0.013`
- Delta Scale/Noise Scale: `0.95/2.05`
- Early Suppression: `turn<= 7` with `positive*0.48`
- Shock: `chance=0.44`, `magnitude=+/-34`

## KPI Snapshot

| KPI | Actual | Target | Status |
|---|---:|---:|---|
| WIN_RATE_EASY | 0.3933 | [0.6500, 0.8000] | FAIL |
| WIN_RATE_STANDARD | 0.3350 | [0.4500, 0.5500] | FAIL |
| WIN_RATE_HARD | 0.6067 | [0.2000, 0.3500] | FAIL |
| WIN_RATE_OVERALL | 0.4340 | [0.4000, 0.6000] | PASS |
| AVG_END_TURN_OVERALL | 14.3010 | [14.0000, 20.0000] | PASS |
| AVG_END_TURN_STANDARD | 16.0325 | [15.0000, 19.0000] | PASS |
| ATTRITION_RATE_OVERALL | 0.2560 | [0.1000, 0.3500] | PASS |
| RESOURCE_CV_GOLDLEAF | 0.3651 | [0.1800, 0.4500] | PASS |
| RESOURCE_CV_FIREOIL | 0.2065 | [0.1500, 0.4000] | PASS |
| RESOURCE_CV_ARMS | 0.2854 | [0.2000, 0.5500] | PASS |
| VICTORY_SHARE_OVERALL | 0.4340 | [0.3500, 0.6500] | PASS |
| DEFEAT_SHARE_OVERALL | 0.3100 | [0.3500, 0.6500] | FAIL |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.5991 | [0.0000, 0.7000] | PASS |

## Endgame Distribution

| End Type | Count | Share |
|---|---:|---:|
| victory | 434 | 0.4340 |
| defeat | 310 | 0.3100 |
| timeout | 256 | 0.2560 |

- Top victory path: `energyliberation` (`260` wins, share `0.5991`)
