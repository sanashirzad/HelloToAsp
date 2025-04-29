using HelloToAsp.Dtos.ToDoList;

namespace HelloToAsp.Dtos.User
{
    public class GetDetailsDto : BaseDto
    {
        public int Id { get; set; }

        public List<ToDoListDto> toDoLists { get; set; } // in dtos should not have field that directly refer to model type
    }
}
