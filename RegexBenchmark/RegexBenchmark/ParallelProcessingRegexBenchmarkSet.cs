using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using System.Collections.Concurrent;
using System.Text.RegularExpressions;

namespace RegexBenchmark
{
    [SimpleJob(RunStrategy.Monitoring, launchCount: 10, warmupCount: 0, iterationCount: 30)]
    public class ParallelProcessingRegexBenchmarkSet
    {
        private string _firstPattern = "";
        private string _lastPattern = "";

        [Benchmark]
        public string[] Match100Rules() => MatchTest(1_00).ToArray();

        [Benchmark]
        public string[] Match1_000Rules() => MatchTest(1_000).ToArray();

        [Benchmark]
        public string[] Match10_000Rules() => MatchTest(10_000).ToArray();

        private IEnumerable<string> MatchTest(int regexSize)
        {
            var regex = SetupRegex(regexSize);

            var data = new string[]
            {
                $"{Guid.NewGuid()} {Guid.NewGuid()}",
                $"{_firstPattern} {Guid.NewGuid()}",
                $"{Guid.NewGuid()} {Guid.NewGuid()}",
                $"{Guid.NewGuid()} {Guid.NewGuid()}",
                $"{Guid.NewGuid()} {_lastPattern}",
            }
            .Select(x => x.ToUpper());

            ConcurrentBag<string> result = new();
            Parallel.ForEach(regex, new ParallelOptions() { MaxDegreeOfParallelism = 4 }, r =>
            {
                var matching = data.Select(x =>
                    {
                        if (r.IsMatch(x))
                        {
                            return x;
                        }
                        else
                        {
                            return string.Empty;
                        }
                    }
                )
                .ToList();
                matching.ForEach(m =>
                {
                    if (!result.Any(x => x == m))
                    {
                        result.Add(m);
                    }
                });
            });

            return result.Where(x => !string.IsNullOrWhiteSpace(x));
        }

        private List<Regex> SetupRegex(int numberOfRules)
        {
            List<Regex> result = new();
            for (int i = 0; i < numberOfRules; i++)
            {
                var guid = Guid.NewGuid().ToString().ToUpper();

                if (i == 0)
                {
                    _firstPattern = guid;
                }
                else if (i == numberOfRules - 1)
                {
                    _lastPattern = guid;
                }

                result.Add(new Regex(i % 2 == 0 ? $"{guid} .*" : $".* {guid}"));
            }
            return result;
        }
    }
}
