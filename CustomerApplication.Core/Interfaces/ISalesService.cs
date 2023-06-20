using CustomerApplication.Domain.DTO;

namespace CustomerApplication.Core.Interfaces
{
    public interface ISalesService
    {
        public Task<List<SalesDTO>> GetAllSales();
        public Task<Object> Register(SalesCreateDTO sales);
    }
}
