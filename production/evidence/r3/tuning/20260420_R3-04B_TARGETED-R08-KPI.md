# R3-04 KPI Evaluation (R08)

- Batch: `R3-04B-B002-R08`
- Profile: `standard-push`
- Runs: `1000`
- Source CSV: `C:\test\4X\production\evidence\r3\tuning\20260420_R3-04B_TARGETED-R08.csv`
- KPI Pass: `6/13`

## Round Parameter Delta

- Bias (Easy/Standard/Hard): `0.03/0.07/0.00`
- Init Scale (Easy/Standard/Hard): `0.88/0.98/1.00`
- Social/Trade Init Scale: `0.58`
- Victory Threshold (Easy/Standard/Hard): `92/88/79`
- Pressure Offset: `0.11`; Conflict Base/TurnScale: `0.330/0.011`
- Delta Scale/Noise Scale: `0.94/1.40`
- Early Suppression: `turn<= 6` with `positive*0.58`
- Shock: `chance=0.20`, `magnitude=+/-16`

## KPI Snapshot

| KPI | Actual | Target | Status |
|---|---:|---:|---|
| WIN_RATE_EASY | 0.1000 | [0.6500, 0.8000] | FAIL |
| WIN_RATE_STANDARD | 0.2000 | [0.4500, 0.5500] | FAIL |
| WIN_RATE_HARD | 0.3067 | [0.2000, 0.3500] | PASS |
| WIN_RATE_OVERALL | 0.2020 | [0.4000, 0.6000] | FAIL |
| AVG_END_TURN_OVERALL | 15.6870 | [14.0000, 20.0000] | PASS |
| AVG_END_TURN_STANDARD | 18.1700 | [15.0000, 19.0000] | PASS |
| ATTRITION_RATE_OVERALL | 0.3330 | [0.1000, 0.3500] | PASS |
| RESOURCE_CV_GOLDLEAF | 0.2089 | [0.1800, 0.4500] | PASS |
| RESOURCE_CV_FIREOIL | 0.1174 | [0.1500, 0.4000] | FAIL |
| RESOURCE_CV_ARMS | 0.1403 | [0.2000, 0.5500] | FAIL |
| VICTORY_SHARE_OVERALL | 0.2020 | [0.3500, 0.6500] | FAIL |
| DEFEAT_SHARE_OVERALL | 0.4650 | [0.3500, 0.6500] | PASS |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.7574 | [0.0000, 0.7000] | FAIL |

## Endgame Distribution

| End Type | Count | Share |
|---|---:|---:|
| victory | 202 | 0.2020 |
| defeat | 465 | 0.4650 |
| timeout | 333 | 0.3330 |

- Top victory path: `energyliberation` (`153` wins, share `0.7574`)
