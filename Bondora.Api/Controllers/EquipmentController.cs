using Bandora.Models;
using Bondora.Api.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bondora.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentController : ControllerBase
    {
        private readonly IEquipmentRepository equipmentRepository;

        public EquipmentController(IEquipmentRepository equipmentRepository)
        {
            this.equipmentRepository = equipmentRepository;
        }

        [HttpPost]
        [Route("CreateEquipment")]
        public async Task<IActionResult> CreateEquipment(EquipmentVM vm)
        {
            var result = await equipmentRepository.CreateEquipment(vm);
            return Ok(result);
        }

        [HttpGet]
        [Route("GetEquipmentsList")]
        public async Task<IActionResult> GetEquipmentsList()
        {
            var result = await equipmentRepository.GetEquipmentsList();
            return Ok(result);
        }

        [HttpPost]
        [Route("GetEquipment")]
        public async Task<IActionResult> GetEquipment(int id)
        {
            var result = await equipmentRepository.GetEquipment(id);
            return Ok(result);
        }
    }
}
