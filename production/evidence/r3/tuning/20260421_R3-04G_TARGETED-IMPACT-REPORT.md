# R3-04 Parameter Tuning Impact Report

- Stage: R3 Balance and AI Tuning
- Task: R3-04 Parameter tuning iterations (3 rounds)
- Date: 2026-04-21

## 1. KPI Gate Score

| Round | KPI Pass | Win(E/S/H) | AvgEndTurn | Attrition | PathMonopoly |
|---|---:|---:|---:|---:|---:|
| R3-02 Run001 baseline | 0/13 | 1.0000/0.9700/0.0000 | 3.4620 | 0.0040 | 0.9055 |
| R70 (r64-clone-hard-edge-a) | 12/13 | 0.6800/0.4500/0.1833 | 14.0580 | 0.1970 | 0.6788 |
| R71 (r64-clone-hard-edge-b) | 9/13 | 0.6367/0.4425/0.1500 | 14.8240 | 0.2090 | 0.7312 |
| R72 (r64-clone-hard-edge-c) | 10/13 | 0.6167/0.4500/0.1600 | 14.5800 | 0.2050 | 0.7385 |

## 2. Round-by-Round Delta

### R70 - r64-clone-hard-edge-a

- KPI pass delta: `+12`
- Overall win rate delta: `-0.2490`
- Hard win rate delta: `+0.1833`
- Avg end turn delta: `+10.5960`
- Attrition delta: `+0.1930`
- Single-path monopoly delta: `-0.2267`
- Profile knobs: `bias=0.24/0.14/0.05`, `threshold=85/83/82`, `deltaScale=0.96`, `noise=2.05`, `shock=0.41x33`
- Artifacts: `C:\test\4X\production\evidence\r3\tuning\20260421_R3-04G_TARGETED-R70.csv`, `C:\test\4X\production\evidence\r3\tuning\20260421_R3-04G_TARGETED-R70-KPI.md`

### R71 - r64-clone-hard-edge-b

- KPI pass delta: `-3`
- Overall win rate delta: `-0.0260`
- Hard win rate delta: `-0.0333`
- Avg end turn delta: `+0.7660`
- Attrition delta: `+0.0120`
- Single-path monopoly delta: `+0.0524`
- Profile knobs: `bias=0.24/0.14/0.05`, `threshold=85/83/82`, `deltaScale=0.96`, `noise=2.05`, `shock=0.41x33`
- Artifacts: `C:\test\4X\production\evidence\r3\tuning\20260421_R3-04G_TARGETED-R71.csv`, `C:\test\4X\production\evidence\r3\tuning\20260421_R3-04G_TARGETED-R71-KPI.md`

### R72 - r64-clone-hard-edge-c

- KPI pass delta: `+1`
- Overall win rate delta: `0.0000`
- Hard win rate delta: `+0.0100`
- Avg end turn delta: `-0.2440`
- Attrition delta: `-0.0040`
- Single-path monopoly delta: `+0.0073`
- Profile knobs: `bias=0.24/0.14/0.05`, `threshold=85/83/82`, `deltaScale=0.96`, `noise=2.05`, `shock=0.41x33`
- Artifacts: `C:\test\4X\production\evidence\r3\tuning\20260421_R3-04G_TARGETED-R72.csv`, `C:\test\4X\production\evidence\r3\tuning\20260421_R3-04G_TARGETED-R72-KPI.md`

## 3. Best Round Selection

- Selected best round: `R70 (r64-clone-hard-edge-a)`
- Reason: highest KPI pass count (`12/13`), then closest to standard win-rate / mid-turn pacing targets.

## 4. Remaining Gaps

- Failed KPI IDs in best round: WIN_RATE_HARD
