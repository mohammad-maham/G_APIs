using G_APIs.BussinesLogic.Interface;
using G_APIs.Models;
using G_APIs.Services;
using Newtonsoft.Json;
using static G_APIs.Models.Enums;

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
        var obj = new { username, password };

        var res = await new GoldApi()
        {
            Data = obj,
            Host = GoldHost.Accounting
        }.PostAsync();

        var user = JsonConvert.DeserializeObject<User>(res.Message);

        return user;
    }

    public  async Task<User?> Register(User model)
    {
        var obj = new { model.Username, model.Password,model.Mobile,model.Name,model.NationalCode};

        var res =await  new GoldApi()
        {
            Data = obj,
            Host = GoldHost.Accounting
        }.PostAsync();
        
        var user = JsonConvert.DeserializeObject<User>(res.Data);

        return user;
    }

  }
