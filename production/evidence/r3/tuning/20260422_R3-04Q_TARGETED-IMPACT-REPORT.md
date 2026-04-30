# R3-04 Parameter Tuning Impact Report

- Stage: R3 Balance and AI Tuning
- Task: R3-04 Parameter tuning iterations (3 rounds)
- Date: 2026-04-22

## 1. KPI Gate Score

| Round | KPI Pass | Win(E/S/H) | AvgEndTurn | Attrition | PathMonopoly |
|---|---:|---:|---:|---:|---:|
| R3-02 Run001 baseline | 0/13 | 1.0000/0.9700/0.0000 | 3.4620 | 0.0040 | 0.9055 |
| R166 (r165-dual-gap-close-a) | 12/13 | 0.7267/0.4400/0.2000 | 14.1230 | 0.1840 | 0.6872 |
| R167 (r165-dual-gap-close-b) | 11/13 | 0.7100/0.4225/0.1800 | 14.0780 | 0.1890 | 0.6858 |
| R168 (r165-dual-gap-close-c) | 11/13 | 0.7533/0.4600/0.1867 | 13.8620 | 0.1770 | 0.6760 |

## 2. Round-by-Round Delta

### R166 - r165-dual-gap-close-a

- KPI pass delta: `+12`
- Overall win rate delta: `-0.2340`
- Hard win rate delta: `+0.2000`
- Avg end turn delta: `+10.6610`
- Attrition delta: `+0.1800`
- Single-path monopoly delta: `-0.2183`
- Profile knobs: `bias=0.27/0.15/0.05`, `threshold=85/83/81`, `deltaScale=0.96`, `noise=2.02`, `shock=0.38x30`, `late=14|p0.095|c0.065|l0.110|cap0.042`
- Artifacts: `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04Q_TARGETED-R166.csv`, `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04Q_TARGETED-R166-KPI.md`

### R167 - r165-dual-gap-close-b

- KPI pass delta: `-1`
- Overall win rate delta: `-0.0180`
- Hard win rate delta: `-0.0200`
- Avg end turn delta: `-0.0450`
- Attrition delta: `+0.0050`
- Single-path monopoly delta: `-0.0014`
- Profile knobs: `bias=0.27/0.15/0.05`, `threshold=84/83/81`, `deltaScale=0.96`, `noise=2.02`, `shock=0.38x30`, `late=13|p0.110|c0.075|l0.125|cap0.050`
- Artifacts: `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04Q_TARGETED-R167.csv`, `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04Q_TARGETED-R167-KPI.md`

### R168 - r165-dual-gap-close-c

- KPI pass delta: `0`
- Overall win rate delta: `+0.0300`
- Hard win rate delta: `+0.0067`
- Avg end turn delta: `-0.2160`
- Attrition delta: `-0.0120`
- Single-path monopoly delta: `-0.0098`
- Profile knobs: `bias=0.28/0.15/0.05`, `threshold=84/83/81`, `deltaScale=0.96`, `noise=2.02`, `shock=0.38x30`, `late=15|p0.080|c0.055|l0.105|cap0.035`
- Artifacts: `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04Q_TARGETED-R168.csv`, `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04Q_TARGETED-R168-KPI.md`

## 3. Best Round Selection

- Selected best round: `R166 (r165-dual-gap-close-a)`
- Reason: highest KPI pass count (`12/13`), then closest to standard win-rate / mid-turn pacing targets.

## 4. Remaining Gaps

- Failed KPI IDs in best round: WIN_RATE_STANDARD
