using InternetExplores.Helpers;
using InternetExplores.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        public AdminController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration configuration, AppDBContext dbContext, IWebHostEnvironment webHostEnvironment)
        {
            _userManager = userManager;
            _signInManager = signInManager;
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
        [Authorize]
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
        public IActionResult StudentsList(bool isSuccess = false)
        {
            List<StudentModel> allStudents = new List<StudentModel>();
            allStudents = DbHelper.SelectAllStudents(_configuration, allStudents);
            ViewBag.IsSuccess = isSuccess;
            return View(allStudents);
        }
        [Authorize]
        [HttpGet]
        public IActionResult ApplicationStudent(string StudentEmail, StudentAppModel stuu)
        {
            StudentModel mystudent = new StudentModel();
            StudentFiles studentFile = new StudentFiles();

            string descrip = stuu.comments;

            if (descrip != null )
            {
                var i = DbHelper.UpdateStudentStatus(_configuration, stuu);

                DbHelper.SendEmails("Status", DbHelper.GetAllStudent(_configuration, stuu.Email), stuu.comments);
                ViewBag.StudentStatusSucess = true;
                return RedirectToAction(nameof(StudentsList), new { isSuccess = true });
            }
            else
            {

                mystudent = DbHelper.GetAllStudent(_configuration, StudentEmail);
                studentFile = DbHelper.GetAllStudentDocuments(_configuration, mystudent.StudentNo);
                mystudent.nextofKinUrl = studentFile.nextofKinUrl;
                mystudent.idcopyUrl = studentFile.idcopyUrl;
                mystudent.financialProofUrl = studentFile.financialProofUrl;
                mystudent.matricResultUrl = studentFile.matricResultUrl;
                ViewBag.StudentDetails = mystudent;
                return View();
            }
          
        }
      [HttpPost]
      [Authorize]
        public IActionResult ApplicationStudent( StudentAppModel mystudent)
        {
            return View();
        }
        [Authorize]
        public IActionResult ModuleList(bool Success = false) {
            ViewBag.lSuccess = Success;
            ViewBag.listOfModules = DbHelper.allmodules( _configuration);
            return View();
        }
        public IActionResult Faculties() {

            return View();
        }
        [Authorize]
        public IActionResult NewPayments( int payentid, int studentNO, bool isSucess  = false ) {
            List<PaymentModel> allStudentpayments = DbHelper.getAllNewStudentsPayments(_configuration);
            if (payentid != 0 & studentNO != 0) {
                double amount = 0.0;
                foreach (PaymentModel py in allStudentpayments)
                {
                    if (py.paymentID == payentid) {
                        amount = Double.Parse(py.paymentAmount.ToString());
                    }

                }
                ViewBag.IsSuccess = true;
                DbHelper.setStudentBalance(_configuration, amount, studentNO);
                DbHelper.updateStudentPaymentStatus(_configuration, payentid);
                return RedirectToAction(nameof(NewPayments), new { isSuccess = true });
                
            }
            
            ViewBag.StudentPaymentslist = allStudentpayments;
            return View();
        }
        [Authorize]
        public IActionResult ModuleDetails( string ModuleCode ,ModuleDeatilsModel mydetails)
        { 
            
            
            List<ModuleModel> module = new List<ModuleModel>();
            module.Add(DbHelper.getModule(_configuration, ModuleCode));
            string descr = DbHelper.getModule(_configuration, ModuleCode).ModuleDescription;

            ViewBag.ModuleDetails = DbHelper.getModule(_configuration, ModuleCode);

            string myModuleCode = mydetails.ModuleCode;
            string myModuleDescription = mydetails.ModuleDescription;
            decimal myModuleCost = mydetails.ModuleCost;
            int myModuleCredit = mydetails.ModuleCredit; 
            string myModulePre_requisites = mydetails.ModulePre_requisites;
            string myModulesStatus = mydetails.ModulesStatus;

            if (myModulePre_requisites !=null ) {

                if (myModuleDescription != null)
                {

                    DbHelper.UpdatesModuleDetails(_configuration, mydetails);
                }
                else
                {
                    mydetails.ModuleDescription = descr;
                    DbHelper.UpdatesModuleDetails(_configuration, mydetails);
                }
                return RedirectToAction(nameof(ModuleList), new { Success = true });
               
            }
            return View();
        }
        [Authorize]
        public IActionResult NewModule( ModuleModel mymodel , bool sucess = false) {


            if (mymodel.ModuleDescription != null) { 
                   DbHelper.InsertModulesDetails(_configuration, mymodel);
                return RedirectToAction(nameof(NewModule), new { sucess = true });
            }
            ViewBag.MSucess = sucess;
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> AdminRegister( bool isdone = false )
        {
            /*if (model.AdminName != null) {

                if (ModelState.IsValid)
                {
                    var user = new IdentityUser
                    {
                        UserName = model.AdminEmail,
                        Email = model.AdminEmail,
                    };

                    var result = await _userManager.CreateAsync(user, model.password);

                    if (result.Succeeded)
                    {
                        DbHelper.RegistrationOfAdmin(_configuration, model);
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return RedirectToAction("index", "Admin");
                    }
                    ModelState.AddModelError(string.Empty, "Registering failed");
                    ViewBag.regError = true;
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
                return View(model);
            }
            */
            ViewBag.done = isdone;
            return View();
        }

            public async Task<IActionResult> AdminRegister(AdminModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    UserName = model.AdminEmail,
                    Email = model.AdminEmail,
                };

                var result = await _userManager.CreateAsync(user, model.password);

                if (result.Succeeded)
                {

                    DbHelper.RegistrationOfAdmin(_configuration, model);
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("AdminRegister", "Admin");
                }
                ModelState.AddModelError(string.Empty, "Registering failed");
                ViewBag.regError = true;
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                ViewBag.regError = true;

            }
            return View(model);
        }
    }
}
