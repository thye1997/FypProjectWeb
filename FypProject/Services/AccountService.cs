using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using FypProject.ApiViewModel;
using FypProject.Models;
using FypProject.Utils;
using FypProject.Repository;

namespace FypProject.Services
{
    public class AccountService
    {
        private readonly IUserRepository userRepository;
        private readonly IGenericRepository<Account> accRepository;
        private readonly IGenericRepository<AccountProfile> accProfileRepository;

        public AccountService(IUserRepository userRepository, IGenericRepository<Account> accRepository, IGenericRepository<AccountProfile> accProfileRepository)
        {
            this.userRepository = userRepository;
            this.accRepository = accRepository;
            this.accProfileRepository = accProfileRepository;
        }
        public AccountLoginResponse AccountRegister(AccountLoginRequest accountLoginRequest)
        {
            var account = accRepository.List().Where(c => c.EmailAddress == accountLoginRequest.EmailAddress).FirstOrDefault();
            if (account == null)
            {
                var newAccount = new Account
                {
                    EmailAddress = accountLoginRequest.EmailAddress,
                    Password = BCrypt.Net.BCrypt.HashPassword(accountLoginRequest.password),
                    AppointmentPushReminderEnabled = true,
                    PushNotificationEnabled= true,
                    AppointmentSMSReminderEnabled = true                 
                };
                accRepository.Add(newAccount);
                return new AccountLoginResponse
                {
                    message = "User Registered Successfully.",
                    isSuccess = true
                };
            }
            else
            {
                return new AccountLoginResponse
                {
                    message = "Email Address already Registered.",
                    isSuccess = false
                };
            }
        }

        public GeneralResponse AddNewProfile(AddProfileRequest viewModel)
        {
            //check exist nric
            var exist = userRepository.List().Where(c => c.NRIC == viewModel.NRIC).FirstOrDefault();
            if(exist != null)
            {
                return new GeneralResponse
                {
                    isSuccess = false,
                    message = "Same nric has exist, please try with different nric..."
                };
            }
            else
            {
                //add profile into user
                var newProfile = new User
                {
                    NRIC = viewModel.NRIC,
                    FullName = viewModel.FullName,
                    Gender = viewModel.Gender,
                    DOB = viewModel.DOB,
                    PhoneNumber = viewModel.PhoneNumber
                };
                try
                {
                    userRepository.Add(newProfile);
                    // get the profile to be added to accountProfile
                    var profile = userRepository.List().Where(c => c.NRIC == viewModel.NRIC).FirstOrDefault();
                    var accProfile = accProfileRepository.List().Where(c => c.accountId == viewModel.AccId).ToList();
                    if (accProfile.Count > 0)
                    {
                        accProfileRepository.Add(new AccountProfile
                        {
                            accountId = viewModel.AccId,
                            Relationship = viewModel.Relationship,
                            userId = profile.Id,
                            isDefault = false // since already got exist profile, one of it will be default                      
                        });
                    }
                    else
                    {
                        accProfileRepository.Add(new AccountProfile
                        {
                            accountId = viewModel.AccId,
                            Relationship = viewModel.Relationship,
                            userId = profile.Id,
                            isDefault = true
                        });

                    }

                    return new GeneralResponse
                    {
                        message = "Profile added successfully.",
                        isSuccess = true
                    };

                }catch(Exception ex)
                {
                    return new GeneralResponse
                    {
                        message = "Unknown error occurred.",
                        isSuccess = false
                    };
                };                             
            }
        }

        public AccountLoginResponse AccountLogin(AccountLoginRequest accountLoginRequest)
        {
            var account = accRepository.List().Where(c => c.EmailAddress == accountLoginRequest.EmailAddress).FirstOrDefault();
            if(account == null)
            {
                Debug.WriteLine("account not found");
                return new AccountLoginResponse
                {
                    message = "User account not found",
                    isSuccess = false
                };
            }
            else
            {
                if(BCrypt.Net.BCrypt.Verify(accountLoginRequest.password, account.Password))
                {
                    return new AccountLoginResponse
                    {
                        AccId = account.Id,
                        message = "User login success",
                        isSuccess = true
                    };
                }
                else
                {
                    return new AccountLoginResponse
                    {
                        message = "Invalid email or password",
                        isSuccess = false
                    };

                }      
            }
        }

