# R3-04 KPI Evaluation (R27)

- Batch: `R3-04F-B001-R27`
- Profile: `pace-floor-guard`
- Runs: `1000`
- Source CSV: `C:\test\4X\production\evidence\r3\tuning\20260421_R3-04F_TARGETED-R27.csv`
- KPI Pass: `6/13`

## Round Parameter Delta

- Bias (Easy/Standard/Hard): `0.24/0.12/-0.08`
- Init Scale (Easy/Standard/Hard): `1.10/1.00/0.90`
- Social/Trade Init Scale: `0.22`
- Victory Threshold (Easy/Standard/Hard): `87/84/85`
- Pressure Offset: `0.25`; Conflict Base/TurnScale: `0.420/0.014`
- Delta Scale/Noise Scale: `0.93/2.15`
- Early Suppression: `turn<= 8` with `positive*0.42`
- Shock: `chance=0.46`, `magnitude=+/-37`

## KPI Snapshot

| KPI | Actual | Target | Status |
|---|---:|---:|---|
| WIN_RATE_EASY | 0.4800 | [0.6500, 0.8000] | FAIL |
| WIN_RATE_STANDARD | 0.2950 | [0.4500, 0.5500] | FAIL |
| WIN_RATE_HARD | 0.0067 | [0.2000, 0.3500] | FAIL |
| WIN_RATE_OVERALL | 0.2640 | [0.4000, 0.6000] | FAIL |
| AVG_END_TURN_OVERALL | 12.9200 | [14.0000, 20.0000] | FAIL |
| AVG_END_TURN_STANDARD | 15.3050 | [15.0000, 19.0000] | PASS |
| ATTRITION_RATE_OVERALL | 0.2030 | [0.1000, 0.3500] | PASS |
| RESOURCE_CV_GOLDLEAF | 0.4052 | [0.1800, 0.4500] | PASS |
| RESOURCE_CV_FIREOIL | 0.2301 | [0.1500, 0.4000] | PASS |
| RESOURCE_CV_ARMS | 0.3179 | [0.2000, 0.5500] | PASS |
| VICTORY_SHARE_OVERALL | 0.2640 | [0.3500, 0.6500] | FAIL |
| DEFEAT_SHARE_OVERALL | 0.5330 | [0.3500, 0.6500] | PASS |
| SINGLE_PATH_VICTORY_MONOPOLY | 0.9621 | [0.0000, 0.7000] | FAIL |

## Endgame Distribution

| End Type | Count | Share |
|---|---:|---:|
| victory | 264 | 0.2640 |
| defeat | 533 | 0.5330 |
| timeout | 203 | 0.2030 |

- Top victory path: `energyliberation` (`254` wins, share `0.9621`)
