# R3-04 KPI Evaluation (R84)

- Batch: `R3-04H-B004-R84`
- Profile: `r79-pace-backpull-c`
- Runs: `1000`
- Source CSV: `C:\test\4X\production\evidence\r3\tuning\20260421_R3-04H_TARGETED-R84.csv`
- KPI Pass: `12/13`

## Round Parameter Delta

- Bias (Easy/Standard/Hard): `0.26/0.15/0.05`
- Init Scale (Easy/Standard/Hard): `1.12/1.06/1.03`
- Social/Trade Init Scale: `0.16`
- Victory Threshold (Easy/Standard/Hard): `85/83/81`
- Pressure Offset: `0.24`; Conflict Base/TurnScale: `0.390/0.013`
- Delta Scale/Noise Scale: `0.96/2.05`
- Early Suppression: `turn<= 7` with `positive*0.47`
- Shock: `chance=0.41`, `magnitude=+/-33`

## KPI Snapshot

| KPI | Actual | Target | Status |
|---|---:|---:|---|
| WIN_RATE_EASY | 0.6800 | [0.6500, 0.8000] | PASS |
| WIN_RATE_STANDARD | 0.4925 | [0.4500, 0.5500] | PASS |
| WIN_RATE_HARD | 0.2100 | [0.2000, 0.3500] | PASS |
| WIN_RATE_OVERALL | 0.4640 | [0.4000, 0.6000] | PASS |
| AVG_END_TURN_OVERALL | 14.0170 | [14.0000, 20.0000] | PASS |
| AVG_END_TURN_STANDARD | 15.5350 | [15.0000, 19.0000] | PASS |
| ATTRITION_RATE_OVERALL | 0.1930 | [0.1000, 0.3500] | PASS |
| RESOURCE_CV_GOLDLEAF | 0.3420 | [0.1800, 0.4500] | PASS |
| RESOURCE_CV_FIREOIL | 0.1827 | [0.1500, 0.4000] | PASS |
| RESOURCE_CV_ARMS | 0.2706 | [0.2000, 0.5500] | PASS |
| VICTORY_SHARE_OVERALL | 0.4640 | [0.3500, 0.6500] | PASS |
| DEFEAT_SHARE_OVERALL | 0.3430 | [0.3500, 0.6500] | FAIL |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.6832 | [0.0000, 0.7000] | PASS |

## Endgame Distribution

| End Type | Count | Share |
|---|---:|---:|
| victory | 464 | 0.4640 |
| defeat | 343 | 0.3430 |
| timeout | 193 | 0.1930 |

- Top victory path: `energyliberation` (`317` wins, share `0.6832`)
