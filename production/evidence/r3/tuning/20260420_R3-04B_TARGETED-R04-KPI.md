# R3-04 KPI Evaluation (R04)

- Batch: `R3-04B-B001-R04`
- Profile: `pace-extend`
- Runs: `1000`
- Source CSV: `C:\test\4X\production\evidence\r3\tuning\20260420_R3-04B_TARGETED-R04.csv`
- KPI Pass: `4/13`

## Round Parameter Delta

- Bias (Easy/Standard/Hard): `-0.04/0.05/-0.03`
- Init Scale (Easy/Standard/Hard): `0.82/0.96/0.94`
- Social/Trade Init Scale: `0.52`
- Victory Threshold (Easy/Standard/Hard): `96/92/84`
- Pressure Offset: `0.16`; Conflict Base/TurnScale: `0.350/0.011`
- Delta Scale/Noise Scale: `0.82/1.50`
- Early Suppression: `turn<= 9` with `positive*0.38`
- Shock: `chance=0.28`, `magnitude=+/-22`

## KPI Snapshot

| KPI | Actual | Target | Status |
|---|---:|---:|---|
| WIN_RATE_EASY | 0.0000 | [0.6500, 0.8000] | FAIL |
| WIN_RATE_STANDARD | 0.0175 | [0.4500, 0.5500] | FAIL |
| WIN_RATE_HARD | 0.0300 | [0.2000, 0.3500] | FAIL |
| WIN_RATE_OVERALL | 0.0160 | [0.4000, 0.6000] | FAIL |
| AVG_END_TURN_OVERALL | 14.5830 | [14.0000, 20.0000] | PASS |
| AVG_END_TURN_STANDARD | 17.2150 | [15.0000, 19.0000] | PASS |
| ATTRITION_RATE_OVERALL | 0.1300 | [0.1000, 0.3500] | PASS |
| RESOURCE_CV_GOLDLEAF | 0.3639 | [0.1800, 0.4500] | PASS |
| RESOURCE_CV_FIREOIL | 0.1407 | [0.1500, 0.4000] | FAIL |
| RESOURCE_CV_ARMS | 0.1951 | [0.2000, 0.5500] | FAIL |
| VICTORY_SHARE_OVERALL | 0.0160 | [0.3500, 0.6500] | FAIL |
| DEFEAT_SHARE_OVERALL | 0.8540 | [0.3500, 0.6500] | FAIL |
| SINGLE_PATH_VICTORY_MONOPOLY | 1.0000 | [0.0000, 0.7000] | FAIL |

## Endgame Distribution

| End Type | Count | Share |
|---|---:|---:|
| victory | 16 | 0.0160 |
| defeat | 854 | 0.8540 |
| timeout | 130 | 0.1300 |

- Top victory path: `energyliberation` (`16` wins, share `1.0000`)
