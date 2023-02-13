﻿using CI_Plateform.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CI_Plateform.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult login()
        {
            return View();
        }
        public IActionResult forgot()
        {
            return View();
        }
        public IActionResult newpass()
        {
            return View();
        }
        public IActionResult registetion()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}