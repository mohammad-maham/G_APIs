using G_APIs.BussinesLogic.Interface;
using G_APIs.Common;
using G_APIs.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Diagnostics;

namespace G_APIs.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IFund _fund;

    public HomeController(ILogger<HomeController> logger, IConfiguration config, IFund fund)
    {
        _logger = logger;
        _fund = fund;
    }

    public IActionResult Index(Dashboard model)
    {

        //return View(model);
        var logedin = User.Identity!.IsAuthenticated;

        return !logedin ? RedirectToAction("Login", "Account") : (IActionResult)View(model);

    }
    public IActionResult Chart1()
    {
        return View(new Chart1());
    }

    public IActionResult Wallet(WalletCurrency model)
    {
        try
        {
            model.Id = 1;
            var res = _fund.GetWallet(model);

            return View(new List<WalletCurrency>());
        }
        catch (Exception ex)
        {

            return Json(new { result = false, message = ex.Message });
        }


    }
    public IActionResult Sidebar(User model)
    {
        return View(model);
    }
    public IActionResult Header(User model)
    {
        return View(model);
    }
}
