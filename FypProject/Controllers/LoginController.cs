using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FypProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FypProject.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        public IActionResult Login(Login login)
        {
            if (ModelState.IsValid)
            {
                return View("../Dashboard/Index");
            }
            return View();
        }
    }
}
