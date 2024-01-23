using MVC.Model;
using MVC.Models.ViewModel;
using MVC.DataAcess;
using Microsoft.AspNetCore.Mvc;
using MVC.DataAcess.Repository.IRepository;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing.Constraints;

namespace MVC_ecom.Controllers
{
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _UnitOfWork;
        private readonly IWebHostEnvironment _WebHostEnviroment;
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _UnitOfWork = unitOfWork;
            _WebHostEnviroment = webHostEnvironment;
        }


        public IActionResult Index()
        {
            List<Product> objProductList = _UnitOfWork.product.GetAll().ToList();
            return View(objProductList);
        }

        public IActionResult Upsert(int? id)
        {
            ProductVM productVM = new()
            {
                CategoryList = _UnitOfWork.category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                product = new Product()
            };
            if (id == null || id == 0)
            {
                return View(productVM);
            }
            else
            {
                productVM.product = _UnitOfWork.product.get(u => u.Id == id);
                return View(productVM);
            }


        }


        [HttpPost]
        public IActionResult Upsert(ProductVM obj, IFormFile? file)
        {
            List<Product> objProductList = _UnitOfWork.product.GetAll().ToList();

            foreach (Product value in objProductList)
            {
                if (value.Title.ToLower() == obj.product.Title.ToLower())
                {
                    ModelState.AddModelError("name", $"{obj.product.Title} already exists");
                }
            }

            if (ModelState.IsValid && file!=null)
            {
              
                string wwwRootPath = _WebHostEnviroment.WebRootPath;
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string productPath = Path.Combine(wwwRootPath, "images", "product");


                 Directory.CreateDirectory(productPath);

                 using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                 {
                     file.CopyTo(fileStream);
                 }

                obj.product.ImageURL = Path.Combine("/images/product", fileName);

                _UnitOfWork.product.Add(obj.product);
                _UnitOfWork.Save();

                return RedirectToAction("index");
                
            }
            
            else
            {

                obj.CategoryList = _UnitOfWork.category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
                return View(obj);
            }
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