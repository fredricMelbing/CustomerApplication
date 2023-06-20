using AutoMapper;
using CustomerApplication.Core.Interfaces;
using CustomerApplication.Domain.DTO;
using CustomerApplication.Domain.Entities;
using CustomerApplication.Infrastructure.Interfaces;

namespace CustomerApplication.Core.Services
{
    public class SalesService: ISalesService
    {
        private readonly ISalesRepo _salesRepo;
        private readonly IMapper _mapper;
        public SalesService(ISalesRepo salesRepo, IMapper mapper)
        {
            _salesRepo = salesRepo;
            _mapper = mapper;
        }

        public async Task<List<SalesDTO>> GetAllSales()
        {
            var result = await _salesRepo.GetAllSales();
            return _mapper.Map<List<SalesDTO>>(result);
        }
        public async Task<Object> Register(SalesCreateDTO sales)
        {
            Sales newSales = new Sales();
            newSales = _mapper.Map<Sales>(sales);

            List<string> idTextList = await _salesRepo.GetAllSalesId();

            List<int> idList = idTextList
                    .Select(s => Int32.TryParse(s, out int id) ? id : (int?)null)
                    .Where(id => id.HasValue)
                    .Select(id => id.Value).ToList();

            int newId = Enumerable.Range(1, int.MaxValue)
                .Except(idList).FirstOrDefault();
            newSales.SalesId = newId.ToString();                     

            await _salesRepo.Register(newSales);
            return new {
                message = $"New Sales person {newSales.FullName} successfully registred",
                Id = $"{ newSales.SalesId}"
            };
        }
    }
}
