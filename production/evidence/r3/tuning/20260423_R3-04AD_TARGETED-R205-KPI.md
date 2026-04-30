# R3-04 KPI Evaluation (R205)

- Batch: `R3-04AD-B001-R205`
- Profile: `r204-variance-close-a`
- Runs: `1000`
- Source CSV: `C:\test\4X\production\evidence\r3\tuning\20260423_R3-04AD_TARGETED-R205.csv`
- KPI Pass: `11/13`

## Round Parameter Delta

- Bias (Easy/Standard/Hard): `0.27/0.16/0.05`
- Init Scale (Easy/Standard/Hard): `1.15/1.11/1.09`
- Social/Trade Init Scale: `0.15`
- Victory Threshold (Easy/Standard/Hard): `85/83/81`
- Pressure Offset/TurnScale: `0.24/0.247`; Conflict Base/TurnScale: `0.397/0.015`
- Delta Scale/Noise Scale: `0.96/1.96`
- Early Suppression: `turn<= 14` with `positive*0.33`
- Shock: `chance=0.31`, `magnitude=+/-24`
- Late Escalation: `startTurn=14`, `pressure+=0.094`, `conflict+=0.066`, `loss+=0.107`, `capture-=0.036`

## KPI Snapshot

| KPI | Actual | Target | Status |
|---|---:|---:|---|
| WIN_RATE_EASY | 0.6267 | [0.6500, 0.8000] | FAIL |
| WIN_RATE_STANDARD | 0.4500 | [0.4500, 0.5500] | PASS |
| WIN_RATE_HARD | 0.1833 | [0.2000, 0.3500] | FAIL |
| WIN_RATE_OVERALL | 0.4230 | [0.4000, 0.6000] | PASS |
| AVG_END_TURN_OVERALL | 14.8900 | [14.0000, 20.0000] | PASS |
| AVG_END_TURN_STANDARD | 16.0000 | [15.0000, 19.0000] | PASS |
| ATTRITION_RATE_OVERALL | 0.2190 | [0.1000, 0.3500] | PASS |
| RESOURCE_CV_GOLDLEAF | 0.4219 | [0.1800, 0.4500] | PASS |
| RESOURCE_CV_FIREOIL | 0.1625 | [0.1500, 0.4000] | PASS |
| RESOURCE_CV_ARMS | 0.2199 | [0.2000, 0.5500] | PASS |
| VICTORY_SHARE_OVERALL | 0.4230 | [0.3500, 0.6500] | PASS |
| DEFEAT_SHARE_OVERALL | 0.3580 | [0.3500, 0.6500] | PASS |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.5981 | [0.0000, 0.7000] | PASS |

## Endgame Distribution

| End Type | Count | Share |
|---|---:|---:|
| victory | 423 | 0.4230 |
| defeat | 358 | 0.3580 |
| timeout | 219 | 0.2190 |

- Top victory path: `energyliberation` (`253` wins, share `0.5981`)
