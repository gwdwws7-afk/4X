# R3-04 Parameter Tuning Impact Report

- Stage: R3 Balance and AI Tuning
- Task: R3-04 Parameter tuning iterations (3 rounds)
- Date: 2026-04-22

## 1. KPI Gate Score

| Round | KPI Pass | Win(E/S/H) | AvgEndTurn | Attrition | PathMonopoly |
|---|---:|---:|---:|---:|---:|
| R3-02 Run001 baseline | 0/13 | 1.0000/0.9700/0.0000 | 3.4620 | 0.0040 | 0.9055 |
| R136 (r134-avgturn-recover-a) | 13/13 | 0.6767/0.4625/0.2333 | 14.1640 | 0.1910 | 0.6332 |
| R137 (r134-avgturn-recover-b) | 12/13 | 0.6367/0.4600/0.2200 | 14.1900 | 0.2000 | 0.6304 |
| R138 (r134-avgturn-recover-c) | 12/13 | 0.6300/0.4550/0.2367 | 14.3300 | 0.2040 | 0.6290 |

## 2. Round-by-Round Delta

### R136 - r134-avgturn-recover-a

- KPI pass delta: `+13`
- Overall win rate delta: `-0.2300`
- Hard win rate delta: `+0.2333`
- Avg end turn delta: `+10.7020`
- Attrition delta: `+0.1870`
- Single-path monopoly delta: `-0.2723`
- Profile knobs: `bias=0.26/0.16/0.05`, `threshold=85/83/81`, `deltaScale=0.96`, `noise=2.02`, `shock=0.38x30`
- Artifacts: `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04L_TARGETED-R136.csv`, `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04L_TARGETED-R136-KPI.md`

### R137 - r134-avgturn-recover-b

- KPI pass delta: `-1`
- Overall win rate delta: `-0.0170`
- Hard win rate delta: `-0.0133`
- Avg end turn delta: `+0.0260`
- Attrition delta: `+0.0090`
- Single-path monopoly delta: `-0.0028`
- Profile knobs: `bias=0.26/0.16/0.05`, `threshold=85/83/81`, `deltaScale=0.96`, `noise=2.02`, `shock=0.38x30`
- Artifacts: `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04L_TARGETED-R137.csv`, `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04L_TARGETED-R137-KPI.md`

### R138 - r134-avgturn-recover-c

- KPI pass delta: `0`
- Overall win rate delta: `+0.0010`
- Hard win rate delta: `+0.0167`
- Avg end turn delta: `+0.1400`
- Attrition delta: `+0.0040`
- Single-path monopoly delta: `-0.0014`
- Profile knobs: `bias=0.26/0.15/0.05`, `threshold=85/83/81`, `deltaScale=0.96`, `noise=2.02`, `shock=0.38x30`
- Artifacts: `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04L_TARGETED-R138.csv`, `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04L_TARGETED-R138-KPI.md`

## 3. Best Round Selection

- Selected best round: `R136 (r134-avgturn-recover-a)`
- Reason: highest KPI pass count (`13/13`), then closest to standard win-rate / mid-turn pacing targets.

## 4. Remaining Gaps

- No KPI hard-gate failures in best round.
