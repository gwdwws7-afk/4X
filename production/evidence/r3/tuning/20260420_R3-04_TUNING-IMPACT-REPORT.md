# R3-04 Parameter Tuning Impact Report

- Stage: R3 Balance and AI Tuning
- Task: R3-04 Parameter tuning iterations (3 rounds)
- Date: 2026-04-20

## 1. KPI Gate Score

| Round | KPI Pass | Win(E/S/H) | AvgEndTurn | Attrition | PathMonopoly |
|---|---:|---:|---:|---:|---:|
| R3-02 Run001 baseline | 0/13 | 1.0000/0.9700/0.0000 | 3.4620 | 0.0040 | 0.9055 |
| R01 (decelerate-window) | 4/13 | 0.0000/0.0000/0.0000 | 14.5310 | 0.1920 | 0.0000 |
| R02 (stability-mix) | 4/13 | 0.3467/0.0000/0.1933 | 13.9750 | 0.2410 | 0.7469 |
| R03 (target-seek) | 4/13 | 0.9233/0.3275/0.3100 | 9.7610 | 0.0970 | 0.9182 |

## 2. Round-by-Round Delta

### R01 - decelerate-window

- KPI pass delta: `+4`
- Overall win rate delta: `-0.6880`
- Hard win rate delta: `0.0000`
- Avg end turn delta: `+11.0690`
- Attrition delta: `+0.1880`
- Single-path monopoly delta: `-0.9055`
- Profile knobs: `bias=0.06/-0.02/-0.06`, `threshold=90/92/86`, `deltaScale=0.80`, `noise=1.40`, `shock=0.20x18`
- Artifacts: `C:\test\4X\production\evidence\r3\tuning\20260420_R3-04_TUNING-R01.csv`, `C:\test\4X\production\evidence\r3\tuning\20260420_R3-04_TUNING-R01-KPI.md`

### R02 - stability-mix

- KPI pass delta: `0`
- Overall win rate delta: `+0.1620`
- Hard win rate delta: `+0.1933`
- Avg end turn delta: `-0.5560`
- Attrition delta: `+0.0490`
- Single-path monopoly delta: `+0.7469`
- Profile knobs: `bias=0.10/0.00/-0.03`, `threshold=86/88/80`, `deltaScale=0.90`, `noise=1.30`, `shock=0.18x16`
- Artifacts: `C:\test\4X\production\evidence\r3\tuning\20260420_R3-04_TUNING-R02.csv`, `C:\test\4X\production\evidence\r3\tuning\20260420_R3-04_TUNING-R02-KPI.md`

### R03 - target-seek

- KPI pass delta: `0`
- Overall win rate delta: `+0.3390`
- Hard win rate delta: `+0.1167`
- Avg end turn delta: `-4.2140`
- Attrition delta: `-0.1440`
- Single-path monopoly delta: `+0.1713`
- Profile knobs: `bias=0.12/0.02/-0.01`, `threshold=84/85/78`, `deltaScale=0.95`, `noise=1.25`, `shock=0.15x14`
- Artifacts: `C:\test\4X\production\evidence\r3\tuning\20260420_R3-04_TUNING-R03.csv`, `C:\test\4X\production\evidence\r3\tuning\20260420_R3-04_TUNING-R03-KPI.md`

## 3. Best Round Selection

- Selected best round: `R03 (target-seek)`
- Reason: highest KPI pass count (`4/13`), then closest to standard win-rate / mid-turn pacing targets.

## 4. Remaining Gaps

- Failed KPI IDs in best round: WIN_RATE_EASY, WIN_RATE_STANDARD, AVG_END_TURN_OVERALL, AVG_END_TURN_STANDARD, ATTRITION_RATE_OVERALL, RESOURCE_CV_GOLDLEAF, RESOURCE_CV_FIREOIL, RESOURCE_CV_ARMS, SINGLE_PATH_VICTORY_MONOPOLY
