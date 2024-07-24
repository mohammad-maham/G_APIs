using Microsoft.AspNetCore.Mvc;

namespace G_APIs.Controllers;

public class FundController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
