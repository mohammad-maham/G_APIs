using G_APIs.BussinesLogic.Interface;
using G_APIs.Models;
using Microsoft.AspNetCore.Mvc;

namespace G_APIs.Views.Shared.Components;

public class WalletViewComponent : ViewComponent
{
    private readonly IFund _fund;

    public WalletViewComponent(IFund fund)
    {
        _fund = fund;
    }

    public IViewComponentResult Invoke(WalletCurrency model)
    {

        //var t = _fund.GetWallet(model);
        return View("Wallet" );
    }
}
public class Chart1ViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(Chart1 model)
    {

        return View("Chart", model);
    }
}

public class Chart2ViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(Chart2 model)
    {

        return View("Chart", model);
    }
}

public class Chart3ViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(Chart3 model)
    {

        return View("Chart", model);
    }
}
public class HeaderViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(User model)
    {

        return View("Header", model);
    }
}
public class SidebarViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(Menu model)
    {

        return View("Sidebar", model);
    }
}