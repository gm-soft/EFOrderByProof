using BenchmarkDotNet.Running;

namespace PostgreApp;

public class Program
{
    public static async Task Main()
    {
        // BenchmarkRunner.Run<PostgreBenchmark>();
        await DebugAsync();
    }

    private static async Task DebugAsync()
    {
        var benchmarker = new PostgreBenchmark();
        await benchmarker.Setup();
        Console.WriteLine(await benchmarker.RunDefaultAsync());
        Console.WriteLine(await benchmarker.RunAsSplitAsync());
    }
}