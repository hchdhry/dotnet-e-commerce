using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using razor_ecom.Data;
using razor_ecom.models;

namespace razor_ecom.Pages.Categories
{
  
    public class EditModel : PageModel
    {
        private readonly AppDbContext _db;
        [BindProperty]
        public Category Category { get; set; }
        public EditModel(AppDbContext db)
        {
            _db = db;
        }
        public void OnGet(int? id)
        {
            if (id != null && id != 0)
            {
                Category = _db.Categories.Find(id);
            }
        }

        public IActionResult OnPost(int id)
        {
            Category existingCategory = _db.Categories.Find(id);

            if (existingCategory != null)
            {
              
                existingCategory.Name = Category.Name;
                existingCategory.DisplayOrderNumber = Category.DisplayOrderNumber;

                _db.Categories.Update(existingCategory);
                _db.SaveChanges();

                TempData["success"] = "Category updated successfully";
                return RedirectToPage("index");
            }

            return NotFound();
        }


    }
}