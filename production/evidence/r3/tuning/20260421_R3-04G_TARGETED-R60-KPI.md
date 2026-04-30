# R3-04 KPI Evaluation (R60)

- Batch: `R3-04G-B011-R60`
- Profile: `energy-suppress-axis-boost-c`
- Runs: `1000`
- Source CSV: `C:\test\4X\production\evidence\r3\tuning\20260421_R3-04G_TARGETED-R60.csv`
- KPI Pass: `8/13`

## Round Parameter Delta

- Bias (Easy/Standard/Hard): `0.25/0.14/0.04`
- Init Scale (Easy/Standard/Hard): `1.12/1.05/0.99`
- Social/Trade Init Scale: `0.20`
- Victory Threshold (Easy/Standard/Hard): `86/83/82`
- Pressure Offset: `0.24`; Conflict Base/TurnScale: `0.410/0.013`
- Delta Scale/Noise Scale: `0.96/2.06`
- Early Suppression: `turn<= 7` with `positive*0.47`
- Shock: `chance=0.41`, `magnitude=+/-33`

## KPI Snapshot

| KPI | Actual | Target | Status |
|---|---:|---:|---|
| WIN_RATE_EASY | 0.5933 | [0.6500, 0.8000] | FAIL |
| WIN_RATE_STANDARD | 0.4550 | [0.4500, 0.5500] | PASS |
| WIN_RATE_HARD | 0.1033 | [0.2000, 0.3500] | FAIL |
| WIN_RATE_OVERALL | 0.3910 | [0.4000, 0.6000] | FAIL |
| AVG_END_TURN_OVERALL | 13.8830 | [14.0000, 20.0000] | FAIL |
| AVG_END_TURN_STANDARD | 15.0025 | [15.0000, 19.0000] | PASS |
| ATTRITION_RATE_OVERALL | 0.2070 | [0.1000, 0.3500] | PASS |
| RESOURCE_CV_GOLDLEAF | 0.3644 | [0.1800, 0.4500] | PASS |
| RESOURCE_CV_FIREOIL | 0.1952 | [0.1500, 0.4000] | PASS |
| RESOURCE_CV_ARMS | 0.2776 | [0.2000, 0.5500] | PASS |
| VICTORY_SHARE_OVERALL | 0.3910 | [0.3500, 0.6500] | PASS |
| DEFEAT_SHARE_OVERALL | 0.4020 | [0.3500, 0.6500] | PASS |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.7417 | [0.0000, 0.7000] | FAIL |

## Endgame Distribution

| End Type | Count | Share |
|---|---:|---:|
| victory | 391 | 0.3910 |
| defeat | 402 | 0.4020 |
| timeout | 207 | 0.2070 |

- Top victory path: `energyliberation` (`290` wins, share `0.7417`)
