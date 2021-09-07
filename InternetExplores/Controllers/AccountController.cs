using InternetExplores.Helpers;
using InternetExplores.Models;
using InternetExplores.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternetExplores.Controllers
{
    public class AccountController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly AppDBContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private string IdentityNumber;
        private DateTime BirthDate;
        public AccountController(RoleManager<IdentityRole> roleManager , UserManager<IdentityUser> userManager,SignInManager<IdentityUser> signInManager,IConfiguration configuration, AppDBContext dbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _dbContext = dbContext;
            _roleManager = roleManager;
        } 

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
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
            return View();
        }

        private bool MyIdChecker(string identityNumber)
        {
            try { 
             bool IsValid = false;
            this.IdentityNumber = (identityNumber ?? string.Empty).Replace(" ", "");
            if (this.IdentityNumber.Length == 13)
            {
                var digits = new int[13];
                for (int i = 0; i < 13; i++)
                {
                    digits[i] = int.Parse(this.IdentityNumber.Substring(i, 1));
                }
                int control1 = digits.Where((v, i) => i % 2 == 0 && i < 12).Sum();
                string second = string.Empty;
                digits.Where((v, i) => i % 2 != 0 && i < 12).ToList().ForEach(v =>
                      second += v.ToString());
                var string2 = (int.Parse(second) * 2).ToString();
                int control2 = 0;
                for (int i = 0; i < string2.Length; i++)
                {
                    control2 += int.Parse(string2.Substring(i, 1));
                }
                int control = (10 - ((control1 + control2) % 10)) % 10;
                if (digits[12] == control)
                {

                    this.BirthDate = DateTime.ParseExact(this.IdentityNumber.Substring(0, 6), "yyMMdd", null);

                    IsValid = true;
                }
                else
                {
                    IsValid = false;
                }

            }
            return IsValid;
            } catch (Exception) {
                return false;
            }
           
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
           

                    if (MyIdChecker(model.StudentIdNo))
                    {
                        if (ModelState.IsValid)
                        {
               
                            var user = new IdentityUser
                            {
                                UserName = model.Email,
                                Email = model.Email,
                            };

                            var result = await _userManager.CreateAsync(user, model.Password);

                            if (result.Succeeded)
                            {

                                    model.StudentDateOfBirth = BirthDate;
                                    DbHelper.RegistrationOfStudent(_configuration, model);

                                    await _signInManager.SignInAsync(user, isPersistent: false);
                                    await _userManager.AddToRoleAsync(user, "Student");
                                    var results = await _signInManager.PasswordSignInAsync(model.Email, model.Password,true, false);
                     
                                        ViewBag.ApplicationStatus = DbHelper.GetAllStudent(_configuration, model.Email).ApplicationStatus;

                                        string regStatus = DbHelper.GetAllStudent(_configuration, model.Email).registeredStatus;
                                        if (regStatus != null)
                                        {
                                            ViewBag.RegiStatus = regStatus;
                                        }
                                        else
                                        {
                                            ViewBag.RegiStatus = "None";
                                        }

                        
                                    return RedirectToAction("index", "Home");
                  
                     
                            }
                            ViewBag.regError = true;
                            foreach (var error in result.Errors)
                            {
                                ModelState.AddModelError("", error.Description);
                            }

                        }
                        ModelState.AddModelError(string.Empty, "Registering failed");
                        ModelState.AddModelError(string.Empty, "Make sure You provide valid inputs");
                        ViewBag.regError = true;
                     }
                    else
                    {
                        ModelState.Clear();
                        ModelState.AddModelError(string.Empty, "Invalid Id number");
                        ModelState.AddModelError(string.Empty, "Entered Id number does not exist");
                        ViewBag.regError = true;
                        return View(model);
                    }
            return View(model);
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel user)
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
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(user.Email, user.Password, user.RememberMe, false);
                // User.Identity.IsAuthenticated
               
                if (result.Succeeded)
                {
                    StudentModel mystudent = DbHelper.GetAllStudent(_configuration, user.Email.ToString());
                    if (mystudent.StudentEmail == null)
                    {
                        
                        return RedirectToAction("Index", "Admin");
                    }
                    else if (mystudent.StudentEmail == user.Email)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else {
                        return RedirectToAction("Login");
                    }
                   
                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");

            }
            ViewBag.myError = true;
            return View(user);
        }
        public async Task<IActionResult> Logout()
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
            await _signInManager.SignOutAsync();

            return RedirectToAction("Login");
        }
    }
  
}

