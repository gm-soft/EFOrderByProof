using Shared;

namespace SqlServerApp;

public class SqlServerBenchmark : BenchmarkRunnerBase
{
    protected override DatabaseContext CreateContext()
    {
        return new SqlServerDatabaseContext();
    }
}