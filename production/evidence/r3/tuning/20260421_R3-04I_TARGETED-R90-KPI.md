# R3-04 KPI Evaluation (R90)

- Batch: `R3-04I-B002-R90`
- Profile: `r82-stability-pace-c`
- Runs: `1000`
- Source CSV: `C:\test\4X\production\evidence\r3\tuning\20260421_R3-04I_TARGETED-R90.csv`
- KPI Pass: `10/13`

## Round Parameter Delta

- Bias (Easy/Standard/Hard): `0.24/0.15/0.05`
- Init Scale (Easy/Standard/Hard): `1.09/1.07/1.02`
- Social/Trade Init Scale: `0.15`
- Victory Threshold (Easy/Standard/Hard): `86/84/81`
- Pressure Offset: `0.25`; Conflict Base/TurnScale: `0.400/0.013`
- Delta Scale/Noise Scale: `0.95/2.00`
- Early Suppression: `turn<= 8` with `positive*0.45`
- Shock: `chance=0.39`, `magnitude=+/-31`

## KPI Snapshot

| KPI | Actual | Target | Status |
|---|---:|---:|---|
| WIN_RATE_EASY | 0.5800 | [0.6500, 0.8000] | FAIL |
| WIN_RATE_STANDARD | 0.4100 | [0.4500, 0.5500] | FAIL |
| WIN_RATE_HARD | 0.2100 | [0.2000, 0.3500] | PASS |
| WIN_RATE_OVERALL | 0.4010 | [0.4000, 0.6000] | PASS |
| AVG_END_TURN_OVERALL | 14.4120 | [14.0000, 20.0000] | PASS |
| AVG_END_TURN_STANDARD | 16.2775 | [15.0000, 19.0000] | PASS |
| ATTRITION_RATE_OVERALL | 0.2240 | [0.1000, 0.3500] | PASS |
| RESOURCE_CV_GOLDLEAF | 0.3606 | [0.1800, 0.4500] | PASS |
| RESOURCE_CV_FIREOIL | 0.1823 | [0.1500, 0.4000] | PASS |
| RESOURCE_CV_ARMS | 0.2523 | [0.2000, 0.5500] | PASS |
| VICTORY_SHARE_OVERALL | 0.4010 | [0.3500, 0.6500] | PASS |
| DEFEAT_SHARE_OVERALL | 0.3750 | [0.3500, 0.6500] | PASS |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.7506 | [0.0000, 0.7000] | FAIL |

## Endgame Distribution

| End Type | Count | Share |
|---|---:|---:|
| victory | 401 | 0.4010 |
| defeat | 375 | 0.3750 |
| timeout | 224 | 0.2240 |

- Top victory path: `energyliberation` (`301` wins, share `0.7506`)
