using BenchmarkDotNet.Running;
using Shared;

namespace SqlServerApp;

public class AzureBench : Benchmarker<AzureSqlServerDockerContainer>
{
}

public class Program
{
    public static void Main()
    {
        BenchmarkRunner.Run<AzureBench>();
    }
    
    private static async Task DebugAsync()
    {
        var benchmarker = new AzureBench();
        await benchmarker.Setup();
        Console.WriteLine(await benchmarker.RunWithoutOrderingAsync());
        Console.WriteLine(await benchmarker.RunDefaultAsync());
        Console.WriteLine(await benchmarker.RunAsSplitAsync());
        await benchmarker.CleanUp();
    }
}