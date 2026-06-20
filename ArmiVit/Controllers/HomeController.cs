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

        public IActionResult Index(
            string? searchTerm,
            decimal? minPrice,
            decimal? maxPrice)
        {
            // Беремо лише НЕ видалені товари
            var productsQuery = _context.Products
                .Where(p => !p.IsDeleted)
                .AsQueryable();

            // Пошук
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                productsQuery = productsQuery.Where(p =>
                    p.Name.Contains(searchTerm) ||
                    p.Description.Contains(searchTerm));
            }

            // Мінімальна ціна
            if (minPrice.HasValue)
            {
                productsQuery = productsQuery.Where(p =>
                    p.Price >= minPrice.Value);
            }

            // Максимальна ціна
            if (maxPrice.HasValue)
            {
                productsQuery = productsQuery.Where(p =>
                    p.Price <= maxPrice.Value);
            }

            var products = productsQuery.ToList();

            // Категорії теж тільки активні
            var categories = _context.Categories
                .Where(x => !x.IsDeleted)
                .ToList();

            var model = new ProductViewModel
            {
                Products = products,
                Categories = categories,
                SearchTerm = searchTerm
            };

            return View(model);
        }

        public IActionResult Category(int id)
        {
            var category = _context.Categories
                .FirstOrDefault(c => c.Id == id && !c.IsDeleted);

            if (category == null)
            {
                return NotFound();
            }

            // Тільки НЕ видалені товари
            var products = _context.Products
                .Where(p =>
                    p.CategoryId == id &&
                    !p.IsDeleted)
                .ToList();

            var model = new ProductViewModel
            {
                Categories = _context.Categories
                    .Where(x => !x.IsDeleted)
                    .ToList(),

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

        [ResponseCache(
            Duration = 0,
            Location = ResponseCacheLocation.None,
            NoStore = true)]

        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId =
                    Activity.Current?.Id ??
                    HttpContext.TraceIdentifier
            });
        }


        public IActionResult Pigeon(int id)
        {
            var product = _context.Products.Find(id);
            return View(product);
           
        }

    }
}