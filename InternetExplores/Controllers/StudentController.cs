using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetExplores.Models;

namespace InternetExplores.Controllers
{
    public class StudentController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly AppDBContext _dbContext;
        public StudentController( IConfiguration configuration, AppDBContext dbContext) {
            _configuration = configuration;
            _dbContext = dbContext;
        }
        public static void IntializeConection() {
            
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Registration() {

            return View();
        }
    }
}
