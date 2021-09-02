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
        private readonly UserService userService;
        private readonly MedicalHistoryService medicalHistoryService;
        protected override string pageName { get; set; } = SystemData.View.UserIndex;

        public UserController(IUserRepository userRepository,  UserService userService,
            MedicalHistoryService medicalHistoryService)
        {
            this.userService = userService;
            _userRepository = userRepository;
            this.medicalHistoryService = medicalHistoryService;
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
        [HttpPost]
        public JsonResult AddUser(User user)
        {
            try
            {
                userService.AddPatient(user);
                return SetMessage(SystemData.ResponseStatus.Success, "Profile added successfully.");
            }
            catch (Exception ex)
            {
               return SetError(ex);
            }           
        }
        //done
        [HttpPost]
        public JsonResult AddMedHistory(UserViewModel obj)
        {
            try
            {
                userService.AddMedicalHistory(obj);
                return SetMessage(SystemData.ResponseStatus.Success, "Medical history added successfully.");
            }
            catch (Exception ex)
            {
                return SetError(ex);
            }
        }
        //done
        [HttpPost]
        public JsonResult UpdateDetail(User user)
        {
            try
            {
             var result =  userService.UpdateUserDetail(user);
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
            return View(userService.UserDetail(id));
        }

        [HttpPost] 
        public JsonResult getMedHistory(int Id)
        {
            string start = null;
            string length = null;
            int pageSize = 0, skip = 0;
            List<MedHistoryCustomData> customData = new List<MedHistoryCustomData>();
            try
            {
                var draw = HttpContext.Request.Form["draw"].FirstOrDefault();

                int recordsTotal = 0;

                // getting all Customer data  
                //var customerData = (List<MedicalHistory>)_medHistoryRepository.getList(Id);
                var customerData = medicalHistoryService.RetrieveMedicalHistoryList(Id);
                List<int> countId = new List<int>();

                    recordsTotal = customerData.Count;
                base.dataLoad(ref start, ref length, ref pageSize, ref skip);
                //total number of rows counts   
                //Paging 

                //Returning Json Data  
                var data = customerData.Skip(skip).Take(pageSize).ToList();
                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });

            }
            catch (Exception e)
            {
                Debug.Write($"{e}");

                return SetError(e);
            }
        }
        [HttpPost]
        public JsonResult LoadData()
        {
            string start = null;
            string length = null;
            int pageSize = 0, skip = 0;
            List<PatientUserCustomData> customData = new List<PatientUserCustomData>();
            try
            {
                var draw = HttpContext.Request.Form["draw"].FirstOrDefault();

                int recordsTotal = 0;
                // Search Value from (Search box)  
                var searchValue = Request.Form["search[value]"].FirstOrDefault().ToLower();
                Debug.Write($"{searchValue}");

                // getting all Customer data  
                var customerData = _userRepository.List().ToList();
                List<int> countId = new List<int>();
                int count = 1;
                if (!string.IsNullOrEmpty(searchValue))
                {
                    var searchData = (List<User>)_userRepository.GetUserListBySearch(searchValue);
                    foreach (var cD in searchData)
                    {
                        customData.Add(new PatientUserCustomData { customId = count, Id = cD.Id, fullName = cD.FullName, NRIC = cD.NRIC, Gender = cD.Gender, phoneNumber = cD.PhoneNumber });
                        count++;
                    }
                    recordsTotal = searchData.Count;
                }
                else {
                    foreach (var cD in customerData)
                    {
                        customData.Add(new PatientUserCustomData { customId = count, Id = cD.Id, fullName = cD.FullName, NRIC = cD.NRIC, Gender = cD.Gender, phoneNumber = cD.PhoneNumber });
                        count++;
                    }
                    recordsTotal = customerData.Count;
                }

                base.dataLoad(ref start, ref length, ref pageSize, ref skip);
                //total number of rows counts   
                //Paging 

                //Returning Json Data  
                var data = customData.Skip(skip).Take(pageSize).ToList();
                return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });

            }
            catch (Exception e)
            {
                Debug.Write($"{e}");

                return SetError(e);
            }

        }
        [HttpPost]
        public JsonResult GetUserList()
        {
            try
            {
                var result = userService.GetUserList();
                return SetMessage(SystemData.ResponseStatus.Success, data: new { userList = result });
            }
            catch(Exception ex)
            {
               return SetError(ex);
            }
        }
    }
    class PatientUserCustomData
    {
        public int customId { set; get; }
        public int Id { set; get; }
        public string fullName { set; get; }
        public string NRIC { set; get; }
        public string Gender { set; get; }
        public string phoneNumber { set; get; }

    }
    class MedHistoryCustomData
    {
        public int customId { set; get; }
        public int Id { set; get; }
        public string Description { set; get; }
    }
}
