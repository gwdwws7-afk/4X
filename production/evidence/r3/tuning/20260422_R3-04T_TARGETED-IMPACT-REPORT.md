# R3-04 Parameter Tuning Impact Report

- Stage: R3 Balance and AI Tuning
- Task: R3-04 Parameter tuning iterations (3 rounds)
- Date: 2026-04-22

## 1. KPI Gate Score

| Round | KPI Pass | Win(E/S/H) | AvgEndTurn | Attrition | PathMonopoly |
|---|---:|---:|---:|---:|---:|
| R3-02 Run001 baseline | 0/13 | 1.0000/0.9700/0.0000 | 3.4620 | 0.0040 | 0.9055 |
| R175 (r173-endturn-floor-a) | 11/13 | 0.7200/0.4525/0.2367 | 13.5580 | 0.1850 | 0.6517 |
| R176 (r173-endturn-floor-b) | 11/13 | 0.7267/0.4525/0.2300 | 13.5550 | 0.1890 | 0.6560 |
| R177 (r173-endturn-floor-c) | 11/13 | 0.7200/0.4550/0.2367 | 13.5510 | 0.1840 | 0.6503 |

## 2. Round-by-Round Delta

### R175 - r173-endturn-floor-a

- KPI pass delta: `+11`
- Overall win rate delta: `-0.2200`
- Hard win rate delta: `+0.2367`
- Avg end turn delta: `+10.0960`
- Attrition delta: `+0.1810`
- Single-path monopoly delta: `-0.2538`
- Profile knobs: `bias=0.27/0.16/0.05`, `threshold=85/83/81`, `deltaScale=0.96`, `noise=2.02`, `shock=0.38x30`, `late=13|p0.099|c0.069|l0.118|cap0.043`
- Artifacts: `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04T_TARGETED-R175.csv`, `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04T_TARGETED-R175-KPI.md`

### R176 - r173-endturn-floor-b

- KPI pass delta: `0`
- Overall win rate delta: `0.0000`
- Hard win rate delta: `-0.0067`
- Avg end turn delta: `-0.0030`
- Attrition delta: `+0.0040`
- Single-path monopoly delta: `+0.0043`
- Profile knobs: `bias=0.27/0.16/0.05`, `threshold=85/83/81`, `deltaScale=0.96`, `noise=2.02`, `shock=0.38x30`, `late=14|p0.096|c0.067|l0.116|cap0.042`
- Artifacts: `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04T_TARGETED-R176.csv`, `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04T_TARGETED-R176-KPI.md`

### R177 - r173-endturn-floor-c

- KPI pass delta: `0`
- Overall win rate delta: `+0.0010`
- Hard win rate delta: `+0.0067`
- Avg end turn delta: `-0.0040`
- Attrition delta: `-0.0050`
- Single-path monopoly delta: `-0.0057`
- Profile knobs: `bias=0.27/0.16/0.05`, `threshold=85/83/81`, `deltaScale=0.96`, `noise=2.02`, `shock=0.38x30`, `late=13|p0.100|c0.070|l0.121|cap0.044`
- Artifacts: `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04T_TARGETED-R177.csv`, `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04T_TARGETED-R177-KPI.md`

## 3. Best Round Selection

- Selected best round: `R177 (r173-endturn-floor-c)`
- Reason: highest KPI pass count (`11/13`), then closest to standard win-rate / mid-turn pacing targets.

## 4. Remaining Gaps

- Failed KPI IDs in best round: AVG_END_TURN_OVERALL, DEFEAT_SHARE_OVERALL
