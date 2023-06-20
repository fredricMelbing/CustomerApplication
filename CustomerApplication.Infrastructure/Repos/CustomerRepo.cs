using CustomerApplication.Domain.Entities;
using CustomerApplication.Infrastructure.DbContexts;
using CustomerApplication.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CustomerApplication.Infrastructure.Repos
{
    public class CustomerRepo: ICustomerRepo
    {
        private readonly CustomerApplicationContext _db;
        public CustomerRepo(CustomerApplicationContext db)
        {
            _db = db;
        }
        public async Task <List<Customer>> GetAllCustomer()
        {            
            await _db.Sales.ToListAsync();
            var result = await _db.Customer.ToListAsync();

            return result;
        }
        public async Task<Customer> GetCustomer(string customerId)
        {
            await _db.Sales.ToListAsync();
            var result = await _db.Customer.SingleOrDefaultAsync(c => c.CustomerId.Equals(customerId));

            return result;
        }
        public async Task<List<Customer>> SearchCustomer(string customerName)
        {
            await _db.Sales.ToListAsync();
            var result = await _db.Customer.Where(c => c.FullName.Contains(customerName)).ToListAsync();

            return result;
        }        
        public async Task<List<string>> GetAllCustomerId()
        {
            var result = await _db.Customer.Select(s => s.CustomerId).ToListAsync();
            return result;
        }
        public async Task Register(Customer customer)
        {
            await _db.Customer.AddAsync(customer);
            await _db.SaveChangesAsync();
        }
        public async Task UpdateCustomer(Customer customer)
        {            
            _db.Customer.Update(customer);
            await _db.SaveChangesAsync();
        }
        public async Task DeleteCustomer(Customer customer)
        {
            _db.Customer.Remove(customer);
            await _db.SaveChangesAsync();
        }
    }
}
