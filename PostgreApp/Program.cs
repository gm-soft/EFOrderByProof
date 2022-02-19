using BenchmarkDotNet.Running;

namespace PostgreApp;

public class Program
{
    public static Task Main()
    {
        BenchmarkRunner.Run<PostgreBenchmark>();
        return Task.CompletedTask;
    }

    private static async Task DebugAsync()
    {
        var benchmarker = new PostgreBenchmark();
        await benchmarker.Setup();
        Console.WriteLine(await benchmarker.RunWithoutOrderingAsync());
        Console.WriteLine(await benchmarker.RunDefaultAsync());
        Console.WriteLine(await benchmarker.RunAsSplitAsync());
    }
}