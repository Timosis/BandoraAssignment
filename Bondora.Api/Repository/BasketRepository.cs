using AutoMapper;
using Bandora.Models;
using Bondora.Api.Data;
using Bondora.Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bondora.Api.Repository
{
    public interface IBasketRepository
    {
        Task<ServiceResult> CheckoutCustomerBasket(OrderVM order);
    }

    public class BasketRepository : IBasketRepository
    {
        private readonly IMapper mapper;
        private readonly BondoraDataContext context;

        public BasketRepository(IMapper mapper, BondoraDataContext context)
        {
            this.mapper = mapper;
            this.context = context;
        }

        public async Task<ServiceResult> CheckoutCustomerBasket(OrderVM order)
        {
            ServiceResult result = new ServiceResult()
            {
                Type = ResultType.UnKnown,
                Message = "Unknown Process"
            };

            var customer = await context.Customers.FindAsync(order.CustomerId);
            if (customer != null)
            {
                customer.Points += order.OrderTotalPoint;
                context.Customers.Update(customer);

                var orderEntity = mapper.Map<OrderVM, Order>(order);
                await context.Orders.AddRangeAsync(orderEntity);
                int saveResult = await context.SaveChangesAsync();
                if (saveResult > 0)
                {
                    result.Type = ResultType.Success;
                    result.Message = "Customer's Order Checkout Succesfully";
                }
            }
            else
            {
                result.Type = ResultType.Error;
                result.Message = "Customer Couldn't Find For Checkingout";
            }            
            return result;
        }
    }
}
