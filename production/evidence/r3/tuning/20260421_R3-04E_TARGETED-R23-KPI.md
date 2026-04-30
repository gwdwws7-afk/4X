# R3-04 KPI Evaluation (R23)

- Batch: `R3-04E-B001-R23`
- Profile: `win-floor-recover`
- Runs: `1000`
- Source CSV: `C:\test\4X\production\evidence\r3\tuning\20260421_R3-04E_TARGETED-R23.csv`
- KPI Pass: `9/13`

## Round Parameter Delta

- Bias (Easy/Standard/Hard): `0.22/0.14/0.07`
- Init Scale (Easy/Standard/Hard): `1.08/1.05/1.08`
- Social/Trade Init Scale: `0.24`
- Victory Threshold (Easy/Standard/Hard): `87/83/79`
- Pressure Offset: `0.22`; Conflict Base/TurnScale: `0.380/0.012`
- Delta Scale/Noise Scale: `0.98/2.00`
- Early Suppression: `turn<= 6` with `positive*0.50`
- Shock: `chance=0.40`, `magnitude=+/-32`

## KPI Snapshot

| KPI | Actual | Target | Status |
|---|---:|---:|---|
| WIN_RATE_EASY | 0.6067 | [0.6500, 0.8000] | FAIL |
| WIN_RATE_STANDARD | 0.4675 | [0.4500, 0.5500] | PASS |
| WIN_RATE_HARD | 0.6467 | [0.2000, 0.3500] | FAIL |
| WIN_RATE_OVERALL | 0.5630 | [0.4000, 0.6000] | PASS |
| AVG_END_TURN_OVERALL | 13.8410 | [14.0000, 20.0000] | FAIL |
| AVG_END_TURN_STANDARD | 15.8025 | [15.0000, 19.0000] | PASS |
| ATTRITION_RATE_OVERALL | 0.1940 | [0.1000, 0.3500] | PASS |
| RESOURCE_CV_GOLDLEAF | 0.3108 | [0.1800, 0.4500] | PASS |
| RESOURCE_CV_FIREOIL | 0.1705 | [0.1500, 0.4000] | PASS |
| RESOURCE_CV_ARMS | 0.2647 | [0.2000, 0.5500] | PASS |
| VICTORY_SHARE_OVERALL | 0.5630 | [0.3500, 0.6500] | PASS |
| DEFEAT_SHARE_OVERALL | 0.2430 | [0.3500, 0.6500] | FAIL |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.6696 | [0.0000, 0.7000] | PASS |

## Endgame Distribution

| End Type | Count | Share |
|---|---:|---:|
| victory | 563 | 0.5630 |
| defeat | 243 | 0.2430 |
| timeout | 194 | 0.1940 |

- Top victory path: `energyliberation` (`377` wins, share `0.6696`)
