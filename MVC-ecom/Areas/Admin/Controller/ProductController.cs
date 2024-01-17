using MVC.Model;
using Microsoft.AspNetCore.Mvc;
using MVC.DataAcess.Repository.IRepository;
using MVC.Model.ViewModel;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVC_ecom.Controllers
{
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _UnitOfWork;
        public ProductController(IUnitOfWork unitOfWork)
        {
            _UnitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<Product> objProductList = _UnitOfWork.product.GetAll().ToList();
            return View(objProductList);
        }

        public IActionResult Create()
        {
            IEnumerable<SelectListItem> CategoryList = _UnitOfWork.product.GetAll().Select
            (u => new SelectListItem{
                Text = u.Title,
                Value = u.Id.ToString()
            });

            ProductVM productVM = new()
            {
                CategoryList = CategoryList,
                product = new Product()

            };
            
            
            
            
            return View(productVM);
        }

        [HttpPost]    
          public IActionResult Create(Product obj)
        {
            List<Product> objProductList = _UnitOfWork.product.GetAll().ToList();
            foreach (Product Value in objProductList)
            {
                if (Value.Title.ToLower() == obj.Title.ToLower())
                {
                    ModelState.AddModelError("name", $"{obj.Title} already exists");
                }
            }

            if (ModelState.IsValid)
            {
                _UnitOfWork.product.Add(obj);
                _UnitOfWork.Save();

                return RedirectToAction("index");
            }
            return View();
        }


        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Product? productFromDB = _UnitOfWork.product.get(u => u.Id == id);
            if (productFromDB == null)
            {
                return NotFound();
            }
            Console.WriteLine(productFromDB);

            return View(productFromDB);
        }

        [HttpPost]
        public IActionResult Edit(Product obj)
        {
            if (ModelState.IsValid)
            {
                _UnitOfWork.product.Update(obj);
                _UnitOfWork.Save();
                TempData["success"] = "Product updated successfully";
                return RedirectToAction("index");
            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Product? productFromDB = _UnitOfWork.product.get(u => u.Id == id);
            if (productFromDB == null)
            {
                return NotFound();
            }

            return View(productFromDB);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Product obj = _UnitOfWork.product.get(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }

            _UnitOfWork.product.Remove(obj);
            _UnitOfWork.Save();
            TempData["success"] = "Product deleted successfully";
            return RedirectToAction("index");
        }
    }
}