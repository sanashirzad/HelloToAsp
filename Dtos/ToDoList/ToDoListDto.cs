namespace HelloToAsp.Dtos.ToDoList
{
    public class ToDoListDto
    {
        public int Id { get; set; }
        public string Task { get; set; }
        public bool IsCompleted { get; set; } = false;
    }
}
