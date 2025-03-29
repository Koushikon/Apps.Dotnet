using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace AppModels;

public class ApplicationUser : IdentityUser
{
    [MaxLength(200, ErrorMessage = "First name must be less than 200character.")]
    public string FirstName { get; set; } = default!;

    [MaxLength(200, ErrorMessage = "First name must be less than 200character.")]
    public string LastName { get; set; } = default!;
}