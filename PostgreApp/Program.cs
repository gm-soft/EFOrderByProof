using BenchmarkDotNet.Running;

namespace PostgreApp;

public class Program
{
    public static void Main()
    {
        // BenchmarkRunner.Run<PostgreBenchmark>();
        var benchmarker = new PostgreBenchmark();
        benchmarker.Setup().Wait();
        var ordering = benchmarker.RunWithoutOrderingAsync().GetAwaiter().GetResult();
        Console.WriteLine(ordering);
    }
}