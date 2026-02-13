using ArmiVit.Models;
using ArmiVit.Models.ViewsModel;
using Microsoft.AspNetCore.Mvc;
using ProductApi.Data;
using ProductApi.Models;

namespace Controllers
{
    
    public class CategoriesController : Controller
    {
        private readonly AppDbContext _context;

        public CategoriesController(AppDbContext context)
        {
            _context = context;
        }

    public IActionResult AddCategories()
    {
        var categories = _context.Categories.ToList();
        var model = new CategoryViewModel
        {
            Categories = categories
        };


        return View(model);
    }
 
        [HttpPost]
        public async Task<IActionResult> AddCategory(CategoryViewModel model)
        {
          
            var category = new Categories
            {
                Name = model.Name
            };  
        
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return RedirectToAction("AddCategories");
        }

    }
}
