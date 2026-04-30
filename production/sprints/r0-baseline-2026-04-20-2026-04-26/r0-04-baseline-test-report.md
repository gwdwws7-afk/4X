# R0-04 Baseline Automated Regression Report (3 Stable Runs)

## Command
- `Unity.exe -batchmode -quit -projectPath C:\test\4X -executeMethod EventideAge.Editor.TestMenu.RunAllTests -logFile Temp/r0_baseline_run*.log`

## Run Evidence
| Run Log | Key Result Line | FAIL Hits | Compile Error Hits | Exit Code |
|---|---|---:|---:|---|
| `Temp/r0_baseline_run1.log` | `5668:=== Results: 288 passed, 0 failed ===` | 0 | 0 | 0 |
| `Temp/r0_baseline_run2.log` | `5650:=== Results: 288 passed, 0 failed ===` | 0 | 0 | 0 |
| `Temp/r0_baseline_run3.log` | `5650:=== Results: 288 passed, 0 failed ===` | 0 | 0 | 0 |

## Entry + Exit Anchors
- `Temp/r0_baseline_run1.log:307` (`=== EventideAge: Run All Tests ===`)
- `Temp/r0_baseline_run1.log:5872` (`return code 0`)
- `Temp/r0_baseline_run2.log:289` (`=== EventideAge: Run All Tests ===`)
- `Temp/r0_baseline_run2.log:5845` (`return code 0`)
- `Temp/r0_baseline_run3.log:289` (`=== EventideAge: Run All Tests ===`)
- `Temp/r0_baseline_run3.log:5845` (`return code 0`)

## Conclusion
- R0-04 acceptance met: 3 stable automated runs with identical green results.

