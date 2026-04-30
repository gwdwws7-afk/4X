# R3-04 KPI Evaluation (V02)

- Batch: `R3-05-B002-R88-V02`
- Profile: `r82-stability-pace-a-validation`
- Runs: `1000`
- Source CSV: `C:\test\4X\production\evidence\r3\validation\20260421_R3-05_VALIDATION-R88-V02.csv`
- KPI Pass: `11/13`

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
| WIN_RATE_EASY | 0.5567 | [0.6500, 0.8000] | FAIL |
| WIN_RATE_STANDARD | 0.4550 | [0.4500, 0.5500] | PASS |
| WIN_RATE_HARD | 0.2800 | [0.2000, 0.3500] | PASS |
| WIN_RATE_OVERALL | 0.4330 | [0.4000, 0.6000] | PASS |
| AVG_END_TURN_OVERALL | 14.5460 | [14.0000, 20.0000] | PASS |
| AVG_END_TURN_STANDARD | 15.8375 | [15.0000, 19.0000] | PASS |
| ATTRITION_RATE_OVERALL | 0.2390 | [0.1000, 0.3500] | PASS |
| RESOURCE_CV_GOLDLEAF | 0.3731 | [0.1800, 0.4500] | PASS |
| RESOURCE_CV_FIREOIL | 0.1918 | [0.1500, 0.4000] | PASS |
| RESOURCE_CV_ARMS | 0.2570 | [0.2000, 0.5500] | PASS |
| VICTORY_SHARE_OVERALL | 0.4330 | [0.3500, 0.6500] | PASS |
| DEFEAT_SHARE_OVERALL | 0.3280 | [0.3500, 0.6500] | FAIL |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.6975 | [0.0000, 0.7000] | PASS |

## Endgame Distribution

| End Type | Count | Share |
|---|---:|---:|
| victory | 433 | 0.4330 |
| defeat | 328 | 0.3280 |
| timeout | 239 | 0.2390 |

- Top victory path: `energyliberation` (`302` wins, share `0.6975`)
