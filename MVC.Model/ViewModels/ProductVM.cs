using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVC.Model.ViewModel{

public class ProductVM
{
    public Product product{get;set;}
    [ValidateNever]
    public IEnumerable<SelectListItem> CategoryList {get;set;}

}
}
