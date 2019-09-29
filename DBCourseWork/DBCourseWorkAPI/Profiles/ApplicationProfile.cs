using Application.DTOs;
using AutoMapper;
using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBCourseWorkAPI.Profiles {
    public class ApplicationProfile : Profile {
        public ApplicationProfile() {
            CreateMap<User, UserDto>();
            CreateMap<Group, GroupDto>();
            CreateMap<User, UserWithBalanceDto>().IncludeBase<User, UserDto>();
        }
    }
}
