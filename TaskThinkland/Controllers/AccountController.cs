using Microsoft.AspNetCore.Mvc;
using TaskThinkland.Api.DTOs.UserDTOs;
using TaskThinkland.Api.Exceptions;
using TaskThinkland.Api.Services.UserService;

namespace TaskThinkland.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IUserService _userService;

    public AccountController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async ValueTask<IActionResult> Register(CreateUserDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
           return Created("Register", await _userService.InsertAsync(dto));
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }


    [HttpPost("login")]
    public async ValueTask<IActionResult> Login(LoginUserDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            return Ok( await _userService.LoginAsync(dto));
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (AnotherExceptions e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }
}