namespace Api_I.Models;

public record Authentication(string? UserName, string? UserPassword);
public record Users(int Id, string FirstName, string LastName, string UserName);