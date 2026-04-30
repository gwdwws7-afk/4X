# R3-04 KPI Evaluation (V01)

- Batch: `R3-05-B002-R85-V01`
- Profile: `r82-gate-stability-a-validation`
- Runs: `1000`
- Source CSV: `C:\test\4X\production\evidence\r3\validation\20260421_R3-05_VALIDATION-R85-V01.csv`
- KPI Pass: `11/13`

## Round Parameter Delta

- Bias (Easy/Standard/Hard): `0.26/0.15/0.05`
- Init Scale (Easy/Standard/Hard): `1.12/1.07/1.03`
- Social/Trade Init Scale: `0.16`
- Victory Threshold (Easy/Standard/Hard): `85/83/81`
- Pressure Offset: `0.25`; Conflict Base/TurnScale: `0.405/0.014`
- Delta Scale/Noise Scale: `0.95/2.00`
- Early Suppression: `turn<= 8` with `positive*0.46`
- Shock: `chance=0.40`, `magnitude=+/-32`

## KPI Snapshot

| KPI | Actual | Target | Status |
|---|---:|---:|---|
| WIN_RATE_EASY | 0.6600 | [0.6500, 0.8000] | PASS |
| WIN_RATE_STANDARD | 0.4875 | [0.4500, 0.5500] | PASS |
| WIN_RATE_HARD | 0.2233 | [0.2000, 0.3500] | PASS |
| WIN_RATE_OVERALL | 0.4600 | [0.4000, 0.6000] | PASS |
| AVG_END_TURN_OVERALL | 13.8990 | [14.0000, 20.0000] | FAIL |
| AVG_END_TURN_STANDARD | 15.3850 | [15.0000, 19.0000] | PASS |
| ATTRITION_RATE_OVERALL | 0.1930 | [0.1000, 0.3500] | PASS |
| RESOURCE_CV_GOLDLEAF | 0.3510 | [0.1800, 0.4500] | PASS |
| RESOURCE_CV_FIREOIL | 0.1846 | [0.1500, 0.4000] | PASS |
| RESOURCE_CV_ARMS | 0.2538 | [0.2000, 0.5500] | PASS |
| VICTORY_SHARE_OVERALL | 0.4600 | [0.3500, 0.6500] | PASS |
| DEFEAT_SHARE_OVERALL | 0.3470 | [0.3500, 0.6500] | FAIL |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.6696 | [0.0000, 0.7000] | PASS |

## Endgame Distribution

| End Type | Count | Share |
|---|---:|---:|
| victory | 460 | 0.4600 |
| defeat | 347 | 0.3470 |
| timeout | 193 | 0.1930 |

- Top victory path: `energyliberation` (`308` wins, share `0.6696`)
