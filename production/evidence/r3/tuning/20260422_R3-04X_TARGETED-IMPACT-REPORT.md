# R3-04 Parameter Tuning Impact Report

- Stage: R3 Balance and AI Tuning
- Task: R3-04 Parameter tuning iterations (3 rounds)
- Date: 2026-04-22

## 1. KPI Gate Score

| Round | KPI Pass | Win(E/S/H) | AvgEndTurn | Attrition | PathMonopoly |
|---|---:|---:|---:|---:|---:|
| R3-02 Run001 baseline | 0/13 | 1.0000/0.9700/0.0000 | 3.4620 | 0.0040 | 0.9055 |
| R187 (r185-residue-close-a) | 8/13 | 0.6167/0.3825/0.1433 | 14.8270 | 0.2190 | 0.8241 |
| R188 (r185-residue-close-b) | 8/13 | 0.6233/0.3850/0.1400 | 14.8210 | 0.2160 | 0.8225 |
| R189 (r185-residue-close-c) | 8/13 | 0.6333/0.3925/0.1467 | 14.7710 | 0.2130 | 0.8133 |

## 2. Round-by-Round Delta

### R187 - r185-residue-close-a

- KPI pass delta: `+8`
- Overall win rate delta: `-0.3070`
- Hard win rate delta: `+0.1433`
- Avg end turn delta: `+11.3650`
- Attrition delta: `+0.2150`
- Single-path monopoly delta: `-0.0814`
- Profile knobs: `bias=0.27/0.16/0.05`, `threshold=85/83/81`, `deltaScale=0.96`, `noise=2.02`, `shock=0.38x30`, `late=14|p0.097|c0.068|l0.115|cap0.041`
- Artifacts: `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04X_TARGETED-R187.csv`, `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04X_TARGETED-R187-KPI.md`

### R188 - r185-residue-close-b

- KPI pass delta: `0`
- Overall win rate delta: `+0.0020`
- Hard win rate delta: `-0.0033`
- Avg end turn delta: `-0.0060`
- Attrition delta: `-0.0030`
- Single-path monopoly delta: `-0.0017`
- Profile knobs: `bias=0.27/0.16/0.06`, `threshold=85/83/81`, `deltaScale=0.96`, `noise=2.02`, `shock=0.38x30`, `late=14|p0.097|c0.068|l0.114|cap0.041`
- Artifacts: `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04X_TARGETED-R188.csv`, `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04X_TARGETED-R188-KPI.md`

### R189 - r185-residue-close-c

- KPI pass delta: `0`
- Overall win rate delta: `+0.0080`
- Hard win rate delta: `+0.0067`
- Avg end turn delta: `-0.0500`
- Attrition delta: `-0.0030`
- Single-path monopoly delta: `-0.0092`
- Profile knobs: `bias=0.27/0.16/0.06`, `threshold=85/83/81`, `deltaScale=0.96`, `noise=2.02`, `shock=0.38x30`, `late=14|p0.097|c0.068|l0.115|cap0.040`
- Artifacts: `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04X_TARGETED-R189.csv`, `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04X_TARGETED-R189-KPI.md`

## 3. Best Round Selection

- Selected best round: `R189 (r185-residue-close-c)`
- Reason: highest KPI pass count (`8/13`), then closest to standard win-rate / mid-turn pacing targets.

## 4. Remaining Gaps

- Failed KPI IDs in best round: WIN_RATE_EASY, WIN_RATE_STANDARD, WIN_RATE_HARD, WIN_RATE_OVERALL, SINGLE_PATH_VICTORY_MONOPOLY
