# R3-04 KPI Evaluation (R05)

- Batch: `R3-04B-B001-R05`
- Profile: `standard-recover`
- Runs: `1000`
- Source CSV: `C:\test\4X\production\evidence\r3\tuning\20260420_R3-04B_TARGETED-R05.csv`
- KPI Pass: `5/13`

## Round Parameter Delta

- Bias (Easy/Standard/Hard): `-0.01/0.08/-0.02`
- Init Scale (Easy/Standard/Hard): `0.86/1.00/0.95`
- Social/Trade Init Scale: `0.56`
- Victory Threshold (Easy/Standard/Hard): `94/90/83`
- Pressure Offset: `0.14`; Conflict Base/TurnScale: `0.370/0.012`
- Delta Scale/Noise Scale: `0.88/1.60`
- Early Suppression: `turn<= 8` with `positive*0.44`
- Shock: `chance=0.30`, `magnitude=+/-24`

## KPI Snapshot

| KPI | Actual | Target | Status |
|---|---:|---:|---|
| WIN_RATE_EASY | 0.0167 | [0.6500, 0.8000] | FAIL |
| WIN_RATE_STANDARD | 0.2000 | [0.4500, 0.5500] | FAIL |
| WIN_RATE_HARD | 0.0600 | [0.2000, 0.3500] | FAIL |
| WIN_RATE_OVERALL | 0.1030 | [0.4000, 0.6000] | FAIL |
| AVG_END_TURN_OVERALL | 14.4300 | [14.0000, 20.0000] | PASS |
| AVG_END_TURN_STANDARD | 16.6400 | [15.0000, 19.0000] | PASS |
| ATTRITION_RATE_OVERALL | 0.1850 | [0.1000, 0.3500] | PASS |
| RESOURCE_CV_GOLDLEAF | 0.3245 | [0.1800, 0.4500] | PASS |
| RESOURCE_CV_FIREOIL | 0.1498 | [0.1500, 0.4000] | FAIL |
| RESOURCE_CV_ARMS | 0.2061 | [0.2000, 0.5500] | PASS |
| VICTORY_SHARE_OVERALL | 0.1030 | [0.3500, 0.6500] | FAIL |
| DEFEAT_SHARE_OVERALL | 0.7120 | [0.3500, 0.6500] | FAIL |
| SINGLE_PATH_VICTORY_MONOPOLY | 1.0000 | [0.0000, 0.7000] | FAIL |

## Endgame Distribution

| End Type | Count | Share |
|---|---:|---:|
| victory | 103 | 0.1030 |
| defeat | 712 | 0.7120 |
| timeout | 185 | 0.1850 |

- Top victory path: `energyliberation` (`103` wins, share `1.0000`)
