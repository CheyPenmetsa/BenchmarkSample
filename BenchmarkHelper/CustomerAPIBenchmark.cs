using BenchmarkDotNet.Attributes;

namespace BenchmarkHelper
{
    [HtmlExporter]
    public class CustomerAPIBenchmark
    {
        [Params(10, 20)]
        public int IterationCount;

        private readonly CustomerClient _customerClient = new CustomerClient();

        [Benchmark]
        public async Task TestRestAPIAsync()
        {
            for (int i = 0; i < IterationCount; i++)
            {
                await _customerClient.CreateAndVerifyCustomer();
            }
        }

    }
}
