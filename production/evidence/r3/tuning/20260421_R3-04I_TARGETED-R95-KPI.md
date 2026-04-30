# R3-04 KPI Evaluation (R95)

- Batch: `R3-04I-B004-R95`
- Profile: `r82-gate-balance-b`
- Runs: `1000`
- Source CSV: `C:\test\4X\production\evidence\r3\tuning\20260421_R3-04I_TARGETED-R95.csv`
- KPI Pass: `11/13`

## Round Parameter Delta

- Bias (Easy/Standard/Hard): `0.27/0.15/0.05`
- Init Scale (Easy/Standard/Hard): `1.14/1.07/1.03`
- Social/Trade Init Scale: `0.16`
- Victory Threshold (Easy/Standard/Hard): `86/83/81`
- Pressure Offset: `0.25`; Conflict Base/TurnScale: `0.404/0.013`
- Delta Scale/Noise Scale: `0.95/2.00`
- Early Suppression: `turn<= 8` with `positive*0.46`
- Shock: `chance=0.39`, `magnitude=+/-31`

## KPI Snapshot

| KPI | Actual | Target | Status |
|---|---:|---:|---|
| WIN_RATE_EASY | 0.6300 | [0.6500, 0.8000] | FAIL |
| WIN_RATE_STANDARD | 0.4800 | [0.4500, 0.5500] | PASS |
| WIN_RATE_HARD | 0.2367 | [0.2000, 0.3500] | PASS |
| WIN_RATE_OVERALL | 0.4520 | [0.4000, 0.6000] | PASS |
| AVG_END_TURN_OVERALL | 13.8070 | [14.0000, 20.0000] | FAIL |
| AVG_END_TURN_STANDARD | 15.2025 | [15.0000, 19.0000] | PASS |
| ATTRITION_RATE_OVERALL | 0.1950 | [0.1000, 0.3500] | PASS |
| RESOURCE_CV_GOLDLEAF | 0.3445 | [0.1800, 0.4500] | PASS |
| RESOURCE_CV_FIREOIL | 0.1774 | [0.1500, 0.4000] | PASS |
| RESOURCE_CV_ARMS | 0.2399 | [0.2000, 0.5500] | PASS |
| VICTORY_SHARE_OVERALL | 0.4520 | [0.3500, 0.6500] | PASS |
| DEFEAT_SHARE_OVERALL | 0.3530 | [0.3500, 0.6500] | PASS |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.6947 | [0.0000, 0.7000] | PASS |

## Endgame Distribution

| End Type | Count | Share |
|---|---:|---:|
| victory | 452 | 0.4520 |
| defeat | 353 | 0.3530 |
| timeout | 195 | 0.1950 |

- Top victory path: `energyliberation` (`314` wins, share `0.6947`)
