using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using FypProject.ApiViewModel;
using FypProject.Models;
using FypProject.Utils;
using FypProject.Repository;
using FypProject.Models.DBContext;

namespace FypProject.Services
{
    public class AccountService
    {
        private readonly IUserRepository _userRepository;
        private readonly IGenericRepository<Account> _accRepository;
        private readonly IGenericRepository<AccountProfile> _accProfileRepository;
        private readonly AppDbContext _appDbContext;

        public AccountService(IUserRepository userRepository, IGenericRepository<Account> accRepository, IGenericRepository<AccountProfile> accProfileRepository, AppDbContext appDbContext)
        {
            _userRepository = userRepository;
            _accRepository = accRepository;
            _accProfileRepository = accProfileRepository;
            _appDbContext = appDbContext;
        }
        public GeneralResponse AccountRegister(AccountLoginRequest accountLoginRequest)
        {
            var account = _accRepository.Where(c => c.EmailAddress == accountLoginRequest.EmailAddress).FirstOrDefault();
            if (account == null)
            {
                var newAccount = new Account
                {
                    EmailAddress = accountLoginRequest.EmailAddress,
                    Password = BCrypt.Net.BCrypt.HashPassword(accountLoginRequest.Password),
                    AppointmentPushReminderEnabled = true,
                    PushNotificationEnabled= true,
                    AppointmentSMSReminderEnabled = true                 
                };
                _accRepository.Add(newAccount);
                return new GeneralResponse
                {
                    message = "User Registered Successfully.",
                    isSuccess = true
                };
            }
            else
            {
                return new GeneralResponse
                {
                    message = "Email Address already Registered.",
                    isSuccess = false
                };
            }
        }

