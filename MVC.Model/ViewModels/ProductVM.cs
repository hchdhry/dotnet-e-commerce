using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC.Model;

namespace MVC.Models.ViewModel{

public class ProductVM
{
    public Product product{get;set;}
    [ValidateNever]
    public IEnumerable<SelectListItem> CategoryList {get;set;}

}
}
