namespace ReadJsonArray.Models;

public record AppSettings
{
    public List<User>? Users { get; init; }
    public List<Group>? Groups { get; init; }
};