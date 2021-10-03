using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using FypProject.CustomException;
using FypProject.Models;
using FypProject.Repository;
using FypProject.ViewModel;

namespace FypProject.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IGenericRepository<MedicalHistory> _medHistoryRepository;
        public UserService(
            IUserRepository userRepository,
            IGenericRepository<MedicalHistory> medicalHistoryRepository        
            )
        {
            _userRepository = userRepository;
            _medHistoryRepository = medicalHistoryRepository;
        }

        public User AddPatient(User obj)
        {
            if (!_userRepository.FindByNRIC(obj))
            {
                _userRepository.Add(obj);
                return obj;
            }
            throw new BusinessException("Patient has existed.");
        }

        public MedicalHistory AddMedicalHistory(UserViewModel obj)
        {
            var medHistory = new MedicalHistory
            {
                Description = obj.Description,
                userId = obj.userId
            };

            _medHistoryRepository.Add(medHistory);
            return medHistory;

        }
        public List<User> GetUserList()
        {
            return _userRepository.ToQueryable().ToList();
        }

        public User UpdateUserDetail(User obj)
        {
            var result = obj.Id > 0 ? _userRepository.Update(obj) : null;
            return result;
        }

        public UserViewModel UserDetail(int Id)
        {
            UserViewModel userViewModel = new UserViewModel();
            userViewModel.user = _userRepository.Find(Id);
            DateTime DOB = DateTime.Parse(userViewModel.user.DOB);
            userViewModel.user.DOB = DOB.ToString("yyyy-MM-dd"); // change to this format to make it able to show in browser input field type = date
            Debug.WriteLine("DOB of patient=>" + userViewModel.user.DOB);
            return userViewModel;
        }

    }
}
