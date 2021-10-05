using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FypProject.ApiViewModel
{
    public class AccountLoginRequest
    {
        public string EmailAddress { set; get; }
        public string Password { set; get; }
    }

    public class AccountLoginResponse
    {
        public int AccId { set; get; }
        public int ProfileId { set; get; }
        public string Message { set; get; }
        public bool IsSuccess { set; get; }
    }

    public class DefaultProfileRequest
    {
        public int ProfileId { set; get; }
    }

    public class DefaultProfileResponse
    {
        public int AccProfileId { set; get; }
        public bool HasDefault { set; get; }
    }

    public class SearchProfileResponse
    {
        public int ProfileId { set; get; }
        public string FullName { set; get; }
        public string PhoneNumber { set; get; }
        public string Gender { set; get; }
        public string DOB { set; get; }
        public bool ProfileExist { set; get; }
    }

    public class AddProfileRequest
    {
        public int ProfileId { set; get; }
        public int AccId { set; get; }
        public string Relationship { set; get; }
        public string NRIC { set; get; }
        public string FullName { set; get; }
        public string PhoneNumber { set; get; }
        public string Gender { set; get; }
        public string DOB { set; get; }
    }
    public class AddNewProfileRequest
    {
        public int AccId { set; get; }
        public string Relationship { set; get; }
        public string NRIC { set; get; }
        public string FullName { set; get; }
        public string PhoneNumber { set; get; }
        public string Gender { set; get; }
        public string DOB { set; get; }
    }

    public class DefaultProfileData
    {
        public int AccId { set; get; }
        public string AccountRegistered { set; get; }
        public int ProfileId { set; get; }
        public string NRIC { set; get; }
        public string FullName { set; get; }
        public string PhoneNumber { set; get; }
        public string Gender { set; get; }
        public string DOB { set; get; }
    }

    public class AddProfileResponse:GeneralResponse
    {
    }


    

    public class UpdateFirebaseTokenRequest
    {
        public int AccId { set; get; }
        public string FirebaseToken { set; get; }
    }

    public class UpdateNotificationPrefsRequest
    {
        public int AccId { set; get; }
        public bool AppPushNotification { set; get; }
        public bool AppReminderPushNotification { set; get; }
        public bool SmsReminderNotification { set; get; }
    }

    public class NotificationPrefsResponse
    {
        public bool AppPushNotification { set; get; }
        public bool AppReminderPushNotification { set; get; }
        public bool SmsReminderNotification { set; get; }
        public bool HasAccount { set; get; }
    }

    public class SwitchProfileRequest
    {
        public int AccId { set; get; }
        public int Id { set; get; }
    }

    public class ProfileListResponse
    {
        public int Id { set; get; }
        public int ProfileId { set; get; }
        public string FullName { set; get; }
        public string Relationship { set; get; }
        public bool IsDefault { set; get; }
    }

}
