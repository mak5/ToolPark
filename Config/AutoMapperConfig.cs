using AutoMapper;
using Common.Dtos;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToolPark.Config
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<ToolDto, Tool>().ReverseMap()
                .ForMember(t => t.CurrentSerialNumber, dst => dst.MapFrom(src => src.SerialNumber));
        }
    }
}