        public DefaultProfileResponse CheckDefaultProfile(int AccId)
        {
            var HasDefault = accProfileRepository.List().Where(c => c.accountId == AccId && c.isDefault).FirstOrDefault();
            //Debug.WriteLine("account profile count=> " + HasDefault.Count);
            if(HasDefault !=null)
            {
                return new DefaultProfileResponse
                {
                   accProfileId = HasDefault.Id,
                    HasDefault = true
                };
            }
            else
            {
                return new DefaultProfileResponse
                {
                    HasDefault = false
                };
            }
        }

        public SearchProfileResponse SearchExistProfile (string NRIC)
        {
            var SearchProfile = userRepository.List().Where(c => c.NRIC == NRIC).FirstOrDefault();
            if(SearchProfile != null)
            {
                Debug.WriteLine("value not null");
                return new SearchProfileResponse
                {
                    ProfileId = SearchProfile.Id,
                    FullName = SearchProfile.FullName,
                    PhoneNumber = SearchProfile.PhoneNumber,
                    Gender = SearchProfile.Gender,
                    DOB = SearchProfile.DOB,
                    ProfileExist = true
                };
            }
            else
            {
                return new SearchProfileResponse
                {                
                    ProfileExist = false
                };
            }
        }

        public AddProfileResponse AddProfile(AddProfileRequest addProfileRequest)
        {
            var profile = (User)null;
            var account = (Account)null;
            var accProfile = (AccountProfile)null;
            var accProfileIsDefault = (AccountProfile)null;
            if(addProfileRequest.ProfileId != 0)
            {
                account = accRepository.List().Where(c => c.Id == addProfileRequest.AccId).FirstOrDefault();
               profile= userRepository.List().Where(c => c.Id == addProfileRequest.ProfileId).FirstOrDefault();
                accProfileIsDefault = accProfileRepository.List().Where(c => c.accountId == addProfileRequest.AccId && c.isDefault).FirstOrDefault();
                if (profile != null)
                {   
                    Debug.WriteLine("account id not empty=> "+ account.Id);
                    if (accProfileIsDefault == null)
                    {
                        accProfile = new AccountProfile
                        {
                            accountId = account.Id,
                            userId = profile.Id,
                            Relationship = addProfileRequest.Relationship,
                            isDefault = true
                        };
                    }
                    else
                    {
                        accProfile = new AccountProfile
                        {
                            accountId = account.Id,
                            userId = profile.Id,
                            Relationship = addProfileRequest.Relationship,
                            isDefault = false
                        };
                    }
                    accProfileRepository.Add(accProfile);
                    return new AddProfileResponse
                    {
                        isSuccess = true,
                        message = "Profile added successfully."
                    };
                }
                return new AddProfileResponse
                {
                    isSuccess = false,
                    message = "Profile failed to add."
                };
            }
            else
            {
              return  new AddProfileResponse
                {
                    isSuccess = false,
                    message = "Profile failed to add."
              };
            }
        }

        public GeneralResponse UpdateFirebaseToken(UpdateFirebaseTokenRequest updateFirebaseTokenRequest)
        {
            var result = accRepository.List().Where(c => c.Id == updateFirebaseTokenRequest.accId).FirstOrDefault();

            if (result != null)
            {
                result.FirebaseToken = updateFirebaseTokenRequest.firebaseToken;
                accProfileRepository.SaveChanges();
                return new GeneralResponse
                {
                    isSuccess = true,
                    message = "Firebase token updated successfully."
                };
            }
            else
            {
                return new GeneralResponse
                {
                    isSuccess = false,
                    message = "Firebase token update failed."
                };

            }
        }

