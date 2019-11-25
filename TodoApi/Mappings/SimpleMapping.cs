using AutoMapper;
using TodoApi.Dto;
using TodoApi.Models;

namespace TodoApi.Mappings
{
   public class SimpleMappings : Profile
   {
                public SimpleMappings()
        {
            CreateMap<NewTodoItemDTO, TodoItem>();
            CreateMap<NewDepartmentDTO, DepartmentZone>();
        }
    }
    }
