using G_APIs.BussinesLogic.Interface;
using G_APIs.Models;
using System.Text.Json;

namespace G_APIs.BussinesLogic;

public class UserService:IUserService
{
    private readonly List<User> _users = new List<User>
        {
            new User { Id = 1, Username = "admin", Password = "admin" },
            new User { Id = 2, Username = "maham", Password = "maham" }
        };

    //public Task<User?> Authenticate(string username, string password)
    //{
    //    var user = _users.SingleOrDefault(x => x.Username == username && x.Password == password);
    //    return Task.FromResult(user);
    //}

    private readonly HttpClient _httpClient;

    public UserService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<User?> Authenticate(string username, string password)
    {



        var response = await _httpClient.PostAsJsonAsync("https://api.example.com/login", new { username, password });
        response.EnsureSuccessStatusCode();

        var responseBody = await response.Content.ReadAsStringAsync();
        var user = JsonSerializer.Deserialize<User>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        return user;
    }
}
