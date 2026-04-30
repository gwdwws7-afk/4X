# R3-04 Parameter Tuning Impact Report

- Stage: R3 Balance and AI Tuning
- Task: R3-04 Parameter tuning iterations (3 rounds)
- Date: 2026-04-22

## 1. KPI Gate Score

| Round | KPI Pass | Win(E/S/H) | AvgEndTurn | Attrition | PathMonopoly |
|---|---:|---:|---:|---:|---:|
| R3-02 Run001 baseline | 0/13 | 1.0000/0.9700/0.0000 | 3.4620 | 0.0040 | 0.9055 |
| R127 (r125-standard-recover-plus-a) | 11/13 | 0.6833/0.5075/0.2533 | 13.4850 | 0.1810 | 0.6653 |
| R128 (r125-standard-recover-plus-b) | 11/13 | 0.6800/0.4175/0.2600 | 14.0570 | 0.1990 | 0.7216 |
| R129 (r125-standard-recover-plus-c) | 10/13 | 0.6733/0.4350/0.2667 | 13.8680 | 0.1950 | 0.6996 |

## 2. Round-by-Round Delta

### R127 - r125-standard-recover-plus-a

- KPI pass delta: `+11`
- Overall win rate delta: `-0.2040`
- Hard win rate delta: `+0.2533`
- Avg end turn delta: `+10.0230`
- Attrition delta: `+0.1770`
- Single-path monopoly delta: `-0.2402`
- Profile knobs: `bias=0.26/0.16/0.05`, `threshold=85/83/81`, `deltaScale=0.96`, `noise=2.02`, `shock=0.38x30`
- Artifacts: `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04K_TARGETED-R127.csv`, `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04K_TARGETED-R127-KPI.md`

### R128 - r125-standard-recover-plus-b

- KPI pass delta: `0`
- Overall win rate delta: `-0.0350`
- Hard win rate delta: `+0.0067`
- Avg end turn delta: `+0.5720`
- Attrition delta: `+0.0180`
- Single-path monopoly delta: `+0.0563`
- Profile knobs: `bias=0.26/0.15/0.05`, `threshold=85/84/81`, `deltaScale=0.96`, `noise=2.02`, `shock=0.38x30`
- Artifacts: `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04K_TARGETED-R128.csv`, `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04K_TARGETED-R128-KPI.md`

### R129 - r125-standard-recover-plus-c

- KPI pass delta: `-1`
- Overall win rate delta: `+0.0070`
- Hard win rate delta: `+0.0067`
- Avg end turn delta: `-0.1890`
- Attrition delta: `-0.0040`
- Single-path monopoly delta: `-0.0220`
- Profile knobs: `bias=0.26/0.15/0.05`, `threshold=85/84/81`, `deltaScale=0.96`, `noise=2.02`, `shock=0.38x30`
- Artifacts: `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04K_TARGETED-R129.csv`, `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04K_TARGETED-R129-KPI.md`

## 3. Best Round Selection

- Selected best round: `R127 (r125-standard-recover-plus-a)`
- Reason: highest KPI pass count (`11/13`), then closest to standard win-rate / mid-turn pacing targets.

## 4. Remaining Gaps

- Failed KPI IDs in best round: AVG_END_TURN_OVERALL, DEFEAT_SHARE_OVERALL
