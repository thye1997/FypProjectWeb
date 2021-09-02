using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FypProject.Models;
using FypProject.Repository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FypProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly IGenericRepository<SystemUser> sysUserRepository;
        public AccountController(IGenericRepository<SystemUser> sysUserRepository)
        {
            this.sysUserRepository = sysUserRepository;
        }
        public IActionResult Login()
        {
            //if (User.Identity.IsAuthenticated)
            //{
            //    return RedirectToAction("Index", "Dashboard");
            //}
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> LoginUser(Login model, [FromQuery] string ReturnUrl)
        {

            if (ModelState.IsValid)
            {

                var sysUser = new SystemUser
                {
                    userName = model.UserName,
                    Password = model.Password
                };
                var user = sysUserRepository.List().Where(c => c.userName == model.UserName).FirstOrDefault();
                if (user != null)
                {
                    if (BCrypt.Net.BCrypt.Verify(model.Password, user.Password))
                    {

                        var claims = new List<Claim>
                        {
                        new Claim(ClaimTypes.Name,user.userName),
                        new Claim("FullName", user.Name),
                        new Claim("Id", user.Id.ToString()),
                        new Claim(ClaimTypes.Role, user.Role),
                        };

                        var claimsIdentity = new ClaimsIdentity(
                            claims, CookieAuthenticationDefaults.AuthenticationScheme);

                        var authProperties = new AuthenticationProperties
                        {

                            RedirectUri = "/Dashboard/Index",

                        };
                        await HttpContext.SignInAsync(
                                CookieAuthenticationDefaults.AuthenticationScheme,
                                new ClaimsPrincipal(claimsIdentity),
                                authProperties);
                        /*if (!string.IsNullOrEmpty(ReturnUrl))
                        {

                            if (Url.IsLocalUrl(ReturnUrl))
                            {
                                return RedirectToAction("Index", "Dashboard");

                            }
                            else
                            {
                                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                                return RedirectToAction(nameof(AccountController.AccessDenied), "Account");

                            }
                        }*/

                        return RedirectToAction("Index", "Dashboard");
                    }
                    else
                    {
                        ViewBag.loginErr = "invalid";
                        return View("Login");
                    }
                }
                else
                {
                    //if model state is valid but credential not valid, return to login page
                    ViewBag.loginErr = "invalid";
                    return View("Login");
                }
            }
            else
            {   //if model state not valid, return to login page
                return View("Login");
            }

        }

        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
        //[Route("/Account/")] //either match this /Account
        [Route("Error/{statusCode}")] //or match this /Error/{} the "/" before text can be optional, still can be matched without "/" Eg. Error/{statusCode} or /Error/{statusCode}
        public IActionResult AccessDenied(int statusCode) // for error access
        {
            Debug.WriteLine("status code"+ statusCode.ToString());
            return View();
        }
    }
}
