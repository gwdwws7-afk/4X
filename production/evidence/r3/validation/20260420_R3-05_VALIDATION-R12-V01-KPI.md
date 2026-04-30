# R3-04 KPI Evaluation (V01)

- Batch: `R3-05-B002-R12-V01`
- Profile: `winrate-recover-diversify-validation`
- Runs: `1000`
- Source CSV: `C:\test\4X\production\evidence\r3\validation\20260420_R3-05_VALIDATION-R12-V01.csv`
- KPI Pass: `6/13`

## Round Parameter Delta

- Bias (Easy/Standard/Hard): `0.05/0.10/0.02`
- Init Scale (Easy/Standard/Hard): `0.90/0.98/1.00`
- Social/Trade Init Scale: `0.55`
- Victory Threshold (Easy/Standard/Hard): `91/84/79`
- Pressure Offset: `0.14`; Conflict Base/TurnScale: `0.360/0.012`
- Delta Scale/Noise Scale: `0.97/1.55`
- Early Suppression: `turn<= 7` with `positive*0.55`
- Shock: `chance=0.22`, `magnitude=+/-20`

## KPI Snapshot

| KPI | Actual | Target | Status |
|---|---:|---:|---|
| WIN_RATE_EASY | 0.1533 | [0.6500, 0.8000] | FAIL |
| WIN_RATE_STANDARD | 0.5250 | [0.4500, 0.5500] | PASS |
| WIN_RATE_HARD | 0.3933 | [0.2000, 0.3500] | FAIL |
| WIN_RATE_OVERALL | 0.3740 | [0.4000, 0.6000] | FAIL |
| AVG_END_TURN_OVERALL | 14.3600 | [14.0000, 20.0000] | PASS |
| AVG_END_TURN_STANDARD | 13.6650 | [15.0000, 19.0000] | FAIL |
| ATTRITION_RATE_OVERALL | 0.2500 | [0.1000, 0.3500] | PASS |
| RESOURCE_CV_GOLDLEAF | 0.2654 | [0.1800, 0.4500] | PASS |
| RESOURCE_CV_FIREOIL | 0.1245 | [0.1500, 0.4000] | FAIL |
| RESOURCE_CV_ARMS | 0.1542 | [0.2000, 0.5500] | FAIL |
| VICTORY_SHARE_OVERALL | 0.3740 | [0.3500, 0.6500] | PASS |
| DEFEAT_SHARE_OVERALL | 0.3760 | [0.3500, 0.6500] | PASS |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.8235 | [0.0000, 0.7000] | FAIL |

## Endgame Distribution

| End Type | Count | Share |
|---|---:|---:|
| victory | 374 | 0.3740 |
| defeat | 376 | 0.3760 |
| timeout | 250 | 0.2500 |

- Top victory path: `energyliberation` (`308` wins, share `0.8235`)
