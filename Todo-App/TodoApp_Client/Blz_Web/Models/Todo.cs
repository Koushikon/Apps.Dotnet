namespace Blz_Web.Models;

public class Todo
{
    public int? Id { get; set; }
    public string? Task { get; set; }
    public int? AssignTo { get; set; }
    public bool? IsComplete { get; set; }
}
