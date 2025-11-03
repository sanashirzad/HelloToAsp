using HelloToAsp.Core.Dtos.User;

namespace HelloToAsp.Core.Dtos.ToDoList
{
    public class ToDoListGetDetailsDto : BaseDto
    {
        public int Id { get; set; }
        public bool IsCompleted { get; set; }
        public UserDto User { get; set; }
    }
}
