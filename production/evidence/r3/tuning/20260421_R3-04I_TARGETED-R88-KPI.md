# R3-04 KPI Evaluation (R88)

- Batch: `R3-04I-B002-R88`
- Profile: `r82-stability-pace-a`
- Runs: `1000`
- Source CSV: `C:\test\4X\production\evidence\r3\tuning\20260421_R3-04I_TARGETED-R88.csv`
- KPI Pass: `9/13`

## Round Parameter Delta

- Bias (Easy/Standard/Hard): `0.25/0.15/0.05`
- Init Scale (Easy/Standard/Hard): `1.10/1.07/1.02`
- Social/Trade Init Scale: `0.16`
- Victory Threshold (Easy/Standard/Hard): `86/83/81`
- Pressure Offset: `0.25`; Conflict Base/TurnScale: `0.398/0.013`
- Delta Scale/Noise Scale: `0.96/2.03`
- Early Suppression: `turn<= 8` with `positive*0.45`
- Shock: `chance=0.40`, `magnitude=+/-32`

## KPI Snapshot

| KPI | Actual | Target | Status |
|---|---:|---:|---|
| WIN_RATE_EASY | 0.5967 | [0.6500, 0.8000] | FAIL |
| WIN_RATE_STANDARD | 0.5000 | [0.4500, 0.5500] | PASS |
| WIN_RATE_HARD | 0.2433 | [0.2000, 0.3500] | PASS |
| WIN_RATE_OVERALL | 0.4520 | [0.4000, 0.6000] | PASS |
| AVG_END_TURN_OVERALL | 13.6730 | [14.0000, 20.0000] | FAIL |
| AVG_END_TURN_STANDARD | 14.8100 | [15.0000, 19.0000] | FAIL |
| ATTRITION_RATE_OVERALL | 0.1870 | [0.1000, 0.3500] | PASS |
| RESOURCE_CV_GOLDLEAF | 0.3563 | [0.1800, 0.4500] | PASS |
| RESOURCE_CV_FIREOIL | 0.1800 | [0.1500, 0.4000] | PASS |
| RESOURCE_CV_ARMS | 0.2563 | [0.2000, 0.5500] | PASS |
| VICTORY_SHARE_OVERALL | 0.4520 | [0.3500, 0.6500] | PASS |
| DEFEAT_SHARE_OVERALL | 0.3610 | [0.3500, 0.6500] | PASS |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.7058 | [0.0000, 0.7000] | FAIL |

## Endgame Distribution

| End Type | Count | Share |
|---|---:|---:|
| victory | 452 | 0.4520 |
| defeat | 361 | 0.3610 |
| timeout | 187 | 0.1870 |

- Top victory path: `energyliberation` (`319` wins, share `0.7058`)
