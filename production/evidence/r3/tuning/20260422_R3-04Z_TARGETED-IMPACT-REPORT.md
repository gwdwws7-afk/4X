# R3-04 Parameter Tuning Impact Report

- Stage: R3 Balance and AI Tuning
- Task: R3-04 Parameter tuning iterations (3 rounds)
- Date: 2026-04-22

## 1. KPI Gate Score

| Round | KPI Pass | Win(E/S/H) | AvgEndTurn | Attrition | PathMonopoly |
|---|---:|---:|---:|---:|---:|
| R3-02 Run001 baseline | 0/13 | 1.0000/0.9700/0.0000 | 3.4620 | 0.0040 | 0.9055 |
| R193 (r185-gap-close-z-a) | 12/13 | 0.7100/0.4525/0.2000 | 13.8260 | 0.1800 | 0.6608 |
| R194 (r185-gap-close-z-b) | 12/13 | 0.7067/0.4525/0.2033 | 13.7920 | 0.1760 | 0.6145 |
| R195 (r185-gap-close-z-c) | 11/13 | 0.7133/0.4675/0.2133 | 13.5600 | 0.1780 | 0.6022 |

## 2. Round-by-Round Delta

### R193 - r185-gap-close-z-a

- KPI pass delta: `+12`
- Overall win rate delta: `-0.2340`
- Hard win rate delta: `+0.2000`
- Avg end turn delta: `+10.3640`
- Attrition delta: `+0.1760`
- Single-path monopoly delta: `-0.2447`
- Profile knobs: `bias=0.27/0.16/0.05`, `threshold=85/83/81`, `deltaScale=0.96`, `noise=2.02`, `shock=0.38x30`, `late=14|p0.097|c0.068|l0.112|cap0.039`
- Artifacts: `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04Z_TARGETED-R193.csv`, `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04Z_TARGETED-R193-KPI.md`

### R194 - r185-gap-close-z-b

- KPI pass delta: `0`
- Overall win rate delta: `0.0000`
- Hard win rate delta: `+0.0033`
- Avg end turn delta: `-0.0340`
- Attrition delta: `-0.0040`
- Single-path monopoly delta: `-0.0463`
- Profile knobs: `bias=0.27/0.16/0.05`, `threshold=85/83/81`, `deltaScale=0.96`, `noise=2.02`, `shock=0.38x30`, `late=14|p0.097|c0.070|l0.111|cap0.038`
- Artifacts: `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04Z_TARGETED-R194.csv`, `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04Z_TARGETED-R194-KPI.md`

### R195 - r185-gap-close-z-c

- KPI pass delta: `-1`
- Overall win rate delta: `+0.0110`
- Hard win rate delta: `+0.0100`
- Avg end turn delta: `-0.2320`
- Attrition delta: `+0.0020`
- Single-path monopoly delta: `-0.0124`
- Profile knobs: `bias=0.27/0.16/0.05`, `threshold=85/83/81`, `deltaScale=0.96`, `noise=2.02`, `shock=0.38x30`, `late=14|p0.097|c0.068|l0.110|cap0.037`
- Artifacts: `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04Z_TARGETED-R195.csv`, `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04Z_TARGETED-R195-KPI.md`

## 3. Best Round Selection

- Selected best round: `R193 (r185-gap-close-z-a)`
- Reason: highest KPI pass count (`12/13`), then closest to standard win-rate / mid-turn pacing targets.

## 4. Remaining Gaps

- Failed KPI IDs in best round: AVG_END_TURN_OVERALL
