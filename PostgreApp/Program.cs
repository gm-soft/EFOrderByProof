using BenchmarkDotNet.Running;
using Shared;

namespace PostgreApp;

public class PostgreBenchmark : Benchmarker<PostgreDatabaseDockerContainer>
{
}

public class Program
{
    public static void Main()
    {
        BenchmarkRunner.Run<PostgreBenchmark>();
    }

    private static async Task DebugAsync()
    {
        var benchmarker = new PostgreBenchmark();
        await benchmarker.Setup();
        Console.WriteLine(await benchmarker.RunWithoutOrderingAsync());
        Console.WriteLine(await benchmarker.RunDefaultAsync());
        Console.WriteLine(await benchmarker.RunAsSplitAsync());
        await benchmarker.CleanUp();
    }
}