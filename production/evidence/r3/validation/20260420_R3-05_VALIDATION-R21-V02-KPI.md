# R3-04 KPI Evaluation (V02)

- Batch: `R3-05-B002-R21-V02`
- Profile: `win-floor-guard-validation`
- Runs: `1000`
- Source CSV: `C:\test\4X\production\evidence\r3\validation\20260420_R3-05_VALIDATION-R21-V02.csv`
- KPI Pass: `10/13`

## Round Parameter Delta

- Bias (Easy/Standard/Hard): `0.20/0.11/0.01`
- Init Scale (Easy/Standard/Hard): `1.06/1.01/0.98`
- Social/Trade Init Scale: `0.30`
- Victory Threshold (Easy/Standard/Hard): `88/85/82`
- Pressure Offset: `0.17`; Conflict Base/TurnScale: `0.360/0.012`
- Delta Scale/Noise Scale: `0.94/1.95`
- Early Suppression: `turn<= 7` with `positive*0.52`
- Shock: `chance=0.38`, `magnitude=+/-30`

## KPI Snapshot

| KPI | Actual | Target | Status |
|---|---:|---:|---|
| WIN_RATE_EASY | 0.6100 | [0.6500, 0.8000] | FAIL |
| WIN_RATE_STANDARD | 0.4500 | [0.4500, 0.5500] | PASS |
| WIN_RATE_HARD | 0.1600 | [0.2000, 0.3500] | FAIL |
| WIN_RATE_OVERALL | 0.4110 | [0.4000, 0.6000] | PASS |
| AVG_END_TURN_OVERALL | 15.0360 | [14.0000, 20.0000] | PASS |
| AVG_END_TURN_STANDARD | 16.3725 | [15.0000, 19.0000] | PASS |
| ATTRITION_RATE_OVERALL | 0.2180 | [0.1000, 0.3500] | PASS |
| RESOURCE_CV_GOLDLEAF | 0.3228 | [0.1800, 0.4500] | PASS |
| RESOURCE_CV_FIREOIL | 0.1885 | [0.1500, 0.4000] | PASS |
| RESOURCE_CV_ARMS | 0.2393 | [0.2000, 0.5500] | PASS |
| VICTORY_SHARE_OVERALL | 0.4110 | [0.3500, 0.6500] | PASS |
| DEFEAT_SHARE_OVERALL | 0.3710 | [0.3500, 0.6500] | PASS |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.9659 | [0.0000, 0.7000] | FAIL |

## Endgame Distribution

| End Type | Count | Share |
|---|---:|---:|
| victory | 411 | 0.4110 |
| defeat | 371 | 0.3710 |
| timeout | 218 | 0.2180 |

- Top victory path: `energyliberation` (`397` wins, share `0.9659`)
