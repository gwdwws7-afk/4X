# R3-04 KPI Evaluation (R96)

- Batch: `R3-04I-B004-R96`
- Profile: `r82-gate-balance-c`
- Runs: `1000`
- Source CSV: `C:\test\4X\production\evidence\r3\tuning\20260421_R3-04I_TARGETED-R96.csv`
- KPI Pass: `11/13`

## Round Parameter Delta

- Bias (Easy/Standard/Hard): `0.26/0.15/0.05`
- Init Scale (Easy/Standard/Hard): `1.13/1.07/1.03`
- Social/Trade Init Scale: `0.16`
- Victory Threshold (Easy/Standard/Hard): `86/83/81`
- Pressure Offset: `0.25`; Conflict Base/TurnScale: `0.403/0.013`
- Delta Scale/Noise Scale: `0.95/1.98`
- Early Suppression: `turn<= 8` with `positive*0.46`
- Shock: `chance=0.38`, `magnitude=+/-30`

## KPI Snapshot

| KPI | Actual | Target | Status |
|---|---:|---:|---|
| WIN_RATE_EASY | 0.5967 | [0.6500, 0.8000] | FAIL |
| WIN_RATE_STANDARD | 0.4625 | [0.4500, 0.5500] | PASS |
| WIN_RATE_HARD | 0.2100 | [0.2000, 0.3500] | PASS |
| WIN_RATE_OVERALL | 0.4270 | [0.4000, 0.6000] | PASS |
| AVG_END_TURN_OVERALL | 13.9710 | [14.0000, 20.0000] | FAIL |
| AVG_END_TURN_STANDARD | 15.2700 | [15.0000, 19.0000] | PASS |
| ATTRITION_RATE_OVERALL | 0.2140 | [0.1000, 0.3500] | PASS |
| RESOURCE_CV_GOLDLEAF | 0.3327 | [0.1800, 0.4500] | PASS |
| RESOURCE_CV_FIREOIL | 0.1726 | [0.1500, 0.4000] | PASS |
| RESOURCE_CV_ARMS | 0.2336 | [0.2000, 0.5500] | PASS |
| VICTORY_SHARE_OVERALL | 0.4270 | [0.3500, 0.6500] | PASS |
| DEFEAT_SHARE_OVERALL | 0.3590 | [0.3500, 0.6500] | PASS |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.6721 | [0.0000, 0.7000] | PASS |

## Endgame Distribution

| End Type | Count | Share |
|---|---:|---:|
| victory | 427 | 0.4270 |
| defeat | 359 | 0.3590 |
| timeout | 214 | 0.2140 |

- Top victory path: `energyliberation` (`287` wins, share `0.6721`)
