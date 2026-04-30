# R3-04 KPI Evaluation (R87)

- Batch: `R3-04I-B001-R87`
- Profile: `r82-gate-stability-c`
- Runs: `1000`
- Source CSV: `C:\test\4X\production\evidence\r3\tuning\20260421_R3-04I_TARGETED-R87.csv`
- KPI Pass: `8/13`

## Round Parameter Delta

- Bias (Easy/Standard/Hard): `0.25/0.16/0.05`
- Init Scale (Easy/Standard/Hard): `1.11/1.08/1.03`
- Social/Trade Init Scale: `0.15`
- Victory Threshold (Easy/Standard/Hard): `85/82/81`
- Pressure Offset: `0.25`; Conflict Base/TurnScale: `0.404/0.014`
- Delta Scale/Noise Scale: `0.95/1.98`
- Early Suppression: `turn<= 8` with `positive*0.45`
- Shock: `chance=0.39`, `magnitude=+/-31`

## KPI Snapshot

| KPI | Actual | Target | Status |
|---|---:|---:|---|
| WIN_RATE_EASY | 0.6333 | [0.6500, 0.8000] | FAIL |
| WIN_RATE_STANDARD | 0.5825 | [0.4500, 0.5500] | FAIL |
| WIN_RATE_HARD | 0.2267 | [0.2000, 0.3500] | PASS |
| WIN_RATE_OVERALL | 0.4910 | [0.4000, 0.6000] | PASS |
| AVG_END_TURN_OVERALL | 13.2190 | [14.0000, 20.0000] | FAIL |
| AVG_END_TURN_STANDARD | 14.0500 | [15.0000, 19.0000] | FAIL |
| ATTRITION_RATE_OVERALL | 0.1820 | [0.1000, 0.3500] | PASS |
| RESOURCE_CV_GOLDLEAF | 0.3341 | [0.1800, 0.4500] | PASS |
| RESOURCE_CV_FIREOIL | 0.1740 | [0.1500, 0.4000] | PASS |
| RESOURCE_CV_ARMS | 0.2347 | [0.2000, 0.5500] | PASS |
| VICTORY_SHARE_OVERALL | 0.4910 | [0.3500, 0.6500] | PASS |
| DEFEAT_SHARE_OVERALL | 0.3270 | [0.3500, 0.6500] | FAIL |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.5967 | [0.0000, 0.7000] | PASS |

## Endgame Distribution

| End Type | Count | Share |
|---|---:|---:|
| victory | 491 | 0.4910 |
| defeat | 327 | 0.3270 |
| timeout | 182 | 0.1820 |

- Top victory path: `energyliberation` (`293` wins, share `0.5967`)
