﻿using InternetExplores.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace InternetExplores.Controllers
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
            TimerViewModel model = new TimerViewModel();
            TempData["EndTime"] = ViewData["EndTime"];
            return View(model);

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    
        [HttpPost]
        public void PostCountdownTimer(TimerViewModel model)
        {
            if (ViewData["EndTime"] == null)
            {
                ViewData["EndTime"] = DateTime.Now.AddHours(model.Hour).AddMinutes(model.Minute).AddSeconds(model.Second).ToString("dd-MM-yyyy h:mm:ss tt");
            }
            TempData["EndTime"] = ViewData["EndTime"];
            Response.Redirect("/Home/Index");
        }

        // Destroys EndTime ViewData object
        public void StopTimer()
        {
           // ViewData.Abandon();
        }
    }
}
