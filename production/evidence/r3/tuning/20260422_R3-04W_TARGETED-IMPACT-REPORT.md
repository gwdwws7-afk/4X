# R3-04 Parameter Tuning Impact Report

- Stage: R3 Balance and AI Tuning
- Task: R3-04 Parameter tuning iterations (3 rounds)
- Date: 2026-04-22

## 1. KPI Gate Score

| Round | KPI Pass | Win(E/S/H) | AvgEndTurn | Attrition | PathMonopoly |
|---|---:|---:|---:|---:|---:|
| R3-02 Run001 baseline | 0/13 | 1.0000/0.9700/0.0000 | 3.4620 | 0.0040 | 0.9055 |
| R184 (r180-r305-gapfix-a) | 10/13 | 0.6767/0.4450/0.1833 | 14.0100 | 0.1740 | 0.7225 |
| R185 (r180-r305-gapfix-b) | 10/13 | 0.6333/0.4500/0.1833 | 14.1270 | 0.1850 | 0.7106 |
| R186 (r180-r305-gapfix-c) | 8/13 | 0.6300/0.3675/0.1200 | 14.8950 | 0.2190 | 0.8387 |

## 2. Round-by-Round Delta

### R184 - r180-r305-gapfix-a

- KPI pass delta: `+10`
- Overall win rate delta: `-0.2520`
- Hard win rate delta: `+0.1833`
- Avg end turn delta: `+10.5480`
- Attrition delta: `+0.1700`
- Single-path monopoly delta: `-0.1830`
- Profile knobs: `bias=0.27/0.16/0.05`, `threshold=85/83/81`, `deltaScale=0.96`, `noise=2.02`, `shock=0.38x30`, `late=13|p0.100|c0.070|l0.118|cap0.043`
- Artifacts: `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04W_TARGETED-R184.csv`, `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04W_TARGETED-R184-KPI.md`

### R185 - r180-r305-gapfix-b

- KPI pass delta: `0`
- Overall win rate delta: `-0.0110`
- Hard win rate delta: `0.0000`
- Avg end turn delta: `+0.1170`
- Attrition delta: `+0.0110`
- Single-path monopoly delta: `-0.0119`
- Profile knobs: `bias=0.27/0.16/0.05`, `threshold=85/83/81`, `deltaScale=0.96`, `noise=2.02`, `shock=0.38x30`, `late=14|p0.097|c0.068|l0.115|cap0.041`
- Artifacts: `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04W_TARGETED-R185.csv`, `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04W_TARGETED-R185-KPI.md`

### R186 - r180-r305-gapfix-c

- KPI pass delta: `-2`
- Overall win rate delta: `-0.0530`
- Hard win rate delta: `-0.0633`
- Avg end turn delta: `+0.7680`
- Attrition delta: `+0.0340`
- Single-path monopoly delta: `+0.1281`
- Profile knobs: `bias=0.27/0.16/0.06`, `threshold=85/83/81`, `deltaScale=0.96`, `noise=2.02`, `shock=0.38x30`, `late=13|p0.100|c0.070|l0.119|cap0.044`
- Artifacts: `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04W_TARGETED-R186.csv`, `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04W_TARGETED-R186-KPI.md`

## 3. Best Round Selection

- Selected best round: `R185 (r180-r305-gapfix-b)`
- Reason: highest KPI pass count (`10/13`), then closest to standard win-rate / mid-turn pacing targets.

## 4. Remaining Gaps

- Failed KPI IDs in best round: WIN_RATE_EASY, WIN_RATE_HARD, SINGLE_PATH_VICTORY_MONOPOLY
