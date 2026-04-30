# R3-04 Parameter Tuning Impact Report

- Stage: R3 Balance and AI Tuning
- Task: R3-04 Parameter tuning iterations (3 rounds)
- Date: 2026-04-20

## 1. KPI Gate Score

| Round | KPI Pass | Win(E/S/H) | AvgEndTurn | Attrition | PathMonopoly |
|---|---:|---:|---:|---:|---:|
| R3-02 Run001 baseline | 0/13 | 1.0000/0.9700/0.0000 | 3.4620 | 0.0040 | 0.9055 |
| R19 (axis-shift-balanced) | 7/13 | 0.5333/0.3525/0.1133 | 14.9600 | 0.2270 | 0.9761 |
| R20 (window-tighten) | 7/13 | 0.4033/0.2425/0.0633 | 14.5700 | 0.2340 | 0.9873 |
| R21 (win-floor-guard) | 8/13 | 0.5967/0.4350/0.1433 | 14.5180 | 0.2010 | 0.9773 |

## 2. Round-by-Round Delta

### R19 - axis-shift-balanced

- KPI pass delta: `+7`
- Overall win rate delta: `-0.3530`
- Hard win rate delta: `+0.1133`
- Avg end turn delta: `+11.4980`
- Attrition delta: `+0.2230`
- Single-path monopoly delta: `+0.0706`
- Profile knobs: `bias=0.18/0.10/0.00`, `threshold=88/85/82`, `deltaScale=0.93`, `noise=2.00`, `shock=0.40x32`
- Artifacts: `C:\test\4X\production\evidence\r3\tuning\20260420_R3-04D_TARGETED-R19.csv`, `C:\test\4X\production\evidence\r3\tuning\20260420_R3-04D_TARGETED-R19-KPI.md`

### R20 - window-tighten

- KPI pass delta: `0`
- Overall win rate delta: `-0.0980`
- Hard win rate delta: `-0.0500`
- Avg end turn delta: `-0.3900`
- Attrition delta: `+0.0070`
- Single-path monopoly delta: `+0.0112`
- Profile knobs: `bias=0.16/0.09/-0.01`, `threshold=89/86/83`, `deltaScale=0.92`, `noise=2.10`, `shock=0.42x34`
- Artifacts: `C:\test\4X\production\evidence\r3\tuning\20260420_R3-04D_TARGETED-R20.csv`, `C:\test\4X\production\evidence\r3\tuning\20260420_R3-04D_TARGETED-R20-KPI.md`

### R21 - win-floor-guard

- KPI pass delta: `+1`
- Overall win rate delta: `+0.1590`
- Hard win rate delta: `+0.0800`
- Avg end turn delta: `-0.0520`
- Attrition delta: `-0.0330`
- Single-path monopoly delta: `-0.0101`
- Profile knobs: `bias=0.20/0.11/0.01`, `threshold=88/85/82`, `deltaScale=0.94`, `noise=1.95`, `shock=0.38x30`
- Artifacts: `C:\test\4X\production\evidence\r3\tuning\20260420_R3-04D_TARGETED-R21.csv`, `C:\test\4X\production\evidence\r3\tuning\20260420_R3-04D_TARGETED-R21-KPI.md`

## 3. Best Round Selection

- Selected best round: `R21 (win-floor-guard)`
- Reason: highest KPI pass count (`8/13`), then closest to standard win-rate / mid-turn pacing targets.

## 4. Remaining Gaps

- Failed KPI IDs in best round: WIN_RATE_EASY, WIN_RATE_STANDARD, WIN_RATE_HARD, WIN_RATE_OVERALL, SINGLE_PATH_VICTORY_MONOPOLY
