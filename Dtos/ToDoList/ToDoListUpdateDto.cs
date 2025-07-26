namespace HelloToAsp.Dtos.ToDoList
{
    public class ToDoListUpdateDto: BaseDto
    {
        public int Id { get; set; }
        public bool? IsCompleted { get; set; }
    }
}
