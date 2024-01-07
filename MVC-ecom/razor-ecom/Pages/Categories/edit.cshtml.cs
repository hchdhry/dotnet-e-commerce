using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using razor_ecom.Data;
using razor_ecom.models;

namespace razor_ecom.Pages.Categories{
public class EditModel : PageModel
{
    private readonly AppDbContext _db;

    [BindProperty]
    public Category Category {get; set;}
        public EditModel(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult OnGet(int id)
        {

            Category = _db.Categories.Find(id);

            if (Category == null)
            {

                return NotFound();
            }

            return Page();
        }

        public IActionResult OnPost()
        {
           if(ModelState.IsValid){
                _db.Categories.Update(Category);
                _db.SaveChanges();
                TempData["success"] = "category updated successfully";
                return RedirectToPage("index");
           }
            return RedirectToPage("index");
        }
    }


}

    
