using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MVC.DataAcess.Repository.IRepository
{

public interface IRepository<T> where T : class
{
    IEnumerable<T> GetAll(string? includeProperties = null);
    T get(Expression<Func<T,bool>>filter, string? includeProperties = null);
    void Add(T entity);
    void Remove(T entity);
    void RemoveRange(IEnumerable<T> entity);



}
}