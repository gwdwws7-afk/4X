# R3-04 Parameter Tuning Impact Report

- Stage: R3 Balance and AI Tuning
- Task: R3-04 Parameter tuning iterations (3 rounds)
- Date: 2026-04-22

## 1. KPI Gate Score

| Round | KPI Pass | Win(E/S/H) | AvgEndTurn | Attrition | PathMonopoly |
|---|---:|---:|---:|---:|---:|
| R3-02 Run001 baseline | 0/13 | 1.0000/0.9700/0.0000 | 3.4620 | 0.0040 | 0.9055 |
| R163 (r160-late-pressure-a) | 11/13 | 0.6267/0.4575/0.2333 | 14.2560 | 0.2100 | 0.6712 |
| R164 (r160-late-pressure-b) | 11/13 | 0.6200/0.4450/0.2300 | 14.2630 | 0.2100 | 0.6605 |
| R165 (r160-late-pressure-c) | 11/13 | 0.6367/0.4700/0.2333 | 14.2230 | 0.2100 | 0.6704 |

## 2. Round-by-Round Delta

### R163 - r160-late-pressure-a

- KPI pass delta: `+11`
- Overall win rate delta: `-0.2470`
- Hard win rate delta: `+0.2333`
- Avg end turn delta: `+10.7940`
- Attrition delta: `+0.2060`
- Single-path monopoly delta: `-0.2343`
- Profile knobs: `bias=0.26/0.15/0.05`, `threshold=85/83/81`, `deltaScale=0.96`, `noise=2.02`, `shock=0.38x30`, `late=14|p0.090|c0.060|l0.100|cap0.040`
- Artifacts: `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04P_TARGETED-R163.csv`, `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04P_TARGETED-R163-KPI.md`

### R164 - r160-late-pressure-b

- KPI pass delta: `0`
- Overall win rate delta: `-0.0080`
- Hard win rate delta: `-0.0033`
- Avg end turn delta: `+0.0070`
- Attrition delta: `0.0000`
- Single-path monopoly delta: `-0.0107`
- Profile knobs: `bias=0.26/0.15/0.05`, `threshold=85/83/81`, `deltaScale=0.96`, `noise=2.02`, `shock=0.38x30`, `late=13|p0.120|c0.080|l0.130|cap0.055`
- Artifacts: `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04P_TARGETED-R164.csv`, `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04P_TARGETED-R164-KPI.md`

### R165 - r160-late-pressure-c

- KPI pass delta: `0`
- Overall win rate delta: `+0.0160`
- Hard win rate delta: `+0.0033`
- Avg end turn delta: `-0.0400`
- Attrition delta: `0.0000`
- Single-path monopoly delta: `+0.0099`
- Profile knobs: `bias=0.26/0.15/0.05`, `threshold=85/83/81`, `deltaScale=0.96`, `noise=2.02`, `shock=0.38x30`, `late=15|p0.070|c0.050|l0.090|cap0.030`
- Artifacts: `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04P_TARGETED-R165.csv`, `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04P_TARGETED-R165-KPI.md`

## 3. Best Round Selection

- Selected best round: `R165 (r160-late-pressure-c)`
- Reason: highest KPI pass count (`11/13`), then closest to standard win-rate / mid-turn pacing targets.

## 4. Remaining Gaps

- Failed KPI IDs in best round: WIN_RATE_EASY, DEFEAT_SHARE_OVERALL
