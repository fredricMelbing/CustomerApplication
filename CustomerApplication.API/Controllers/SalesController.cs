using CustomerApplication.Core.Interfaces;
using CustomerApplication.Domain.DTO;
using Microsoft.AspNetCore.Mvc;

namespace CustomerApplication.API.Controllers
{
    [Route("[controller]/[Action]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly ISalesService _salesService;
        public SalesController(ISalesService salesService)
        {
            _salesService = salesService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = await _salesService.GetAllSales();
                return Ok(result);                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(SalesCreateDTO sales)
        {
            try
            {
                return Ok(await _salesService.Register(sales));                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
