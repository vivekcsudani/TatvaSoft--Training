using Microsoft.AspNetCore.Mvc;

namespace CI_Platform.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}
