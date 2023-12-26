using System.ComponentModel.DataAnnotations;
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
            List<Category> objCategoryList = _db.Categories.ToList();
            foreach(Category Value in objCategoryList ){
                if(Value.Name.ToLower() == obj.Name.ToLower())
                {
                    ModelState.AddModelError("name",$"{obj.Name} already exists");
                }

            }
            if (obj.Name == obj.DisplayOrderNumber.ToString())
            {
                ModelState.AddModelError("Name","name cannot be the same as display order");
            }
            if(ModelState.IsValid)
            {
            _db.Categories.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("index");
            }
            return View();

        }

        public IActionResult Edit(int? id){
            if(id == null || id == 0)
            {
                return NotFound();

            }
            Category? categoryFromDB = _db.Categories.Find(id);
            if(categoryFromDB == null){
                
                return NotFound();
            }
            Console.WriteLine(categoryFromDB);

            return View(categoryFromDB);
        }
    }
}
