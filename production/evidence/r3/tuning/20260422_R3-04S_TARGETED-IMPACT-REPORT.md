# R3-04 Parameter Tuning Impact Report

- Stage: R3 Balance and AI Tuning
- Task: R3-04 Parameter tuning iterations (3 rounds)
- Date: 2026-04-22

## 1. KPI Gate Score

| Round | KPI Pass | Win(E/S/H) | AvgEndTurn | Attrition | PathMonopoly |
|---|---:|---:|---:|---:|---:|
| R3-02 Run001 baseline | 0/13 | 1.0000/0.9700/0.0000 | 3.4620 | 0.0040 | 0.9055 |
| R172 (r171-defeat-lift-a) | 11/13 | 0.7067/0.4650/0.2167 | 13.8390 | 0.1890 | 0.6415 |
| R173 (r171-defeat-lift-b) | 12/13 | 0.6933/0.4525/0.2133 | 13.8950 | 0.1950 | 0.6380 |
| R174 (r171-defeat-lift-c) | 11/13 | 0.7033/0.4675/0.2167 | 13.7420 | 0.1900 | 0.6371 |

## 2. Round-by-Round Delta

### R172 - r171-defeat-lift-a

- KPI pass delta: `+11`
- Overall win rate delta: `-0.2250`
- Hard win rate delta: `+0.2167`
- Avg end turn delta: `+10.3770`
- Attrition delta: `+0.1850`
- Single-path monopoly delta: `-0.2640`
- Profile knobs: `bias=0.27/0.16/0.05`, `threshold=85/83/81`, `deltaScale=0.96`, `noise=2.02`, `shock=0.38x30`, `late=14|p0.094|c0.066|l0.114|cap0.041`
- Artifacts: `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04S_TARGETED-R172.csv`, `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04S_TARGETED-R172-KPI.md`

### R173 - r171-defeat-lift-b

- KPI pass delta: `+1`
- Overall win rate delta: `-0.0100`
- Hard win rate delta: `-0.0033`
- Avg end turn delta: `+0.0560`
- Attrition delta: `+0.0060`
- Single-path monopoly delta: `-0.0035`
- Profile knobs: `bias=0.27/0.16/0.05`, `threshold=85/83/81`, `deltaScale=0.96`, `noise=2.02`, `shock=0.38x30`, `late=13|p0.100|c0.070|l0.118|cap0.043`
- Artifacts: `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04S_TARGETED-R173.csv`, `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04S_TARGETED-R173-KPI.md`

### R174 - r171-defeat-lift-c

- KPI pass delta: `-1`
- Overall win rate delta: `+0.0100`
- Hard win rate delta: `+0.0033`
- Avg end turn delta: `-0.1530`
- Attrition delta: `-0.0050`
- Single-path monopoly delta: `-0.0008`
- Profile knobs: `bias=0.27/0.16/0.05`, `threshold=85/83/81`, `deltaScale=0.96`, `noise=2.02`, `shock=0.38x30`, `late=14|p0.095|c0.066|l0.112|cap0.040`
- Artifacts: `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04S_TARGETED-R174.csv`, `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04S_TARGETED-R174-KPI.md`

## 3. Best Round Selection

- Selected best round: `R173 (r171-defeat-lift-b)`
- Reason: highest KPI pass count (`12/13`), then closest to standard win-rate / mid-turn pacing targets.

## 4. Remaining Gaps

- Failed KPI IDs in best round: AVG_END_TURN_OVERALL
