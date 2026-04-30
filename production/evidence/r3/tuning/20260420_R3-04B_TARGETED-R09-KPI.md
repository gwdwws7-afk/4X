# R3-04 KPI Evaluation (R09)

- Batch: `R3-04B-B002-R09`
- Profile: `anti-energy-diversify`
- Runs: `1000`
- Source CSV: `C:\test\4X\production\evidence\r3\tuning\20260420_R3-04B_TARGETED-R09.csv`
- KPI Pass: `6/13`

## Round Parameter Delta

- Bias (Easy/Standard/Hard): `0.02/0.06/0.00`
- Init Scale (Easy/Standard/Hard): `0.89/0.96/0.99`
- Social/Trade Init Scale: `0.50`
- Victory Threshold (Easy/Standard/Hard): `90/89/80`
- Pressure Offset: `0.15`; Conflict Base/TurnScale: `0.360/0.012`
- Delta Scale/Noise Scale: `0.95/1.55`
- Early Suppression: `turn<= 7` with `positive*0.52`
- Shock: `chance=0.24`, `magnitude=+/-20`

## KPI Snapshot

| KPI | Actual | Target | Status |
|---|---:|---:|---|
| WIN_RATE_EASY | 0.0200 | [0.6500, 0.8000] | FAIL |
| WIN_RATE_STANDARD | 0.0625 | [0.4500, 0.5500] | FAIL |
| WIN_RATE_HARD | 0.2500 | [0.2000, 0.3500] | PASS |
| WIN_RATE_OVERALL | 0.1060 | [0.4000, 0.6000] | FAIL |
| AVG_END_TURN_OVERALL | 15.5970 | [14.0000, 20.0000] | PASS |
| AVG_END_TURN_STANDARD | 17.2450 | [15.0000, 19.0000] | PASS |
| ATTRITION_RATE_OVERALL | 0.2410 | [0.1000, 0.3500] | PASS |
| RESOURCE_CV_GOLDLEAF | 0.3299 | [0.1800, 0.4500] | PASS |
| RESOURCE_CV_FIREOIL | 0.1342 | [0.1500, 0.4000] | FAIL |
| RESOURCE_CV_ARMS | 0.1673 | [0.2000, 0.5500] | FAIL |
| VICTORY_SHARE_OVERALL | 0.1060 | [0.3500, 0.6500] | FAIL |
| DEFEAT_SHARE_OVERALL | 0.6530 | [0.3500, 0.6500] | FAIL |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.5000 | [0.0000, 0.7000] | PASS |

## Endgame Distribution

| End Type | Count | Share |
|---|---:|---:|
| victory | 106 | 0.1060 |
| defeat | 653 | 0.6530 |
| timeout | 241 | 0.2410 |

- Top victory path: `energyliberation` (`53` wins, share `0.5000`)
