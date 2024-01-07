using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using razor_ecom.Data;
using razor_ecom.models;

namespace razor_ecom.Pages.Categories
{
public class CreateModel : PageModel
{
    private readonly AppDbContext _db;
    [BindProperty]
    public Category Category {get ; set ;}

    public CreateModel(AppDbContext db)
    {
        _db = db;

    }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            _db.Categories.Add(Category);
            _db.SaveChanges();
            return RedirectToPage("index");
        }
    }
}