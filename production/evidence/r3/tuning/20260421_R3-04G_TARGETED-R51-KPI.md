# R3-04 KPI Evaluation (R51)

- Batch: `R3-04G-B008-R51`
- Profile: `r23-monopoly-guard-b`
- Runs: `1000`
- Source CSV: `C:\test\4X\production\evidence\r3\tuning\20260421_R3-04G_TARGETED-R51.csv`
- KPI Pass: `10/13`

## Round Parameter Delta

- Bias (Easy/Standard/Hard): `0.23/0.14/0.05`
- Init Scale (Easy/Standard/Hard): `1.10/1.05/1.01`
- Social/Trade Init Scale: `0.24`
- Victory Threshold (Easy/Standard/Hard): `86/83/81`
- Pressure Offset: `0.22`; Conflict Base/TurnScale: `0.380/0.012`
- Delta Scale/Noise Scale: `0.97/2.03`
- Early Suppression: `turn<= 6` with `positive*0.48`
- Shock: `chance=0.41`, `magnitude=+/-32`

## KPI Snapshot

| KPI | Actual | Target | Status |
|---|---:|---:|---|
| WIN_RATE_EASY | 0.6100 | [0.6500, 0.8000] | FAIL |
| WIN_RATE_STANDARD | 0.4825 | [0.4500, 0.5500] | PASS |
| WIN_RATE_HARD | 0.2033 | [0.2000, 0.3500] | PASS |
| WIN_RATE_OVERALL | 0.4370 | [0.4000, 0.6000] | PASS |
| AVG_END_TURN_OVERALL | 14.4810 | [14.0000, 20.0000] | PASS |
| AVG_END_TURN_STANDARD | 15.5950 | [15.0000, 19.0000] | PASS |
| ATTRITION_RATE_OVERALL | 0.2360 | [0.1000, 0.3500] | PASS |
| RESOURCE_CV_GOLDLEAF | 0.3466 | [0.1800, 0.4500] | PASS |
| RESOURCE_CV_FIREOIL | 0.1964 | [0.1500, 0.4000] | PASS |
| RESOURCE_CV_ARMS | 0.2580 | [0.2000, 0.5500] | PASS |
| VICTORY_SHARE_OVERALL | 0.4370 | [0.3500, 0.6500] | PASS |
| DEFEAT_SHARE_OVERALL | 0.3270 | [0.3500, 0.6500] | FAIL |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.8284 | [0.0000, 0.7000] | FAIL |

## Endgame Distribution

| End Type | Count | Share |
|---|---:|---:|
| victory | 437 | 0.4370 |
| defeat | 327 | 0.3270 |
| timeout | 236 | 0.2360 |

- Top victory path: `energyliberation` (`362` wins, share `0.8284`)
