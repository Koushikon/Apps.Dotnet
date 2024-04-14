namespace ReadJsonArray.Models;


public record Group(int Id, string Name)
{
    public List<Member>? Members { get; init; }
};