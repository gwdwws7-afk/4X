# R3-04 KPI Evaluation (R36)

- Batch: `R3-04G-B003-R36`
- Profile: `standard-protect-balance`
- Runs: `1000`
- Source CSV: `C:\test\4X\production\evidence\r3\tuning\20260421_R3-04G_TARGETED-R36.csv`
- KPI Pass: `9/13`

## Round Parameter Delta

- Bias (Easy/Standard/Hard): `0.23/0.15/-0.01`
- Init Scale (Easy/Standard/Hard): `1.14/1.06/0.96`
- Social/Trade Init Scale: `0.22`
- Victory Threshold (Easy/Standard/Hard): `85/82/84`
- Pressure Offset: `0.23`; Conflict Base/TurnScale: `0.380/0.012`
- Delta Scale/Noise Scale: `0.97/2.00`
- Early Suppression: `turn<= 6` with `positive*0.49`
- Shock: `chance=0.40`, `magnitude=+/-32`

## KPI Snapshot

| KPI | Actual | Target | Status |
|---|---:|---:|---|
| WIN_RATE_EASY | 0.6700 | [0.6500, 0.8000] | PASS |
| WIN_RATE_STANDARD | 0.5450 | [0.4500, 0.5500] | PASS |
| WIN_RATE_HARD | 0.0400 | [0.2000, 0.3500] | FAIL |
| WIN_RATE_OVERALL | 0.4310 | [0.4000, 0.6000] | PASS |
| AVG_END_TURN_OVERALL | 13.5630 | [14.0000, 20.0000] | FAIL |
| AVG_END_TURN_STANDARD | 14.6575 | [15.0000, 19.0000] | FAIL |
| ATTRITION_RATE_OVERALL | 0.1900 | [0.1000, 0.3500] | PASS |
| RESOURCE_CV_GOLDLEAF | 0.3570 | [0.1800, 0.4500] | PASS |
| RESOURCE_CV_FIREOIL | 0.1921 | [0.1500, 0.4000] | PASS |
| RESOURCE_CV_ARMS | 0.2537 | [0.2000, 0.5500] | PASS |
| VICTORY_SHARE_OVERALL | 0.4310 | [0.3500, 0.6500] | PASS |
| DEFEAT_SHARE_OVERALL | 0.3790 | [0.3500, 0.6500] | PASS |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.7657 | [0.0000, 0.7000] | FAIL |

## Endgame Distribution

| End Type | Count | Share |
|---|---:|---:|
| victory | 431 | 0.4310 |
| defeat | 379 | 0.3790 |
| timeout | 190 | 0.1900 |

- Top victory path: `energyliberation` (`330` wins, share `0.7657`)
