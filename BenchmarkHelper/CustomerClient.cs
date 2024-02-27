using Amazon.Runtime.Internal.Endpoints.StandardLibrary;
using BusinessLogic.Models;
using MongoDB.Bson.IO;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BenchmarkHelper
{
    public class CustomerClient
    {
        private readonly HttpClient _httpClient;

        public CustomerClient() 
        { 
            _httpClient = new HttpClient();
        }

        public async Task CreateAndVerifyCustomer()
        {
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var ticks = DateTime.Now.Ticks;
            var createCustomer = new CreateCustomerRequest()
            {
                FirstName = $"FN_{ticks}",
                LastName = $"LN_{ticks}",
                Address = $"{ticks} Main Street",
                Email = $"{ticks}@gmail.com",
            };
            HttpContent content = new StringContent(JsonSerializer.Serialize(createCustomer), Encoding.UTF8, "application/json");
            HttpResponseMessage createResponse = await _httpClient.PostAsync("http://localhost:5000/api/customer", content);
            string responseContent = string.Empty;
            int customerId = 1;
            if (createResponse.IsSuccessStatusCode)
            {
                responseContent = await createResponse.Content.ReadAsStringAsync();
                var trimmedResponse = TrimQuotes(responseContent);
                if (!int.TryParse(trimmedResponse, out customerId))
                {
                    throw new Exception("Failed to parse response as integer.");
                }
            }

            HttpResponseMessage response = await _httpClient.GetAsync($"http://localhost:5000/api/customer/{customerId}");

            if (response.IsSuccessStatusCode)
            {
                string getResponseContent = await response.Content.ReadAsStringAsync();
            }
        }

        public static string TrimQuotes(string input)
        {
            // Check if the input string starts and ends with double quotes
            if (input.StartsWith("\"") && input.EndsWith("\""))
            {
                // Remove the first and last character (which are the double quotes)
                return input.Substring(1, input.Length - 2);
            }
            else
            {
                // If the input does not have both starting and ending quotes,
                // return the input as it is.
                return input;
            }
        }
    }
}
