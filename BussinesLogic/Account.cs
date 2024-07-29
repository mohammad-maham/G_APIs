using G_APIs.BussinesLogic.Interface;
using G_APIs.Models;
using G_APIs.Services;
using Newtonsoft.Json;
using RestSharp;
using static G_APIs.Common.Enums;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace G_APIs.BussinesLogic;

public class Account : IAccount
{
    private readonly ILogger<Account> _logger;

    private readonly IHttpContextAccessor _httpContextAccessor;


    public Account(ILogger<Account> logger, IHttpContextAccessor httpContextAccessor)
    {
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<ApiResult> Login(User model)
    {

        var res = await new GoldApi(GoldHost.Accounting, "/api/User/SignIn", model).PostAsync();

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

    public async Task<ApiResult> CompleteProfile(User model, string token)
    {
        var res = await new GoldApi(GoldHost.Accounting, "/api/User/CompleteProfile", model,authorization:token).PostAsync();

        return res;
    }

    public async Task<ApiResult> SubmitContact(User model, string token)
    {
        var res = await new GoldApi(GoldHost.Accounting, "/api/User/SubmitContact", model, authorization: token).PostAsync();

        return res;
    }

    public async Task<ApiResult> GetUserInfo(User model, string token)
    {
        var res = await new GoldApi(GoldHost.Accounting, "/api/User/GetUserInfo", model, authorization: token).PostAsync();

        return res;
    }
}
