# R3-04 Parameter Tuning Impact Report

- Stage: R3 Balance and AI Tuning
- Task: R3-04 Parameter tuning iterations (3 rounds)
- Date: 2026-04-21

## 1. KPI Gate Score

| Round | KPI Pass | Win(E/S/H) | AvgEndTurn | Attrition | PathMonopoly |
|---|---:|---:|---:|---:|---:|
| R3-02 Run001 baseline | 0/13 | 1.0000/0.9700/0.0000 | 3.4620 | 0.0040 | 0.9055 |
| R82 (r79-pace-backpull-a) | 13/13 | 0.6733/0.4650/0.2133 | 14.0970 | 0.1970 | 0.6770 |
| R83 (r79-pace-backpull-b) | 11/13 | 0.6667/0.5250/0.2267 | 13.8610 | 0.1820 | 0.6862 |
| R84 (r79-pace-backpull-c) | 12/13 | 0.6800/0.4925/0.2100 | 14.0170 | 0.1930 | 0.6832 |

## 2. Round-by-Round Delta

### R82 - r79-pace-backpull-a

- KPI pass delta: `+13`
- Overall win rate delta: `-0.2360`
- Hard win rate delta: `+0.2133`
- Avg end turn delta: `+10.6350`
- Attrition delta: `+0.1930`
- Single-path monopoly delta: `-0.2285`
- Profile knobs: `bias=0.26/0.14/0.05`, `threshold=85/83/81`, `deltaScale=0.96`, `noise=2.05`, `shock=0.41x33`
- Artifacts: `C:\test\4X\production\evidence\r3\tuning\20260421_R3-04H_TARGETED-R82.csv`, `C:\test\4X\production\evidence\r3\tuning\20260421_R3-04H_TARGETED-R82-KPI.md`

### R83 - r79-pace-backpull-b

- KPI pass delta: `-2`
- Overall win rate delta: `+0.0260`
- Hard win rate delta: `+0.0133`
- Avg end turn delta: `-0.2360`
- Attrition delta: `-0.0150`
- Single-path monopoly delta: `+0.0092`
- Profile knobs: `bias=0.26/0.14/0.05`, `threshold=85/83/81`, `deltaScale=0.96`, `noise=2.05`, `shock=0.41x33`
- Artifacts: `C:\test\4X\production\evidence\r3\tuning\20260421_R3-04H_TARGETED-R83.csv`, `C:\test\4X\production\evidence\r3\tuning\20260421_R3-04H_TARGETED-R83-KPI.md`

### R84 - r79-pace-backpull-c

- KPI pass delta: `+1`
- Overall win rate delta: `-0.0140`
- Hard win rate delta: `-0.0167`
- Avg end turn delta: `+0.1560`
- Attrition delta: `+0.0110`
- Single-path monopoly delta: `-0.0030`
- Profile knobs: `bias=0.26/0.15/0.05`, `threshold=85/83/81`, `deltaScale=0.96`, `noise=2.05`, `shock=0.41x33`
- Artifacts: `C:\test\4X\production\evidence\r3\tuning\20260421_R3-04H_TARGETED-R84.csv`, `C:\test\4X\production\evidence\r3\tuning\20260421_R3-04H_TARGETED-R84-KPI.md`

## 3. Best Round Selection

- Selected best round: `R82 (r79-pace-backpull-a)`
- Reason: highest KPI pass count (`13/13`), then closest to standard win-rate / mid-turn pacing targets.

## 4. Remaining Gaps

- No KPI hard-gate failures in best round.
