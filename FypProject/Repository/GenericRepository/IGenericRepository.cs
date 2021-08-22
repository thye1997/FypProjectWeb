using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FypProject.Repository
{
    public interface IGenericRepository<T> where T: class
    {
        public void Add(T obj);
        public T Update(T obj);
        public void Delete(int Id);
        public IEnumerable<T> List();
        public int SaveChanges();
    }
}
