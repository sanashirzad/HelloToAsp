namespace HelloToAsp.Data;

public class ToDoList
{
    public int Id { get; set; }
    public string Task { get; set; }
    public string? StartDateTime { get; set; }
    public string? EndDateTime { get; set; }
    public int? Duration { get; set; }
}
