using HelloToAsp.Dtos.User;

namespace HelloToAsp.Dtos.ToDoList
{
    public class GetDetailsDto : BaseDto
    {
        public int Id { get; set; }
        public bool IsCompleted { get; set; }
        public UserDto users { get; set; }
    }
}
