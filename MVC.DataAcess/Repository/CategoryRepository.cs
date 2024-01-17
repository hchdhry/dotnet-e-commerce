using System.Linq.Expressions;
using MVC.DataAcess.data;
using MVC.DataAcess.Repository.IRepository;
using MVC.DataAcess.Repository;
using MVC.Model;

namespace MVC.DataAcess.Repository{

public class CategoryRepository : Repo<Category>,IRepositoryCategory
{

    private AppDbContext _db;
    public CategoryRepository(AppDbContext db): base(db)
    {
        _db = db;
    }

    public void Save()
    {
        _db.SaveChanges();
    }

    public void update(Category obj)
    {
        _db.Categories.Update(obj);
    }
}
}
