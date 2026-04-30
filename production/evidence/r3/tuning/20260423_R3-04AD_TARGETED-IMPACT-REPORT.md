# R3-04 Parameter Tuning Impact Report

- Stage: R3 Balance and AI Tuning
- Task: R3-04 Parameter tuning iterations (3 rounds)
- Date: 2026-04-23

## 1. KPI Gate Score

| Round | KPI Pass | Win(E/S/H) | AvgEndTurn | Attrition | PathMonopoly |
|---|---:|---:|---:|---:|---:|
| R3-02 Run001 baseline | 0/13 | 1.0000/0.9700/0.0000 | 3.4620 | 0.0040 | 0.9055 |
| R205 (r204-variance-close-a) | 11/13 | 0.6267/0.4500/0.1833 | 14.8900 | 0.2190 | 0.5981 |
| R206 (r204-variance-close-b) | 10/13 | 0.6033/0.4475/0.1833 | 15.0470 | 0.2300 | 0.5855 |
| R207 (r204-variance-close-c) | 10/13 | 0.6333/0.4400/0.1867 | 14.8930 | 0.2220 | 0.6043 |

## 2. Round-by-Round Delta

### R205 - r204-variance-close-a

- KPI pass delta: `+11`
- Overall win rate delta: `-0.2650`
- Hard win rate delta: `+0.1833`
- Avg end turn delta: `+11.4280`
- Attrition delta: `+0.2150`
- Single-path monopoly delta: `-0.3074`
- Profile knobs: `bias=0.27/0.16/0.05`, `threshold=85/83/81`, `deltaScale=0.96`, `noise=1.96`, `shock=0.31x24`, `late=14|p0.094|c0.066|l0.107|cap0.036`
- Artifacts: `C:\test\4X\production\evidence\r3\tuning\20260423_R3-04AD_TARGETED-R205.csv`, `C:\test\4X\production\evidence\r3\tuning\20260423_R3-04AD_TARGETED-R205-KPI.md`

### R206 - r204-variance-close-b

- KPI pass delta: `-1`
- Overall win rate delta: `-0.0080`
- Hard win rate delta: `0.0000`
- Avg end turn delta: `+0.1570`
- Attrition delta: `+0.0110`
- Single-path monopoly delta: `-0.0126`
- Profile knobs: `bias=0.27/0.16/0.05`, `threshold=85/83/81`, `deltaScale=0.96`, `noise=1.94`, `shock=0.30x22`, `late=14|p0.094|c0.066|l0.107|cap0.036`
- Artifacts: `C:\test\4X\production\evidence\r3\tuning\20260423_R3-04AD_TARGETED-R206.csv`, `C:\test\4X\production\evidence\r3\tuning\20260423_R3-04AD_TARGETED-R206-KPI.md`

### R207 - r204-variance-close-c

- KPI pass delta: `0`
- Overall win rate delta: `+0.0070`
- Hard win rate delta: `+0.0033`
- Avg end turn delta: `-0.1540`
- Attrition delta: `-0.0080`
- Single-path monopoly delta: `+0.0187`
- Profile knobs: `bias=0.27/0.16/0.05`, `threshold=85/83/81`, `deltaScale=0.96`, `noise=1.95`, `shock=0.32x24`, `late=14|p0.094|c0.066|l0.107|cap0.036`
- Artifacts: `C:\test\4X\production\evidence\r3\tuning\20260423_R3-04AD_TARGETED-R207.csv`, `C:\test\4X\production\evidence\r3\tuning\20260423_R3-04AD_TARGETED-R207-KPI.md`

## 3. Best Round Selection

- Selected best round: `R205 (r204-variance-close-a)`
- Reason: highest KPI pass count (`11/13`), then closest to standard win-rate / mid-turn pacing targets.

## 4. Remaining Gaps

- Failed KPI IDs in best round: WIN_RATE_EASY, WIN_RATE_HARD
