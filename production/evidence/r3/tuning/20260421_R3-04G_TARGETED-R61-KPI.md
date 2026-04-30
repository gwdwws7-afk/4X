# R3-04 KPI Evaluation (R61)

- Batch: `R3-04G-B012-R61`
- Profile: `r58-micro-easyup-a`
- Runs: `1000`
- Source CSV: `C:\test\4X\production\evidence\r3\tuning\20260421_R3-04G_TARGETED-R61.csv`
- KPI Pass: `10/13`

## Round Parameter Delta

- Bias (Easy/Standard/Hard): `0.25/0.14/0.05`
- Init Scale (Easy/Standard/Hard): `1.11/1.05/1.01`
- Social/Trade Init Scale: `0.16`
- Victory Threshold (Easy/Standard/Hard): `86/83/82`
- Pressure Offset: `0.24`; Conflict Base/TurnScale: `0.400/0.013`
- Delta Scale/Noise Scale: `0.96/2.05`
- Early Suppression: `turn<= 7` with `positive*0.47`
- Shock: `chance=0.41`, `magnitude=+/-33`

## KPI Snapshot

| KPI | Actual | Target | Status |
|---|---:|---:|---|
| WIN_RATE_EASY | 0.6400 | [0.6500, 0.8000] | FAIL |
| WIN_RATE_STANDARD | 0.4500 | [0.4500, 0.5500] | PASS |
| WIN_RATE_HARD | 0.1633 | [0.2000, 0.3500] | FAIL |
| WIN_RATE_OVERALL | 0.4210 | [0.4000, 0.6000] | PASS |
| AVG_END_TURN_OVERALL | 14.1500 | [14.0000, 20.0000] | PASS |
| AVG_END_TURN_STANDARD | 15.4975 | [15.0000, 19.0000] | PASS |
| ATTRITION_RATE_OVERALL | 0.2080 | [0.1000, 0.3500] | PASS |
| RESOURCE_CV_GOLDLEAF | 0.3682 | [0.1800, 0.4500] | PASS |
| RESOURCE_CV_FIREOIL | 0.1960 | [0.1500, 0.4000] | PASS |
| RESOURCE_CV_ARMS | 0.2772 | [0.2000, 0.5500] | PASS |
| VICTORY_SHARE_OVERALL | 0.4210 | [0.3500, 0.6500] | PASS |
| DEFEAT_SHARE_OVERALL | 0.3710 | [0.3500, 0.6500] | PASS |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.7007 | [0.0000, 0.7000] | FAIL |

## Endgame Distribution

| End Type | Count | Share |
|---|---:|---:|
| victory | 421 | 0.4210 |
| defeat | 371 | 0.3710 |
| timeout | 208 | 0.2080 |

- Top victory path: `energyliberation` (`295` wins, share `0.7007`)
