# R3-04 Parameter Tuning Impact Report

- Stage: R3 Balance and AI Tuning
- Task: R3-04 Parameter tuning iterations (3 rounds)
- Date: 2026-04-23

## 1. KPI Gate Score

| Round | KPI Pass | Win(E/S/H) | AvgEndTurn | Attrition | PathMonopoly |
|---|---:|---:|---:|---:|---:|
| R3-02 Run001 baseline | 0/13 | 1.0000/0.9700/0.0000 | 3.4620 | 0.0040 | 0.9055 |
| R211 (r208-hard-timeout-convert-a) | 12/13 | 0.7100/0.4525/0.1933 | 14.5510 | 0.1960 | 0.6173 |
| R212 (r208-hard-timeout-convert-b) | 12/13 | 0.7100/0.4525/0.2400 | 14.3940 | 0.1960 | 0.6052 |
| R213 (r208-hard-timeout-convert-c) | 12/13 | 0.7100/0.4525/0.2400 | 14.3940 | 0.1960 | 0.6052 |

## 2. Round-by-Round Delta

### R211 - r208-hard-timeout-convert-a

- KPI pass delta: `+12`
- Overall win rate delta: `-0.2360`
- Hard win rate delta: `+0.1933`
- Avg end turn delta: `+11.0890`
- Attrition delta: `+0.1920`
- Single-path monopoly delta: `-0.2882`
- Profile knobs: `bias=0.27/0.16/0.05`, `threshold=85/83/81`, `deltaScale=0.96`, `noise=1.96`, `shock=0.31x24`, `late=13|p0.094|c0.069|l0.118|cap0.038`
- Artifacts: `C:\test\4X\production\evidence\r3\tuning\20260423_R3-04AF_TARGETED-R211.csv`, `C:\test\4X\production\evidence\r3\tuning\20260423_R3-04AF_TARGETED-R211-KPI.md`

### R212 - r208-hard-timeout-convert-b

- KPI pass delta: `0`
- Overall win rate delta: `+0.0140`
- Hard win rate delta: `+0.0467`
- Avg end turn delta: `-0.1570`
- Attrition delta: `0.0000`
- Single-path monopoly delta: `-0.0121`
- Profile knobs: `bias=0.27/0.16/0.05`, `threshold=85/83/81`, `deltaScale=0.96`, `noise=1.96`, `shock=0.31x24`, `late=13|p0.094|c0.069|l0.118|cap0.038`
- Artifacts: `C:\test\4X\production\evidence\r3\tuning\20260423_R3-04AF_TARGETED-R212.csv`, `C:\test\4X\production\evidence\r3\tuning\20260423_R3-04AF_TARGETED-R212-KPI.md`

### R213 - r208-hard-timeout-convert-c

- KPI pass delta: `0`
- Overall win rate delta: `0.0000`
- Hard win rate delta: `0.0000`
- Avg end turn delta: `0.0000`
- Attrition delta: `0.0000`
- Single-path monopoly delta: `0.0000`
- Profile knobs: `bias=0.27/0.16/0.05`, `threshold=85/83/81`, `deltaScale=0.96`, `noise=1.96`, `shock=0.31x24`, `late=13|p0.094|c0.069|l0.118|cap0.038`
- Artifacts: `C:\test\4X\production\evidence\r3\tuning\20260423_R3-04AF_TARGETED-R213.csv`, `C:\test\4X\production\evidence\r3\tuning\20260423_R3-04AF_TARGETED-R213-KPI.md`

## 3. Best Round Selection

- Selected best round: `R211 (r208-hard-timeout-convert-a)`
- Reason: highest KPI pass count (`12/13`), then closest to standard win-rate / mid-turn pacing targets.

## 4. Remaining Gaps

- Failed KPI IDs in best round: WIN_RATE_HARD
