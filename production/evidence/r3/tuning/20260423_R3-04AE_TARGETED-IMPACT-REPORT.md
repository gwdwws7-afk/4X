# R3-04 Parameter Tuning Impact Report

- Stage: R3 Balance and AI Tuning
- Task: R3-04 Parameter tuning iterations (3 rounds)
- Date: 2026-04-23

## 1. KPI Gate Score

| Round | KPI Pass | Win(E/S/H) | AvgEndTurn | Attrition | PathMonopoly |
|---|---:|---:|---:|---:|---:|
| R3-02 Run001 baseline | 0/13 | 1.0000/0.9700/0.0000 | 3.4620 | 0.0040 | 0.9055 |
| R208 (r205-converge-close-a) | 12/13 | 0.7100/0.4525/0.1833 | 14.5710 | 0.1970 | 0.6214 |
| R209 (r205-converge-close-b) | 12/13 | 0.7367/0.4375/0.2033 | 14.5150 | 0.1910 | 0.6149 |
| R210 (r205-converge-close-c) | 11/13 | 0.7167/0.4225/0.1967 | 14.4570 | 0.1860 | 0.6185 |

## 2. Round-by-Round Delta

### R208 - r205-converge-close-a

- KPI pass delta: `+12`
- Overall win rate delta: `-0.2390`
- Hard win rate delta: `+0.1833`
- Avg end turn delta: `+11.1090`
- Attrition delta: `+0.1930`
- Single-path monopoly delta: `-0.2841`
- Profile knobs: `bias=0.27/0.16/0.05`, `threshold=85/83/81`, `deltaScale=0.96`, `noise=1.96`, `shock=0.31x24`, `late=13|p0.094|c0.069|l0.118|cap0.038`
- Artifacts: `C:\test\4X\production\evidence\r3\tuning\20260423_R3-04AE_TARGETED-R208.csv`, `C:\test\4X\production\evidence\r3\tuning\20260423_R3-04AE_TARGETED-R208-KPI.md`

### R209 - r205-converge-close-b

- KPI pass delta: `0`
- Overall win rate delta: `+0.0080`
- Hard win rate delta: `+0.0200`
- Avg end turn delta: `-0.0560`
- Attrition delta: `-0.0060`
- Single-path monopoly delta: `-0.0065`
- Profile knobs: `bias=0.28/0.16/0.05`, `threshold=85/83/81`, `deltaScale=0.96`, `noise=1.95`, `shock=0.32x24`, `late=13|p0.094|c0.068|l0.116|cap0.037`
- Artifacts: `C:\test\4X\production\evidence\r3\tuning\20260423_R3-04AE_TARGETED-R209.csv`, `C:\test\4X\production\evidence\r3\tuning\20260423_R3-04AE_TARGETED-R209-KPI.md`

### R210 - r205-converge-close-c

- KPI pass delta: `-1`
- Overall win rate delta: `-0.0140`
- Hard win rate delta: `-0.0067`
- Avg end turn delta: `-0.0580`
- Attrition delta: `-0.0050`
- Single-path monopoly delta: `+0.0036`
- Profile knobs: `bias=0.27/0.16/0.05`, `threshold=85/83/81`, `deltaScale=0.96`, `noise=1.96`, `shock=0.33x24`, `late=12|p0.094|c0.071|l0.121|cap0.039`
- Artifacts: `C:\test\4X\production\evidence\r3\tuning\20260423_R3-04AE_TARGETED-R210.csv`, `C:\test\4X\production\evidence\r3\tuning\20260423_R3-04AE_TARGETED-R210-KPI.md`

## 3. Best Round Selection

- Selected best round: `R208 (r205-converge-close-a)`
- Reason: highest KPI pass count (`12/13`), then closest to standard win-rate / mid-turn pacing targets.

## 4. Remaining Gaps

- Failed KPI IDs in best round: WIN_RATE_HARD
