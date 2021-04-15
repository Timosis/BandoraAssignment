using Bandora.Models;
using Bondora.Web.ApiServices;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bondora.Web.Controllers
{
    public class EquipmentController : Controller
    {
        /// <summary>
        /// Equipment operations api service interface
        /// </summary>
        public IEquipmentApiService equipmentApiService;

        /// <summary>
        /// Constructor
        /// </summary>
        public EquipmentController(IEquipmentApiService equipmentApiClient)
        {
            this.equipmentApiService = equipmentApiClient;
        }

        /// <summary>
        /// Equipment management main page
        /// </summary>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Getting equipment list from db
        /// </summary>        
        public async Task<JsonResult> GetEquipments()
        {
            var result = await equipmentApiService.GetEquipments();
            return Json(result.Data);
        }

        /// <summary>
        /// Creating equipment view
        /// </summary>
        /// <returns>Partial view and set it general modal</returns>
        public IActionResult CreateEquipmentPartial()
        {
            return PartialView("~/Views/Equipment/_CreateEquipmentPartial.cshtml");
        }

        /// <summary>
        /// Creating equipment function
        /// </summary>        
        /// <returns>Service result</returns>
        public async Task<JsonResult> CreateEquipment(EquipmentVM equipment)
        {
            var result = await equipmentApiService.CreateEquipment(equipment);

            return Json(result);
        }
    }
}

