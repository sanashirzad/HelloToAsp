using HelloToAsp.Core.Dtos.User;

namespace HelloToAsp.Core.Dtos.ToDoList
{
    public class GetDto : ToDoListDto
    {
        public UserDto users { get; set; }
    }
}
