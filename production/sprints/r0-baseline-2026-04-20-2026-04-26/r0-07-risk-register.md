# R0-07 Risk Register (2026-04-16)

| Risk ID | Description | Probability | Impact | Owner | Mitigation | Trigger |
|---|---|---|---|---|---|---|
| R0-R1 | Public API drift after freeze | Medium | High | Tech Lead | API freeze checklist diff in every PR | changed public signature |
| R0-R2 | Missing cross-system replay coverage | Medium | High | QA Lead | build R1 replay pack for top critical chains | integration bug without replay |
| R0-R3 | Save/load edge-case inconsistency | Low | High | Persistence Owner | add save compatibility matrix in R1 | load mismatch in long session |
| R0-R4 | UI binding regression during prefab hardening | Medium | Medium | UI Lead | lock binding checklist + regression rerun | null-ref or stale data panel |
| R0-R5 | R3 balancing slips due late rule churn | Medium | Medium | Producer | reserve fixed balancing windows and freeze churn | KPI not converging mid-R3 |

## Immediate Actions
1. Add API freeze verification item to PR template.
2. Prepare replay-case skeleton before R1 day-1.
3. Define save schema version tag convention.

