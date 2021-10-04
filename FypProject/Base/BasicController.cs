using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FypProject.Config;
using FypProject.CustomException;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace FypProject.Base
{
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public abstract class BasicController : Controller
    {
        protected const string authenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme;
        protected abstract string pageName { set; get; }
        public IDictionary<string, string> dict => HttpContext.Request.Form.ToDictionary(x => x.Key, x => x.Value.ToString());

        protected IActionResult Index()
        {
            return View(pageName);
        }
        protected JsonResult SetError(Exception ex)
        {
            return ex is BusinessException ? Json(new { res = SystemData.ResponseStatus.Exist, msg = ex.Message })
                                           : Json(new { res = SystemData.ResponseStatus.Error, msg = ex.Message });
        }
        protected JsonResult SetMessage(int res = 0, string msg = "", object data = null)
        {
            return Json(new { res = res, msg = msg, data });
        }
    }
}
