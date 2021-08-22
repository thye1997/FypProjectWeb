using System;
using System.Collections.Generic;
using FypProject.Models;

namespace FypProject.Repository
{
    public interface IUserRepository:IGenericRepository<User>
    {
        public User Find(int Id);
        public IEnumerable<User> GetUserListBySearch(string searchValue);
        public bool FindByNRIC(User user);
    }
}
