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
    public abstract class BasicController : Controller
    {
        protected const string authenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme;
        protected abstract string pageName { set; get; }
       public IDictionary<string, string> dict => HttpContext.Request.Form.ToDictionary(x => x.Key, x => x.Value.ToString());

        protected IActionResult Index()
        {
            return View(pageName);
        }

       /* protected void dataLoad(ref string start, ref string length, ref int pageSize, ref int skip)
        {
            // Skip number of Rows count  
            start = Request.Form["start"].FirstOrDefault();
            // Paging Length 10,20  
            length = Request.Form["length"].FirstOrDefault();
            //Paging Size (10, 20, 50,100)  
            pageSize = length != null ? Convert.ToInt32(length) : 0;

            skip = start != null ? Convert.ToInt32(start) : 0;

        }*/
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
