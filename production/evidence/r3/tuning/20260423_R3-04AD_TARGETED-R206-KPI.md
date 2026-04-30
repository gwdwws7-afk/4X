# R3-04 KPI Evaluation (R206)

- Batch: `R3-04AD-B001-R206`
- Profile: `r204-variance-close-b`
- Runs: `1000`
- Source CSV: `C:\test\4X\production\evidence\r3\tuning\20260423_R3-04AD_TARGETED-R206.csv`
- KPI Pass: `10/13`

## Round Parameter Delta

- Bias (Easy/Standard/Hard): `0.27/0.16/0.05`
- Init Scale (Easy/Standard/Hard): `1.15/1.11/1.09`
- Social/Trade Init Scale: `0.15`
- Victory Threshold (Easy/Standard/Hard): `85/83/81`
- Pressure Offset/TurnScale: `0.24/0.247`; Conflict Base/TurnScale: `0.397/0.015`
- Delta Scale/Noise Scale: `0.96/1.94`
- Early Suppression: `turn<= 14` with `positive*0.33`
- Shock: `chance=0.30`, `magnitude=+/-22`
- Late Escalation: `startTurn=14`, `pressure+=0.094`, `conflict+=0.066`, `loss+=0.107`, `capture-=0.036`

## KPI Snapshot

| KPI | Actual | Target | Status |
|---|---:|---:|---|
| WIN_RATE_EASY | 0.6033 | [0.6500, 0.8000] | FAIL |
| WIN_RATE_STANDARD | 0.4475 | [0.4500, 0.5500] | FAIL |
| WIN_RATE_HARD | 0.1833 | [0.2000, 0.3500] | FAIL |
| WIN_RATE_OVERALL | 0.4150 | [0.4000, 0.6000] | PASS |
| AVG_END_TURN_OVERALL | 15.0470 | [14.0000, 20.0000] | PASS |
| AVG_END_TURN_STANDARD | 16.3050 | [15.0000, 19.0000] | PASS |
| ATTRITION_RATE_OVERALL | 0.2300 | [0.1000, 0.3500] | PASS |
| RESOURCE_CV_GOLDLEAF | 0.4163 | [0.1800, 0.4500] | PASS |
| RESOURCE_CV_FIREOIL | 0.1500 | [0.1500, 0.4000] | PASS |
| RESOURCE_CV_ARMS | 0.2025 | [0.2000, 0.5500] | PASS |
| VICTORY_SHARE_OVERALL | 0.4150 | [0.3500, 0.6500] | PASS |
| DEFEAT_SHARE_OVERALL | 0.3550 | [0.3500, 0.6500] | PASS |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.5855 | [0.0000, 0.7000] | PASS |

## Endgame Distribution

| End Type | Count | Share |
|---|---:|---:|
| victory | 415 | 0.4150 |
| defeat | 355 | 0.3550 |
| timeout | 230 | 0.2300 |

- Top victory path: `energyliberation` (`243` wins, share `0.5855`)
