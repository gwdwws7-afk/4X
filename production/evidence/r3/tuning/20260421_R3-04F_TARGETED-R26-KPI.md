# R3-04 KPI Evaluation (R26)

- Batch: `R3-04F-B001-R26`
- Profile: `defeat-share-balance`
- Runs: `1000`
- Source CSV: `C:\test\4X\production\evidence\r3\tuning\20260421_R3-04F_TARGETED-R26.csv`
- KPI Pass: `6/13`

## Round Parameter Delta

- Bias (Easy/Standard/Hard): `0.25/0.13/-0.06`
- Init Scale (Easy/Standard/Hard): `1.12/1.02/0.92`
- Social/Trade Init Scale: `0.24`
- Victory Threshold (Easy/Standard/Hard): `86/83/84`
- Pressure Offset: `0.23`; Conflict Base/TurnScale: `0.390/0.013`
- Delta Scale/Noise Scale: `0.97/2.05`
- Early Suppression: `turn<= 6` with `positive*0.48`
- Shock: `chance=0.42`, `magnitude=+/-34`

## KPI Snapshot

| KPI | Actual | Target | Status |
|---|---:|---:|---|
| WIN_RATE_EASY | 0.6367 | [0.6500, 0.8000] | FAIL |
| WIN_RATE_STANDARD | 0.3700 | [0.4500, 0.5500] | FAIL |
| WIN_RATE_HARD | 0.0100 | [0.2000, 0.3500] | FAIL |
| WIN_RATE_OVERALL | 0.3420 | [0.4000, 0.6000] | FAIL |
| AVG_END_TURN_OVERALL | 13.0780 | [14.0000, 20.0000] | FAIL |
| AVG_END_TURN_STANDARD | 15.7300 | [15.0000, 19.0000] | PASS |
| ATTRITION_RATE_OVERALL | 0.2000 | [0.1000, 0.3500] | PASS |
| RESOURCE_CV_GOLDLEAF | 0.3751 | [0.1800, 0.4500] | PASS |
| RESOURCE_CV_FIREOIL | 0.2018 | [0.1500, 0.4000] | PASS |
| RESOURCE_CV_ARMS | 0.2653 | [0.2000, 0.5500] | PASS |
| VICTORY_SHARE_OVERALL | 0.3420 | [0.3500, 0.6500] | FAIL |
| DEFEAT_SHARE_OVERALL | 0.4580 | [0.3500, 0.6500] | PASS |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.9240 | [0.0000, 0.7000] | FAIL |

## Endgame Distribution

| End Type | Count | Share |
|---|---:|---:|
| victory | 342 | 0.3420 |
| defeat | 458 | 0.4580 |
| timeout | 200 | 0.2000 |

- Top victory path: `energyliberation` (`316` wins, share `0.9240`)
