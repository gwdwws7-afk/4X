# R3-04 KPI Evaluation (R07)

- Batch: `R3-04B-B002-R07`
- Profile: `balanced-pacing`
- Runs: `1000`
- Source CSV: `C:\test\4X\production\evidence\r3\tuning\20260420_R3-04B_TARGETED-R07.csv`
- KPI Pass: `6/13`

## Round Parameter Delta

- Bias (Easy/Standard/Hard): `0.05/0.04/-0.01`
- Init Scale (Easy/Standard/Hard): `0.90/0.93/0.98`
- Social/Trade Init Scale: `0.60`
- Victory Threshold (Easy/Standard/Hard): `90/90/80`
- Pressure Offset: `0.12`; Conflict Base/TurnScale: `0.340/0.011`
- Delta Scale/Noise Scale: `0.92/1.45`
- Early Suppression: `turn<= 7` with `positive*0.55`
- Shock: `chance=0.22`, `magnitude=+/-18`

## KPI Snapshot

| KPI | Actual | Target | Status |
|---|---:|---:|---|
| WIN_RATE_EASY | 0.2133 | [0.6500, 0.8000] | FAIL |
| WIN_RATE_STANDARD | 0.0775 | [0.4500, 0.5500] | FAIL |
| WIN_RATE_HARD | 0.2800 | [0.2000, 0.3500] | PASS |
| WIN_RATE_OVERALL | 0.1790 | [0.4000, 0.6000] | FAIL |
| AVG_END_TURN_OVERALL | 14.6540 | [14.0000, 20.0000] | PASS |
| AVG_END_TURN_STANDARD | 16.8000 | [15.0000, 19.0000] | PASS |
| ATTRITION_RATE_OVERALL | 0.2570 | [0.1000, 0.3500] | PASS |
| RESOURCE_CV_GOLDLEAF | 0.2389 | [0.1800, 0.4500] | PASS |
| RESOURCE_CV_FIREOIL | 0.1150 | [0.1500, 0.4000] | FAIL |
| RESOURCE_CV_ARMS | 0.1454 | [0.2000, 0.5500] | FAIL |
| VICTORY_SHARE_OVERALL | 0.1790 | [0.3500, 0.6500] | FAIL |
| DEFEAT_SHARE_OVERALL | 0.5640 | [0.3500, 0.6500] | PASS |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.7486 | [0.0000, 0.7000] | FAIL |

## Endgame Distribution

| End Type | Count | Share |
|---|---:|---:|
| victory | 179 | 0.1790 |
| defeat | 564 | 0.5640 |
| timeout | 257 | 0.2570 |

- Top victory path: `energyliberation` (`134` wins, share `0.7486`)
