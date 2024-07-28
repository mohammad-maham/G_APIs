using G_APIs.Models;

namespace G_APIs.BussinesLogic.Interface;

public interface IFund
{
    Task<ApiResult> GetWallet(WalletCurrency model);

}
