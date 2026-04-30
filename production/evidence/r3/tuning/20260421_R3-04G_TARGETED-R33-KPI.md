# R3-04 KPI Evaluation (R33)

- Batch: `R3-04G-B002-R33`
- Profile: `r23-guided-diversity-guard`
- Runs: `1000`
- Source CSV: `C:\test\4X\production\evidence\r3\tuning\20260421_R3-04G_TARGETED-R33.csv`
- KPI Pass: `9/13`

## Round Parameter Delta

- Bias (Easy/Standard/Hard): `0.25/0.14/0.01`
- Init Scale (Easy/Standard/Hard): `1.11/1.03/0.99`
- Social/Trade Init Scale: `0.22`
- Victory Threshold (Easy/Standard/Hard): `86/84/83`
- Pressure Offset: `0.22`; Conflict Base/TurnScale: `0.380/0.012`
- Delta Scale/Noise Scale: `0.97/1.98`
- Early Suppression: `turn<= 7` with `positive*0.49`
- Shock: `chance=0.40`, `magnitude=+/-32`

## KPI Snapshot

| KPI | Actual | Target | Status |
|---|---:|---:|---|
| WIN_RATE_EASY | 0.6867 | [0.6500, 0.8000] | PASS |
| WIN_RATE_STANDARD | 0.4025 | [0.4500, 0.5500] | FAIL |
| WIN_RATE_HARD | 0.0833 | [0.2000, 0.3500] | FAIL |
| WIN_RATE_OVERALL | 0.3920 | [0.4000, 0.6000] | FAIL |
| AVG_END_TURN_OVERALL | 14.7420 | [14.0000, 20.0000] | PASS |
| AVG_END_TURN_STANDARD | 16.3650 | [15.0000, 19.0000] | PASS |
| ATTRITION_RATE_OVERALL | 0.2200 | [0.1000, 0.3500] | PASS |
| RESOURCE_CV_GOLDLEAF | 0.3606 | [0.1800, 0.4500] | PASS |
| RESOURCE_CV_FIREOIL | 0.2045 | [0.1500, 0.4000] | PASS |
| RESOURCE_CV_ARMS | 0.2698 | [0.2000, 0.5500] | PASS |
| VICTORY_SHARE_OVERALL | 0.3920 | [0.3500, 0.6500] | PASS |
| DEFEAT_SHARE_OVERALL | 0.3880 | [0.3500, 0.6500] | PASS |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.8929 | [0.0000, 0.7000] | FAIL |

## Endgame Distribution

| End Type | Count | Share |
|---|---:|---:|
| victory | 392 | 0.3920 |
| defeat | 388 | 0.3880 |
| timeout | 220 | 0.2200 |

- Top victory path: `energyliberation` (`350` wins, share `0.8929`)
