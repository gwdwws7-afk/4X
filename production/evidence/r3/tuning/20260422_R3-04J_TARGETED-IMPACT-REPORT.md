# R3-04 Parameter Tuning Impact Report

- Stage: R3 Balance and AI Tuning
- Task: R3-04 Parameter tuning iterations (3 rounds)
- Date: 2026-04-22

## 1. KPI Gate Score

| Round | KPI Pass | Win(E/S/H) | AvgEndTurn | Attrition | PathMonopoly |
|---|---:|---:|---:|---:|---:|
| R3-02 Run001 baseline | 0/13 | 1.0000/0.9700/0.0000 | 3.4620 | 0.0040 | 0.9055 |
| R115 (r112-hard-recover-floor-a) | 11/13 | 0.5933/0.4250/0.2700 | 14.4520 | 0.2210 | 0.6737 |
| R116 (r112-hard-recover-floor-b) | 10/13 | 0.6067/0.4375/0.2767 | 14.3700 | 0.2140 | 0.6705 |
| R117 (r112-hard-recover-floor-c) | 10/13 | 0.5900/0.4450/0.2633 | 14.3030 | 0.2200 | 0.6774 |

## 2. Round-by-Round Delta

### R115 - r112-hard-recover-floor-a

- KPI pass delta: `+11`
- Overall win rate delta: `-0.2590`
- Hard win rate delta: `+0.2700`
- Avg end turn delta: `+10.9900`
- Attrition delta: `+0.2170`
- Single-path monopoly delta: `-0.2318`
- Profile knobs: `bias=0.26/0.15/0.05`, `threshold=86/83/81`, `deltaScale=0.96`, `noise=2.02`, `shock=0.38x30`
- Artifacts: `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04J_TARGETED-R115.csv`, `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04J_TARGETED-R115-KPI.md`

### R116 - r112-hard-recover-floor-b

- KPI pass delta: `-1`
- Overall win rate delta: `+0.0110`
- Hard win rate delta: `+0.0067`
- Avg end turn delta: `-0.0820`
- Attrition delta: `-0.0070`
- Single-path monopoly delta: `-0.0032`
- Profile knobs: `bias=0.26/0.15/0.05`, `threshold=86/83/81`, `deltaScale=0.96`, `noise=2.02`, `shock=0.38x30`
- Artifacts: `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04J_TARGETED-R116.csv`, `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04J_TARGETED-R116-KPI.md`

### R117 - r112-hard-recover-floor-c

- KPI pass delta: `0`
- Overall win rate delta: `-0.0060`
- Hard win rate delta: `-0.0133`
- Avg end turn delta: `-0.0670`
- Attrition delta: `+0.0060`
- Single-path monopoly delta: `+0.0070`
- Profile knobs: `bias=0.26/0.15/0.05`, `threshold=87/83/81`, `deltaScale=0.96`, `noise=2.02`, `shock=0.38x30`
- Artifacts: `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04J_TARGETED-R117.csv`, `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04J_TARGETED-R117-KPI.md`

## 3. Best Round Selection

- Selected best round: `R115 (r112-hard-recover-floor-a)`
- Reason: highest KPI pass count (`11/13`), then closest to standard win-rate / mid-turn pacing targets.

## 4. Remaining Gaps

- Failed KPI IDs in best round: WIN_RATE_EASY, WIN_RATE_STANDARD
