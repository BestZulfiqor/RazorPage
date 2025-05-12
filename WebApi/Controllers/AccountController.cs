using System.Net;
using Domain.DTOs;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController(IAuthService service,
    UserManager<IdentityUser> userManager,
    SignInManager<IdentityUser> signInManager,
    DataContext context) : ControllerBase
{
    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto model)
    {
        var user = new IdentityUser
        {
            UserName = model.Username,
            PhoneNumber = model.Phone,
        };

        var result = await userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded)
            return BadRequest(result.Errors);

        return Ok("User registered");
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto model)
    {
        var result = await service.Login(model);
        if (result.StatusCode != (int)HttpStatusCode.OK)
        {
            return BadRequest(result.Message);
        }

        return Ok(result.Data);
    }
        
    [Authorize]
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await signInManager.SignOutAsync();
        return Ok("Logged out");
    }
        
    [Authorize]
    [HttpGet("customers")]
    public async Task<IActionResult> GetCustomers()
    {
        return Ok(await context.Customers.ToListAsync());
    }
}