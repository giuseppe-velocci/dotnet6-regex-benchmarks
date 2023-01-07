using BenchmarkDotNet.Running;
using RegexBenchmark;

var summaryRegex = BenchmarkRunner.Run<RegexBenchmarkSet>();
var summaryParallelRegex = BenchmarkRunner.Run<ParallelProcessingRegexBenchmarkSet>();
var summaryStringEndsWithStartsWith = BenchmarkRunner.Run<StringMethodStartWithEndsWithBenchmarkSet>();
var summaryStringContains = BenchmarkRunner.Run<StringMethodContainsBenchmarkSet>();