using BenchmarkDotNet.Running;

namespace SqlServerApp;

public class Program
{
    public static void Main()
    {
        // BenchmarkRunner.Run<SqlServerBenchmark>();
        var benchmarker = new SqlServerBenchmark();
        benchmarker.Setup().Wait();
        var ordering = benchmarker.RunDefaultAsync().GetAwaiter().GetResult();
        Console.WriteLine(ordering);
    }
}