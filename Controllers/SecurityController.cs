using System.Globalization;
using System.Runtime.CompilerServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using EmployeeManager.Security;
using EmployeeManager.Models;

namespace EmployeeManager.Controllers
{
    [Route("[controller]")]
    public class SecurityController : Controller
    {
        
        private readonly UserManager<AppIdentityUser> userManager;
        private readonly RoleManager<AppIdentityRole> roleManager;
        private readonly SignInManager<AppIdentityUser> signInManager;

        private readonly ILogger<SecurityController> _logger;

        public SecurityController(ILogger<SecurityController> logger)
        {
            _logger = logger;
        }

        public SecurityController(UserManager<AppIdentityUser> userManager,
                                  RoleManager<AppIdentityRole> roleManager,
                                  SignInManager<AppIdentityUser> signInManager) {
                                    this.userManager = userManager;
                                    this.roleManager = roleManager;
                                    this.signInManager= signInManager;
                                  }

        public IActionResult Register(){
            return View();
        }

        [HttpPost]
        public IActionResult Register(Register obj){
            if (ModelState.IsValid) {
                if (!roleManager.RoleExistsAsync("Manager").Result){
                    AppIdentityRole role = new AppIdentityRole();
                    role.Name = "Manager";
                    role.Description = "Can perform CRUD operations.";
                    IdentityResult roleResult = roleManager.CreateAsync(role).Result;
                }
                AppIdentityUser user = new AppIdentityUser();
                user.UserName = obj.UserName;
                user.Email = obj.Email;
                user.FullName = obj.FullName;
                user.BirthDate = obj.BirthDate;

                IdentityResult result = userManager.CreateAsync(user, obj.Password).Result;

                if (result.Succeeded) {
                    userManager.AddToRoleAsync(user, "Manager").Wait();
                    return RedirectToAction("SignIn", "Security");
                }
                else {
                    ModelState.AddModelError("", "Invalid user details!");
                }
            }
            return View(obj);
        }

        public IActionResult SignIn() {

                return View();
        }

        [HttpPost]
        public IActionResult SignIn(SignIn obj){
            if (ModelState.IsValid) {
                var result = signInManager.PasswordSignInAsync(obj.UserName, obj.Password,obj.RememberMe, false).Result;
                if(result.Succeeded) {
                    return RedirectToAction("List", "EmployeeManager");
                }
                else {
                    ModelState.AddModelError("", "Invalid user details!");
                }
            }
            return View(obj);
        }


        [HttpPost]
        public IActionResult SignOut(){
            signInManager.SignOutAsync().Wait();
            return RedirectToAction("SignIn", "Security");
            
        }



        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}