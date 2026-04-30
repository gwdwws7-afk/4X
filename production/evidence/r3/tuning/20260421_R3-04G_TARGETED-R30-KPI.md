# R3-04 KPI Evaluation (R30)

- Batch: `R3-04G-B001-R30`
- Profile: `pace-defeat-balance`
- Runs: `1000`
- Source CSV: `C:\test\4X\production\evidence\r3\tuning\20260421_R3-04G_TARGETED-R30.csv`
- KPI Pass: `8/13`

## Round Parameter Delta

- Bias (Easy/Standard/Hard): `0.27/0.12/-0.01`
- Init Scale (Easy/Standard/Hard): `1.12/1.02/0.98`
- Social/Trade Init Scale: `0.22`
- Victory Threshold (Easy/Standard/Hard): `86/84/84`
- Pressure Offset: `0.22`; Conflict Base/TurnScale: `0.380/0.012`
- Delta Scale/Noise Scale: `0.95/2.05`
- Early Suppression: `turn<= 8` with `positive*0.44`
- Shock: `chance=0.43`, `magnitude=+/-34`

## KPI Snapshot

| KPI | Actual | Target | Status |
|---|---:|---:|---|
| WIN_RATE_EASY | 0.6900 | [0.6500, 0.8000] | PASS |
| WIN_RATE_STANDARD | 0.3650 | [0.4500, 0.5500] | FAIL |
| WIN_RATE_HARD | 0.0500 | [0.2000, 0.3500] | FAIL |
| WIN_RATE_OVERALL | 0.3680 | [0.4000, 0.6000] | FAIL |
| AVG_END_TURN_OVERALL | 13.8470 | [14.0000, 20.0000] | FAIL |
| AVG_END_TURN_STANDARD | 15.8850 | [15.0000, 19.0000] | PASS |
| ATTRITION_RATE_OVERALL | 0.1800 | [0.1000, 0.3500] | PASS |
| RESOURCE_CV_GOLDLEAF | 0.3839 | [0.1800, 0.4500] | PASS |
| RESOURCE_CV_FIREOIL | 0.2140 | [0.1500, 0.4000] | PASS |
| RESOURCE_CV_ARMS | 0.2933 | [0.2000, 0.5500] | PASS |
| VICTORY_SHARE_OVERALL | 0.3680 | [0.3500, 0.6500] | PASS |
| DEFEAT_SHARE_OVERALL | 0.4520 | [0.3500, 0.6500] | PASS |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.9212 | [0.0000, 0.7000] | FAIL |

## Endgame Distribution

| End Type | Count | Share |
|---|---:|---:|
| victory | 368 | 0.3680 |
| defeat | 452 | 0.4520 |
| timeout | 180 | 0.1800 |

- Top victory path: `energyliberation` (`339` wins, share `0.9212`)
