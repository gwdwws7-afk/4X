# R3-04 KPI Evaluation (R29)

- Batch: `R3-04G-B001-R29`
- Profile: `difficulty-rebalance-b`
- Runs: `1000`
- Source CSV: `C:\test\4X\production\evidence\r3\tuning\20260421_R3-04G_TARGETED-R29.csv`
- KPI Pass: `8/13`

## Round Parameter Delta

- Bias (Easy/Standard/Hard): `0.29/0.14/-0.04`
- Init Scale (Easy/Standard/Hard): `1.15/1.03/0.94`
- Social/Trade Init Scale: `0.24`
- Victory Threshold (Easy/Standard/Hard): `85/84/85`
- Pressure Offset: `0.24`; Conflict Base/TurnScale: `0.400/0.013`
- Delta Scale/Noise Scale: `0.96/2.08`
- Early Suppression: `turn<= 7` with `positive*0.46`
- Shock: `chance=0.44`, `magnitude=+/-35`

## KPI Snapshot

| KPI | Actual | Target | Status |
|---|---:|---:|---|
| WIN_RATE_EASY | 0.7600 | [0.6500, 0.8000] | PASS |
| WIN_RATE_STANDARD | 0.3250 | [0.4500, 0.5500] | FAIL |
| WIN_RATE_HARD | 0.0267 | [0.2000, 0.3500] | FAIL |
| WIN_RATE_OVERALL | 0.3660 | [0.4000, 0.6000] | FAIL |
| AVG_END_TURN_OVERALL | 13.5250 | [14.0000, 20.0000] | FAIL |
| AVG_END_TURN_STANDARD | 16.4800 | [15.0000, 19.0000] | PASS |
| ATTRITION_RATE_OVERALL | 0.1760 | [0.1000, 0.3500] | PASS |
| RESOURCE_CV_GOLDLEAF | 0.3881 | [0.1800, 0.4500] | PASS |
| RESOURCE_CV_FIREOIL | 0.2169 | [0.1500, 0.4000] | PASS |
| RESOURCE_CV_ARMS | 0.3000 | [0.2000, 0.5500] | PASS |
| VICTORY_SHARE_OVERALL | 0.3660 | [0.3500, 0.6500] | PASS |
| DEFEAT_SHARE_OVERALL | 0.4580 | [0.3500, 0.6500] | PASS |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.8989 | [0.0000, 0.7000] | FAIL |

## Endgame Distribution

| End Type | Count | Share |
|---|---:|---:|
| victory | 366 | 0.3660 |
| defeat | 458 | 0.4580 |
| timeout | 176 | 0.1760 |

- Top victory path: `energyliberation` (`329` wins, share `0.8989`)