        public GeneralResponse AddNewProfile(AddProfileRequest viewModel)
        {
            //check exist nric
            var profileExist = _userRepository.Where(c => c.NRIC == viewModel.NRIC).FirstOrDefault();
            if (profileExist != null )
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
                    _userRepository.Add(newProfile);
                    // get the profile to be added to accountProfile
                    var profile = _userRepository.Where(c => c.NRIC == viewModel.NRIC).FirstOrDefault();
                    var accProfile = _accProfileRepository.Where(c => c.accountId == viewModel.AccId).ToList();
                    if (accProfile.Count > 0)
                    {
                        _accProfileRepository.Add(new AccountProfile
                        {
                            accountId = viewModel.AccId,
                            Relationship = viewModel.Relationship,
                            userId = profile.Id,
                            isDefault = false // since already got exist profile, one of it will be default                      
                        });
                    }
                    else
                    {
                        _accProfileRepository.Add(new AccountProfile
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
            var account = _accRepository.Where(c => c.EmailAddress == accountLoginRequest.EmailAddress).FirstOrDefault();
            if(account == null)
            {
                //Debug.WriteLine("account not found");
                return new AccountLoginResponse
                {
                    Message = "Invalid email or password",
                    IsSuccess = false
                };
            }
            else
            {
                if(BCrypt.Net.BCrypt.Verify(accountLoginRequest.Password, account.Password))
                {
                    return new AccountLoginResponse
                    {
                        AccId = account.Id,
                        Message = "User login success",
                        IsSuccess = true
                    };
                }
                else
                {
                    return new AccountLoginResponse
                    {
                        Message = "Invalid email or password",
                        IsSuccess = false
                    };

                }      
            }
        }

        public DefaultProfileResponse CheckDefaultProfile(int AccId)
        {
            var HasDefault = _accProfileRepository.Where(c => c.accountId == AccId && c.isDefault).FirstOrDefault();
            //Debug.WriteLine("account profile count=> " + HasDefault.Count);
            if(HasDefault !=null)
            {
                return new DefaultProfileResponse
                {
                    AccProfileId = HasDefault.Id,
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
            var SearchProfile = _userRepository.Where(c => c.NRIC == NRIC).FirstOrDefault();
            if(SearchProfile != null)
            {
                //Debug.WriteLine("value not null");
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
            if(addProfileRequest.ProfileId > 0)
            {
               var accProfile = (AccountProfile)null;
               //var accountExist = _accRepository.ToQueryable().Any(c=>c.Id == addProfileRequest.AccId); // fast check if account exist
               //var profile = _userRepository.Where(c => c.Id == addProfileRequest.ProfileId).FirstOrDefault();
               var accProfileHasDefault = _accProfileRepository.ToQueryable().Any(c => c.accountId == addProfileRequest.AccId && c.isDefault);
                if (_userRepository.ToQueryable().Any(c => c.Id == addProfileRequest.ProfileId))
                {   
                    //Debug.WriteLine("account id not empty=> "+ account.Id);
                    if (accProfileHasDefault == false) // first profile added after account created
                    {
                        accProfile = new AccountProfile
                        {
                            accountId = addProfileRequest.AccId,
                            userId = addProfileRequest.ProfileId,
                            Relationship = addProfileRequest.Relationship,
                            isDefault = true 
                        };
                    }
                    else
                    {
                        accProfile = new AccountProfile
                        {
                            accountId = addProfileRequest.AccId,
                            userId = addProfileRequest.ProfileId,
                            Relationship = addProfileRequest.Relationship,
                            isDefault = false
                        };
                    }
                    _accProfileRepository.Add(accProfile);

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

        public GeneralResponse UpdateFirebaseToken(UpdateFirebaseTokenRequest updateFirebaseTokenRequest) // can have a table store multiple tokens of same account (assume user might use different phone to login)
        {
            var result = _accRepository.Where(c => c.Id == updateFirebaseTokenRequest.AccId).FirstOrDefault();

            if (result != null)
            {
                result.FirebaseToken = updateFirebaseTokenRequest.FirebaseToken;
                _accProfileRepository.SaveChanges();
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
        public NotificationPrefsResponse GetNotifcationPrefsSettings(int accId)
        {
            var account = _accRepository.Where(c => c.Id == accId).FirstOrDefault();

            if (account != null)
            {
                return new NotificationPrefsResponse
                {
                    AppPushNotification = account.PushNotificationEnabled,
                    AppReminderPushNotification = account.AppointmentPushReminderEnabled,
                    SmsReminderNotification = account.AppointmentSMSReminderEnabled,
                    HasAccount = true
                };
            }
            else
            {
                return new NotificationPrefsResponse
                {
                    HasAccount = false
                };
            }
        }
        public GeneralResponse UpdateNotificationPreferences(UpdateNotificationPrefsRequest obj)
        {
           var account = _accRepository.Where(c => c.Id == obj.AccId).FirstOrDefault();
            if (account != null)
            {
                account.PushNotificationEnabled = obj.AppPushNotification;
                account.AppointmentPushReminderEnabled = obj.AppReminderPushNotification;
                account.AppointmentSMSReminderEnabled = obj.SmsReminderNotification;
                _accProfileRepository.SaveChanges();
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
            var accProfile = _accProfileRepository.ToQueryable();
            var user = _userRepository.ToQueryable();
            var profileList = from a in accProfile
                              join u in user on a.userId equals u.Id
                              where a.accountId == accId
                         select new ProfileListResponse
                         {
                             Id = a.Id,
                             ProfileId = a.userId,
                             FullName = u.FullName,
                             Relationship = a.Relationship,
                             IsDefault = a.isDefault
                         };
                return profileList.ToList();            
        }
        public GeneralResponse DeleteProfile(int Id)
        {
            var accProfile = _accProfileRepository.Where(c => c.Id == Id).FirstOrDefault();
            if(accProfile != null)
            {
                _accProfileRepository.Delete(Id);
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
            var accProfile = _accProfileRepository.Where(c => c.accountId == obj.AccId).ToList();
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
                _accProfileRepository.SaveChanges();
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
            var accProfile = _accProfileRepository.ToQueryable();
            var user = _userRepository.ToQueryable();
            var account = _accRepository.ToQueryable();
            var result = from u in user
                         join ap in accProfile on u.Id equals ap.userId
                         where ap.accountId == obj.AccId && ap.isDefault
                         select new DefaultProfileData
                         {
                             AccountRegistered = account.Where(c=>c.Id == obj.AccId).Select(account => account.EmailAddress).FirstOrDefault(),
                             ProfileId = ap.userId,
                             NRIC = u.NRIC,
                             FullName = u.FullName,
                             PhoneNumber = u.PhoneNumber,
                             DOB = u.DOB,
                             Gender = u.Gender,
                         };
            return result.FirstOrDefault();
        }
       
        public DefaultProfileData UpdateDefaultProfileData(DefaultProfileData obj)
        {
            var profile = _userRepository.Where(c => c.Id == obj.ProfileId).FirstOrDefault();
            //Debug.WriteLine("profile id =>" + profile.Id);
            if (profile != null)
            {
                profile.FullName = obj.FullName;
                profile.PhoneNumber = obj.PhoneNumber;
                profile.Gender = obj.Gender;
                profile.DOB = obj.DOB;
                _userRepository.SaveChanges();
                return obj;
            }
            else
            {
                return null;
            }
        }

    }
}
