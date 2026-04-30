# R3-04 KPI Evaluation (R39)

- Batch: `R3-04G-B004-R39`
- Profile: `anti-energy-rebalance-c`
- Runs: `1000`
- Source CSV: `C:\test\4X\production\evidence\r3\tuning\20260421_R3-04G_TARGETED-R39.csv`
- KPI Pass: `7/13`

## Round Parameter Delta

- Bias (Easy/Standard/Hard): `0.22/0.13/0.02`
- Init Scale (Easy/Standard/Hard): `1.12/1.04/1.00`
- Social/Trade Init Scale: `0.20`
- Victory Threshold (Easy/Standard/Hard): `86/84/83`
- Pressure Offset: `0.23`; Conflict Base/TurnScale: `0.390/0.012`
- Delta Scale/Noise Scale: `0.97/2.00`
- Early Suppression: `turn<= 7` with `positive*0.47`
- Shock: `chance=0.40`, `magnitude=+/-32`

## KPI Snapshot

| KPI | Actual | Target | Status |
|---|---:|---:|---|
| WIN_RATE_EASY | 0.5500 | [0.6500, 0.8000] | FAIL |
| WIN_RATE_STANDARD | 0.3550 | [0.4500, 0.5500] | FAIL |
| WIN_RATE_HARD | 0.0767 | [0.2000, 0.3500] | FAIL |
| WIN_RATE_OVERALL | 0.3300 | [0.4000, 0.6000] | FAIL |
| AVG_END_TURN_OVERALL | 15.4050 | [14.0000, 20.0000] | PASS |
| AVG_END_TURN_STANDARD | 16.3850 | [15.0000, 19.0000] | PASS |
| ATTRITION_RATE_OVERALL | 0.2300 | [0.1000, 0.3500] | PASS |
| RESOURCE_CV_GOLDLEAF | 0.3973 | [0.1800, 0.4500] | PASS |
| RESOURCE_CV_FIREOIL | 0.2133 | [0.1500, 0.4000] | PASS |
| RESOURCE_CV_ARMS | 0.2756 | [0.2000, 0.5500] | PASS |
| VICTORY_SHARE_OVERALL | 0.3300 | [0.3500, 0.6500] | FAIL |
| DEFEAT_SHARE_OVERALL | 0.4400 | [0.3500, 0.6500] | PASS |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.9121 | [0.0000, 0.7000] | FAIL |

## Endgame Distribution

| End Type | Count | Share |
|---|---:|---:|
| victory | 330 | 0.3300 |
| defeat | 440 | 0.4400 |
| timeout | 230 | 0.2300 |

- Top victory path: `energyliberation` (`301` wins, share `0.9121`)
