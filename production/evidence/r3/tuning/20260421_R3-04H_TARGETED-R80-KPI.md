# R3-04 KPI Evaluation (R80)

- Batch: `R3-04H-B003-R80`
- Profile: `r77-easy-up-hard-clamp-b`
- Runs: `1000`
- Source CSV: `C:\test\4X\production\evidence\r3\tuning\20260421_R3-04H_TARGETED-R80.csv`
- KPI Pass: `11/13`

## Round Parameter Delta

- Bias (Easy/Standard/Hard): `0.26/0.14/0.05`
- Init Scale (Easy/Standard/Hard): `1.12/1.05/1.04`
- Social/Trade Init Scale: `0.16`
- Victory Threshold (Easy/Standard/Hard): `84/83/81`
- Pressure Offset: `0.24`; Conflict Base/TurnScale: `0.400/0.013`
- Delta Scale/Noise Scale: `0.96/2.05`
- Early Suppression: `turn<= 7` with `positive*0.47`
- Shock: `chance=0.41`, `magnitude=+/-33`

## KPI Snapshot

| KPI | Actual | Target | Status |
|---|---:|---:|---|
| WIN_RATE_EASY | 0.6933 | [0.6500, 0.8000] | PASS |
| WIN_RATE_STANDARD | 0.4650 | [0.4500, 0.5500] | PASS |
| WIN_RATE_HARD | 0.2200 | [0.2000, 0.3500] | PASS |
| WIN_RATE_OVERALL | 0.4600 | [0.4000, 0.6000] | PASS |
| AVG_END_TURN_OVERALL | 13.8920 | [14.0000, 20.0000] | FAIL |
| AVG_END_TURN_STANDARD | 15.7900 | [15.0000, 19.0000] | PASS |
| ATTRITION_RATE_OVERALL | 0.1910 | [0.1000, 0.3500] | PASS |
| RESOURCE_CV_GOLDLEAF | 0.3457 | [0.1800, 0.4500] | PASS |
| RESOURCE_CV_FIREOIL | 0.1832 | [0.1500, 0.4000] | PASS |
| RESOURCE_CV_ARMS | 0.2760 | [0.2000, 0.5500] | PASS |
| VICTORY_SHARE_OVERALL | 0.4600 | [0.3500, 0.6500] | PASS |
| DEFEAT_SHARE_OVERALL | 0.3490 | [0.3500, 0.6500] | FAIL |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.6370 | [0.0000, 0.7000] | PASS |

## Endgame Distribution

| End Type | Count | Share |
|---|---:|---:|
| victory | 460 | 0.4600 |
| defeat | 349 | 0.3490 |
| timeout | 191 | 0.1910 |

- Top victory path: `energyliberation` (`293` wins, share `0.6370`)
