# R3-04 KPI Evaluation (R13)

- Batch: `R3-04C-B001-R13`
- Profile: `easy-recover-balance`
- Runs: `1000`
- Source CSV: `C:\test\4X\production\evidence\r3\tuning\20260420_R3-04C_TARGETED-R13.csv`
- KPI Pass: `4/13`

## Round Parameter Delta

- Bias (Easy/Standard/Hard): `0.16/0.08/-0.03`
- Init Scale (Easy/Standard/Hard): `1.00/1.00/0.95`
- Social/Trade Init Scale: `0.48`
- Victory Threshold (Easy/Standard/Hard): `90/88/83`
- Pressure Offset: `0.16`; Conflict Base/TurnScale: `0.350/0.012`
- Delta Scale/Noise Scale: `0.96/1.75`
- Early Suppression: `turn<= 7` with `positive*0.52`
- Shock: `chance=0.30`, `magnitude=+/-24`

## KPI Snapshot

| KPI | Actual | Target | Status |
|---|---:|---:|---|
| WIN_RATE_EASY | 0.5467 | [0.6500, 0.8000] | FAIL |
| WIN_RATE_STANDARD | 0.2550 | [0.4500, 0.5500] | FAIL |
| WIN_RATE_HARD | 0.0667 | [0.2000, 0.3500] | FAIL |
| WIN_RATE_OVERALL | 0.2860 | [0.4000, 0.6000] | FAIL |
| AVG_END_TURN_OVERALL | 13.9360 | [14.0000, 20.0000] | FAIL |
| AVG_END_TURN_STANDARD | 17.0250 | [15.0000, 19.0000] | PASS |
| ATTRITION_RATE_OVERALL | 0.2660 | [0.1000, 0.3500] | PASS |
| RESOURCE_CV_GOLDLEAF | 0.2934 | [0.1800, 0.4500] | PASS |
| RESOURCE_CV_FIREOIL | 0.1413 | [0.1500, 0.4000] | FAIL |
| RESOURCE_CV_ARMS | 0.1861 | [0.2000, 0.5500] | FAIL |
| VICTORY_SHARE_OVERALL | 0.2860 | [0.3500, 0.6500] | FAIL |
| DEFEAT_SHARE_OVERALL | 0.4480 | [0.3500, 0.6500] | PASS |
| SINGLE_PATH_VICTORY_MONOPOLY | 1.0000 | [0.0000, 0.7000] | FAIL |

## Endgame Distribution

| End Type | Count | Share |
|---|---:|---:|
| victory | 286 | 0.2860 |
| defeat | 448 | 0.4480 |
| timeout | 266 | 0.2660 |

- Top victory path: `energyliberation` (`286` wins, share `1.0000`)
