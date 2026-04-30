# R3-04 KPI Evaluation (V01)

- Batch: `R3-05-B002-R82-V01`
- Profile: `r79-pace-backpull-a-validation`
- Runs: `1000`
- Source CSV: `C:\test\4X\production\evidence\r3\validation\20260421_R3-05_VALIDATION-R82-V01.csv`
- KPI Pass: `10/13`

## Round Parameter Delta

- Bias (Easy/Standard/Hard): `0.26/0.14/0.05`
- Init Scale (Easy/Standard/Hard): `1.13/1.05/1.03`
- Social/Trade Init Scale: `0.16`
- Victory Threshold (Easy/Standard/Hard): `85/83/81`
- Pressure Offset: `0.24`; Conflict Base/TurnScale: `0.400/0.013`
- Delta Scale/Noise Scale: `0.96/2.05`
- Early Suppression: `turn<= 7` with `positive*0.47`
- Shock: `chance=0.41`, `magnitude=+/-33`

## KPI Snapshot

| KPI | Actual | Target | Status |
|---|---:|---:|---|
| WIN_RATE_EASY | 0.7200 | [0.6500, 0.8000] | PASS |
| WIN_RATE_STANDARD | 0.5050 | [0.4500, 0.5500] | PASS |
| WIN_RATE_HARD | 0.2567 | [0.2000, 0.3500] | PASS |
| WIN_RATE_OVERALL | 0.4950 | [0.4000, 0.6000] | PASS |
| AVG_END_TURN_OVERALL | 13.2570 | [14.0000, 20.0000] | FAIL |
| AVG_END_TURN_STANDARD | 14.8325 | [15.0000, 19.0000] | FAIL |
| ATTRITION_RATE_OVERALL | 0.1590 | [0.1000, 0.3500] | PASS |
| RESOURCE_CV_GOLDLEAF | 0.3394 | [0.1800, 0.4500] | PASS |
| RESOURCE_CV_FIREOIL | 0.1887 | [0.1500, 0.4000] | PASS |
| RESOURCE_CV_ARMS | 0.2554 | [0.2000, 0.5500] | PASS |
| VICTORY_SHARE_OVERALL | 0.4950 | [0.3500, 0.6500] | PASS |
| DEFEAT_SHARE_OVERALL | 0.3460 | [0.3500, 0.6500] | FAIL |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.6788 | [0.0000, 0.7000] | PASS |

## Endgame Distribution

| End Type | Count | Share |
|---|---:|---:|
| victory | 495 | 0.4950 |
| defeat | 346 | 0.3460 |
| timeout | 159 | 0.1590 |

- Top victory path: `energyliberation` (`336` wins, share `0.6788`)
