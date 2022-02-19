using BenchmarkDotNet.Running;
using Shared;

namespace PostgreApp;

public class Program
{
    public static Task Main()
    {
        // BenchmarkRunner.Run<Benchmarker<PostgreDatabaseDockerContainer>>();
        return DebugAsync();
    }

    private static async Task DebugAsync()
    {
        await using var benchmarker = new Benchmarker<PostgreDatabaseDockerContainer>();
        await benchmarker.Setup();
        Console.WriteLine(await benchmarker.RunWithoutOrderingAsync());
        Console.WriteLine(await benchmarker.RunDefaultAsync());
        Console.WriteLine(await benchmarker.RunAsSplitAsync());
    }
}