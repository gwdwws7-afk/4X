# R0-03 Test Asset Map & Gap Table (2026-04-16)

## Test Asset Map
- Test entry menu: `Assets/Editor/TestMenu.cs` -> `EventideAge.Editor.TestMenu.RunAllTests`.
- Primary regression suite: `Assets/Scripts/Tests/StandaloneTest.cs`.
- Additional suites:
  - `Assets/Scripts/Tests/IntegrationTest.cs`
  - `Assets/Scripts/Tests/CoreMechanicsTest.cs`

## Coverage Snapshot
- `StandaloneTest.cs` currently contains `46` guardrail test methods (`private static void Test*`).
- Covered domains include A4/A5/J, B1/B4/B5, C1-C5, D1-D6, E/F1/G/H1/H2/H3/I1/K.

## Gap Table (R0)
| Area | Current State | Gap | R1 Action |
|---|---|---|---|
| Unit-level deterministic tests | Strong in Standalone suite | Some systems still rely on broad scenario assertions only | Add focused micro-fixtures for C2/B3/G |
| Integration replay pack | Partial | No standardized replay pack for top chains | Add replay scripts for 10 critical chains |
| Save compatibility matrix | Basic | No schema/version compatibility matrix | Add multi-version save/load matrix |
| Performance regression | Limited | No threshold-based automated perf gate | Define perf smoke gate before R5 |
| L1-L4 meta systems | Not implemented | No runtime/no tests | Keep out of R0 scope; track in roadmap |

