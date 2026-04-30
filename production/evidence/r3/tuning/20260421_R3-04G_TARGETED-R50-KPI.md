# R3-04 KPI Evaluation (R50)

- Batch: `R3-04G-B008-R50`
- Profile: `r23-moderate-split-b`
- Runs: `1000`
- Source CSV: `C:\test\4X\production\evidence\r3\tuning\20260421_R3-04G_TARGETED-R50.csv`
- KPI Pass: `8/13`

## Round Parameter Delta

- Bias (Easy/Standard/Hard): `0.24/0.14/0.04`
- Init Scale (Easy/Standard/Hard): `1.10/1.05/1.00`
- Social/Trade Init Scale: `0.24`
- Victory Threshold (Easy/Standard/Hard): `86/83/82`
- Pressure Offset: `0.23`; Conflict Base/TurnScale: `0.390/0.013`
- Delta Scale/Noise Scale: `0.97/2.02`
- Early Suppression: `turn<= 7` with `positive*0.47`
- Shock: `chance=0.41`, `magnitude=+/-33`

## KPI Snapshot

| KPI | Actual | Target | Status |
|---|---:|---:|---|
| WIN_RATE_EASY | 0.5567 | [0.6500, 0.8000] | FAIL |
| WIN_RATE_STANDARD | 0.4425 | [0.4500, 0.5500] | FAIL |
| WIN_RATE_HARD | 0.1200 | [0.2000, 0.3500] | FAIL |
| WIN_RATE_OVERALL | 0.3800 | [0.4000, 0.6000] | FAIL |
| AVG_END_TURN_OVERALL | 15.0950 | [14.0000, 20.0000] | PASS |
| AVG_END_TURN_STANDARD | 15.8625 | [15.0000, 19.0000] | PASS |
| ATTRITION_RATE_OVERALL | 0.2350 | [0.1000, 0.3500] | PASS |
| RESOURCE_CV_GOLDLEAF | 0.3788 | [0.1800, 0.4500] | PASS |
| RESOURCE_CV_FIREOIL | 0.2157 | [0.1500, 0.4000] | PASS |
| RESOURCE_CV_ARMS | 0.2778 | [0.2000, 0.5500] | PASS |
| VICTORY_SHARE_OVERALL | 0.3800 | [0.3500, 0.6500] | PASS |
| DEFEAT_SHARE_OVERALL | 0.3850 | [0.3500, 0.6500] | PASS |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.8632 | [0.0000, 0.7000] | FAIL |

## Endgame Distribution

| End Type | Count | Share |
|---|---:|---:|
| victory | 380 | 0.3800 |
| defeat | 385 | 0.3850 |
| timeout | 235 | 0.2350 |

- Top victory path: `energyliberation` (`328` wins, share `0.8632`)
