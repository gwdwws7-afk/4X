# R3-04 KPI Evaluation (V01)

- Batch: `R3-05-B002-R99-V01`
- Profile: `r82-early-suppress-c-validation`
- Runs: `1000`
- Source CSV: `C:\test\4X\production\evidence\r3\validation\20260421_R3-05_VALIDATION-R99-V01.csv`
- KPI Pass: `11/13`

## Round Parameter Delta

- Bias (Easy/Standard/Hard): `0.26/0.15/0.05`
- Init Scale (Easy/Standard/Hard): `1.13/1.07/1.03`
- Social/Trade Init Scale: `0.16`
- Victory Threshold (Easy/Standard/Hard): `85/83/81`
- Pressure Offset: `0.24`; Conflict Base/TurnScale: `0.402/0.013`
- Delta Scale/Noise Scale: `0.96/2.02`
- Early Suppression: `turn<= 10` with `positive*0.39`
- Shock: `chance=0.38`, `magnitude=+/-30`

## KPI Snapshot

| KPI | Actual | Target | Status |
|---|---:|---:|---|
| WIN_RATE_EASY | 0.6867 | [0.6500, 0.8000] | PASS |
| WIN_RATE_STANDARD | 0.5100 | [0.4500, 0.5500] | PASS |
| WIN_RATE_HARD | 0.2367 | [0.2000, 0.3500] | PASS |
| WIN_RATE_OVERALL | 0.4810 | [0.4000, 0.6000] | PASS |
| AVG_END_TURN_OVERALL | 13.4530 | [14.0000, 20.0000] | FAIL |
| AVG_END_TURN_STANDARD | 15.0775 | [15.0000, 19.0000] | PASS |
| ATTRITION_RATE_OVERALL | 0.1760 | [0.1000, 0.3500] | PASS |
| RESOURCE_CV_GOLDLEAF | 0.3689 | [0.1800, 0.4500] | PASS |
| RESOURCE_CV_FIREOIL | 0.1767 | [0.1500, 0.4000] | PASS |
| RESOURCE_CV_ARMS | 0.2442 | [0.2000, 0.5500] | PASS |
| VICTORY_SHARE_OVERALL | 0.4810 | [0.3500, 0.6500] | PASS |
| DEFEAT_SHARE_OVERALL | 0.3430 | [0.3500, 0.6500] | FAIL |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.6590 | [0.0000, 0.7000] | PASS |

## Endgame Distribution

| End Type | Count | Share |
|---|---:|---:|
| victory | 481 | 0.4810 |
| defeat | 343 | 0.3430 |
| timeout | 176 | 0.1760 |

- Top victory path: `energyliberation` (`317` wins, share `0.6590`)
