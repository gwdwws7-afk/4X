# R3-04 KPI Evaluation (R48)

- Batch: `R3-04G-B007-R48`
- Profile: `r23-monopoly-guard`
- Runs: `1000`
- Source CSV: `C:\test\4X\production\evidence\r3\tuning\20260421_R3-04G_TARGETED-R48.csv`
- KPI Pass: `9/13`

## Round Parameter Delta

- Bias (Easy/Standard/Hard): `0.26/0.15/-0.01`
- Init Scale (Easy/Standard/Hard): `1.13/1.06/0.96`
- Social/Trade Init Scale: `0.24`
- Victory Threshold (Easy/Standard/Hard): `86/83/84`
- Pressure Offset: `0.22`; Conflict Base/TurnScale: `0.400/0.013`
- Delta Scale/Noise Scale: `0.96/2.04`
- Early Suppression: `turn<= 7` with `positive*0.45`
- Shock: `chance=0.43`, `magnitude=+/-34`

## KPI Snapshot

| KPI | Actual | Target | Status |
|---|---:|---:|---|
| WIN_RATE_EASY | 0.7100 | [0.6500, 0.8000] | PASS |
| WIN_RATE_STANDARD | 0.5250 | [0.4500, 0.5500] | PASS |
| WIN_RATE_HARD | 0.0433 | [0.2000, 0.3500] | FAIL |
| WIN_RATE_OVERALL | 0.4360 | [0.4000, 0.6000] | PASS |
| AVG_END_TURN_OVERALL | 13.2430 | [14.0000, 20.0000] | FAIL |
| AVG_END_TURN_STANDARD | 14.8525 | [15.0000, 19.0000] | FAIL |
| ATTRITION_RATE_OVERALL | 0.1730 | [0.1000, 0.3500] | PASS |
| RESOURCE_CV_GOLDLEAF | 0.3575 | [0.1800, 0.4500] | PASS |
| RESOURCE_CV_FIREOIL | 0.1995 | [0.1500, 0.4000] | PASS |
| RESOURCE_CV_ARMS | 0.2842 | [0.2000, 0.5500] | PASS |
| VICTORY_SHARE_OVERALL | 0.4360 | [0.3500, 0.6500] | PASS |
| DEFEAT_SHARE_OVERALL | 0.3910 | [0.3500, 0.6500] | PASS |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.8624 | [0.0000, 0.7000] | FAIL |

## Endgame Distribution

| End Type | Count | Share |
|---|---:|---:|
| victory | 436 | 0.4360 |
| defeat | 391 | 0.3910 |
| timeout | 173 | 0.1730 |

- Top victory path: `energyliberation` (`376` wins, share `0.8624`)
