using razor_ecom.Data;
using razor_ecom.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;

namespace razor_ecom.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _db;

        public List<Category> CategoryList { get; private set; }

        public IndexModel(AppDbContext db)
        {
            _db = db;
        }

        public void OnGet()
        {
            CategoryList = _db.Categories.ToList();
        }
    }
}
