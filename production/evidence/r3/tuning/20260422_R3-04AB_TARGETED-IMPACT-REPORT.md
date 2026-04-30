# R3-04 Parameter Tuning Impact Report

- Stage: R3 Balance and AI Tuning
- Task: R3-04 Parameter tuning iterations (3 rounds)
- Date: 2026-04-22

## 1. KPI Gate Score

| Round | KPI Pass | Win(E/S/H) | AvgEndTurn | Attrition | PathMonopoly |
|---|---:|---:|---:|---:|---:|
| R3-02 Run001 baseline | 0/13 | 1.0000/0.9700/0.0000 | 3.4620 | 0.0040 | 0.9055 |
| R199 (r198-hard-survival-a) | 13/13 | 0.7167/0.4500/0.2067 | 14.0380 | 0.1770 | 0.6258 |
| R200 (r198-hard-survival-b) | 13/13 | 0.7167/0.4500/0.2033 | 14.1460 | 0.1770 | 0.6338 |
| R201 (r198-hard-survival-c) | 13/13 | 0.7167/0.4500/0.2100 | 14.1950 | 0.1770 | 0.6332 |

## 2. Round-by-Round Delta

### R199 - r198-hard-survival-a

- KPI pass delta: `+13`
- Overall win rate delta: `-0.2310`
- Hard win rate delta: `+0.2067`
- Avg end turn delta: `+10.5760`
- Attrition delta: `+0.1730`
- Single-path monopoly delta: `-0.2797`
- Profile knobs: `bias=0.27/0.16/0.05`, `threshold=85/83/81`, `deltaScale=0.96`, `noise=2.02`, `shock=0.38x30`, `late=14|p0.094|c0.066|l0.107|cap0.036`
- Artifacts: `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04AB_TARGETED-R199.csv`, `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04AB_TARGETED-R199-KPI.md`

### R200 - r198-hard-survival-b

- KPI pass delta: `0`
- Overall win rate delta: `-0.0010`
- Hard win rate delta: `-0.0033`
- Avg end turn delta: `+0.1080`
- Attrition delta: `0.0000`
- Single-path monopoly delta: `+0.0080`
- Profile knobs: `bias=0.27/0.16/0.05`, `threshold=85/83/81`, `deltaScale=0.96`, `noise=2.02`, `shock=0.38x30`, `late=14|p0.094|c0.066|l0.107|cap0.036`
- Artifacts: `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04AB_TARGETED-R200.csv`, `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04AB_TARGETED-R200-KPI.md`

### R201 - r198-hard-survival-c

- KPI pass delta: `0`
- Overall win rate delta: `+0.0020`
- Hard win rate delta: `+0.0067`
- Avg end turn delta: `+0.0490`
- Attrition delta: `0.0000`
- Single-path monopoly delta: `-0.0006`
- Profile knobs: `bias=0.27/0.16/0.05`, `threshold=85/83/81`, `deltaScale=0.96`, `noise=2.02`, `shock=0.38x30`, `late=14|p0.094|c0.066|l0.107|cap0.036`
- Artifacts: `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04AB_TARGETED-R201.csv`, `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04AB_TARGETED-R201-KPI.md`

## 3. Best Round Selection

- Selected best round: `R201 (r198-hard-survival-c)`
- Reason: highest KPI pass count (`13/13`), then closest to standard win-rate / mid-turn pacing targets.

## 4. Remaining Gaps

- No KPI hard-gate failures in best round.
