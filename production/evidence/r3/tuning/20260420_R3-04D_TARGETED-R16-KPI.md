# R3-04 KPI Evaluation (R16)

- Batch: `R3-04D-B001-R16`
- Profile: `winlift-window`
- Runs: `1000`
- Source CSV: `C:\test\4X\production\evidence\r3\tuning\20260420_R3-04D_TARGETED-R16.csv`
- KPI Pass: `5/13`

## Round Parameter Delta

- Bias (Easy/Standard/Hard): `0.20/0.14/0.02`
- Init Scale (Easy/Standard/Hard): `1.08/1.05/1.00`
- Social/Trade Init Scale: `0.42`
- Victory Threshold (Easy/Standard/Hard): `86/82/80`
- Pressure Offset: `0.16`; Conflict Base/TurnScale: `0.340/0.012`
- Delta Scale/Noise Scale: `0.95/1.85`
- Early Suppression: `turn<= 6` with `positive*0.58`
- Shock: `chance=0.34`, `magnitude=+/-28`

## KPI Snapshot

| KPI | Actual | Target | Status |
|---|---:|---:|---|
| WIN_RATE_EASY | 0.7433 | [0.6500, 0.8000] | PASS |
| WIN_RATE_STANDARD | 0.6400 | [0.4500, 0.5500] | FAIL |
| WIN_RATE_HARD | 0.4300 | [0.2000, 0.3500] | FAIL |
| WIN_RATE_OVERALL | 0.6080 | [0.4000, 0.6000] | FAIL |
| AVG_END_TURN_OVERALL | 12.1500 | [14.0000, 20.0000] | FAIL |
| AVG_END_TURN_STANDARD | 13.2000 | [15.0000, 19.0000] | FAIL |
| ATTRITION_RATE_OVERALL | 0.1460 | [0.1000, 0.3500] | PASS |
| RESOURCE_CV_GOLDLEAF | 0.2359 | [0.1800, 0.4500] | PASS |
| RESOURCE_CV_FIREOIL | 0.1505 | [0.1500, 0.4000] | PASS |
| RESOURCE_CV_ARMS | 0.1854 | [0.2000, 0.5500] | FAIL |
| VICTORY_SHARE_OVERALL | 0.6080 | [0.3500, 0.6500] | PASS |
| DEFEAT_SHARE_OVERALL | 0.2460 | [0.3500, 0.6500] | FAIL |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.8240 | [0.0000, 0.7000] | FAIL |

## Endgame Distribution

| End Type | Count | Share |
|---|---:|---:|
| victory | 608 | 0.6080 |
| defeat | 246 | 0.2460 |
| timeout | 146 | 0.1460 |

- Top victory path: `energyliberation` (`501` wins, share `0.8240`)
