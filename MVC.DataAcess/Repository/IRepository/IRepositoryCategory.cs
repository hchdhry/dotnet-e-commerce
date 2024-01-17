using MVC.DataAcess.Repository;
using MVC.Model;

namespace MVC.DataAcess.Repository.IRepository{

public interface IRepositoryCategory : IRepository<Category>
{
    void update(Category obj);
    void Save();
}
}