using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Application.Dto.Mission;
using ToDo.Domain.Models;

namespace ToDo.Application.MapProfiles
{
    public class MissionProfile : Profile
    {
        public MissionProfile()
        {
            CreateMap<Mission, MissionDto>().ReverseMap();
            CreateMap<Mission, MissionAllDto>().ReverseMap();
        }
    }
}
