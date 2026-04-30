# R3-04 KPI Evaluation (R63)

- Batch: `R3-04G-B012-R63`
- Profile: `r58-micro-easyup-c`
- Runs: `1000`
- Source CSV: `C:\test\4X\production\evidence\r3\tuning\20260421_R3-04G_TARGETED-R63.csv`
- KPI Pass: `9/13`

## Round Parameter Delta

- Bias (Easy/Standard/Hard): `0.26/0.14/0.06`
- Init Scale (Easy/Standard/Hard): `1.13/1.05/1.02`
- Social/Trade Init Scale: `0.16`
- Victory Threshold (Easy/Standard/Hard): `86/83/82`
- Pressure Offset: `0.24`; Conflict Base/TurnScale: `0.410/0.013`
- Delta Scale/Noise Scale: `0.96/2.06`
- Early Suppression: `turn<= 8` with `positive*0.46`
- Shock: `chance=0.41`, `magnitude=+/-33`

## KPI Snapshot

| KPI | Actual | Target | Status |
|---|---:|---:|---|
| WIN_RATE_EASY | 0.6067 | [0.6500, 0.8000] | FAIL |
| WIN_RATE_STANDARD | 0.4400 | [0.4500, 0.5500] | FAIL |
| WIN_RATE_HARD | 0.1400 | [0.2000, 0.3500] | FAIL |
| WIN_RATE_OVERALL | 0.4000 | [0.4000, 0.6000] | PASS |
| AVG_END_TURN_OVERALL | 14.2490 | [14.0000, 20.0000] | PASS |
| AVG_END_TURN_STANDARD | 15.1675 | [15.0000, 19.0000] | PASS |
| ATTRITION_RATE_OVERALL | 0.2060 | [0.1000, 0.3500] | PASS |
| RESOURCE_CV_GOLDLEAF | 0.3657 | [0.1800, 0.4500] | PASS |
| RESOURCE_CV_FIREOIL | 0.1979 | [0.1500, 0.4000] | PASS |
| RESOURCE_CV_ARMS | 0.2858 | [0.2000, 0.5500] | PASS |
| VICTORY_SHARE_OVERALL | 0.4000 | [0.3500, 0.6500] | PASS |
| DEFEAT_SHARE_OVERALL | 0.3940 | [0.3500, 0.6500] | PASS |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.7150 | [0.0000, 0.7000] | FAIL |

## Endgame Distribution

| End Type | Count | Share |
|---|---:|---:|
| victory | 400 | 0.4000 |
| defeat | 394 | 0.3940 |
| timeout | 206 | 0.2060 |

- Top victory path: `energyliberation` (`286` wins, share `0.7150`)
