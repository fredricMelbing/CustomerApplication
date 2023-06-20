using CustomerApplication.Domain.Entities;

namespace CustomerApplication.Infrastructure.Interfaces
{
    public interface ISalesRepo
    {
        public Task<List<Sales>> GetAllSales();
        public Task<Sales> GetSaleObject(string id);
        public Task<List<string>> GetAllSalesId();
        public Task Register(Sales sales);        
    }
}
