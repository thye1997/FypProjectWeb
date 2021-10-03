using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using FypProject.Config;
using FypProject.CustomException;
using FypProject.Models;
using FypProject.Models.DBContext;
using FypProject.Repository;
using FypProject.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace FypProject.Base
{
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public abstract class BaseController<T,VM> : Controller
        where T: class, new()
        where VM:  ListViewModel<T>, new()
    {
       /* protected abstract string pageName { set; get; }

        protected  IActionResult Index()
        {
            return View(pageName);
        }
        protected  VM getList(IGenericRepository<T> repository)
        {
            VM obj = new VM();
            obj.DataList = (List<T>)repository.List();
            return obj;
        }
      
        protected void dataLoad(ref string start, ref string length, ref int pageSize, ref int skip)
        {
            // Skip number of Rows count  
             start = Request.Form["start"].FirstOrDefault();
            // Paging Length 10,20  
             length = Request.Form["length"].FirstOrDefault();
            //Paging Size (10, 20, 50,100)  
             pageSize = length != null ? Convert.ToInt32(length) : 0;

             skip = start != null ? Convert.ToInt32(start) : 0;

        }

        /*public  IGenericRepository<DB> GetRepo<DB>() where DB : class, IBusinessEntity, new()
        {
            var services = this.HttpContext.RequestServices;
            var dbContext = (AppDbContext)services.GetService(typeof(AppDbContext));
            IGenericRepository<DB> store = new GenericRepository<DB>(dbContext);
            return store;
        }
        private JsonResult SetError(Exception ex)
        {
            return ex is BusinessException? Json(new { res = SystemData.ResponseStatus.Exist, msg = ex.Message })
                                           : Json(new { res = SystemData.ResponseStatus.Error, msg = ex.Message });
        }       
        protected  JsonResult SetMessage(int res=0, string msg="", object data = null, Exception ex =null)
        {
            if (ex == null && data !=null)  //for response type that contain data
            {
                return Json(new { res = res, msg = msg , data});
            }
            if(ex == null && data == null) // for response type that only contain success message
            {
                return Json(new { res = res, msg = msg });
            }
            return SetError(ex); // for response that contain error
        }*/
    }
}
