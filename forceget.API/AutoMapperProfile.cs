using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using forceget.DataAccess.Models.Dtos;
using forceget.DataAccess.Models.Entities;

namespace forcegetAPI
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {

            CreateMap<User, GetUserDto>().ReverseMap();
            CreateMap<GetUserDto, User>().ReverseMap();

            CreateMap<List<User>, GetUserDto>().ReverseMap();
            CreateMap<GetUserDto, List<User>>().ReverseMap();



            // CreateUserDto -> User 
            CreateMap<User, CreateUserDto>().ReverseMap();
            CreateMap<CreateUserDto, User>().ReverseMap();

            // GetUserDto -> User 
            CreateMap<User, GetUserDto>().ReverseMap();
            CreateMap<GetUserDto, User>().ReverseMap();

            // CreateUserDto -> GetUserDto 
            CreateMap<CreateUserDto, GetUserDto>().ReverseMap();
            CreateMap<GetUserDto, CreateUserDto>().ReverseMap();

        }

    }
}