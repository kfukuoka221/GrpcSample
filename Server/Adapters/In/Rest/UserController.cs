using Microsoft.AspNetCore.Mvc;
using Server.Application.Dtos;
using Server.Application.Ports.In;

namespace Server.Adapters.In.Rest;

[ApiController]
[Route("[controller]")]
public class UserController(IUserService userService) : ControllerBase
{
    private readonly IUserService _userService = userService;

    [HttpGet]
    public async Task<UserDto[]> GetAsync()
    {
        var user = await _userService.GetUsersAsync();
        return user.ToArray();
    }
}
