using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dtos.Account;
using WebApi.Interfaces;
using WebApi.Models;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly ITokenService _tokenService;

    public AccountController(UserManager<AppUser> userManager, ITokenService tokenService)
    {
        _userManager = userManager;
        _tokenService = tokenService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var appUser = new AppUser
            {
                UserName = registerDto.Username,
                Email = registerDto.Email
            };

            var createdUser = await _userManager.CreateAsync(appUser, registerDto.Password!);

            // User not create
            if (!createdUser.Succeeded)
                return StatusCode(500, createdUser.Errors);

            var roleResult = await _userManager.AddToRoleAsync(appUser, "User");

            // User role not added
            if (!roleResult.Succeeded)
                return StatusCode(500, roleResult.Errors);

            return Ok(new NewUserDto
            {
                Username = appUser.UserName!,
                Email = appUser.Email!,
                Token = _tokenService.CreateToken(appUser)
            });
        }
        catch (Exception e)
        {
            return StatusCode(500, e);
        }
    }

}