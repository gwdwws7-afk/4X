# R3-04 Parameter Tuning Impact Report

- Stage: R3 Balance and AI Tuning
- Task: R3-04 Parameter tuning iterations (3 rounds)
- Date: 2026-04-22

## 1. KPI Gate Score

| Round | KPI Pass | Win(E/S/H) | AvgEndTurn | Attrition | PathMonopoly |
|---|---:|---:|---:|---:|---:|
| R3-02 Run001 baseline | 0/13 | 1.0000/0.9700/0.0000 | 3.4620 | 0.0040 | 0.9055 |
| R169 (r166-standard-lift-a) | 11/13 | 0.6733/0.4475/0.2233 | 14.2810 | 0.2120 | 0.6853 |
| R170 (r166-standard-lift-b) | 12/13 | 0.6733/0.4500/0.2233 | 14.2740 | 0.2100 | 0.6882 |
| R171 (r166-standard-lift-c) | 12/13 | 0.6733/0.4675/0.2233 | 14.1340 | 0.2030 | 0.6732 |

## 2. Round-by-Round Delta

### R169 - r166-standard-lift-a

- KPI pass delta: `+11`
- Overall win rate delta: `-0.2400`
- Hard win rate delta: `+0.2233`
- Avg end turn delta: `+10.8190`
- Attrition delta: `+0.2080`
- Single-path monopoly delta: `-0.2202`
- Profile knobs: `bias=0.27/0.16/0.05`, `threshold=85/83/81`, `deltaScale=0.96`, `noise=2.02`, `shock=0.38x30`, `late=15|p0.088|c0.058|l0.103|cap0.040`
- Artifacts: `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04R_TARGETED-R169.csv`, `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04R_TARGETED-R169-KPI.md`

### R170 - r166-standard-lift-b

- KPI pass delta: `+1`
- Overall win rate delta: `+0.0010`
- Hard win rate delta: `0.0000`
- Avg end turn delta: `-0.0070`
- Attrition delta: `-0.0020`
- Single-path monopoly delta: `+0.0029`
- Profile knobs: `bias=0.27/0.15/0.05`, `threshold=85/83/81`, `deltaScale=0.96`, `noise=2.02`, `shock=0.38x30`, `late=15|p0.090|c0.060|l0.105|cap0.041`
- Artifacts: `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04R_TARGETED-R170.csv`, `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04R_TARGETED-R170-KPI.md`

### R171 - r166-standard-lift-c

- KPI pass delta: `0`
- Overall win rate delta: `+0.0070`
- Hard win rate delta: `0.0000`
- Avg end turn delta: `-0.1400`
- Attrition delta: `-0.0070`
- Single-path monopoly delta: `-0.0150`
- Profile knobs: `bias=0.27/0.16/0.05`, `threshold=85/83/81`, `deltaScale=0.96`, `noise=2.02`, `shock=0.38x30`, `late=14|p0.092|c0.062|l0.107|cap0.040`
- Artifacts: `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04R_TARGETED-R171.csv`, `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04R_TARGETED-R171-KPI.md`

## 3. Best Round Selection

- Selected best round: `R171 (r166-standard-lift-c)`
- Reason: highest KPI pass count (`12/13`), then closest to standard win-rate / mid-turn pacing targets.

## 4. Remaining Gaps

- Failed KPI IDs in best round: DEFEAT_SHARE_OVERALL
