using CustomerApplication.Domain.Entities;

namespace CustomerApplication.Infrastructure.Interfaces
{
    public interface ICustomerRepo
    {
        public Task<List<Customer>> GetAllCustomer();
        public Task<Customer> GetCustomer(string customerId);
        public Task<List<Customer>> SearchCustomer(string customerName);        
        public Task<List<string>> GetAllCustomerId();
        public Task Register(Customer customer);
        public Task UpdateCustomer(Customer customer);
        public Task DeleteCustomer(Customer customer);
    }
}
