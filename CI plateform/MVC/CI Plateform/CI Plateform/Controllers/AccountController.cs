using Microsoft.AspNetCore.Mvc;

namespace CI_Plateform.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
