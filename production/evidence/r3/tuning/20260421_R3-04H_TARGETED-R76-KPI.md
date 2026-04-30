# R3-04 KPI Evaluation (R76)

- Batch: `R3-04H-B002-R76`
- Profile: `r70-baseline-control`
- Runs: `1000`
- Source CSV: `C:\test\4X\production\evidence\r3\tuning\20260421_R3-04H_TARGETED-R76.csv`
- KPI Pass: `10/13`

## Round Parameter Delta

- Bias (Easy/Standard/Hard): `0.24/0.14/0.05`
- Init Scale (Easy/Standard/Hard): `1.11/1.05/1.04`
- Social/Trade Init Scale: `0.16`
- Victory Threshold (Easy/Standard/Hard): `85/83/82`
- Pressure Offset: `0.24`; Conflict Base/TurnScale: `0.400/0.013`
- Delta Scale/Noise Scale: `0.96/2.05`
- Early Suppression: `turn<= 7` with `positive*0.47`
- Shock: `chance=0.41`, `magnitude=+/-33`

## KPI Snapshot

| KPI | Actual | Target | Status |
|---|---:|---:|---|
| WIN_RATE_EASY | 0.6300 | [0.6500, 0.8000] | FAIL |
| WIN_RATE_STANDARD | 0.4650 | [0.4500, 0.5500] | PASS |
| WIN_RATE_HARD | 0.1400 | [0.2000, 0.3500] | FAIL |
| WIN_RATE_OVERALL | 0.4170 | [0.4000, 0.6000] | PASS |
| AVG_END_TURN_OVERALL | 14.5690 | [14.0000, 20.0000] | PASS |
| AVG_END_TURN_STANDARD | 15.7900 | [15.0000, 19.0000] | PASS |
| ATTRITION_RATE_OVERALL | 0.2110 | [0.1000, 0.3500] | PASS |
| RESOURCE_CV_GOLDLEAF | 0.3667 | [0.1800, 0.4500] | PASS |
| RESOURCE_CV_FIREOIL | 0.1894 | [0.1500, 0.4000] | PASS |
| RESOURCE_CV_ARMS | 0.2838 | [0.2000, 0.5500] | PASS |
| VICTORY_SHARE_OVERALL | 0.4170 | [0.3500, 0.6500] | PASS |
| DEFEAT_SHARE_OVERALL | 0.3720 | [0.3500, 0.6500] | PASS |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.7146 | [0.0000, 0.7000] | FAIL |

## Endgame Distribution

| End Type | Count | Share |
|---|---:|---:|
| victory | 417 | 0.4170 |
| defeat | 372 | 0.3720 |
| timeout | 211 | 0.2110 |

- Top victory path: `energyliberation` (`298` wins, share `0.7146`)
