# R3-04 Parameter Tuning Impact Report

- Stage: R3 Balance and AI Tuning
- Task: R3-04 Parameter tuning iterations (3 rounds)
- Date: 2026-04-21

## 1. KPI Gate Score

| Round | KPI Pass | Win(E/S/H) | AvgEndTurn | Attrition | PathMonopoly |
|---|---:|---:|---:|---:|---:|
| R3-02 Run001 baseline | 0/13 | 1.0000/0.9700/0.0000 | 3.4620 | 0.0040 | 0.9055 |
| R25 (hard-clamp-easy-lift) | 7/13 | 0.6833/0.2900/0.0033 | 12.1380 | 0.1700 | 0.9565 |
| R26 (defeat-share-balance) | 6/13 | 0.6367/0.3700/0.0100 | 13.0780 | 0.2000 | 0.9240 |
| R27 (pace-floor-guard) | 6/13 | 0.4800/0.2950/0.0067 | 12.9200 | 0.2030 | 0.9621 |

## 2. Round-by-Round Delta

### R25 - hard-clamp-easy-lift

- KPI pass delta: `+7`
- Overall win rate delta: `-0.3660`
- Hard win rate delta: `+0.0033`
- Avg end turn delta: `+8.6760`
- Attrition delta: `+0.1660`
- Single-path monopoly delta: `+0.0510`
- Profile knobs: `bias=0.28/0.11/-0.10`, `threshold=86/84/85`, `deltaScale=0.95`, `noise=2.12`, `shock=0.45x36`
- Artifacts: `C:\test\4X\production\evidence\r3\tuning\20260421_R3-04F_TARGETED-R25.csv`, `C:\test\4X\production\evidence\r3\tuning\20260421_R3-04F_TARGETED-R25-KPI.md`

### R26 - defeat-share-balance

- KPI pass delta: `-1`
- Overall win rate delta: `+0.0200`
- Hard win rate delta: `+0.0067`
- Avg end turn delta: `+0.9400`
- Attrition delta: `+0.0300`
- Single-path monopoly delta: `-0.0325`
- Profile knobs: `bias=0.25/0.13/-0.06`, `threshold=86/83/84`, `deltaScale=0.97`, `noise=2.05`, `shock=0.42x34`
- Artifacts: `C:\test\4X\production\evidence\r3\tuning\20260421_R3-04F_TARGETED-R26.csv`, `C:\test\4X\production\evidence\r3\tuning\20260421_R3-04F_TARGETED-R26-KPI.md`

### R27 - pace-floor-guard

- KPI pass delta: `0`
- Overall win rate delta: `-0.0780`
- Hard win rate delta: `-0.0033`
- Avg end turn delta: `-0.1580`
- Attrition delta: `+0.0030`
- Single-path monopoly delta: `+0.0381`
- Profile knobs: `bias=0.24/0.12/-0.08`, `threshold=87/84/85`, `deltaScale=0.93`, `noise=2.15`, `shock=0.46x37`
- Artifacts: `C:\test\4X\production\evidence\r3\tuning\20260421_R3-04F_TARGETED-R27.csv`, `C:\test\4X\production\evidence\r3\tuning\20260421_R3-04F_TARGETED-R27-KPI.md`

## 3. Best Round Selection

- Selected best round: `R25 (hard-clamp-easy-lift)`
- Reason: highest KPI pass count (`7/13`), then closest to standard win-rate / mid-turn pacing targets.

## 4. Remaining Gaps

- Failed KPI IDs in best round: WIN_RATE_STANDARD, WIN_RATE_HARD, WIN_RATE_OVERALL, AVG_END_TURN_OVERALL, VICTORY_SHARE_OVERALL, SINGLE_PATH_VICTORY_MONOPOLY
