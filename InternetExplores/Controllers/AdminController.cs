﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetExplores.Controllers
{
    public class AdminController : Controller
    { 
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult IndexAdmin()
        {
            return View();
        }
       
    }
}
