using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Threading.Tasks;
using User_Service.Exceptions;
using User_Service.Model;
using User_Service.Service;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateUser([FromBody] UserDetails user)
    {
        try
        {
            //Log.Information("Attempting to create user: {@UserDetails}", user);

            var createdUser = await _userService.CreateUser(user);

            Log.Information("User created: {@UserDetails}", createdUser);

            return Ok(createdUser);
        }
        catch (UsernameAlreadyExistException ex)
        {
            Log.Warning(ex, "Username already exists: {@UserDetails}", user);
            return Conflict(ex.Message);
        }
        catch (UserNotCreatedException ex)
        {
            Log.Error(ex, "User creation failed: {@UserDetails}", user);
            return StatusCode(500, ex.Message);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "An error occurred while processing the request for user: {@UserDetails}", user);
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUser()
    {
        try
        {
           // Log.Information("Attempting to retrieve all users.");

            var users = await _userService.GetAllUser();

            Log.Information("Retrieved all users: {@Users}", users);

            return Ok(users);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "An error occurred while processing the request to retrieve all users.");
            return StatusCode(500, "An error occurred while processing the request.");
        }
    }

    /*  [HttpGet("{userId}")]
    public IActionResult GetByUserId(int userId)
    {
        var user = _userService.GetUserById(userId);
        return Ok(user);
    }*/
}
