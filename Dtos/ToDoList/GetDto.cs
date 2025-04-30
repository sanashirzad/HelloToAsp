using HelloToAsp.Dtos.User;

namespace HelloToAsp.Dtos.ToDoList
{
    public class GetDto : ToDoListDto
    {
        public UserDto users { get; set; }
    }
}
