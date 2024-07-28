using G_APIs.BussinesLogic.Interface;
using G_APIs.Models;
using G_APIs.Services;
using static G_APIs.Common.Enums;

namespace G_APIs.BussinesLogic;

public class Fund:IFund
{

    private readonly ILogger<Fund> _logger;

    private readonly IHttpContextAccessor _httpContextAccessor;


    public Fund(ILogger<Fund> logger, IHttpContextAccessor httpContextAccessor)
    {
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<ApiResult> GetWallet(WalletCurrency model)
    {

        var res = await new GoldApi(GoldHost.Wallet, "/api/Fund/GetWalletCurrency", model).PostAsync();

        return res;
    }
}
