using System.Text.Json.Serialization;

namespace UserApi.Models;

public class UserModel
{
	[JsonIgnore]
	public int Id { get; set; }

	public string Email { get; set; } = string.Empty;
	public string Password { get; set; } = string.Empty;

	public string FirstName { get; set; } = string.Empty;
	public string LastName { get; set; } = string.Empty;

	public string FullName { get => $"{FirstName} {LastName}"; }
}