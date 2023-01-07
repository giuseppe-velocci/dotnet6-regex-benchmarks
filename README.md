# dotnet6-regex-benchmarks
A benchmark on regex performances in Net 6 with distinct storage of positive matches. These results should only be considered as a reference for the time-wise order of magnitude needed to fulfill processing of all checks. 

# How to run
This solution leverages [BenchmarkDotnet](https://benchmarkdotnet.org/).
From a command line window pointing at project directory, input

`dotnet run -c Release`

## Regex performances in Net6

Created 100, 1000, 10000 different regex based on random GUIDs staring or ending with a whitespace and checked on 5 different strings, 2 with a matching pattern, the others not. Also the strings were generated via GUIDs.

Checks has been run synchronously.

| Method  | Mean | Error | StdDev | Median |
| --- | --- | --- | --- | --- |
| Match100Rules | 10.94 ms | 0.685 ms | 3.573 ms | 10.05 ms |
| Match1_000Rules | 96.93 ms | 1.955 ms | 10.189 ms | 95.65 ms |
| Match10_000Rules | 960.75 ms | 8.413 ms | 43.845 ms | 957.94 ms |


## Parallel regex processing performances in Net6

Created 100, 1000, 10000 different regex based on random GUIDs staring or ending with a whitespace and checked on 5 different strings, 2 with a matching pattern, the others not. Also the strings were generated via GUIDs. Processing was run with a degree of parallelism of 4.

Checks has been run synchronously.

| Method  | Mean | Error | StdDev | Median |
| --- | --- | --- | --- | --- |
| Match100Rules | 4.081 ms | 0.7160 ms | 3.731 ms | 3.218 ms |
| Match1_000Rules | 29.647 ms | 1.0112 ms | 5.270 ms | 28.523 ms |
| Match10_000Rules | 300.837 ms | 3.0728 ms | 16.014 ms | 297.481 ms |


## String comparison using StartsWith(), EndsWith() performances in Net6

Created 100, 1000, 10000 different methods (string.StartsWith() vs string.EndsWith()) based on random GUIDs staring or ending with a whitespace and checked on 5 different strings, 2 with a matching pattern, the others not. Also the strings were generated via GUIDs.

Checks has been run synchronously.

| Method  | Mean | Error | StdDev | Median |
| --- | --- | --- | --- | --- |
| Match100Rules | 1.273 ms | 0.5415 ms | 2.822 ms | 0.7223 ms |
| Match1_000Rules | 7.372 ms | 0.5921 ms | 3.086 ms | 6.6761 ms |
| Match10_000Rules | 66.046 ms | 1.0017 ms | 5.220 ms | 64.6812 ms |


## String comparison using Contains() performances in Net6

Created 100, 1000, 10000 methods (string.Contains()) based on random GUIDs staring or ending with a whitespace and checked on 5 different strings, 2 with a matching pattern, the others not. Also the strings were generated via GUIDs.

Checks has been run synchronously.

| Method  | Mean | Error | StdDev | Median |
| --- | --- | --- | --- | --- |
| Match100Rules | 1.128 ms | 0.5462 ms | 2.847 ms | 0.5864 ms |
| Match1_000Rules | 6.136 ms | 0.5817 ms | 3.032 ms | 5.5049 ms |
| Match10_000Rules | 55.208 ms | 0.9882 ms | 5.150 ms | 53.4980 ms |