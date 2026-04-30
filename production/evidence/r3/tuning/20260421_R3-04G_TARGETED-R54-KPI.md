# R3-04 KPI Evaluation (R54)

- Batch: `R3-04G-B009-R54`
- Profile: `r23-easyup-hardcollapse-c`
- Runs: `1000`
- Source CSV: `C:\test\4X\production\evidence\r3\tuning\20260421_R3-04G_TARGETED-R54.csv`
- KPI Pass: `8/13`

## Round Parameter Delta

- Bias (Easy/Standard/Hard): `0.25/0.14/0.05`
- Init Scale (Easy/Standard/Hard): `1.12/1.05/1.04`
- Social/Trade Init Scale: `0.24`
- Victory Threshold (Easy/Standard/Hard): `86/83/79`
- Pressure Offset: `0.23`; Conflict Base/TurnScale: `0.390/0.013`
- Delta Scale/Noise Scale: `0.97/2.02`
- Early Suppression: `turn<= 7` with `positive*0.47`
- Shock: `chance=0.41`, `magnitude=+/-33`

## KPI Snapshot

| KPI | Actual | Target | Status |
|---|---:|---:|---|
| WIN_RATE_EASY | 0.6067 | [0.6500, 0.8000] | FAIL |
| WIN_RATE_STANDARD | 0.4350 | [0.4500, 0.5500] | FAIL |
| WIN_RATE_HARD | 0.4667 | [0.2000, 0.3500] | FAIL |
| WIN_RATE_OVERALL | 0.4960 | [0.4000, 0.6000] | PASS |
| AVG_END_TURN_OVERALL | 13.5810 | [14.0000, 20.0000] | FAIL |
| AVG_END_TURN_STANDARD | 15.5225 | [15.0000, 19.0000] | PASS |
| ATTRITION_RATE_OVERALL | 0.2150 | [0.1000, 0.3500] | PASS |
| RESOURCE_CV_GOLDLEAF | 0.3210 | [0.1800, 0.4500] | PASS |
| RESOURCE_CV_FIREOIL | 0.1835 | [0.1500, 0.4000] | PASS |
| RESOURCE_CV_ARMS | 0.2573 | [0.2000, 0.5500] | PASS |
| VICTORY_SHARE_OVERALL | 0.4960 | [0.3500, 0.6500] | PASS |
| DEFEAT_SHARE_OVERALL | 0.2890 | [0.3500, 0.6500] | FAIL |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.6774 | [0.0000, 0.7000] | PASS |

## Endgame Distribution

| End Type | Count | Share |
|---|---:|---:|
| victory | 496 | 0.4960 |
| defeat | 289 | 0.2890 |
| timeout | 215 | 0.2150 |

- Top victory path: `energyliberation` (`336` wins, share `0.6774`)
