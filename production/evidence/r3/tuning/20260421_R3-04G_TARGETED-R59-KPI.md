# R3-04 KPI Evaluation (R59)

- Batch: `R3-04G-B011-R59`
- Profile: `energy-suppress-axis-boost-b`
- Runs: `1000`
- Source CSV: `C:\test\4X\production\evidence\r3\tuning\20260421_R3-04G_TARGETED-R59.csv`
- KPI Pass: `10/13`

## Round Parameter Delta

- Bias (Easy/Standard/Hard): `0.25/0.14/0.03`
- Init Scale (Easy/Standard/Hard): `1.12/1.05/0.99`
- Social/Trade Init Scale: `0.18`
- Victory Threshold (Easy/Standard/Hard): `86/83/81`
- Pressure Offset: `0.23`; Conflict Base/TurnScale: `0.390/0.013`
- Delta Scale/Noise Scale: `0.97/2.03`
- Early Suppression: `turn<= 7` with `positive*0.47`
- Shock: `chance=0.40`, `magnitude=+/-32`

## KPI Snapshot

| KPI | Actual | Target | Status |
|---|---:|---:|---|
| WIN_RATE_EASY | 0.6467 | [0.6500, 0.8000] | FAIL |
| WIN_RATE_STANDARD | 0.4350 | [0.4500, 0.5500] | FAIL |
| WIN_RATE_HARD | 0.2233 | [0.2000, 0.3500] | PASS |
| WIN_RATE_OVERALL | 0.4350 | [0.4000, 0.6000] | PASS |
| AVG_END_TURN_OVERALL | 14.3540 | [14.0000, 20.0000] | PASS |
| AVG_END_TURN_STANDARD | 16.0075 | [15.0000, 19.0000] | PASS |
| ATTRITION_RATE_OVERALL | 0.2060 | [0.1000, 0.3500] | PASS |
| RESOURCE_CV_GOLDLEAF | 0.3578 | [0.1800, 0.4500] | PASS |
| RESOURCE_CV_FIREOIL | 0.1948 | [0.1500, 0.4000] | PASS |
| RESOURCE_CV_ARMS | 0.2590 | [0.2000, 0.5500] | PASS |
| VICTORY_SHARE_OVERALL | 0.4350 | [0.3500, 0.6500] | PASS |
| DEFEAT_SHARE_OVERALL | 0.3590 | [0.3500, 0.6500] | PASS |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.7034 | [0.0000, 0.7000] | FAIL |

## Endgame Distribution

| End Type | Count | Share |
|---|---:|---:|
| victory | 435 | 0.4350 |
| defeat | 359 | 0.3590 |
| timeout | 206 | 0.2060 |

- Top victory path: `energyliberation` (`306` wins, share `0.7034`)
