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

    public HomeController(ILogger<HomeController> logger, IConfiguration config)
    {
        _logger = logger;
    }

    public IActionResult Index(Dashboard model)
    {

        //return !User.Identity!.IsAuthenticated ? RedirectToAction("Login", "Account") : (IActionResult)View(model);
         var logedin = User.Identity!.IsAuthenticated;

         return !logedin ? RedirectToAction("Login", "Account") : (IActionResult)View(model);

    }
    public IActionResult Chart1()
    {
        return View(new Chart1());
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
