# R3-04 KPI Evaluation (R34)

- Batch: `R3-04G-B003-R34`
- Profile: `easy-floor-hard-cap`
- Runs: `1000`
- Source CSV: `C:\test\4X\production\evidence\r3\tuning\20260421_R3-04G_TARGETED-R34.csv`
- KPI Pass: `9/13`

## Round Parameter Delta

- Bias (Easy/Standard/Hard): `0.22/0.13/0.00`
- Init Scale (Easy/Standard/Hard): `1.16/1.05/0.95`
- Social/Trade Init Scale: `0.23`
- Victory Threshold (Easy/Standard/Hard): `85/83/84`
- Pressure Offset: `0.23`; Conflict Base/TurnScale: `0.380/0.012`
- Delta Scale/Noise Scale: `0.98/1.98`
- Early Suppression: `turn<= 6` with `positive*0.50`
- Shock: `chance=0.39`, `magnitude=+/-31`

## KPI Snapshot

| KPI | Actual | Target | Status |
|---|---:|---:|---|
| WIN_RATE_EASY | 0.6667 | [0.6500, 0.8000] | PASS |
| WIN_RATE_STANDARD | 0.4075 | [0.4500, 0.5500] | FAIL |
| WIN_RATE_HARD | 0.0633 | [0.2000, 0.3500] | FAIL |
| WIN_RATE_OVERALL | 0.3820 | [0.4000, 0.6000] | FAIL |
| AVG_END_TURN_OVERALL | 14.4350 | [14.0000, 20.0000] | PASS |
| AVG_END_TURN_STANDARD | 16.2775 | [15.0000, 19.0000] | PASS |
| ATTRITION_RATE_OVERALL | 0.1970 | [0.1000, 0.3500] | PASS |
| RESOURCE_CV_GOLDLEAF | 0.3926 | [0.1800, 0.4500] | PASS |
| RESOURCE_CV_FIREOIL | 0.1916 | [0.1500, 0.4000] | PASS |
| RESOURCE_CV_ARMS | 0.2609 | [0.2000, 0.5500] | PASS |
| VICTORY_SHARE_OVERALL | 0.3820 | [0.3500, 0.6500] | PASS |
| DEFEAT_SHARE_OVERALL | 0.4210 | [0.3500, 0.6500] | PASS |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.8927 | [0.0000, 0.7000] | FAIL |

## Endgame Distribution

| End Type | Count | Share |
|---|---:|---:|
| victory | 382 | 0.3820 |
| defeat | 421 | 0.4210 |
| timeout | 197 | 0.1970 |

- Top victory path: `energyliberation` (`341` wins, share `0.8927`)
