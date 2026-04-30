# R3-04 KPI Evaluation (R57)

- Batch: `R3-04G-B010-R57`
- Profile: `axis-diversify-hard-clamp-c`
- Runs: `1000`
- Source CSV: `C:\test\4X\production\evidence\r3\tuning\20260421_R3-04G_TARGETED-R57.csv`
- KPI Pass: `9/13`

## Round Parameter Delta

- Bias (Easy/Standard/Hard): `0.25/0.14/0.04`
- Init Scale (Easy/Standard/Hard): `1.12/1.05/1.00`
- Social/Trade Init Scale: `0.24`
- Victory Threshold (Easy/Standard/Hard): `86/83/81`
- Pressure Offset: `0.21`; Conflict Base/TurnScale: `0.370/0.012`
- Delta Scale/Noise Scale: `0.97/2.02`
- Early Suppression: `turn<= 7` with `positive*0.47`
- Shock: `chance=0.41`, `magnitude=+/-33`

## KPI Snapshot

| KPI | Actual | Target | Status |
|---|---:|---:|---|
| WIN_RATE_EASY | 0.7067 | [0.6500, 0.8000] | PASS |
| WIN_RATE_STANDARD | 0.5375 | [0.4500, 0.5500] | PASS |
| WIN_RATE_HARD | 0.2200 | [0.2000, 0.3500] | PASS |
| WIN_RATE_OVERALL | 0.4930 | [0.4000, 0.6000] | PASS |
| AVG_END_TURN_OVERALL | 13.4590 | [14.0000, 20.0000] | FAIL |
| AVG_END_TURN_STANDARD | 14.4750 | [15.0000, 19.0000] | FAIL |
| ATTRITION_RATE_OVERALL | 0.1940 | [0.1000, 0.3500] | PASS |
| RESOURCE_CV_GOLDLEAF | 0.3261 | [0.1800, 0.4500] | PASS |
| RESOURCE_CV_FIREOIL | 0.1946 | [0.1500, 0.4000] | PASS |
| RESOURCE_CV_ARMS | 0.2574 | [0.2000, 0.5500] | PASS |
| VICTORY_SHARE_OVERALL | 0.4930 | [0.3500, 0.6500] | PASS |
| DEFEAT_SHARE_OVERALL | 0.3130 | [0.3500, 0.6500] | FAIL |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.7911 | [0.0000, 0.7000] | FAIL |

## Endgame Distribution

| End Type | Count | Share |
|---|---:|---:|
| victory | 493 | 0.4930 |
| defeat | 313 | 0.3130 |
| timeout | 194 | 0.1940 |

- Top victory path: `energyliberation` (`390` wins, share `0.7911`)
