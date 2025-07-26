using AutoMapper;
using HelloToAsp.Data;
using HelloToAsp.Dtos.Auth;
using HelloToAsp.Dtos.ToDoList;
using HelloToAsp.Dtos.User;

namespace HelloToAsp.Configs
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<User, CreateDto>().ReverseMap();
            CreateMap<User, Dtos.User.GetDto>().ReverseMap();
            CreateMap<User, GetDetailsDto>().ReverseMap();
            CreateMap<User, UpdateDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();

            CreateMap<ToDoList, ToDoListDto>().ReverseMap();
            CreateMap<ToDoList, ToDoListGetDetailsDto>().ReverseMap();
            CreateMap<ToDoList, ToDoListGetDetailsDto>().ReverseMap();
            CreateMap<ToDoList, ToDoListUpdateDto>().ReverseMap();
            CreateMap<ToDoList, ToDoListCreateDto>().ReverseMap();

            CreateMap<User, RegUserDto>().ReverseMap();
        }
    }
}
