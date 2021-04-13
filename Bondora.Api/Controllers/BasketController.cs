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
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository basketRepository;

        public BasketController(IBasketRepository basketRepository)
        {
            this.basketRepository = basketRepository;
        }

        [HttpPost]
        [Route("CheckoutCustomerBasket")]
        public async Task<IActionResult> CheckoutCustomerBasket(OrderVM order)
        {
            var result = await basketRepository.CheckoutCustomerBasket(order);
            return Ok(result);
        }
    }
}
