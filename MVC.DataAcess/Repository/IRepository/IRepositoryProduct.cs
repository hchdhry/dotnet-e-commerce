using MVC.Model;

namespace MVC.DataAcess.Repository.IRepository{

public interface IRepositoryProduct:IRepository<Product>
{
    void Save();
    void Update(Product obj);

}
}
