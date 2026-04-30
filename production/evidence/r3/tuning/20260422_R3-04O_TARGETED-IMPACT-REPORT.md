# R3-04 Parameter Tuning Impact Report

- Stage: R3 Balance and AI Tuning
- Task: R3-04 Parameter tuning iterations (3 rounds)
- Date: 2026-04-22

## 1. KPI Gate Score

| Round | KPI Pass | Win(E/S/H) | AvgEndTurn | Attrition | PathMonopoly |
|---|---:|---:|---:|---:|---:|
| R3-02 Run001 baseline | 0/13 | 1.0000/0.9700/0.0000 | 3.4620 | 0.0040 | 0.9055 |
| R160 (r157-defeat-share-recover-a) | 12/13 | 0.6833/0.4225/0.2567 | 14.1650 | 0.1970 | 0.6430 |
| R161 (r157-defeat-share-recover-b) | 12/13 | 0.6733/0.4125/0.2400 | 14.3410 | 0.2040 | 0.6492 |
| R162 (r157-defeat-share-recover-c) | 11/13 | 0.6733/0.4275/0.2833 | 14.0930 | 0.1970 | 0.6288 |

## 2. Round-by-Round Delta

### R160 - r157-defeat-share-recover-a

- KPI pass delta: `+12`
- Overall win rate delta: `-0.2370`
- Hard win rate delta: `+0.2567`
- Avg end turn delta: `+10.7030`
- Attrition delta: `+0.1930`
- Single-path monopoly delta: `-0.2625`
- Profile knobs: `bias=0.26/0.15/0.05`, `threshold=85/83/81`, `deltaScale=0.96`, `noise=2.02`, `shock=0.38x30`
- Artifacts: `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04O_TARGETED-R160.csv`, `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04O_TARGETED-R160-KPI.md`

### R161 - r157-defeat-share-recover-b

- KPI pass delta: `0`
- Overall win rate delta: `-0.0120`
- Hard win rate delta: `-0.0167`
- Avg end turn delta: `+0.1760`
- Attrition delta: `+0.0070`
- Single-path monopoly delta: `+0.0062`
- Profile knobs: `bias=0.26/0.15/0.05`, `threshold=85/83/81`, `deltaScale=0.96`, `noise=2.02`, `shock=0.38x30`
- Artifacts: `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04O_TARGETED-R161.csv`, `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04O_TARGETED-R161-KPI.md`

### R162 - r157-defeat-share-recover-c

- KPI pass delta: `-1`
- Overall win rate delta: `+0.0190`
- Hard win rate delta: `+0.0433`
- Avg end turn delta: `-0.2480`
- Attrition delta: `-0.0070`
- Single-path monopoly delta: `-0.0204`
- Profile knobs: `bias=0.26/0.15/0.05`, `threshold=85/83/81`, `deltaScale=0.96`, `noise=2.02`, `shock=0.38x30`
- Artifacts: `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04O_TARGETED-R162.csv`, `C:\test\4X\production\evidence\r3\tuning\20260422_R3-04O_TARGETED-R162-KPI.md`

## 3. Best Round Selection

- Selected best round: `R160 (r157-defeat-share-recover-a)`
- Reason: highest KPI pass count (`12/13`), then closest to standard win-rate / mid-turn pacing targets.

## 4. Remaining Gaps

- Failed KPI IDs in best round: WIN_RATE_STANDARD
