using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace MySuperPrjMvc.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        public IActionResult UserPage()
        {
            var role = TempData["Role"]?.ToString();

            if (role == "User")
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
