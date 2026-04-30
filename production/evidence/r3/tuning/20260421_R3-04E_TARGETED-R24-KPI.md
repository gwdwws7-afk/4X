# R3-04 KPI Evaluation (R24)

- Batch: `R3-04E-B001-R24`
- Profile: `hybrid-floor-vs-monopoly`
- Runs: `1000`
- Source CSV: `C:\test\4X\production\evidence\r3\tuning\20260421_R3-04E_TARGETED-R24.csv`
- KPI Pass: `9/13`

## Round Parameter Delta

- Bias (Easy/Standard/Hard): `0.21/0.13/0.06`
- Init Scale (Easy/Standard/Hard): `1.07/1.04/1.06`
- Social/Trade Init Scale: `0.21`
- Victory Threshold (Easy/Standard/Hard): `88/84/80`
- Pressure Offset: `0.24`; Conflict Base/TurnScale: `0.400/0.013`
- Delta Scale/Noise Scale: `0.96/2.08`
- Early Suppression: `turn<= 7` with `positive*0.46`
- Shock: `chance=0.45`, `magnitude=+/-35`

## KPI Snapshot

| KPI | Actual | Target | Status |
|---|---:|---:|---|
| WIN_RATE_EASY | 0.4467 | [0.6500, 0.8000] | FAIL |
| WIN_RATE_STANDARD | 0.3400 | [0.4500, 0.5500] | FAIL |
| WIN_RATE_HARD | 0.6467 | [0.2000, 0.3500] | FAIL |
| WIN_RATE_OVERALL | 0.4640 | [0.4000, 0.6000] | PASS |
| AVG_END_TURN_OVERALL | 14.5890 | [14.0000, 20.0000] | PASS |
| AVG_END_TURN_STANDARD | 16.7450 | [15.0000, 19.0000] | PASS |
| ATTRITION_RATE_OVERALL | 0.2460 | [0.1000, 0.3500] | PASS |
| RESOURCE_CV_GOLDLEAF | 0.3769 | [0.1800, 0.4500] | PASS |
| RESOURCE_CV_FIREOIL | 0.2079 | [0.1500, 0.4000] | PASS |
| RESOURCE_CV_ARMS | 0.3036 | [0.2000, 0.5500] | PASS |
| VICTORY_SHARE_OVERALL | 0.4640 | [0.3500, 0.6500] | PASS |
| DEFEAT_SHARE_OVERALL | 0.2900 | [0.3500, 0.6500] | FAIL |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.5991 | [0.0000, 0.7000] | PASS |

## Endgame Distribution

| End Type | Count | Share |
|---|---:|---:|
| victory | 464 | 0.4640 |
| defeat | 290 | 0.2900 |
| timeout | 246 | 0.2460 |

- Top victory path: `energyliberation` (`278` wins, share `0.5991`)
