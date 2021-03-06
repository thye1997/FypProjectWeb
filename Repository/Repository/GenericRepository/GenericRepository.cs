using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FypProject.Base;
using FypProject.Models;
using FypProject.Models.DBContext;
using Microsoft.EntityFrameworkCore;

namespace FypProject.Repository
{
    public  class GenericRepository<T>: IGenericRepository<T>  
        where T : class, IBusinessEntity, new()
    {
        protected readonly AppDbContext _dbContexts;
        private readonly DbSet<T> entities;
        public GenericRepository(AppDbContext dbContext)
        {
            //Debug.WriteLine($"This is called for => {typeof(T).Name}");
            _dbContexts = dbContext;
            entities = _dbContexts.Set<T>();
        }
 
        public virtual void Add(T obj)
        {
            entities.Add(obj);
            _dbContexts.SaveChanges();

        }

        public virtual T Update (T obj)
        { 
                T objs = entities.Find(obj.Id);
                if (objs != null)
                {
                _dbContexts.Entry(objs).State = EntityState.Detached;
                var Attach = _dbContexts.Attach(obj);
                Attach.State =EntityState.Modified;
                _dbContexts.SaveChanges();
                }
            return objs;
            
        }

        public virtual void Delete(int Id)
        {
            T obj = entities.Find(Id);
            if (obj != null)
            {
                entities.Remove(obj);
                _dbContexts.SaveChanges();
            }
        }

        public virtual IQueryable<T> Where(Expression<Func<T, bool>> exp)
        {
            return ToQueryable().Where(exp);
        }

        public virtual IQueryable<T> ToQueryable()
        {
            return entities.AsQueryable();
        }

        public  IQueryable<T> List()
        {
            var obj = entities.AsQueryable();
            return obj;
        }

        public int SaveChanges()
        {
           return _dbContexts.SaveChanges();
        }
    }
}
