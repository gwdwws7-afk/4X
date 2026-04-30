# R3-04 KPI Evaluation (R52)

- Batch: `R3-04G-B009-R52`
- Profile: `r23-easyup-hardcollapse-a`
- Runs: `1000`
- Source CSV: `C:\test\4X\production\evidence\r3\tuning\20260421_R3-04G_TARGETED-R52.csv`
- KPI Pass: `9/13`

## Round Parameter Delta

- Bias (Easy/Standard/Hard): `0.24/0.14/0.06`
- Init Scale (Easy/Standard/Hard): `1.11/1.05/1.05`
- Social/Trade Init Scale: `0.24`
- Victory Threshold (Easy/Standard/Hard): `86/83/79`
- Pressure Offset: `0.22`; Conflict Base/TurnScale: `0.380/0.012`
- Delta Scale/Noise Scale: `0.98/2.00`
- Early Suppression: `turn<= 7` with `positive*0.48`
- Shock: `chance=0.40`, `magnitude=+/-32`

## KPI Snapshot

| KPI | Actual | Target | Status |
|---|---:|---:|---|
| WIN_RATE_EASY | 0.6600 | [0.6500, 0.8000] | PASS |
| WIN_RATE_STANDARD | 0.4450 | [0.4500, 0.5500] | FAIL |
| WIN_RATE_HARD | 0.5833 | [0.2000, 0.3500] | FAIL |
| WIN_RATE_OVERALL | 0.5510 | [0.4000, 0.6000] | PASS |
| AVG_END_TURN_OVERALL | 13.4380 | [14.0000, 20.0000] | FAIL |
| AVG_END_TURN_STANDARD | 15.7625 | [15.0000, 19.0000] | PASS |
| ATTRITION_RATE_OVERALL | 0.1780 | [0.1000, 0.3500] | PASS |
| RESOURCE_CV_GOLDLEAF | 0.3097 | [0.1800, 0.4500] | PASS |
| RESOURCE_CV_FIREOIL | 0.1779 | [0.1500, 0.4000] | PASS |
| RESOURCE_CV_ARMS | 0.2464 | [0.2000, 0.5500] | PASS |
| VICTORY_SHARE_OVERALL | 0.5510 | [0.3500, 0.6500] | PASS |
| DEFEAT_SHARE_OVERALL | 0.2710 | [0.3500, 0.6500] | FAIL |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.6824 | [0.0000, 0.7000] | PASS |

## Endgame Distribution

| End Type | Count | Share |
|---|---:|---:|
| victory | 551 | 0.5510 |
| defeat | 271 | 0.2710 |
| timeout | 178 | 0.1780 |

- Top victory path: `energyliberation` (`376` wins, share `0.6824`)
