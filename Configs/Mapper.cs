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
            CreateMap<User, Dtos.User.CreateDto>().ReverseMap();
            CreateMap<User, Dtos.User.GetDto>().ReverseMap();
            CreateMap<User, Dtos.User.GetDetailsDto>().ReverseMap();
            CreateMap<User, Dtos.User.UpdateDto>().ReverseMap();

            CreateMap<ToDoList, ToDoListDto>().ReverseMap();
        }
    }
}
