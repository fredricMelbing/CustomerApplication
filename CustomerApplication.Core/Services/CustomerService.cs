using AutoMapper;
using CustomerApplication.Core.Interfaces;
using CustomerApplication.Domain.DTO;
using CustomerApplication.Domain.Entities;
using CustomerApplication.Infrastructure.Interfaces;

namespace CustomerApplication.Core.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepo _customerRepo;
        private readonly ISalesRepo _salesRepo;
        private readonly IMapper _mapper;
        public CustomerService(ICustomerRepo customerRepo, ISalesRepo salesRepo, IMapper mapper)
        {
            _customerRepo = customerRepo;
            _salesRepo = salesRepo;
            _mapper = mapper;
        }

        public async Task<List<CustomerDTO>> GetAllCustomer()
        {
            var result = _mapper.Map<List<CustomerDTO>>(await _customerRepo.GetAllCustomer());
            return result;
        }
        public async Task<List<CustomerDTO>> SearchCustomer(string customerName)
        {
            var result = _mapper.Map<List<CustomerDTO>>(await _customerRepo.SearchCustomer(customerName));
            return result;
        }
        public async Task<List<CustomerDTO>> SearchSales(int salesId)
        {
            var salesObject = await _salesRepo.GetSaleObject(salesId.ToString());
            if (salesObject is null)
                throw new Exception($"{salesId}: Sales contact not found!");

            var customer = await _customerRepo.GetAllCustomer();
            var sortedCustomer = customer.Where(c => c.Sales.SalesId.Equals(salesId.ToString())).ToList();
            
            var result = _mapper.Map<List<CustomerDTO>>(sortedCustomer);
            return result;
        }

        public async Task<Object> Register(CustomerCreateDTO customer)
        {            
            Customer newCustomer = new Customer();
            newCustomer = _mapper.Map<Customer>(customer);
                        
            var salesObject = await _salesRepo.GetSaleObject(customer.SalesId.ToString());
            if (salesObject is null)
                throw new Exception($"{customer.SalesId}: Sales contact not found!");

            newCustomer.Sales = salesObject;

            List<string> idTextList = await _customerRepo.GetAllCustomerId();

            List<int> idList = idTextList
                    .Select(s => Int32.TryParse(s, out int id) ? id : (int?)null)
                    .Where(id => id.HasValue)
                    .Select(id => id.Value).ToList();

            int newId = Enumerable.Range(1, int.MaxValue)
                .Except(idList).FirstOrDefault();
            newCustomer.CustomerId = newId.ToString();

            await _customerRepo.Register(newCustomer);
            return new
            {
                message = $"New Customer {newCustomer.FullName} successfully registred",
                Id = $"{newCustomer.CustomerId}"
            };
        }
        public async Task<Object> UpdateCustomer(CustomerUpdateDTO updateCustomer)
        {
        var customer = await _customerRepo.GetCustomer(updateCustomer.CustomerIdnumber.ToString());
            if (customer is null)
                throw new Exception($"CustomerId {updateCustomer.CustomerIdnumber} not found!");

            customer.FullName = updateCustomer.FullName ?? customer.FullName;
            customer.Title = updateCustomer.Title ?? customer.Title;
            customer.PhoneNumber = updateCustomer.PhoneNumber ?? customer.PhoneNumber;
            customer.Address = updateCustomer.Address ?? customer.Address;
            customer.Email = updateCustomer.Email ?? customer.Email;

            if (updateCustomer.SalesId is not null && !customer.Sales.SalesId.Equals(updateCustomer.SalesId.ToString()))
            {
                var sales = await _salesRepo.GetSaleObject(updateCustomer.SalesId.ToString());
                customer.Sales = sales ?? throw new Exception($"Invalid Sales Contact, SalesId {updateCustomer.SalesId} not found!");
            }            
            await _customerRepo.UpdateCustomer(customer);
            var result = _mapper.Map<CustomerDTO>(customer);
            return new {
                message = "Customer successfully updated",
                UpdatedCustomer = result
            };
        }
        public async Task<Object> DeleteCustomer(int customerId)
        {
            var result = await _customerRepo.GetCustomer(customerId.ToString());
            if (result is null)
                throw new Exception($"CustomerId: {customerId} not found!");
            await _customerRepo.DeleteCustomer(result);

            return new
            {
                message = $"Deleted Customer: {result.FullName} successfully!",
                Id = $"{result.CustomerId}"
            };
        }
    }
}
