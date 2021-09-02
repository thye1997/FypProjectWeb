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
            string start = null;
            string length = null;
            int pageSize = 0, skip = 0;
            List<SysUserCustomData> customData = new List<SysUserCustomData>();
            try
            {
                var draw = HttpContext.Request.Form["draw"].FirstOrDefault();
              
                int recordsTotal = 0;

                // getting all Customer data  
                var customerData = _systemUserRepository.List().ToList();
                List<int> countId = new List<int>();
                int count = 1;
                foreach (var cD in customerData)
                {
                    customData.Add(new SysUserCustomData { customId = count, Id = cD.Id, userName = cD.userName, createdBy = cD.createdBy, createdOn = cD.createdOn });
                    count++;
                }
                base.dataLoad(ref start, ref length, ref pageSize, ref skip);
                //total number of rows counts   
                recordsTotal = customerData.Count;
                //Paging 

                //Returning Json Data  
                var data = customData.Skip(skip).Take(pageSize).ToList();
                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });

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
    class SysUserCustomData
    {
        public int customId { set; get; }
        public int Id { set; get; }
        public string userName { set; get; }
        public string createdOn { set; get; }
        public string createdBy { set; get; }

    }
}
