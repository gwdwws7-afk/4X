# R3-04 Parameter Tuning Impact Report

- Stage: R3 Balance and AI Tuning
- Task: R3-04 Parameter tuning iterations (3 rounds)
- Date: 2026-04-20

## 1. KPI Gate Score

| Round | KPI Pass | Win(E/S/H) | AvgEndTurn | Attrition | PathMonopoly |
|---|---:|---:|---:|---:|---:|
| R3-02 Run001 baseline | 0/13 | 1.0000/0.9700/0.0000 | 3.4620 | 0.0040 | 0.9055 |
| R10 (standard-revival-a) | 4/13 | 0.2667/0.6475/0.3267 | 13.5620 | 0.2680 | 0.8787 |
| R11 (standard-revival-b) | 4/13 | 0.1833/0.7375/0.4100 | 12.9920 | 0.2370 | 0.8753 |
| R12 (winrate-recover-diversify) | 6/13 | 0.1767/0.5000/0.4133 | 14.3930 | 0.2430 | 0.8249 |

## 2. Round-by-Round Delta

### R10 - standard-revival-a

- KPI pass delta: `+4`
- Overall win rate delta: `-0.2510`
- Hard win rate delta: `+0.3267`
- Avg end turn delta: `+10.1000`
- Attrition delta: `+0.2640`
- Single-path monopoly delta: `-0.0268`
- Profile knobs: `bias=0.06/0.09/0.00`, `threshold=94/84/79`, `deltaScale=0.96`, `noise=1.35`, `shock=0.18x16`
- Artifacts: `C:\test\4X\production\evidence\r3\tuning\20260420_R3-04B_TARGETED-R10.csv`, `C:\test\4X\production\evidence\r3\tuning\20260420_R3-04B_TARGETED-R10-KPI.md`

### R11 - standard-revival-b

- KPI pass delta: `0`
- Overall win rate delta: `+0.0360`
- Hard win rate delta: `+0.0833`
- Avg end turn delta: `-0.5700`
- Attrition delta: `-0.0310`
- Single-path monopoly delta: `-0.0035`
- Profile knobs: `bias=0.04/0.12/0.01`, `threshold=92/83/78`, `deltaScale=0.98`, `noise=1.45`, `shock=0.20x18`
- Artifacts: `C:\test\4X\production\evidence\r3\tuning\20260420_R3-04B_TARGETED-R11.csv`, `C:\test\4X\production\evidence\r3\tuning\20260420_R3-04B_TARGETED-R11-KPI.md`

### R12 - winrate-recover-diversify

- KPI pass delta: `+2`
- Overall win rate delta: `-0.0960`
- Hard win rate delta: `+0.0033`
- Avg end turn delta: `+1.4010`
- Attrition delta: `+0.0060`
- Single-path monopoly delta: `-0.0503`
- Profile knobs: `bias=0.05/0.10/0.02`, `threshold=91/84/79`, `deltaScale=0.97`, `noise=1.55`, `shock=0.22x20`
- Artifacts: `C:\test\4X\production\evidence\r3\tuning\20260420_R3-04B_TARGETED-R12.csv`, `C:\test\4X\production\evidence\r3\tuning\20260420_R3-04B_TARGETED-R12-KPI.md`

## 3. Best Round Selection

- Selected best round: `R12 (winrate-recover-diversify)`
- Reason: highest KPI pass count (`6/13`), then closest to standard win-rate / mid-turn pacing targets.

## 4. Remaining Gaps

- Failed KPI IDs in best round: WIN_RATE_EASY, WIN_RATE_HARD, WIN_RATE_OVERALL, AVG_END_TURN_STANDARD, RESOURCE_CV_FIREOIL, RESOURCE_CV_ARMS, SINGLE_PATH_VICTORY_MONOPOLY
