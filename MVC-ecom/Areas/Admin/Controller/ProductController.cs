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
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnviroment)
        {
            _UnitOfWork = unitOfWork;
            _WebHostEnviroment = webHostEnviroment;
        }


        public IActionResult Index()
        {
            List<Product> objProductList = _UnitOfWork.product.GetAll(includeProperties:"Category").ToList();
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
        public IActionResult Upsert(ProductVM productVM, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _WebHostEnviroment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, "images", "product");

                    if (!string.IsNullOrEmpty(productVM.product.ImageURL))
                    {
                       
                        var oldImagePath =
                            Path.Combine(wwwRootPath, productVM.product.ImageURL.TrimStart('\\'));

                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    productVM.product.ImageURL = @"\images\product\" + fileName;
                }

                if (productVM.product.Id == 0)
                {
                    _UnitOfWork.product.Add(productVM.product);
                }
                else
                {
                    _UnitOfWork.product.Update(productVM.product);
                }

                _UnitOfWork.Save();
                TempData["success"] = "Product created successfully";
                return RedirectToAction("Index");
            }
            else
            {
                productVM.CategoryList = _UnitOfWork.category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
                return View(productVM);
            }
        }






     
        //api call//

        [HttpGet]
        public IActionResult getAll()
        {
            List<Product> objProductList = _UnitOfWork.product.GetAll(includeProperties: "Category").ToList();
            return Json(new{data = objProductList});
           
        }
      
    
        public IActionResult Delete(int? id)
        {
            var productToBeDeleted = _UnitOfWork.product.get(u => u.Id == id);
            if (productToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            var oldImagePath =
                           Path.Combine(_WebHostEnviroment.WebRootPath,
                           productToBeDeleted.ImageURL.TrimStart('\\'));

            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            _UnitOfWork.product.Remove(productToBeDeleted);
            _UnitOfWork.Save();

            return Json(new { success = true, message = "Delete Successful" });
        }
    } 
}


