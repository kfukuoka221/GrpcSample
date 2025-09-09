using Grpc.Core;
using Protos.Services;
using Server.Application.Dtos;
using Server.Application.Ports.In;

namespace Server.Adapters.In.Grpc;

public class UserController(IUserService userService) : UserService.UserServiceBase
{
    private readonly IUserService _userService = userService;

    public override async Task<UserList> GetAllUsers(
        Empty request,
        ServerCallContext context)
    {
        var userDtos = await _userService.GetUsersAsync();
        var users = userDtos.Select(Map);
        return new UserList { Users = { users } };
    }

    private static User Map(UserDto userDto)
    {
        return new User()
        {
            Id = userDto.Id,
            Name = userDto.Name,
            Email = userDto.Email,
        };
    }
}
