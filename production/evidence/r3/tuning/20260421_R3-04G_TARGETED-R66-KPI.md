# R3-04 KPI Evaluation (R66)

- Batch: `R3-04G-B013-R66`
- Profile: `r58-easy-th85-hard-comp-c`
- Runs: `1000`
- Source CSV: `C:\test\4X\production\evidence\r3\tuning\20260421_R3-04G_TARGETED-R66.csv`
- KPI Pass: `10/13`

## Round Parameter Delta

- Bias (Easy/Standard/Hard): `0.24/0.14/0.05`
- Init Scale (Easy/Standard/Hard): `1.13/1.05/1.05`
- Social/Trade Init Scale: `0.16`
- Victory Threshold (Easy/Standard/Hard): `85/83/82`
- Pressure Offset: `0.24`; Conflict Base/TurnScale: `0.410/0.013`
- Delta Scale/Noise Scale: `0.96/2.06`
- Early Suppression: `turn<= 7` with `positive*0.47`
- Shock: `chance=0.41`, `magnitude=+/-33`

## KPI Snapshot

| KPI | Actual | Target | Status |
|---|---:|---:|---|
| WIN_RATE_EASY | 0.6300 | [0.6500, 0.8000] | FAIL |
| WIN_RATE_STANDARD | 0.4400 | [0.4500, 0.5500] | FAIL |
| WIN_RATE_HARD | 0.1567 | [0.2000, 0.3500] | FAIL |
| WIN_RATE_OVERALL | 0.4120 | [0.4000, 0.6000] | PASS |
| AVG_END_TURN_OVERALL | 14.4230 | [14.0000, 20.0000] | PASS |
| AVG_END_TURN_STANDARD | 15.1950 | [15.0000, 19.0000] | PASS |
| ATTRITION_RATE_OVERALL | 0.2030 | [0.1000, 0.3500] | PASS |
| RESOURCE_CV_GOLDLEAF | 0.3737 | [0.1800, 0.4500] | PASS |
| RESOURCE_CV_FIREOIL | 0.1996 | [0.1500, 0.4000] | PASS |
| RESOURCE_CV_ARMS | 0.2832 | [0.2000, 0.5500] | PASS |
| VICTORY_SHARE_OVERALL | 0.4120 | [0.3500, 0.6500] | PASS |
| DEFEAT_SHARE_OVERALL | 0.3850 | [0.3500, 0.6500] | PASS |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.6990 | [0.0000, 0.7000] | PASS |

## Endgame Distribution

| End Type | Count | Share |
|---|---:|---:|
| victory | 412 | 0.4120 |
| defeat | 385 | 0.3850 |
| timeout | 203 | 0.2030 |

- Top victory path: `energyliberation` (`288` wins, share `0.6990`)
