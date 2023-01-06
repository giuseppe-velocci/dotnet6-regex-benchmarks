using BenchmarkDotNet.Running;
using RegexBenchmark;

var summaryRegex = BenchmarkRunner.Run<RegexBenchmarkSet>();
var summaryString = BenchmarkRunner.Run<StringMethodsBenchmarkSet>();