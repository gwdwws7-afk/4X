# R3-04 KPI Evaluation (R62)

- Batch: `R3-04G-B012-R62`
- Profile: `r58-micro-easyup-b`
- Runs: `1000`
- Source CSV: `C:\test\4X\production\evidence\r3\tuning\20260421_R3-04G_TARGETED-R62.csv`
- KPI Pass: `10/13`

## Round Parameter Delta

- Bias (Easy/Standard/Hard): `0.25/0.14/0.05`
- Init Scale (Easy/Standard/Hard): `1.13/1.05/1.02`
- Social/Trade Init Scale: `0.18`
- Victory Threshold (Easy/Standard/Hard): `86/83/82`
- Pressure Offset: `0.24`; Conflict Base/TurnScale: `0.400/0.013`
- Delta Scale/Noise Scale: `0.96/2.05`
- Early Suppression: `turn<= 7` with `positive*0.47`
- Shock: `chance=0.41`, `magnitude=+/-33`

## KPI Snapshot

| KPI | Actual | Target | Status |
|---|---:|---:|---|
| WIN_RATE_EASY | 0.6100 | [0.6500, 0.8000] | FAIL |
| WIN_RATE_STANDARD | 0.4500 | [0.4500, 0.5500] | PASS |
| WIN_RATE_HARD | 0.1400 | [0.2000, 0.3500] | FAIL |
| WIN_RATE_OVERALL | 0.4050 | [0.4000, 0.6000] | PASS |
| AVG_END_TURN_OVERALL | 14.6340 | [14.0000, 20.0000] | PASS |
| AVG_END_TURN_STANDARD | 15.6400 | [15.0000, 19.0000] | PASS |
| ATTRITION_RATE_OVERALL | 0.2190 | [0.1000, 0.3500] | PASS |
| RESOURCE_CV_GOLDLEAF | 0.3743 | [0.1800, 0.4500] | PASS |
| RESOURCE_CV_FIREOIL | 0.2003 | [0.1500, 0.4000] | PASS |
| RESOURCE_CV_ARMS | 0.2758 | [0.2000, 0.5500] | PASS |
| VICTORY_SHARE_OVERALL | 0.4050 | [0.3500, 0.6500] | PASS |
| DEFEAT_SHARE_OVERALL | 0.3760 | [0.3500, 0.6500] | PASS |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.7481 | [0.0000, 0.7000] | FAIL |

## Endgame Distribution

| End Type | Count | Share |
|---|---:|---:|
| victory | 405 | 0.4050 |
| defeat | 376 | 0.3760 |
| timeout | 219 | 0.2190 |

- Top victory path: `energyliberation` (`303` wins, share `0.7481`)
