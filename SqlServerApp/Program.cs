using BenchmarkDotNet.Running;
using Shared;

namespace SqlServerApp;

public class Program
{
    public static Task Main()
    {
        BenchmarkRunner.Run<Benchmarker<AzureSqlServerDockerContainer>>();
        return Task.CompletedTask;
    }

    private static async Task DebugAsync()
    {
        var benchmarker = new Benchmarker<AzureSqlServerDockerContainer>();
        await benchmarker.Setup();
        Console.WriteLine(await benchmarker.RunWithoutOrderingAsync());
        Console.WriteLine(await benchmarker.RunDefaultAsync());
        Console.WriteLine(await benchmarker.RunAsSplitAsync());
        await benchmarker.CleanUp();
    }
}