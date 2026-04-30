# R3-04 KPI Evaluation (R197)

- Batch: `R3-04AA-B001-R197`
- Profile: `r194-pace-floor-b`
- Runs: `1000`
- Source CSV: `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04AA_TARGETED-R197.csv`
- KPI Pass: `12/13`

## Round Parameter Delta

- Bias (Easy/Standard/Hard): `0.27/0.16/0.05`
- Init Scale (Easy/Standard/Hard): `1.15/1.11/1.08`
- Social/Trade Init Scale: `0.15`
- Victory Threshold (Easy/Standard/Hard): `85/83/81`
- Pressure Offset/TurnScale: `0.24/0.247`; Conflict Base/TurnScale: `0.398/0.015`
- Delta Scale/Noise Scale: `0.96/2.02`
- Early Suppression: `turn<= 14` with `positive*0.33`
- Shock: `chance=0.38`, `magnitude=+/-30`
- Late Escalation: `startTurn=15`, `pressure+=0.095`, `conflict+=0.067`, `loss+=0.108`, `capture-=0.037`

## KPI Snapshot

| KPI | Actual | Target | Status |
|---|---:|---:|---|
| WIN_RATE_EASY | 0.7200 | [0.6500, 0.8000] | PASS |
| WIN_RATE_STANDARD | 0.4500 | [0.4500, 0.5500] | PASS |
| WIN_RATE_HARD | 0.2100 | [0.2000, 0.3500] | PASS |
| WIN_RATE_OVERALL | 0.4590 | [0.4000, 0.6000] | PASS |
| AVG_END_TURN_OVERALL | 13.7630 | [14.0000, 20.0000] | FAIL |
| AVG_END_TURN_STANDARD | 15.3300 | [15.0000, 19.0000] | PASS |
| ATTRITION_RATE_OVERALL | 0.1730 | [0.1000, 0.3500] | PASS |
| RESOURCE_CV_GOLDLEAF | 0.4206 | [0.1800, 0.4500] | PASS |
| RESOURCE_CV_FIREOIL | 0.1926 | [0.1500, 0.4000] | PASS |
| RESOURCE_CV_ARMS | 0.2705 | [0.2000, 0.5500] | PASS |
| VICTORY_SHARE_OVERALL | 0.4590 | [0.3500, 0.6500] | PASS |
| DEFEAT_SHARE_OVERALL | 0.3680 | [0.3500, 0.6500] | PASS |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.6187 | [0.0000, 0.7000] | PASS |

## Endgame Distribution

| End Type | Count | Share |
|---|---:|---:|
| victory | 459 | 0.4590 |
| defeat | 368 | 0.3680 |
| timeout | 173 | 0.1730 |

- Top victory path: `energyliberation` (`284` wins, share `0.6187`)
