using BenchmarkDotNet.Running;
using RegexBenchmark;

var summaryRegex = BenchmarkRunner.Run<RegexBenchmarkSet>();
var summaryParallelRegex = BenchmarkRunner.Run<ParallelProcessingRegexBenchmarkSet>();
var summaryString = BenchmarkRunner.Run<StringMethodsBenchmarkSet>();