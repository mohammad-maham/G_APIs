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

    public HomeController(ILogger<HomeController> logger, IConfiguration config )
    {
        _logger = logger;
    }

    public IActionResult Index()
    {

        return !User.Identity!.IsAuthenticated ? RedirectToAction("Login", "Account") : (IActionResult)View();
    }
    public IActionResult Chart1()
    {
        return View(new Chart1());
    }

}
