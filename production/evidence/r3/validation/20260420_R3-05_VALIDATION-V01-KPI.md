# R3-04 KPI Evaluation (V01)

- Batch: `R3-05-B001-V01`
- Profile: `target-seek-validation`
- Runs: `1000`
- Source CSV: `C:\test\4X\production\evidence\r3\validation\20260420_R3-05_VALIDATION-V01.csv`
- KPI Pass: `4/13`

## Round Parameter Delta

- Bias (Easy/Standard/Hard): `0.12/0.02/-0.01`
- Init Scale (Easy/Standard/Hard): `0.95/0.90/1.00`
- Social/Trade Init Scale: `0.70`
- Victory Threshold (Easy/Standard/Hard): `84/85/78`
- Pressure Offset: `0.10`; Conflict Base/TurnScale: `0.310/0.010`
- Delta Scale/Noise Scale: `0.95/1.25`
- Early Suppression: `turn<= 5` with `positive*0.62`
- Shock: `chance=0.15`, `magnitude=+/-14`

## KPI Snapshot

| KPI | Actual | Target | Status |
|---|---:|---:|---|
| WIN_RATE_EASY | 0.9267 | [0.6500, 0.8000] | FAIL |
| WIN_RATE_STANDARD | 0.2925 | [0.4500, 0.5500] | FAIL |
| WIN_RATE_HARD | 0.3133 | [0.2000, 0.3500] | PASS |
| WIN_RATE_OVERALL | 0.4890 | [0.4000, 0.6000] | PASS |
| AVG_END_TURN_OVERALL | 9.8450 | [14.0000, 20.0000] | FAIL |
| AVG_END_TURN_STANDARD | 13.4650 | [15.0000, 19.0000] | FAIL |
| ATTRITION_RATE_OVERALL | 0.0700 | [0.1000, 0.3500] | FAIL |
| RESOURCE_CV_GOLDLEAF | 0.1400 | [0.1800, 0.4500] | FAIL |
| RESOURCE_CV_FIREOIL | 0.0805 | [0.1500, 0.4000] | FAIL |
| RESOURCE_CV_ARMS | 0.1020 | [0.2000, 0.5500] | FAIL |
| VICTORY_SHARE_OVERALL | 0.4890 | [0.3500, 0.6500] | PASS |
| DEFEAT_SHARE_OVERALL | 0.4410 | [0.3500, 0.6500] | PASS |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.8896 | [0.0000, 0.7000] | FAIL |

## Endgame Distribution

| End Type | Count | Share |
|---|---:|---:|
| victory | 489 | 0.4890 |
| defeat | 441 | 0.4410 |
| timeout | 70 | 0.0700 |

- Top victory path: `energyliberation` (`435` wins, share `0.8896`)
