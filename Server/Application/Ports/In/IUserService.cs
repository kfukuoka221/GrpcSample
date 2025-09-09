using Server.Application.Dtos;

namespace Server.Application.Ports.In;

public interface IUserService
{
    public Task<IEnumerable<UserDto>> GetUsersAsync();
}
