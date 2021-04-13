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
        public IEquipmentApiService equipmentApiService;

        public EquipmentController(IEquipmentApiService equipmentApiClient)
        {
            this.equipmentApiService = equipmentApiClient;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> GetEquipments()
        {
            var result = await equipmentApiService.GetEquipments();
            return Json(result.Data);
        }

        public IActionResult CreateEquipmentPartial()
        {
            return PartialView("~/Views/Equipment/_CreateEquipmentPartial.cshtml");
        }

        public async Task<JsonResult> CreateEquipment(EquipmentVM equipment)
        {
            var result = await equipmentApiService.CreateEquipment(equipment);

            return Json(result);
        }
    }
}

