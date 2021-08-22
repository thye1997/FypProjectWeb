using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FypProject.ApiViewModel
{
    public class AccountLoginRequest
    {
        public string EmailAddress { set; get; }
        public string password { set; get; }
    }

    public class AccountLoginResponse
    {
        public int AccId { set; get; }
        public int ProfileId { set; get; }
        public string message { set; get; }
        public bool isSuccess { set; get; }
    }

    public class DefaultProfileRequest
    {
        public int ProfileId { set; get; }
    }

    public class DefaultProfileResponse
    {
        public int accProfileId { set; get; }
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
        public int accId { set; get; }
        public string Relationship { set; get; }
        public string nric { set; get; }
        public string fullName { set; get; }
        public string phoneNumber { set; get; }
        public string gender { set; get; }
        public string dob { set; get; }
    }

    public class DefaultProfileData
    {
        public int accId { set; get; }
        public string accountRegistered { set; get; }
        public int profileId { set; get; }
        public string NRIC { set; get; }
        public string fullName { set; get; }
        public string phoneNumber { set; get; }
        public string gender { set; get; }
        public string DOB { set; get; }
    }

    public class AddProfileResponse:GeneralResponse
    {
    }


    public class GeneralResponse{
        public bool isSuccess { set; get; } 
        public string message { set; get; }
    }

    public class UpdateFirebaseTokenRequest
    {
        public int accId { set; get; }
        public string firebaseToken { set; get; }
    }

    public class UpdateNotificationPrefsRequest
    {
        public int accId { set; get; }
        public bool appPushNotification { set; get; }
        public bool appReminderPushNotification { set; get; }
        public bool smsReminderNotification { set; get; }
    }

    public class NotificationPrefsResponse
    {
        public bool appPushNotification { set; get; }
        public bool appReminderPushNotification { set; get; }
        public bool smsReminderNotification { set; get; }
        public bool hasAccount { set; get; }
    }

    public class SwitchProfileRequest
    {
        public int accId { set; get; }
        public int Id { set; get; }
    }

    public class ProfileListResponse
    {
        public int Id { set; get; }
        public int profileId { set; get; }
        public string fullName { set; get; }
        public string relationship { set; get; }
        public bool isDefault { set; get; }
    }

}
