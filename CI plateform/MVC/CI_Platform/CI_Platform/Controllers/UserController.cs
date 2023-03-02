using Ciplatform.Entities.Data;
using Ciplatform.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System.Web.Helpers;
using Umbraco.Core.Persistence.Repositories;

namespace CI_Platform.Controllers
{
    public class UserController : Controller
    {
        private readonly CiContext _Context;

        public UserController(CiContext context)
        {
            _Context = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(User u)
        {

            var Data = _Context.Users.FirstOrDefault(m => m.Email == u.Email && m.Password == u.Password);

            if (Data == null)
            {
                TempData["Error"] = "Login Failed";
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        

        

        public IActionResult Forgot() { 
                
            return View();
        }
        public IActionResult Newpass()
        {

            return View();
        }
        public IActionResult Registation()
        {

            return View();
        }
    }
}
