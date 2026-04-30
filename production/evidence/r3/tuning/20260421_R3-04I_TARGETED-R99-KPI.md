# R3-04 KPI Evaluation (R99)

- Batch: `R3-04I-B005-R99`
- Profile: `r82-early-suppress-c`
- Runs: `1000`
- Source CSV: `C:\test\4X\production\evidence\r3\tuning\20260421_R3-04I_TARGETED-R99.csv`
- KPI Pass: `12/13`

## Round Parameter Delta

- Bias (Easy/Standard/Hard): `0.26/0.15/0.05`
- Init Scale (Easy/Standard/Hard): `1.13/1.07/1.03`
- Social/Trade Init Scale: `0.16`
- Victory Threshold (Easy/Standard/Hard): `85/83/81`
- Pressure Offset: `0.24`; Conflict Base/TurnScale: `0.402/0.013`
- Delta Scale/Noise Scale: `0.96/2.02`
- Early Suppression: `turn<= 10` with `positive*0.39`
- Shock: `chance=0.38`, `magnitude=+/-30`

## KPI Snapshot

| KPI | Actual | Target | Status |
|---|---:|---:|---|
| WIN_RATE_EASY | 0.7000 | [0.6500, 0.8000] | PASS |
| WIN_RATE_STANDARD | 0.4825 | [0.4500, 0.5500] | PASS |
| WIN_RATE_HARD | 0.2200 | [0.2000, 0.3500] | PASS |
| WIN_RATE_OVERALL | 0.4690 | [0.4000, 0.6000] | PASS |
| AVG_END_TURN_OVERALL | 13.3660 | [14.0000, 20.0000] | FAIL |
| AVG_END_TURN_STANDARD | 15.0675 | [15.0000, 19.0000] | PASS |
| ATTRITION_RATE_OVERALL | 0.1750 | [0.1000, 0.3500] | PASS |
| RESOURCE_CV_GOLDLEAF | 0.3603 | [0.1800, 0.4500] | PASS |
| RESOURCE_CV_FIREOIL | 0.1773 | [0.1500, 0.4000] | PASS |
| RESOURCE_CV_ARMS | 0.2492 | [0.2000, 0.5500] | PASS |
| VICTORY_SHARE_OVERALL | 0.4690 | [0.3500, 0.6500] | PASS |
| DEFEAT_SHARE_OVERALL | 0.3560 | [0.3500, 0.6500] | PASS |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.6887 | [0.0000, 0.7000] | PASS |

## Endgame Distribution

| End Type | Count | Share |
|---|---:|---:|
| victory | 469 | 0.4690 |
| defeat | 356 | 0.3560 |
| timeout | 175 | 0.1750 |

- Top victory path: `energyliberation` (`323` wins, share `0.6887`)
