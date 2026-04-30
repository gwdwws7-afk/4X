# R3-04 KPI Evaluation (R275)

- Batch: `R3-04AG-B010-R275`
- Profile: `r262-fine-closure-d`
- Runs: `1000`
- Source CSV: `C:\test\4X\production\evidence\r3\tuning\20260423_R3-04AG_TARGETED-R275.csv`
- KPI Pass: `13/13`

## Round Parameter Delta

- Bias (Easy/Standard/Hard): `0.27/0.16/0.05`
- Init Scale (Easy/Standard/Hard): `1.15/1.11/1.08`
- Social/Trade Init Scale: `0.15`
- Victory Threshold (Easy/Standard/Hard): `85/83/81`
- Pressure Offset/TurnScale: `0.24/0.247`; Conflict Base/TurnScale: `0.397/0.015`
- Delta Scale/Noise Scale: `0.96/2.02`
- Early Suppression: `turn<= 14` with `positive*0.33`
- Shock: `chance=0.38`, `magnitude=+/-30`
- Late Escalation: `startTurn=14`, `pressure+=0.094`, `conflict+=0.067`, `loss+=0.113`, `capture-=0.037`

## KPI Snapshot

| KPI | Actual | Target | Status |
|---|---:|---:|---|
| WIN_RATE_EASY | 0.7067 | [0.6500, 0.8000] | PASS |
| WIN_RATE_STANDARD | 0.4500 | [0.4500, 0.5500] | PASS |
| WIN_RATE_HARD | 0.2133 | [0.2000, 0.3500] | PASS |
| WIN_RATE_OVERALL | 0.4560 | [0.4000, 0.6000] | PASS |
| AVG_END_TURN_OVERALL | 14.1800 | [14.0000, 20.0000] | PASS |
| AVG_END_TURN_STANDARD | 15.2400 | [15.0000, 19.0000] | PASS |
| ATTRITION_RATE_OVERALL | 0.1750 | [0.1000, 0.3500] | PASS |
| RESOURCE_CV_GOLDLEAF | 0.4431 | [0.1800, 0.4500] | PASS |
| RESOURCE_CV_FIREOIL | 0.1997 | [0.1500, 0.4000] | PASS |
| RESOURCE_CV_ARMS | 0.2749 | [0.2000, 0.5500] | PASS |
| VICTORY_SHARE_OVERALL | 0.4560 | [0.3500, 0.6500] | PASS |
| DEFEAT_SHARE_OVERALL | 0.3690 | [0.3500, 0.6500] | PASS |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.6272 | [0.0000, 0.7000] | PASS |

## Endgame Distribution

| End Type | Count | Share |
|---|---:|---:|
| victory | 456 | 0.4560 |
| defeat | 369 | 0.3690 |
| timeout | 175 | 0.1750 |

- Top victory path: `energyliberation` (`286` wins, share `0.6272`)
