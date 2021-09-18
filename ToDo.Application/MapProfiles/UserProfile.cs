using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Application.Dto.User;
using ToDo.Domain.Models;

namespace ToDo.Application.MapProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserRegisterDto>().ReverseMap();
        }
    }
}
