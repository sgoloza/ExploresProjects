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
        
        public  IActionResult Registration(bool isSuccess = false) {

            ViewBag.IsSuccess = isSuccess;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Registration(StudentModel myStudent)
        {
            StudentModel student = DbHelper.GetAllStudent(_configuration, User.Identity.Name.ToString());
            myStudent.StudentNo = student.StudentNo;
            //if (ModelState.IsValid)
           // {
                int i;
                int j;
                if (myStudent.idcopy != null && myStudent.matricResult != null && myStudent.nextofKin != null)
                {
                    string folder = "Documents/ID/";
                    myStudent.idcopyUrl = await UploadImage(folder, myStudent.idcopy);
                    

                    folder = "Documents/Matric/";
                    myStudent.matricResultUrl = await UploadImage(folder, myStudent.idcopy);
                    

                   folder = "Documents/Kin/";
                   myStudent.nextofKinUrl = await UploadImage(folder, myStudent.idcopy);
                  
                if (myStudent.financialProofUrl != null)
                {
                   folder = "Documents/FinancialProof/";
                   myStudent.financialProofUrl = await UploadImage(folder, myStudent.idcopy);
                   
                }
                   
            }
                i = DbHelper.insertSudentsDocuments(_configuration, myStudent);
                j = DbHelper.UpdatetudentDetails(_configuration, myStudent);
                if (i > 0  && j > 0)
                {
                    return RedirectToAction(nameof(Registration), new { isSuccess = true });
                }
            //}
                return View();
        }
            public IActionResult Profile()
        {
            StudentModel mystudent = DbHelper.GetAllStudent(_configuration, User.Identity.Name.ToString());
            StudentFiles studentFile = DbHelper.GetAllStudentDocuments(_configuration, mystudent.StudentNo);
            mystudent.nextofKinUrl = studentFile.nextofKinUrl;
            mystudent.idcopyUrl = studentFile.idcopyUrl;
            mystudent.financialProofUrl = studentFile.financialProofUrl;
            mystudent.matricResultUrl = studentFile.matricResultUrl;

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
