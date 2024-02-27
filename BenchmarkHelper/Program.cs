using BenchmarkDotNet.Running;
using BenchmarkHelper;

public class Program
{
    public static async Task Main(string[] args)
    {
        //Benchmarking C# classes
        //var summary = BenchmarkRunner.Run<StringUtilityHelperBenchmark>();

        //Benchmarking REST API
        var summary = BenchmarkRunner.Run<CustomerAPIBenchmark>();
        Console.WriteLine(summary);
    }
}