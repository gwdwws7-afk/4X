# R3-04 Parameter Tuning Impact Report

- Stage: R3 Balance and AI Tuning
- Task: R3-04 Parameter tuning iterations (3 rounds)
- Date: 2026-04-23

## 1. KPI Gate Score

| Round | KPI Pass | Win(E/S/H) | AvgEndTurn | Attrition | PathMonopoly |
|---|---:|---:|---:|---:|---:|
| R3-02 Run001 baseline | 0/13 | 1.0000/0.9700/0.0000 | 3.4620 | 0.0040 | 0.9055 |
| R202 (r201-stability-close-a) | 11/13 | 0.7167/0.4375/0.1933 | 14.3980 | 0.1930 | 0.6429 |
| R203 (r201-stability-close-b) | 12/13 | 0.7467/0.4575/0.2200 | 14.1160 | 0.1800 | 0.6364 |
| R204 (r201-stability-close-c) | 13/13 | 0.7133/0.4575/0.2067 | 14.4720 | 0.1890 | 0.6383 |

## 2. Round-by-Round Delta

### R202 - r201-stability-close-a

- KPI pass delta: `+11`
- Overall win rate delta: `-0.2400`
- Hard win rate delta: `+0.1933`
- Avg end turn delta: `+10.9360`
- Attrition delta: `+0.1890`
- Single-path monopoly delta: `-0.2626`
- Profile knobs: `bias=0.27/0.16/0.05`, `threshold=85/83/81`, `deltaScale=0.96`, `noise=2.02`, `shock=0.34x28`, `late=14|p0.094|c0.066|l0.107|cap0.036`
- Artifacts: `C:\test\4X\production\evidence\r3\tuning\20260423_R3-04AC_TARGETED-R202.csv`, `C:\test\4X\production\evidence\r3\tuning\20260423_R3-04AC_TARGETED-R202-KPI.md`

### R203 - r201-stability-close-b

- KPI pass delta: `+1`
- Overall win rate delta: `+0.0250`
- Hard win rate delta: `+0.0267`
- Avg end turn delta: `-0.2820`
- Attrition delta: `-0.0130`
- Single-path monopoly delta: `-0.0065`
- Profile knobs: `bias=0.27/0.16/0.05`, `threshold=85/83/81`, `deltaScale=0.96`, `noise=2.02`, `shock=0.33x26`, `late=14|p0.094|c0.066|l0.107|cap0.036`
- Artifacts: `C:\test\4X\production\evidence\r3\tuning\20260423_R3-04AC_TARGETED-R203.csv`, `C:\test\4X\production\evidence\r3\tuning\20260423_R3-04AC_TARGETED-R203-KPI.md`

### R204 - r201-stability-close-c

- KPI pass delta: `+1`
- Overall win rate delta: `-0.0140`
- Hard win rate delta: `-0.0133`
- Avg end turn delta: `+0.3560`
- Attrition delta: `+0.0090`
- Single-path monopoly delta: `+0.0020`
- Profile knobs: `bias=0.27/0.16/0.05`, `threshold=85/83/81`, `deltaScale=0.96`, `noise=2.02`, `shock=0.35x28`, `late=14|p0.094|c0.066|l0.107|cap0.036`
- Artifacts: `C:\test\4X\production\evidence\r3\tuning\20260423_R3-04AC_TARGETED-R204.csv`, `C:\test\4X\production\evidence\r3\tuning\20260423_R3-04AC_TARGETED-R204-KPI.md`

## 3. Best Round Selection

- Selected best round: `R204 (r201-stability-close-c)`
- Reason: highest KPI pass count (`13/13`), then closest to standard win-rate / mid-turn pacing targets.

## 4. Remaining Gaps

- No KPI hard-gate failures in best round.
