using InternetExplores.Helpers;
using InternetExplores.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace InternetExplores.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private readonly AppDBContext _dbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public HomeController(ILogger<HomeController> logger, IConfiguration configuration, AppDBContext dbContext, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _configuration = configuration;
            _dbContext = dbContext;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            ViewBag.StudentCount= DbHelper.ActiveStudents(_configuration);
            TimerViewModel model = new TimerViewModel();
            TempData["EndTime"] = ViewData["EndTime"];
            if (User.Identity.IsAuthenticated) { 
             ViewBag.ApplicationStatus =  DbHelper.GetAllStudent(_configuration, User.Identity.Name.ToString()).ApplicationStatus;
            
            string regStatus  = DbHelper.GetAllStudent(_configuration, User.Identity.Name.ToString()).registeredStatus;
                if (regStatus != null)
                {
                    ViewBag.RegiStatus = regStatus;
                }
                else {
                    ViewBag.RegiStatus = "None";
                }
        
            }
           
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
    
        public ActionResult About(bool isSuccess = false)
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.ApplicationStatus = DbHelper.GetAllStudent(_configuration, User.Identity.Name.ToString()).ApplicationStatus;

                string regStatus = DbHelper.GetAllStudent(_configuration, User.Identity.Name.ToString()).registeredStatus;
                if (regStatus != null)
                {
                    ViewBag.RegiStatus = regStatus;
                }
                else
                {
                    ViewBag.RegiStatus = "None";
                }

            }
            ViewBag.IsSuccess = isSuccess;
            return View();
        }
        [HttpPost]
        public ActionResult About(AboutModel aboutmyModel )
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.ApplicationStatus = DbHelper.GetAllStudent(_configuration, User.Identity.Name.ToString()).ApplicationStatus;

                string regStatus = DbHelper.GetAllStudent(_configuration, User.Identity.Name.ToString()).registeredStatus;
                if (regStatus != null)
                {
                    ViewBag.RegiStatus = regStatus;
                }
                else
                {
                    ViewBag.RegiStatus = "None";
                }

            }
            try
                {
                           var subject = "Message Form About Page Email : "+aboutmyModel.senderEmail;
                           var body = aboutmyModel.message;
                           body += "<br/><a href = 'mailto:" + aboutmyModel.senderEmail + "'>Send Email</a>";
                           var senderEmail = new MailAddress("internetexploresuniversity@gmail.com", "Internet Explorers ");
                           var receiverEmail = new MailAddress("internetexploresuniversity@gmail.com", "Internet Explores");
                           var password = "InternetExplores@University";
                            var smtp = new SmtpClient
                        {
                            Host = "smtp.gmail.com",
                            Port = 587,
                            EnableSsl = true,
                            DeliveryMethod = SmtpDeliveryMethod.Network,
                            UseDefaultCredentials = false,
                            Credentials = new NetworkCredential(senderEmail.Address, password)
                        };
                        using (var mess = new MailMessage(senderEmail, receiverEmail)
                        {
                            Subject = subject,
                            Body = body,
                            IsBodyHtml = true
                        })
                        {
                            smtp.Send(mess);
                        }
                return RedirectToAction(nameof(About), new { isSuccess = true });

            }
                catch (Exception)
                {
                    ViewBag.Error = "Some Error";  
                return View();
                }
              

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
