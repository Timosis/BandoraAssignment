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

    public interface IEquipmentRepository
    {
        Task<ServiceResult<List<EquipmentVM>>> GetEquipmentsList();
        Task<ServiceResult<EquipmentVM>> GetEquipment(int id);
        Task<ServiceResult> CreateEquipment(EquipmentVM customer);                
    }

    public class EquipmentRepository : IEquipmentRepository
    {
        private readonly IMapper mapper;
        private readonly BondoraDataContext context;

        public EquipmentRepository(IMapper mapper, BondoraDataContext context)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<ServiceResult> CreateEquipment(EquipmentVM equipment)
        {
            ServiceResult result = new ServiceResult()
            {
                Type = ResultType.UnKnown,
                Message = "Unknown Process"
            };

            var equipmentEntity = mapper.Map<EquipmentVM, Equipment>(equipment);
            await context.Equipments.AddAsync(equipmentEntity);
            int saveResult = await context.SaveChangesAsync();

            if (saveResult > 0)
            {
                result.Type = ResultType.Success;
                result.Message = "Equipment Is Created Succesfully";
            }
            return result;
        }

        public async Task<ServiceResult<EquipmentVM>> GetEquipment(int id)
        {
            ServiceResult<EquipmentVM> result = new ServiceResult<EquipmentVM>
            {
                Type = ResultType.UnKnown,
                Message = "Unknown Process"
            };
            var equipment = mapper.Map<Equipment, EquipmentVM>(await context.Equipments.FindAsync(id));
            if (equipment != null)
            {
                result.Data = equipment;
                result.Type = ResultType.Success;
                result.Message = "Equipment Got Succesfully";
            }
            else
            {
                result.Type = ResultType.Error;
                result.Message = "Something Went Wrong";
            }

            return result;
        }

        public async Task<ServiceResult<List<EquipmentVM>>> GetEquipmentsList()
        {
            ServiceResult<List<EquipmentVM>> result = new ServiceResult<List<EquipmentVM>>
            {
                Type = ResultType.UnKnown,
                Message = "Unknown Process"
            };

            var equipments = mapper.Map<List<Equipment>, List<EquipmentVM>>(await context.Equipments.ToListAsync());

            if (equipments != null)
            {
                result.Data = equipments;
                result.Type = ResultType.Success;
                result.Message = "Equipment List Got Succesfully";
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
