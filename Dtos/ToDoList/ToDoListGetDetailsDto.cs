using HelloToAsp.Dtos.User;

namespace HelloToAsp.Dtos.ToDoList
{
    public class ToDoListGetDetailsDto : BaseDto
    {
        public int Id { get; set; }
        public bool IsCompleted { get; set; }
        public UserDto User { get; set; }
    }
}
