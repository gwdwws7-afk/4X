# R3-04 KPI Evaluation (R210)

- Batch: `R3-04AE-B001-R210`
- Profile: `r205-converge-close-c`
- Runs: `1000`
- Source CSV: `C:\test\4X\production\evidence\r3\tuning\20260423_R3-04AE_TARGETED-R210.csv`
- KPI Pass: `11/13`

## Round Parameter Delta

- Bias (Easy/Standard/Hard): `0.27/0.16/0.05`
- Init Scale (Easy/Standard/Hard): `1.15/1.12/1.09`
- Social/Trade Init Scale: `0.15`
- Victory Threshold (Easy/Standard/Hard): `85/83/81`
- Pressure Offset/TurnScale: `0.24/0.252`; Conflict Base/TurnScale: `0.397/0.016`
- Delta Scale/Noise Scale: `0.96/1.96`
- Early Suppression: `turn<= 14` with `positive*0.33`
- Shock: `chance=0.33`, `magnitude=+/-24`
- Late Escalation: `startTurn=12`, `pressure+=0.094`, `conflict+=0.071`, `loss+=0.121`, `capture-=0.039`

## KPI Snapshot

| KPI | Actual | Target | Status |
|---|---:|---:|---|
| WIN_RATE_EASY | 0.7167 | [0.6500, 0.8000] | PASS |
| WIN_RATE_STANDARD | 0.4225 | [0.4500, 0.5500] | FAIL |
| WIN_RATE_HARD | 0.1967 | [0.2000, 0.3500] | FAIL |
| WIN_RATE_OVERALL | 0.4430 | [0.4000, 0.6000] | PASS |
| AVG_END_TURN_OVERALL | 14.4570 | [14.0000, 20.0000] | PASS |
| AVG_END_TURN_STANDARD | 15.9100 | [15.0000, 19.0000] | PASS |
| ATTRITION_RATE_OVERALL | 0.1860 | [0.1000, 0.3500] | PASS |
| RESOURCE_CV_GOLDLEAF | 0.4122 | [0.1800, 0.4500] | PASS |
| RESOURCE_CV_FIREOIL | 0.1653 | [0.1500, 0.4000] | PASS |
| RESOURCE_CV_ARMS | 0.2180 | [0.2000, 0.5500] | PASS |
| VICTORY_SHARE_OVERALL | 0.4430 | [0.3500, 0.6500] | PASS |
| DEFEAT_SHARE_OVERALL | 0.3710 | [0.3500, 0.6500] | PASS |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.6185 | [0.0000, 0.7000] | PASS |

## Endgame Distribution

| End Type | Count | Share |
|---|---:|---:|
| victory | 443 | 0.4430 |
| defeat | 371 | 0.3710 |
| timeout | 186 | 0.1860 |

- Top victory path: `energyliberation` (`274` wins, share `0.6185`)
