# R3-04 Parameter Tuning Impact Report

- Stage: R3 Balance and AI Tuning
- Task: R3-04 Parameter tuning iterations (3 rounds)
- Date: 2026-04-23

## 1. KPI Gate Score

| Round | KPI Pass | Win(E/S/H) | AvgEndTurn | Attrition | PathMonopoly |
|---|---:|---:|---:|---:|---:|
| R3-02 Run001 baseline | 0/13 | 1.0000/0.9700/0.0000 | 3.4620 | 0.0040 | 0.9055 |
| R290 (r287-standard-recover-a) | 13/13 | 0.7133/0.4600/0.2333 | 14.2420 | 0.1780 | 0.6239 |
| R291 (r287-standard-recover-b) | 13/13 | 0.7133/0.4550/0.2333 | 14.2670 | 0.1790 | 0.6245 |
| R292 (r287-standard-recover-c) | 13/13 | 0.7133/0.4600/0.2333 | 14.2260 | 0.1780 | 0.6239 |
| R293 (r287-standard-recover-d) | 13/13 | 0.7067/0.4600/0.2333 | 14.2670 | 0.1790 | 0.6223 |
| R294 (r287-standard-recover-e) | 13/13 | 0.7133/0.4600/0.2367 | 14.2320 | 0.1790 | 0.6226 |
| R295 (r287-standard-recover-f) | 13/13 | 0.7133/0.4550/0.2367 | 14.2580 | 0.1800 | 0.6231 |

## 2. Round-by-Round Delta

### R290 - r287-standard-recover-a

- KPI pass delta: `+13`
- Overall win rate delta: `-0.2200`
- Hard win rate delta: `+0.2333`
- Avg end turn delta: `+10.7800`
- Attrition delta: `+0.1740`
- Single-path monopoly delta: `-0.2816`
- Profile knobs: `bias=0.27/0.16/0.06`, `threshold=85/83/81`, `deltaScale=0.96`, `noise=2.02`, `shock=0.38x30`, `late=14|p0.094|c0.071|l0.122|cap0.039`
- Artifacts: `C:\test\4X\production\evidence\r3\tuning\20260423_R3-04AG_TARGETED-R290.csv`, `C:\test\4X\production\evidence\r3\tuning\20260423_R3-04AG_TARGETED-R290-KPI.md`

### R291 - r287-standard-recover-b

- KPI pass delta: `0`
- Overall win rate delta: `-0.0020`
- Hard win rate delta: `0.0000`
- Avg end turn delta: `+0.0250`
- Attrition delta: `+0.0010`
- Single-path monopoly delta: `+0.0005`
- Profile knobs: `bias=0.27/0.16/0.06`, `threshold=85/83/81`, `deltaScale=0.96`, `noise=2.02`, `shock=0.38x30`, `late=14|p0.094|c0.071|l0.122|cap0.039`
- Artifacts: `C:\test\4X\production\evidence\r3\tuning\20260423_R3-04AG_TARGETED-R291.csv`, `C:\test\4X\production\evidence\r3\tuning\20260423_R3-04AG_TARGETED-R291-KPI.md`

### R292 - r287-standard-recover-c

- KPI pass delta: `0`
- Overall win rate delta: `+0.0020`
- Hard win rate delta: `0.0000`
- Avg end turn delta: `-0.0410`
- Attrition delta: `-0.0010`
- Single-path monopoly delta: `-0.0005`
- Profile knobs: `bias=0.27/0.16/0.06`, `threshold=85/83/81`, `deltaScale=0.96`, `noise=2.02`, `shock=0.38x30`, `late=14|p0.094|c0.071|l0.122|cap0.039`
- Artifacts: `C:\test\4X\production\evidence\r3\tuning\20260423_R3-04AG_TARGETED-R292.csv`, `C:\test\4X\production\evidence\r3\tuning\20260423_R3-04AG_TARGETED-R292-KPI.md`

### R293 - r287-standard-recover-d

- KPI pass delta: `0`
- Overall win rate delta: `-0.0020`
- Hard win rate delta: `0.0000`
- Avg end turn delta: `+0.0410`
- Attrition delta: `+0.0010`
- Single-path monopoly delta: `-0.0016`
- Profile knobs: `bias=0.27/0.16/0.06`, `threshold=85/83/81`, `deltaScale=0.96`, `noise=2.02`, `shock=0.38x30`, `late=14|p0.094|c0.071|l0.122|cap0.039`
- Artifacts: `C:\test\4X\production\evidence\r3\tuning\20260423_R3-04AG_TARGETED-R293.csv`, `C:\test\4X\production\evidence\r3\tuning\20260423_R3-04AG_TARGETED-R293-KPI.md`

### R294 - r287-standard-recover-e

- KPI pass delta: `0`
- Overall win rate delta: `+0.0030`
- Hard win rate delta: `+0.0033`
- Avg end turn delta: `-0.0350`
- Attrition delta: `0.0000`
- Single-path monopoly delta: `+0.0003`
- Profile knobs: `bias=0.27/0.16/0.06`, `threshold=85/83/81`, `deltaScale=0.96`, `noise=2.02`, `shock=0.38x30`, `late=14|p0.094|c0.071|l0.122|cap0.039`
- Artifacts: `C:\test\4X\production\evidence\r3\tuning\20260423_R3-04AG_TARGETED-R294.csv`, `C:\test\4X\production\evidence\r3\tuning\20260423_R3-04AG_TARGETED-R294-KPI.md`

### R295 - r287-standard-recover-f

- KPI pass delta: `0`
- Overall win rate delta: `-0.0020`
- Hard win rate delta: `0.0000`
- Avg end turn delta: `+0.0260`
- Attrition delta: `+0.0010`
- Single-path monopoly delta: `+0.0005`
- Profile knobs: `bias=0.27/0.16/0.06`, `threshold=85/83/81`, `deltaScale=0.96`, `noise=2.02`, `shock=0.38x30`, `late=14|p0.094|c0.071|l0.122|cap0.039`
- Artifacts: `C:\test\4X\production\evidence\r3\tuning\20260423_R3-04AG_TARGETED-R295.csv`, `C:\test\4X\production\evidence\r3\tuning\20260423_R3-04AG_TARGETED-R295-KPI.md`

## 3. Best Round Selection

- Selected best round: `R293 (r287-standard-recover-d)`
- Reason: highest KPI pass count (`13/13`), then closest to standard win-rate / mid-turn pacing targets.

## 4. Remaining Gaps

- No KPI hard-gate failures in best round.
