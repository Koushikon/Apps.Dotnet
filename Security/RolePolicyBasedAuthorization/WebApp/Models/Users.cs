namespace WebApp.Models;

public class Users
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string Name { get; set; }
    public string EmailId { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
    public string DateOfBirth { get; set; }

    public IEnumerable<Users> GetUsers()
    {
        var user1 = new Users { Id = 103, UserName = "admin", Name = "Admin", EmailId = "admin@gmail.com", Password = "1234", Role = "Admin", DateOfBirth = "01/01/2000" };
        var user2 = new Users { Id = 105, UserName = "super", Name = "Super", EmailId = "super@gmail.com", Password = "1234", Role = "Super", DateOfBirth = "01/01/2005" };
        var user3 = new Users { Id = 107, UserName = "koushik", Name = "Koushik", EmailId = "koushik@gmail.com", Password = "1234", Role = "User", DateOfBirth = "01/01/2002" };
        var users = new List<Users> { user1, user2, user3 };
        return users;
    }
}