﻿using Application.DTOs;
using AutoMapper;
using DataLayer.Entities;
using DBCourseWorkAPI.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBCourseWorkAPI.Profiles {
    public class ApplicationProfile : Profile {
        public ApplicationProfile() {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Group, GroupDto>().ReverseMap();
            CreateMap<User, UserWithBalanceDto>().IncludeBase<User, UserDto>();
            CreateMap<Expense, ExpenseDto>().ReverseMap();
            CreateMap<Payment, PaymentDto>().ReverseMap();
        }
    }
}
