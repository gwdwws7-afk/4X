# R3-04 KPI Evaluation (R19)

- Batch: `R3-04D-B002-R19`
- Profile: `axis-shift-balanced`
- Runs: `1000`
- Source CSV: `C:\test\4X\production\evidence\r3\tuning\20260420_R3-04D_TARGETED-R19.csv`
- KPI Pass: `7/13`

## Round Parameter Delta

- Bias (Easy/Standard/Hard): `0.18/0.10/0.00`
- Init Scale (Easy/Standard/Hard): `1.04/1.00/0.97`
- Social/Trade Init Scale: `0.26`
- Victory Threshold (Easy/Standard/Hard): `88/85/82`
- Pressure Offset: `0.18`; Conflict Base/TurnScale: `0.370/0.013`
- Delta Scale/Noise Scale: `0.93/2.00`
- Early Suppression: `turn<= 7` with `positive*0.50`
- Shock: `chance=0.40`, `magnitude=+/-32`

## KPI Snapshot

| KPI | Actual | Target | Status |
|---|---:|---:|---|
| WIN_RATE_EASY | 0.5333 | [0.6500, 0.8000] | FAIL |
| WIN_RATE_STANDARD | 0.3525 | [0.4500, 0.5500] | FAIL |
| WIN_RATE_HARD | 0.1133 | [0.2000, 0.3500] | FAIL |
| WIN_RATE_OVERALL | 0.3350 | [0.4000, 0.6000] | FAIL |
| AVG_END_TURN_OVERALL | 14.9600 | [14.0000, 20.0000] | PASS |
| AVG_END_TURN_STANDARD | 16.5450 | [15.0000, 19.0000] | PASS |
| ATTRITION_RATE_OVERALL | 0.2270 | [0.1000, 0.3500] | PASS |
| RESOURCE_CV_GOLDLEAF | 0.3644 | [0.1800, 0.4500] | PASS |
| RESOURCE_CV_FIREOIL | 0.2108 | [0.1500, 0.4000] | PASS |
| RESOURCE_CV_ARMS | 0.2784 | [0.2000, 0.5500] | PASS |
| VICTORY_SHARE_OVERALL | 0.3350 | [0.3500, 0.6500] | FAIL |
| DEFEAT_SHARE_OVERALL | 0.4380 | [0.3500, 0.6500] | PASS |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.9761 | [0.0000, 0.7000] | FAIL |

## Endgame Distribution

| End Type | Count | Share |
|---|---:|---:|
| victory | 335 | 0.3350 |
| defeat | 438 | 0.4380 |
| timeout | 227 | 0.2270 |

- Top victory path: `energyliberation` (`327` wins, share `0.9761`)
