using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using FypProject.ApiViewModel;
using FypProject.Services;
using FypProject.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FypProject.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountApiController : ControllerBase
    {
        private readonly AccountService accountService;
    
        public AccountApiController(AccountService accountService)
        {
            this.accountService = accountService;
        }
        [HttpPost]
        public IActionResult Register([FromBody] AccountLoginRequest accountLoginRequest)
        {
            try
            {
                Debug.WriteLine("register account api is called");
                var accountLoginResponse = accountService.AccountRegister(accountLoginRequest);
                return Ok(accountLoginResponse);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        [HttpPost]
        public IActionResult Login([FromBody] AccountLoginRequest accountLoginRequest)
        {
            try
            {
                Debug.WriteLine("login api is called");
               var accountLoginResponse= accountService.AccountLogin(accountLoginRequest);
                return Ok(accountLoginResponse);
            }
            catch(Exception ex)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult DefaultProfile([FromBody] int AccId)
        {
            try
            {
                Debug.WriteLine("Default profile api is called"+ AccId);
                var HasDefault = accountService.CheckDefaultProfile(AccId);
                return Ok(HasDefault);
            }
            catch(Exception ex)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult SearchExistProfile([FromBody] string NRIC)
        {
            try
            {
                Debug.WriteLine("Search Profile api is called "+ NRIC);
                var SearchProfile = accountService.SearchExistProfile(NRIC);
                Debug.WriteLine("searched full name: " + SearchProfile.FullName);
                return Ok(SearchProfile);

            }
            catch (Exception ex)
            {
                Debug.WriteLine("error runned");

                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult SearchProfile([FromBody] string NRIC)
        {
            try
            {
                Debug.WriteLine("Search Profile api is called " + NRIC);
                var SearchProfile = accountService.SearchExistProfile(NRIC);
                Debug.WriteLine("searched full name: " + SearchProfile.FullName);
                return Ok(SearchProfile);

            }
            catch (Exception ex)
            {
                Debug.WriteLine("error runned");

                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult AddProfile([FromBody] AddProfileRequest addProfileRequest)
        {
            try
            {
                Debug.WriteLine("add profile api called "+addProfileRequest.ProfileId +" "+addProfileRequest.AccId);
                var result = accountService.AddProfile(addProfileRequest);
                return Ok(result);

            }catch(Exception ex)
            {
                Debug.WriteLine("error runned");

                return NotFound();
            }
        }
        [HttpPost]
        public IActionResult AddNewProfile([FromBody] AddProfileRequest addNewProfileRequest)
        {
            try
            {
                Debug.WriteLine("add new profile  api is called");
                var accountLoginResponse = accountService.AddNewProfile(addNewProfileRequest);
                return Ok(accountLoginResponse);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult GetNotificationPrefs([FromBody] int accId)
        {
            Debug.WriteLine("Notification prefs settings called");
            try
            {
                var result = accountService.NotifcationPrefsSettings(accId);
                return Ok(result);

            }catch(Exception ex)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult NotificationPrefsUpdate([FromBody] UpdateNotificationPrefsRequest obj)
        {
            Debug.WriteLine("Notification prefs settings update called");

            try
            {
                var result = accountService.UpdateNotificationPreferences(obj);
                return Ok(result);
            }catch(Exception ex)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult UpdateFirebaseToken(UpdateFirebaseTokenRequest updateFirebaseTokenRequest)
        {
            try
            {
                Debug.WriteLine("FCM token => " + updateFirebaseTokenRequest.FirebaseToken);
                var result = accountService.UpdateFirebaseToken(updateFirebaseTokenRequest);
                return Ok(result);
            }catch(Exception ex)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult SendNotification()
        {
            accountService.SendNotification();
            return Ok();
        }

        [HttpPost]
        public IActionResult ProfileList([FromBody]int accId)
        {
            try
            {
                Debug.WriteLine("Profile list api called");
                var result = accountService.GetProfileList(accId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
        [HttpPost]
        public IActionResult DeleteProfile([FromBody] int id)
        {
            try
            {
                Debug.WriteLine("delete Profile  api called");
                var result = accountService.DeleteProfile(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult SwitchDefaultProfile([FromBody] SwitchProfileRequest switchProfileRequest)
        {
            try
            {
                Debug.WriteLine("switch Profile api called");
                var result = accountService.SwitchDefaultProfile(switchProfileRequest);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult GetDefaultProfileData([FromBody] DefaultProfileData viewModel)
        {
            try
            {
                var result = (DefaultProfileData)null;
                Debug.WriteLine("Get default profile data is called");
                 result  = viewModel.ProfileId>0? accountService.UpdateDefaultProfileData(viewModel): accountService.GetDefaultProfileData(viewModel);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return NotFound();
            }

        }

    }
}
