using G_APIs.Models;
using Microsoft.AspNetCore.Mvc;

namespace G_APIs.Views.Shared.Components;

public class WalletViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(Wallet model)
    {

        return View("Wallet", model);
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
