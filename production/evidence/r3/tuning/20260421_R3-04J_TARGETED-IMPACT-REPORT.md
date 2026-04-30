# R3-04 Parameter Tuning Impact Report

- Stage: R3 Balance and AI Tuning
- Task: R3-04 Parameter tuning iterations (3 rounds)
- Date: 2026-04-21

## 1. KPI Gate Score

| Round | KPI Pass | Win(E/S/H) | AvgEndTurn | Attrition | PathMonopoly |
|---|---:|---:|---:|---:|---:|
| R3-02 Run001 baseline | 0/13 | 1.0000/0.9700/0.0000 | 3.4620 | 0.0040 | 0.9055 |
| R109 (r99-stability-tune-a) | 11/13 | 0.6667/0.4300/0.2567 | 13.7710 | 0.1970 | 0.6704 |
| R110 (r99-stability-tune-b) | 11/13 | 0.6733/0.4225/0.2600 | 13.7300 | 0.1980 | 0.6659 |
| R111 (r99-stability-tune-c) | 10/13 | 0.6800/0.4275/0.2600 | 13.7900 | 0.1980 | 0.6667 |

## 2. Round-by-Round Delta

### R109 - r99-stability-tune-a

- KPI pass delta: `+11`
- Overall win rate delta: `-0.2390`
- Hard win rate delta: `+0.2567`
- Avg end turn delta: `+10.3090`
- Attrition delta: `+0.1930`
- Single-path monopoly delta: `-0.2351`
- Profile knobs: `bias=0.26/0.15/0.05`, `threshold=85/83/81`, `deltaScale=0.96`, `noise=2.02`, `shock=0.38x30`
- Artifacts: `C:\test\4X\production\evidence\r3\tuning\20260421_R3-04J_TARGETED-R109.csv`, `C:\test\4X\production\evidence\r3\tuning\20260421_R3-04J_TARGETED-R109-KPI.md`

### R110 - r99-stability-tune-b

- KPI pass delta: `0`
- Overall win rate delta: `0.0000`
- Hard win rate delta: `+0.0033`
- Avg end turn delta: `-0.0410`
- Attrition delta: `+0.0010`
- Single-path monopoly delta: `-0.0045`
- Profile knobs: `bias=0.26/0.15/0.05`, `threshold=85/83/81`, `deltaScale=0.96`, `noise=2.02`, `shock=0.38x30`
- Artifacts: `C:\test\4X\production\evidence\r3\tuning\20260421_R3-04J_TARGETED-R110.csv`, `C:\test\4X\production\evidence\r3\tuning\20260421_R3-04J_TARGETED-R110-KPI.md`

### R111 - r99-stability-tune-c

- KPI pass delta: `-1`
- Overall win rate delta: `+0.0040`
- Hard win rate delta: `0.0000`
- Avg end turn delta: `+0.0600`
- Attrition delta: `0.0000`
- Single-path monopoly delta: `+0.0007`
- Profile knobs: `bias=0.26/0.15/0.05`, `threshold=85/83/81`, `deltaScale=0.96`, `noise=2.02`, `shock=0.38x30`
- Artifacts: `C:\test\4X\production\evidence\r3\tuning\20260421_R3-04J_TARGETED-R111.csv`, `C:\test\4X\production\evidence\r3\tuning\20260421_R3-04J_TARGETED-R111-KPI.md`

## 3. Best Round Selection

- Selected best round: `R109 (r99-stability-tune-a)`
- Reason: highest KPI pass count (`11/13`), then closest to standard win-rate / mid-turn pacing targets.

## 4. Remaining Gaps

- Failed KPI IDs in best round: WIN_RATE_STANDARD, AVG_END_TURN_OVERALL
