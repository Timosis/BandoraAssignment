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
    public class EquipmentApiTest
    {
        private async Task<ServiceResult<List<EquipmentVM>>> GetEquipmentFakeData()
        {
            ServiceResult<List<EquipmentVM>> equipmentListResult = new ServiceResult<List<EquipmentVM>>();

            var i = 1;
            var equipments = A.ListOf<EquipmentVM>(20);
            equipments.ForEach(x => x.Id = i++);

            equipmentListResult.Data = equipments;

            return await Task.Run(() => equipmentListResult);
        }

        [Fact]
        public async Task Get_All_Equipments()
        {
            var service = new Mock<IEquipmentRepository>();

            var equipments = GetEquipmentFakeData();

            await Task.Run(() => service.Setup(x => x.GetEquipmentsList()).Returns(equipments));

            var result = await new EquipmentController(service.Object).GetEquipmentsList() as ObjectResult;
            var equipmentListResult = result.Value as ServiceResult<List<EquipmentVM>>;

            Assert.Equal(20, equipmentListResult.Data.Count);
        }

        [Fact]
        public async Task Create_Equipment()
        {
            var service = new Mock<IEquipmentRepository>();

            var equipmentResult = new ServiceResult() { Type = ResultType.Success };


            var equipment = A.New<EquipmentVM>();

            service.Setup(x => x.CreateEquipment(equipment)).Returns(Task.FromResult(equipmentResult));

            var result = await new EquipmentController(service.Object).CreateEquipment(equipment) as ObjectResult;
            var createEquipmentResult = result.Value as ServiceResult;

            Assert.Equal(ResultType.Success, createEquipmentResult.Type);

        }

        [Fact]
        public async Task Get_Equipment()
        {
            var service = new Mock<IEquipmentRepository>();

            var equipment = new ServiceResult<EquipmentVM>();

            var equipmentList = await GetEquipmentFakeData();

            equipment.Data = equipmentList.Data.Find(x => x.Id == 10);

            service.Setup(x => x.GetEquipment(10)).Returns(Task.FromResult(equipment));

            var result = await new EquipmentController(service.Object).GetEquipment(10) as ObjectResult;
            var equipmentResult = result.Value as ServiceResult<EquipmentVM>;

            Assert.Equal(10, equipmentResult.Data.Id);
        }
    }
}
