# R3-04 KPI Evaluation (R74)

- Batch: `R3-04H-B001-R74`
- Profile: `r70-hard-floor-lift-b`
- Runs: `1000`
- Source CSV: `C:\test\4X\production\evidence\r3\tuning\20260421_R3-04H_TARGETED-R74.csv`
- KPI Pass: `12/13`

## Round Parameter Delta

- Bias (Easy/Standard/Hard): `0.24/0.14/0.05`
- Init Scale (Easy/Standard/Hard): `1.11/1.05/1.04`
- Social/Trade Init Scale: `0.16`
- Victory Threshold (Easy/Standard/Hard): `85/83/81`
- Pressure Offset: `0.24`; Conflict Base/TurnScale: `0.400/0.013`
- Delta Scale/Noise Scale: `0.96/2.05`
- Early Suppression: `turn<= 7` with `positive*0.47`
- Shock: `chance=0.41`, `magnitude=+/-33`

## KPI Snapshot

| KPI | Actual | Target | Status |
|---|---:|---:|---|
| WIN_RATE_EASY | 0.6733 | [0.6500, 0.8000] | PASS |
| WIN_RATE_STANDARD | 0.3875 | [0.4500, 0.5500] | FAIL |
| WIN_RATE_HARD | 0.2500 | [0.2000, 0.3500] | PASS |
| WIN_RATE_OVERALL | 0.4320 | [0.4000, 0.6000] | PASS |
| AVG_END_TURN_OVERALL | 14.3150 | [14.0000, 20.0000] | PASS |
| AVG_END_TURN_STANDARD | 16.3650 | [15.0000, 19.0000] | PASS |
| ATTRITION_RATE_OVERALL | 0.1940 | [0.1000, 0.3500] | PASS |
| RESOURCE_CV_GOLDLEAF | 0.3672 | [0.1800, 0.4500] | PASS |
| RESOURCE_CV_FIREOIL | 0.2008 | [0.1500, 0.4000] | PASS |
| RESOURCE_CV_ARMS | 0.2776 | [0.2000, 0.5500] | PASS |
| VICTORY_SHARE_OVERALL | 0.4320 | [0.3500, 0.6500] | PASS |
| DEFEAT_SHARE_OVERALL | 0.3740 | [0.3500, 0.6500] | PASS |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.6759 | [0.0000, 0.7000] | PASS |

## Endgame Distribution

| End Type | Count | Share |
|---|---:|---:|
| victory | 432 | 0.4320 |
| defeat | 374 | 0.3740 |
| timeout | 194 | 0.1940 |

- Top victory path: `energyliberation` (`292` wins, share `0.6759`)
