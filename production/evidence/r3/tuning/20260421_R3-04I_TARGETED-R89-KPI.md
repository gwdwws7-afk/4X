# R3-04 KPI Evaluation (R89)

- Batch: `R3-04I-B002-R89`
- Profile: `r82-stability-pace-b`
- Runs: `1000`
- Source CSV: `C:\test\4X\production\evidence\r3\tuning\20260421_R3-04I_TARGETED-R89.csv`
- KPI Pass: `10/13`

## Round Parameter Delta

- Bias (Easy/Standard/Hard): `0.24/0.16/0.05`
- Init Scale (Easy/Standard/Hard): `1.10/1.08/1.02`
- Social/Trade Init Scale: `0.16`
- Victory Threshold (Easy/Standard/Hard): `86/83/81`
- Pressure Offset: `0.25`; Conflict Base/TurnScale: `0.398/0.013`
- Delta Scale/Noise Scale: `0.96/2.00`
- Early Suppression: `turn<= 8` with `positive*0.45`
- Shock: `chance=0.40`, `magnitude=+/-32`

## KPI Snapshot

| KPI | Actual | Target | Status |
|---|---:|---:|---|
| WIN_RATE_EASY | 0.5667 | [0.6500, 0.8000] | FAIL |
| WIN_RATE_STANDARD | 0.5050 | [0.4500, 0.5500] | PASS |
| WIN_RATE_HARD | 0.2367 | [0.2000, 0.3500] | PASS |
| WIN_RATE_OVERALL | 0.4430 | [0.4000, 0.6000] | PASS |
| AVG_END_TURN_OVERALL | 13.9070 | [14.0000, 20.0000] | FAIL |
| AVG_END_TURN_STANDARD | 14.9650 | [15.0000, 19.0000] | FAIL |
| ATTRITION_RATE_OVERALL | 0.2020 | [0.1000, 0.3500] | PASS |
| RESOURCE_CV_GOLDLEAF | 0.3509 | [0.1800, 0.4500] | PASS |
| RESOURCE_CV_FIREOIL | 0.1811 | [0.1500, 0.4000] | PASS |
| RESOURCE_CV_ARMS | 0.2539 | [0.2000, 0.5500] | PASS |
| VICTORY_SHARE_OVERALL | 0.4430 | [0.3500, 0.6500] | PASS |
| DEFEAT_SHARE_OVERALL | 0.3550 | [0.3500, 0.6500] | PASS |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.6998 | [0.0000, 0.7000] | PASS |

## Endgame Distribution

| End Type | Count | Share |
|---|---:|---:|
| victory | 443 | 0.4430 |
| defeat | 355 | 0.3550 |
| timeout | 202 | 0.2020 |

- Top victory path: `energyliberation` (`310` wins, share `0.6998`)
