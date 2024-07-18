using G_APIs.BussinesLogic.Interface;
using G_APIs.Models;
using G_APIs.Services;
using Newtonsoft.Json;
using static G_APIs.Models.Enums;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace G_APIs.BussinesLogic;

public class Account : IAccount
{
    private readonly ILogger<Account> _logger;

    public Account(ILogger<Account> logger)
    {
        _logger = logger;
    }

    private readonly List<User> _users = new List<User>
        {
            new User { Id = 1, Username = "admin", Password = "admin" },
            new User { Id = 2, Username = "maham", Password = "maham" }
        };

    public async Task<User?> Login(string username, string password)
    {
        //return new User();

        var obj = new { username, password };

        var res = await new GoldApi(GoldHost.Accounting, "/api/User/SignIn", obj).PostAsync();

        var user = JsonConvert.DeserializeObject<User>(res.Message);

        return user;
    }

    public async Task<User?> Register(User model)
    {
        var obj = new { model.Username, model.Password, model.Mobile, model.Name, model.NationalCode, model.ConfirmCode };

        var res = await new GoldApi(GoldHost.Accounting, "/api/User/SendOTP", obj).PostAsync();

        var user = JsonConvert.DeserializeObject<User>(res.Data);

        return user;
    }

    public async Task<User?> GetConfirmCode(User model)
    {

        var res = await new GoldApi( GoldHost.Accounting,   "/api/User/SignUp", model).PostAsync();

        var user = JsonConvert.DeserializeObject<User>(res.Data) as User;

        return user;
    }

    public async Task<User?> SetPassword(User model)
    {
        var res = await new GoldApi(GoldHost.Accounting, "/api/User/SetPassword", model).PostAsync();

        var user = JsonConvert.DeserializeObject<User>(res.Data) as User;

        return user;
    }
}
