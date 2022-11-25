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


        }

        [HttpPost]
        public IActionResult Register(Register obj){

        }

        public IActionResult SignIn() {

        }

        [HttpPost]
        public IActionResult SignIn(SignInManager obj){

        }
        
        [HttpPost]
        public IActionResult SignOut(){
            
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