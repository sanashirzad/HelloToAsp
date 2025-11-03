using HelloToAsp.Core.Dtos.ToDoList;

namespace HelloToAsp.Core.Dtos.User
{
    public class GetDetailsDto : BaseDto
    {
        public int Id { get; set; }

        public List<ToDoListDto> toDoLists { get; set; } // in dtos should not have field that directly refer to model type
    }
}
