using AutoMapper;
using Bandora.Models;
using Bondora.Api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bondora.Api.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Customer, CustomerVM>().ReverseMap();
            CreateMap<Order, OrderVM>().ForMember(dest => dest.OrderDetails, c => c.MapFrom(m => m.OrderDetails)).ReverseMap();
            CreateMap<Equipment, EquipmentVM>().ReverseMap();
            CreateMap<OrderDetail, OrderDetailVM>().ForMember(dest => dest.Equipment, c => c.MapFrom(m => m.Equipment))                                                    
                                                       .ReverseMap();
        }        
    }
}
