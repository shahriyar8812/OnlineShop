using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Domain.Entities;
using System.Security.Claims;
using System.Linq;
using System.Collections.Generic;
using Infrastructure.Data;

public class AccountController : Controller
{
    private readonly ApplicationDbContext _context;

    public AccountController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(string username, string password, string returnUrl = null)
    {
        // جستجو در جدول UserModel برای پیدا کردن کاربر
        var user = _context.Users.FirstOrDefault(u => u.Username == username && u.Password == password);

        // اگر کاربر پیدا شد
        if (user != null)
        {
            // افزودن ادعاهای مربوط به نام کاربری و نقش
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, user.Role)  // نقش کاربر از فیلد Role
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            // ورود به سیستم
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

            // هدایت به صفحه محصولات یا صفحه‌ای که کاربر می‌خواست
            return Redirect(returnUrl ?? "/Product/Index");
        }

        // اگر کاربر یافت نشد
        ViewBag.ErrorMessage = "اطلاعات وارد شده نادرست است!";
        return View();
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login");
    }
}
