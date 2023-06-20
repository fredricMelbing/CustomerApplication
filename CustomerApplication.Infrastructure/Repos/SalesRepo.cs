using CustomerApplication.Domain.Entities;
using CustomerApplication.Infrastructure.DbContexts;
using CustomerApplication.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CustomerApplication.Infrastructure.Repos
{
    public class SalesRepo: ISalesRepo
    {
        private readonly CustomerApplicationContext _db;
        public SalesRepo(CustomerApplicationContext db)
        {
            _db = db;
        }

        public async Task<List<Sales>> GetAllSales()
        {
            var result = await _db.Sales.ToListAsync();
            return result;
        }
        public async Task<Sales> GetSaleObject(string id)
        {
            var result = await _db.Sales
                .SingleOrDefaultAsync(s => s.SalesId.Equals(id));
            return result;
        }
        public async Task<List<string>> GetAllSalesId()
        {
            var result = await _db.Sales.Select(s => s.SalesId).ToListAsync();
            return result;
        }
        public async Task Register(Sales sales)
        {
            await _db.Sales.AddAsync(sales);
            await _db.SaveChangesAsync();
        }
    }
}
