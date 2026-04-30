# R3-04 Parameter Tuning Impact Report

- Stage: R3 Balance and AI Tuning
- Task: R3-04 Parameter tuning iterations (3 rounds)
- Date: 2026-04-20

## 1. KPI Gate Score

| Round | KPI Pass | Win(E/S/H) | AvgEndTurn | Attrition | PathMonopoly |
|---|---:|---:|---:|---:|---:|
| R3-02 Run001 baseline | 0/13 | 1.0000/0.9700/0.0000 | 3.4620 | 0.0040 | 0.9055 |
| R13 (easy-recover-balance) | 4/13 | 0.5467/0.2550/0.0667 | 13.9360 | 0.2660 | 1.0000 |
| R14 (duration-volatility) | 7/13 | 0.3400/0.0625/0.0233 | 14.5540 | 0.2450 | 1.0000 |
| R15 (overall-balance-candidate) | 5/13 | 0.6333/0.2425/0.0333 | 13.7840 | 0.2640 | 1.0000 |

## 2. Round-by-Round Delta

### R13 - easy-recover-balance

- KPI pass delta: `+4`
- Overall win rate delta: `-0.4020`
- Hard win rate delta: `+0.0667`
- Avg end turn delta: `+10.4740`
- Attrition delta: `+0.2620`
- Single-path monopoly delta: `+0.0945`
- Profile knobs: `bias=0.16/0.08/-0.03`, `threshold=90/88/83`, `deltaScale=0.96`, `noise=1.75`, `shock=0.30x24`
- Artifacts: `C:\test\4X\production\evidence\r3\tuning\20260420_R3-04C_TARGETED-R13.csv`, `C:\test\4X\production\evidence\r3\tuning\20260420_R3-04C_TARGETED-R13-KPI.md`

### R14 - duration-volatility

- KPI pass delta: `+3`
- Overall win rate delta: `-0.1520`
- Hard win rate delta: `-0.0433`
- Avg end turn delta: `+0.6180`
- Attrition delta: `-0.0210`
- Single-path monopoly delta: `0.0000`
- Profile knobs: `bias=0.14/0.06/-0.04`, `threshold=92/90/84`, `deltaScale=0.92`, `noise=1.85`, `shock=0.34x28`
- Artifacts: `C:\test\4X\production\evidence\r3\tuning\20260420_R3-04C_TARGETED-R14.csv`, `C:\test\4X\production\evidence\r3\tuning\20260420_R3-04C_TARGETED-R14-KPI.md`

### R15 - overall-balance-candidate

- KPI pass delta: `-2`
- Overall win rate delta: `+0.1630`
- Hard win rate delta: `+0.0100`
- Avg end turn delta: `-0.7700`
- Attrition delta: `+0.0190`
- Single-path monopoly delta: `0.0000`
- Profile knobs: `bias=0.18/0.09/-0.05`, `threshold=90/89/84`, `deltaScale=0.95`, `noise=1.90`, `shock=0.32x26`
- Artifacts: `C:\test\4X\production\evidence\r3\tuning\20260420_R3-04C_TARGETED-R15.csv`, `C:\test\4X\production\evidence\r3\tuning\20260420_R3-04C_TARGETED-R15-KPI.md`

## 3. Best Round Selection

- Selected best round: `R14 (duration-volatility)`
- Reason: highest KPI pass count (`7/13`), then closest to standard win-rate / mid-turn pacing targets.

## 4. Remaining Gaps

- Failed KPI IDs in best round: WIN_RATE_EASY, WIN_RATE_STANDARD, WIN_RATE_HARD, WIN_RATE_OVERALL, VICTORY_SHARE_OVERALL, SINGLE_PATH_VICTORY_MONOPOLY
