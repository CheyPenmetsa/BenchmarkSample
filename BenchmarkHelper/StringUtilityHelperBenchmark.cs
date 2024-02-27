using BenchmarkDotNet.Attributes;
using BusinessLogic;

namespace BenchmarkHelper
{
    public class StringUtilityHelperBenchmark
    {
        string csvContent = @"
            Name,Age,Country
            John,25,USA
            Alice,30,Canada
            Bob,22,UK
            Emily,28,Australia
        ";
        private static readonly StringUtilityHelper helper = new StringUtilityHelper();

        [Benchmark]
        public void GetLastRowBenchmarkPositive()
        {
            helper.GetLastRowFromCSV(csvContent);
        }

        [Benchmark]
        public void GetLastRowBenchmarkNegative()
        {
            helper.GetLastRowFromCSV(string.Empty);
        }
    }
}
