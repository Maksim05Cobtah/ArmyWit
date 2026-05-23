using ArmiVit.Models;
using ArmiVit.Models.ViewsModel;
using Microsoft.AspNetCore.Mvc;
using ProductApi.Data;
using ProductApi.Models;
using Microsoft.AspNetCore.Authorization;

namespace Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoriesController : Controller
    {
        private readonly AppDbContext _context;

        public CategoriesController(AppDbContext context)
        {
            _context = context;
        }

    public IActionResult AddCategories()
    {
        var categories = _context.Categories
            .Where(x => !x.IsDeleted)
            .ToList();
            var model = new CategoryViewModel
        {
            Categories = categories
        };


        return View(model);
    }

        public IActionResult Delete(int id)
        {
            var category = _context.Categories.Find(id);

            category.IsDeleted = true;
            _context.SaveChanges();
            return RedirectToAction("AddCategories");
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
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var category = _context.Categories.Find(id);

            if (category == null)
            {
                return NotFound();
            }

            var model = new CategoryViewModel
            {
                Id = category.Id,
                Name = category.Name
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CategoryViewModel model)
        {
            var category = _context.Categories.Find(model.Id);

            if (category == null)
            {
                return NotFound();
            }

            category.Name = model.Name;

            _context.Categories.Update(category);
            _context.SaveChanges();

            return RedirectToAction("AddCategories");
        }

    }
}