        public void SendNotification()
        {
            /*FirebaseNotificationHelper firebase = new FirebaseNotificationHelper();
            firebase.SendNotifcation(accProfileRepository);*/
        }
        public NotificationPrefsResponse NotifcationPrefsSettings(int accId)
        {
            var account = (Account)null;
            account = accRepository.List().Where(c => c.Id == accId).FirstOrDefault();

            if (account != null)
            {
                return new NotificationPrefsResponse
                {
                    appPushNotification = account.PushNotificationEnabled,
                    appReminderPushNotification = account.AppointmentPushReminderEnabled,
                    smsReminderNotification = account.AppointmentSMSReminderEnabled,
                    hasAccount = true
                };
            }
            else
            {
                return new NotificationPrefsResponse
                {
                    hasAccount = false
                };
            }
        }
        public GeneralResponse UpdateNotificationPreferences(UpdateNotificationPrefsRequest obj)
        {
            var account = (Account)null;
            account = accRepository.List().Where(c => c.Id == obj.accId).FirstOrDefault();
            if (account != null)
            {
                account.PushNotificationEnabled = obj.appPushNotification;
                account.AppointmentPushReminderEnabled = obj.appReminderPushNotification;
                account.AppointmentSMSReminderEnabled = obj.smsReminderNotification;
                accProfileRepository.SaveChanges();
                return new GeneralResponse
                {
                    isSuccess = true,
                    message = "Notification settings updated successfully."

                };
            }
            else
            {
                return new GeneralResponse
                {
                    isSuccess = false,
                    message = "Notification settings update failed."
                };
            }

        }
        public List<ProfileListResponse> GetProfileList(int accId)
        {
            List<ProfileListResponse> profileList = new List<ProfileListResponse>(); 
            var profile = accProfileRepository.List().Where(c=>c.accountId == accId).ToList();
            if (profile.Count > 0)
            {
                foreach(var n in profile)
                {
                    profileList.Add(new ProfileListResponse
                    {   Id = n.Id,
                        profileId = n.userId,
                        fullName = userRepository.List().Where(c => c.Id == n.userId).FirstOrDefault().FullName,
                        relationship = n.Relationship,
                        isDefault = n.isDefault
                    });
                }

                return profileList;
            }
            else
            {
                return null;
            }
        }
        public GeneralResponse DeleteProfile(int Id)
        {
            var accProfile = accProfileRepository.List().Where(c => c.Id == Id).FirstOrDefault();
            if(accProfile != null)
            {
                accProfileRepository.Delete(Id);
                return new GeneralResponse
                {
                    message = "Profile deleted successfully.",
                    isSuccess = true
                };
            }
            else
            {
                return new GeneralResponse
                {
                    message = "Profile deleted failed.",
                    isSuccess = false
                };

            }
        }
        public GeneralResponse SwitchDefaultProfile(SwitchProfileRequest obj)
        {
            var accProfile = accProfileRepository.List().Where(c => c.accountId == obj.accId).ToList();
            if (accProfile.Count >0)
            {   foreach(var n in accProfile)
                {
                    if(n.Id != obj.Id)
                    {
                        n.isDefault = false;
                    }
                    else
                    {
                        n.isDefault = true;
                    }
                }
                accProfileRepository.SaveChanges();
                return new GeneralResponse
                {
                    message = "Default profile switched successfully.",
                    isSuccess = true
                };
            }
            else
            {
                return new GeneralResponse
                {
                    message = "Default profile switch failed.",
                    isSuccess = false
                };

            }
        }

        public DefaultProfileData GetDefaultProfileData(DefaultProfileData obj)
        {
            var defaultProfileId = accProfileRepository.List().Where(c => c.accountId == obj.accId && c.isDefault).FirstOrDefault().userId;
            if(defaultProfileId >0)
            {
                var defaultProfile = userRepository.List().Where(c => c.Id == defaultProfileId).FirstOrDefault();
                if (defaultProfile != null)
                {
                    return new DefaultProfileData
                    {
                        accountRegistered = accRepository.List().Where(c => c.Id == obj.accId).FirstOrDefault().EmailAddress,
                        profileId = defaultProfileId,
                        NRIC = defaultProfile.NRIC,
                        fullName = defaultProfile.FullName,
                        phoneNumber = defaultProfile.PhoneNumber,
                        DOB = defaultProfile.DOB,
                        gender = defaultProfile.Gender,
                    };
                }

                else
                {
                    return null;
                }
            }
            return null;
        }
       
        public DefaultProfileData UpdateDefaultProfileData(DefaultProfileData obj)
        {
            var profile = userRepository.List().Where(c => c.Id == obj.profileId).FirstOrDefault();
            Debug.WriteLine("profile id =>" + profile.Id);
            if (profile != null)
            {
                profile.FullName = obj.fullName;
                profile.PhoneNumber = obj.phoneNumber;
                profile.Gender = obj.gender;
                profile.DOB = obj.DOB;

                userRepository.SaveChanges();

                return obj;
            }
            else
            {
                return null;
            }
        }

    }
}
