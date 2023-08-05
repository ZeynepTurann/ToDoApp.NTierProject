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
    public class ToDoProfile:Profile
    {
        public ToDoProfile()
        {
            CreateMap<ToDo,ToDoListDto>().ReverseMap();
            CreateMap<ToDo,ToDoCreateDto>().ReverseMap();
            CreateMap<ToDo,ToDoUpdateDto>().ReverseMap();
            CreateMap<ToDoListDto, ToDoUpdateDto>().ReverseMap();
        }
    }
}
