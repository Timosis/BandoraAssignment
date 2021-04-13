using Bandora.Models;
using Bondora.Api.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bondora.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository customerRepository;

        public CustomerController(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        [HttpPost]
        [Route("CreateCustomer")]
        public async Task<IActionResult> CreateCustomer(CustomerVM vm)
        {
            var result = await customerRepository.CreateCustomer(vm);
            return Ok(result);
        }

        [HttpGet]
        [Route("GetCustomersList")]
        public async Task<IActionResult> GetCustomersList()
        {
            var result = await customerRepository.GetCustomersList();
            return Ok(result);
        }

        [HttpGet("GetCustomer/{id:int}")]
        public async Task<IActionResult> GetCustomer(int id)
        {
            var result = await customerRepository.GetCustomer(id);
            return Ok(result);
        }

        [HttpGet("GetCustomerOrdersList/{customerId:int}")]
        public async Task<IActionResult> GetCustomerOrdersList(int customerId)
        {
            var result = await customerRepository.GetCustomerOrdersList(customerId);
            return Ok(result);
        }

        [HttpGet("GetCustomerOrderDetail/{orderId:int}")]
        public async Task<IActionResult> GetCustomerOrderDetail(int orderId)
        {
            var result = await customerRepository.GetCustomerOrderDetail(orderId);
            return Ok(result);
        }
    }
}
