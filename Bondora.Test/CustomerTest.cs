using Bandora.Models;
using Bondora.Api.Controllers;
using Bondora.Api.Repository;
using GenFu;
using Microsoft.Build.Tasks.Deployment.Bootstrapper;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;
using Xunit;

namespace Bondora.Test
{

    public class CustomerTest
    {
        private async Task<ServiceResult<List<CustomerVM>>> GetCustomerFakeData()
        {
            ServiceResult<List<CustomerVM>> customerListResult = new ServiceResult<List<CustomerVM>>();
            var i = 1;
            var customers = A.ListOf<CustomerVM>(20);
            customerListResult.Data = customers;
            customers.ForEach(x => x.Id = i++);
            return await Task.Run(() => customerListResult);
        }

        private async Task<ServiceResult<List<OrderVM>>> GetOrderFakeData()
        {
            ServiceResult<List<OrderVM>> orderListResult = new ServiceResult<List<OrderVM>>();

            var i = 1;
            var orders = A.ListOf<OrderVM>(20);
            orders.ForEach(x => x.Id = i++);
            orderListResult.Data = orders;
            return await Task.Run(() => orderListResult);
        }

        private async Task<ServiceResult<List<OrderDetailVM>>> GetOrderDetailFakeData()
        {
            ServiceResult<List<OrderDetailVM>> orderDetailListResult = new ServiceResult<List<OrderDetailVM>>();

            var i = 1;
            var orderDetails = A.ListOf<OrderDetailVM>(20);
            orderDetails.ForEach(x => x.Id = i++);
            orderDetailListResult.Data = orderDetails;
            return await Task.Run(() => orderDetailListResult);
        }


        [Fact]
        public async Task Get_All_Customer()
        {
            var service = new Mock<ICustomerRepository>();
            var customers = GetCustomerFakeData();
            await Task.Run(() => service.Setup(x => x.GetCustomersList()).Returns(customers));

            var controller = new CustomerController(service.Object);
            var results = await controller.GetCustomersList();
            var count = results.ToString();
            Assert.NotNull(3, count);
        }

    }
}
