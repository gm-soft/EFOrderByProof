using BenchmarkDotNet.Running;

namespace SqlServerApp;

public class Program
{
    public static Task Main()
    {
        BenchmarkRunner.Run<SqlServerBenchmark>();
        return Task.CompletedTask;
    }

    private static async Task DebugAsync()
    {
        var benchmarker = new SqlServerBenchmark();
        await benchmarker.Setup();
        Console.WriteLine(await benchmarker.RunWithoutOrderingAsync());
        Console.WriteLine(await benchmarker.RunDefaultAsync());
        Console.WriteLine(await benchmarker.RunAsSplitAsync());
    }
}