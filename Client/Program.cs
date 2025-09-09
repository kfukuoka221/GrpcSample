using System.Diagnostics;
using System.Text.Json;
using Grpc.Net.Client;
using Protos.Services;

const string serverUrl = "https://localhost:7178";

Console.WriteLine("通信時間計測テスト");
Console.WriteLine("===================");

await MeasureRestApiPerformance();
Console.WriteLine();
await MeasureGrpcApiPerformance();

static async Task MeasureRestApiPerformance()
{
    Console.WriteLine("REST API 通信時間計測開始...");
    
    using var httpClient = new HttpClient();
    var stopwatch = Stopwatch.StartNew();
    
    try
    {
        var response = await httpClient.GetAsync($"{serverUrl}/User");
        response.EnsureSuccessStatusCode();
        
        var content = await response.Content.ReadAsStringAsync();
        var users = JsonSerializer.Deserialize<UserDto[]>(content, new JsonSerializerOptions 
        { 
            PropertyNameCaseInsensitive = true 
        });
        
        stopwatch.Stop();
        
        Console.WriteLine($"REST API: {users?.Length ?? 0} ユーザーを取得");
        Console.WriteLine($"通信時間: {stopwatch.ElapsedMilliseconds} ms");
    }
    catch (Exception ex)
    {
        stopwatch.Stop();
        Console.WriteLine($"REST API エラー: {ex.Message}");
        Console.WriteLine($"エラー発生までの時間: {stopwatch.ElapsedMilliseconds} ms");
    }
}

static async Task MeasureGrpcApiPerformance()
{
    Console.WriteLine("gRPC 通信時間計測開始...");
    
    using var channel = GrpcChannel.ForAddress(serverUrl);
    var client = new UserService.UserServiceClient(channel);
    var stopwatch = Stopwatch.StartNew();
    
    try
    {
        var response = await client.GetAllUsersAsync(new Empty());
        
        stopwatch.Stop();
        
        Console.WriteLine($"gRPC: {response.Users.Count} ユーザーを取得");
        Console.WriteLine($"通信時間: {stopwatch.ElapsedMilliseconds} ms");
    }
    catch (Exception ex)
    {
        stopwatch.Stop();
        Console.WriteLine($"gRPC エラー: {ex.Message}");
        Console.WriteLine($"エラー発生までの時間: {stopwatch.ElapsedMilliseconds} ms");
    }
}

public class UserDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}
