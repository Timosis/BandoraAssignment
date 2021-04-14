using Bandora.Models;
using Bondora.Web.ApiServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Bondora.Web.Controllers
{
    public class CustomerController : Controller
    {
        /// <summary>
        /// Customer operations api service interface
        /// </summary>
        private readonly ICustomerApiService customerApiService;

        /// <summary>
        ///  Constructor
        /// </summary>
        public CustomerController(ICustomerApiService customerApiService)
        {
            this.customerApiService = customerApiService;            
        }

        /// <summary>
        ///  Customer main index
        /// </summary>
        /// <returns>View</returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        ///  Adding customer view
        /// </summary>
        /// <returns>Partial view</returns>
        public IActionResult CreateCustomerPartial()
        {
            return PartialView("~/Views/Customer/_CreateCustomerPartial.cshtml");
        }

        /// <summary>
        /// Getting customer list
        /// </summary>
        /// <returns>Service result data</returns>
        public async Task<JsonResult> GetCustomers()
        {
            var result = await customerApiService.GetCustomersList();
            return Json(result.Data);
        }

        /// <summary>
        /// Creating customer
        /// </summary>
        /// <param name="customer">Name, surname, email</param>
        /// <returns>Api response result</returns>
        public async Task<JsonResult> CreateCustomer(CustomerVM customer)
        {
            var result = await customerApiService.CreateCustomer(customer);
            return Json(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public async Task<IActionResult> RentEquipment(int customerId)
        {
            var result = await customerApiService.GetCustomer(customerId);
            return View(result.Data);
        }

        public async Task<IActionResult> GetCustomerOrdersList([FromBody]string customerId)
        {
            var result = await customerApiService.GetCustomerOrdersList(int.Parse(customerId));
            return Json(result.Data);
        }

        public IActionResult CustomerOrderDetail(int orderId)
        {
            ViewBag.OrderId = orderId;
            return View();
        }

        public async Task<IActionResult> GetCustomerOrderDetail([FromBody]string orderId)
        {
            var result = await customerApiService.GetCustomerOrderDetail(int.Parse(orderId));
            return Json(result.Data);
        }

    }
}
