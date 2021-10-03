using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FypProject.Repository
{
    public interface IGenericRepository<T> where T: class
    {
        public void Add(T obj);
        public T Update(T obj);
        public void Delete(int Id);
        public IQueryable<T> Where(Expression<Func<T, bool>> exp);
        public IQueryable<T> ToQueryable();
        public IQueryable<T> List();
        public int SaveChanges();
    }
}
