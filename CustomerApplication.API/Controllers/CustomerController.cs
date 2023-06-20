using CustomerApplication.Core.Interfaces;
using CustomerApplication.Domain.DTO;
using Microsoft.AspNetCore.Mvc;

namespace CustomerApplication.API.Controllers
{
    [Route("[controller]/[Action]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _customerService.GetAllCustomer();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> SearchCustomer(string customerName)
        {
            try
            {             
                var result = await _customerService.SearchCustomer(customerName);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> SearchSales(int salesId)
        {
            try
            {
                var result = await _customerService.SearchSales(salesId);                
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Create(CustomerCreateDTO sales)
        {
            try
            {
                var result = await _customerService.Register(sales);                
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        public async Task<IActionResult> Update(CustomerUpdateDTO updateCustomer)
        {
            try
            {
                var result = await _customerService.UpdateCustomer(updateCustomer);                
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int customerId)
        {
            try
            {
                var result = await _customerService.DeleteCustomer(customerId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
