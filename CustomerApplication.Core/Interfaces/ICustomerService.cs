using CustomerApplication.Domain.DTO;

namespace CustomerApplication.Core.Interfaces
{
    public interface ICustomerService
    {
        public Task<List<CustomerDTO>> GetAllCustomer();
        public Task<List<CustomerDTO>> SearchCustomer(string customerName);
        public Task<List<CustomerDTO>> SearchSales(int salesId);
        public Task<Object> Register(CustomerCreateDTO sales);
        public Task<Object> UpdateCustomer(CustomerUpdateDTO updateCustomer);
        public Task<Object> DeleteCustomer(int CustomerId);
    }
}
