﻿using AutoMapper;
using Data.DTOs;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Order, OrderDto>().ReverseMap().ForMember(x => x.Id, opt => opt.Ignore());
            CreateMap<UserType, UserTypeDto>().ReverseMap().ForMember(x => x.Id, opt => opt.Ignore());
            CreateMap<ProductCategory, ProductCategoryDto>().ReverseMap().ForMember(x => x.Id, opt => opt.Ignore());
            CreateMap<ProductSubCategory, ProductSubCategoryDto>().ReverseMap().ForMember(x => x.Id, opt => opt.Ignore());
            CreateMap<AdminUser, AdminUserDto>().ReverseMap().ForMember(x => x.Id, opt => opt.Ignore());
            CreateMap<Product, ProductDto>().ReverseMap().ForMember(x => x.Id, opt => opt.Ignore());
            CreateMap<OrderStatus, OrderStatusDto>().ReverseMap().ForMember(x => x.Id, opt => opt.Ignore());
            CreateMap<User, UserDto>().ReverseMap().ForMember(x => x.Id, opt => opt.Ignore());
            CreateMap<User, UserTokenGenerationInformation>().ReverseMap().ForMember(x => x.Id, opt => opt.Ignore());
            CreateMap<User, UserRegisterDto>().ReverseMap().ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.UserName, opt => opt.AddTransform(x => x.ToLower()))
                .ForMember(x => x.UserEmail, opt => opt.AddTransform(x => x.ToLower()));
            CreateMap<AdminUserLoginDto, UserTokenGenerationInformation>().ReverseMap().ForMember(x => x.Id, opt => opt.Ignore());
        }
    }
}
