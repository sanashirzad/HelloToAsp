using System.ComponentModel.DataAnnotations.Schema;

namespace HelloToAsp.Data;

public class ToDoList
{
    public int Id { get; set; }
    public string Task { get; set; }
    public DateOnly? StartDateTime { get; set; }
    public DateOnly? EndDateTime { get; set; }
    public int? Duration { get; set; }
    public string? Description { get; set; }
    public bool IsCompleted { get; set; } = false;

    [ForeignKey(nameof(UserId))]
    public int UserId { get; set; }
    public User User { get; set; }
}
