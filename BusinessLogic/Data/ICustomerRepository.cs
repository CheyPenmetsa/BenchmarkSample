namespace BusinessLogic.Data
{
    public interface ICustomerRepository
    {
        Task CreateCustomerAsync(Customer customer);

        Task<Customer> GetCustomerByIdAsync(string id);

        Task<long> GetTotalDocumentsCountAsync();
    }
}
