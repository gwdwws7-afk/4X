# R3-04 KPI Evaluation (R18)

- Batch: `R3-04D-B001-R18`
- Profile: `anti-monopoly`
- Runs: `1000`
- Source CSV: `C:\test\4X\production\evidence\r3\tuning\20260420_R3-04D_TARGETED-R18.csv`
- KPI Pass: `7/13`

## Round Parameter Delta

- Bias (Easy/Standard/Hard): `0.10/0.08/0.03`
- Init Scale (Easy/Standard/Hard): `0.98/0.98/0.98`
- Social/Trade Init Scale: `0.28`
- Victory Threshold (Easy/Standard/Hard): `88/85/82`
- Pressure Offset: `0.20`; Conflict Base/TurnScale: `0.380/0.013`
- Delta Scale/Noise Scale: `0.92/2.05`
- Early Suppression: `turn<= 8` with `positive*0.46`
- Shock: `chance=0.38`, `magnitude=+/-32`

## KPI Snapshot

| KPI | Actual | Target | Status |
|---|---:|---:|---|
| WIN_RATE_EASY | 0.2867 | [0.6500, 0.8000] | FAIL |
| WIN_RATE_STANDARD | 0.2750 | [0.4500, 0.5500] | FAIL |
| WIN_RATE_HARD | 0.1333 | [0.2000, 0.3500] | FAIL |
| WIN_RATE_OVERALL | 0.2360 | [0.4000, 0.6000] | FAIL |
| AVG_END_TURN_OVERALL | 14.8490 | [14.0000, 20.0000] | PASS |
| AVG_END_TURN_STANDARD | 15.4075 | [15.0000, 19.0000] | PASS |
| ATTRITION_RATE_OVERALL | 0.2180 | [0.1000, 0.3500] | PASS |
| RESOURCE_CV_GOLDLEAF | 0.4428 | [0.1800, 0.4500] | PASS |
| RESOURCE_CV_FIREOIL | 0.2170 | [0.1500, 0.4000] | PASS |
| RESOURCE_CV_ARMS | 0.2925 | [0.2000, 0.5500] | PASS |
| VICTORY_SHARE_OVERALL | 0.2360 | [0.3500, 0.6500] | FAIL |
| DEFEAT_SHARE_OVERALL | 0.5460 | [0.3500, 0.6500] | PASS |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.9788 | [0.0000, 0.7000] | FAIL |

## Endgame Distribution

| End Type | Count | Share |
|---|---:|---:|
| victory | 236 | 0.2360 |
| defeat | 546 | 0.5460 |
| timeout | 218 | 0.2180 |

- Top victory path: `energyliberation` (`231` wins, share `0.9788`)
