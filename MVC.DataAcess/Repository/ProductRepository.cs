using System.Linq.Expressions;
using MVC.DataAcess.data;
using MVC.DataAcess.Repository.IRepository;
using MVC.DataAcess.Repository;
using MVC.Model;

namespace MVC.DataAcess.Repository
{

    public class ProductRepository : Repo<Product>, IRepositoryProduct
    {

        private AppDbContext _db;
        public ProductRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(Product obj)
        {
            _db.Product.Update(obj);
        }

       
    }
}
