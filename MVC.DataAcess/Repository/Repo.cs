using MVC.DataAcess.Repository;
using MVC.DataAcess.Repository.IRepository;
using System.Linq.Expressions;
using MVC.DataAcess.data;
using Microsoft.EntityFrameworkCore;

namespace MVC.DataAcess.Repository
{

    public class Repo<T> : IRepository<T> where T : class
    {

        private readonly AppDbContext _db;
        internal DbSet<T> dbSet;

        public Repo(AppDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();

        }

        public IEnumerable<T> GetAll()
        {
            IQueryable<T> query = dbSet;
            return query.ToList();
        }

       

        public void Add(T entity)
        {
            dbSet.Add(entity);
        }


        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entity)
        {
            dbSet.RemoveRange(entity);
        }

        public T get(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = dbSet;
            query = query.Where(filter);
            return query.FirstOrDefault();
        }


    }
}