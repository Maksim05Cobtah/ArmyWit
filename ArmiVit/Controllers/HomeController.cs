using ArmiVit.Models;
using ArmiVit.Models.ViewsModel;
using Microsoft.AspNetCore.Mvc;
using Data;
using System.Diagnostics;

namespace ArmiVit.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)

        {
            _context = context;
        }
 public IActionResult Index(string? searchTerm, decimal? minPrice, decimal? maxPrice)
    {
        var productsQuery = _context.Products.AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchTerm))

        if (minPrice.HasValue)
        {
            productsQuery = productsQuery.Where(p => p.Price >= minPrice.Value);
        }

        // ФІЛЬТР ПО ЦІНІ ДО
        if (maxPrice.HasValue)
        {
            productsQuery = productsQuery.Where(p => p.Price <= maxPrice.Value);
        }

        var products = productsQuery.ToList();
        var categories = _context.Categories.ToList();

        var model = new ProductViewModel
        {
            // Беремо лише ті товари, які НЕ видалені (IsDeleted == false)
            var productsQuery = _context.Products.Where(p => !p.IsDeleted).AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                productsQuery = productsQuery.Where(p => p.Name.Contains(searchTerm) || p.Description.Contains(searchTerm));
            }
        return View(model);
    }

    public IActionResult Category(int id)
    {
        var category = _context.Categories.FirstOrDefault(c => c.Id == id);

            var products = productsQuery.ToList();
            var categories = _context.Categories.Where(x => !x.IsDeleted).ToList();

            var model = new ProductViewModel
            {
                Categories = categories,
                Products = products,
                SearchTerm = searchTerm
            };

            return View(model);
        }

        public IActionResult Category(int id)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            // Тут також показуємо лише активні товари з цієї категорії
            var products = _context.Products
                .Where(p => p.CategoryId == id && !p.IsDeleted)
                .ToList();

            var model = new ProductViewModel
            {
                Categories = _context.Categories.ToList(),
                Products = products
            };

            ViewBag.CategoryName = category.Name;

            return View(model);
        }

        public IActionResult AboutMe()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
    }
}