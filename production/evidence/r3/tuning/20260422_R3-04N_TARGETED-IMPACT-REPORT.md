# R3-04 Parameter Tuning Impact Report

- Stage: R3 Balance and AI Tuning
- Task: R3-04 Parameter tuning iterations (3 rounds)
- Date: 2026-04-22

## 1. KPI Gate Score

| Round | KPI Pass | Win(E/S/H) | AvgEndTurn | Attrition | PathMonopoly |
|---|---:|---:|---:|---:|---:|
| R3-02 Run001 baseline | 0/13 | 1.0000/0.9700/0.0000 | 3.4620 | 0.0040 | 0.9055 |
| R154 (r151-r305-gap-close-a) | 11/13 | 0.6100/0.4925/0.1800 | 14.2310 | 0.2080 | 0.6912 |
| R155 (r151-r305-gap-close-b) | 9/13 | 0.6433/0.5025/0.1867 | 13.9940 | 0.2040 | 0.6800 |
| R156 (r151-r305-gap-close-c) | 11/13 | 0.6200/0.4775/0.1900 | 14.2410 | 0.2060 | 0.6912 |

## 2. Round-by-Round Delta

### R154 - r151-r305-gap-close-a

- KPI pass delta: `+11`
- Overall win rate delta: `-0.2540`
- Hard win rate delta: `+0.1800`
- Avg end turn delta: `+10.7690`
- Attrition delta: `+0.2040`
- Single-path monopoly delta: `-0.2143`
- Profile knobs: `bias=0.26/0.15/0.05`, `threshold=85/83/81`, `deltaScale=0.96`, `noise=2.02`, `shock=0.38x30`
- Artifacts: `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04N_TARGETED-R154.csv`, `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04N_TARGETED-R154-KPI.md`

### R155 - r151-r305-gap-close-b

- KPI pass delta: `-2`
- Overall win rate delta: `+0.0160`
- Hard win rate delta: `+0.0067`
- Avg end turn delta: `-0.2370`
- Attrition delta: `-0.0040`
- Single-path monopoly delta: `-0.0112`
- Profile knobs: `bias=0.26/0.15/0.05`, `threshold=85/83/81`, `deltaScale=0.96`, `noise=2.02`, `shock=0.38x30`
- Artifacts: `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04N_TARGETED-R155.csv`, `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04N_TARGETED-R155-KPI.md`

### R156 - r151-r305-gap-close-c

- KPI pass delta: `+2`
- Overall win rate delta: `-0.0160`
- Hard win rate delta: `+0.0033`
- Avg end turn delta: `+0.2470`
- Attrition delta: `+0.0020`
- Single-path monopoly delta: `+0.0112`
- Profile knobs: `bias=0.26/0.15/0.05`, `threshold=85/83/82`, `deltaScale=0.96`, `noise=2.02`, `shock=0.38x30`
- Artifacts: `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04N_TARGETED-R156.csv`, `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04N_TARGETED-R156-KPI.md`

## 3. Best Round Selection

- Selected best round: `R154 (r151-r305-gap-close-a)`
- Reason: highest KPI pass count (`11/13`), then closest to standard win-rate / mid-turn pacing targets.

## 4. Remaining Gaps

- Failed KPI IDs in best round: WIN_RATE_EASY, WIN_RATE_HARD
