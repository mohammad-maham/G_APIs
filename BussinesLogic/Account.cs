using G_APIs.BussinesLogic.Interface;
using G_APIs.Models;
using G_APIs.Services;
using Newtonsoft.Json;
using RestSharp;
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

    public async Task<ApiResult> Login(User model)
    {

        var res = await new GoldApi(GoldHost.Accounting, "/api/User/SignIn", model,Method.Get).PostAsync();

        //var user = JsonConvert.DeserializeObject<User>(res.Message!);

        return res;
    }

    public async Task<ApiResult> SignUp(User model)
    {

        var res = await new GoldApi(GoldHost.Accounting, "/api/User/SignUp", model).PostAsync();

        //var user = JsonConvert.DeserializeObject<User>(res.Data);

        return res;
    }

    public async Task<ApiResult> SetPassword(User model)
    {
        var res = await new GoldApi(GoldHost.Accounting, "/api/User/SetPassword", model).PostAsync();

        return res;
    }

    public async Task<ApiResult> CompleteProfile(User model)
    {
        var res = await new GoldApi(GoldHost.Accounting, "/api/User/CompleteProfile", model).PostAsync();

        return res;
    }


}
