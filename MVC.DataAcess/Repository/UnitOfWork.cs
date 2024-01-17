using System.Data.Common;
using MVC.DataAcess.data;
using MVC.DataAcess.Repository.IRepository;
using MVC.Model;

namespace MVC.DataAcess.Repository{

public class UnitOfWork : IUnitOfWork
{
    private AppDbContext _db;
    public IRepositoryCategory category{get;private set;}
    public IRepositoryProduct product{get;private set;}
    public UnitOfWork(AppDbContext db)
    {
        _db = db;
        category = new CategoryRepository(db);
        product = new ProductRepository(db);

    }
       


        public void Save()
        {
           _db.SaveChanges();
        }
    }
}
