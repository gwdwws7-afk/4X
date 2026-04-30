# R3-04 KPI Evaluation (R71)

- Batch: `R3-04G-B015-R71`
- Profile: `r64-clone-hard-edge-b`
- Runs: `1000`
- Source CSV: `C:\test\4X\production\evidence\r3\tuning\20260421_R3-04G_TARGETED-R71.csv`
- KPI Pass: `9/13`

## Round Parameter Delta

- Bias (Easy/Standard/Hard): `0.24/0.14/0.05`
- Init Scale (Easy/Standard/Hard): `1.11/1.05/1.05`
- Social/Trade Init Scale: `0.16`
- Victory Threshold (Easy/Standard/Hard): `85/83/82`
- Pressure Offset: `0.24`; Conflict Base/TurnScale: `0.405/0.013`
- Delta Scale/Noise Scale: `0.96/2.05`
- Early Suppression: `turn<= 7` with `positive*0.47`
- Shock: `chance=0.41`, `magnitude=+/-33`

## KPI Snapshot

| KPI | Actual | Target | Status |
|---|---:|---:|---|
| WIN_RATE_EASY | 0.6367 | [0.6500, 0.8000] | FAIL |
| WIN_RATE_STANDARD | 0.4425 | [0.4500, 0.5500] | FAIL |
| WIN_RATE_HARD | 0.1500 | [0.2000, 0.3500] | FAIL |
| WIN_RATE_OVERALL | 0.4130 | [0.4000, 0.6000] | PASS |
| AVG_END_TURN_OVERALL | 14.8240 | [14.0000, 20.0000] | PASS |
| AVG_END_TURN_STANDARD | 15.8500 | [15.0000, 19.0000] | PASS |
| ATTRITION_RATE_OVERALL | 0.2090 | [0.1000, 0.3500] | PASS |
| RESOURCE_CV_GOLDLEAF | 0.3825 | [0.1800, 0.4500] | PASS |
| RESOURCE_CV_FIREOIL | 0.2028 | [0.1500, 0.4000] | PASS |
| RESOURCE_CV_ARMS | 0.2806 | [0.2000, 0.5500] | PASS |
| VICTORY_SHARE_OVERALL | 0.4130 | [0.3500, 0.6500] | PASS |
| DEFEAT_SHARE_OVERALL | 0.3780 | [0.3500, 0.6500] | PASS |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.7312 | [0.0000, 0.7000] | FAIL |

## Endgame Distribution

| End Type | Count | Share |
|---|---:|---:|
| victory | 413 | 0.4130 |
| defeat | 378 | 0.3780 |
| timeout | 209 | 0.2090 |

- Top victory path: `energyliberation` (`302` wins, share `0.7312`)
