using System.Data.Common;
using Microsoft.AspNetCore.Mvc;
using MVC_ecom.data;
using MVC_ecom.Model;
namespace MVC_ecom.Controllers
{
    public class CategoryController : Controller
    {
        private readonly AppDbContext _db;
        public CategoryController(AppDbContext db)
        {
            _db = db;

        }

        public IActionResult Index()
        {
            List<Category> objCategoryList = _db.Categories.ToList();
            return View(objCategoryList);

        }

        public IActionResult Create()
        {
            return View();

        }


        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if(ModelState.IsValid)
            {
            _db.Categories.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("index");
            }
            return View();

        }
    }
}
