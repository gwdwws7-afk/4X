# R3-04 Parameter Tuning Impact Report

- Stage: R3 Balance and AI Tuning
- Task: R3-04 Parameter tuning iterations (3 rounds)
- Date: 2026-04-21

## 1. KPI Gate Score

| Round | KPI Pass | Win(E/S/H) | AvgEndTurn | Attrition | PathMonopoly |
|---|---:|---:|---:|---:|---:|
| R3-02 Run001 baseline | 0/13 | 1.0000/0.9700/0.0000 | 3.4620 | 0.0040 | 0.9055 |
| R22 (path-diversify-axis-push) | 9/13 | 0.3933/0.3350/0.6067 | 14.3010 | 0.2560 | 0.5991 |
| R23 (win-floor-recover) | 9/13 | 0.6067/0.4675/0.6467 | 13.8410 | 0.1940 | 0.6696 |
| R24 (hybrid-floor-vs-monopoly) | 9/13 | 0.4467/0.3400/0.6467 | 14.5890 | 0.2460 | 0.5991 |

## 2. Round-by-Round Delta

### R22 - path-diversify-axis-push

- KPI pass delta: `+9`
- Overall win rate delta: `-0.2540`
- Hard win rate delta: `+0.6067`
- Avg end turn delta: `+10.8390`
- Attrition delta: `+0.2520`
- Single-path monopoly delta: `-0.3064`
- Profile knobs: `bias=0.19/0.12/0.05`, `threshold=88/84/80`, `deltaScale=0.95`, `noise=2.05`, `shock=0.44x34`
- Artifacts: `C:\test\4X\production\evidence\r3\tuning\20260421_R3-04E_TARGETED-R22.csv`, `C:\test\4X\production\evidence\r3\tuning\20260421_R3-04E_TARGETED-R22-KPI.md`

### R23 - win-floor-recover

- KPI pass delta: `0`
- Overall win rate delta: `+0.1290`
- Hard win rate delta: `+0.0400`
- Avg end turn delta: `-0.4600`
- Attrition delta: `-0.0620`
- Single-path monopoly delta: `+0.0705`
- Profile knobs: `bias=0.22/0.14/0.07`, `threshold=87/83/79`, `deltaScale=0.98`, `noise=2.00`, `shock=0.40x32`
- Artifacts: `C:\test\4X\production\evidence\r3\tuning\20260421_R3-04E_TARGETED-R23.csv`, `C:\test\4X\production\evidence\r3\tuning\20260421_R3-04E_TARGETED-R23-KPI.md`

### R24 - hybrid-floor-vs-monopoly

- KPI pass delta: `0`
- Overall win rate delta: `-0.0990`
- Hard win rate delta: `0.0000`
- Avg end turn delta: `+0.7480`
- Attrition delta: `+0.0520`
- Single-path monopoly delta: `-0.0705`
- Profile knobs: `bias=0.21/0.13/0.06`, `threshold=88/84/80`, `deltaScale=0.96`, `noise=2.08`, `shock=0.45x35`
- Artifacts: `C:\test\4X\production\evidence\r3\tuning\20260421_R3-04E_TARGETED-R24.csv`, `C:\test\4X\production\evidence\r3\tuning\20260421_R3-04E_TARGETED-R24-KPI.md`

## 3. Best Round Selection

- Selected best round: `R23 (win-floor-recover)`
- Reason: highest KPI pass count (`9/13`), then closest to standard win-rate / mid-turn pacing targets.

## 4. Remaining Gaps

- Failed KPI IDs in best round: WIN_RATE_EASY, WIN_RATE_HARD, AVG_END_TURN_OVERALL, DEFEAT_SHARE_OVERALL
