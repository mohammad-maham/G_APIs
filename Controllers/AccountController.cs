using G_APIs.BussinesLogic.Interface;
using G_APIs.Common;
using G_APIs.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace G_APIs.Controllers;

public class AccountController : Controller
{
    private readonly IAccount _account;
    private readonly IHttpContextAccessor _session;

    public AccountController(IAccount account, IHttpContextAccessor session)
    {
        _account = account;
        _session = session;
    }

    public IActionResult Login()
    {
        var strCaptcha = new Captcha().Create(out string urlCaptcha);

        _session.Set("Captcha", strCaptcha);
        var model = new User() { Captcha = urlCaptcha };

        return View(model);
    }

    public IActionResult Signup(User model)
    {
        //var strCaptcha = new Captcha().Create(out string urlCaptcha);

        //_session.Set("Captcha", strCaptcha);
        //var model = new User() { Captcha = urlCaptcha };

        return View(model ?? new Models.User());
    }

    public    IActionResult Header(User model)
    {
        return View(model);
    }
    public IActionResult Sidebar(Menu model)
    {
        return View(model);
    }
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    public async Task<IActionResult> Login(User model)
    {
        try
        {
            var captcha = _session.Get<string>("Captcha");

            if (captcha != null && model.Captcha != null && model.Captcha != captcha)
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

            return Json(new { result = false, message = " رمز عبور یا نام کاربری اشتباه است " });
        }
        catch (Exception ex)
        {

            return Json(new { result = false, message = ex.Message });
        }
      
    }
    
    [HttpPost]
    public async Task<IActionResult> GetConfirmCode(User model)
    {
        try
        {
            model.Mobile = model.Mobile.StartsWith("0") ? model.Mobile.Remove(0, 1) : model.Mobile;

            var user = await _account.GetConfirmCode(model);

            return Json(new
            {
                result = true,
                Data = JsonConvert.SerializeObject(user)
            });
        }
        catch (Exception ex)
        {

            return Json(new { result = false, message = ex.Message });
        }
      
    }

    [HttpPost]
    public async Task<IActionResult> Register(User model)
    {
        try
        {
            var confirmCode = _session.Get<string>("ConfirmCode");

            if (confirmCode != null && model.ConfirmCode != null && model.ConfirmCode != confirmCode)
                return Json(new { result = true, message = "کد موبایل وارد شده اشتباه است" });

            var res = await _account.Register(model);

            if (res != null)
            {
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, model.Username)
            };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                return Json(new { result = 3, message = "ثبت نام با موفقیت انجام شد" });
            }

            return Json(new { result = false, message = "" });
        }
        catch (Exception ex)
        {
            return Json(new { result = false, message = ex.Message });
        }

    }

    [HttpPost]
    public async Task<IActionResult> SetPassword(User model)
    {
        try
        {
            var user = await _account.SetPassword(model);

            return Json(new
            {
                result = true,
                Data = JsonConvert.SerializeObject(user)
            });
        }
        catch (Exception ex)
        {

            return Json(new { result = false, message = ex.Message });
        }

    }
}

