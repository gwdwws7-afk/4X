# R3-04 KPI Evaluation (R77)

- Batch: `R3-04H-B002-R77`
- Profile: `r70-hard-threshold-lift`
- Runs: `1000`
- Source CSV: `C:\test\4X\production\evidence\r3\tuning\20260421_R3-04H_TARGETED-R77.csv`
- KPI Pass: `11/13`

## Round Parameter Delta

- Bias (Easy/Standard/Hard): `0.24/0.14/0.05`
- Init Scale (Easy/Standard/Hard): `1.11/1.05/1.04`
- Social/Trade Init Scale: `0.16`
- Victory Threshold (Easy/Standard/Hard): `85/83/81`
- Pressure Offset: `0.24`; Conflict Base/TurnScale: `0.400/0.013`
- Delta Scale/Noise Scale: `0.96/2.05`
- Early Suppression: `turn<= 7` with `positive*0.47`
- Shock: `chance=0.41`, `magnitude=+/-33`

## KPI Snapshot

| KPI | Actual | Target | Status |
|---|---:|---:|---|
| WIN_RATE_EASY | 0.6300 | [0.6500, 0.8000] | FAIL |
| WIN_RATE_STANDARD | 0.4650 | [0.4500, 0.5500] | PASS |
| WIN_RATE_HARD | 0.2267 | [0.2000, 0.3500] | PASS |
| WIN_RATE_OVERALL | 0.4430 | [0.4000, 0.6000] | PASS |
| AVG_END_TURN_OVERALL | 14.3260 | [14.0000, 20.0000] | PASS |
| AVG_END_TURN_STANDARD | 15.7900 | [15.0000, 19.0000] | PASS |
| ATTRITION_RATE_OVERALL | 0.2090 | [0.1000, 0.3500] | PASS |
| RESOURCE_CV_GOLDLEAF | 0.3588 | [0.1800, 0.4500] | PASS |
| RESOURCE_CV_FIREOIL | 0.1858 | [0.1500, 0.4000] | PASS |
| RESOURCE_CV_ARMS | 0.2796 | [0.2000, 0.5500] | PASS |
| VICTORY_SHARE_OVERALL | 0.4430 | [0.3500, 0.6500] | PASS |
| DEFEAT_SHARE_OVERALL | 0.3480 | [0.3500, 0.6500] | FAIL |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.6795 | [0.0000, 0.7000] | PASS |

## Endgame Distribution

| End Type | Count | Share |
|---|---:|---:|
| victory | 443 | 0.4430 |
| defeat | 348 | 0.3480 |
| timeout | 209 | 0.2090 |

- Top victory path: `energyliberation` (`301` wins, share `0.6795`)
