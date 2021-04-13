using Bandora.Models;
using Bondora.Web.ApiServices;
using Bondora.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Bandora.Test
{
    public class CalculatePriceTest
    {

        [Fact]
        public void Calculate_Heavy_Equipment_Rent_Price()
        {
            var equipmentService = new Mock<IEquipmentApiService>();
            var basketService = new Mock<IBasketApiService>();
            var customerService = new Mock<ICustomerApiService>();
            var cacheMemory = new Mock<IMemoryCache>();

            BasketItemVM heavyItem = new BasketItemVM() { 
                CustomerId = 1,
                Day = 10,
                Equipment = new EquipmentVM()
                {
                    Id = 1,
                    Name = "Caterpillar Bulldozer",
                    Type = EquiptmentType.Heavy
                }                           
            };
            

            var result =  new BasketController(equipmentService.Object,basketService.Object,customerService.Object, cacheMemory.Object)
                                        .CalculateRentPrice(heavyItem);
            
            Assert.Equal(700, result.Price);
            Assert.Equal(2, result.Point);
        }


        [Fact]
        public void Calculate_Regular_Equipment_Rent_Price()
        {
            var equipmentService = new Mock<IEquipmentApiService>();
            var basketService = new Mock<IBasketApiService>();
            var customerService = new Mock<ICustomerApiService>();
            var cacheMemory = new Mock<IMemoryCache>();

            BasketItemVM heavyItem = new BasketItemVM()
            {
                CustomerId = 1,
                Day = 10,
                Equipment = new EquipmentVM()
                {
                    Id = 1,
                    Name = "Kamaz Truck",
                    Type = EquiptmentType.Regular
                }
            };


            var result = new BasketController(equipmentService.Object, basketService.Object, customerService.Object, cacheMemory.Object)
                                        .CalculateRentPrice(heavyItem);

            Assert.Equal(540, result.Price);
            Assert.Equal(1, result.Point);
        }

        [Fact]
        public void Calculate_Specialized_Equipment_Rent_Price()
        {
            var equipmentService = new Mock<IEquipmentApiService>();
            var basketService = new Mock<IBasketApiService>();
            var customerService = new Mock<ICustomerApiService>();
            var cacheMemory = new Mock<IMemoryCache>();

            BasketItemVM heavyItem = new BasketItemVM()
            {
                CustomerId = 1,
                Day = 10,
                Equipment = new EquipmentVM()
                {
                    Id = 1,
                    Name = "Bosch Jackhammer",
                    Type = EquiptmentType.Specialize
                }
            };


            var result = new BasketController(equipmentService.Object, basketService.Object, customerService.Object, cacheMemory.Object)
                                        .CalculateRentPrice(heavyItem);

            Assert.Equal(460, result.Price);
            Assert.Equal(1, result.Point);
        }

    }
}
