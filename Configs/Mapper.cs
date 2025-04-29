using AutoMapper;
using HelloToAsp.Data;
using HelloToAsp.Dtos.ToDoList;
using HelloToAsp.Dtos.User;

namespace HelloToAsp.Configs
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<User, CreateDto>().ReverseMap();
            CreateMap<User, GetDto>().ReverseMap();
            CreateMap<User, GetDetailsDto>().ReverseMap();
            CreateMap<User, UpdateDto>().ReverseMap();

            CreateMap<ToDoList, ToDoListDto>().ReverseMap();
        }
    }
}
