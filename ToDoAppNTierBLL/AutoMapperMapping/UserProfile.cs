using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoAppNTierDTO;
using ToDoAppNTierEntities.Domains;

namespace ToDoAppNTierBLL.AutoMapperMapping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User,UserListDto>().ReverseMap();
            CreateMap<User,UserRegisterDto>().ReverseMap();
            CreateMap<User,UserLoginDto>().ReverseMap();
            CreateMap<UserEditDto,UserListDto>().ReverseMap();
            CreateMap<UserEditDto,User>().ReverseMap();


        }
    }
}
