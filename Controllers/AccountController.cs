using G_APIs.BussinesLogic.Interface;
using G_APIs.Common;
using G_APIs.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace G_APIs.Controllers;

public class AccountController : Controller
{
    private readonly IAccount _account;
    private readonly ISession _session;

    public AccountController(IAccount account, ISession session)
    {
        _account = account;
        _session = session;
    }

    public IActionResult Login()
    {
        var captcha = new Captcha(_session).Create();
        var model = new User() { Captcha = captcha };
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Login(User model)
    {
        var captcha = _session.Get<string>("Captcha");
        if (captcha != null && model.Captcha!=null && model.Captcha!=captcha)
            return Json(new { result = false, message = "متن تصویر اشتباه است." });

        var loginResult = await _account.Login(model.Username, model.Password);

        if (loginResult != null)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, model.Username)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            return Json(new { result = true });
        }

        return Json(new { result = false, message = "" });
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    public async Task<IActionResult> Register(User model)
    {
        var loginResult = await _account.Login(model.Username, model.Password);

        if (loginResult != null)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, model.Username)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            return Json(new { result = true });
        }

        return Json(new { result = false, message = "" });
    }
}