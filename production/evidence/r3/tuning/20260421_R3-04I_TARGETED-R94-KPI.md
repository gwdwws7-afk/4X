# R3-04 KPI Evaluation (R94)

- Batch: `R3-04I-B004-R94`
- Profile: `r82-gate-balance-a`
- Runs: `1000`
- Source CSV: `C:\test\4X\production\evidence\r3\tuning\20260421_R3-04I_TARGETED-R94.csv`
- KPI Pass: `10/13`

## Round Parameter Delta

- Bias (Easy/Standard/Hard): `0.26/0.15/0.05`
- Init Scale (Easy/Standard/Hard): `1.13/1.06/1.03`
- Social/Trade Init Scale: `0.16`
- Victory Threshold (Easy/Standard/Hard): `86/83/81`
- Pressure Offset: `0.25`; Conflict Base/TurnScale: `0.401/0.013`
- Delta Scale/Noise Scale: `0.96/2.00`
- Early Suppression: `turn<= 8` with `positive*0.46`
- Shock: `chance=0.39`, `magnitude=+/-31`

## KPI Snapshot

| KPI | Actual | Target | Status |
|---|---:|---:|---|
| WIN_RATE_EASY | 0.6233 | [0.6500, 0.8000] | FAIL |
| WIN_RATE_STANDARD | 0.4725 | [0.4500, 0.5500] | PASS |
| WIN_RATE_HARD | 0.2233 | [0.2000, 0.3500] | PASS |
| WIN_RATE_OVERALL | 0.4430 | [0.4000, 0.6000] | PASS |
| AVG_END_TURN_OVERALL | 13.8220 | [14.0000, 20.0000] | FAIL |
| AVG_END_TURN_STANDARD | 15.0975 | [15.0000, 19.0000] | PASS |
| ATTRITION_RATE_OVERALL | 0.1890 | [0.1000, 0.3500] | PASS |
| RESOURCE_CV_GOLDLEAF | 0.3442 | [0.1800, 0.4500] | PASS |
| RESOURCE_CV_FIREOIL | 0.1764 | [0.1500, 0.4000] | PASS |
| RESOURCE_CV_ARMS | 0.2451 | [0.2000, 0.5500] | PASS |
| VICTORY_SHARE_OVERALL | 0.4430 | [0.3500, 0.6500] | PASS |
| DEFEAT_SHARE_OVERALL | 0.3680 | [0.3500, 0.6500] | PASS |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.7065 | [0.0000, 0.7000] | FAIL |

## Endgame Distribution

| End Type | Count | Share |
|---|---:|---:|
| victory | 443 | 0.4430 |
| defeat | 368 | 0.3680 |
| timeout | 189 | 0.1890 |

- Top victory path: `energyliberation` (`313` wins, share `0.7065`)
