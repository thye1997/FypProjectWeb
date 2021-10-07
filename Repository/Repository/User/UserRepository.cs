using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FypProject.Models.DBContext;
using FypProject.Models;
using Microsoft.EntityFrameworkCore;

namespace FypProject.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {

        public UserRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public IEnumerable<User> GetUserListBySearch(string searchValue)
        {
            List<User> user = new List<User>();
            bool isDigitPresent = searchValue.Any(c => char.IsDigit(c)); /// check if string contain value
            if (isDigitPresent)
            {
                user =_dbContexts.User.Where(c => c.NRIC == searchValue).ToList(); 
            }
            else
            {
                user = _dbContexts.User.Where(c => c.FullName.ToLower() == searchValue.ToLower()).ToList();
            }
            return user;
        }


        public User Find(int Id)
        {
            User user = _dbContexts.User.Find(Id);
            if(user != null)
            {
                return user;
            }
            return null;
        }


        public bool FindByNRIC(User obj)
        {
            User user = new User();
            user = _dbContexts.User.Where(c => c.NRIC == obj.NRIC).FirstOrDefault();

            if (user != null)
            {
                return true;
            }
            return false;
        }
    }
}
