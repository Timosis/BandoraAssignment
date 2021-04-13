using Bandora.Models;
using Bondora.Web.ApiServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bondora.Web.Controllers
{
    public class BasketController : Controller
    {
        public IEquipmentApiService equipmentApiService;
        public IBasketApiService basketApiService;
        public ICustomerApiService customerApiService;
        private readonly IMemoryCache memoryCache;


        public BasketController(IEquipmentApiService equipmentApiService,
            IBasketApiService basketApiService,
            ICustomerApiService customerApiService,
            IMemoryCache memoryCache
            )
        {
            this.equipmentApiService = equipmentApiService;
            this.basketApiService = basketApiService;
            this.customerApiService = customerApiService;
            this.memoryCache = memoryCache;
        }

        public JsonResult AddEquipmentToBasket(BasketItemVM model)
        {
            ServiceResult AddEquipmentToBasketResult = new ServiceResult()
            {
                Type = ResultType.UnKnown,
                Message = "Something went wrong"
            };

            try
            {
                var result = CalculateRentPrice(model);
                var hasCustomerBasket = memoryCache.TryGetValue(model.CustomerId, out BasketVM customerBasket);                
                if (!hasCustomerBasket)
                {
                    customerBasket = new BasketVM()
                    {
                        CustomerId = model.CustomerId,
                        BasketItems = new List<BasketItemVM>()
                    };
                    memoryCache.Set(model.CustomerId, customerBasket);
                }

                customerBasket.BasketItems.Add(new BasketItemVM()
                {
                    CustomerId = model.CustomerId,
                    Day = model.Day,
                    Point = result.Point,
                    Price = result.Price,
                    Equipment = new EquipmentVM
                    {
                        Id = model.Equipment.Id,
                        Name = model.Equipment.Name,
                        Type = model.Equipment.Type
                    }
                });


                AddEquipmentToBasketResult.Type = ResultType.Success;
                AddEquipmentToBasketResult.Message = "Item Added To Basket Succesfully";
            }
            catch
            {

                AddEquipmentToBasketResult.Type = ResultType.Error;
                AddEquipmentToBasketResult.Message = "Error Occured While Adding Equiptment To Basket";
            }

            return Json(AddEquipmentToBasketResult);
        }

        public JsonResult RemoveItemFromBasket(RemoveEquipmentVM model)
        {
            ServiceResult removeItemFromBasketResult = new ServiceResult()
            {
                Type = ResultType.UnKnown,
                Message = "Something went wrong"
            };

            var hasCustomerBasket = memoryCache.TryGetValue(model.CustomerId, out BasketVM customerBasket);

            if (hasCustomerBasket)
            {
                customerBasket.BasketItems.RemoveAt(model.EquipmentIndex);
                removeItemFromBasketResult.Type = ResultType.Success;
                removeItemFromBasketResult.Message = "Equipment Removed Customer Basket Succesfully!";
            }

            return Json(removeItemFromBasketResult);
        }

        public async Task<ActionResult> CustomerBasket(int customerId)
        {
            ViewBag.CustomerId = customerId;
            ViewBag.EquipmentList = await equipmentApiService.GetEquipments();
            return PartialView("~/Views/Basket/_CustomerBasketPartial.cshtml");
        }

        public JsonResult GetCustomerBasket([FromBody] string customerId)
        {
            var hasCustomerBasket = memoryCache.TryGetValue(int.Parse(customerId), out BasketVM customerBasket);

            if (hasCustomerBasket)
            {
                return Json(customerBasket.BasketItems);
            }
            return Json(null);
        }

        public async Task<JsonResult> CheckoutBasket(int customerId)
        {
            ServiceResult checkOutBasketResult = new ServiceResult()
            {
                Type = ResultType.UnKnown,
                Message = "Something went wrong"
            };

            var hasCustomerBasket = memoryCache.TryGetValue(customerId, out BasketVM customerBasket);

            if (hasCustomerBasket)
            {
                OrderVM customerOrder = new OrderVM();
                customerOrder.OrderDetails = new List<OrderDetailVM>();
                customerBasket.BasketItems.ForEach(basketItem =>
                {
                    customerOrder.CustomerId = basketItem.CustomerId;
                    customerOrder.TotalPrice += basketItem.Price;
                    customerOrder.OrderTotalPoint += basketItem.Point;
                    customerOrder.OrderDetails.Add(new OrderDetailVM
                    {
                        Days = basketItem.Day,
                        EquipmentId = basketItem.Equipment.Id,
                        Points = basketItem.Point,
                        Price = basketItem.Price
                    });
                });
                checkOutBasketResult = await basketApiService.CheckoutCustomerBasket(customerOrder);

                if (checkOutBasketResult.Type == ResultType.Success)
                {
                    memoryCache.Remove(customerId);
                }
            }

            return Json(checkOutBasketResult);
        }

        public BasketItemVM CalculateRentPrice(BasketItemVM basketItem)
        {

            switch (basketItem.Equipment.Type)
            {
                case EquiptmentType.Heavy:
                    basketItem.Price = PriceOfRentals.OneTimeRentalFee + PriceOfRentals.PremiumDailyFee * basketItem.Day;
                    basketItem.Point = 2;
                    break;
                case EquiptmentType.Regular:
                    basketItem.Price = PriceOfRentals.OneTimeRentalFee +
                                            (basketItem.Day <= 2 ? PriceOfRentals.PremiumDailyFee * basketItem.Day :
                                             PriceOfRentals.PremiumDailyFee * 2 + PriceOfRentals.RegularDailyFee * (basketItem.Day - 2));
                    basketItem.Point = 1;
                    break;
                case EquiptmentType.Specialize:
                    basketItem.Price = basketItem.Day <= 3 ? PriceOfRentals.PremiumDailyFee * basketItem.Day : PriceOfRentals.PremiumDailyFee * 3 + PriceOfRentals.RegularDailyFee * (basketItem.Day - 3);
                    basketItem.Point = 1;
                    break;
                default:
                    break;
            }
            return basketItem;
        }
    }
}
