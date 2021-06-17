using InternetExplores.Helpers;
using InternetExplores.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetExplores.Controllers
{
   
    public class AdminController : Controller
    { 
        
        private List<StudentModel> allStudent = new List<StudentModel>();
        private readonly IConfiguration _configuration;
        private readonly AppDBContext _dbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AdminController(IConfiguration configuration, AppDBContext dbContext, IWebHostEnvironment webHostEnvironment)
        {
            _configuration = configuration;
            _dbContext = dbContext;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult IndexAdmin()
        {
            return View();
        }
       public IActionResult StudentsList()
        {
            allStudent = DbHelper.SelectAllStudents(_configuration, allStudent);
            return View(allStudent);
        }

        public IActionResult ApplicationStudent(string StudentEmail)
        {

            StudentModel mystudent = DbHelper.GetAllStudent(_configuration, StudentEmail);
            StudentFiles studentFile = DbHelper.GetAllStudentDocuments(_configuration, mystudent.StudentNo);
            mystudent.nextofKinUrl = studentFile.nextofKinUrl;
            mystudent.idcopyUrl = studentFile.idcopyUrl;
            mystudent.financialProofUrl = studentFile.financialProofUrl;
            mystudent.matricResultUrl = studentFile.matricResultUrl;
            return View(mystudent);



        }
      [HttpPost]
        public IActionResult ApplicationStudent(StudentModel student)
        {

            StudentModel mystudent = DbHelper.GetAllStudent(_configuration, student.StudentEmail);
            StudentFiles studentFile = DbHelper.GetAllStudentDocuments(_configuration, mystudent.StudentNo);
            mystudent.nextofKinUrl = studentFile.nextofKinUrl;
            mystudent.idcopyUrl = studentFile.idcopyUrl;
            mystudent.financialProofUrl = studentFile.financialProofUrl;
            mystudent.matricResultUrl = studentFile.matricResultUrl;
            return View(mystudent);



        }

        public IActionResult StudentApplication(string StudentEmail) {
            return View();
        }
    }
}
