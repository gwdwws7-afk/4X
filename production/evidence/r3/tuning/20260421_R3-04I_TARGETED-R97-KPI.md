# R3-04 KPI Evaluation (R97)

- Batch: `R3-04I-B005-R97`
- Profile: `r82-early-suppress-a`
- Runs: `1000`
- Source CSV: `C:\test\4X\production\evidence\r3\tuning\20260421_R3-04I_TARGETED-R97.csv`
- KPI Pass: `11/13`

## Round Parameter Delta

- Bias (Easy/Standard/Hard): `0.26/0.15/0.05`
- Init Scale (Easy/Standard/Hard): `1.13/1.06/1.03`
- Social/Trade Init Scale: `0.16`
- Victory Threshold (Easy/Standard/Hard): `85/83/81`
- Pressure Offset: `0.24`; Conflict Base/TurnScale: `0.401/0.013`
- Delta Scale/Noise Scale: `0.96/2.03`
- Early Suppression: `turn<= 10` with `positive*0.40`
- Shock: `chance=0.39`, `magnitude=+/-31`

## KPI Snapshot

| KPI | Actual | Target | Status |
|---|---:|---:|---|
| WIN_RATE_EASY | 0.6967 | [0.6500, 0.8000] | PASS |
| WIN_RATE_STANDARD | 0.4850 | [0.4500, 0.5500] | PASS |
| WIN_RATE_HARD | 0.2567 | [0.2000, 0.3500] | PASS |
| WIN_RATE_OVERALL | 0.4800 | [0.4000, 0.6000] | PASS |
| AVG_END_TURN_OVERALL | 13.2660 | [14.0000, 20.0000] | FAIL |
| AVG_END_TURN_STANDARD | 15.0225 | [15.0000, 19.0000] | PASS |
| ATTRITION_RATE_OVERALL | 0.1680 | [0.1000, 0.3500] | PASS |
| RESOURCE_CV_GOLDLEAF | 0.3715 | [0.1800, 0.4500] | PASS |
| RESOURCE_CV_FIREOIL | 0.1799 | [0.1500, 0.4000] | PASS |
| RESOURCE_CV_ARMS | 0.2570 | [0.2000, 0.5500] | PASS |
| VICTORY_SHARE_OVERALL | 0.4800 | [0.3500, 0.6500] | PASS |
| DEFEAT_SHARE_OVERALL | 0.3520 | [0.3500, 0.6500] | PASS |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.7083 | [0.0000, 0.7000] | FAIL |

## Endgame Distribution

| End Type | Count | Share |
|---|---:|---:|
| victory | 480 | 0.4800 |
| defeat | 352 | 0.3520 |
| timeout | 168 | 0.1680 |

- Top victory path: `energyliberation` (`340` wins, share `0.7083`)
