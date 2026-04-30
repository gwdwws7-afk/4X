# R3-04 KPI Evaluation (R32)

- Batch: `R3-04G-B002-R32`
- Profile: `r23-guided-rebalance-b`
- Runs: `1000`
- Source CSV: `C:\test\4X\production\evidence\r3\tuning\20260421_R3-04G_TARGETED-R32.csv`
- KPI Pass: `7/13`

## Round Parameter Delta

- Bias (Easy/Standard/Hard): `0.27/0.13/-0.04`
- Init Scale (Easy/Standard/Hard): `1.13/1.02/0.96`
- Social/Trade Init Scale: `0.24`
- Victory Threshold (Easy/Standard/Hard): `86/84/85`
- Pressure Offset: `0.24`; Conflict Base/TurnScale: `0.400/0.013`
- Delta Scale/Noise Scale: `0.96/2.08`
- Early Suppression: `turn<= 7` with `positive*0.47`
- Shock: `chance=0.42`, `magnitude=+/-34`

## KPI Snapshot

| KPI | Actual | Target | Status |
|---|---:|---:|---|
| WIN_RATE_EASY | 0.6200 | [0.6500, 0.8000] | FAIL |
| WIN_RATE_STANDARD | 0.3225 | [0.4500, 0.5500] | FAIL |
| WIN_RATE_HARD | 0.0133 | [0.2000, 0.3500] | FAIL |
| WIN_RATE_OVERALL | 0.3190 | [0.4000, 0.6000] | FAIL |
| AVG_END_TURN_OVERALL | 14.4040 | [14.0000, 20.0000] | PASS |
| AVG_END_TURN_STANDARD | 16.2425 | [15.0000, 19.0000] | PASS |
| ATTRITION_RATE_OVERALL | 0.2080 | [0.1000, 0.3500] | PASS |
| RESOURCE_CV_GOLDLEAF | 0.4196 | [0.1800, 0.4500] | PASS |
| RESOURCE_CV_FIREOIL | 0.2285 | [0.1500, 0.4000] | PASS |
| RESOURCE_CV_ARMS | 0.3054 | [0.2000, 0.5500] | PASS |
| VICTORY_SHARE_OVERALL | 0.3190 | [0.3500, 0.6500] | FAIL |
| DEFEAT_SHARE_OVERALL | 0.4730 | [0.3500, 0.6500] | PASS |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.9154 | [0.0000, 0.7000] | FAIL |

## Endgame Distribution

| End Type | Count | Share |
|---|---:|---:|
| victory | 319 | 0.3190 |
| defeat | 473 | 0.4730 |
| timeout | 208 | 0.2080 |

- Top victory path: `energyliberation` (`292` wins, share `0.9154`)
