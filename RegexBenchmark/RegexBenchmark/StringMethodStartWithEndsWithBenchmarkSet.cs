using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;

namespace RegexBenchmark
{
    [SimpleJob(RunStrategy.Monitoring, launchCount: 10, warmupCount: 0, iterationCount: 30)]
    public class StringMethodStartWithEndsWithBenchmarkSet
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
            var regex = SetupFunctions(regexSize);

            var data = new string[]
            {
                $"{Guid.NewGuid()} {Guid.NewGuid()}",
                $"{_firstPattern} {Guid.NewGuid()}",
                $"{Guid.NewGuid()} {Guid.NewGuid()}",
                $"{Guid.NewGuid()} {Guid.NewGuid()}",
                $"{Guid.NewGuid()} {_lastPattern}",
            }
            .Select(x => x.ToUpper());

            List<string> result = new();
            regex.ForEach(r =>
            {
                var matching = data.Select(x =>
                    {
                        if (r(x))
                        {
                            return x;
                        }
                        else
                        {
                            return string.Empty;
                        }
                    }
                ).ToList();
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

        private List<Func<string, bool>> SetupFunctions(int numberOfRules)
        {
            List<Func<string, bool>> result = new();
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

                result.Add(i % 2 == 0 ?
                    new Func<string, bool>(str => str.StartsWith($"{guid} ")) :
                    new Func<string, bool>(str => str.EndsWith($" {guid}"))
                );
            }
            return result;
        }
    }
}
