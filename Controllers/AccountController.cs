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
        //var strCaptcha = new Captcha().Create(out string urlCaptcha);

        //_session.Set("Captcha", strCaptcha);

        return View(new User() { Captcha = GetCaptcha() });
    }

    public IActionResult Signup()
    {
        //var strCaptcha = new Captcha().Create(out string urlCaptcha);

        //_session.Set("Captcha", strCaptcha);
        //var model = new User() { Captcha = urlCaptcha };

        return View(new User() { Captcha = GetCaptcha() });

    }

    public IActionResult Header(User model)
    {
        return View(model);
    }
    public IActionResult Sidebar(Menu model)
    {
        return View(model);
    }
    public IActionResult Profile(User model)
    {
        return View(model ?? new User());
    }
    public IActionResult BankAccount(BankAccount model)
    {
        return View(model ?? new BankAccount());
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

            var res = await _account.Login(model);

            if (res != null)
            {

                if (res.StatusCode == 0)
                    return Json(new { result = false, message = res.Message });

                if (res.StatusCode == 200 && res.Data != null)
                {
                    var claims = new List<Claim> { new Claim(ClaimTypes.Name, (model.Username! ?? model.NationalCode!)) };
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                     return Json(new { result = true, data = res.Data });

                }

            }

            return Json(new { result = false, message = " رمز عبور یا نام کاربری اشتباه است " });
        }
        catch (Exception ex)
        {

            return Json(new { result = false, message = ex.Message });
        }

    }


    [HttpPost]
    public async Task<IActionResult> SignUp(User model)
    {
        try
        {
            var captcha = _session.Get<string>("Captcha");

            if (captcha != null && model.Captcha != null && model.Captcha != captcha)
                return Json(new { result = false, message = "متن تصویر اشتباه است." });


            model.Mobile = model.Mobile!.StartsWith("0") ? model.Mobile.Remove(0, 1) : model.Mobile;

            var res = await _account.SignUp(model);

            if (res != null)
            {

                if (res.StatusCode != 200)
                    return Json(new { result = false, message = res.Message });

                if (res.StatusCode == 200 && res.Data != null)
                {
                    var user = JsonConvert.DeserializeObject<User>(res.Data);

                    if (user.Id != null)
                        return Json(new { result = true, message = "لطفا کد ارسال شده به موبایل را وارد کنید", data = JsonConvert.SerializeObject(user) });

                }

            }

            return Json(new { result = false, message = "بروز خطا لطفا دوباره تلاش کنید." });
        }
        catch (Exception ex)
        {
            return Json(new
            {
                result = false,
                message = ex.Message
            });
        }

    }

    [HttpPost]
    public async Task<IActionResult> SetPassword(User model)
    {
        try
        {
            model.Username = model.NationalCode;
            var res = await _account.SetPassword(model);

            if (res != null)
            {

                if (res.StatusCode != 200)
                    return Json(new { result = false, message = res.Message });

                if (res.StatusCode == 200)
                    return Json(new { result = true, message = res.Message });

            }

            return Json(new { result = false, message = "بروز خطا لطفا دوباره تلاش کنید." });
        }
        catch (Exception ex)
        {

            return Json(new { result = false, message = ex.Message });
        }

    }

    public string GetCaptcha()
    {

        var strCaptcha = new Captcha().Create(out string urlCaptcha);

        _session.Set("Captcha", strCaptcha);

        return urlCaptcha;

    }

    [HttpPost]
    public async Task<IActionResult> CompleteProfile(User model)
    {
        try
        {
            var token = Request.Headers["Authorization"].FirstOrDefault();

            if (token == null || !token.StartsWith("Bearer "))
            {
                //return Unauthorized("Missing or invalid Authorization header.");
                return Json(new { result = false, message = "ورود غیر مجاز لطفا دوباره وارد شوید." });
            }

            var res = await _account.CompleteProfile(model, token);

            if (res != null)
            {

                if (res.StatusCode != 200)
                    return Json(new { result = false, message = res.Message });

                if (res.StatusCode == 200 && res.Data != null)
                  return Json(new { result = true, message = res.Message });


            }

            return Json(new { result = false, message = "بروز خطا لطفا دوباره تلاش کنید." });
        }
        catch (Exception ex)
        {
            return Json(new
            {
                result = false,
                message = ex.Message
            });
        }

    }
}

