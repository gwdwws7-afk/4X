# R3-04 KPI Evaluation (R11)

- Batch: `R3-04B-B003-R11`
- Profile: `standard-revival-b`
- Runs: `1000`
- Source CSV: `C:\test\4X\production\evidence\r3\tuning\20260420_R3-04B_TARGETED-R11.csv`
- KPI Pass: `4/13`

## Round Parameter Delta

- Bias (Easy/Standard/Hard): `0.04/0.12/0.01`
- Init Scale (Easy/Standard/Hard): `0.90/1.00/1.00`
- Social/Trade Init Scale: `0.62`
- Victory Threshold (Easy/Standard/Hard): `92/83/78`
- Pressure Offset: `0.12`; Conflict Base/TurnScale: `0.340/0.011`
- Delta Scale/Noise Scale: `0.98/1.45`
- Early Suppression: `turn<= 6` with `positive*0.58`
- Shock: `chance=0.20`, `magnitude=+/-18`

## KPI Snapshot

| KPI | Actual | Target | Status |
|---|---:|---:|---|
| WIN_RATE_EASY | 0.1833 | [0.6500, 0.8000] | FAIL |
| WIN_RATE_STANDARD | 0.7375 | [0.4500, 0.5500] | FAIL |
| WIN_RATE_HARD | 0.4100 | [0.2000, 0.3500] | FAIL |
| WIN_RATE_OVERALL | 0.4730 | [0.4000, 0.6000] | PASS |
| AVG_END_TURN_OVERALL | 12.9920 | [14.0000, 20.0000] | FAIL |
| AVG_END_TURN_STANDARD | 11.2400 | [15.0000, 19.0000] | FAIL |
| ATTRITION_RATE_OVERALL | 0.2370 | [0.1000, 0.3500] | PASS |
| RESOURCE_CV_GOLDLEAF | 0.1953 | [0.1800, 0.4500] | PASS |
| RESOURCE_CV_FIREOIL | 0.1112 | [0.1500, 0.4000] | FAIL |
| RESOURCE_CV_ARMS | 0.1307 | [0.2000, 0.5500] | FAIL |
| VICTORY_SHARE_OVERALL | 0.4730 | [0.3500, 0.6500] | PASS |
| DEFEAT_SHARE_OVERALL | 0.2900 | [0.3500, 0.6500] | FAIL |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.8753 | [0.0000, 0.7000] | FAIL |

## Endgame Distribution

| End Type | Count | Share |
|---|---:|---:|
| victory | 473 | 0.4730 |
| defeat | 290 | 0.2900 |
| timeout | 237 | 0.2370 |

- Top victory path: `energyliberation` (`414` wins, share `0.8753`)
