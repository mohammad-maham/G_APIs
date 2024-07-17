using Newtonsoft.Json;
using G_APIs.Models;
using RestSharp;
using static G_APIs.Models.Enums;

namespace G_APIs.Services;

public class GoldApi
{
    private string? ApiPath { get; set; }
    public GoldHost Host { get; set; }
    public string? Authorization { get; set; }
    public string? Action { get; set; }
    public object? Data { get; set; }


    private readonly IConfiguration _config;

    public GoldApi() { }

    public GoldApi(IConfiguration config)
    {
        _config = config;

    }

    public GoldApi(GoldHost host, string action, string authorization, object data)
    {
        if (host == GoldHost.Accounting)
            this.ApiPath = ConfigurationManager.AppSetting["GoldApi:AccountingApi"]!;

        if (host == GoldHost.IPG)
            this.ApiPath = ConfigurationManager.AppSetting["GoldApi:IPGApi"]!;

        if (host == GoldHost.Store)
            this.ApiPath = ConfigurationManager.AppSetting["GoldApi:StoreApi"]!;

        if (host == GoldHost.Wallet)
            this.ApiPath = ConfigurationManager.AppSetting["GoldApi:WalletApi"]!;

        this.Action = action;
        this.Authorization = authorization ?? "";
        this.Data = data;
    }

    public async Task<ApiResult> PostAsync()
    {
        try
        {
            var json = JsonConvert.SerializeObject(this.Data);
            var client = new RestClient();
            var request = new RestRequest
            {
                Method = Method.Post,
                Timeout = TimeSpan.FromSeconds(20),
            };
            request.AddHeader("Authorization:Bearer", this.Authorization ?? "");
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", json, ParameterType.RequestBody);

            var response = await client.ExecuteAsync(request);

            var res =
                 JsonConvert.DeserializeObject<ApiResult>(response.Content!)
                 ?? new ApiResult() { ResultCode = 0 };

            return res;

        }
        catch (Exception ex)
        {
            return new ApiResult()
            {
                ResultCode = -1,
                Message = ex.Message
            };
        }
    }
}


