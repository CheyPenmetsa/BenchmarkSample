using BusinessLogic.Data;
using BusinessLogic.Models;
using Microsoft.AspNetCore.Mvc;

namespace CustomerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerDetailsAsync([FromRoute] string id)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(id);
            var customerResponse = new GetCustomerResponse()
            {
                Id = customer.Id,
                LastName = customer.LastName,
                FirstName = customer.FirstName,
                Address = customer.Address,
                Email = customer.Email,
            };
            return Ok(customerResponse);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomerAsync([FromBody] CreateCustomerRequest createCustomerRequest)
        {
            var totalCustomerCount = await _customerRepository.GetTotalDocumentsCountAsync();
            var customer = new Customer()
            {
                Id = (totalCustomerCount + 1).ToString(),
                FirstName = createCustomerRequest.FirstName,
                LastName = createCustomerRequest.LastName,
                Email = createCustomerRequest.Email,
                Address = createCustomerRequest.Address
            };
            await _customerRepository.CreateCustomerAsync(customer);
            return Ok(customer.Id);
        }
    }
}
