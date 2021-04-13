using AutoMapper;
using Bandora.Models;
using Bondora.Api.Data;
using Bondora.Api.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bondora.Api.Repository
{
    public interface ICustomerRepository
    {
        Task<ServiceResult<List<CustomerVM>>> GetCustomersList();
        Task<ServiceResult<CustomerVM>> GetCustomer(int id);
        Task<ServiceResult> CreateCustomer(CustomerVM customer);
        Task<ServiceResult<List<OrderVM>>> GetCustomerOrdersList(int customerId);
        Task<ServiceResult<CustomerVM>> UpdateCustomer(CustomerVM customer);
        Task<ServiceResult<List<OrderDetailVM>>> GetCustomerOrderDetail(int customerId);
    }

    public class CustomerRepository : ICustomerRepository
    {
        private readonly IMapper mapper;
        private readonly BondoraDataContext context;

        public CustomerRepository(BondoraDataContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<ServiceResult> CreateCustomer(CustomerVM customer)
        {
            ServiceResult result = new ServiceResult()
            {
                Type = ResultType.UnKnown,
                Message = "Unknown Process"
            };

            var customerEntity = mapper.Map<CustomerVM, Customer>(customer);
            await context.Customers.AddAsync(customerEntity);
            int saveResult = await context.SaveChangesAsync();

            if (saveResult > 0)
            {
                result.Type = ResultType.Success;
                result.Message = "Customer Is Created Succesfully";
            }

            return result;
        }

        public async Task<ServiceResult<List<CustomerVM>>> GetCustomersList()
        {
            ServiceResult<List<CustomerVM>> result = new ServiceResult<List<CustomerVM>>
            {
                Type = ResultType.UnKnown,
                Message = "Unknown Process"
            };

            var customers = mapper.Map<List<Customer>, List<CustomerVM>>(await context.Customers.ToListAsync());

            if (customers != null)
            {
                result.Data = customers;
                result.Type = ResultType.Success;
                result.Message = "Customer List Got Succesfully";
            }
            else
            {
                result.Type = ResultType.Error;
                result.Message = "Something Went Wrong";
            }

            return result;
        }
       
        public async Task<ServiceResult<CustomerVM>> GetCustomer(int id)
        {
            ServiceResult<CustomerVM> result = new ServiceResult<CustomerVM>
            {
                Type = ResultType.UnKnown,
                Message = "Unknown Process"
            };
            var customer = mapper.Map<Customer, CustomerVM>(await context.Customers.FindAsync(id));
            if (customer != null)
            {
                result.Data = customer;
                result.Type = ResultType.Success;
                result.Message = "Customer Got Succesfully";
            }
            else
            {
                result.Type = ResultType.Error;
                result.Message = "Something Went Wrong";
            }

            return result;
        }

        public async Task<ServiceResult<List<OrderVM>>> GetCustomerOrdersList(int customerId)
        {
            ServiceResult<List<OrderVM>> result = new ServiceResult<List<OrderVM>>
            {
                Type = ResultType.UnKnown,
                Message = "Unknown Process"
            };

            var orders = mapper.Map<List<Order>, List<OrderVM>>(await context.Orders.Where(x=>x.CustomerId == customerId).ToListAsync());


            if (orders != null)
            {
                result.Data = orders;
                result.Type = ResultType.Success;
                result.Message = "Customer's Order Got Succesfully";
            }
            else
            {
                result.Type = ResultType.Error;
                result.Message = "Something Went Wrong";
            }
            return result;
        }

        public async Task<ServiceResult<List<OrderDetailVM>>> GetCustomerOrderDetail(int orderId)
        {
            ServiceResult<List<OrderDetailVM>> result = new ServiceResult<List<OrderDetailVM>>
            {
                Type = ResultType.UnKnown,
                Message = "Unknown Process"
            };

            var orderDetails = mapper.Map<List<OrderDetail>, List<OrderDetailVM>>(await context.OrderDetails.Include("Order").Include("Equipment")
                                                                                                .Where(x => x.OrderId == orderId).ToListAsync());

            if (orderDetails != null)
            {
                result.Data = orderDetails;
                result.Type = ResultType.Success;
                result.Message = "Order Detail Got Succesfully";
            }
            else
            {
                result.Type = ResultType.Error;
                result.Message = "Something Went Wrong";
            }
            return result;


        }

        public async Task<ServiceResult<CustomerVM>> UpdateCustomer(CustomerVM customer)
        {
            ServiceResult<CustomerVM> result = new ServiceResult<CustomerVM>
            {
                Type = ResultType.UnKnown,
                Message = "Unknown Process"
            };

            var customerResult = await context.Customers.FirstOrDefaultAsync(x => x.Id == customer.Id);

            if (customerResult != null)
            {
                customerResult.Points += customer.Points;

            }
            context.Customers.Update(customerResult);
            var updateResult =  await context.SaveChangesAsync();

            if (updateResult > 0)
            {
                result.Data = mapper.Map<Customer, CustomerVM>(customerResult);
                result.Type = ResultType.Success;
                result.Message = "Order Detail Got Succesfully";
            }
            else
            {
                result.Type = ResultType.Error;
                result.Message = "Something Went Wrong";
            }
            return result;            
        }
    }
}
