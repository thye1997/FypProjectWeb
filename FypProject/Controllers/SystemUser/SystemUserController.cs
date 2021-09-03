using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Helpers;
using FypProject.Base;
using FypProject.Config;
using FypProject.Models;
using FypProject.ViewModel;
using Microsoft.AspNetCore.Mvc;
using BCrypt.Net;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;
using FypProject.CustomException;
using FypProject.Repository;


namespace FypProject.Controllers
{
    [Authorize(AuthenticationSchemes = authenticationSchemes , Roles =SystemData.Role.Admin)]
    public class SystemUserController : BasicController
    {
        private readonly IGenericRepository<SystemUser> _systemUserRepository;

        protected override string pageName { get; set; } = SystemData.View.SystemUserIndex;

        public SystemUserController(IGenericRepository<SystemUser> systemUserRepository)
        {
            _systemUserRepository = systemUserRepository;
        }
        public IActionResult Index()
        {
            return base.Index();
        }
        [HttpPost]
        public JsonResult AddSystemUser(SystemUser systemUser)
        {
            try
            {
                var sysUser = _systemUserRepository.List().Where(c => c.userName == systemUser.userName).FirstOrDefault();
                if (sysUser != null) throw new BusinessException("Duplicate user name found");
                systemUser.createdBy = User.Identity.Name;
                systemUser.Password = BCrypt.Net.BCrypt.HashPassword(systemUser.Password);
                _systemUserRepository.Add(systemUser);
                return SetMessage(SystemData.ResponseStatus.Success, "User added successfully.");
            }
            catch (Exception ex)
            {
                return SetError(ex: ex);
            }
        }

        [HttpPost]
        public JsonResult LoadData()
        {
            try
            {
                var dataList = _systemUserRepository.List().ToList();
                return this.DataTableResult(dict, dataList);
                //yea boi cleaner
            }
            catch (Exception e)
            {
                return SetError(e);
            }

        }
        [HttpPost]
        public JsonResult DeleteSystemUser(int Id)
        {
            try
            {
                if (Id <= 0) throw new BusinessException("Invalid user id");
                var sysUser = _systemUserRepository.List().Where(c => c.Id == Id).FirstOrDefault();
                _systemUserRepository.Delete(Id);
                return SetMessage(SystemData.ResponseStatus.Success, "User deleted successfully.");
            }
            catch(Exception ex)
            {
               return SetError(ex);
            }

        }
    }

    /* return this.DataTableResult(dict, customerData,
       (obj) =>
       {    
           foreach(var n in obj)
           {
              // n.Id = count;

              count++;
           }
       }
      );*/
    /* return this.DataTableResult(dict, customerData,
         obj =>
         {
              count = 0;
             foreach(var n in customerData)
             {
                 obj.Add(new
                 {
                     customId = count++,
                     Id = n.Id,
                     userName = n.userName,
                     createdBy = n.createdBy,
                     createdOn = n.createdOn                                
               });
             }
         },

         true);*/
   /* class SysUserCustomData : SystemUser
    {
        public int customId { set; get; }
    }*/
}
