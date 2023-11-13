using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Common.Entities;

namespace NorthwindWeb.Pages
{
    public class CategoriesModel : PageModel
    {
        public Category Category { get; set; }
        public IEnumerable<Category> Categories { get; set; }

        private static NorthwindContext db;

        public CategoriesModel(NorthwindContext Db)
        {
            db = Db;
        }

        public void OnGet()
        {
            Categories = db.Categories.ToList();
        }

        public IActionResult OnPostAdd()
        {   
            string? Category_CategoryName = Request.Form["Category_CategoryName"];
            string? Category_Description = Request.Form["Category_Description"];

            if(Category_CategoryName is not null){
            Category categoryToAdd = new()
            {
                CategoryName = Category_CategoryName,
                Description = Category_Description
            };

                db.Categories.Add(categoryToAdd);
                db.SaveChanges();
            
            return RedirectToPage("/index");
            }
            return RedirectToPage("/index");
        }
        
        public IActionResult OnPostDelete(int id)
        {
            var categoryToDelete = db.Categories.Find(id);

            if (categoryToDelete != null)
            {
                db.Categories.Remove(categoryToDelete);
                db.SaveChanges();
            }
            return RedirectToPage("/index");
        }
    }
}
