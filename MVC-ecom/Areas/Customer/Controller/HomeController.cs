using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MVC.DataAcess.Repository.IRepository;
using MVC.Model;
using MVC_ecom.Models;

namespace MVC_ecom.Controllers;
[Area("Customer")]


public class HomeController : Controller
{

    private readonly IUnitOfWork _UnitofWork;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger,IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _UnitofWork = unitOfWork;

    }

    public IActionResult Index()
    {
       IEnumerable<Product> productList = _UnitofWork.product.GetAll(includeProperties: "Category");
        
        return View(productList);
    }
    public IActionResult Details(int ? productId)
    {
        Product Product = _UnitofWork.product.get(u=>u.Id == productId,includeProperties: "Category");
        return View(Product);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
