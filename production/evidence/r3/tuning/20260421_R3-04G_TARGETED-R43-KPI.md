# R3-04 KPI Evaluation (R43)

- Batch: `R3-04G-B006-R43`
- Profile: `post-combined-rebalance-a`
- Runs: `1000`
- Source CSV: `C:\test\4X\production\evidence\r3\tuning\20260421_R3-04G_TARGETED-R43.csv`
- KPI Pass: `9/13`

## Round Parameter Delta

- Bias (Easy/Standard/Hard): `0.24/0.15/0.03`
- Init Scale (Easy/Standard/Hard): `1.15/1.06/1.03`
- Social/Trade Init Scale: `0.22`
- Victory Threshold (Easy/Standard/Hard): `86/83/83`
- Pressure Offset: `0.24`; Conflict Base/TurnScale: `0.390/0.012`
- Delta Scale/Noise Scale: `0.96/2.00`
- Early Suppression: `turn<= 8` with `positive*0.44`
- Shock: `chance=0.40`, `magnitude=+/-32`

## KPI Snapshot

| KPI | Actual | Target | Status |
|---|---:|---:|---|
| WIN_RATE_EASY | 0.6167 | [0.6500, 0.8000] | FAIL |
| WIN_RATE_STANDARD | 0.4500 | [0.4500, 0.5500] | PASS |
| WIN_RATE_HARD | 0.0867 | [0.2000, 0.3500] | FAIL |
| WIN_RATE_OVERALL | 0.3910 | [0.4000, 0.6000] | FAIL |
| AVG_END_TURN_OVERALL | 14.9960 | [14.0000, 20.0000] | PASS |
| AVG_END_TURN_STANDARD | 16.1175 | [15.0000, 19.0000] | PASS |
| ATTRITION_RATE_OVERALL | 0.2290 | [0.1000, 0.3500] | PASS |
| RESOURCE_CV_GOLDLEAF | 0.3804 | [0.1800, 0.4500] | PASS |
| RESOURCE_CV_FIREOIL | 0.1997 | [0.1500, 0.4000] | PASS |
| RESOURCE_CV_ARMS | 0.2798 | [0.2000, 0.5500] | PASS |
| VICTORY_SHARE_OVERALL | 0.3910 | [0.3500, 0.6500] | PASS |
| DEFEAT_SHARE_OVERALL | 0.3800 | [0.3500, 0.6500] | PASS |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.8568 | [0.0000, 0.7000] | FAIL |

## Endgame Distribution

| End Type | Count | Share |
|---|---:|---:|
| victory | 391 | 0.3910 |
| defeat | 380 | 0.3800 |
| timeout | 229 | 0.2290 |

- Top victory path: `energyliberation` (`335` wins, share `0.8568`)
