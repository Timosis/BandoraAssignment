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
    public class CustomerApiTest
    {
        private async Task<ServiceResult<List<CustomerVM>>> GetCustomerListFakeData()
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
        public async Task Get_Customer_List()
        {
            var service = new Mock<ICustomerRepository>();
            var customers = GetCustomerListFakeData();
            await Task.Run(() => service.Setup(x => x.GetCustomersList()).Returns(customers));

            var results = await new CustomerController(service.Object).GetCustomersList();            
            var okResult = results as ObjectResult;
            var customerList = okResult.Value as ServiceResult<List<CustomerVM>>;

            Assert.Equal(20, customerList.Data.Count());
        }

        [Fact]
        public async Task Get_Customer()
        {
            var service = new Mock<ICustomerRepository>();

            var customer = new ServiceResult<CustomerVM>();            
            var customerlist = await GetCustomerListFakeData();

            customer.Data = customerlist.Data.Find(x => x.Id == 10);                          

            service.Setup(x => x.GetCustomer(10)).Returns(Task.FromResult(customer));

            var result = await new CustomerController(service.Object).GetCustomer(10) as ObjectResult;
            var customerResult = result.Value as ServiceResult<CustomerVM>;

            Assert.Equal(10, customer.Data.Id);
        }

        [Fact]
        public async Task Create_Customer()
        {
            
            var service = new Mock<ICustomerRepository>();

            var customerResult = new ServiceResult() { Type = ResultType.Success };
            var customer = A.New<CustomerVM>();
            
            service.Setup(x => x.CreateCustomer(customer)).Returns(Task.FromResult(customerResult));

            var result = await new CustomerController(service.Object).CreateCustomer(customer) as ObjectResult;
            var createCustomerResult = result.Value as ServiceResult;

            Assert.Equal(ResultType.Success, customerResult.Type);
        }

        [Fact]
        public async Task Get_Customer_Order_List()
        {
            var service = new Mock<ICustomerRepository>();

            var orders = await GetOrderFakeData();

            service.Setup(x => x.GetCustomerOrdersList(10)).Returns(Task.FromResult(orders));

            var result = await new CustomerController(service.Object).GetCustomerOrdersList(10) as ObjectResult;
            var orderListResult = result.Value as ServiceResult<List<OrderVM>>;
            
            Assert.Equal(20, orderListResult.Data.Count);
        }

        [Fact]
        public async Task Get_Customer_Order_Detail_List()
        {
            var service = new Mock<ICustomerRepository>();

            var orderDetails = await GetOrderDetailFakeData();

            service.Setup(x => x.GetCustomerOrderDetail(5)).Returns(Task.FromResult(orderDetails));

            var result = await new CustomerController(service.Object).GetCustomerOrderDetail(5) as ObjectResult;
            var orderDetailResult = result.Value as ServiceResult<List<OrderDetailVM>>;
            Assert.Equal(20, orderDetails.Data.Count);
        }
    }
}
