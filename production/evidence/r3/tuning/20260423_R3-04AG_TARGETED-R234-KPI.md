# R3-04 KPI Evaluation (R234)

- Batch: `R3-04AG-B004-R234`
- Profile: `r99-gate-close-c`
- Runs: `1000`
- Source CSV: `C:\test\4X\production\evidence\r3\tuning\20260423_R3-04AG_TARGETED-R234.csv`
- KPI Pass: `8/13`

## Round Parameter Delta

- Bias (Easy/Standard/Hard): `0.26/0.15/0.05`
- Init Scale (Easy/Standard/Hard): `1.13/1.07/1.03`
- Social/Trade Init Scale: `0.16`
- Victory Threshold (Easy/Standard/Hard): `87/85/82`
- Pressure Offset/TurnScale: `0.24/0.223`; Conflict Base/TurnScale: `0.402/0.013`
- Delta Scale/Noise Scale: `0.96/2.02`
- Early Suppression: `turn<= 10` with `positive*0.39`
- Shock: `chance=0.38`, `magnitude=+/-30`
- Late Escalation: `startTurn=0`, `pressure+=0.000`, `conflict+=0.000`, `loss+=0.000`, `capture-=0.000`

## KPI Snapshot

| KPI | Actual | Target | Status |
|---|---:|---:|---|
| WIN_RATE_EASY | 0.6333 | [0.6500, 0.8000] | FAIL |
| WIN_RATE_STANDARD | 0.3525 | [0.4500, 0.5500] | FAIL |
| WIN_RATE_HARD | 0.1167 | [0.2000, 0.3500] | FAIL |
| WIN_RATE_OVERALL | 0.3660 | [0.4000, 0.6000] | FAIL |
| AVG_END_TURN_OVERALL | 14.8550 | [14.0000, 20.0000] | PASS |
| AVG_END_TURN_STANDARD | 16.7025 | [15.0000, 19.0000] | PASS |
| ATTRITION_RATE_OVERALL | 0.2260 | [0.1000, 0.3500] | PASS |
| RESOURCE_CV_GOLDLEAF | 0.4023 | [0.1800, 0.4500] | PASS |
| RESOURCE_CV_FIREOIL | 0.1962 | [0.1500, 0.4000] | PASS |
| RESOURCE_CV_ARMS | 0.2622 | [0.2000, 0.5500] | PASS |
| VICTORY_SHARE_OVERALL | 0.3660 | [0.3500, 0.6500] | PASS |
| DEFEAT_SHARE_OVERALL | 0.4080 | [0.3500, 0.6500] | PASS |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.8279 | [0.0000, 0.7000] | FAIL |

## Endgame Distribution

| End Type | Count | Share |
|---|---:|---:|
| victory | 366 | 0.3660 |
| defeat | 408 | 0.4080 |
| timeout | 226 | 0.2260 |

- Top victory path: `energyliberation` (`303` wins, share `0.8279`)
