# R3-04 KPI Evaluation (R214)

- Batch: `R3-04AG-B001-R214`
- Profile: `r212-hard-defeat-recover-a`
- Runs: `1000`
- Source CSV: `C:\test\4X\production\evidence\r3\tuning\20260423_R3-04AG_TARGETED-R214.csv`
- KPI Pass: `12/13`

## Round Parameter Delta

- Bias (Easy/Standard/Hard): `0.27/0.16/0.05`
- Init Scale (Easy/Standard/Hard): `1.15/1.11/1.09`
- Social/Trade Init Scale: `0.15`
- Victory Threshold (Easy/Standard/Hard): `85/83/81`
- Pressure Offset/TurnScale: `0.24/0.247`; Conflict Base/TurnScale: `0.397/0.015`
- Delta Scale/Noise Scale: `0.96/1.96`
- Early Suppression: `turn<= 14` with `positive*0.33`
- Shock: `chance=0.31`, `magnitude=+/-24`
- Late Escalation: `startTurn=13`, `pressure+=0.094`, `conflict+=0.069`, `loss+=0.118`, `capture-=0.038`

## KPI Snapshot

| KPI | Actual | Target | Status |
|---|---:|---:|---|
| WIN_RATE_EASY | 0.7100 | [0.6500, 0.8000] | PASS |
| WIN_RATE_STANDARD | 0.4525 | [0.4500, 0.5500] | PASS |
| WIN_RATE_HARD | 0.2333 | [0.2000, 0.3500] | PASS |
| WIN_RATE_OVERALL | 0.4640 | [0.4000, 0.6000] | PASS |
| AVG_END_TURN_OVERALL | 14.3400 | [14.0000, 20.0000] | PASS |
| AVG_END_TURN_STANDARD | 15.8225 | [15.0000, 19.0000] | PASS |
| ATTRITION_RATE_OVERALL | 0.1960 | [0.1000, 0.3500] | PASS |
| RESOURCE_CV_GOLDLEAF | 0.4040 | [0.1800, 0.4500] | PASS |
| RESOURCE_CV_FIREOIL | 0.1572 | [0.1500, 0.4000] | PASS |
| RESOURCE_CV_ARMS | 0.2149 | [0.2000, 0.5500] | PASS |
| VICTORY_SHARE_OVERALL | 0.4640 | [0.3500, 0.6500] | PASS |
| DEFEAT_SHARE_OVERALL | 0.3400 | [0.3500, 0.6500] | FAIL |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.6056 | [0.0000, 0.7000] | PASS |

## Endgame Distribution

| End Type | Count | Share |
|---|---:|---:|
| victory | 464 | 0.4640 |
| defeat | 340 | 0.3400 |
| timeout | 196 | 0.1960 |

- Top victory path: `energyliberation` (`281` wins, share `0.6056`)
