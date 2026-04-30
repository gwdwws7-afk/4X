# R3-04 KPI Evaluation (R06)

- Batch: `R3-04B-B001-R06`
- Profile: `path-diversify`
- Runs: `1000`
- Source CSV: `C:\test\4X\production\evidence\r3\tuning\20260420_R3-04B_TARGETED-R06.csv`
- KPI Pass: `5/13`

## Round Parameter Delta

- Bias (Easy/Standard/Hard): `0.00/0.10/-0.01`
- Init Scale (Easy/Standard/Hard): `0.84/1.02/0.96`
- Social/Trade Init Scale: `0.50`
- Victory Threshold (Easy/Standard/Hard): `95/91/84`
- Pressure Offset: `0.18`; Conflict Base/TurnScale: `0.400/0.013`
- Delta Scale/Noise Scale: `0.86/1.75`
- Early Suppression: `turn<= 10` with `positive*0.40`
- Shock: `chance=0.34`, `magnitude=+/-26`

## KPI Snapshot

| KPI | Actual | Target | Status |
|---|---:|---:|---|
| WIN_RATE_EASY | 0.0100 | [0.6500, 0.8000] | FAIL |
| WIN_RATE_STANDARD | 0.1475 | [0.4500, 0.5500] | FAIL |
| WIN_RATE_HARD | 0.0467 | [0.2000, 0.3500] | FAIL |
| WIN_RATE_OVERALL | 0.0760 | [0.4000, 0.6000] | FAIL |
| AVG_END_TURN_OVERALL | 13.8150 | [14.0000, 20.0000] | FAIL |
| AVG_END_TURN_STANDARD | 16.2050 | [15.0000, 19.0000] | PASS |
| ATTRITION_RATE_OVERALL | 0.1470 | [0.1000, 0.3500] | PASS |
| RESOURCE_CV_GOLDLEAF | 0.3848 | [0.1800, 0.4500] | PASS |
| RESOURCE_CV_FIREOIL | 0.1746 | [0.1500, 0.4000] | PASS |
| RESOURCE_CV_ARMS | 0.2328 | [0.2000, 0.5500] | PASS |
| VICTORY_SHARE_OVERALL | 0.0760 | [0.3500, 0.6500] | FAIL |
| DEFEAT_SHARE_OVERALL | 0.7770 | [0.3500, 0.6500] | FAIL |
| SINGLE_PATH_VICTORY_MONOPOLY | 1.0000 | [0.0000, 0.7000] | FAIL |

## Endgame Distribution

| End Type | Count | Share |
|---|---:|---:|
| victory | 76 | 0.0760 |
| defeat | 777 | 0.7770 |
| timeout | 147 | 0.1470 |

- Top victory path: `energyliberation` (`76` wins, share `1.0000`)
