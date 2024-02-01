using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC.DataAcess.Repository.IRepository;
using MVC.DataAcess.data;
using MVC.Model;
namespace MVC_ecom.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _UnitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _UnitOfWork = unitOfWork;

        }

        public IActionResult Index()
        {
            List<Category> objCategoryList = _UnitOfWork.category.GetAll().ToList();
            return View(objCategoryList);

        }

        public IActionResult Create()
        {
            return View();

        }


        [HttpPost]
        public IActionResult Create(Category obj)
        {
            List<Category> objCategoryList = _UnitOfWork.category.GetAll().ToList();
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
                _UnitOfWork.category.Add(obj);
                _UnitOfWork.Save();

                return RedirectToAction("index");
            }
            return View();

        }

        public IActionResult Edit(int? id){
            if(id == null || id == 0)
            {
                return NotFound();

            }
            Category? categoryFromDB = _UnitOfWork.category.get(u => u.Id == id);
            if (categoryFromDB == null){
                
                return NotFound();
            }
            Console.WriteLine(categoryFromDB);

            return View(categoryFromDB);
        }

        [HttpPost]
        public IActionResult Edit (Category obj)
        {
            if(ModelState.IsValid)
            {
                _UnitOfWork.category.update(obj);
                _UnitOfWork.Save();
                TempData["success"] = "category updated successfully";
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
            Category? categoryFromDB = _UnitOfWork.category.get(u =>u.Id==id);
            if (categoryFromDB == null)
            {

                return NotFound();
            }
            

            return View(categoryFromDB);
        }

        [HttpPost,ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Category obj = _UnitOfWork.category.get(u => u.Id == id);
            if(obj == null)
            {
                return NotFound();

            }

            _UnitOfWork.category.Remove(obj);
            _UnitOfWork.Save();
                TempData["success"] = "category deleted successfully";
                return RedirectToAction("index");
            }
            
        }


    }

