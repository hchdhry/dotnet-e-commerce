using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using razor_ecom.Data;
using razor_ecom.models;
namespace razor_ecom.Pages.Categories{
public class DeleteModel:PageModel
{
    private readonly AppDbContext _db;

 
    public Category Category;

    public DeleteModel(AppDbContext db)
    {
        _db = db;
    }

    public IActionResult OnGet(int id)
    {
        Category = _db.Categories.Find(id);
        if(Category == null)
        {
            return NotFound();
        }
        return Page();
    }

    public IActionResult OnPost(int id)
    {
        Category? obj = _db.Categories.Find(id);
        if(obj==null){
            return NotFound();
       
        }
        _db.Categories.Remove(obj);
        _db.SaveChanges();
        return RedirectToPage("index");
    }
    
}
}