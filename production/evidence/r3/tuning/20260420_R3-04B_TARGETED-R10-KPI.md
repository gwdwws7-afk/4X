# R3-04 KPI Evaluation (R10)

- Batch: `R3-04B-B003-R10`
- Profile: `standard-revival-a`
- Runs: `1000`
- Source CSV: `C:\test\4X\production\evidence\r3\tuning\20260420_R3-04B_TARGETED-R10.csv`
- KPI Pass: `4/13`

## Round Parameter Delta

- Bias (Easy/Standard/Hard): `0.06/0.09/0.00`
- Init Scale (Easy/Standard/Hard): `0.92/0.98/0.98`
- Social/Trade Init Scale: `0.66`
- Victory Threshold (Easy/Standard/Hard): `94/84/79`
- Pressure Offset: `0.11`; Conflict Base/TurnScale: `0.320/0.010`
- Delta Scale/Noise Scale: `0.96/1.35`
- Early Suppression: `turn<= 6` with `positive*0.60`
- Shock: `chance=0.18`, `magnitude=+/-16`

## KPI Snapshot

| KPI | Actual | Target | Status |
|---|---:|---:|---|
| WIN_RATE_EASY | 0.2667 | [0.6500, 0.8000] | FAIL |
| WIN_RATE_STANDARD | 0.6475 | [0.4500, 0.5500] | FAIL |
| WIN_RATE_HARD | 0.3267 | [0.2000, 0.3500] | PASS |
| WIN_RATE_OVERALL | 0.4370 | [0.4000, 0.6000] | PASS |
| AVG_END_TURN_OVERALL | 13.5620 | [14.0000, 20.0000] | FAIL |
| AVG_END_TURN_STANDARD | 12.4525 | [15.0000, 19.0000] | FAIL |
| ATTRITION_RATE_OVERALL | 0.2680 | [0.1000, 0.3500] | PASS |
| RESOURCE_CV_GOLDLEAF | 0.1677 | [0.1800, 0.4500] | FAIL |
| RESOURCE_CV_FIREOIL | 0.1053 | [0.1500, 0.4000] | FAIL |
| RESOURCE_CV_ARMS | 0.1253 | [0.2000, 0.5500] | FAIL |
| VICTORY_SHARE_OVERALL | 0.4370 | [0.3500, 0.6500] | PASS |
| DEFEAT_SHARE_OVERALL | 0.2950 | [0.3500, 0.6500] | FAIL |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.8787 | [0.0000, 0.7000] | FAIL |

## Endgame Distribution

| End Type | Count | Share |
|---|---:|---:|
| victory | 437 | 0.4370 |
| defeat | 295 | 0.2950 |
| timeout | 268 | 0.2680 |

- Top victory path: `energyliberation` (`384` wins, share `0.8787`)
