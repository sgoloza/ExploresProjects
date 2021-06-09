using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternetExplores.Models;
using InternetExplores.Helpers;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace InternetExplores.Controllers
{
    public class StudentController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly AppDBContext _dbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public StudentController( IConfiguration configuration, AppDBContext dbContext,IWebHostEnvironment webHostEnvironment) {
            _configuration = configuration;
            _dbContext = dbContext;
            _webHostEnvironment = webHostEnvironment;
        }
        public static void IntializeConection() {
            
        }
        public IActionResult Index()
        {
            return View();
        }
        
        public  IActionResult Registration() {

          
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Registration(StudentModel myStudent)
        {
            
            if (ModelState.IsValid)
            {
                if (myStudent.CoverPhoto != null)
                {
                    string folder = "Documents/ID/";
                    myStudent.CoverImageUrl = await UploadImage(folder, myStudent.CoverPhoto);
                }
            }
                return View();
        }
            public IActionResult Profile()
        {
            StudentModel mystudent = DbHelper.GetAllStudent(_configuration, User.Identity.Name.ToString());
            return View(mystudent);
        }
            private async Task<string> UploadImage(string folderPath, IFormFile file)
            {

                folderPath += Guid.NewGuid().ToString() + "_" + file.FileName;

                string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folderPath);

                await file.CopyToAsync(new FileStream(serverFolder, FileMode.Create));

                return "/" + folderPath;
            }
        }
}
