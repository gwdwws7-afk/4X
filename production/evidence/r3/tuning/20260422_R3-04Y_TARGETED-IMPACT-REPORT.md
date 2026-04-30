# R3-04 Parameter Tuning Impact Report

- Stage: R3 Balance and AI Tuning
- Task: R3-04 Parameter tuning iterations (3 rounds)
- Date: 2026-04-22

## 1. KPI Gate Score

| Round | KPI Pass | Win(E/S/H) | AvgEndTurn | Attrition | PathMonopoly |
|---|---:|---:|---:|---:|---:|
| R3-02 Run001 baseline | 0/13 | 1.0000/0.9700/0.0000 | 3.4620 | 0.0040 | 0.9055 |
| R190 (r185-gap-close-a) | 8/13 | 0.6167/0.3825/0.1400 | 14.8460 | 0.2180 | 0.8211 |
| R191 (r185-gap-close-b) | 7/13 | 0.5733/0.3275/0.0933 | 15.3680 | 0.2430 | 0.9909 |
| R192 (r185-gap-close-c) | 7/13 | 0.5433/0.3275/0.1067 | 15.4690 | 0.2490 | 0.9939 |

## 2. Round-by-Round Delta

### R190 - r185-gap-close-a

- KPI pass delta: `+8`
- Overall win rate delta: `-0.3080`
- Hard win rate delta: `+0.1400`
- Avg end turn delta: `+11.3840`
- Attrition delta: `+0.2140`
- Single-path monopoly delta: `-0.0844`
- Profile knobs: `bias=0.27/0.16/0.05`, `threshold=85/83/81`, `deltaScale=0.96`, `noise=2.02`, `shock=0.38x30`, `late=14|p0.097|c0.068|l0.114|cap0.040`
- Artifacts: `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04Y_TARGETED-R190.csv`, `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04Y_TARGETED-R190-KPI.md`

### R191 - r185-gap-close-b

- KPI pass delta: `-1`
- Overall win rate delta: `-0.0490`
- Hard win rate delta: `-0.0467`
- Avg end turn delta: `+0.5220`
- Attrition delta: `+0.0250`
- Single-path monopoly delta: `+0.1699`
- Profile knobs: `bias=0.27/0.16/0.06`, `threshold=85/83/81`, `deltaScale=0.96`, `noise=2.02`, `shock=0.38x30`, `late=14|p0.097|c0.068|l0.113|cap0.041`
- Artifacts: `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04Y_TARGETED-R191.csv`, `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04Y_TARGETED-R191-KPI.md`

### R192 - r185-gap-close-c

- KPI pass delta: `0`
- Overall win rate delta: `-0.0050`
- Hard win rate delta: `+0.0133`
- Avg end turn delta: `+0.1010`
- Attrition delta: `+0.0060`
- Single-path monopoly delta: `+0.0029`
- Profile knobs: `bias=0.27/0.16/0.06`, `threshold=85/83/81`, `deltaScale=0.96`, `noise=2.02`, `shock=0.38x30`, `late=14|p0.097|c0.068|l0.115|cap0.040`
- Artifacts: `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04Y_TARGETED-R192.csv`, `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04Y_TARGETED-R192-KPI.md`

## 3. Best Round Selection

- Selected best round: `R190 (r185-gap-close-a)`
- Reason: highest KPI pass count (`8/13`), then closest to standard win-rate / mid-turn pacing targets.

## 4. Remaining Gaps

- Failed KPI IDs in best round: WIN_RATE_EASY, WIN_RATE_STANDARD, WIN_RATE_HARD, WIN_RATE_OVERALL, SINGLE_PATH_VICTORY_MONOPOLY
