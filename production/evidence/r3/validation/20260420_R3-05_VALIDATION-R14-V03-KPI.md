# R3-04 KPI Evaluation (V03)

- Batch: `R3-05-B002-R14-V03`
- Profile: `duration-volatility-validation`
- Runs: `1000`
- Source CSV: `C:\test\4X\production\evidence\r3\validation\20260420_R3-05_VALIDATION-R14-V03.csv`
- KPI Pass: `7/13`

## Round Parameter Delta

- Bias (Easy/Standard/Hard): `0.14/0.06/-0.04`
- Init Scale (Easy/Standard/Hard): `0.98/0.96/0.93`
- Social/Trade Init Scale: `0.44`
- Victory Threshold (Easy/Standard/Hard): `92/90/84`
- Pressure Offset: `0.20`; Conflict Base/TurnScale: `0.370/0.013`
- Delta Scale/Noise Scale: `0.92/1.85`
- Early Suppression: `turn<= 8` with `positive*0.46`
- Shock: `chance=0.34`, `magnitude=+/-28`

## KPI Snapshot

| KPI | Actual | Target | Status |
|---|---:|---:|---|
| WIN_RATE_EASY | 0.3300 | [0.6500, 0.8000] | FAIL |
| WIN_RATE_STANDARD | 0.0525 | [0.4500, 0.5500] | FAIL |
| WIN_RATE_HARD | 0.0300 | [0.2000, 0.3500] | FAIL |
| WIN_RATE_OVERALL | 0.1290 | [0.4000, 0.6000] | FAIL |
| AVG_END_TURN_OVERALL | 14.7100 | [14.0000, 20.0000] | PASS |
| AVG_END_TURN_STANDARD | 17.7050 | [15.0000, 19.0000] | PASS |
| ATTRITION_RATE_OVERALL | 0.2690 | [0.1000, 0.3500] | PASS |
| RESOURCE_CV_GOLDLEAF | 0.3833 | [0.1800, 0.4500] | PASS |
| RESOURCE_CV_FIREOIL | 0.1814 | [0.1500, 0.4000] | PASS |
| RESOURCE_CV_ARMS | 0.2453 | [0.2000, 0.5500] | PASS |
| VICTORY_SHARE_OVERALL | 0.1290 | [0.3500, 0.6500] | FAIL |
| DEFEAT_SHARE_OVERALL | 0.6020 | [0.3500, 0.6500] | PASS |
| SINGLE_PATH_VICTORY_MONOPOLY | 1.0000 | [0.0000, 0.7000] | FAIL |

## Endgame Distribution

| End Type | Count | Share |
|---|---:|---:|
| victory | 129 | 0.1290 |
| defeat | 602 | 0.6020 |
| timeout | 269 | 0.2690 |

- Top victory path: `energyliberation` (`129` wins, share `1.0000`)
