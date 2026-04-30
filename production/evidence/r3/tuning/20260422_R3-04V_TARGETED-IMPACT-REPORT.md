# R3-04 Parameter Tuning Impact Report

- Stage: R3 Balance and AI Tuning
- Task: R3-04 Parameter tuning iterations (3 rounds)
- Date: 2026-04-22

## 1. KPI Gate Score

| Round | KPI Pass | Win(E/S/H) | AvgEndTurn | Attrition | PathMonopoly |
|---|---:|---:|---:|---:|---:|
| R3-02 Run001 baseline | 0/13 | 1.0000/0.9700/0.0000 | 3.4620 | 0.0040 | 0.9055 |
| R181 (r180-stability-recover-a) | 11/13 | 0.6567/0.4400/0.2100 | 13.8630 | 0.1920 | 0.6491 |
| R182 (r180-stability-recover-b) | 11/13 | 0.6667/0.4400/0.2100 | 13.8150 | 0.1890 | 0.6538 |
| R183 (r180-stability-recover-c) | 11/13 | 0.6667/0.4400/0.2167 | 13.7890 | 0.1880 | 0.6531 |

## 2. Round-by-Round Delta

### R181 - r180-stability-recover-a

- KPI pass delta: `+11`
- Overall win rate delta: `-0.2520`
- Hard win rate delta: `+0.2100`
- Avg end turn delta: `+10.4010`
- Attrition delta: `+0.1880`
- Single-path monopoly delta: `-0.2564`
- Profile knobs: `bias=0.27/0.16/0.05`, `threshold=85/83/81`, `deltaScale=0.96`, `noise=2.02`, `shock=0.38x30`, `late=14|p0.096|c0.067|l0.114|cap0.041`
- Artifacts: `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04V_TARGETED-R181.csv`, `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04V_TARGETED-R181-KPI.md`

### R182 - r180-stability-recover-b

- KPI pass delta: `0`
- Overall win rate delta: `+0.0030`
- Hard win rate delta: `0.0000`
- Avg end turn delta: `-0.0480`
- Attrition delta: `-0.0030`
- Single-path monopoly delta: `+0.0047`
- Profile knobs: `bias=0.27/0.16/0.05`, `threshold=85/83/81`, `deltaScale=0.96`, `noise=2.02`, `shock=0.38x30`, `late=14|p0.095|c0.066|l0.113|cap0.041`
- Artifacts: `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04V_TARGETED-R182.csv`, `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04V_TARGETED-R182-KPI.md`

### R183 - r180-stability-recover-c

- KPI pass delta: `0`
- Overall win rate delta: `+0.0020`
- Hard win rate delta: `+0.0067`
- Avg end turn delta: `-0.0260`
- Attrition delta: `-0.0010`
- Single-path monopoly delta: `-0.0007`
- Profile knobs: `bias=0.27/0.16/0.05`, `threshold=85/83/81`, `deltaScale=0.96`, `noise=2.02`, `shock=0.38x30`, `late=14|p0.094|c0.066|l0.112|cap0.040`
- Artifacts: `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04V_TARGETED-R183.csv`, `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04V_TARGETED-R183-KPI.md`

## 3. Best Round Selection

- Selected best round: `R181 (r180-stability-recover-a)`
- Reason: highest KPI pass count (`11/13`), then closest to standard win-rate / mid-turn pacing targets.

## 4. Remaining Gaps

- Failed KPI IDs in best round: WIN_RATE_STANDARD, AVG_END_TURN_OVERALL
