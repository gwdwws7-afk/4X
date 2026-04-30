# R3-04 KPI Evaluation (R49)

- Batch: `R3-04G-B008-R49`
- Profile: `r23-moderate-split-a`
- Runs: `1000`
- Source CSV: `C:\test\4X\production\evidence\r3\tuning\20260421_R3-04G_TARGETED-R49.csv`
- KPI Pass: `11/13`

## Round Parameter Delta

- Bias (Easy/Standard/Hard): `0.24/0.14/0.05`
- Init Scale (Easy/Standard/Hard): `1.11/1.05/1.02`
- Social/Trade Init Scale: `0.24`
- Victory Threshold (Easy/Standard/Hard): `86/83/82`
- Pressure Offset: `0.22`; Conflict Base/TurnScale: `0.380/0.012`
- Delta Scale/Noise Scale: `0.97/2.00`
- Early Suppression: `turn<= 6` with `positive*0.48`
- Shock: `chance=0.40`, `magnitude=+/-32`

## KPI Snapshot

| KPI | Actual | Target | Status |
|---|---:|---:|---|
| WIN_RATE_EASY | 0.6633 | [0.6500, 0.8000] | PASS |
| WIN_RATE_STANDARD | 0.4500 | [0.4500, 0.5500] | PASS |
| WIN_RATE_HARD | 0.1600 | [0.2000, 0.3500] | FAIL |
| WIN_RATE_OVERALL | 0.4270 | [0.4000, 0.6000] | PASS |
| AVG_END_TURN_OVERALL | 14.6540 | [14.0000, 20.0000] | PASS |
| AVG_END_TURN_STANDARD | 15.7600 | [15.0000, 19.0000] | PASS |
| ATTRITION_RATE_OVERALL | 0.1980 | [0.1000, 0.3500] | PASS |
| RESOURCE_CV_GOLDLEAF | 0.3437 | [0.1800, 0.4500] | PASS |
| RESOURCE_CV_FIREOIL | 0.1975 | [0.1500, 0.4000] | PASS |
| RESOURCE_CV_ARMS | 0.2603 | [0.2000, 0.5500] | PASS |
| VICTORY_SHARE_OVERALL | 0.4270 | [0.3500, 0.6500] | PASS |
| DEFEAT_SHARE_OVERALL | 0.3750 | [0.3500, 0.6500] | PASS |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.8970 | [0.0000, 0.7000] | FAIL |

## Endgame Distribution

| End Type | Count | Share |
|---|---:|---:|
| victory | 427 | 0.4270 |
| defeat | 375 | 0.3750 |
| timeout | 198 | 0.1980 |

- Top victory path: `energyliberation` (`383` wins, share `0.8970`)
