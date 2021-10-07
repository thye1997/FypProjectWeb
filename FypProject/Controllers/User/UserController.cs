using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FypProject.Models;
using System.Text.RegularExpressions;
using FypProject.ViewModel;
using Microsoft.AspNetCore.Authorization;
using FypProject.Base;
using FypProject.Config;
using FypProject.Repository;
using FypProject.Services;
namespace FypProject.Controllers
{
    [Authorize(AuthenticationSchemes = authenticationSchemes)]
    public class UserController : BasicController
    {
        private IUserRepository _userRepository;
        private readonly UserService _userService;
        private readonly MedicalHistoryService _medicalHistoryService;
        protected override string pageName { get; set; } = SystemData.View.UserIndex;

        public UserController(IUserRepository userRepository,  UserService userService,
            MedicalHistoryService medicalHistoryService)
        {
            _userService = userService;
            _userRepository = userRepository;
            _medicalHistoryService = medicalHistoryService;
        }
        public IActionResult Index()
        {
            return base.Index();
            //return View(userViewModel); // correct way to pass data from controller to View
        }
        //[Route("")] if the route is from any of these 2, it will route to the action below
        //[Route("Home/Searches")] if this route is declared, user can navigate to this action by typing Home/Searches
        /*public IActionResult Search(string search) 
        {
           return validateSearch(search);
        }*/

        //done
        public JsonResult AddUser(User user)
        {
            try
            {
                _userService.AddPatient(user);
                return SetMessage(SystemData.ResponseStatus.Success, "Profile added successfully.");
            }
            catch (Exception ex)
            {
               return SetError(ex);
            }           
        }
        //done
        public JsonResult AddMedHistory(UserViewModel obj)
        {
            try
            {
                _userService.AddMedicalHistory(obj);
                return SetMessage(SystemData.ResponseStatus.Success, "Medical history added successfully.");
            }
            catch (Exception ex)
            {
                return SetError(ex);
            }
        }
        //done
        public JsonResult UpdateDetail(User user)
        {
            try
            {
             var result =  _userService.UpdateUserDetail(user);
                return SetMessage(SystemData.ResponseStatus.Success, "Patient details updated successfully.");
            }
            catch(Exception ex)
            {
                return SetError(ex);
            }
        }
        //done
        public IActionResult UserDetail(int id)
        {
            return View(_userService.UserDetail(id));
        }

        public JsonResult GetMedHistory(int Id)
        {
            try
            {
                var dataList = _medicalHistoryService.RetrieveMedicalHistoryListById(Id).AsQueryable();  //TODO: modify to skip() and tak()
                return this.DataTableResult<MedicalHistoryListViewModel,MedicalHistory>(dict, dataList, orderBy:c=> DateTime.Parse(c.Date));
            }
            catch (Exception e)
            {
                Debug.Write($"{e}");

                return SetError(e);
            }
        }
        public JsonResult LoadData()
        {
            try
            {
                var searchValue = Request.Form["search[value]"].FirstOrDefault().ToLower();
                Debug.Write($"{searchValue}");

                var dataList = (IQueryable<User>)null;
                if (!string.IsNullOrEmpty(searchValue))
                {
                     dataList = _userRepository.GetUserListBySearch(searchValue).AsQueryable();
                }
                else {
                    dataList = _userRepository.ToQueryable();
                }

                return this.DataTableResult(dict, dataList);

            }
            catch (Exception e)
            {
                Debug.Write($"{e}");

                return SetError(e);
            }

        }
        public JsonResult GetUserList()
        {
            try
            {
                var result = _userService.GetUserList();
                return SetMessage(SystemData.ResponseStatus.Success, data: new { userList = result });
            }
            catch(Exception ex)
            {
               return SetError(ex);
            }
        }
    }
}
