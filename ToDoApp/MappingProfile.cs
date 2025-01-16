using AutoMapper;
using Entities.Dto_s;
using Entities.Models;

namespace ToDoApp
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TodoItem, GetTodoItemDto>().ReverseMap();
            CreateMap<TodoItem, AddTodoItemdto>().ReverseMap();
        }
    }
}
