# R3-04 Parameter Tuning Impact Report

- Stage: R3 Balance and AI Tuning
- Task: R3-04 Parameter tuning iterations (3 rounds)
- Date: 2026-04-22

## 1. KPI Gate Score

| Round | KPI Pass | Win(E/S/H) | AvgEndTurn | Attrition | PathMonopoly |
|---|---:|---:|---:|---:|---:|
| R3-02 Run001 baseline | 0/13 | 1.0000/0.9700/0.0000 | 3.4620 | 0.0040 | 0.9055 |
| R196 (r194-pace-floor-a) | 11/13 | 0.7267/0.4475/0.2100 | 13.7490 | 0.1730 | 0.6196 |
| R197 (r194-pace-floor-b) | 12/13 | 0.7200/0.4500/0.2100 | 13.7630 | 0.1730 | 0.6187 |
| R198 (r194-pace-floor-c) | 12/13 | 0.7167/0.4500/0.2067 | 13.7750 | 0.1760 | 0.6171 |

## 2. Round-by-Round Delta

### R196 - r194-pace-floor-a

- KPI pass delta: `+11`
- Overall win rate delta: `-0.2280`
- Hard win rate delta: `+0.2100`
- Avg end turn delta: `+10.2870`
- Attrition delta: `+0.1690`
- Single-path monopoly delta: `-0.2859`
- Profile knobs: `bias=0.27/0.16/0.05`, `threshold=85/83/81`, `deltaScale=0.96`, `noise=2.02`, `shock=0.38x30`, `late=15|p0.093|c0.066|l0.106|cap0.036`
- Artifacts: `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04AA_TARGETED-R196.csv`, `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04AA_TARGETED-R196-KPI.md`

### R197 - r194-pace-floor-b

- KPI pass delta: `+1`
- Overall win rate delta: `-0.0010`
- Hard win rate delta: `0.0000`
- Avg end turn delta: `+0.0140`
- Attrition delta: `0.0000`
- Single-path monopoly delta: `-0.0008`
- Profile knobs: `bias=0.27/0.16/0.05`, `threshold=85/83/81`, `deltaScale=0.96`, `noise=2.02`, `shock=0.38x30`, `late=15|p0.095|c0.067|l0.108|cap0.037`
- Artifacts: `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04AA_TARGETED-R197.csv`, `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04AA_TARGETED-R197-KPI.md`

### R198 - r194-pace-floor-c

- KPI pass delta: `0`
- Overall win rate delta: `-0.0020`
- Hard win rate delta: `-0.0033`
- Avg end turn delta: `+0.0120`
- Attrition delta: `+0.0030`
- Single-path monopoly delta: `-0.0017`
- Profile knobs: `bias=0.27/0.16/0.05`, `threshold=85/83/81`, `deltaScale=0.96`, `noise=2.02`, `shock=0.38x30`, `late=14|p0.094|c0.066|l0.107|cap0.036`
- Artifacts: `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04AA_TARGETED-R198.csv`, `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04AA_TARGETED-R198-KPI.md`

## 3. Best Round Selection

- Selected best round: `R198 (r194-pace-floor-c)`
- Reason: highest KPI pass count (`12/13`), then closest to standard win-rate / mid-turn pacing targets.

## 4. Remaining Gaps

- Failed KPI IDs in best round: AVG_END_TURN_OVERALL
