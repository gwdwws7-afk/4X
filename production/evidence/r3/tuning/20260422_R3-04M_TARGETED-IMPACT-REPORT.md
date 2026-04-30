# R3-04 Parameter Tuning Impact Report

- Stage: R3 Balance and AI Tuning
- Task: R3-04 Parameter tuning iterations (3 rounds)
- Date: 2026-04-22

## 1. KPI Gate Score

| Round | KPI Pass | Win(E/S/H) | AvgEndTurn | Attrition | PathMonopoly |
|---|---:|---:|---:|---:|---:|
| R3-02 Run001 baseline | 0/13 | 1.0000/0.9700/0.0000 | 3.4620 | 0.0040 | 0.9055 |
| R151 (r149-defeat-floor-recover-a) | 13/13 | 0.6600/0.4550/0.2000 | 14.1220 | 0.2070 | 0.6364 |
| R152 (r149-defeat-floor-recover-b) | 13/13 | 0.6633/0.4500/0.2000 | 14.1140 | 0.2060 | 0.6401 |
| R153 (r149-defeat-floor-recover-c) | 12/13 | 0.6600/0.4500/0.2200 | 14.1140 | 0.2090 | 0.6306 |

## 2. Round-by-Round Delta

### R151 - r149-defeat-floor-recover-a

- KPI pass delta: `+13`
- Overall win rate delta: `-0.2480`
- Hard win rate delta: `+0.2000`
- Avg end turn delta: `+10.6600`
- Attrition delta: `+0.2030`
- Single-path monopoly delta: `-0.2691`
- Profile knobs: `bias=0.26/0.16/0.05`, `threshold=85/83/81`, `deltaScale=0.96`, `noise=2.02`, `shock=0.38x30`
- Artifacts: `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04M_TARGETED-R151.csv`, `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04M_TARGETED-R151-KPI.md`

### R152 - r149-defeat-floor-recover-b

- KPI pass delta: `0`
- Overall win rate delta: `-0.0010`
- Hard win rate delta: `0.0000`
- Avg end turn delta: `-0.0080`
- Attrition delta: `-0.0010`
- Single-path monopoly delta: `+0.0037`
- Profile knobs: `bias=0.26/0.16/0.05`, `threshold=85/83/81`, `deltaScale=0.96`, `noise=2.02`, `shock=0.38x30`
- Artifacts: `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04M_TARGETED-R152.csv`, `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04M_TARGETED-R152-KPI.md`

### R153 - r149-defeat-floor-recover-c

- KPI pass delta: `-1`
- Overall win rate delta: `+0.0050`
- Hard win rate delta: `+0.0200`
- Avg end turn delta: `0.0000`
- Attrition delta: `+0.0030`
- Single-path monopoly delta: `-0.0095`
- Profile knobs: `bias=0.26/0.16/0.05`, `threshold=85/83/81`, `deltaScale=0.96`, `noise=2.02`, `shock=0.38x30`
- Artifacts: `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04M_TARGETED-R153.csv`, `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04M_TARGETED-R153-KPI.md`

## 3. Best Round Selection

- Selected best round: `R151 (r149-defeat-floor-recover-a)`
- Reason: highest KPI pass count (`13/13`), then closest to standard win-rate / mid-turn pacing targets.

## 4. Remaining Gaps

- No KPI hard-gate failures in best round.
