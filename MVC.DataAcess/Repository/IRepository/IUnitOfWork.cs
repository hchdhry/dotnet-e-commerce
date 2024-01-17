using MVC.DataAcess.data;

namespace MVC.DataAcess.Repository.IRepository{

public interface IUnitOfWork
{
    IRepositoryCategory category {get;}
    IRepositoryProduct product {get;}
    
    void Save();

}
}
