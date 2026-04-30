# R3-04 Parameter Tuning Impact Report

- Stage: R3 Balance and AI Tuning
- Task: R3-04 Parameter tuning iterations (3 rounds)
- Date: 2026-04-21

## 1. KPI Gate Score

| Round | KPI Pass | Win(E/S/H) | AvgEndTurn | Attrition | PathMonopoly |
|---|---:|---:|---:|---:|---:|
| R3-02 Run001 baseline | 0/13 | 1.0000/0.9700/0.0000 | 3.4620 | 0.0040 | 0.9055 |
| R100 (r99-close-gaps-a) | 9/13 | 0.6433/0.4225/0.1433 | 14.0680 | 0.1990 | 0.8025 |
| R101 (r99-close-gaps-b) | 9/13 | 0.7000/0.4200/0.1300 | 13.7380 | 0.1890 | 0.7938 |
| R102 (r99-close-gaps-c) | 10/13 | 0.6933/0.4025/0.2033 | 13.7430 | 0.1900 | 0.7326 |

## 2. Round-by-Round Delta

### R100 - r99-close-gaps-a

- KPI pass delta: `+9`
- Overall win rate delta: `-0.2830`
- Hard win rate delta: `+0.1433`
- Avg end turn delta: `+10.6060`
- Attrition delta: `+0.1950`
- Single-path monopoly delta: `-0.1030`
- Profile knobs: `bias=0.26/0.15/0.05`, `threshold=86/84/82`, `deltaScale=0.96`, `noise=2.03`, `shock=0.39x31`
- Artifacts: `C:\test\4X\production\evidence\r3\tuning\20260421_R3-04I_TARGETED-R100.csv`, `C:\test\4X\production\evidence\r3\tuning\20260421_R3-04I_TARGETED-R100-KPI.md`

### R101 - r99-close-gaps-b

- KPI pass delta: `0`
- Overall win rate delta: `+0.0120`
- Hard win rate delta: `-0.0133`
- Avg end turn delta: `-0.3300`
- Attrition delta: `-0.0100`
- Single-path monopoly delta: `-0.0087`
- Profile knobs: `bias=0.26/0.15/0.05`, `threshold=85/84/82`, `deltaScale=0.96`, `noise=2.02`, `shock=0.39x31`
- Artifacts: `C:\test\4X\production\evidence\r3\tuning\20260421_R3-04I_TARGETED-R101.csv`, `C:\test\4X\production\evidence\r3\tuning\20260421_R3-04I_TARGETED-R101-KPI.md`

### R102 - r99-close-gaps-c

- KPI pass delta: `+1`
- Overall win rate delta: `+0.0130`
- Hard win rate delta: `+0.0733`
- Avg end turn delta: `+0.0050`
- Attrition delta: `+0.0010`
- Single-path monopoly delta: `-0.0612`
- Profile knobs: `bias=0.26/0.15/0.05`, `threshold=85/84/81`, `deltaScale=0.96`, `noise=2.02`, `shock=0.38x30`
- Artifacts: `C:\test\4X\production\evidence\r3\tuning\20260421_R3-04I_TARGETED-R102.csv`, `C:\test\4X\production\evidence\r3\tuning\20260421_R3-04I_TARGETED-R102-KPI.md`

## 3. Best Round Selection

- Selected best round: `R102 (r99-close-gaps-c)`
- Reason: highest KPI pass count (`10/13`), then closest to standard win-rate / mid-turn pacing targets.

## 4. Remaining Gaps

- Failed KPI IDs in best round: WIN_RATE_STANDARD, AVG_END_TURN_OVERALL, SINGLE_PATH_VICTORY_MONOPOLY
