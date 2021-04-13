using Bandora.Models;
using Bondora.Api.Controllers;
using Bondora.Api.Repository;
using GenFu;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Bandora.Test
{
    public class BasketApiTest
    {
        [Fact]
        public async Task Checkout_Customer_Basket()
        {

            var service = new Mock<IBasketRepository>();

            var customerResult = new ServiceResult() { Type = ResultType.Success };
            var order = A.New<OrderVM>();
            order.OrderDetails = new List<OrderDetailVM>();
            var orderDetail = A.ListOf<OrderDetailVM>(5);

            orderDetail.ToList().ForEach(o => {
                order.OrderDetails.Add(o);
            });

            service.Setup(x => x.CheckoutCustomerBasket(order)).Returns(Task.FromResult(customerResult));

            var result = await new BasketController(service.Object).CheckoutCustomerBasket(order) as ObjectResult;
            var checkoutOrderResult = result.Value as ServiceResult;

            Assert.Equal(ResultType.Success, customerResult.Type);
        }
    }
}
