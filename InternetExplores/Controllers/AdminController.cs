using InternetExplores.Helpers;
using InternetExplores.Models;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        [HttpGet]
        public IActionResult Index()
        {
           
            DbHelper.SelectAllStudents(_configuration, allStudent);
            ViewBag.AdminNewStudents = allStudent;
            return View();
        }
        
        [HttpPost]
        public IActionResult Index( StudentModel mystudent)
        {
            DbHelper.SelectAllStudents(_configuration, allStudent);
            ViewBag.AdminNewStudents = allStudent;
            return View();
        }
        [Authorize]
        public IActionResult IndexAdmin()
        {
            return View();
        }
        [Authorize]
        public IActionResult StudentsList()
        {
            List<StudentModel> allStudents = new List<StudentModel>();
            allStudents = DbHelper.SelectAllStudents(_configuration, allStudents);

            return View(allStudents);
        }
        [Authorize]
        [HttpGet]
        public IActionResult ApplicationStudent(string StudentEmail, string Studentstatus = "null" , string Commentsection = "null")
        {
            StudentModel mystudent = new StudentModel();
            StudentFiles studentFile = new StudentFiles();
            mystudent = DbHelper.GetAllStudent(_configuration, StudentEmail);
            studentFile = DbHelper.GetAllStudentDocuments(_configuration, mystudent.StudentNo);
            mystudent.nextofKinUrl = studentFile.nextofKinUrl;
            mystudent.idcopyUrl = studentFile.idcopyUrl;
            mystudent.financialProofUrl = studentFile.financialProofUrl;
            mystudent.matricResultUrl = studentFile.matricResultUrl;
            return View(mystudent);
        }
      [HttpPost]
      [Authorize]
        public IActionResult ApplicationStudent( StudentModel mystudent)
        {
            return View();
        }
        [Authorize]
        public IActionResult StudentApplication(string StudentEmail) {
            return View();
        }
        [Authorize]
        public IActionResult NewPayments() {
            List<PaymentModel> allStudent = DbHelper.getAllNewStudentsPayments(_configuration);
            ViewBag.StudentPaymentslist = allStudent;
            return View();
        }
    }
}
