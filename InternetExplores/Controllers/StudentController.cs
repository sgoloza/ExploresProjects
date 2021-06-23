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
using System.Net.Mail;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InternetExplores.Controllers
{
    public class StudentController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly AppDBContext _dbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private List<PaymentModel> studentpayments = new List<PaymentModel>();

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
         [Authorize]
        public  IActionResult Registration(bool isSuccess = false) {
            ViewBag.IsSuccess = isSuccess;
            return View();
        }
        [HttpPost]
        [Authorize]
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
                  
                if (myStudent.financialProof != null)
                {
                   folder = "Documents/FinancialProof/";
                   myStudent.financialProofUrl = await UploadImage(folder, myStudent.idcopy);
                   
                }
                   
            }
                i = DbHelper.insertSudentsDocuments(_configuration, myStudent);
            myStudent.StudentDegree = myStudent.StudentFaculty;
                j = DbHelper.UpdatetudentDetails(_configuration, myStudent);
                DbHelper.SendEmails("Registration", DbHelper.GetAllStudent(_configuration, User.Identity.Name.ToString()));
            if ( j > 0)
                {
                    return RedirectToAction(nameof(Registration), new { isSuccess = true });
                }
            //}
                return View();
        }
        [Authorize]
        public IActionResult EditProfile()
        {
            return View();
        }
        public IActionResult Profile(bool isdone = false)
        {
            //DbHelper.SendEmails("Registration", DbHelper.GetAllStudent(_configuration, "kwaneleluthan@gmail.com"));
            

            StudentModel mystudent = DbHelper.GetAllStudent(_configuration, User.Identity.Name.ToString());

            StudentFiles studentFile = DbHelper.GetAllStudentDocuments(_configuration, mystudent.StudentNo);
            string url = studentFile.idcopyUrl;
            if (url != null)
            {
                mystudent.nextofKinUrl = studentFile.nextofKinUrl;
                mystudent.idcopyUrl = studentFile.idcopyUrl;
                mystudent.financialProofUrl = studentFile.financialProofUrl;
                mystudent.matricResultUrl = studentFile.matricResultUrl;
                ViewBag.document = "not null";
            }
            else {
                ViewBag.document = "null";
            }
           

            List<PaymentModel> studentpayment = DbHelper.getStudentsPayments(_configuration, mystudent.StudentNo);
            ViewBag.PaymentCount = studentpayment.Count;
            if (ViewBag.PaymentCount > 0) {
               
            }
            ViewBag.StudentPayments = studentpayment;

            List<string> modulescodes = DbHelper.enrolled(_configuration, mystudent.StudentNo);

            List<ModuleModel> listOfmodules = new List<ModuleModel>();
            double total = 0.0;
            int credits = 0;
            foreach (string code in modulescodes)
            {
                listOfmodules.Add(DbHelper.getModule(_configuration, code));
            }
            
            foreach (ModuleModel modulesname in listOfmodules)
            {
                 total += Double.Parse(modulesname.ModuleCost.ToString());
                 credits += modulesname.ModuleCredit;
            }
            ViewBag.StudentEnrolledModiles = listOfmodules;
            ViewBag.moduleCount = listOfmodules.Count;
            ViewBag.Total = total;
            ViewBag.Credits = credits;
            ViewBag.Udone = isdone;
            return View(mystudent);
        }
        [HttpPost]
        public IActionResult Profile( StudentModel mystudent)
        {
           int  j = DbHelper.UpdatetudentProfile(_configuration, mystudent);

            return RedirectToAction(nameof(Profile), new { isdone = true });
        }
        private async Task<string> UploadImage(string folderPath, IFormFile file)
            {

                folderPath += Guid.NewGuid().ToString() + "_" + file.FileName;

                string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folderPath);

                await file.CopyToAsync(new FileStream(serverFolder, FileMode.Create));

                return "/" + folderPath;
            }


         [Authorize]
        public ActionResult MakePayment(bool isSuccess = false)
        {
        

            ViewBag.IsSuccess = isSuccess;
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> MakePayment( PaymentModel payment)
        {
            try { 
                    StudentModel mystudent = DbHelper.GetAllStudent(_configuration, User.Identity.Name.ToString());
                    payment.StudentNo = mystudent.StudentNo;
                    if (payment.paymentProof != null)
                    {
                        string folder = "Documents/Payments/";
                        payment.paymentProofUrl = await UploadImage(folder, payment.paymentProof);

                    }
                   int j =  DbHelper.InsertStudentpayment(_configuration, payment);
                DbHelper.SendEmails("Payment", DbHelper.GetAllStudent(_configuration, User.Identity.Name.ToString()), payment.paymentDescription);
                ViewBag.Error = false;
                return RedirectToAction(nameof(MakePayment), new { isSuccess = true });

            } catch (Exception) {
                ViewBag.Error = true;
                return View();
            }

         
          
           
        }
        [HttpGet]
        [Authorize]
        public ActionResult StudentEnrollement(bool isSuccess = false , int p = 1) {
            ViewBag.IsSuccess = isSuccess;
            
            StudentModel mystudent = DbHelper.GetAllStudent(_configuration, User.Identity.Name.ToString());

            StudentFiles studentFile = DbHelper.GetAllStudentDocuments(_configuration, mystudent.StudentNo);
            mystudent.nextofKinUrl = studentFile.nextofKinUrl;
            mystudent.idcopyUrl = studentFile.idcopyUrl;
            mystudent.financialProofUrl = studentFile.financialProofUrl;
            mystudent.matricResultUrl = studentFile.matricResultUrl;

            // List<ModuleModel> studentpayment = DbHelper.getStudentsPayments(_configuration, mystudent.StudentNo);
            //  ViewBag.PaymentCount = studentpayment.Count;
            List<SelectListItem> studentmodulelist = new List<SelectListItem>();
            foreach (var mo in DbHelper.GetModulelist(_configuration, mystudent)) {
                studentmodulelist.Add( new SelectListItem() { Text = mo.Modulename.ToString(), Value = mo.ModuleCode.ToString() } );
            }
            if (isSuccess == false && studentmodulelist.Count > 0 && p == 2)
            {
                ModelState.AddModelError(string.Empty, "Please Make Sure to Select different Module");
                ModelState.AddModelError(string.Empty, "Example :");
                foreach (var mo in DbHelper.GetModulelist(_configuration, mystudent))
                {
                    ModelState.AddModelError(string.Empty, ">"+mo.Modulename);
                }
            }
            else {
                ModelState.AddModelError(string.Empty, "Please Select different Module");
            }

           EnrollmentsModel myEnroll = new EnrollmentsModel();
            myEnroll.ModulesList = studentmodulelist;
            ViewBag.Modulelist = studentmodulelist;
           /* myEnroll.ModulesList =
                DbHelper.GetModulelist(_configuration, mystudent).Select(x => new ModuleModel { ModuleCode = x.ModuleCode,
                ModuleCost = x.ModuleCost , Modulename = x.Modulename , ModuleDescription = x.ModuleDescription, ModuleCredit = x.ModuleCredit }).ToList();
            List<string> list = new List<string>();*/
            return View(myEnroll);
        }
        [HttpPost]
        [Authorize]
        public ActionResult StudentEnrollement( EnrollmentsModel enrollments)
        {
            string module0 = enrollments.ModuleCode0;
            string module1 = enrollments.ModuleCode1;
            string module2 = enrollments.ModuleCode2;
            string module3 = enrollments.ModuleCode3;

            string[] myarr = { module0, module1, module2, module3 };
            List<string> mylist = new List<string>();
            foreach ( string str in myarr)
            {
                if (str != null)
                    mylist.Add(str);

            }
            var d = mylist.Distinct().Count();

            if (d == 3 || d == 4 ) {
                double studentBalance = 0.0;
                StudentModel mystudent = DbHelper.GetAllStudent(_configuration, User.Identity.Name.ToString());
                enrollments.StudentNo = mystudent.StudentNo;
                foreach ( string code in mylist) {
                    studentBalance += Double.Parse( DbHelper.getModule(_configuration, code).ModuleCost.ToString());
                }
                DbHelper.StudentEnrollemt(_configuration, enrollments);
                DbHelper.setStudentBalance(_configuration, studentBalance, enrollments.StudentNo);
                return RedirectToAction(nameof(StudentEnrollement), new { isSuccess = true });
            } else
            {
                return RedirectToAction(nameof(StudentEnrollement), new { isSuccess = false, p = 2 });
            }
        }

    }
}
