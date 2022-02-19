using Shared;

namespace PostgreApp;

public class PostgreBenchmark : BenchmarkRunnerBase
{
    protected override DatabaseContext CreateContext()
    {
        return new PostgreDbContext();
    }
}