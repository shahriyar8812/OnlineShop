using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace MySuperPrjMvc.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        public IActionResult AdminPage()
        {
            var role = TempData["Role"]?.ToString();

            if (role == "Admin")
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
