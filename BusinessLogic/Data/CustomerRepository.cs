using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BusinessLogic.Data
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IMongoCollection<Customer> _customerCollection;

        public CustomerRepository(IOptions<MongoDbSettings> config)
        {
            var mongoClient = new MongoClient(config.Value.ConnectionString);
            var mongoDbName = mongoClient.GetDatabase(config.Value.DatabaseName);

            _customerCollection = mongoDbName.GetCollection<Customer>(config.Value.CollectionName);
        }

        public async Task CreateCustomerAsync(Customer customer)
        {
            var customerModel = await GetCustomerByIdAsync(customer.Id);

            if (customerModel == null)
            {
                await _customerCollection.InsertOneAsync(customer).ConfigureAwait(false);
            }            
        }

        public async Task<Customer> GetCustomerByIdAsync(string id)
        {
            return await _customerCollection.Find(x => x.Id == id).FirstOrDefaultAsync().ConfigureAwait(false);
        }

        public async Task<long> GetTotalDocumentsCountAsync()
        {
            // Get total count of all documents in the collection
            long totalDocuments = await _customerCollection.CountDocumentsAsync(FilterDefinition<Customer>.Empty);
            return totalDocuments;
        }
    }
}
