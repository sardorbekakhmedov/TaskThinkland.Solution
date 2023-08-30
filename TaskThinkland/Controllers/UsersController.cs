using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskThinkland.Api.DTOs.UserDTOs;
using TaskThinkland.Api.Exceptions;
using TaskThinkland.Api.Filters;
using TaskThinkland.Api.Services.UserService;

namespace TaskThinkland.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    [Authorize]
    public async ValueTask<IActionResult> GetAll([FromQuery] UserFilter filter)
    {
        try
        {
            return Ok(await _userService.GetAllAsync(filter));
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }

    [HttpGet("{userId:guid}")]
    [Authorize]
    public async ValueTask<IActionResult> GetById(Guid userId)
    {
        try
        {
            return Ok(await _userService.GetByIdAsync(userId));
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }

    [HttpPut("{userId:guid}")]
    [Authorize]
    public async ValueTask<IActionResult> Update(Guid userId, UpdateUserDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            return Ok(await _userService.UpdateAsync(userId, dto));
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }

    [HttpDelete("{userId:guid}")]
    [Authorize]
    public async ValueTask<IActionResult> Delete(Guid userId)
    {
        try
        {
            await _userService.DeleteAsync(userId);
            return Ok();
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }
}