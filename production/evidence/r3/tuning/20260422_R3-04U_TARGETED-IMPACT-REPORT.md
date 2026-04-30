# R3-04 Parameter Tuning Impact Report

- Stage: R3 Balance and AI Tuning
- Task: R3-04 Parameter tuning iterations (3 rounds)
- Date: 2026-04-22

## 1. KPI Gate Score

| Round | KPI Pass | Win(E/S/H) | AvgEndTurn | Attrition | PathMonopoly |
|---|---:|---:|---:|---:|---:|
| R3-02 Run001 baseline | 0/13 | 1.0000/0.9700/0.0000 | 3.4620 | 0.0040 | 0.9055 |
| R178 (r173-endturn-correct-a) | 13/13 | 0.6500/0.4550/0.2100 | 14.2730 | 0.2070 | 0.6318 |
| R179 (r173-endturn-correct-b) | 13/13 | 0.6500/0.4550/0.2100 | 14.2800 | 0.2070 | 0.6318 |
| R180 (r173-endturn-correct-c) | 13/13 | 0.6500/0.4600/0.2100 | 14.2620 | 0.2080 | 0.6312 |

## 2. Round-by-Round Delta

### R178 - r173-endturn-correct-a

- KPI pass delta: `+13`
- Overall win rate delta: `-0.2480`
- Hard win rate delta: `+0.2100`
- Avg end turn delta: `+10.8110`
- Attrition delta: `+0.2030`
- Single-path monopoly delta: `-0.2737`
- Profile knobs: `bias=0.27/0.16/0.05`, `threshold=85/83/81`, `deltaScale=0.96`, `noise=2.02`, `shock=0.38x30`, `late=13|p0.100|c0.070|l0.118|cap0.043`
- Artifacts: `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04U_TARGETED-R178.csv`, `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04U_TARGETED-R178-KPI.md`

### R179 - r173-endturn-correct-b

- KPI pass delta: `0`
- Overall win rate delta: `0.0000`
- Hard win rate delta: `0.0000`
- Avg end turn delta: `+0.0070`
- Attrition delta: `0.0000`
- Single-path monopoly delta: `0.0000`
- Profile knobs: `bias=0.27/0.16/0.05`, `threshold=85/83/81`, `deltaScale=0.96`, `noise=2.02`, `shock=0.38x30`, `late=13|p0.100|c0.070|l0.118|cap0.043`
- Artifacts: `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04U_TARGETED-R179.csv`, `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04U_TARGETED-R179-KPI.md`

### R180 - r173-endturn-correct-c

- KPI pass delta: `0`
- Overall win rate delta: `+0.0020`
- Hard win rate delta: `0.0000`
- Avg end turn delta: `-0.0180`
- Attrition delta: `+0.0010`
- Single-path monopoly delta: `-0.0006`
- Profile knobs: `bias=0.27/0.16/0.05`, `threshold=85/83/81`, `deltaScale=0.96`, `noise=2.02`, `shock=0.38x30`, `late=13|p0.100|c0.070|l0.118|cap0.043`
- Artifacts: `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04U_TARGETED-R180.csv`, `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04U_TARGETED-R180-KPI.md`

## 3. Best Round Selection

- Selected best round: `R180 (r173-endturn-correct-c)`
- Reason: highest KPI pass count (`13/13`), then closest to standard win-rate / mid-turn pacing targets.

## 4. Remaining Gaps

- No KPI hard-gate failures in best round.
