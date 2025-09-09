using Server.Application.Dtos;
using Server.Application.Ports.In;

namespace Server.Application.Services;

public class UserService : IUserService
{
    private static readonly Random Random = new();

    public async Task<IEnumerable<UserDto>> GetUsersAsync()
    {
        await Task.CompletedTask;

        const int userCount = 100000;
        var users = new List<UserDto>(userCount);

        for (int i = 1; i <= userCount; i++)
        {
            users.Add(new UserDto
            {
                Id = i,
                Name = GenerateRandomName(),
                Email = $"user{i}@test{Random.Next(1, 100)}.com"
            });
        }

        return users;
    }

    private static string GenerateRandomName()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        var firstNameLength = Random.Next(3, 8);
        var lastNameLength = Random.Next(3, 10);
        
        var firstName = new string(Enumerable.Range(0, firstNameLength)
            .Select(_ => chars[Random.Next(chars.Length)]).ToArray());
        var lastName = new string(Enumerable.Range(0, lastNameLength)
            .Select(_ => chars[Random.Next(chars.Length)]).ToArray());
        
        return $"{firstName} {lastName}";
    }
}
