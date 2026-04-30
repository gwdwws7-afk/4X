# R3-04 KPI Evaluation (R20)

- Batch: `R3-04D-B002-R20`
- Profile: `window-tighten`
- Runs: `1000`
- Source CSV: `C:\test\4X\production\evidence\r3\tuning\20260420_R3-04D_TARGETED-R20.csv`
- KPI Pass: `7/13`

## Round Parameter Delta

- Bias (Easy/Standard/Hard): `0.16/0.09/-0.01`
- Init Scale (Easy/Standard/Hard): `1.02/0.99/0.95`
- Social/Trade Init Scale: `0.24`
- Victory Threshold (Easy/Standard/Hard): `89/86/83`
- Pressure Offset: `0.20`; Conflict Base/TurnScale: `0.380/0.013`
- Delta Scale/Noise Scale: `0.92/2.10`
- Early Suppression: `turn<= 8` with `positive*0.46`
- Shock: `chance=0.42`, `magnitude=+/-34`

## KPI Snapshot

| KPI | Actual | Target | Status |
|---|---:|---:|---|
| WIN_RATE_EASY | 0.4033 | [0.6500, 0.8000] | FAIL |
| WIN_RATE_STANDARD | 0.2425 | [0.4500, 0.5500] | FAIL |
| WIN_RATE_HARD | 0.0633 | [0.2000, 0.3500] | FAIL |
| WIN_RATE_OVERALL | 0.2370 | [0.4000, 0.6000] | FAIL |
| AVG_END_TURN_OVERALL | 14.5700 | [14.0000, 20.0000] | PASS |
| AVG_END_TURN_STANDARD | 15.8800 | [15.0000, 19.0000] | PASS |
| ATTRITION_RATE_OVERALL | 0.2340 | [0.1000, 0.3500] | PASS |
| RESOURCE_CV_GOLDLEAF | 0.4289 | [0.1800, 0.4500] | PASS |
| RESOURCE_CV_FIREOIL | 0.2258 | [0.1500, 0.4000] | PASS |
| RESOURCE_CV_ARMS | 0.3251 | [0.2000, 0.5500] | PASS |
| VICTORY_SHARE_OVERALL | 0.2370 | [0.3500, 0.6500] | FAIL |
| DEFEAT_SHARE_OVERALL | 0.5290 | [0.3500, 0.6500] | PASS |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.9873 | [0.0000, 0.7000] | FAIL |

## Endgame Distribution

| End Type | Count | Share |
|---|---:|---:|
| victory | 237 | 0.2370 |
| defeat | 529 | 0.5290 |
| timeout | 234 | 0.2340 |

- Top victory path: `energyliberation` (`234` wins, share `0.9873`)
