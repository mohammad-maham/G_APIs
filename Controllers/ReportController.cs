using G_APIs.Models;
using Microsoft.AspNetCore.Mvc;

namespace G_APIs.Controllers;

public class ReportController : Controller
{
    public IActionResult Report1(Report model)
    {
        return View(model ?? new Report());
    }
    public IActionResult Report2(Report model)
    {
        return View(model ?? new Report());
    }
    public IActionResult Report3(Report model)
    {
        return View(model ?? new Report());
    }
}
