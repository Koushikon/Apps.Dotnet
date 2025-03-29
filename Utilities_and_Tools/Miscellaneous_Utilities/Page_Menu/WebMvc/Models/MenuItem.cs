namespace WebMvc.Models;

public class MenuItem
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string ActionName { get; set; } = default!;
    public string ControllerName { get; set; } = default!;
    public string Url { get; set; } = default!;
    public bool Disable { get; set; } = default!;
    public bool HasAccess { get; set; } = default!;
    public Menu? ParentMenu { get; set; }
}